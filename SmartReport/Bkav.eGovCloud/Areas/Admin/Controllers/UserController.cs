using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Admin;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Mailers;
using Bkav.eGovCloud.SingleSignOnService;
using Bkav.eGovCloud.Web.Framework;
using ClosedXML.Excel;
using Mvc.Mailer;
using Bkav.eGovCloud.Helper;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class UserController : CustomController
    {
        private readonly UserBll _userService;
        private readonly LdapProvider _ldap;
        private readonly JobTitlesBll _jobTitlesService;
        private readonly PermissionBll _permissionService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly LogBll _logService;
        private readonly ResourceBll _resourceService;
        private readonly RoleBll _roleService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly SsoSettings _connectionSettings;
        private readonly PasswordPolicySettings _passwordPolicySettings;
        private readonly IUserMailer _userMailer;
        private readonly WorkflowBll _workflowService;
        private readonly DocTypeBll _docTypeService;
        private readonly AuthorizeBll _authorizeService;
        private readonly MobileDeviceBll _mobileDeviceService;

        private readonly MemoryCacheManager _cache;
        private const string DEFAULT_SORT_BY = "Username";

        public UserController(
            UserBll userService,
            LdapProvider ldap,
            JobTitlesBll jobTitlesService,
            PasswordPolicySettings passwordPolicySettings,
            PermissionBll permissionService,
            DepartmentBll departmentService,
            PositionBll positionService,
            LogBll logService,
            ResourceBll resourceService, RoleBll roleService,
            IUserMailer userMailer,
            AdminGeneralSettings generalSettings,
            AuthenticationSettings authenticationSettings,
            WorkflowBll workflowService,
            AuthorizeBll authorizeService,
            DocTypeBll docTypeService, MobileDeviceBll mobileDeviceService,
            MemoryCacheManager cache)
            : base()
        {
            _userService = userService;
            _ldap = ldap;
            _departmentService = departmentService;
            _permissionService = permissionService;
            _logService = logService;
            _resourceService = resourceService;
            _roleService = roleService;
            _generalSettings = generalSettings;
            _authenticationSettings = authenticationSettings;
            _jobTitlesService = jobTitlesService;
            _userMailer = userMailer;
            _positionService = positionService;
            _passwordPolicySettings = passwordPolicySettings;
            _workflowService = workflowService;
            _authorizeService = authorizeService;
            _docTypeService = docTypeService;
            _cache = cache;
            _connectionSettings = SsoSettings.Instance;
            _mobileDeviceService = mobileDeviceService;
        }

        #region Cán bộ

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            var isAdminDepartmentUser = false;
            if (!HasPermission())
            {
                if (!_departmentService.IsAdminDepartment(User.GetUserId()))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.User.NotPermission"));
                    ErrorNotification(_resourceService.GetResource("Customer.User.NotPermission"));
                    return RedirectToAction("Index", "Welcome");
                }
                isAdminDepartmentUser = true;
            }

            var search = new UserSearchModel();
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };

            var httpCookie = Request.Cookies[CookieName.SearchUser];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<UserSearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            int totalRecords;
            var model = isAdminDepartmentUser
                        ? _userService.GetsUserAccess(out totalRecords, u => new UserModel
                        {
                            UserId = u.UserId,
                            Username = u.Username,
                            FullName = u.FullName,
                            IsActivated = u.IsActivated
                        }, pageSize: sortAndPage.PageSize,
                                                sortBy: sortAndPage.SortBy, isDescending: sortAndPage.IsSortDescending,
                                                username: search.Username, fullname: search.FullName,
                                                isActivated: search.IsActivated, currentPage: sortAndPage.CurrentPage, userId: User.GetUserId(),
                                                 positionId: search.PositionId, roleId: search.RoleId)
                        : _userService.GetsAs(out totalRecords, u => new UserModel
                        {
                            UserId = u.UserId,
                            Username = u.Username,
                            FullName = u.FullName,
                            IsActivated = u.IsActivated
                        }, pageSize: sortAndPage.PageSize,
                                                sortBy: sortAndPage.SortBy, isDescending: sortAndPage.IsSortDescending,
                                                username: search.Username, fullname: search.FullName,
                                                isActivated: search.IsActivated, currentPage: sortAndPage.CurrentPage,
                                                 positionId: search.PositionId, roleId: search.RoleId);
            sortAndPage.TotalRecordCount = totalRecords;
            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.AllPositions = _positionService.GetCacheAllPosition().Select(p => new SelectListItem
            {
                Value = p.PositionId.ToString(),
                Text = p.PositionName
            });
            ViewBag.AllRoles = _roleService.Gets(true).Select(p => new SelectListItem
            {
                Value = p.RoleId.ToString(),
                Text = string.Format("{0} - {1}", p.RoleKey, p.RoleName)
            });

            return View(model);
        }

        public ActionResult Search(UserSearchModel search, int pageSize)
        {
            IEnumerable<UserModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    var isAdminDepartmentUser = false;
                    if (!HasPermission(SystemPermission.UserIndex.PermissionKey))
                    {
                        if (!_departmentService.IsAdminDepartment(User.GetUserId()))
                        {
                            ViewBag.SortAndPage = new SortAndPagingModel
                            {
                                PageSize = pageSize,
                                CurrentPage = 1,
                                IsSortDescending = false,
                                SortBy = DEFAULT_SORT_BY,
                                TotalRecordCount = 0
                            };
                            return PartialView("_PartialList", null);
                        }

                        isAdminDepartmentUser = true;
                    }

                    int totalRecords;
                    model = isAdminDepartmentUser
                            ? _userService.GetsUserAccess(out totalRecords, u => new UserModel
                            {
                                UserId = u.UserId,
                                Username = u.Username,
                                FullName = u.FullName,
                                IsActivated = u.IsActivated
                            }, pageSize: pageSize,
                                                        sortBy: DEFAULT_SORT_BY, isDescending: false,
                                                        username: search.Username, fullname: search.FullName,
                                                        isActivated: search.IsActivated, userId: User.GetUserId(),
                                                        positionId: search.PositionId, roleId: search.RoleId)
                            : _userService.GetsAs(out totalRecords, u => new UserModel
                            {
                                UserId = u.UserId,
                                Username = u.Username,
                                FullName = u.FullName,
                                IsActivated = u.IsActivated
                            }, pageSize: pageSize,
                                                        sortBy: DEFAULT_SORT_BY, isDescending: false,
                                                        username: search.Username, fullname: search.FullName,
                                                        isActivated: search.IsActivated,
                                                         positionId: search.PositionId, roleId: search.RoleId);
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = false,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };

                    ViewBag.SortAndPage = sortAndPage;

                    CreateCookieSearch(search, sortAndPage);
                }
                else
                {
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = false,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = 0
                    };
                    ViewBag.SortAndPage = sortAndPage;
                }
            }

            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(
            UserSearchModel search,
            string sortBy, bool isSortDesc,
            int page, int pageSize)
        {
            IEnumerable<UserModel> model = null;
            if (Request.IsAjaxRequest())
            {
                var isAdminDepartmentUser = false;

                //TrinhNVd: Sắp xếp không cần kiểm tra quyền
                //if (!HasPermission())
                //{
                //    if (!_departmentService.IsAdminDepartment(User.GetUserId()))
                //    {
                //        return PartialView("_PartialList", null);
                //    }
                //    isAdminDepartmentUser = true;
                //}
                int totalRecords;
                model = isAdminDepartmentUser
                        ? _userService.GetsUserAccess(out totalRecords, u => new UserModel
                        {
                            UserId = u.UserId,
                            Username = u.Username,
                            FullName = u.FullName,
                            IsActivated = u.IsActivated
                        }, pageSize: pageSize,
                                                sortBy: DEFAULT_SORT_BY, isDescending: isSortDesc,
                                                username: search.Username, fullname: search.FullName,
                                                isActivated: search.IsActivated, currentPage: page, userId: User.GetUserId(),
                                                 positionId: search.PositionId, roleId: search.RoleId)
                        : _userService.GetsAs(out totalRecords, u => new UserModel
                        {
                            UserId = u.UserId,
                            Username = u.Username,
                            FullName = u.FullName,
                            IsActivated = u.IsActivated
                        }, pageSize: pageSize,
                                                sortBy: DEFAULT_SORT_BY, isDescending: isSortDesc,
                                                username: search.Username, fullname: search.FullName,
                                                isActivated: search.IsActivated, currentPage: page,
                                                 positionId: search.PositionId, roleId: search.RoleId);
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };

                CreateCookieSearch(search, sortAndPage);

                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Search = search;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
            }

            return PartialView("_PartialList", model);
        }

        [HttpPost]
        public JsonResult GetRolePermissions(int[] roleIds)
        {
            if (roleIds == null)
            {
                return Json(new List<string>());
            }
            var rolePermissions = _roleService.GetRolePermissions(roleIds, rp => new { rp.PermissionId, rp.AllowAccess }).Distinct();
            return Json(rolePermissions);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            var isAdminDepartmentUser = false;
            if (!HasPermission())
            {
                if (!_departmentService.IsAdminDepartment(User.GetUserId()))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.User.NotPermissionCreate"));
                    ErrorNotification(_resourceService.GetResource("Customer.User.NotPermissionCreate"));
                    return RedirectToAction("Index");
                }
                isAdminDepartmentUser = true;
            }

            var allRoles = _roleService.Gets(true).ToListModel();
            ViewBag.AllRoles = allRoles;

            ViewBag.PermissionsInSystem = _permissionService.GetAllPermissonsInSystem().ToListModel();
            ViewBag.AllJobTitles = _jobTitlesService.Gets().ToListModel();
            ViewBag.AllPositions = _positionService.Gets().ToListModel();
            ViewBag.AllDepartments = GetAllDepartments(isAdminDepartmentUser);
            ViewBag.PasswordMessageMatches = UserValidator.GetMessageMatches(_resourceService, _passwordPolicySettings);
            ViewBag.PasswordExpression = _passwordPolicySettings.PasswordExpression;
            ViewBag.ListDomain = GetListDomain();

            //var roleIds = _roleService.GetAllRoleIdAutoAssign();
            var roleIds = allRoles.Where(p => p.IsAutoAssignment).Select(p => p.RoleId);
            if (roleIds != null && roleIds.Any())
            {
                ViewBag.RolePermissions = _roleService.GetRolePermissions(roleIds,
                    rp => new { rp.PermissionId, rp.AllowAccess })
                                         .Distinct().StringifyJs();
            }

            ViewBag.SelectedRoles = roleIds.StringifyJs();

            var model = new UserModel();
            model.Password = _passwordPolicySettings.DefaultCreatePassword;
            return View(model);
        }

        [HttpPost]
        public JsonResult IsExistUsernameEmailDomain(string usernameEmailDomain)
        {
            if (_userService.IsExistUsernameEmailDomain(usernameEmailDomain))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.User.Create.Exist"));
                return Json(new { error = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.User.Create.Exist") });
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.User.Create.Error"));
            return Json(new { success = _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.User.Create.Error") });
        }

        [HttpPost]
        public ActionResult Create(UserModel model)
        {
            var isAdminDepartmentUser = false;
            if (!HasPermission())
            {
                if (!_departmentService.IsAdminDepartment(User.GetUserId()))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.User.NotPermissionCreate"));
                    ErrorNotification(_resourceService.GetResource("Customer.User.NotPermissionCreate"));
                    return RedirectToAction("Index");
                }
                isAdminDepartmentUser = true;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var transactionUser = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        model.Username = model.Username.StripVietnameseChars().Replace(" ", "").ToLower();
                        if (model.Username.Contains("@"))
                        {
                            // Trường hợp cho nhập domain tự do
                            var userNameArr = model.Username.Split('@');
                            model.Username = userNameArr[0];
                            model.DomainName = userNameArr[1];
                        }

                        if (string.IsNullOrEmpty(model.DomainName))
                        {
                            throw new Exception("Domain không được để trống!");
                        }

                        model.UsernameEmailDomain = string.Format(@"{0}@{1}", model.Username, model.DomainName);
                        model.FullName = string.Format("{0} {1}", model.LastName.Trim(), model.FirstName.Trim());

                        var entity = model.ToEntity();
                        var salt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
                        var hash = Generate.GetInputPasswordHash(model.Password, salt);
                        var now = DateTime.Now;
                        entity.PasswordHash = hash;
                        entity.PasswordSalt = salt;
                        entity.CreatedByUserId = User.GetUserId();
                        entity.CreatedOnDate = now;
                        entity.VersionDateTime = now;
                        entity.PasswordLastModifiedOnDate = now;
                        entity.IsActivated = true;

                        _userService.Create(entity);

                        SuccessNotification(_resourceService.GetResource("User.Created"));
                        transactionUser.Complete();
                    }
#if QuanTriTapTrungEdition

                    // CreateUserToSso(model);
                    // SyncUserSso();
#endif
                    return RedirectToAction("Create");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            ViewBag.ListDomain = GetListDomain();
            ReBindDataWhenError(model, isAdminDepartmentUser);
            ViewBag.PasswordMessageMatches = UserValidator.GetMessageMatches(_resourceService, _passwordPolicySettings);
            ViewBag.PasswordExpression = _passwordPolicySettings.PasswordExpression;
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            var isAdminDepartmentUser = false;
            if (!HasPermission())
            {
                if (!_departmentService.IsAdminDepartment(User.GetUserId()))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.User.NotPermissionUpdate"));
                    ErrorNotification(_resourceService.GetResource("Customer.User.NotPermissionUpdate"));
                    return RedirectToAction("Index");
                }
                isAdminDepartmentUser = true;
            }

            var user = _userService.Get(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            var roleIds = user.UserRoles.Select(ur => ur.RoleId);
            if (roleIds.Any())
            {
                ViewBag.RolePermissions = _roleService.GetRolePermissions(roleIds, rp => new { rp.PermissionId, rp.AllowAccess })
                                         .Distinct().StringifyJs();
            }

            ViewBag.AllRoles = _roleService.Gets(true).ToListModel();
            ViewBag.SelectedRoles = roleIds.StringifyJs();
            ViewBag.PermissionsInSystem = _permissionService.GetAllPermissonsInSystem().ToListModel();
            ViewBag.PermissionsSelected = user.UserRolePermissions
                                            .Select(urp => new
                                            {
                                                urp.PermissionId,
                                                urp.AllowAccess
                                            }).StringifyJs();

            ViewBag.AllJobTitles = _jobTitlesService.Gets().ToListModel();
            ViewBag.AllPositions = _positionService.Gets().ToListModel();
            ViewBag.AllDepartments = GetAllDepartments(isAdminDepartmentUser);
            ViewBag.DepartmentJobTitlesIdsSelected = user.UserDepartmentJobTitless
                                                    .Select(udp => new { udp.DepartmentId, udp.JobTitlesId, udp.PositionId, udp.IsPrimary, udp.IsAdmin })
                                                    .StringifyJs();
            ViewBag.DefaultResetPassword = _passwordPolicySettings.DefaultResetPassword;

            var devices = _mobileDeviceService.Gets(id).OrderByDescending(m => m.LastUpdate);
            ViewBag.Devices = devices.ToList();

            user.HasLimitByMac = user.HasLimitByMac ?? _authenticationSettings.LimitByMAC;

            return View(user.ToModel());
        }

        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            var isAdminDepartmentUser = false;
            if (!HasPermission())
            {
                if (!_departmentService.IsAdminDepartment(User.GetUserId()))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.User.NotPermissionUpdate"));
                    ErrorNotification(_resourceService.GetResource("Customer.User.NotPermissionUpdate"));
                    return RedirectToAction("Index");
                }
                isAdminDepartmentUser = true;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userService.Get(model.UserId);
                    if (user == null)
                    {
                        return RedirectToAction("Index");
                    }
                    var oldOpenId = user.OpenId;
                    model.Username = user.Username;
                    model.UsernameEmailDomain = user.UsernameEmailDomain;
                    model.DomainName = user.DomainName;
                    user = model.ToEntity(user);
                    user.LastModifiedByUserId = User.GetUserId();
                    user.LastModifiedOnDate = DateTime.Now;
                    user.FullName = string.Format("{0} {1}", user.LastName.Trim(), user.FirstName.Trim());

                    using (var transactionUser = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        _userService.Update(user, oldOpenId);
#if QuanTriTapTrungEdition
                        var client = GetClientService();
                        client.UpdateUser(user.Username, user.FullName, user.Gender,
                                          user.Phone, user.Fax, user.Address, user.OpenId, user.IsActivated, user.DomainName);
                        client.Close();
#endif
                        transactionUser.Complete();
                    }
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    ReBindDataWhenError(model, isAdminDepartmentUser);
                    return View(model);
                }

                SuccessNotification(_resourceService.GetResource("User.Updated"));
                return RedirectToAction("Index");
            }

            ReBindDataWhenError(model, isAdminDepartmentUser);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _userService.Delete(id);
                CreateActivityLog(ActivityLogType.Admin, "Xóa người dùng thành công.");
                SuccessNotification("Xóa người dùng thành công.");
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, "Xóa người dùng thất bại. Vui lòng thử lại sau.");
                ErrorNotification("Xóa người dùng thất bại. Vui lòng thử lại sau.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ResetPassword(int id, string defaultPassword)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.User.NotPermissionResetPassword"));
                return Json(new { error = _resourceService.GetResource("Customer.User.NotPermissionResetPassword") });
            }

            var currentUser = User.GetUserNameWithDomain();
            var user = _userService.Get(id);

            // kiểm tra quyền reset tài khoản admin
            if (user.Username.Equals("admin", StringComparison.OrdinalIgnoreCase) || !user.DomainName.Equals(_authenticationSettings.DefaultDomain))
            {
                if (!currentUser.Equals(user.UsernameEmailDomain, StringComparison.OrdinalIgnoreCase))
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.User.NotPermissionResetPassword"));
                    return Json(new { error = _resourceService.GetResource("Customer.User.NotPermissionResetPassword") });
                }
            }

            try
            {
                var newPassword = string.IsNullOrWhiteSpace(defaultPassword) ? Generate.GenerateRandomString(8) : defaultPassword;
                var resetSuccess = false;

#if QuanTriTapTrungEdition
                using (var transactionUser = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    if (_userService.ResetPassword(id, newPassword))
                    {
                        using (var transactionService = new TransactionScope(TransactionScopeOption.Required))
                        {
                            var client = GetClientService();

                            var status = client.ResetPassword(user.UsernameEmailDomain, newPassword,
                                                               _passwordPolicySettings.EnableHistory,
                                                               _passwordPolicySettings.HistoryCount);

                            if (!status.Success)
                            {
                                return Json(new { error = status.Message });
                            }
                            resetSuccess = true;
                            transactionService.Complete();
                        }
                    }

                    transactionUser.Complete();
                }
#else
                resetSuccess = _userService.ResetPassword(id, newPassword);
#endif
                if (resetSuccess)
                {
                    try
                    {
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("User.Password.Reseted"));
                        SuccessNotification(_resourceService.GetResource("User.Password.Reseted"));
                        //var mail = _userMailer.ResetPassword(user.FullName, user.UsernameEmailDomain, newPassword);
                        //mail.To.Add(user.UsernameEmailDomain);
                        //mail.Send(_userMailer.SmtpClient);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }
                }
            }
            catch (EgovException ex)
            {
                return Json(new { error = ex.Message });
            }

            return Json(new { success = true });
        }

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public ActionResult ImportUserFromLdap()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.User.NotPermissionImportUserFromLdap"));
                ErrorNotification(_resourceService.GetResource("Customer.User.NotPermissionImportUserFromLdap"));
                return RedirectToAction("Index");
            }

            if (_authenticationSettings.LdapEnableImport)
            {
                var usersImport = _ldap.GetAllUserImport(_authenticationSettings.LdapHost,
                                                         _authenticationSettings.LdapPort,
                                                         _authenticationSettings.LdapSSL,
                                                         _authenticationSettings.LdapBaseDn,
                                                         _authenticationSettings.LdapUsername,
                                                         _authenticationSettings.LdapPassword,
                                                         _authenticationSettings.LdapImportUsersFromLdapFilter,
                                                         _authenticationSettings.LdapMappingUsername,
                                                         _authenticationSettings.LdapMappingEmail,
                                                         _authenticationSettings.LdapMappingFirstName,
                                                         _authenticationSettings.LdapMappingLastName,
                                                         _authenticationSettings.LdapMappingFullName).OrderBy(u => u.Username);
                var numbers = 0;
                ViewBag.UsersImport = usersImport.Select(u =>
                                                             {
                                                                 numbers++;
                                                                 return new
                                                                 {
                                                                     numbers,
                                                                     label = u.Email + " - " + u.FullName,
                                                                     value = u.Username,
                                                                     fullname = u.FullName,
                                                                     email = u.Email
                                                                 };
                                                             }).StringifyJs();
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ImportUserFromLdap(string users)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.User.NotPermissionImportUserFromLdap"));
                ErrorNotification(_resourceService.GetResource("Customer.User.NotPermissionImportUserFromLdap"));
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(_passwordPolicySettings.DefaultCreatePassword))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.User.DefaultCreatePassword.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.User.DefaultCreatePassword.NotExist"));
                return RedirectToAction("Index");
            }

            var usersDict = Json2.ParseAs<List<Dictionary<string, string>>>(users);
            var usersImport = new List<User>();
            var domainName = string.IsNullOrEmpty(_connectionSettings.BkavSSOParentDomain) ? Request.GetDomainName() : _connectionSettings.BkavSSOParentDomain;
            var userid = User.GetUserId();
            foreach (var dict in usersDict)
            {
                if (string.IsNullOrWhiteSpace(dict["value"])
                    || string.IsNullOrWhiteSpace(dict["email"])
                    || string.IsNullOrWhiteSpace(dict["fullname"]))
                {
                    continue;
                }
                var salt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
                var hash = Generate.GetInputPasswordHash(_passwordPolicySettings.DefaultCreatePassword, salt);
                var now = DateTime.Now;
                var user = new User
                {
                    Username = dict["value"],
                    UsernameEmailDomain = dict["value"] + "@" + domainName,
                    DomainName = Request.GetDomainName(), // domainName
                    PasswordSalt = salt,
                    PasswordHash = hash,
                    PasswordLastModifiedOnDate = now,
                    FullName = dict["fullname"],
                    Gender = true,
                    IsActivated = true,
                    CreatedOnDate = now,
                    VersionDateTime = now,
                    CreatedByUserId = userid
                };
                usersImport.Add(user);
            }
            try
            {
                _userService.Create(usersImport);
                return Json(new { success = true, message = string.Empty });
            }
            catch (Exception ex)
            {
                LogException(ex);
                _logService.Error(ex.Message, ex);
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult ImportPhoneFromFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportPhoneFromFile(HttpPostedFileBase importFile, string sheetName)
        {
            if (importFile != null)
            {
                var xlsxParser = new XlsxToJson(importFile.InputStream);
                var json = xlsxParser.ConvertXlsxToJson(1, 2, 1, 1);
                var jsonConvert = Newtonsoft.Json.JsonConvert.SerializeObject(json);
                var listJson = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DepartmentImport>>(jsonConvert);

                var departmentLevel1 = listJson.Where(d => d.edocid.Contains("000.00.00"));
                var departmentLevel2 = listJson.Where(d => d.edocid.Contains("000.00") && !d.edocid.Contains("000.00.00"));
                var departmentLevel3 = listJson.Where(d => d.edocid.Contains("000") && !d.edocid.Contains("000.00"));
                var departmentLevel4 = listJson.Where(d => !d.edocid.Contains("000"));

                departmentLevel1 = departmentLevel1.Select(d=>  {
                    d.namepath = "\\" + d.name;
                    EnsureDepartment(d.namepath, d.edocid);
                    return d;
                });

                departmentLevel2 = departmentLevel2.Select(d => {
                    var eDocSub = d.edocid.Replace("000.00", "");
                    var parent = departmentLevel1.FirstOrDefault(dp => eDocSub.Contains(dp.edocid.Replace("000.00.00", "")));
                    if (parent != null)
                    {
                        d.namepath = parent.namepath + "\\" + d.name;
                        EnsureDepartment(d.namepath, d.edocid);
                    }
                    return d;
                });

                departmentLevel3 = departmentLevel3.Select(d => {
                    var eDocSub = d.edocid.Replace("000", "");
                    var parent = departmentLevel2.FirstOrDefault(dp => eDocSub.Contains(dp.edocid.Replace("000.00", "")));
                    if (parent != null)
                    {
                        d.namepath = parent.namepath + "\\" + d.name;
                        EnsureDepartment(d.namepath, d.edocid);
                    }
                    return d;
                });

                departmentLevel4 = departmentLevel4.Select(d => {
                    var eDocSub = d.edocid;
                    var parent = departmentLevel3.FirstOrDefault(dp => eDocSub.Contains(dp.edocid.Replace("000", "")));
                    if (parent != null)
                    {
                        d.namepath = parent.namepath + "\\" + d.name;
                        EnsureDepartment(d.namepath, d.edocid);
                    }
                    return d;
                });

                Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(departmentLevel1));
                Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(departmentLevel2));
                Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(departmentLevel3));
                Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(departmentLevel4));
            }

            return View();
        }

        public ActionResult ImportUserFromFile()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ImportUserFromFile(HttpPostedFileBase importFile, string sheetName, string defaultPass, string domain)
        {
            try
            {
                //Đưa lên trên thì tất cả user được import sẽ giống salt và hash ở database, nên random salt theo mỗi user để hash khác nhau
                //var salt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
                //var hash = Generate.GetInputPasswordHash(defaultPass, salt);
                var now = DateTime.Now;
                var domainName = domain; //na.gov.la

                var userNameColumn = "A";
                var accountColumn = "B";
                var genderColumn = "C";
                var positionColumn = "D";
                var jobtitleColumn = "E";
                var departmentColumn = "F";

                if (importFile != null)
                {
                    var xlsxParser = new XlsxToJson(importFile.InputStream);
                    var rows = xlsxParser.GetRows(sheetName, string.Empty);

                    foreach (IXLRow row in rows)
                    {
                        var rowIndex = row.RowNumber();
                        try
                        {
                            if (row.IsEmpty(true))
                            {
                                continue;
                            }

                            var userName = row.Cell(userNameColumn).Value.ToString();
                            var account = row.Cell(accountColumn).Value.ToString();

                            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(account))
                            {
                                continue;
                            }

                            var positionName = row.Cell(positionColumn).Value.ToString();
                            var positionId = EnsurePosition(positionName);

                            var jobTitleName = row.Cell(jobtitleColumn).Value.ToString();
                            var jobTitleId = EnsureJobtitle(jobTitleName);

                            var departmentName = row.Cell(departmentColumn).Value.ToString();
                            string departmentExt;
                            var departmentId = EnsureDepartmentDefault(departmentName, out departmentExt);

                            var gender = row.Cell(genderColumn).Value.ToString().Contains("Mr.") ||
                                            row.Cell(genderColumn).Value.ToString().Contains("Dr.");

                            string firstName, lastName;
                            userName = userName.Trim();

                            var salt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
                            var hash = Generate.GetInputPasswordHash(defaultPass, salt);

                            var lastSpaceIndex = userName.LastIndexOf(' ');
                            firstName = userName.Substring(0, lastSpaceIndex).Trim();
                            lastName = userName.Substring(lastSpaceIndex + 1).Trim();

                            account = account.Split('@').First();

                            var user = new User()
                            {
                                Username = account,
                                UsernameEmailDomain = account + "@" + domainName,
                                FullName = userName,
                                FirstName = firstName,
                                LastName = lastName,
                                PasswordHash = hash,
                                PasswordSalt = salt,
                                Gender = gender,
                                CreatedByUserId = User.GetUserId(),
                                CreatedOnDate = now,
                                VersionDateTime = now,
                                PasswordLastModifiedOnDate = now,
                                IsActivated = true,
                                DomainName = domainName
                            };

                            if (positionId != 0 && jobTitleId != 0 && departmentId != 0)
                            {
                                user.UserDepartmentJobTitless = new List<UserDepartmentJobTitlesPosition>();
                                user.UserDepartmentJobTitless.Add(new UserDepartmentJobTitlesPosition()
                                {
                                    DepartmentId = departmentId,
                                    DepartmentIdExt = departmentExt,
                                    IsAdmin = false,
                                    IsPrimary = true,
                                    JobTitlesId = jobTitleId,
                                    PositionId = positionId
                                });
                            }

                            _userService.Create(user);

#if QuanTriTapTrungEdition

                            var accountModel = user.ToModel();
                            accountModel.Password = defaultPass;
                            accountModel.FullName = userName;
                            accountModel.Gender = gender;
                            accountModel.Phone = "";
                            accountModel.Fax = "";
                            accountModel.Address = "";
                            accountModel.OpenId = "";
                            CreateUserToSso(accountModel);
#endif
                        }
                        catch (Exception ex)
                        {
                            LogException("Lỗi: RowNumber = " + rowIndex + ", Message = " + ex.Message);
                        }

                        // break; //test dòng đầu
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogException("Lỗi: Import, Message = " + ex.Message);
            }
            return View();
        }


        private int EnsureDepartmentDefault(string departmentName, out string departmentExt)
        {
            if (string.IsNullOrEmpty(departmentName))
            {
                departmentExt = "";
                return 0;
            }

            var ind = departmentName.LastIndexOf('\\');
            var parrentExt = "";
            if (ind > 0)
            {
                parrentExt = departmentName.Substring(0, ind);
                departmentName = departmentName.Substring(ind + 1);
            }

            var checkDepartments = _departmentService.GetCacheAllDepartments()
                                        .Where(d => d.DepartmentName.Equals(departmentName, StringComparison.OrdinalIgnoreCase));
            if (checkDepartments.Any())
            {
                departmentExt = checkDepartments.First().DepartmentIdExt;
                return checkDepartments.First().DepartmentId;
            }

            var allDepartment = _departmentService.GetCacheAllDepartments();
            var parrent = allDepartment.SingleOrDefault(d => d.DepartmentPath.Equals(parrentExt, StringComparison.OrdinalIgnoreCase));
            if (parrent == null)
            {
                parrent = allDepartment.SingleOrDefault(d => !d.ParentId.HasValue);
            }

            var newDept = new Department()
            {
                DepartmentName = departmentName,
                CreatedOnDate = DateTime.Now,
                IsActivated = true,
                ParentId = parrent.DepartmentId
            };

            _departmentService.Create(newDept);
            departmentExt = newDept.DepartmentIdExt;
            return newDept.DepartmentId;
        }

        private int EnsureDepartment(string departmentName, string edocId)
        {
            if (string.IsNullOrEmpty(departmentName))
            {
                return 0;
            }

            var ind = departmentName.LastIndexOf('\\');
            var parrentExt = "";
            if (ind > 0)
            {
                parrentExt = departmentName.Substring(0, ind);
                departmentName = departmentName.Substring(ind + 1);
            }

            var checkDepartments = _departmentService.GetCacheAllDepartments()
                                        .Where(d => d.DepartmentName.Equals(departmentName, StringComparison.OrdinalIgnoreCase));
            if (checkDepartments.Any())
            {
                return checkDepartments.First().DepartmentId;
            }

            var allDepartment = _departmentService.GetCacheAllDepartments();
            var parrent = allDepartment.SingleOrDefault(d => d.DepartmentPath.Equals(parrentExt, StringComparison.OrdinalIgnoreCase));
            if (parrent == null)
            {
                parrent = allDepartment.SingleOrDefault(d => !d.ParentId.HasValue);
            }

            var newDept = new Department()
            {
                DepartmentName = departmentName,
                CreatedOnDate = DateTime.Now,
                IsActivated = true,
                ParentId = parrent.DepartmentId,
                Emails = edocId
            };

            _departmentService.Create(newDept);
            return newDept.DepartmentId;
        }

        private int EnsureJobtitle(string jobTitleName)
        {
            jobTitleName = jobTitleName.Trim();
            if (string.IsNullOrEmpty(jobTitleName))
            {
                return 0;
            }

            var allJobtitles = _jobTitlesService.GetCacheAllJobtitles().Where(j => j.JobTitlesName.Equals(jobTitleName, StringComparison.OrdinalIgnoreCase));
            if (allJobtitles.Any())
            {
                return allJobtitles.First().JobTitlesId;
            }

            var newJobtitle = new JobTitles()
            {
                JobTitlesName = jobTitleName,
                PriorityLevel = 1,
                IsClerical = false,
                IsApproved = false,
                CanGetDocumentCome = false
            };

            _jobTitlesService.Create(newJobtitle);
            return newJobtitle.JobTitlesId;
        }

        private int EnsurePosition(string positionName)
        {
            positionName = positionName.Trim();
            if (string.IsNullOrEmpty(positionName))
            {
                return 0;
            }

            var positions = _positionService.GetCacheAllPosition().Where(p => p.PositionName.Equals(positionName, StringComparison.OrdinalIgnoreCase));
            if (positions.Any())
            {
                return positions.First().PositionId;
            }

            var newPosition = new Position()
            {
                PositionName = positionName,
                PriorityLevel = 1,
                IsApproved = false
            };

            _positionService.Create(newPosition);
            return newPosition.PositionId;
        }

        [HttpPost]
        public JsonResult AddUserDeptPosJob(List<int> userIds, List<int> deptIds, int posId, int jobId)
        {
            if (userIds == null || !userIds.Any())
            {
                return Json(new { result = false, message = "Danh sách người dùng không được trống" });
            }

            if (deptIds == null || !deptIds.Any())
            {
                return Json(new { result = false, message = "Danh phòng ban không được trống" });
            }

            if (posId <= 0)
            {
                return Json(new { result = false, message = "Bạn chưa chọn chức vụ" });
            }

            if (jobId <= 0)
            {
                return Json(new { result = false, message = "Bạn chưa chọn chức danh" });
            }

            var allUser = _userService.GetAllCached();
            var inUsers = allUser.Where(p => userIds.Contains(p.UserId));
            if (inUsers.Count() != userIds.Count)
            {
                return Json(new { result = false, message = "Có người dùng không tồn tại" });
            }

            var allDepts = _departmentService.GetCacheAllDepartments();
            var inDepts = allDepts.Where(p => deptIds.Contains(p.DepartmentId));
            if (inDepts.Count() != deptIds.Count)
            {
                return Json(new { result = false, message = "Có phòng ban không tồn tại" });
            }

            var inJob = _jobTitlesService.Get(jobId);
            if (inJob == null)
            {
                return Json(new { result = false, message = "Chức danh không tồn tại" });
            }

            var inPos = _positionService.Get(posId);
            if (inPos == null)
            {
                return Json(new { result = false, message = "Chức vụ không tồn tại" });
            }

            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (var deptId in deptIds)
                    {
                        var dept = _departmentService.Get(deptId);
                        if (dept == null)
                        {
                            continue;
                        }

                        _departmentService.Update(dept, inUsers, inPos, inJob);
                    }
                    trans.Complete();
                }
            }
            catch
            {
                return Json(new { result = false, message = "Có lỗi trong quá trình thêm" });
            }

            return Json(new { result = true, message = "Thêm mới thành công" });
        }

        public JsonResult GetAllDeptJobPos()
        {
            var alldepartments = _departmentService.GetCacheAllDepartments();
            var resultDept = alldepartments.OrderBy(t => t.Order).Select(u => new
            {
                value = u.DepartmentId,
                label = u.DepartmentName,
                parentid = u.ParentId.HasValue ? u.ParentId : 0,
                data = u.DepartmentName,
                metadata = new { id = u.DepartmentId },
                state = "leaf",
                extvalue = u.DepartmentIdExt,
                path = u.DepartmentPath,
                order = u.Order,
                level = u.Level,
                attr = new { id = u.DepartmentId, rel = u.IsActivated ? "dept" : "dept-deactivated" }
            });

            var allJobTitless = _jobTitlesService.GetCacheAllJobtitles();
            var resultJobs = allJobTitless.Select(u => new { value = u.JobTitlesId, label = u.JobTitlesName });

            var allPositions = _positionService.GetCacheAllPosition();
            var resultPos = allPositions.Select(u => new { value = u.PositionId, label = u.PositionName });

            return Json(new { depts = resultDept, jobs = resultJobs, pos = resultPos }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region thay thế người dùng

        public ActionResult ReplaceUser()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Position.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            BindData();
            return View(new ReplaceUserModel());
        }

        [HttpPost]
        public ActionResult ReplaceUser(ReplaceUserModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Position.NotPermission"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                var workflows = _workflowService.Gets(p => model.WorkflowIds.Contains(p.WorkflowId));
                if (workflows == null || !workflows.Any())
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.ReplaceUser.UpdateWorkflow.Workflows.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Admin.ReplaceUser.UpdateWorkflow.Workflows.NotExist"));
                    BindData();
                    return View(model);
                }

                try
                {
                    using (var trans = new TransactionScope(TransactionScopeOption.Required))
                    {
                        UyQuyen(model, workflows);

                        QuyTrinh(model, workflows);

                        ChucVuChucDanhPhongBan(model);

                        ActivatedUser(model);

                        trans.Complete();
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.ReplaceUser.UpdateWorkflow.Success"));
                    SuccessNotification(_resourceService.GetResource("Admin.ReplaceUser.UpdateWorkflow.Success"));
                    return RedirectToAction("ReplaceUser");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.ReplaceUser.UpdateWorkflow.Error"));
                    ErrorNotification(_resourceService.GetResource("Admin.ReplaceUser.UpdateWorkflow.Error"));
                }
            }

            BindData();
            return View(model);
        }

        private void UyQuyen(ReplaceUserModel model, IEnumerable<Workflow> workflows)
        {
            if (model.HasAuthorize)
            {
                //Lấy danh sách ủy quyền mà người thay thế ủy quyền cho người được thay thế và ngược lại
                var existAuthens = _authorizeService.Gets(
                    p => p.Active
                        && ((p.AuthorizedUserId == model.OldUserId
                                && p.AuthorizeUserId == model.NewUserId)
                            || (p.AuthorizedUserId == model.NewUserId
                                && p.AuthorizeUserId == model.OldUserId)));
                //Xóa bỏ các ủy quyền đã được thiết lập
                if (existAuthens != null && existAuthens.Any())
                {
                    _authorizeService.Delete(existAuthens);
                }

                //Nếu xóa hẳn người dùng trên toàn quy trình thì mặc định sẽ tạo ủy quyền toàn bộ của người thay thế cho người được thay thế
                PermissionTypes permissType = PermissionTypes.KTao | PermissionTypes.XLy;

                var workflowIds = workflows.Select(p => p.WorkflowId);

                //HopCV: Test phần quy trình theo code mới
                // var doctypeIds = _workflowService.GetDocTypes(workflowIds).Select(p => p.DocTypeId);
                var doctypeIds = _docTypeService.GetAllFromCache().Where(dt => workflowIds.Contains(dt.WorkflowId)).Select(dt => dt.DocTypeId);

                var authenObj = new Authorize
                {
                    Active = true,
                    AuthorizeUserId = model.OldUserId,
                    AuthorizedUserId = model.NewUserId,
                    Permission = (int)permissType,
                    DateBegin = DateTime.MinValue,
                    DateEnd = DateTime.MaxValue,
                    DocTypeId = doctypeIds.Stringify()
                };

                if (!model.IsDeletedUserWorkflow)
                {
                    authenObj.DateBegin = model.BeginDated.Value;
                    authenObj.DateEnd = model.EndDated.Value;
                }

                _authorizeService.Create(authenObj);
            }
        }

        private void QuyTrinh(ReplaceUserModel model, IEnumerable<Workflow> workflows)
        {
            if (model.IsDeletedUserWorkflow)
            {
                foreach (var workflow in workflows)
                {
                    var path = workflow.JsonInObject;
                    if (path == null || (path.Nodes == null || !path.Nodes.Any()))
                    {
                        continue;
                    }

                    #region Xử lý trên danh sách quan hệ

                    var nodes = path.Nodes;
                    foreach (var node in nodes)
                    {
                        var listAddress = node.Address;
                        if (listAddress == null || !listAddress.Any())
                            continue;

                        foreach (var address in listAddress)
                        {
                            // Do khi cấu hình trên quy trình chỉ có quan hệ theo người dùng là thêm trực tiếp userID  và0
                            // còn trên các quan hệ khác lại phải dựa vào người dùng hiện tại=> chỉ replace được trên quan hệ người dùng
                            if (address.UserQueries != null && address.UserQueries.Any())
                            {
                                foreach (var user in address.UserQueries)
                                {
                                    if (user.UserId == model.OldUserId)
                                    {
                                        user.UserId = model.NewUserId;
                                    }
                                }
                                address.UserQueries = address.UserQueries.Distinct().ToList();
                            }
                        }
                    }

                    #endregion

                    workflow.Json = path.Stringify();
                }

                _workflowService.Update(workflows);
            }
        }

        private void ChucVuChucDanhPhongBan(ReplaceUserModel model)
        {
            var oldUserDeptPos = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition()
                                                          .Where(p => p.UserId == model.OldUserId);

            var newUserDeptPos = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition()
                                            .Where(p => p.UserId == model.NewUserId);

            if (oldUserDeptPos != null && oldUserDeptPos.Any())
            {
                var notExist = new List<UserDepartmentJobTitlesPosition>();

                if (newUserDeptPos != null && newUserDeptPos.Any())
                {
                    //lấy ra những quan hệ chức vụ phòng ban mà người được thay thế không có theo người thay thế
                    foreach (var item in oldUserDeptPos)
                    {
                        var checkExist = newUserDeptPos.Any(
                            p =>
                                p.DepartmentId == item.DepartmentId
                                && p.DepartmentIdExt == item.DepartmentIdExt
                                && p.JobTitlesId == item.JobTitlesId
                                && p.PositionId == item.PositionId
                            );

                        if (!checkExist)
                        {
                            notExist.Add(new UserDepartmentJobTitlesPosition
                            {
                                UserId = model.NewUserId,
                                DepartmentId = item.DepartmentId,
                                DepartmentIdExt = item.DepartmentIdExt,
                                PositionId = item.PositionId,
                                JobTitlesId = item.JobTitlesId,
                                IsAdmin = item.IsAdmin
                            });
                        }
                    }
                }

                //Thêm mới những quan hệ chức vụ phong ban mà người được thay thế không có
                if (notExist != null && notExist.Any())
                {
                    _userService.CreateUserDeptPos(notExist);
                }

                //Nếu Loại bỏ hẳn người thay thế thì loại bỏ luôn các quan hệ trong phòng ban, chúc vụ người dùng
                if (model.IsDeletedUserWorkflow)
                {
                    _userService.DeleteUserDeptPos(oldUserDeptPos
                        .Select(p => p.UserDepartmentJobTitlesPositionId));
                }
            }
        }

        private void ActivatedUser(ReplaceUserModel model)
        {
            if (model.HasUnActivateUser)
            {
                var oldUser = _userService.Get(model.OldUserId);
                _userService.UpdateActivated(oldUser, false);
            }
        }

        private void BindData()
        {
            ViewBag.AllCategoryBusines = GetCategoryBusiness();

            //ViewBag.AllWorkflows = _workflowService.Gets2(true)
            //                        .Select(p => new
            //                        {
            //                            WorkflowId = p.WorkflowId,
            //                            WorkflowName = p.WorkflowName,
            //                            DocTypeId = p.DocTypeId,
            //                            DocTypeName = p.DocType.DocTypeName,
            //                            CategoryBusinessId = p.DocType.CategoryBusinessId,
            //                            CategoryBusinessIdInEnum = p.DocType.CategoryBusinessIdInEnum
            //                        }).Stringify();

            ViewBag.AllWorkflows = _workflowService.Raw
                .Join(_workflowService.RawDocfieldDoctypeWorkflow,
                    p => p.WorkflowId,
                    x => x.WorkflowId,
                    (p, x) => new { WorkflowId = p.WorkflowId, WorkflowName = p.WorkflowName, DocType = x.DocType })
                .Join(_docTypeService.Raw, p => p.DocType.DocTypeId, x => x.DocTypeId,
                        (p, x) => new
                        {
                            WorkflowId = p.WorkflowId,
                            WorkflowName = p.WorkflowName,
                            DocTypeId = p.DocType.DocTypeId,
                            DocTypeName = p.DocType.DocTypeName,
                            CategoryBusinessId = p.DocType.CategoryBusinessId
                        }).Select(p => new
                        {
                            WorkflowId = p.WorkflowId,
                            WorkflowName = p.WorkflowName,
                            DocTypeId = p.DocTypeId,
                            DocTypeName = p.DocTypeName,
                            CategoryBusinessId = p.CategoryBusinessId,
                            CategoryBusinessIdInEnum = (CategoryBusinessTypes)p.CategoryBusinessId
                        }).Stringify();

            ViewBag.AllUsers = _userService.GetAllCached(true)
                                    .Select(p => new
                                    {
                                        value = p.UserId,
                                        label = string.Format("{0}-{1}", p.FullName, p.Username),
                                    }).Stringify();

        }

        private IEnumerable<SelectListItem> GetCategoryBusiness()
        {
            var result = new List<SelectListItem>();
            var enumValArray = Enum.GetValues(typeof(CategoryBusinessTypes));
            foreach (var val in enumValArray)
            {
                var itemValue = Convert.ToInt32(((CategoryBusinessTypes)val));
                result.Add(new SelectListItem
                {
                    Text = _resourceService.GetEnumDescription<CategoryBusinessTypes>((CategoryBusinessTypes)val),
                    Value = itemValue.ToString()
                });
            }
            return result;
        }

        #endregion

        #region Đồng bộ người dùng

#if QuanTriTapTrungEdition

        public JsonResult SyncWithSSO()
        {
            SyncUserSso();
            return Json(new { success = "Dong bo thanh cong!" }, JsonRequestBehavior.AllowGet);
        }

#endif

        #endregion

        #region Devices

        [HttpPost]
        public JsonResult RemoveDevices(int userId)
        {
            if (userId < 1)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            _mobileDeviceService.RemoveAll(userId);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActiveDevice(int deviceId, bool hasBlock)
        {
            try
            {
                var logoutDevice = _mobileDeviceService.ActiveDevice(deviceId, hasBlock, null);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region private

        private void CreateCookieSearch(UserSearchModel search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchUser];
            if (cookie == null)
            {
                cookie = new HttpCookie(CookieName.SearchUser);
            }

            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Value = data.StringifyJs();
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        private void ReBindDataWhenError(UserModel model, bool isAdminDepartmentUser)
        {
            ViewBag.AllRoles = _roleService.Gets(true).ToListModel();
            ViewBag.SelectedRoles = model.RoleIds.StringifyJs();
            if (model.RoleIds != null)
            {
                ViewBag.RolePermissions = _roleService.GetRolePermissions(model.RoleIds, rp => new { rp.PermissionId, rp.AllowAccess })
                                        .Distinct().StringifyJs();
            }

            ViewBag.PermissionsInSystem = _permissionService.GetAllPermissonsInSystem().ToListModel();
            ViewBag.PermissionsSelected = GetPermissionsSelected(model.DenyPermissionIds, model.GrantPermissionIds);
            ViewBag.AllJobTitles = _jobTitlesService.Gets().ToListModel();
            ViewBag.AllPositions = _positionService.Gets().ToListModel();
            ViewBag.AllDepartments = GetAllDepartments(isAdminDepartmentUser);
            ViewBag.DepartmentJobTitlesIdsSelected = model.DepartmentJobTitlesId
                                                    .StringifyJs();

            var devices = _mobileDeviceService.Gets(model.UserId).OrderByDescending(m => m.LastUpdate);
            ViewBag.Devices = devices.ToList();

        }

        private static string GetPermissionsSelected(IEnumerable<int> denyPermissionIds, IEnumerable<int> grantPermissionIds)
        {
            if (denyPermissionIds == null)
            {
                denyPermissionIds = new List<int>();
            }
            if (grantPermissionIds == null)
            {
                grantPermissionIds = new List<int>();
            }
            return denyPermissionIds
                    .Select(id => new
                    {
                        PermissionId = id,
                        AllowAccess = false
                    })
                    .Concat(grantPermissionIds
                    .Select(id => new
                    {
                        PermissionId = id,
                        AllowAccess = true
                    })).StringifyJs();
        }

        private string GetAllDepartments(bool isAdminDepartmentUser)
        {
            var result = "[]";
            var allDepartments = isAdminDepartmentUser
                                    ? _departmentService.GetAllDepartmentUserAccess(User.GetUserId())
                                    : _departmentService.GetCacheAllDepartments(true);
            if (allDepartments != null)
            {
                result = allDepartments
                            .Select(d =>
                                new
                                {
                                    label = d.DepartmentPath,
                                    value = d.DepartmentId,
                                    departmentName = d.DepartmentName,
                                    parentId = d.ParentId
                                }
                            )
                            .OrderBy(d => d.label).StringifyJs();
            }
            return result;
        }

        private void WriteLog(string message)
        {
            var logFile = Server.MapPath("~/TempFile/ImportLog.txt");
            using (var writer = new StreamWriter(logFile, true, Encoding.UTF8))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        private List<SelectListItem> GetListDomain()
        {
            var result = new List<SelectListItem>();
            var defaultDomain = _connectionSettings.BkavSSOParentDomain;

            var currentDomain = HttpContext.Request.GetDomainName();
            result.Add(new SelectListItem { Value = currentDomain, Text = currentDomain, Selected = defaultDomain == currentDomain });

            var domainItems = currentDomain.Split('.').ToList(); // ["test", "egov", "bkav", "com"]
            var countItem = domainItems.Count();   // 4
            while (countItem > 2)
            {
                domainItems.RemoveAt(0);
                var parentDomain = string.Join(".", domainItems); // domainItems[countItem - 2] + "." + domainItems[countItem - 1];
                result.Add(new SelectListItem { Value = parentDomain, Text = parentDomain, Selected = defaultDomain == parentDomain });
                countItem = domainItems.Count();
            }

            return result;
        }

#if QuanTriTapTrungEdition

        private CustomerServiceClient GetClientService()
        {
            var ssoUrl = _authenticationSettings.SingleSignOnDomain;
            ssoUrl = Path.Combine(ssoUrl, "Customer");
            ssoUrl = ssoUrl.ToLower().Replace("https", "http");
            var client = new CustomerServiceClient("CustomerEndpoint", ssoUrl);
            return client;
        }

        private void CreateUserToSso(UserModel model)
        {
            var client = GetClientService();
            //var userName = System.Web.HttpContext.Current.User.GetUserName();
            // var domainName = _cache.Get<string>(string.Format(CacheParam.DomainNameKey, userName));

            var domainName = System.Web.HttpContext.Current == null ? "" : System.Web.HttpContext.Current.Request.GetDomainName();

            //Tạo mới account trên SSO đảm bảo domainName chính là domain của site hiện tại để quản lý account theo domain
            client.CreateUserNew(model.Username, model.UsernameEmailDomain, model.Password, model.FullName, model.Gender,
                              model.Phone, model.Fax, model.Address, model.OpenId, domainName);

            client.Close();
        }

        private void SyncUserSso()
        {
            var users = _userService.Gets();

            var _accountService = DependencyResolver.Current.GetService<AccountBll>();
            var _domainServive = DependencyResolver.Current.GetService<DomainBll>();

            var domainName = System.Web.HttpContext.Current == null ? "" : System.Web.HttpContext.Current.Request.GetDomainName();

            var domainId = string.IsNullOrEmpty(domainName) ? 0 : _domainServive.GetDomainIdByDomainAlias(domainName);
            if (domainId == 0)
            {
                return;
            }

            var now = DateTime.Now;
            foreach (var u in users)
            {
                try
                {
                    var userDomainName = u.UsernameEmailDomain;
                    var account = _accountService.GetByUserDomainName(u.UsernameEmailDomain, true);

                    if (account == null)
                    {
                        var newAccount = new Account()
                        {
                            Username = u.Username,
                            UsernameEmailDomain = u.UsernameEmailDomain,
                            FullName = u.FullName,
                            Gender = u.Gender,
                            DomainName = u.DomainName,
                            IsActivated = u.IsActivated,
                            PasswordHash = u.PasswordHash,
                            PasswordSalt = u.PasswordSalt,
                            Phone = "",
                            Fax = "",
                            Address = "",
                            OpenId = "",
                            PasswordLastModifiedOnDate = now,
                            CreatedOnDate = now,
                            IsLockedOut = false,
                            VersionDateTime = now,
                            AccountDomains = new List<AccountDomain>()
                        };

                        newAccount.AccountDomains.Add(new AccountDomain() { DomainId = domainId });

                        _accountService.Create(newAccount);
                    }
                    else
                    {
                        account.IsActivated = u.IsActivated;
                        if (!account.AccountDomains.Any(ac => ac.DomainId == domainId))
                        {
                            account.AccountDomains.Add(new AccountDomain() { DomainId = domainId });
                        }

                        _accountService.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    LogException("Dong bo user " + u.Username + " " + ex.Message);
                }
            }
        }

#endif

        #endregion
    }
    public class DepartmentImport
    {
        public string name { get; set; }
        public string namepath { get; set; }
        public string edocid { get; set; }
    }
}
