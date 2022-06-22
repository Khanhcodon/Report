using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class PermissionSettingController : CustomController
    {
        private readonly PermissionSettingBll _permissionSettingService;
        private readonly ProcessFunctionBll _processFunctionService;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly UserBll _userService;

        private const string DEFAULT_SORT_BY = "PermissionSettingName";

        public PermissionSettingController(
            PermissionSettingBll permissionSettingService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            ProcessFunctionBll processFunctionService,
            DepartmentBll departmentService,
            PositionBll positionService,
            UserBll userService)
            : base()
        {
            _resourceService = resourceService;
            _permissionSettingService = permissionSettingService;
            _generalSettings = generalSettings;
            _processFunctionService = processFunctionService;
            _departmentService = departmentService;
            _positionService = positionService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = true,
                SortBy = DEFAULT_SORT_BY
            };

            var search = string.Empty;
            var httpCookie = Request.Cookies[CookieName.SearchPermissionSetting];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch { }
            }

            int totalRecords;
            Expression<Func<PermissionSetting, bool>> spec = null;
            if (!string.IsNullOrEmpty(search))
            {
                spec = p => p.PermissionSettingName.Contains(search);
            }
            var model = _permissionSettingService.GetsAs(out totalRecords,
                                                t => t,
                                                spec: spec,
                                                pageSize: sortAndPage.PageSize,
                                                sortBy: sortAndPage.SortBy,
                                                isDescending: sortAndPage.IsSortDescending,
                                                currentPage: sortAndPage.CurrentPage).ToListModel();
            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Search = search;

            return View(model);
        }

        public ActionResult Search(string search, int pageSize)
        {
            IEnumerable<PermissionSettingModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    Expression<Func<PermissionSetting, bool>> spec = null;
                    if (!string.IsNullOrEmpty(search))
                    {
                        spec = p => p.PermissionSettingName.Contains(search);
                    }

                    model = _permissionSettingService.GetsAs(out totalRecords,
                                                    t => t,
                                                    spec: spec,
                                                    pageSize: pageSize,
                                                    sortBy: DEFAULT_SORT_BY,
                                                    isDescending: true).ToListModel();

                    var sortAndPage = new SortAndPagingModel
                       {
                           PageSize = pageSize,
                           CurrentPage = 1,
                           IsSortDescending = true,
                           SortBy = DEFAULT_SORT_BY,
                           TotalRecordCount = totalRecords
                       };
                    ViewBag.SortAndPage = sortAndPage;
                    CreateCookieSearch(search, sortAndPage);
                }
                else
                {
                    ViewBag.SortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = true,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = 0
                    };
                }
            }

            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(string search, string sortBy,
            bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<PermissionSettingModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;

                Expression<Func<PermissionSetting, bool>> spec = null;
                if (!string.IsNullOrEmpty(search))
                {
                    spec = p => p.PermissionSettingName.Contains(search);
                }

                model = _permissionSettingService.GetsAs(out totalRecords,
                                                t => t,
                                                spec: spec,
                                                pageSize: pageSize,
                                                sortBy: sortBy,
                                                isDescending: isSortDesc,
                                                currentPage: page).ToListModel();

                var sortAndPage = new SortAndPagingModel
                     {
                         PageSize = pageSize,
                         CurrentPage = page,
                         IsSortDescending = isSortDesc,
                         SortBy = sortBy,
                         TotalRecordCount = totalRecords
                     };
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Search = search;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                CreateCookieSearch(search, sortAndPage);
            }

            return PartialView("_PartialList", model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}
            BindData();
            return View(new PermissionSettingModel());
        }

        [HttpPost]
        public ActionResult Create(PermissionSettingModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotPermissionCreate"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    SetPermission(model);
                    var docColumnSetting = model.ToEntity();
                    _permissionSettingService.Create(docColumnSetting);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.Created.Success"));
                    SuccessNotification(_resourceService.GetResource("Admin.DocColumnSetting.Created.Success"));
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.Created.Error"));
                    ErrorNotification(_resourceService.GetResource("Admin.DocColumnSetting.Created.Error"));
                }
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            var docColumnSetting = _permissionSettingService.Get(id);
            if (docColumnSetting == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.NotExist"));
                ErrorNotification(_resourceService.GetResource("Admin.DocColumnSetting.NotExist"));
                return RedirectToAction("Index");
            }
            BindData();
            return View(docColumnSetting.ToModel());
        }

        [HttpPost]
        public ActionResult Edit(PermissionSettingModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotPermissionUpdate"));
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                var docColumnSetting = _permissionSettingService.Get(model.PermissionSettingId);
                if (docColumnSetting == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.NotExist"));
                    ErrorNotification(_resourceService.GetResource("Admin.DocColumnSetting.NotExist"));
                    return RedirectToAction("Index");
                }

                try
                {
                    SetPermission(model);
                    docColumnSetting = model.ToEntity(docColumnSetting);
                    _permissionSettingService.Update(docColumnSetting);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.Updated.Success"));
                    SuccessNotification(_resourceService.GetResource("Admin.DocColumnSetting.Updated.Success"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.Updated.Error"));
                    ErrorNotification(_resourceService.GetResource("Admin.DocColumnSetting.Updated.Error"));
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.DocColumnSetting.NotPermissionDelete"));
            //    return RedirectToAction("Index");
            //}

            var docColumnSetting = _permissionSettingService.Get(id);
            if (docColumnSetting == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.NotExist"));
                ErrorNotification(_resourceService.GetResource("Admin.DocColumnSetting.NotExist"));
                return RedirectToAction("Index");
            }

            var exist = _processFunctionService.GetsAs(p => p.DocColumnSettingId, p => p.DocColumnSettingId == id);
            if (exist != null && exist.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.IsUse"));
                ErrorNotification(_resourceService.GetResource("Admin.DocColumnSetting.IsUse"));
                return RedirectToAction("Index");
            }

            try
            {
                _permissionSettingService.Delete(docColumnSetting);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.Deleted.Success"));
                SuccessNotification(_resourceService.GetResource("Admin.DocColumnSetting.Deleted.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.Deleted.Error"));
                ErrorNotification(_resourceService.GetResource("Admin.DocColumnSetting.Deleted.Error"));
            }
            return RedirectToAction("Index");
        }

        private void BindData()
        {
            ViewBag.AllDepartments = GetAllDepartments();
            ViewBag.AllUsers = GetAllUsers();
            ViewBag.AllPositions = _positionService.GetCacheAllPosition().StringifyJs();
        }

        private void SetPermission(PermissionSettingModel model)
        {
            if (model.PositionIds != null && model.PositionIds.Any())
            {
                model.PositionHasPermission = model.PositionIds.StringifyJs();
            }
            if (model.UserIds != null && model.UserIds.Any())
            {
                model.UserHasPermission = model.UserIds.StringifyJs();
            }
            if (model.DepartmentPositionIds != null && model.DepartmentPositionIds.Any())
            {
                var allDepartmentIds = _departmentService.GetsAs(d => d.DepartmentId, true);
                var allPositionIds = _positionService.GetsAs(p => p.PositionId);
                var departmentPositions = new List<IDictionary<string, int>>();
                foreach (var item in model.DepartmentPositionIds)
                {
                    var split = item.Split(',');
                    if (split.Length != 2)
                    {
                        continue;
                    }

                    int departmentParsed, positionParsed;
                    if (int.TryParse(split[0], out departmentParsed)
                        && int.TryParse(split[1], out positionParsed))
                    {
                        if (allDepartmentIds.Any(did => did == departmentParsed)
                            && allPositionIds.Any(pid => pid == positionParsed))
                        {
                            departmentPositions.Add(new Dictionary<string, int> { { "DepartmentId", departmentParsed }, { "PositionId", positionParsed } });
                        }
                    }
                }

                model.DepartmentPositionHasPermission = departmentPositions.StringifyJs();
            }
        }

        private string GetAllDepartments()
        {
            var result = "[]";
            var allDepartments = _departmentService.GetCacheAllDepartments(true);
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
                                    username = u.Username,
                                    phone = u.Phone
                                }).StringifyJs();
        }

        private void CreateCookieSearch(string search, SortAndPagingModel sortAndPage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortAndPage } };
            var cookie = Request.Cookies[CookieName.SearchPermissionSetting];
            if (cookie == null)
            {
                cookie = new HttpCookie(CookieName.SearchPermissionSetting);
            }

            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Value = data.StringifyJs();
            cookie.Path = "/admin";

            Response.Cookies.Add(cookie);
        }
    }
}
