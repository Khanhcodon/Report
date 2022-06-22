using System.Configuration;
using System.IO;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.SingleSignOnService;

namespace Bkav.eGovCloud.Helper
{
    public static class DbConnectionHelper
    {
        /// <summary>
        /// Trả về dbconnection của user hiện tại.
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns></returns>
        public static Connection GetDbConnectionByUser(string userName)
        {
            var client = GetSsoClient();
            var connection = Json2.ParseAs<Connection>(client.GetConnectionByUser(userName, ""));
            client.Close();
            return connection;
        }

        /// <summary>
        /// Trả về DbConnection cho Domain hiện tại
        /// </summary>
        /// <param name="domainName">Tên domain</param>
        /// <returns></returns>
        public static Connection GetDbConnectionByDomainName(string domainName)
        {
            if (string.IsNullOrEmpty(domainName))
            {
                return null;
            }

            var client = GetSsoClient();
            var connection = Json2.ParseAs<Connection>(client.GetConnectionByUser("", domainName));
            client.Close();
            return connection;
        }

        /// <summary>
        /// Trả về dbConnection theo user và domain
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="domainName">DomainName</param>
        /// <returns></returns>
        /// <remarks>
        /// Lưu ý domainName ở đây không phải là Request.GetDomainName() mà là cơ quan hiện tại đang thuộc vào. 
        /// Trong trường hợp mặc định thì là Request.GetDomainName(), còn lại là do người dùng lựa chọn.
        /// - với người dùng trong 1 cơ quan => ko cần xét theo domainName nữa.
        /// - Với người dùng nhiều cơ quan (quản trị cấp cao, cán bộ kiêm nghiệm) => càn lấy theo domainName (cơ quan đang chọn) tương ứng.
        /// - Trường hợp Username = "" => là trường hợp dữ liệu lấy qua WebApi => domainName là request.getdomainName();
        /// </remarks>
        public static Connection GetDbConnection(string userName, string domainName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return GetDbConnectionByDomainName(domainName);
            }

            var client = GetSsoClient();
            var result = Json2.ParseAs<Connection>(client.GetConnectionByUser(userName, domainName));
            return result;
        }

        private static CustomerServiceClient GetSsoClient()
        {
            var ssoUrl = Path.Combine(ConfigurationManager.AppSettings.Get("single-sign-on.domain"), "Customer");
            ssoUrl = ssoUrl.Replace("https", "http");
            var client = new CustomerServiceClient("CustomerEndpoint", ssoUrl);

            return client;
        }
    }
}