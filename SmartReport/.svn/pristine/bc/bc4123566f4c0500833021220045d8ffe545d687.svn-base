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
    public class AuthorizeController : CustomController
    {
        private readonly AuthorizeBll _authorizeService;
        private readonly ResourceBll _resourceService;
        private readonly DocFieldBll _docFieldService;
        private readonly DocTypeBll _docTypeService;
        private readonly UserBll _userService;
        private readonly AdminGeneralSettings _generalSettings;

        private const string DEFAULT_SORT_BY = "DateEnd";

        public AuthorizeController(
            AuthorizeBll authorizeService,
            ResourceBll resourceService,
            DocFieldBll docfieldService,
            DocTypeBll doctypeService,
            UserBll userService,
            AdminGeneralSettings generalSettings)
            : base()
        {
            _resourceService = resourceService;
            _authorizeService = authorizeService;
            _docFieldService = docfieldService;
            _docTypeService = doctypeService;
            _userService = userService;
            _generalSettings = generalSettings;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var search = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchAuthorize];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            int totalRecords;
            Expression<Func<Authorize, bool>> spec = null;
            if (!string.IsNullOrEmpty(search))
            {
                spec = p => p.AuthorizedUserName.Contains(search) || p.AuthorizeUserName.Contains(search);
            }

            var model = _authorizeService.GetsAs(out totalRecords,
                                                    t => t,
                                                    spec: spec,
                                                    pageSize: sortAndPage.PageSize,
                                                    sortBy: sortAndPage.SortBy,
                                                    isDescending: sortAndPage.IsSortDescending,
                                                    currentPage: sortAndPage.CurrentPage).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Search = search;
            ViewBag.SortAndPage = sortAndPage;
            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            return View(model);
        }

        private void CreateCookieSearch(string search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchAuthorize];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchAuthorize, data.StringifyJs())
                {
                    Expires = DateTime.Now.AddDays(1),
                    Path = "/admin"
                };
            }
            Response.Cookies.Add(cookie);
        }


        public ActionResult Search(string search, int pageSize)
        {
            IEnumerable<AuthorizeModel> model = null;
            SortAndPagingModel sortAndPage = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    Expression<Func<Authorize, bool>> spec = null;
                    if (!string.IsNullOrEmpty(search))
                    {
                        spec = p => p.AuthorizedUserName.Contains(search) || p.AuthorizeUserName.Contains(search);
                    }
                    model = _authorizeService.GetsAs(out totalRecords,
                                                    t => t,
                                                    spec: spec,
                                                    pageSize: pageSize,
                                                    sortBy: DEFAULT_SORT_BY,
                                                    isDescending: true).ToListModel();
                    sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = true,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };
                }
                else
                {
                    sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = false,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = 0
                    };
                }
            }

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            CreateCookieSearch(search, sortAndPage);
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(string search, string sortBy, bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<AuthorizeModel> model = null;
            SortAndPagingModel sortAndPage = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                Expression<Func<Authorize, bool>> spec = null;
                if (!string.IsNullOrEmpty(search))
                {
                    spec = p => p.AuthorizedUserName.Contains(search) || p.AuthorizeUserName.Contains(search);
                }
                model = _authorizeService.GetsAs(out totalRecords,
                                               t => t,
                                               spec: spec,
                                                pageSize: pageSize,
                                                sortBy: sortBy,
                                                isDescending: isSortDesc,
                                                currentPage: page).ToListModel();
                sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
            }

            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.SortAndPage = sortAndPage;
            CreateCookieSearch(search, sortAndPage);
            return PartialView("_PartialList", model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            BindData();
            return View(new AuthorizeModel());
        }

        [HttpPost]
        public ActionResult Create(AuthorizeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var authorize = model.ToEntity();
                authorize.Permission = 0;
                if (model.Permissions != null && model.Permissions.Any())
                {
                    foreach (var permission in model.Permissions)
                    {
                        authorize.Permission |= permission;
                    }
                }

                authorize.DocTypeId = authorize.DocTypeId == "[]" ? null : authorize.DocTypeId;

                try
                {
                    var dateNow = DateTime.Now;
                    //Lấy ra danh sách người nhận ủy quyền là người ủy quyền và người ủy quyền là người nhận ủy quyền 
                    //còn hoạt động trong khoảng thời gian khi đang tạo mới để cảnh báo 
                    //không cho tạo mới => vòng lặp vô hạn khi hiển thị cây văn bản
                    var exist = _authorizeService.Gets(p =>
                             (p.AuthorizedUserId == model.AuthorizeUserId
                             && p.AuthorizeUserId == model.AuthorizedUserId
                             && p.Active
                             && p.DateEnd >= model.DateBegin
                             && p.DateEnd <= model.DateEnd)
                             || (p.AuthorizedUserId == model.AuthorizedUserId
                             && p.AuthorizeUserId == model.AuthorizeUserId
                             && p.Active == model.Active
                             && p.DateEnd >= model.DateBegin
                             && p.DateEnd <= model.DateEnd));
                    if (exist != null && exist.Any())
                    {
                        if (model.HasDeleteExist)
                        {
                            _authorizeService.Delete(exist);
                        }
                        else
                        {
                            ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Created.ExistOrConflict"));
                            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Created.ExistOrConflict"));
                            BindData();
                            return View(model);
                        }
                    }

                    _authorizeService.Create(authorize);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Created.Success"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Created.Success"));
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    BindData();
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Created.Error"));
                    CreateActivityLog(ActivityLogType.Admin,_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Created.Error"));
                    return View(model);
                }
            }

            BindData();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotPermissionUpdate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Authorize.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var authorize = _authorizeService.Get(id);
            if (authorize == null)
            {
                ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Authorize.NotExist"));
                return RedirectToAction("Index");
            }
            var model = authorize.ToModel();
            BindData();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AuthorizeModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotPermissionUpdate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Authorize.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var authorize = _authorizeService.Get(model.AuthorizeId);
                if (authorize == null)
                {
                    ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotExist"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Authorize.NotExist"));
                    return RedirectToAction("Index");
                }

                try
                {
                    //Lấy ra danh sách người nhận ủy quyền là người ủy quyền và người ủy quyền là người nhận ủy quyền 
                    //còn hoạt động trong khoảng thời gian khi đang tạo mới để cảnh báo 
                    //không cho tạo mới => vòng lặp vô hạn khi hiển thị cây văn bản
                    var exist = _authorizeService.Gets(p =>
                        p.AuthorizedUserId == model.AuthorizeUserId
                        && p.AuthorizeUserId == model.AuthorizedUserId
                        && p.Active
                        && p.DateEnd >= model.DateBegin
                        && p.DateEnd <= model.DateEnd);
                    if (exist != null && exist.Any())
                    {
                        ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Created.ExistOrConflict"));
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Created.ExistOrConflict"));
                        BindData();
                        return View(model);
                    }

                    authorize = model.ToEntity(authorize);
                    authorize.Permission = 0;
                    if (model.Permissions != null && model.Permissions.Any())
                    {
                        foreach (var permission in model.Permissions)
                        {
                            authorize.Permission |= permission;
                        }
                    }

                    authorize.DocTypeId = authorize.DocTypeId == "[]" ? null : authorize.DocTypeId;
                    _authorizeService.Update(authorize);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Updated.Success"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Updated.Success"));
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Updated.Error"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Updated.Error"));
                }
            }
            BindData();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Authorize.NotPermissionDelete"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Authorize.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var authorize = _authorizeService.Get(id);
            if (authorize == null)
            {
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _authorizeService.Delete(authorize);
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Deleted.Success"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Deleted.Success"));
            }
            catch (Exception ex)
            {
                LogException(ex);
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Deleted.Error"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Authorize.Deleted.Error"));
            }
            return RedirectToAction("Index");
        }

        private string GetAllDocFields()
        {
            return _docFieldService.GetsAs(d => new { d.DocFieldId, d.DocFieldName, d.CategoryBusinessId })
                    .OrderBy(d => d.DocFieldName)
                    .StringifyJs();
        }

        private string GetAllDocTypes()
        {
            return _docTypeService.GetsAs(d => new { d.DocTypeId, d.DocTypeName, d.DocFieldId, d.CategoryBusinessId })
                    .OrderBy(d => d.DocTypeName)
                    .StringifyJs();
        }

        private void BindData()
        {
            ViewBag.AllUsers = GetAllUsers();
            ViewBag.CategoryBusinessId = _resourceService.EnumToSelectList<CategoryBusinessTypes>().ToList();

            ViewBag.AllDocFields = GetAllDocFields();
            ViewBag.AllDocTypes = GetAllDocTypes();
            ViewBag.AllPermission = _resourceService.EnumToSelectList<PermissionTypes>((int)PermissionTypes.XLy);
        }

        private string GetAllUsers()
        {
            var allUsers = _userService.GetAllCached(true).OrderBy(u => u.Username);
            return allUsers.Select(
                                u =>
                                new
                                {
                                    value = u.UserId,
                                    label = string.Format("{0}({1})", u.FullName, u.Username),
                                    fullname = u.FullName,
                                    username = u.Username,
                                    firstpositionid = 0
                                }).StringifyJs();
        }
    }
}
