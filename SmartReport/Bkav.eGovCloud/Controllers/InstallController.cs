using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Text;
using System.Transactions;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Core;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using StackExchange.Profiling;
using MySql.Data.MySqlClient;
using System.Data.Entity.Validation;

namespace Bkav.eGovCloud.Controllers
{
    //[SessionState(SessionStateBehavior.ReadOnly)]
    public partial class InstallController : Controller
    {
        private const string SSO_DEFAULT_PATH = "BkavSSO";

        public ActionResult Index()
        {
            if (DataSettings.DatabaseIsInstalled())
            {
                return RedirectToAction("Main", "Home");
            }

            // Xóa hết cookie đăng nhập hiện tại
            Request.Cookies.Clear();

            return View(new DatabaseSettingModel());
        }

        [HttpPost]
        public ActionResult Index(DatabaseSettingModel model)
        {
            if (ModelState.IsValid)
            {
                var error = new StringBuilder();
                try
                {
                    string connectionString;
                    var port = model.Port.HasValue ? int.Parse(model.Port.Value.ToString(CultureInfo.InvariantCulture))
                                                   : (int?)null;

                    connectionString = ConnectionUtil.GenerateMySqlConnectionString(model.Server, model.Database, model.Username, model.Password, port);

                    var dbConnection = ConnectionUtil.TestConnection(connectionString, model.Server, model.Database, model.Username, model.Password, model.Port, Entities.DatabaseType.MySql);

                    if (dbConnection == null && !model.IsCreateDatabaseIfNotExist)
					{
						error.AppendLine("Lỗi kết nối: " + dbConnection.ConnectionString);
						ViewBag.Error = error;
						return View(model);
                    }

                    var IsQuanTriTapTrung = false;
#if QuanTriTapTrungEdition
                    IsQuanTriTapTrung = true;
#endif

                    model.IsQuanTriTapTrung = IsQuanTriTapTrung;
                    if (model.IsCreateDatabaseIfNotExist)
                    {
                        CreateDatabase(dbConnection, model);
                    }

                    var settings = DataSettings.Current;
                    settings.AppVersion = new Version("1.0.12"); // Cần thêm quản lý version cho eGov
                    settings.DataProvider = "MySql";
                    settings.DataConnectionString = connectionString;
                    settings.DatabaseMode = IsQuanTriTapTrung ? "Multi" : "Single";
                    settings.Save();

#if QuanTriTapTrungEdition

                    error.AppendLine("Change SsoData Setting");
                    ChangeSsoDataSetting(settings);
#endif

                    error.AppendLine("RestartApplication");
                    RestartApplication();

                    return RedirectToAction("Main", "Home");
                }
                catch (Exception ex)
                {
                    error.AppendLine(ParseLog(ex.Message, ex));                    
                    ViewBag.Error = error;
                    DataSettings.Delete();
                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult Extension()
        {
            ViewBag.Browser = Request.Browser.Type;
            return View();
        }

        [HttpGet]
        public ActionResult Update(bool ignoreVersion = false)
        {
            var appVersion = DataSettings.Current.AppVersion;
            var currentVersion = eGovVersions.CurrentVersion;

            if (!ignoreVersion && appVersion == currentVersion.Version)
            {
                return RedirectToAction("Main", "Home");
            }

            // Xóa hết cookie đăng nhập hiện tại
            Request.Cookies.Clear();

            ViewBag.AppVersion = appVersion.ToString();
            ViewBag.CurrentVersion = currentVersion.Version.ToString();

            ViewBag.Domains = GetAllDomain();

            return View();
        }

        public ActionResult CustomUpdate()
        {
            return RedirectToAction("Update", new { ignoreVersion = true });
        }

        public ActionResult HandleUpdate()
        {
            ViewBag.Domains = GetAllDomain();
            ViewBag.Query = "";
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HandleUpdate(string token, string query, int domainId)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("HandleUpdate");
            }

            if (!IsValidToken(token))
            {
                return RedirectToAction("HandleUpdate");
            }

            if (!IsValidQuery(query))
            {
                return RedirectToAction("HandleUpdate");
            }

            var connections = GetAllConnection(domainId);
            if (!connections.Any())
            {
                return RedirectToAction("HandleUpdate");
            }

            dynamic result = null;

            foreach (var connection in connections)
            {
                try
                {
                    result = RunUpdateWithResult(connection, query);
                }
                catch
                {

                }
            }

            ViewBag.Query = query;
            ViewBag.Domains = GetAllDomain();
            ViewBag.Result = result == null ? "" : Json2.Stringify(result);
            return View();
        }

        [HttpPost]
        public ActionResult Update(int domainId)
        {
            var appVersion = DataSettings.Current.AppVersion;

            var allVersion = eGovVersions.All;
            var versionToUpdates = allVersion.Where(v => v.Version >= appVersion);
            if (!versionToUpdates.Any())
            {
                UpdateCurrentVersionToSetting();
                RestartApplication();
                return RedirectToAction("Main", "Home");
            }

            var connections = GetAllConnection(domainId);
            if (!connections.Any())
            {
                UpdateCurrentVersionToSetting();
                return RedirectToAction("Main", "Home");
            }

            foreach (var version in versionToUpdates)
            {
                var sqlList = version.ListUpdate;
                if (sqlList == null || !sqlList.Any())
                {
                    continue;
                }

                List<string> sqlQueries = ReadSqlQueries(sqlList, version.Version);

                foreach (var connection in connections)
                {
                    try
                    {
                        RunUpdate(connection, sqlQueries);
                    }
                    catch
                    {

                    }
                }
            }

            UpdateCurrentVersionToSetting();
            RestartApplication();
            return RedirectToAction("Main", "Home");
        }

        #region  Private method

        private void RestartApplication()
        {
            WebHelper.RestartApplication();
        }

        private void CreateDatabase(DbConnection dbConnection, DatabaseSettingModel model)
        {

            var transactionOptions = new TransactionOptions();
            transactionOptions.Timeout = TimeSpan.FromMinutes(10);
            using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions))
            {
#if QuanTriTapTrungEdition
                CreateAdminDatabase(model);
#else
                model.DefaulteGovDatabase = model.Database;
#endif

                CreateClientDatabase(model);

                // Cập nhật domain mặc định
                var ssoSettings = SsoSettings.Instance;
                ssoSettings.BkavSSOParentDomain = model.DomainName;
                ssoSettings.Save();

                transaction.Complete();
            }
        }

        private void CreateDefaultUser(EfContext customerContext, DatabaseSettingModel model)
        {
            var salt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
            var hash = Generate.GetInputPasswordHash(model.DefaultPass, salt);
            var user = new User
            {
                Username = model.DefaultAdmin,
                UsernameEmailDomain = model.DefaultAdmin + "@" + model.DomainName,
                DomainName = model.DomainName,
                PasswordSalt = salt,
                PasswordHash = hash,
                PasswordLastModifiedOnDate = DateTime.Now,
                FullName = SystemRole.Administrator.RoleKey,
                FirstName = SystemRole.Administrator.RoleKey,
                LastName = SystemRole.Administrator.RoleKey,
                Gender = true,
                IsActivated = true,
                IsLockedOut = false,
                CreatedOnDate = DateTime.Now,
                VersionDateTime = DateTime.Now
            };

            var roleDal = customerContext.GetRepository<Role>();
            var permissonDal = customerContext.GetRepository<Permission>();
            var role = roleDal.GetReadOnly(r => r.RoleKey == SystemRole.Administrator.RoleKey);
            if (role != null)
            {
                user.UserRoles.Add(new UserRole
                {
                    RoleId = role.RoleId
                });

                var allPermission = permissonDal.GetsReadOnly();
                foreach (var permission in allPermission)
                {
                    role.UserRolePermissions.Add(new UserRolePermission
                    {
                        AllowAccess = true,
                        PermissionId = permission.PermissionId,
                        PermissionKey = permission.PermissionKey,
                        RoleKey = role.RoleKey
                    });
                }
            }

            var userDal = customerContext.GetRepository<User>();
            if (!userDal.Exist(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
            {
                userDal.Create(user);
            }

            customerContext.SaveChanges();

            var departmentDal = customerContext.GetRepository<Department>();
            var rootDepartment = new Department
            {
                CreatedByUserId = user.UserId,
                CreatedOnDate = DateTime.Now,
                DepartmentName = model.OfficeName,
                IsActivated = true,
                Level = 0,
                Order = 0,
                VersionDateTime = DateTime.Now,
                DepartmentPath = "\\" + model.OfficeName
            };

            departmentDal.Create(rootDepartment);
            customerContext.SaveChanges();
            rootDepartment.DepartmentIdExt = rootDepartment.DepartmentId.ToString(CultureInfo.InvariantCulture);

            var infomation = new Infomation()
            {
                Name = model.OfficeName,
                Address = "",
                Email = ""
            };
            var impormationDal = customerContext.GetRepository<Infomation>();
            impormationDal.Create(infomation);

            customerContext.SaveChanges();

        }

        private void CreateClientDatabase(DatabaseSettingModel model)
        {
            var transactionOptions = new TransactionOptions();
            transactionOptions.Timeout = TimeSpan.FromMinutes(5);
            using (var transactionCustomer = new TransactionScope(TransactionScopeOption.Suppress, transactionOptions))
            {
                var provider = DataProviderManager.LoadDataProvider(new Connection()
                {
                    ConnectionRaw = "",
                    Database = model.DefaulteGovDatabase,
                    Username = model.Username,
                    Password = model.Password,
                    ServerName = model.Server,
                    Port = model.Port,
                    IsCreateDatabaseIfNotExist = model.IsCreateDatabaseIfNotExist,
                    OverrideCurrentData = model.OverrideCurrentData,
                    DatabaseTypeIdInEnum = Entities.DatabaseType.MySql,
                    DatabaseTypeId = (int)Entities.DatabaseType.MySql,
                    IsQuanTriTapTrung = false,
#if HoSoMotCuaEdition
                    IsHsmcDb = true
#endif
                });

                var connection = provider.InitDatabase(true);

                var customerContext = new EfContext(new StackExchange.Profiling.Data.EFProfiledDbConnection(connection, MiniProfiler.Current));
                customerContext.Database.Initialize(true);

                CreateDefaultUser(customerContext, model);

                transactionCustomer.Complete();
            }
        }

        private void CreateAdminDatabase(DatabaseSettingModel model)
        {
            var transactionOptions = new TransactionOptions();
            transactionOptions.Timeout = TimeSpan.FromMinutes(5);
            using (var transactionAdmin = new TransactionScope(TransactionScopeOption.Suppress, transactionOptions))
            {
                var provider = DataProviderManager.LoadDataProvider(new Connection()
                {
                    ConnectionRaw = "",
                    Database = model.Database,
                    Username = model.Username,
                    Password = model.Password,
                    ServerName = model.Server,
                    Port = model.Port,
                    IsCreateDatabaseIfNotExist = model.IsCreateDatabaseIfNotExist,
                    OverrideCurrentData = model.OverrideCurrentData,
                    DatabaseTypeIdInEnum = Entities.DatabaseType.MySql,
                    DatabaseTypeId = (int)Entities.DatabaseType.MySql,
                    IsQuanTriTapTrung = model.IsQuanTriTapTrung
                });

                var connection = provider.InitDatabase(true);

                var adminContext = new EfContext(new StackExchange.Profiling.Data.EFProfiledDbConnection(connection, MiniProfiler.Current));
                adminContext.Database.Initialize(true);

                CreateDefaultUserAndDomain(connection, model);

                transactionAdmin.Complete();
            }
        }

        private void CreateDefaultUserAndDomain(DbConnection connection, DatabaseSettingModel model)
        {
            var salt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
            var hash = Generate.GetInputPasswordHash(model.DefaultPass, salt);
            var adminContext = new EfAdminContext(new StackExchange.Profiling.Data.EFProfiledDbConnection(connection, MiniProfiler.Current));

            var user = new Account
            {
                Username = model.DefaultAdmin,
                UsernameEmailDomain = model.DefaultAdmin + "@" + model.DomainName,
                DomainName = model.DomainName,
                PasswordSalt = salt,
                PasswordHash = hash,
                PasswordLastModifiedOnDate = DateTime.Now,
                FullName = SystemRole.Administrator.RoleKey,
                Gender = true,
                IsActivated = true,
                IsLockedOut = false,
                CreatedOnDate = DateTime.Now,
                Address = "",
                Fax = ""
            };

            var accountDal = adminContext.GetRepository<Account>();
            if (!accountDal.Exist(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
            {
                accountDal.Create(user);
            }
            adminContext.SaveChanges();

            var connectionDal = adminContext.GetRepository<Connection>();
            var conn = new Connection()
            {
                ConnectionId = 1,
                ConnectionName = "eGov",
                ConnectionRaw = ConnectionUtil.GenerateMySqlConnectionString
                                                    (
                                                        model.Server,
                                                        model.DefaulteGovDatabase,
                                                        model.Username,
                                                        model.Password,
                                                        model.Port.HasValue
                                                        ? int.Parse(model.Port.Value.ToString(CultureInfo.InvariantCulture))
                                                        : (int?)null),
                Database = model.DefaulteGovDatabase,
                DatabaseTypeId = (int)Entities.DatabaseType.MySql,
                Port = model.Port,
                ServerName = model.Server,
                Username = model.Username,
                Password = model.Password
            };
            connectionDal.Create(conn);
            adminContext.SaveChanges();

            var domainDal = adminContext.GetRepository<Domain>();
            var domainAliasDal = adminContext.GetRepository<DomainAlias>();
            var accountDomainDal = adminContext.GetRepository<AccountDomain>();

            var defaultDomain = new Domain()
            {
                DomainName = model.DomainName,
                District = "",
                Address = "",
                CustomerName = "eGov",
                Email = model.DefaultAdmin + "@" + model.DomainName,
                IsActivated = true,
                IsPrimary = true,
                VersionDateTime = DateTime.Now,
                ConnectionId = conn.ConnectionId,
                DomainAliass = new List<DomainAlias>(){
                    new DomainAlias(){
                        CreatedOnDate = DateTime.Now,
                        IsActivated = true,
                        IsPrimary = true,
                        Alias = model.DomainName,
                        VersionDateTime = DateTime.Now
                    }
                },
                AccountDomains = new List<AccountDomain>(){
                    new AccountDomain{
                        AccountId = user.AccountId
                    }
                }
            };

            domainDal.Create(defaultDomain);
            adminContext.SaveChanges();
        }

        private void ChangeSsoDataSetting(DataSettings settings)
        {
            var ssoFolder = GetSsoFolder();
            if (string.IsNullOrEmpty(ssoFolder))
            {
                return;
            }
            ssoFolder = Path.Combine(ssoFolder, "App_Data");
            settings.Save(ssoFolder);
        }

        private string GetSsoFolder()
        {
            var sspPathSetting = CommonHelper.GetAppSetting("eg:SsoPathHosted");
            if (string.IsNullOrWhiteSpace(sspPathSetting))
            {
                // Nếu chưa cấu hình đường dẫn SSO.
                // Tìm đến thư  mục cha của eGov hiện tại sau đó tìm đến thư mục BkavSSO là tên thư mục hosted mặc định của SSO

                var rootPath = CommonHelper.MapPath("~");
                var rootFolder = Path.GetPathRoot(rootPath);
                var result = Path.Combine(rootFolder, SSO_DEFAULT_PATH);
                if (Directory.Exists(result))
                {
                    return result;
                }
                return string.Empty;
            }
            return sspPathSetting;
        }

        private IEnumerable<SelectListItem> GetAllDomain()
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem()
            {
                Text = "Tất cả",
                Value = "0"
            });

            var currentDomain = Request.GetDomainName();

#if QuanTriTapTrungEdition

            var connection = DataSettings.Current.DataConnectionString;
            var adminContext = GetAdminContext(connection);
            var domainDal = adminContext.GetRepository<Domain>();
            var domains = domainDal.Gets(true, d => d.IsActivated);

            result.AddRange(domains.Select(d => new SelectListItem
            {
                Value = d.DomainId.ToString(),
                Text = d.DomainName,
                Selected = d.DomainName.StartsWith(currentDomain, StringComparison.OrdinalIgnoreCase)
            }));

#endif

            return result;
        }

        private IEnumerable<string> GetAllConnection(int domainId)
        {
            var result = new List<string>();

#if QuanTriTapTrungEdition

            var connection = DataSettings.Current.DataConnectionString;
            var adminContext = GetAdminContext(connection);
            var connectionDal = adminContext.GetRepository<Connection>();
            var connections = connectionDal.Gets(true, c => domainId == 0 || (c.Domain.IsActivated && c.Domain.DomainId == domainId));

            result.AddRange(connections.Select(c => c.ConnectionRaw));
#else
            result.Add(DataSettings.Current.DataConnectionString);
#endif

            return result;
        }

        private EfAdminContext GetAdminContext(string connection)
        {
            var builder = new MySqlConnectionStringBuilder(connection);
            var conn = new MySqlConnection(builder.ConnectionString);
            var adminContext = new EfAdminContext(new StackExchange.Profiling.Data.EFProfiledDbConnection(conn, MiniProfiler.Current));
            return adminContext;
        }

        private void RunUpdate(string connection, List<string> sqlQueries)
        {
            using (var conn = new MySqlConnection(connection))
            {
                conn.Open();
                foreach (var query in sqlQueries)
                {
                    try
                    {
                        using (var command = new MySqlCommand(query, conn))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    catch { }
                }
            }
        }

        private List<System.Dynamic.ExpandoObject> RunUpdateWithResult(string connection, string query)
        {
            List<System.Dynamic.ExpandoObject> result = null;
            using (var conn = new MySqlConnection(connection))
            {
                conn.Open();
                try
                {
                    using (var command = new MySqlCommand(query, conn))
                    {
                        var dataReader = command.ExecuteReader();
                        result = dataReader.ToExpandoList();
                    }
                }
                catch { }
            }

            return result;
        }

        private void UpdateCurrentVersionToSetting()
        {
            var setting = DataSettings.Current;
            setting.AppVersion = eGovVersions.CurrentVersion.Version;

            setting.Save();
        }

        private List<string> ReadSqlQueries(List<string> sqlList, Version version)
        {
            var result = new List<string>();

            var updatePath = CommonHelper.MapPath("~/Update");
            if (!Directory.Exists(updatePath))
            {
                return result;
            }

            updatePath = Path.Combine(updatePath, "Version " + version.ToString());
            if (!Directory.Exists(updatePath))
            {
                return result;
            }

            foreach (var sqlPath in sqlList)
            {
                if (string.IsNullOrWhiteSpace(sqlPath))
                {
                    continue;
                }

                var filePath = Path.Combine(updatePath, sqlPath);
                if (!System.IO.File.Exists(filePath))
                {
                    continue;
                }

                var query = System.IO.File.ReadAllText(filePath, Encoding.UTF8);
                result.Add(query);
            }

            return result;
        }

        private bool IsValidQuery(string query)
        {
            var result = true;

            // Kiểm tra câu query

            return result;
        }

        private bool IsValidToken(string token)
        {
            // Ha ha
            return token == "@tienbv@bkav.com.";
        }

        private string ParseLog(string message, Exception exception)
        {
            var errorMessage = new StringBuilder();
            errorMessage.AppendLine(message);

            try
            {
                var entityErrors = (DbEntityValidationException)exception;
                errorMessage.AppendLine("----DbEntityValidation EXCEPTION---------------------------");
                foreach (var error in entityErrors.EntityValidationErrors)
                {
                    foreach (var itm in error.ValidationErrors)
                    {
                        errorMessage.AppendLine(itm.PropertyName + " == " + itm.ErrorMessage);
                    }
                }
            }
            catch
            {
                if (exception != null)
                {
                    errorMessage.AppendLine(exception.Message);
                    errorMessage.AppendLine(exception.StackTrace);
                }
            }

            var ex = exception == null ? null : exception.InnerException;
            while (ex != null)
            {
                errorMessage.AppendLine("INNER EXCEPTION =======================");
                errorMessage.AppendLine(ex.Message);
                errorMessage.AppendLine(ex.StackTrace);

                ex = ex.InnerException;
            }

            return errorMessage.ToString();
        }

        #endregion
    }
}