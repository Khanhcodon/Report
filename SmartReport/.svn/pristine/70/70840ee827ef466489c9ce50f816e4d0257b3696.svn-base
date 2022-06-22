using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Admin;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Constant;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [ComVisible(false)]
    //[RequireHttps]
    public class DomainController : CustomController
    {
        private readonly DomainBll _domainService;
        private readonly ServerBll _serverService;
        private readonly ResourceBll _resourceService;
        private readonly UserBll _userService;
        private readonly DomainAliasBll _domainAliasService;
        private readonly MemoryCacheManager _cache;

        public DomainController(DomainBll domainService, ServerBll serverService,
                            ResourceBll resourceService, DomainAliasBll domainAliasService, UserBll userService,
                            MemoryCacheManager cache)
        {
            _domainService = domainService;
            _serverService = serverService;
            _resourceService = resourceService;
            _domainAliasService = domainAliasService;
            _userService = userService;
            _cache = cache;
        }

        public ActionResult Index()
        {
            if (!HasPermission("DomainIndex"))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return RedirectToAction("Index", "Welcome");
            }

            var userName = _userService.CurrentUser.UsernameEmailDomain;
            var model = _domainService.GetsByUser(userName).ToListModel();
            return View(model);
        }

        public ActionResult Create()
        {
            if (!HasPermission("DomainIndex"))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return RedirectToAction("Index", "Welcome");
            }

            ViewBag.DropDownListServer = GetDropDownListServer(0);
            ViewBag.FreeDomainJson = GetFreeDomainJson();
            return View(new DomainModel
                            {
                                CustomerType = Convert.ToBoolean((int)CustomerType.Organization),
                                Connection = new ConnectionModel()
                            });
        }

        [HttpPost]
        public ActionResult Create(DomainModel domainModel)
        {
            if (!HasPermission("DomainIndex"))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var domain = domainModel.ToEntity();
                try
                {
                    //domain.CustomerName = domain.DomainName;
                    domain.Email = "";
                    domain.Connection.DatabaseTypeId = (int)Bkav.eGovCloud.Entities.DatabaseType.MySql;
                    domain.Connection.ConnectionRaw = _domainService.GenerateConnectionString(domain.Connection);
                    domain.Connection.ConnectionName = domain.DomainName;
                    var now = DateTime.Now;

                    Account account = null;
                    var users = new List<User>();

                    if (!domainModel.AccountUsername.IsEmpty())
                    {
                        var salt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
                        var hash = Generate.GetInputPasswordHash(domainModel.AccountPassword, salt);

                        account = new Account
                        {
                            Username = domainModel.AccountUsername,
                            UsernameEmailDomain = domainModel.AccountUsername + "@" + domainModel.DomainName,
                            DomainName = domainModel.DomainName,
                            PasswordSalt = salt,
                            PasswordHash = hash,
                            PasswordLastModifiedOnDate = now,
                            FullName = SystemRole.Administrator.RoleKey,
                            Gender = true,
                            IsActivated = true,
                            CreatedOnDate = now,
                            VersionDateTime = now
                        };

                        // Add user quản trị mặc định vào db khách hàng
                        users.Add(new User
                        {
                            Username = domainModel.AccountUsername,
                            UsernameEmailDomain = domainModel.AccountUsername + "@" + domainModel.DomainName,
                            DomainName = domainModel.DomainName,
                            PasswordSalt = salt,
                            PasswordHash = hash,
                            PasswordLastModifiedOnDate = now,
                            FullName = SystemRole.Administrator.RoleKey,
                            FirstName = SystemRole.Administrator.RoleKey,
                            LastName = SystemRole.Administrator.RoleKey,
                            Gender = true,
                            IsActivated = true,
                            IsLockedOut = false,
                            CreatedOnDate = now,
                            VersionDateTime = now
                        });
                    }

                    var currentUser = _userService.CurrentEditableUser;
                    // Add user hiện tại vào làm quản lý cho db vừa tạo
                    users.Add(new User
                    {
                        Username = currentUser.Username, // domainModel.AccountUsername,
                        UsernameEmailDomain = currentUser.UsernameEmailDomain, // domainModel.AccountUsername + "@" + domainModel.DomainName,
                        DomainName = currentUser.DomainName, // domainModel.DomainName,
                        PasswordSalt = currentUser.PasswordSalt,
                        PasswordHash = currentUser.PasswordHash,
                        PasswordLastModifiedOnDate = now,
                        FullName = SystemRole.Administrator.RoleKey,
                        FirstName = SystemRole.Administrator.RoleKey,
                        LastName = SystemRole.Administrator.RoleKey,
                        Gender = true,
                        IsActivated = true,
                        IsLockedOut = false,
                        CreatedOnDate = now,
                        VersionDateTime = now
                    });

                    domain.DomainAliass.Add(new DomainAlias
                    {
                        Alias = domainModel.DomainName,
                        IsActivated = true,
                        IsPrimary = true,
                        CreatedOnDate = now,
                        VersionDateTime = now
                    });
                    domain.CreatedOnDate = now;
                    domain.VersionDateTime = now;
                    domain.IsActivated = true;

                    _domainService.Create(domain, account, users, currentUser);

                    SuccessNotification(_resourceService.GetResource("Domain.Created"));
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(domainModel);
                }

                return RedirectToAction("Index");
            }

            return View(domainModel);
        }

        public ActionResult Edit(int id)
        {
            if (!HasPermission("DomainIndex"))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return RedirectToAction("Index", "Welcome");
            }

            var domain = _domainService.Get(id);
            if (domain == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.DropDownListServer = new List<SelectListItem>(); // GetDropDownListServer(domain.ServerId);
            ViewBag.FreeDomainJson = GetFreeDomainJson(id);
            ViewBag.DomainChildrenJson = GetDomainChildrenJson(domain);
            var model = domain.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DomainModel domainModel)
        {
            if (!HasPermission("DomainIndex"))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return RedirectToAction("Index", "Welcome");
            }

            if (ModelState.IsValid)
            {
                var domain = _domainService.Get(domainModel.DomainId);
                if (domain == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                    ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                    return RedirectToAction("Index");
                }
                try
                {
                    var oldDomainName = domain.DomainName;
                    var oldConnection = domain.Connection;
                    domain = domainModel.ToEntity(domain);
                    // domain.CustomerName = domain.DomainName;
                    domain.Email = "";
                    domain.Connection.DatabaseTypeId = (int)Bkav.eGovCloud.Entities.DatabaseType.MySql;
                    domain.Connection.ConnectionRaw = _domainService.GenerateConnectionString(domain.Connection);
                    domain.Connection.ConnectionName = domain.DomainName;
                    domain.Connection.DatabaseTypeId = (int)Bkav.eGovCloud.Entities.DatabaseType.MySql;

                    //Nếu mật khẩu để trống thì gán lại bằng mật khẩu cũ
                    if (string.IsNullOrWhiteSpace(domain.Connection.Password))
                    {
                        domain.Connection.Password = oldConnection.Password;
                    }

                    //Nếu có bất kỳ sự thay đỏi về cấu hình connection thì sẽ kiểm tra lại kết nối
                    if (oldConnection.ServerName != domain.Connection.ServerName || oldConnection.Database != domain.Connection.Database
                        || oldConnection.Username != domain.Connection.Username || oldConnection.Password != domain.Connection.Password
                        || oldConnection.DatabaseTypeIdInEnum != domain.Connection.DatabaseTypeIdInEnum || oldConnection.Port != domain.Connection.Port)
                    {
                        domain.Connection.ConnectionRaw = _domainService.GenerateConnectionString(domain.Connection);
                        if (_domainService.TestConnection(domain.Connection) == null)
                        {
                            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Domain.CreateOrEdit.Connection.ConnectionError"));
                            ErrorNotification(_resourceService.GetResource("Domain.CreateOrEdit.Connection.ConnectionError"));
                            return View(domainModel);
                        }
                    }
                    //Nếu có sự thay đổi về tên domain thì sẽ kiểu tra xem tên domain mới có trùng không
                    if (oldDomainName.ToLower() != domain.DomainName.ToLower())
                    {
                        if (_domainAliasService.Exist(domain.DomainName))
                        {
                            CreateActivityLog(ActivityLogType.Admin, string.Format(_resourceService.GetResource("Domain.CreateOrEdit.Fields.DomainName.Exist"), domain.DomainName));
                            ErrorNotification(string.Format(_resourceService.GetResource("Domain.CreateOrEdit.Fields.DomainName.Exist"), domain.DomainName));
                            return View(domainModel);
                        }
                        var aliasPrimary =
                            domain.DomainAliass.Single(a => a.Alias.ToLower() == oldDomainName && a.IsPrimary);
                        aliasPrimary.Alias = domain.DomainName;
                        aliasPrimary.LastModifiedOnDate = DateTime.Now;
                    }
                    domain.LastModifiedOnDate = DateTime.Now;

                    _domainService.Update(domain, oldDomainName, domainModel.DomainIds);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Domain.Updated"));
                    SuccessNotification(_resourceService.GetResource("Domain.Updated"));
                }
                catch (Exception ex)
                {
                    ErrorNotification(ex.Message);
                    return View(domainModel);
                }

                return RedirectToAction("Index");
            }
            return View(domainModel);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!HasPermission("DomainIndex"))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return RedirectToAction("Index");
            }

            var domain = _domainService.Get(id);
            if (domain == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return RedirectToAction("Index");
            }

            try
            {
                _domainService.Delete(domain);
            }
            catch
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Common.Error"));
                ErrorNotification(_resourceService.GetResource("Common.Error"));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ClearCache()
        {
            if (!HasPermission("DomainIndex"))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return Json(new { error = true });
            }
            _cache.Clear();
            return Json(new { success = true });
        }

        public ActionResult Search(string domainName, string province)
        {
            return PartialView("PartialList", _domainService.Gets(domainName, province).ToListModel());
        }

        public ActionResult ChangeDomain(int domainId)
        {
            if (!HasPermission("DomainIndex"))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                ErrorNotification(_resourceService.GetResource("Customer.Setting.NotPermissionGeneral"));
                return RedirectToAction("Index", "Welcome");
            }

            var domain = _domainService.Get(domainId);
            if (domain == null)
            {
                return RedirectToAction("Index");
            }

            // Clear Cache
            _cache.Clear();

            // Đặt lại cache connection cho user hiện tại
            var userName = System.Web.HttpContext.Current.User.GetUserName();
            var connection = domain.Connection;
            if (connection == null)
            {

            }

            _cache.Set(string.Format(CacheParam.DomainConnectionKey, userName), connection, CacheParam.DomainConnectionCacheTimeOut);

            // Đặt lại domain hiện tại user đang sử dụng
            _cache.Set(string.Format(CacheParam.DomainNameKey, userName), domain.DomainName, CacheParam.DomainNameCacheTimeOut);
            System.Web.HttpContext.Current.Session[SessionName.DomainName] = domain.DomainName;

            return RedirectToAction("General", "Setting");
        }

        #region "Private method"

        private IEnumerable<SelectListItem> GetDropDownListServer(int serverIdSelected)
        {
            var listServer = _serverService.Gets();
            var dropDownListServer = listServer.Select(s => new SelectListItem
            {
                Selected = serverIdSelected == s.ServerId,
                Text = s.PublicDomain,
                Value = s.ServerId.ToString(CultureInfo.InvariantCulture)
            });
            return dropDownListServer;
        }

        private string GetFreeDomainJson(int? ignoreId = null)
        {
            return _domainService.GetsDomainChildenAvailable(ignoreId);
        }

        private static string GetDomainChildrenJson(Domain domain)
        {
            return domain.DomainChildren.Select(d => new
            {
                value = d.DomainId,
                domain = d.DomainName,
                customerName = d.CustomerName
            }).StringifyJs();
        }

        #endregion "Private method"
    }
}