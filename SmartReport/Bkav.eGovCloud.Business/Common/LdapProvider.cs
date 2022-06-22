using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Common
{
	/// <summary>
	/// Bkav Corp. - BSO - eGov - eOffice team
	/// Project: eGov Cloud v1.0
	/// Class : LdapProvider - public - BLL
	/// Access Modifiers: 
	/// Create Date : 170812
	/// Author      : TrungVH
	/// Description : Provider giúp truy cập các thông tin từ 1 server LDAP
	/// </summary>
	public class LdapProvider
	{
		private AuthenticationSettings _authenticationSettings;
		private LdapConnection _authenticatedLdapConnection;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="authenticationSettings"></param>
		public LdapProvider(AuthenticationSettings authenticationSettings)
		{
			_authenticationSettings = authenticationSettings;
		}

		//TrinhNVd: Comment vì hàm không test được kết nối SSL
		//        /// <summary>
		//        /// Kiểm tra cấu hình kết nối tới server LDAP
		//        /// </summary>
		//        /// <param name="host">Tên máy chủ LDAP</param>
		//        /// <param name="basedn">DN cấp cao nhất của LDAP</param>
		//        /// <param name="username">Tên đăng nhập</param>
		//        /// <param name="password">Mật khẩu</param>
		//        /// <param name="port">Cổng</param>
		//        /// <returns>true:nếu kết nối thành công và ngược lại</returns>
		//        [System.Security.Permissions.EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
		//        public bool TestConnection(string host, string port, string basedn, string username, string password)
		//        {
		//            var result = false;
		//            try
		//            {
		//                var domain = GenerateDomainLdap(host, port, useSSL, basedn);

		//                var entry = new DirectoryEntry(domain, username, password, AuthenticationTypes.ReadonlyServer);
		//#pragma warning disable 168
		//                var obj = entry.NativeObject;
		//#pragma warning restore 168
		//                result = true;
		//            }
		//            catch (DirectoryServicesCOMException)
		//            {
		//            }
		//            return result;
		//        }

		//        /// <summary>
		//        /// Xác thực người dùng
		//        /// </summary>
		//        /// <param name="host">Tên máy chủ LDAP</param>
		//        /// <param name="port">Cổng</param>
		//        /// <param name="basedn">DN cấp cao nhất của LDAP</param>
		//        /// <param name="usernameSetting">Tên đăng nhập đã được cấu hình</param>
		//        /// <param name="passwordSetting">Mật khẩu đã được cấu hình</param>
		//        /// <param name="usernameAuthenticate">Tên đăng nhập cần xác thực</param>
		//        /// <param name="passwordAuthenticate">Mật khẩu cần xác thực</param>
		//        /// <param name="authenticationFilter">Bộ lọc để tìm được chính xác người dùng</param>
		//        /// <returns>true: nếu tên đăng nhập và mật khẩu chính xác và ngược lại</returns>
		//        [System.Security.Permissions.EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
		//        public bool Authenticate(string host, string port, string basedn,
		//                                    string usernameSetting, string passwordSetting,
		//                                    string usernameAuthenticate, string passwordAuthenticate,
		//                                    string authenticationFilter)
		//        {
		//            var result = false;
		//            try
		//            {
		//                var domain = GenerateDomainLdap(host, port, useSSL, basedn);
		//                var entrySetting = new DirectoryEntry(domain, usernameSetting, passwordSetting, AuthenticationTypes.ReadonlyServer);
		//                var search = new DirectorySearcher(entrySetting) { Filter = string.Format(authenticationFilter, usernameAuthenticate) };
		//                var user = search.FindOne();
		//                if (user != null)
		//                {
		//                    var fullDnOfUser = user.Path.Substring(user.Path.LastIndexOf("/", StringComparison.Ordinal) + 1,
		//                                                            user.Path.Length - user.Path.LastIndexOf("/", StringComparison.Ordinal) - 1);
		//                    var entryAuthenticate = new DirectoryEntry(domain, fullDnOfUser, passwordAuthenticate, AuthenticationTypes.ReadonlyServer);
		//#pragma warning disable 168
		//                    var obj = entryAuthenticate.NativeObject;
		//#pragma warning restore 168
		//                    result = true;
		//                }
		//            }
		//            catch (Exception)
		//            {
		//            }
		//            return result;
		//        }


		/// <summary>
		/// Kiểm tra cấu hình kết nối tới server LDAP
		/// </summary>
		/// <param name="host">Tên máy chủ LDAP</param>
		/// <param name="basedn">DN cấp cao nhất của LDAP</param>
		/// <param name="username">Tên đăng nhập</param>
		/// <param name="password">Mật khẩu</param>
		/// <param name="useSSL"></param>
		/// <param name="port">Cổng</param>
		/// <returns>true:nếu kết nối thành công và ngược lại</returns>
		[System.Security.Permissions.EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
		public bool TestConnection(string host, string port, bool useSSL, string basedn, string username, string password)
		{
			var result = false;
			try
			{

				result = Authenticate(host, port, useSSL, username, password);
			}
			catch (DirectoryServicesCOMException)
			{
			}
			return result;
		}

		/// <summary>
		/// Xác thực LDAP và LDAP over SSL
		/// <para>TrinhNVd (051016)</para>
		/// Xác thực với hàm này không cần DomainName chính (BaseDN), bộ lọc tìm kiếm
		/// <para>Hàm ưu tiên tạo chứng chỉ xác thực với sAMAccountName trước, nếu không được thì tạo chứng chỉ với CommonName</para>
		/// </summary>
		/// <param name="host">LDAP host (không có LDAP://)</param>
		/// <param name="port">Cổng</param>
		/// <param name="useSSL">Sử dụng SSL</param>
		/// <param name="username">Tên đăng nhập</param>
		/// <param name="password">Mật khẩu xác thực</param>
		/// <returns></returns>
		public bool Authenticate(string host, string port, bool useSSL, string username, string password)
		{
			var result = false;
			username += "@" + _authenticationSettings.LdapDomain;
			try
			{
				var entry = new DirectoryEntry(host, username, password);
				var nativeObject = entry.NativeObject;
				result = true;
			}
			catch
			{
				try
				{
					_authenticatedLdapConnection = AuthenticatedConnection(host, port, useSSL, username, password);
					result = true;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}

			return result;
		}
		
		private LdapConnection AuthenticatedConnection(string host, string port, bool useSSL, string username, string password)
		{
			var ldapConnection = new LdapConnection(new LdapDirectoryIdentifier(host, int.Parse(port), false, false));
			ldapConnection.SessionOptions.SecureSocketLayer = useSSL;

			//Bỏ qua tầng Certificate
			ldapConnection.SessionOptions.VerifyServerCertificate = new VerifyServerCertificateCallback(ServerCallback);
			try
			{
				//Tạo mới chứng chỉ với DistinguishedName (tên phân biệt) trong LDAP ObjectInfo
				//username dạng: CN=CustomName(username),OU=OrganizationUnitName,DC=DomainComponent(s)
				var credential = new NetworkCredential(username, password);
				ldapConnection.AuthType = AuthType.Basic;
				ldapConnection.Bind(credential);
			}
			catch (LdapException)
			{
				try
				{
					//Tạo mới chứng chỉ với username (sAMAccountName trong LDAP ObjectInfo)
					var networkCredential = new NetworkCredential(username, password, host);
					ldapConnection.AuthType = AuthType.Ntlm;
					ldapConnection.Bind(networkCredential);
				}
				catch (Exception)
				{
					var credential = new NetworkCredential(username, password);
					ldapConnection.AuthType = AuthType.Negotiate;
					ldapConnection.Bind(credential);
				}
			}
			return ldapConnection;
		}

		/// <summary>
		/// Xác thực người dùng
		/// </summary>
		/// <param name="host">Tên máy chủ LDAP</param>
		/// <param name="port">Cổng</param>
		/// <param name="basedn">DN cấp cao nhất của LDAP</param>
		/// <param name="usernameSetting">Tên đăng nhập đã được cấu hình</param>
		/// <param name="passwordSetting">Mật khẩu đã được cấu hình</param>
		/// <param name="usernameAuthenticate">Tên đăng nhập cần xác thực</param>
		/// <param name="passwordAuthenticate">Mật khẩu cần xác thực</param>
		/// <param name="authenticationFilter">Bộ lọc để tìm được chính xác người dùng</param>
		/// <param name="userSsl"></param>
		/// <returns>true: nếu tên đăng nhập và mật khẩu chính xác và ngược lại</returns>
		[System.Security.Permissions.EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
		public bool Authenticate(string host, string port, string basedn,
									string usernameSetting, string passwordSetting,
									string usernameAuthenticate, string passwordAuthenticate,
									string authenticationFilter, bool userSsl)
		{
			var result = false;
			try
			{
				var domain = GenerateDomainLdap(host, port, userSsl, basedn);
				var entrySetting = new DirectoryEntry(domain, usernameSetting, passwordSetting, AuthenticationTypes.ReadonlyServer);
				var search = new DirectorySearcher(entrySetting) { Filter = string.Format(authenticationFilter, usernameAuthenticate) };
				var user = search.FindOne();
				if (user != null)
				{
					var fullDnOfUser = user.Path.Substring(user.Path.LastIndexOf("/", StringComparison.Ordinal) + 1,
															user.Path.Length - user.Path.LastIndexOf("/", StringComparison.Ordinal) - 1);
					var entryAuthenticate = new DirectoryEntry(domain, fullDnOfUser, passwordAuthenticate, AuthenticationTypes.ReadonlyServer);
#pragma warning disable 168
					var obj = entryAuthenticate.NativeObject;
#pragma warning restore 168
					result = true;
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		/// <summary>
		/// Đổi mật khẩu LDAP
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		/// <param name="useSSL"></param>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="newPassword"></param>
		/// <returns></returns>
		public bool ChangePassword(string host, string port, bool useSSL, string username, string password, string newPassword)
		{
			var result = false;
			var admin = _authenticationSettings.LdapUsername;
			var adminPassword = _authenticationSettings.LdapPassword;

			//Nếu có tài khoản ldapAdmin, thì dùng SetPassword, yêu cầu người dùng đăng nhập trước để xác nhận đúng mật khẩu
			if (!string.IsNullOrWhiteSpace(admin) && !string.IsNullOrWhiteSpace(adminPassword))
			{
				if (Authenticate(host, port, useSSL, username, password))
				{
					try
					{
						using (var context = new PrincipalContext(ContextType.Domain, host, null, ContextOptions.Negotiate, admin, adminPassword))
						{
							using (var userIdentity = TryGetUserIdentity(context, username))
							{
								userIdentity.SetPassword(newPassword);
								userIdentity.Save();
								result = true;
							}
						}
					}
					catch (Exception ex)
					{
						throw new Exception("LdapAdmin invalid!", ex);
					}
				}
			}
			else
			{
				try
				{
					//Trường hợp không được cấu hình tài khoản admin, user đổi mật khẩu với ChangePassword, phải nhập mật khẩu cũ
					using (var context = new PrincipalContext(ContextType.Domain, host, null, ContextOptions.Negotiate, username, password))
					{
						using (var userIdentity = TryGetUserIdentity(context, username))
						{
							userIdentity.ChangePassword(password, newPassword);
							userIdentity.Save();
							result = true;
						}
					}
				}
				catch (Exception ex)
				{
					throw new Exception("ChangePassword Failed!", ex);
				}
			}

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="stringName"></param>
		/// <returns></returns>
		private UserPrincipal TryGetUserIdentity(PrincipalContext context, string stringName)
		{
			UserPrincipal userPrincipal = null;
			//SamAccountName: trinhnvd
			userPrincipal = GetUserIdentity(context, IdentityType.SamAccountName, stringName);

			if (userPrincipal == null)
			{
				//UserPrincipalName: trinhnvd@bkav.com
				userPrincipal = GetUserIdentity(context, IdentityType.UserPrincipalName, stringName);
			}

			//if (userPrincipal == null)
			//{
			//    //DistinguishedName: CN=trinhnvd,CN=Users,DC=bkav,DC=com
			//    userPrincipal = GetUserIdentity(context, IdentityType.DistinguishedName, stringName);
			//}

			//if (userPrincipal == null)
			//{
			//    //Name: Nguyễn Văn Trình
			//    userPrincipal = GetUserIdentity(context, IdentityType.Name, stringName);
			//}
			return userPrincipal;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="type"></param>
		/// <param name="username"></param>
		/// <returns></returns>
		private UserPrincipal GetUserIdentity(PrincipalContext context, IdentityType type, string username)
		{
			try
			{
				return UserPrincipal.FindByIdentity(context, type, username);
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// Allow certificate
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="certificate"></param>
		/// <returns></returns>
		private bool ServerCallback(LdapConnection connection, X509Certificate certificate)
		{
			return true;
		}

		///// <summary>
		///// Đăng nhập LDAP đơn giản
		///// </summary>
		///// <param name="ldapHost"></param>
		///// <param name="useSSL"></param>
		///// <param name="username"></param>
		///// <param name="password"></param>
		///// <returns></returns>
		//public bool SimpleAuthenticatioinValidate(string ldapHost, bool useSSL, string username, string password)
		//{
		//    var check = false;
		//    try
		//    {
		//        if (!ldapHost.ToLower().Contains("ldap://"))
		//        {
		//            ldapHost = "LDAP://" + ldapHost;
		//        }
		//        var authenticationType = AuthenticationTypes.Secure;
		//        if (useSSL)
		//        {
		//            authenticationType = AuthenticationTypes.SecureSocketsLayer;
		//        }
		//        var entry = new DirectoryEntry(ldapHost, username, password, authenticationType);
		//        var nativeObject = entry.NativeObject;
		//        check = true;
		//    }
		//    catch (Exception)
		//    {
		//    }

		//    return check;
		//}

		/// <summary>
		/// Lấy ra tất cả thông tin của tất cả người dùng trong LDAP theo bộ lọc truyền vào
		/// </summary>
		/// <param name="host">Tên máy chủ LDAP</param>
		/// <param name="port">Cổng</param>
		/// <param name="useSSL">Sử dụng SSL</param>
		/// <param name="basedn">DN cấp cao nhất của LDAP</param>
		/// <param name="usernameSetting">Tên đăng nhập đã được cấu hình</param>
		/// <param name="passwordSetting">Mật khẩu đã được cấu hình</param>
		/// <param name="importFilter">Bộ lọc để tìm chính xác những người dùng cần import</param>
		/// <param name="mappingUsername">Key để mapping với tên đăng nhập</param>
		/// <param name="mappingEmail">Key để mapping với email</param>
		/// <param name="mappingFirstName">Key để mapping với tên người dùng</param>
		/// <param name="mappingLastName">Key để mapping với họ người dùng</param>
		/// <param name="mappingFullName">Key để mapping với họ và tên người dùng</param>
		/// <returns>Danh sách người dùng đã được mapping</returns>
		[System.Security.Permissions.EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
		public List<LdapUser> GetAllUserImport(string host, string port, bool useSSL, string basedn,
												string usernameSetting, string passwordSetting,
												string importFilter, string mappingUsername,
												string mappingEmail, string mappingFirstName,
												string mappingLastName, string mappingFullName)
		{
			var result = new List<LdapUser>();
			var authenticationType = AuthenticationTypes.ReadonlyServer;
			if (useSSL)
			{
				authenticationType = AuthenticationTypes.SecureSocketsLayer;
			}

			var domain = GenerateDomainLdap(host, port, useSSL, basedn);
			var entrySetting = new DirectoryEntry(domain, usernameSetting, passwordSetting, authenticationType);
			var search = new DirectorySearcher(entrySetting) { Filter = importFilter };
			var listUsers = search.FindAll();
			if (listUsers.Count > 0)
			{
				for (var i = 0; i < listUsers.Count; i++)
				{
					var user = listUsers[i];
					var ldapUser = new LdapUser
					{
						Username = user.Properties[mappingUsername] == null
														  ? string.Empty
														  : user.Properties[mappingUsername][0] == null
																? string.Empty
																: user.Properties[mappingUsername][0].ToString(),
						Email = user.Properties[mappingEmail] == null
														  ? string.Empty
														  : user.Properties[mappingEmail][0] == null
																? string.Empty
																: user.Properties[mappingEmail][0].ToString(),
						FirstName = user.Properties[mappingFirstName] == null
														  ? string.Empty
														  : user.Properties[mappingFirstName][0] == null
																? string.Empty
																: user.Properties[mappingFirstName][0].ToString(),
						LastName = user.Properties[mappingLastName] == null
														  ? string.Empty
														  : user.Properties[mappingLastName][0] == null
																? string.Empty
																: user.Properties[mappingLastName][0].ToString(),
						FullName = user.Properties[mappingFullName] == null
														  ? string.Empty
														  : user.Properties[mappingFullName][0] == null
																? string.Empty
																: user.Properties[mappingFullName][0].ToString(),
					};
					result.Add(ldapUser);
				}
			}

			return result;
		}

		private static string GenerateDomainLdap(string host, string port, bool useSSl, string basedn)
		{
			string domain = host;
			if (string.IsNullOrWhiteSpace(host))
			{
				throw new ArgumentNullException("host");
			}
			if (!host.ToLower().Contains("ldap://"))
			{
				domain = "LDAP://" + host;
			}

			if (string.IsNullOrWhiteSpace(port))
			{
				domain += useSSl ? ":636" : ":389";
			}
			else
			{
				domain += ":" + port;
			}
			if (!string.IsNullOrWhiteSpace(basedn))
			{
				domain += "/" + basedn;
			}
			return domain;
		}


		//public string GetDirectoryEntryByAdmin(string userName, out DirectoryEntry objEntry)
		//{
		//	objEntry = null;
		//	try
		//	{
		//		string userAdmin = GetCorrectUserName(ChangePassWord._UserAdmin);

		//		string correctUserAdmin = userAdmin + "@" + _domain;

		//		string correctUser = userName + "@" + _domain;

		//		string passAdmin = ChangePassWord._PassAdmin;// base64.base64Decode(admin[1]);

		//		DirectoryEntry entryAdmin = new DirectoryEntry(_ldaPpath, correctUserAdmin, passAdmin, AuthenticationTypes.Secure);

		//		if (entryAdmin == null) return "Tài khoản Admin hoặc Mật khẩu Admin không đúng.";
		//		DirectorySearcher searcher = new DirectorySearcher(entryAdmin);
		//		searcher.Filter = string.Format(@"(&((&(objectCategory=Person)(objectClass=User)))(UserPrincipalName={0}))", correctUser);
		//		SearchResult objResult = searcher.FindOne();
		//		objEntry = (objResult != null) ? objResult.GetDirectoryEntry() : null;
		//		if (objEntry == null) return "Tài khoản <b>" + userName + "</b>  không tồn tại.";
		//	}
		//	catch (Exception ex)
		//	{
		//		Log.WriteLog("Lỗi kết nối AD LDAP " + ex.Message);
		//		return "Đăng nhập không thành công. <br/>Vui lòng liên hệ quản trị.";
		//	}
		//	return "";
		//}
	}
}
