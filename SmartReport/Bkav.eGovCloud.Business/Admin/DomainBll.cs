using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Customer;
using StackExchange.Profiling;
using DatabaseType = Bkav.eGovCloud.Entities.DatabaseType;
using Bkav.eGovCloud.Core.Logging;

namespace Bkav.eGovCloud.Business.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DomainBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : BLL tương ứng với bảng Domain trong CSDL
    /// </summary>
    public class DomainBll : ServiceBase
    {
        private readonly IRepository<Domain> _domainRepository;
        private readonly IRepository<DomainAlias> _domainAliasRepository;
        private readonly IRepository<Connection> _connectionRepository;
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<AccountDomain> _accountDomainRepository;
        //private readonly MemoryCacheManager _cacheManager;

        ///<summary>
        /// Khởi tạo class <see cref="DomainBll"/>.
        ///</summary>
        ///<param name="context">Context</param>
        ///<param name="cache">Cache</param>
        public DomainBll(IDbAdminContext context, MemoryCacheManager cache)
            : base(context)
        {
            _domainRepository = Context.GetRepository<Domain>();
            _domainAliasRepository = Context.GetRepository<DomainAlias>();
            _connectionRepository = Context.GetRepository<Connection>();
            _accountRepository = Context.GetRepository<Account>();
            _accountDomainRepository = context.GetRepository<AccountDomain>();
            //_cacheManager = cache;
        }

        /// <summary>
        /// Lấy ra tất cả domain phù hợp với các điều kiện truyền vào. Nếu tất cả các điều kiện đều là null thì sẽ lấy ra tất cả các domain
        /// </summary>
        /// <param name="domainName">Tên domain</param>
        /// <param name="province">Tỉnh, thành phố</param>
        /// <returns>Danh sách các domain phù hợp với điều kiện</returns>
        public IEnumerable<Domain> Gets(string domainName = null, string province = null)
        {
            Expression<Func<Domain, bool>> spec = null;
            if (!string.IsNullOrWhiteSpace(domainName))
            {
                spec = r => r.DomainName.Contains(domainName.ToLower());
            }

            if (spec == null)
            {
                if (!string.IsNullOrWhiteSpace(province))
                {
                    spec = r => r.Province == province;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(province))
                {
                    spec = spec.And(d => d.Province == province);
                }
            }

            spec = spec == null ? d => d.IsActivated : spec.And(d => d.IsActivated);

            return _domainRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Trả về danh sách domain của user hiện tại thuộc vào
        /// </summary>
        /// <param name="userEmailDomain"></param>
        /// <param name="isStatictis">Giá trị xác định lấy domains cho thống kê</param>
        /// <returns></returns>
        public IEnumerable<Domain> GetsByUser(string userEmailDomain, bool isStatictis = false)
        {
            var result = new List<Domain>();
            var account = _accountRepository.Get(true, a => a.UsernameEmailDomain.Equals(userEmailDomain, StringComparison.OrdinalIgnoreCase));
            if (account == null)
            {
                return result;
            }

            if (isStatictis)
            {
                if (account.HasViewReport == true)
                {
                    return _domainRepository.GetsReadOnly(d => d.IsActivated);
                }

                return result;
            }

            var accountDomains = account.AccountDomains;
            if (!accountDomains.Any())
            {
                return result;
            }

            var domainIds = accountDomains.Select(d => d.DomainId);
            var domains = _domainRepository.GetsReadOnly(d => domainIds.Contains(d.DomainId) && d.IsActivated);

            return domains;
        }

        /// <summary>
        /// Lấy ra các domain theo mảng các id domain
        /// </summary>
        /// <param name="ids">Mảng các id domain</param>
        /// <returns>Danh sách các domain có id bằng với các trong mảng truyền vào</returns>
        public IEnumerable<Domain> Gets(int[] ids)
        {
            IEnumerable<Domain> result = null;
            if (ids != null)
            {
                Expression<Func<Domain, bool>> spec = d => ids.Contains(d.DomainId);
                result = _domainRepository.Gets(false, spec);
            }
            return result;
        }

        /// <summary>
        /// Trả về tất cả domain trong hệ thống (có lưu cache)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain> GetsAllWithCache()
        {
            return Gets();
            //return _cacheManager.Get<IEnumerable<Domain>>(CacheParam.AllDomainKey, CacheParam.AllDomainCacheTimeOut, () =>
            //{
            //    return Gets();
            //});
        }

        /// <summary>
        /// Lấy ra tất cả các tỉnh đã có khách hàng và tổng số khách hàng tương ứng với tỉnh đó
        /// </summary>
        /// <returns>Chuỗi json có dạng [{name:"Tên tỉnh, thành phố + số domain", value:"Tên tỉnh, thành phố"}, {...}] 
        /// </returns>
        public string GetsAvailableProvince()
        {
            return _domainRepository.GetsAs(d => d.Province)
                    .GroupBy(d => d)
                    .Select(d => new { name = d.Key + " (" + d.Count() + ")", value = d.Key })
                    .OrderBy(d => d.name).StringifyJs();
        }

        /// <summary>
        /// Lấy ra tất cả các domain có thể gán làm domain con
        /// </summary>
        /// <param name="ignoreId">Id domain cần loại khỏi danh sách</param>
        /// <returns>Chuỗi json có dạng [{lable:"Tên domain + Tên khách hàng", value:"Id domain", domain:"Tên domain", customerName:"Tên khách hàng", isfree:"Domain này chưa có cha:true, có cha: false", category:"Loại domain(có cha, không có cha)"}] </returns>
        public string GetsDomainChildenAvailable(int? ignoreId = null)
        {
            var domainRoot = _domainRepository.GetsReadOnly(d => !d.ParentId.HasValue);
            var idDomainParent = _domainRepository.GetsAs(d => d.ParentId.Value, d => d.ParentId.HasValue).Distinct();
            var domainAvailableTemp = domainRoot.Where(d => !idDomainParent.Contains(d.DomainId))
                                .Concat(_domainRepository.GetsReadOnly(d => d.ParentId.HasValue));
            var domainAvailable = ignoreId.HasValue ? domainAvailableTemp.Where(d => d.DomainId != ignoreId) : domainAvailableTemp;
            return domainAvailable.Select(d => new
                                              {
                                                  label = d.DomainName + " - " + d.CustomerName,
                                                  value = d.DomainId,
                                                  domain = d.DomainName,
                                                  customerName = d.CustomerName,
                                                  isfree = !d.ParentId.HasValue
                                                  //category = d.ParentId.HasValue
                                                  //              ? _resourceService.GetResource("Domain.CreateOrEdit.Fields.DomainChildren.ExistParent")
                                                  //              : _resourceService.GetResource("Domain.CreateOrEdit.Fields.DomainChildren.NotExistParent")
                                              }).OrderBy(d => d.label).StringifyJs();
        }

        /// <summary>
        /// Lấy ra domain theo id
        /// </summary>
        /// <param name="id">Id của domain</param>
        /// <returns>Entity domain</returns>
        public Domain Get(int id)
        {
            Domain result = null;
            if (id > 0)
            {
                result = _domainRepository.Get(id);
                result.Connection = _connectionRepository.Get(result.ConnectionId);
            }
            return result;
        }

        /// <summary>
        /// Lấy ra id của domain theo alias
        /// </summary>
        /// <param name="domainAlias">Domain alias</param>
        /// <returns>id của domain</returns>
        public int GetDomainIdByDomainAlias(string domainAlias)
        {
            var id = _domainAliasRepository.GetAs(d => d.DomainId, d => d.Alias == domainAlias);
            if (id <= 0)
            {
                throw new EgovException("Domain not found!");
            }
            return id;
        }

        /// <summary>
        /// Tạo mới domain
        /// </summary>
        /// <param name="domain">Entity domain</param>
        /// <param name="newAccount">Entity người dùng phía quản trị bkav</param>
        /// <param name="users">Entity người dùng phía khách hàng</param>
        /// <param name="currentUser">Entity người dùng hiện tại</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity domain truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi tên domain đã tồn tại</exception>
        public void Create(Domain domain, Account newAccount, List<User> users, User currentUser)
        {
            if (domain == null)
            {
                throw new ArgumentNullException("domain");
            }
            if (users.Count == 0)
            {
                throw new ArgumentNullException("user");
            }
            if (_domainAliasRepository.Exist(DomainAliasQuery.WithAlias(domain.DomainName)))
            {
                throw new EgovException(string.Format("Domain {0} đã tồn tại", domain.DomainName));
            }

            var connection = TestConnection(domain.Connection);
            if (connection == null && !domain.Connection.IsCreateDatabaseIfNotExist)
            {
                throw new EgovException("Lỗi kết nối: " + connection.ConnectionString);
            }

            var transactionOptions = new TransactionOptions();
            transactionOptions.Timeout = TimeSpan.FromMinutes(5);
            using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions))
            {
                using (var transactionCustomer = new TransactionScope(TransactionScopeOption.Suppress, transactionOptions))
                {
                    var isCreateDatabase = connection == null;
                    domain.Connection.OverrideCurrentData = domain.Connection.IsCreateDatabaseIfNotExist;

#if HoSoMotCuaEdition
                    domain.Connection.IsHsmcDb = true;
#endif

                    var provider = DataProviderManager.LoadDataProvider(domain.Connection);
                    connection = provider.InitDatabase(isCreateDatabase);

                    var customerContext = new EfContext(new StackExchange.Profiling.Data.EFProfiledDbConnection(connection, MiniProfiler.Current));
                    customerContext.Database.Initialize(false);

                    var roleDal = customerContext.GetRepository<Role>();
                    var permissonDal = customerContext.GetRepository<Permission>();

                    foreach (var user in users)
                    {
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
                            DepartmentName = domain.CustomerName,
                            IsActivated = true,
                            Level = 0,
                            Order = 0,
                            VersionDateTime = DateTime.Now,
                            DepartmentPath = "\\" + domain.CustomerName
                        };
                        departmentDal.Create(rootDepartment);
                        customerContext.SaveChanges();
                        rootDepartment.DepartmentIdExt = rootDepartment.DepartmentId.ToString(CultureInfo.InvariantCulture);
                        customerContext.SaveChanges();
                    }

                    var infomation = new Infomation()
                    {
                        Name = domain.CustomerName,
                        Address = "",
                        Email = ""
                    };
                    var impormationDal = customerContext.GetRepository<Infomation>();
                    impormationDal.Create(infomation);

                    customerContext.SaveChanges();

                    transactionCustomer.Complete();
                }

                if (newAccount != null)
                {
                    var accountExisted = _accountRepository.Get(true, a => a.Username.Equals(newAccount.Username, StringComparison.OrdinalIgnoreCase));
                    if (accountExisted == null)
                    {
                        _accountRepository.Create(newAccount);
                    }
                    else
                    {
                        newAccount = accountExisted;
                    }

                    domain.AccountDomains.Add(new AccountDomain
                    {
                        AccountId = newAccount.AccountId,
                        DomainId = domain.DomainId
                    });
                }
                _domainRepository.Create(domain);

                //Đây là account tạo mới
                var currentAccount = _accountRepository.Get(true, a => a.Username.Equals(newAccount.Username, StringComparison.OrdinalIgnoreCase));
                if (currentAccount != null)
                {
                    domain.AccountDomains.Add(new AccountDomain
                    {
                        AccountId = currentAccount.AccountId,
                        DomainId = domain.DomainId
                    });
                }

                //Tạo mới accountDomain mang giá trị domain tạo mới và accountId của Id đăng nhập hiện tại (để hiển thị quyền quản trị)
                var adminAccount = _accountRepository.Get(true, a => a.UsernameEmailDomain.Equals(currentUser.UsernameEmailDomain, StringComparison.OrdinalIgnoreCase));
                if (adminAccount != null)
                {
                    domain.AccountDomains.Add(new AccountDomain
                    {
                        AccountId = adminAccount.AccountId,
                        DomainId = domain.DomainId
                    });
                }

                if (domain.IsPrimary)
                {
                    var currentPrimary = _domainRepository.Get(false, d => d.IsPrimary && d.DomainId != domain.DomainId);
                    if (currentPrimary != null)
                    {
                        currentPrimary.IsPrimary = false;
                    }
                }


                Context.SaveChanges();

                domain.ConnectionId = domain.Connection.ConnectionId;
                Context.SaveChanges();

                // _cacheManager.Remove(CacheParam.AllDomainKey);
                transaction.Complete();
            }
        }

        /// <summary>
        /// Cập nhật thông tin đường dẫn
        /// </summary>
        /// <param name="domain">Entity domain</param>
        /// <param name="oldDomainName">Tên domain trước khi cập nhật</param>
        /// <param name="domainIds">Mảng id các domain con</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity domain truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi tên domain đã tồn tại</exception>
        public void Update(Domain domain, string oldDomainName, int[] domainIds)
        {
            if (domain == null)
            {
                throw new ArgumentNullException("domain");
            }
            if (_domainAliasRepository.Exist(DomainAliasQuery.WithAlias(domain.DomainName).And(d => d.Alias.ToLower() != oldDomainName.ToLower())))
            {
                throw new EgovException(string.Format("Domain {0} đã tồn tại", domain.DomainName));
            }

            if (domainIds == null)
            {
                foreach (var delete in domain.DomainChildren)
                {
                    delete.ParentId = null;
                }
            }
            else
            {
                IEnumerable<int> domainChildenIdsAdd;
                IEnumerable<int> domainChildenIdsDelete;
                var isEqual = domain.DomainChildren.Select(d => d.DomainId)
                                    .CompareTo(domainIds, out domainChildenIdsDelete, out domainChildenIdsAdd);
                if (!isEqual)
                {
                    if (domainChildenIdsDelete != null && domainChildenIdsDelete.Any())
                    {
                        var domainChildenDelete = _domainRepository.Gets(false, d => domainChildenIdsDelete.Contains(d.DomainId));
                        foreach (var delete in domainChildenDelete)
                        {
                            delete.ParentId = null;
                        }
                    }
                }
                if (domainChildenIdsAdd != null && domainChildenIdsAdd.Any())
                {
                    var domainChildenAdd = _domainRepository.Gets(false, d => domainChildenIdsAdd.Contains(d.DomainId));
                    foreach (var add in domainChildenAdd)
                    {
                        add.ParentId = domain.DomainId;
                    }
                }
            }

            if (domain.IsPrimary)
            {
                var currentPrimary = _domainRepository.Get(false, d => d.IsPrimary && d.DomainId != domain.DomainId);
                if (currentPrimary != null)
                {
                    currentPrimary.IsPrimary = false;
                }
            }

            Context.SaveChanges();

            // IEnumerable<Domain> allDomain;
            // DomainConfigHelper.Update(domain, out allDomain);
            // _cacheManager.Remove(CacheParam.AllDomainKey);
            //_cacheManager.Set(CacheParam.DomainNameKey, allDomain, CacheParam.DomainNameCacheTimeOut);
        }

        /// <summary>
        /// Xóa bỏ một domain
        /// </summary>
        /// <param name="domain"></param>
        public void Delete(Domain domain)
        {
            if (domain == null)
            {
                throw new ArgumentNullException("domain");
            }

            var domainAlias = domain.DomainAliass.ToList();
            foreach (var alias in domainAlias)
            {
                _domainAliasRepository.Delete(alias);
            }
            var connection = _connectionRepository.Get(domain.ConnectionId);
            if (connection != null)
            {
                _connectionRepository.Delete(connection);
            }

            var accountDomains = _accountDomainRepository.Gets(false, a => a.DomainId == domain.DomainId);
            foreach (var ad in accountDomains)
            {
                _accountDomainRepository.Delete(ad);
            }

            _domainRepository.Delete(domain);


            Context.SaveChanges();

            // _cacheManager.Remove(CacheParam.AllDomainKey);
        }

        /// <summary>
        /// Kiểm tra connection tới CSDL có chính xác hay không
        /// </summary>
        /// <param name="connection">Entity connection</param>
        /// <returns>Trả về DbConnection nếu connection chính xác, ngược lại trả về null</returns>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity connection truyền vào bị null</exception>
        public DbConnection TestConnection(Connection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            return ConnectionUtil.TestConnection(connection.ConnectionRaw, connection.ServerName, connection.Database,
                connection.Username, connection.Password, connection.Port, connection.DatabaseTypeIdInEnum);
        }

        /// <summary>
        /// Tự sinh chuỗi connection tới CSDL dựa vào entity connection
        /// </summary>
        /// <param name="connection">Entity connection</param>
        /// <returns>Chuỗi connection</returns>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity connection truyền vào bị null</exception>
        public string GenerateConnectionString(Connection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            var connectionString = string.Empty;
            switch (connection.DatabaseTypeIdInEnum)
            {
                case DatabaseType.MySql:
                    connectionString = ConnectionUtil
                        .GenerateMySqlConnectionString(connection.ServerName,
                                                       connection.Database,
                                                       connection.Username,
                                                       connection.Password,
                                                       connection.Port.HasValue
                                                           ? int.Parse(
                                                               connection.Port.Value.
                                                                   ToString(CultureInfo.InvariantCulture))
                                                           : (int?)null);
                    break;
                case DatabaseType.SqlServer:
                    connectionString = ConnectionUtil
                        .GenerateSqlConnectionString(connection.ServerName,
                                                     connection.Database,
                                                     connection.Username,
                                                     connection.Password);
                    break;
                case DatabaseType.Oracle:
                    //TODO: Them connection string cho oracle
                    break;
            }

            return connectionString;
        }

        /// <summary>
        /// Lấy ra connection cho từng domain
        /// </summary>
        /// <returns>Connection stringify</returns>
        public string GetConnection(string domainName)
        {
            var alias = _domainAliasRepository.GetReadOnly(a => a.Alias == domainName);
            if (alias == null)
            {
                throw new EgovException("Domain not found!");
            }
            var domainId = alias.DomainId;
            var domain = Get(domainId);
            var connection = domain.Connection;
            return (new Connection
                        {
                            ConnectionId = connection.ConnectionId,
                            Database = connection.Database,
                            ConnectionRaw = connection.ConnectionRaw,
                            DatabaseTypeIdInEnum = connection.DatabaseTypeIdInEnum,
                            DatabaseTypeId = connection.DatabaseTypeId,
                            Password = connection.Password,
                            Port = connection.Port,
                            ServerName = connection.ServerName,
                            Username = connection.Username
                        }).Stringify();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public string GetConnectionNew(string domainName)
        {
            var domain = _domainRepository.Get(true, d => d.DomainName.Equals(domainName, StringComparison.OrdinalIgnoreCase) && d.IsActivated);
            if (domain == null)
            {
                return null;
            }

            var connection = _connectionRepository.Get(domain.ConnectionId);
            if (connection == null)
            {
                return null;
            }

            return connection.ConnectionRaw;
        }

        /// <summary>
        /// Lấy ra connection cho từng user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="domainName"></param>
        /// <returns>Connection stringify</returns>
        public string GetConnectionByUser(string userName, string domainName = "")
        {
            //var account1 = _accountRepository.Get(true, a => a.Username.Equals("thyntn", StringComparison.OrdinalIgnoreCase));
            //var account2 = _accountRepository.Get(true, a => a.UsernameEmailDomain.Equals("thyntn@motcua.sotttt.baria-vungtau.gov.vn", StringComparison.OrdinalIgnoreCase));
            
            if (string.IsNullOrEmpty(userName))
            {
                if (!string.IsNullOrEmpty(domainName))
                {
                    return GetConnection(domainName);
                }
            }

            var account = _accountRepository.Get(true, a => a.UsernameEmailDomain.Equals(userName, StringComparison.OrdinalIgnoreCase));
            if (account == null)
            {
                if (!string.IsNullOrEmpty(domainName))
                {
                    return GetConnection(domainName);
                }

                return GetDefaultConnection();
            }

            var accountDomains = account.AccountDomains;
            if (!accountDomains.Any())
            {
                return GetDefaultConnection();
            }

            var domainIds = accountDomains.Select(d => d.DomainId);
            var domains = _domainRepository.GetsReadOnly(d => domainIds.Contains(d.DomainId));
            Domain domain;
            if (domains.Count() > 1)
            {
                domain = string.IsNullOrEmpty(domainName)
                            ? domains.SingleOrDefault(d => d.IsActivated && d.IsPrimary)
                            : domains.SingleOrDefault(d => d.DomainName.Equals(domainName, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                domain = domains.First();
            }

            if (domain == null)
            {
                return GetDefaultConnection();
            }

            var connection = _connectionRepository.Get(domain.ConnectionId);
            return (new Connection
            {
                ConnectionId = connection.ConnectionId,
                Database = connection.Database,
                ConnectionRaw = connection.ConnectionRaw,
                DatabaseTypeIdInEnum = connection.DatabaseTypeIdInEnum,
                DatabaseTypeId = connection.DatabaseTypeId,
                Password = connection.Password,
                Port = connection.Port,
                ServerName = connection.ServerName,
                Username = connection.Username
            }).Stringify();
        }

        private Domain GetDefault()
        {
            var domainDefault = _domainRepository.Get(true, d => d.IsPrimary && d.IsActivated);
            if (domainDefault == null)
            {
                return null;
            }

            try
            {
                domainDefault.Connection = _connectionRepository.Get(domainDefault.ConnectionId);
            }
            catch
            {

            }

            return domainDefault;
        }

        private string GetDefaultConnection()
        {
            var domain = GetDefault();
            if (domain == null) return "";
            var connection = domain.Connection;
            return (new Connection
            {
                ConnectionId = connection.ConnectionId,
                Database = connection.Database,
                ConnectionRaw = connection.ConnectionRaw,
                DatabaseTypeIdInEnum = connection.DatabaseTypeIdInEnum,
                DatabaseTypeId = connection.DatabaseTypeId,
                Password = connection.Password,
                Port = connection.Port,
                ServerName = connection.ServerName,
                Username = connection.Username
            }).Stringify();
        }

        /// <summary>
        /// Trả về domain tương ứng với user hiện tại
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Domain GetDomain(string userName)
        {
            return DomainConfigHelper.GetDomain(userName);
        }

        /// <summary>
        /// Thêm user vào domain
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="userName"></param>
        public void AddUser(string domainName, string userName)
        {
            DomainConfigHelper.AddUser(domainName, userName);
        }

        /// <summary>
        /// Thêm user vào domain
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="userName"></param>
        public void AddUser(string domainName, IEnumerable<string> userName)
        {
            DomainConfigHelper.AddUser(domainName, userName);
        }
    }
}
