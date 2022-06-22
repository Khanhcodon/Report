using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.DataAccess;
using StackExchange.Profiling;
namespace Bkav.eGovCloud.Business.Utils
{
    /// <summary>
    /// Lớp helper hỗ trợ đọc ghi file domain config
    /// </summary>
    public static class DomainConfigHelper
    {
        private const string CONFIG_PATH = "App_Data/Config/domaincfg.txt";

        /// <summary>
        /// Thêm domain
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="allDomain">Trả về danh sách tất cả domain</param>
        public static void Create(Domain domain, out IEnumerable<Domain> allDomain)
        {
            var config = GetConfig().ToList();
            var checkExist = config.SingleOrDefault(c => c.DomainName.Equals(domain.DomainName, StringComparison.OrdinalIgnoreCase));
            if (checkExist != null)
            {
                config.Remove(checkExist);
            }
            // domain.DomainUsers = GetDomainUsers(domain);

            config.Add(domain);
            WriteConfig(config);
            allDomain = config;
        }

        /// <summary>
        /// Trả về danh sách các domain trong hệ thống
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Domain> Gets()
        {
            return GetConfig();
        }

        /// <summary>
        /// Xóa một domain
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="allDomain">Trả về danh sách tất cả domain</param>
        public static void Delete(Domain domain, out IEnumerable<Domain> allDomain)
        {
            var config = GetConfig().ToList();
            var checkDomain = config.SingleOrDefault(d => d.DomainName.Equals(domain.DomainName, StringComparison.OrdinalIgnoreCase));
            if (checkDomain != null)
            {
                config.Remove(checkDomain);
            }
            WriteConfig(config);
            allDomain = config;
        }

        /// <summary>
        /// Cập nhật 1 domain
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="allDomain">Trả về danh sách tất cả domain</param>
        public static void Update(Domain domain, out IEnumerable<Domain> allDomain)
        {
            var config = GetConfig().ToList();
            var checkDomain = config.SingleOrDefault(d => d.DomainName.Equals(domain.DomainName, StringComparison.OrdinalIgnoreCase));
            if (checkDomain != null)
            {
                config.Remove(checkDomain);

                domain.DomainUsers = GetDomainUsers(domain);
                config.Add(domain);
            }
            WriteConfig(config);
            allDomain = config;
        }

        /// <summary>
        /// Thêm người dùng vào domain
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="userName"></param>
        public static void AddUser(string domainName, string userName)
        {
            var config = GetConfig().ToList();
            var checkDomain = config.SingleOrDefault(d => d.DomainName.Equals(domainName, StringComparison.OrdinalIgnoreCase));
            if (checkDomain != null)
            {
                checkDomain.DomainUsers += ";" + userName.ToLower();
            }
            WriteConfig(config);
        }

        /// <summary>
        /// Trả về tên domain user hiện tại thuộc vào
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static Domain GetDomain(string userName)
        {
            var domains = Gets();
            Domain result = null;
            result = domains.FirstOrDefault(d => d.DomainUsers.Contains(";" + userName.ToLower() + ";"));
            return result;
        }

        /// <summary>
        /// Thêm người dùng vào domain
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="userName"></param>
        public static void AddUser(string domainName, IEnumerable<string> userName)
        {
            var config = GetConfig().ToList();
            var checkDomain = config.SingleOrDefault(d => d.DomainName.Equals(domainName, StringComparison.OrdinalIgnoreCase));
            if (checkDomain != null)
            {
                checkDomain.DomainUsers += ";" + string.Join(";", userName).ToLower();
            }
            WriteConfig(config);
        }

        private static IEnumerable<Domain> GetConfig()
        {
            var path = GetConfigPath();
            if (!File.Exists(path))
            {
                throw new Exception("File config không tồn tại");
            }

            var configStr = File.ReadAllText(path);
            if (string.IsNullOrWhiteSpace(configStr))
            {
                configStr = "[]";
            }

            var result = Json2.ParseAs<IEnumerable<Domain>>(configStr);

            return result;
        }

        private static void WriteConfig(IEnumerable<Domain> config)
        {
            var path = GetConfigPath();
            var configSaveToFile = config.Select(c => new
            {
                DomainName = c.DomainName,
                CustomerName = c.CustomerName,
                DomainId = c.DomainId,
                DomainUsers = c.DomainUsers,
                Connection = c.Connection,
                IsPrimary = c.IsPrimary,
                IsActivated = c.IsActivated
            });
            var content = Json2.Stringify(configSaveToFile);
            File.WriteAllText(path, content);
        }

        private static string GetConfigPath()
        {
            var basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(basePath, CONFIG_PATH);
            if (!File.Exists(path))
            {
                throw new Exception("File config không tồn tại");
            }

            return path;
        }

        private static string GetDomainUsers(Domain domain)
        {
            var provider = DataProviderManager.LoadDataProvider(domain.Connection);
            var connection = provider.InitDatabase(false);
            var domainContext = new EfContext(new StackExchange.Profiling.Data.EFProfiledDbConnection(connection, MiniProfiler.Current));
            var userDal = domainContext.GetRepository<Bkav.eGovCloud.Entities.Customer.User>();
            var result = userDal.GetsAs(u => u.Username.ToLower(), u => u.IsActivated);
            return ";" + string.Join(";", result);
        }
    }
}
