using System;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Linq;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Customer.Settings;
using Bkav.eGovCloud.Infrastructure;
using Bkav.eGovCloud.Mailers;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Models.Binder;
using Bkav.eGovCloud.Search;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Bkav.SsoHelper;
using FluentValidation.Mvc;
using Ninject;
using Ninject.Web.Common;
using StackExchange.Profiling;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Infrastructure;
using System.Threading.Tasks;
using Bkav.eGovCloud.Business.Utils;
using System.IO;
using Bkav.eGovCloud.Business.Caching;
using FluentScheduler;
using Bkav.eGovCloud.Business.Tasks;
using Bkav.eGovCloud.Helper;

#if QuanTriTapTrungEdition

using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Helper;

#endif

namespace Bkav.eGovCloud
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode,
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : NinjectHttpApplication
	{
        private void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
		{
			var ctx = HttpContext.Current;
			if (ctx == null)
			{
				Response.AddHeader("IsAuthenticated", "false");
				return;
			}

			var usr = ctx.User;
			if (usr != null)
			{
				var formIdentity = usr.Identity as FormsIdentity;
				if (formIdentity != null)
				{
					if (!(formIdentity.IsAuthenticated && formIdentity.AuthenticationType == "Forms"))
					{
						Response.AddHeader("IsAuthenticated", "false");
						return;
					}

					var adminTicket = formIdentity.Ticket;
					if (adminTicket == null)
					{
						Response.AddHeader("IsAuthenticated", "false");
						return;
					}

					var adminIdentity = new CustomerIdentity(adminTicket);
					var adminPrincipal = new CustomerPrincipal(adminIdentity);
					ctx.User = Thread.CurrentPrincipal = adminPrincipal;
					return;
				}

				var ssoSettings = SsoSettings.Instance;
				var bkavAuthCookie = ctx.Request.Cookies.Get(ssoSettings.BkavSSOCookieName);
				var ssoHelper = new EncryptData(ssoSettings.BkavSSOKeyVersion, ssoSettings.BkavSSOSecretKey);
                HttpCookie userInfoCookie;
                var isValidToken = TokenIsValid(bkavAuthCookie, ctx, out userInfoCookie);

                if (!isValidToken)
                {
                    Response.AddHeader("IsAuthenticated", "false");
                    return;
                }

                userInfoCookie = userInfoCookie ?? ctx.Request.Cookies.Get(ssoSettings.UserInfoCookie);

                var ticket = FormsAuthentication.Decrypt(userInfoCookie.Value);
				var identity = new CustomerIdentity(ticket);
				var principal = new CustomerPrincipal(identity);
				ctx.User = Thread.CurrentPrincipal = principal;
			}
		}

		protected void Application_PreSendRequestHeaders()
		{
			// Xóa header banner đảm bảo an ninh hệ thống, tránh hacker khai thác.
			Response.Headers.Remove("Server");

#if DEBUG
			// Response.Headers.Remove("X-MiniProfiler-Ids");
#endif
			Response.Headers.Remove("X-AspNet-Version");
			Response.Headers.Remove("X-Powered-By");

			Response.Headers.Remove("X-AspNetMVC-Version");

			// Fix tạm trường hợp bị lỗi cache client ko trả về nội dung file nhưng dưới client ko có dữ liệu.
			// Todo: cần xem rõ nguyên nhân nó là gì? thẻ Etag dùng để đánh dấu phiên bản của OutputCache
			// Response.Headers.Remove("ETag");

			// MvcHandler.DisableMvcResponseHeader = true;
		}

        private bool TokenIsValid(HttpCookie loginCookie, HttpContext ctx, out HttpCookie userInfoCookie)
        {
            userInfoCookie = null;
            var ssoSettings = SsoSettings.Instance;
            var ssoHelper = new EncryptData(ssoSettings.BkavSSOKeyVersion, ssoSettings.BkavSSOSecretKey);

            var result = loginCookie != null && ssoHelper.IsValidCookie(loginCookie.Value);
            if (!result)
            {
                var userName = GetUserNameFromValaToken();
                if (string.IsNullOrEmpty(userName))
                {
                    return false;
                }

                // Trường hợp chưa đăng nhập egov nhưng đăng nhập từ vala
                userInfoCookie = CreateCookieData(userName, ctx);
                return userInfoCookie != null;
            }

            return result;
        }

        private string GetUserNameFromValaToken()
        {
            var result = HttpContext.Current == null ? "" : HttpContext.Current.Request.QueryString["token"];
            var tokenstring = HttpContext.Current == null ? "" : HttpContext.Current.Request.QueryString["tokenVala"];

            if (string.IsNullOrEmpty(tokenstring))
            {
                return "";
            }
            try
            {
                var token_news = tokenstring.Split('.');
                string dataUser = "";
                if (token_news.Length == 3)
                {
                    dataUser = token_news[1];
                }

                var dt = base64urlDecode(dataUser);
                var dataSSO = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(dt);
                result = dataSSO["email"];
            }
            catch (Exception)
            {
                return "";
            }

            if (!string.IsNullOrEmpty(result))
            {
                result = result.Split('@').First();
                return result;
            }

            return "";
        }


        private HttpCookie CreateCookieData(string userName, HttpContext ctx)
        {
            var userService = DependencyResolver.Current.GetService<UserBll>();
            var user = userService.GetByUserName(userName);
            if (user == null)
            {
                Response.AddHeader("IsAuthenticated", "false");
                return null;
            }

            var cookieData = new CustomerCookieData
            {
                UsernameWithDomain = user.UsernameEmailDomain,
                UserId = user.UserId,
                Email = user.UsernameEmailDomain,
                FullName = user.FullName
            };

            string token;
            var result = AddCookie(user.UsernameEmailDomain, false, cookieData, out token);

            return result;
        }


        private HttpCookie AddCookie(string userNameWithDomain, bool remember, CustomerCookieData userDataString, out string token)
        {
            var _ssoSettings = SsoSettings.Instance;
            token = "";
            HttpCookie authCookie;
            FormsAuthenticationTicket ticket;
            FormsAuthenticationTicket newTicket;

            //bkavAuthen
            var ssoHelper = new EncryptData(_ssoSettings.BkavSSOKeyVersion, _ssoSettings.BkavSSOSecretKey);
            var exprire = remember ? DateTime.UtcNow.AddDays(Convert.ToInt32(_ssoSettings.BkavSSOExpire)) : DateTime.UtcNow.AddDays(1);

            var exp = (long)(exprire - new DateTime(1970, 1, 1)).TotalMilliseconds;

            var ssoCookie = new HttpCookie(_ssoSettings.BkavSSOCookieName);
            ssoCookie.Value = ssoHelper.CreateCookieString(userNameWithDomain, exp.ToString());
            InitCookie(ssoCookie, remember);
            token = ssoCookie.Value;

            // bkavUser
            var bkavUserCookie = new HttpCookie(_ssoSettings.BkavSSOCookieUsername);
            bkavUserCookie.Value = userNameWithDomain;
            InitCookie(bkavUserCookie, remember);

            // eGovUserInfo
            var userInfoCookie = new HttpCookie(_ssoSettings.UserInfoCookie);
            authCookie = FormsAuthentication.GetAuthCookie(userNameWithDomain, remember);
            ticket = FormsAuthentication.Decrypt(authCookie.Value);
            newTicket = new FormsAuthenticationTicket(
                ticket.Version,
                ticket.Name,
                ticket.IssueDate,
                ticket.Expiration,
                ticket.IsPersistent,
                userDataString.ToCookieString()
                );
            userInfoCookie.Value = FormsAuthentication.Encrypt(newTicket);
            InitCookie(userInfoCookie, remember);

            return userInfoCookie;
        }

        private HttpCookie InitCookie(HttpCookie httpCookie, bool remember)
        {
            var _ssoSettings = SsoSettings.Instance;
            httpCookie.Expires = remember ? DateTime.UtcNow.ToLocalTime().AddDays(_ssoSettings.BkavSSOExpire) : DateTime.UtcNow.ToLocalTime().AddDays(1);
            httpCookie = SetResponseCookie(httpCookie);
            return httpCookie;
        }

        private HttpCookie SetResponseCookie(HttpCookie httpCookie)
        {
            httpCookie.HttpOnly = false; //False: Cho phép client cập nhật/xóa cookie
            Response.Cookies.Add(httpCookie);
            return httpCookie;
        }

        private string base64urlDecode(string encoded)
        {
            return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(encoded.Replace("_", "/").Replace("-", "+") + new string('=', (4 - encoded.Length % 4) % 4)));
        }

        private void Application_BeginRequest(Object source, EventArgs e)
		{
			FirstRequestInitialisation.Initialise();

#if !DEBUG

            var authenSettings = DependencyResolver.Current.GetService<AuthenticationSettings>();
            if (authenSettings.HttpsOnly && !Context.Request.IsSecureConnection)
            {
                Response.Redirect(Context.Request.Url.ToString().Replace("http:", "https:"));
            }
#endif
		}

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapHubs();

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("AvatarProfile/{*pathInfo}");

			routes.MapHttpRoute(
				"eGov Api",
				"webapi/{controller}/{action}/{id}",
				new { id = RouteParameter.Optional });

			routes.MapHttpRoute(
				"Publish Api",
				"pa/{action}/{id}",
				new { controller = "PublishApi", action = "Test", id = UrlParameter.Optional });

			routes.MapRoute(
				"MobileWithNotify", // Route name
				"n/{id}", // URL with parameters
				new { controller = "Mobile", action = "NotifyIndex", id = UrlParameter.Optional }, // Parameter defaults
				new[] { "Bkav.eGovCloud.Controllers" });
			routes.MapRoute(
				"Mobile", // Route name
				"m", // URL with parameters
				new { controller = "Mobile", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
				new[] { "Bkav.eGovCloud.Controllers" });

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Main", id = UrlParameter.Optional }, // Parameter defaults
				new[] { "Bkav.eGovCloud.Controllers" });
		}

		[EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
		protected override IKernel CreateKernel()
		{
			var kernel = new StandardKernel();

			var isInstalled = DataSettings.DatabaseIsInstalled();

			kernel.Load(Assembly.GetExecutingAssembly());

			kernel.Bind<ResourceBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<CitizenBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<ScopeAreaBll>()
				.ToSelf().InRequestScope();

			//Log
			kernel.Bind<LogBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<LevelBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<ClientBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<NotifyBll>()
			   .ToSelf().InRequestScope();

			//Catalog
			kernel.Bind<CatalogBll>()
				.ToSelf().InRequestScope();

            //asyncCatalog
            //kernel.Bind<AsyncCatalogBll>()
            //    .ToSelf().InRequestScope();

            //indicator
            kernel.Bind<DisaggregationBll>()
                .ToSelf().InRequestScope();

            //categoryDisaggregation
            kernel.Bind<CategoryDisaggregationsBll>()
                .ToSelf().InRequestScope();

			kernel.Bind<OfficeBll>()
			 .ToSelf().InRequestScope();

			kernel.Bind<ISearchInDatabase>()
				.To<EgovSearch>().InRequestScope();

			kernel.Bind<ISearchInSolr>()
				.To<EgovSearch>().InRequestScope();

			kernel.Bind<ExtendFieldBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<FormBll>()
				.ToSelf().InRequestScope();

			//Category
			kernel.Bind<CategoryBll>()
				.ToSelf().InRequestScope();
			//Indatatype
			//kernel.Bind<IndatatypeBll>()
			//	.ToSelf().InRequestScope();
			//Position
			kernel.Bind<PositionBll>()
				.ToSelf().InRequestScope();
			//Unit
			kernel.Bind<UnitBll>()
				.ToSelf().InRequestScope();

			//JobTitles
			kernel.Bind<JobTitlesBll>()
				.ToSelf().InRequestScope();

			//DocType
			kernel.Bind<DocTypeBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<DoctypeTemplateBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<WorkflowBll>()
				.ToSelf().InRequestScope();

			//DocField
			kernel.Bind<DocFieldBll>()
				.ToSelf().InRequestScope();
            
			//Department
			kernel.Bind<DepartmentBll>()
				.ToSelf().InRequestScope();

			//Store
			kernel.Bind<StoreBll>()
				.ToSelf().InRequestScope();

			//Notifications
			kernel.Bind<NotificationBll>()
				.ToSelf().InRequestScope();

			//Increase
			kernel.Bind<IncreaseBll>()
				.ToSelf().InRequestScope();

			//Code
			kernel.Bind<CodeBll>()
				.ToSelf().InRequestScope();

			//Fee
			kernel.Bind<FeeBll>()
				.ToSelf().InRequestScope();

			//Paper
			kernel.Bind<PaperBll>()
				.ToSelf().InRequestScope();

			//Authorize
			kernel.Bind<AuthorizeBll>()
				.ToSelf().InRequestScope();

			//Role
			kernel.Bind<RoleBll>()
				.ToSelf().InRequestScope();

			//Document
			kernel.Bind<DocumentBll>()
				.ToSelf().InRequestScope();

			kernel.Bind(typeof(SettingProvider<>))
				.ToSelf().InRequestScope();

			//User
			kernel.Bind<UserBll>().ToSelf().InRequestScope();

			kernel.Bind<MobileDeviceBll>().ToSelf().InRequestScope();

			kernel.Bind<LdapProvider>().ToSelf();

			kernel.Bind<IUserMailer>().To<UserMailer>().InRequestScope();

			//Otp
			kernel.Bind<OtpBll>().ToSelf().InRequestScope();
            
			//ProcessFunction
			kernel.Bind<ProcessFunctionBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<ProcessFunctionTypeBll>()
				.ToSelf().InRequestScope();

#if QuanTriTapTrungEdition
			kernel.Bind<ICache>()
				.To<StaticCache>().InRequestScope()
				.WithConstructorArgument(
					"domainName",
					c =>
					{
						var domainName = HttpContext.Current == null ? "" : HttpContext.Current.Request.GetDomainName();
						return domainName;
					}
				);
			kernel.Bind<ICacheManager>()
				.To<MemoryCacheManager>().InRequestScope();

#else
            var cacheType = CommonHelper.GetAppSetting("eg:CacheType", "");
            if (cacheType.Equals("MemCached", StringComparison.OrdinalIgnoreCase))
            {
                kernel.Bind<ICache>()
                       .To<MemCached>().InRequestScope();
            }
            else
            {
                kernel.Bind<ICache>()
                       .To<StaticCache>().InRequestScope()
                       .WithConstructorArgument(
                           "domainName",
                           c =>
                           {
                               var domainName = HttpContext.Current == null ? "BkaveGov" : HttpContext.Current.Request.GetDomainName();
                               return domainName;
                           }
                       );
            }

            kernel.Bind<ICacheManager>()
                .To<MemoryCacheManager>().InRequestScope();

#endif
			kernel.Bind<DocumentsCache>()
				.ToSelf().InRequestScope();

			if (!isInstalled)
			{
				kernel.Bind<IDbCustomerContext>()
					.To<EfContext>().InRequestScope();

#if QuanTriTapTrungEdition
				kernel.Bind<IDbAdminContext>()
					.To<EfAdminContext>().InRequestScope();
#endif
			}
			else
			{
				//Setting
				kernel.Bind<SettingBll>().ToConstructor(c =>
						 new SettingBll(
							 c.Context.Kernel.Get<IDbCustomerContext>(),
								 c.Context.Kernel.Get<MemoryCacheManager>())).InRequestScope();

				kernel.Bind<AdminGeneralSettings>()
					.ToMethod(c => c.Kernel.Get<SettingProvider<AdminGeneralSettings>>().Settings)
					.InRequestScope();
				kernel.Bind<FileUploadSettings>()
					.ToMethod(c => c.Kernel.Get<SettingProvider<FileUploadSettings>>().Settings)
					.InRequestScope();
				kernel.Bind<EmailSettings>()
					.ToMethod(c => c.Kernel.Get<SettingProvider<EmailSettings>>().Settings)
					.InRequestScope();
				kernel.Bind<AuthenticationSettings>()
					.ToMethod(c => c.Kernel.Get<SettingProvider<AuthenticationSettings>>().Settings)
					.InRequestScope();
				kernel.Bind<PasswordPolicySettings>()
					.ToMethod(c => c.Kernel.Get<SettingProvider<PasswordPolicySettings>>().Settings)
					.InRequestScope();
				kernel.Bind<FileLocationSettings>()
					.ToMethod(c => c.Kernel.Get<SettingProvider<FileLocationSettings>>().Settings)
					.InRequestScope();
				kernel.Bind<WorkTimeSettings>()
					.ToMethod(c => c.Kernel.Get<SettingProvider<WorkTimeSettings>>().Settings)
					.InRequestScope();
				kernel.Bind<SearchSettings>()
					.ToMethod(c => c.Kernel.Get<SettingProvider<SearchSettings>>().Settings)
					.InRequestScope();
				kernel.Bind<ConnectionSettings>()
					.ToMethod(c => c.Kernel.Get<SettingProvider<ConnectionSettings>>().Settings)
					.InRequestScope();
				kernel.Bind<CBCLSetting>()
				   .ToMethod(c => c.Kernel.Get<SettingProvider<CBCLSetting>>().Settings)
				   .InRequestScope();
				kernel.Bind<VersionTreeSetting>()
				  .ToMethod(c => c.Kernel.Get<SettingProvider<VersionTreeSetting>>().Settings)
				  .InRequestScope();
				kernel.Bind<VoteSettings>()
				 .ToMethod(c => c.Kernel.Get<SettingProvider<VoteSettings>>().Settings)
				 .InRequestScope();
				kernel.Bind<FAQSetting>()
					.ToMethod(c => c.Kernel.Get<SettingProvider<FAQSetting>>().Settings)
					.InRequestScope();
				kernel.Bind<ImageSettings>()
				  .ToMethod(c => c.Kernel.Get<SettingProvider<ImageSettings>>().Settings)
				  .InRequestScope();
				kernel.Bind<SmsSettings>()
				  .ToMethod(c => c.Kernel.Get<SettingProvider<SmsSettings>>().Settings)
				  .InRequestScope();
				kernel.Bind<TransferSettings>()
				  .ToMethod(c => c.Kernel.Get<SettingProvider<TransferSettings>>().Settings)
				  .InRequestScope();
				kernel.Bind<NotificationSettings>()
					.ToMethod(c => c.Kernel.Get<SettingProvider<NotificationSettings>>().Settings)
					.InRequestScope();
				kernel.Bind<LanguageSettings>()
					.ToMethod(c => c.Kernel.Get<SettingProvider<LanguageSettings>>().Settings)
					.InRequestScope();
				kernel.Bind<OnlineRegistrationSettings>()
				  .ToMethod(c => c.Kernel.Get<SettingProvider<OnlineRegistrationSettings>>().Settings)
				  .InRequestScope();
				kernel.Bind<WarningSettings>()
				  .ToMethod(c => c.Kernel.Get<SettingProvider<WarningSettings>>().Settings)
				  .InRequestScope();
				kernel.Bind<OtpSettings>()
				  .ToMethod(c => c.Kernel.Get<SettingProvider<OtpSettings>>().Settings)
				  .InRequestScope();

#if QuanTriTapTrungEdition

				//Context
				kernel.Bind<IDbAdminContext>()
					.To<EfAdminContext>().InRequestScope()
					.WithConstructorArgument(
						"connection",
						c =>
						{
							var cacheManager = c.Kernel.Get<MemoryCacheManager>();
							var dataSettings = cacheManager.Get<DataSettings>(CacheParam.MainConnectionString, CacheParam.MainConnectionStringCacheTimeOut, () =>
							{
								return DataSettings.Current;
							});

							var dbType = dataSettings.DataProvider == Entities.DatabaseType.MySql.ToString() ? Entities.DatabaseType.MySql : Entities.DatabaseType.SqlServer;
							return new StackExchange.Profiling.Data.EFProfiledDbConnection(
									ConnectionUtil.TestConnection(dataSettings.DataConnectionString, "", "", "", "", 0, dbType),
									MiniProfiler.Current);
						}
					);

				kernel.Bind<IDbCustomerContext>()
					.To<EfContext>().InRequestScope()
					.WithConstructorArgument
					(
						"connection",
						c =>
						{
							var cacheManager = c.Kernel.Get<MemoryCacheManager>();
							var userName = "";
							if (HttpContext.Current != null)
							{
								userName = HttpContext.Current.User.GetUserName();
							}

							var domainName = HttpContext.Current == null ? "" : HttpContext.Current.Request.GetDomainName();
							Connection dbConnection;
							var cacheParam = string.IsNullOrEmpty(userName) ? domainName : userName;
							dbConnection = cacheManager.Get
								(
									string.Format(CacheParam.DomainConnectionKey, domainName), CacheParam.DomainConnectionCacheTimeOut,
										() =>
										{
											return DbConnectionHelper.GetDbConnection("", domainName);
										}
									);

							if (dbConnection == null)
							{
								return null;
							}

							return
								new StackExchange.Profiling.Data.EFProfiledDbConnection(
									ConnectionUtil.TestConnection(dbConnection.ConnectionRaw, dbConnection.ServerName,
										dbConnection.Database,
										dbConnection.Username, dbConnection.Password, dbConnection.Port,
										dbConnection.DatabaseTypeIdInEnum), MiniProfiler.Current);
						}
					);
#else

                kernel.Bind<IDbCustomerContext>()
                    .To<EfContext>().InRequestScope()
                    .WithConstructorArgument
                    (
                        "connection",
                        c =>
                        {
                            var cacheManager = c.Kernel.Get<MemoryCacheManager>();
                            var dataSettings = cacheManager.Get<DataSettings>(CacheParam.MainConnectionString, CacheParam.MainConnectionStringCacheTimeOut, () =>
                            {
                                return DataSettings.Current;
                            });

                            var dbType = dataSettings.DataProvider == Entities.DatabaseType.MySql.ToString() ? Entities.DatabaseType.MySql : Entities.DatabaseType.SqlServer;
                            return new StackExchange.Profiling.Data.EFProfiledDbConnection(
                                    ConnectionUtil.TestConnection(dataSettings.DataConnectionString, "", "", "", "", 0, dbType),
                                    MiniProfiler.Current);
                        }
                    );
#endif
			}

			// Time
			kernel.Bind<TimeBll>()
				.ToSelf().InRequestScope();

			// FormHelper
			kernel.Bind<FormHelper>()
				.ToSelf().InRequestScope();

			kernel.Bind<Controllers.DocumentHelper>()
				.ToSelf().InRequestScope();

			// DocRelation
			kernel.Bind<DocRelationBll>()
				.ToSelf().InRequestScope();

			//kernel.Bind<WebEditor>().ToSelf().InRequestScope();

			// Comment
			kernel.Bind<CommentBll>()
				.ToSelf().InRequestScope();

			// Approver
			kernel.Bind<ApproverBll>()
				.ToSelf().InRequestScope();

			// Supplementary
			kernel.Bind<SupplementaryBll>()
				.ToSelf().InRequestScope();

			// Docfinish
			kernel.Bind<DocFinishBll>()
				.ToSelf().InRequestScope();

			// DocContent
			kernel.Bind<DocumentContentBll>()
				.ToSelf().InRequestScope();

			// CommonComment
			kernel.Bind<CommonCommentBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<DocTimelineBll>()
				.ToSelf().InRequestScope();

			// Template Key
			kernel.Bind<TemplateKeyBll>()
				.ToSelf().InRequestScope();

			// Template
			kernel.Bind<TemplateBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<AttachmentBll>()
				.ToSelf().InRequestScope();

			//ExtensionTime - Gia hạn xử lý
			kernel.Bind<ExtensionTimeBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<LuceneBll>()
				.ToSelf().InRequestScope();

			// Report
			kernel.Bind<ReportBll>()
				.ToSelf().InRequestScope();

            //ReportRule
            kernel.Bind<ReportRuleBll>()
                .ToSelf().InRequestScope();
            // Infomation
            //kernel.Bind<InfomationBll>()
            //    .ToSelf().InRequestScope();

            // Address
            kernel.Bind<AddressBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<DocumentOnlineBll>()
				.ToSelf().InRequestScope();

			// StorePrivate
			kernel.Bind<StorePrivateBll>()
				.ToSelf().InRequestScope();

			// StoreDoc
			kernel.Bind<StoreDocBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<EgovSearch>()
				.ToSelf().InRequestScope();

			// BusinessType
			kernel.Bind<BusinessTypeBll>()
				.ToSelf().InRequestScope();

			// Business
			kernel.Bind<BusinessesBll>()
				.ToSelf().InRequestScope();

            // DataType
            kernel.Bind<dataTypeBll>()
                .ToSelf().InRequestScope();

            // FormGroup
            kernel.Bind<FormGroupBll>()
				.ToSelf().InRequestScope();

			// DocTypeForm
			kernel.Bind<DocTypeFormBll>()
				.ToSelf().InRequestScope();

			// City
			kernel.Bind<CityBll>()
				.ToSelf().InRequestScope();

			// District
			kernel.Bind<DistrictBll>()
				.ToSelf().InRequestScope();
            //sso
            kernel.Bind<SSOSettings>()
                .ToMethod(c => c.Kernel.Get<SettingProvider<SSOSettings>>().Settings)
                .InRequestScope();
            //SSOApi
            kernel.Bind<SSOAPISettings>()
              .ToMethod(c => c.Kernel.Get<SettingProvider<SSOAPISettings>>().Settings)
              .InRequestScope();
            //BMM
            kernel.Bind<MissionSettings>()
                   .ToMethod(c => c.Kernel.Get<SettingProvider<MissionSettings>>().Settings)
                   .InRequestScope();
            //Chat tin dieu hanh
            kernel.Bind<ChatSettings>()
                   .ToMethod(c => c.Kernel.Get<SettingProvider<ChatSettings>>().Settings)
                   .InRequestScope();
            //ReportConfig
            kernel.Bind<ReportConfigSettings>()
                   .ToMethod(c => c.Kernel.Get<SettingProvider<ReportConfigSettings>>().Settings)
                   .InRequestScope();
            // Ward
            kernel.Bind<WardBll>()
				.ToSelf().InRequestScope();

			// BusinessLicense
			kernel.Bind<BusinessLicenseBll>()
				.ToSelf().InRequestScope();

			// BusinessLicenseAttach
			kernel.Bind<BusinessLicenseAttachBll>()
				.ToSelf().InRequestScope();

			// KeyWord
			kernel.Bind<KeyWordBll>()
				.ToSelf().InRequestScope();

			// Daily Process
			kernel.Bind<DailyProcessBll>()
				.ToSelf().InRequestScope();

			// DocumentPublish
			kernel.Bind<DocumentPublishBll>()
				.ToSelf().InRequestScope();

			// Anticipate
			kernel.Bind<AnticipateBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<GuideBll>()
			   .ToSelf().InRequestScope();

			// Printer
			kernel.Bind<PrinterBll>()
				.ToSelf().InRequestScope();

			// ActivityLog
			kernel.Bind<ActivityLogBll>()
				.ToSelf().InRequestScope();

			// Signature
			kernel.Bind<SignatureBll>()
				.ToSelf().InRequestScope();

			// UserConnection
			kernel.Bind<UserConnectionBll>()
				 .ToSelf().InRequestScope();

			//NotificationHubs
			kernel.Bind<Bkav.eGovCloud.NotificationService.SignalRMessaging.Hubs>()
				.ToSelf().InRequestScope();

			//TimeJobBll
			kernel.Bind<TimeJobBll>()
			   .ToSelf().InRequestScope();

			//BackupRestoreConfigBll
			kernel.Bind<BackupRestoreConfigBll>()
			   .ToSelf().InRequestScope();

			// BackupDatabaseHistoryBll
			kernel.Bind<BackupRestoreHistoryBll>()
			   .ToSelf().InRequestScope();

			// ShareFolderBll
			kernel.Bind<ShareFolderBll>()
			   .ToSelf().InRequestScope();

			//BackupRestoreFileConfigBll
			kernel.Bind<BackupRestoreFileConfigBll>()
			   .ToSelf().InRequestScope();

			//BackupRestoreManagerBll
			kernel.Bind<BackupRestoreManagerBll>()
			   .ToSelf().InRequestScope();

			kernel.Bind<ReportGroupBll>()
			   .ToSelf().InRequestScope();

			kernel.Bind<StatisticsBll>()
			   .ToSelf().InRequestScope();

			kernel.Bind<MailBll>()
		   .ToSelf().InRequestScope();

			kernel.Bind<SmsBll>()
			  .ToSelf().InRequestScope();

			kernel.Bind<TreeGroupBll>()
		   .ToSelf().InRequestScope();

			kernel.Bind<BussinessDocFieldDocTypeGroupBll>()
		  .ToSelf().InRequestScope();

			kernel.Bind<SyncDocTypeBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<NotifyConfigBll>()
			  .ToSelf().InRequestScope();

			kernel.Bind<VoteBll>()
				.ToSelf().InRequestScope();

			kernel.Bind<VoteDetailBll>()
						 .ToSelf().InRequestScope();

            kernel.Bind<VoiceTextBll>()
                         .ToSelf().InRequestScope();

            kernel.Bind<IndicatorValueDepartmentBll>()
                         .ToSelf().InRequestScope();

            //kernel.Bind<eGovCalendar.Business.CalendarBll>()
            //    .ToSelf().InRequestScope();

            kernel.Bind<ITypeFinder>()
				.To<AppDomainTypeFinder>().InRequestScope();

			return kernel;
		}

		[EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
		protected override void OnApplicationStarted()
		{
			base.OnApplicationStarted();

			var isInstalled = DataSettings.DatabaseIsInstalled();

			GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
			AreaRegistration.RegisterAllAreas();
			RegisterRoutes(RouteTable.Routes);
			AutoMapperStartup.Initialize();
			DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
			ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new EgovValidatorFactory()));

			ModelBinders.Binders[typeof(SearchDocumentParameters)] = new SearchDocumentParametersBinder();
			ModelBinders.Binders[typeof(GetDocumentParameters)] = new GetDocumentParametersBinder();

			if (isInstalled)
			{
				RegisterGlobalFilters(GlobalFilters.Filters);
				RegisterBundles(BundleTable.Bundles);
				BundleTable.EnableOptimizations = true;

				RunStartupTasks();
			}
			else
			{
				GlobalFilters.Filters.Add(new HandleInstallFilter());
			}
		}

		protected void Application_Error(Object sender, EventArgs e)
		{
			// LogException(Server.GetLastError());
		}

		protected void LogException(Exception exc)
		{
			if (exc == null)
			{
				return;
			}

			// TienBV: cần xem lại cách lưu log chổ này.
			// var logService = DependencyResolver.Current.GetService<LogBll>();
			// logService.Error(exc.Message, exc);
		}

		public static void RegisterBundles(BundleCollection bundles)
		{
			//Creating bundle for your css files
			bundles.Add(new StyleBundle("~/Content/documentCss").Include(
				"~/Content/font-awesome-4.2.0/css/font-awesome.min.css",
				"~/Scripts/aloha/css/aloha.css",
				"~/Scripts/aloha/css/style.css",
				"~/Content/bootstrap/css/bootstrap.min.css",
				"~/Content/bootstrap/css/bootstrap-theme.css",
				"~/Scripts/bkav.egov/libs/jquery/jquery.qtip/jquery.qtip.min.css",
				"~/Content/bkav.egov/egov.document.css",
				"~/Content/bkav.egov/egov.color.css",
				"~/Content/bkav.egov/egov.home.css",
				"~/Content/site.min.css"));

			bundles.Add(new StyleBundle("~/Content/mainCss").Include(
				"~/Content/bootstrap/css/bootstrap.min.css",
				"~/Content/bootstrap/css/bootstrap-theme.css",
				"~/Content/bkav.egov/egov.main.css",
				"~/Content/bkav.egov/egov.color.css",
				"~/Scripts/bkav.egov/libs/jquery/jquery.fileupload/css/jquery.fileupload-ui.css"));

			bundles.Add(new ScriptBundle("~/bundles/mainScript").Include(
			"~/Scripts/bkav.egov/libs/jquery/jquery-2.2.3.min.js",
			"~/Scripts/bkav.egov/libs/jquery/signalR/jquery.signalR-1.2.1.min.js",
			"~/Content/bootstrap/js/bootstrap.min.js",
			"~/Scripts/bkav.egov/libs/jquery/jquery.ui.layout/jquery.layout-latest.js",
			"~/Scripts/bkav.egov/libs/jquery/jquery.validate.min.js",
			"~/Scripts/bkav.egov/libs/jquery/jquery.validate.unobtrusive.min.js",
			"~/Scripts/bkav.egov/libs/jquery/jquery.unobtrusive-ajax.min.js",
			"~/Scripts/bkav.egov/libs/jquery/jquery.fileupload/js/vendor/jquery.ui.widget.js",
			"~/Scripts/bkav.egov/libs/jquery/jquery.fileupload/js/jquery.fileupload.js",
			"~/Scripts/bkav.egov/libs/underscore/underscore-1.8.3.min.js",
			"~/Scripts/bkav.egov/libs/jquery/jQuery.dotdotdot/jquery.dotdotdot.min.js",
			"~/Scripts/bkav.egov/libs/require/require.js",
			"~/Scripts/bkav.egov/util/bkav.utilities.js",
			"~/Scripts/bkav.egov/libs/notify/desktop-notify.js",
			"~/Scripts/bkav.egov/views/main/main.js",
			"~/Scripts/bkav.egov/libs/mudim-minorfixed.js"
			));

			BundleTable.EnableOptimizations = true;
		}

		protected void RunStartupTasks()
		{
			var typeFinder = DependencyResolver.Current.GetService<ITypeFinder>();
			var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
			var startUpTasks = new List<IStartupTask>();

			foreach (var startUpTaskType in startUpTaskTypes)
			{
				startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
			}

			var groupedTasks = startUpTasks.OrderBy(st => st.Order).ToLookup(x => x.Order);
			foreach (var tasks in groupedTasks)
			{
				Parallel.ForEach(tasks, task => { task.Execute(); });
			}
		}
	}

	internal class FirstRequestInitialisation
	{
		private static readonly Object SLock = new Object();
		private static bool _initializedAlready;

		public static void Initialise()
		{
			if (_initializedAlready)
			{
				return;
			}
            if (!DataSettings.DatabaseIsInstalled())
            {
                return;
            }

            lock (SLock)
			{

				var cacheManager = DependencyResolver.Current.GetService<MemoryCacheManager>();
				JobManager.RemoveAllJobs();

				var schedule = new eGovScheduler(cacheManager);
				JobManager.Initialize(schedule);

				// DependencyResolver.Current.GetService<TimerJob>().Run();
				//var licenseTask = new LicenseTask();
				//licenseTask.InitTimer();
				_initializedAlready = true;
			}
		}
	}
}