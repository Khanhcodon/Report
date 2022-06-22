using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class RoleController : CustomController
    {
        private readonly RoleBll _roleService;
        private readonly UserBll _userService;
        private readonly ResourceBll _resourceService;
        private readonly PermissionBll _permissionService;

        public RoleController(
            RoleBll roleService,
            UserBll userService,
            ResourceBll resourceService,
            PermissionBll permissionService)
            : base()
        {
            _roleService = roleService;
            _userService = userService;
            _resourceService = resourceService;
            _permissionService = permissionService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Role.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Role.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var roles = _roleService.GetsAs(r => new RoleModel
            {
                RoleId = r.RoleId,
                RoleKey = r.RoleKey,
                RoleName = r.RoleName,
                Description = r.Description,
                IsActivated = r.IsActivated
            });
            return View(roles);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Role.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Role.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            ViewBag.AllUsers = GetAllUsers();
            ViewBag.PermissionsInSystem = GetAllPermission(); //_permissionService.GetAllPermissonsInSystem().ToListModel();
            return View(new RoleModel());
        }

        [HttpPost]
        public JsonResult IsExistRoleKey(string roleKey, string oldRoleKey)
        {
            if (_roleService.IsExistRoleKey(roleKey, oldRoleKey))
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Role.CreateOrEdit.Fields.RoleKey.Exist"));
                return Json(new { error = _resourceService.GetResource("Role.CreateOrEdit.Fields.RoleKey.Exist") });
            }
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Role.CreateOrEdit.Fields.RoleKey.Valid"));
            return Json(new { success = _resourceService.GetResource("Role.CreateOrEdit.Fields.RoleKey.Valid") });
        }

        [HttpPost]
        public ActionResult Create(RoleModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Role.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Role.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    model.RoleKey = model.RoleKey.StripVietnameseChars().Replace(" ", "");
                    var role = model.ToEntity();
                    var now = DateTime.Now;
                    role.CreatedByUserId = User.GetUserId();
                    role.CreatedOnDate = now;
                    role.VersionDateTime = now;
                    role.IsActivated = true;
                    _roleService.Create(role);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Role.Created"));
                    SuccessNotification(_resourceService.GetResource("Role.Created"));
                    return RedirectToAction("Create");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            ReBindDataWhenError(model);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Role.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Role.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var role = _roleService.Get(id);
            if (role == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Role.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Role.NotExist"));
                return RedirectToAction("Index");
            }

            ViewBag.AllUsers = GetAllUsers();
            ViewBag.SelectedUsers = role.UserRoles.Select(ur => ur.UserId).StringifyJs();
            ViewBag.PermissionsInSystem = GetAllPermission(); //_permissionService.GetAllPermissonsInSystem().ToListModel();
            ViewBag.PermissionsSelected = role.UserRolePermissions
                                            .Select(urp => new
                                            {
                                                urp.PermissionId,
                                                urp.AllowAccess
                                            }).StringifyJs();
            return View(role.ToModel());
        }

        [HttpPost]
        public ActionResult Edit(RoleModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Role.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Role.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var role = _roleService.Get(model.RoleId);
                if (role == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Role.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Customer.Role.NotExist"));
                    return RedirectToAction("Index");
                }

                var oldRoleKey = role.RoleKey;
                role = model.ToEntity(role);
                role.LastModifiedOnDate = DateTime.Now;
                role.LastModifiedByUserId = User.GetUserId();

                try
                {
                    _roleService.Update(role, oldRoleKey);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Role.Updated"));
                    SuccessNotification(_resourceService.GetResource("Role.Updated"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            ReBindDataWhenError(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Role.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Role.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var role = _roleService.Get(id);
            if (role == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Role.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Role.NotExist"));
                return RedirectToAction("Index");
            }

            _roleService.Delete(role);
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Role.Deleted"));
            SuccessNotification(_resourceService.GetResource("Role.Deleted"));

            return RedirectToAction("Index");
        }

        #region private method

        private static string GetPermissionsSelected(IEnumerable<int> grantPermissionIds)
        {
            if (grantPermissionIds == null)
            {
                grantPermissionIds = new List<int>();
            }

            return grantPermissionIds.Select(id => new
                    {
                        PermissionId = id,
                        AllowAccess = true
                    }).StringifyJs();
        }


        private void ReBindDataWhenError(RoleModel model)
        {
            ViewBag.AllUsers = GetAllUsers();
            ViewBag.SelectedUsers = model.UserIds.StringifyJs();
            ViewBag.PermissionsInSystem = GetAllPermission(); // _permissionService.GetAllPermissonsInSystem().ToListModel();
            ViewBag.PermissionsSelected = GetPermissionsSelected(model.GrantPermissionIds);
        }

        private IEnumerable<PermissionModel> GetAllPermission()
        {
            var result = _permissionService.GetAllPermissonsInSystem();
            return result.ToListModel();
        }

        private string GetAllUsers()
        {
            var allUsers = _userService.GetAllCached(true).OrderBy(u => u.Username);
            return allUsers.Select(
                                u =>
                                new
                                {
                                    value = u.UserId,
                                    label = u.Username + " - " + u.FullName,
                                    fullname = u.FullName,
                                    username = u.Username
                                }).StringifyJs();
        }

        #endregion
    }
}
