using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace Bkav.eGovCloud.Core.Utils
{
	/// <summary>
	/// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
	/// <para>Project: eGov Cloud v1.0</para>
	/// <para>Class : RequestExtension - public - Core</para>
	/// <para>Access Modifiers: </para>
	/// <para>Create Date : 170613</para>
	/// <para>Author      : TrungVH</para>
	/// <para>Description : Thư viện mở rộng cho http request</para>
	/// </summary>
	public static class RequestExtension
	{
		/// <summary>
		/// Mã từ điển các hệ điều hành kết nối với hệ thống
		/// </summary>
		public static Dictionary<int, string> dictionaryOS = new Dictionary<int, string>{
		   {(int)DeviceOS.Windows, "(Windows 98)|(Win98)|(Windows NT 5.1)|(Windows XP)|(Windows NT 6.0)|(Windows NT 7.0)|(Windows NT 10.0)"},
		   {(int)DeviceOS.LINUX, "(Linux)|(X11)"},
		   {(int)DeviceOS.MAC, "(Mac_PowerPC)|(Macintosh)"}
		};
		/// <summary>
		/// Lấy ra tên domain (đã loại bỏ sub domain)
		/// </summary>
		/// <param name="request">Http Request</param>
		/// <returns></returns>
		public static string GetDomainName(this HttpRequest request)
		{
			return request.Url.Host.GetDomainName();
		}

		/// <summary>
		/// Lấy ra tên domain (đã loại bỏ sub domain)
		/// </summary>
		/// <param name="request">Http Request</param>
		/// <returns></returns>
		public static string GetDomainName(this HttpRequestBase request)
		{
			var result = string.Empty;
			if (request.Url != null)
			{
				result = request.Url.Host.GetDomainName();
			}
			return result;
		}

		/// <summary>
		/// Lấy ra tên domain và port (đã loại bỏ sub domain)
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static string GetDomainAndPort(this HttpRequest request)
		{
			return string.Format("{0}{1}", request.Url.Host, GetPortExtension(request.Url.Port));
		}

		/// <summary>
		/// Lấy ra tên domain (đã loại bỏ sub domain)
		/// </summary>
		/// <param name="host"></param>
		/// <returns></returns>
		public static string GetDomainName(this string host)
		{
			//var result = string.Empty;
			//const string pattern = @"^(?<subdomain>[\w\.\-]+\.)*(?<domain>[\w\-]+\.)(?<com>[\w]{2,3})(?<countryextension>\.[\w]{2})?$";
			//var match = Regex.Match(host, pattern, RegexOptions.RightToLeft);
			//if (match.Success)
			//{
			//    result = match.Groups["domain"] + match.Groups["com"].ToString() + match.Groups["countryextension"];
			//}
			//return result;
			return host;
		}

		/// <summary>
		/// Lấy ra đường dẫn đầy đủ bao gồm scheme, host, port và application path (VD: http://egovcloud.vn:8080/ApplicationPath)
		/// </summary>
		/// <param name="request">Http Request</param>
		/// <returns></returns>
		public static string GetFullUrl(this HttpRequest request)
		{
			return string.Format("{0}://{1}{2}{3}",
									 request.Url.Scheme,
									 request.Url.Host,
									 GetPortExtension(request.Url.Port),
									 request.ApplicationPath);
		}

		/// <summary>
		/// Lấy ra đường dẫn đầy đủ bao gồm scheme, host và port (VD: http://egovcloud.vn:8080)
		/// </summary>
		/// <param name="request">Http Request</param>
		/// <returns></returns>
		public static string GetFullDomainUrl(this HttpRequestBase request)
		{
			return string.Format("{0}://{1}{2}",
									 request.Url.Scheme,
									 request.Url.Host,
									 GetPortExtension(request.Url.Port));
		}

		/// <summary>
		/// Lấy ra đường dẫn đầy đủ bao gồm scheme, host và port (VD: http://egovcloud.vn:8080)
		/// </summary>
		/// <param name="uri"></param>
		/// <returns></returns>
		public static string GetFullDomainUrl(this Uri uri)
		{
			return string.Format("{0}://{1}{2}",
									 uri.Scheme,
									 uri.Host,
									 GetPortExtension(uri.Port));
		}

		/// <summary>
		/// Lấy ra đường dẫn đầy đủ bao gồm scheme, host, port và application path (VD: http://egovcloud.vn:8080/ApplicationPath)
		/// </summary>
		/// <param name="request">Http Request</param>
		/// <returns></returns>
		public static string GetFullUrl(this HttpRequestBase request)
		{
			if (request.Url == null)
			{
				return string.Empty;
			}
			return string.Format("{0}://{1}{2}{3}",
									 request.Url.Scheme,
									 request.Url.Host,
									 GetPortExtension(request.Url.Port),
									 request.ApplicationPath);
		}

		/// <summary>
		/// hàm xử lý chuỗi userAgent lầy từ request của browser thành tên hệ điều hành người đăng nhập vào hệ thống
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static int GetOS(this HttpRequestBase request)
		{
			var userAgent = request.UserAgent;
			if (userAgent == null)
			{
				return (int)DeviceOS.Other;
			}

			foreach (KeyValuePair<int, string> kvpPair in dictionaryOS)
			{
				if (Regex.IsMatch(userAgent, kvpPair.Value, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
				{
					return kvpPair.Key;
				}
			}
			return (int)DeviceOS.Other;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static string GetOSName(this HttpRequestBase request)
		{
			var userAgent = request.UserAgent;
			if (userAgent.IndexOf("Windows NT 5.1") > 0)
			{
				return "Windows XP";
			}

			if (userAgent.IndexOf("Windows NT 6.0") > 0)
			{
				return "Windows VISTA";
			}

			if (userAgent.IndexOf("Windows NT 6.1") > 0)
			{
				return "Windows 7";
			}

			if (userAgent.IndexOf("Windows NT 6.2") > 0)
			{
				return "Windows 8";
			}

			if (userAgent.IndexOf("Windows NT 6.3") > 0)
			{
				return "Windows 8.1";
			}

			if (userAgent.IndexOf("Windows NT 10.0") > 0)
			{
				return "Windows 10";
			}

			if (Regex.IsMatch(userAgent, "(Android)"))
			{
				return "Android";
			}

			if (Regex.IsMatch(userAgent, "(Linux)|(X11)"))
			{
				return "Linux";
			}

			if (Regex.IsMatch(userAgent, "(Iphone)") || Regex.IsMatch(userAgent, "(Ipad)"))
			{
				return "iOS";
			}

			if (Regex.IsMatch(userAgent, "Macintosh"))
			{
				return "Mac OS";
			}

			return "Không xác định";
		}

		/// <summary>
		/// Trả về IP của client
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static string GetIP(this HttpRequestBase request)
		{
			return request.ServerVariables["REMOTE_ADDR"];
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="portNumber"></param>
		/// <returns></returns>
		private static string GetPortExtension(int portNumber)
		{
			var port = "";
			if (portNumber != 80 && portNumber != 443)
			{
				port = ":" + portNumber;
			}
			return port;
		}

		/// <summary>
		/// Trả về trạng thái xác định request gửi lên từ Mobile hoặc tablet
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static bool IsMobileOrTablet(this HttpRequestBase request)
		{
			var isMobile = request.Browser.IsMobileDevice;

			var userAgent = request.Browser.Capabilities[""].ToString();
			var r = new Regex("ipad|(android(?!.*mobile))|xoom|sch-i800|playbook|tablet|kindle|nexus|silk", RegexOptions.IgnoreCase);
			var isTablet = r.IsMatch(userAgent);

			Logging.StaticLog.Log(new List<string>() { "Login:" + userAgent });

			return isMobile || isTablet;
		}
	}
}
