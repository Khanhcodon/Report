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
using Bkav.eGovCloud.Entities.Customer.Settings;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities.Enum;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class DocColumnSettingController : CustomController
    {
        private readonly DocColumnSettingBll _docColumnSettingService;
        private readonly ProcessFunctionBll _processFunctionService;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private const string DEFAULT_SORT_BY = "DocColumnSettingName";

        public DocColumnSettingController(
            DocColumnSettingBll docColumnSettingService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings,
            ProcessFunctionBll processFunctionService)
            : base()
        {
            _resourceService = resourceService;
            _docColumnSettingService = docColumnSettingService;
            _generalSettings = generalSettings;
            _processFunctionService = processFunctionService;
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

            var model = InitData(ColumnSettingType.ProcessFunction);
            ViewBag.Type = ColumnSettingType.ProcessFunction;
            return View(model);
        }

        public ActionResult IndexReport()
        {
            var model = InitData(ColumnSettingType.Report);
            ViewBag.Type = ColumnSettingType.Report;
            return View("Index", model);
        }

        private IEnumerable<DocColumnSettingModel> InitData(ColumnSettingType type)
        {
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = true,
                SortBy = DEFAULT_SORT_BY
            };

            var search = string.Empty;
            var httpCookie = Request.Cookies[CookieName.SearchDocColumnSetting];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            Expression<Func<DocColumnSetting, bool>> spec = p => p.Type == (int)type;
            if (!string.IsNullOrEmpty(search))
            {
                spec.And(p => p.DocColumnSettingName.Contains(search));
            }

            int totalRecords;
            var model = _docColumnSettingService.GetsAs(out totalRecords,
                                                    t => t,
                                                    spec: spec,
                                                    pageSize: sortAndPage.PageSize,
                                                    sortBy: sortAndPage.SortBy,
                                                    currentPage: sortAndPage.CurrentPage,
                                                    isDescending: sortAndPage.IsSortDescending).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Search = search;

            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            return model;
        }

        private void CreateCookieSearch(string search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchDocColumnSetting];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchDocColumnSetting, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        public ActionResult Search(string search, int pageSize)
        {
            IEnumerable<DocColumnSettingModel> model = null;
            SortAndPagingModel sortAndPage = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    Expression<Func<DocColumnSetting, bool>> spec = p => p.Type == (int)ColumnSettingType.ProcessFunction;
                    if (!string.IsNullOrEmpty(search))
                    {
                        spec = p => p.DocColumnSettingName.Contains(search);
                    }

                    model = _docColumnSettingService.GetsAs(out totalRecords,
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
                        IsSortDescending = true,
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
            IEnumerable<DocColumnSettingModel> model = null;
            SortAndPagingModel sortAndPage = null;

            if (Request.IsAjaxRequest())
            {
                int totalRecords;

                Expression<Func<DocColumnSetting, bool>> spec = p => p.Type == (int)ColumnSettingType.ProcessFunction;
                if (!string.IsNullOrEmpty(search))
                {
                    spec = p => p.DocColumnSettingName.Contains(search);
                }

                model = _docColumnSettingService.GetsAs(out totalRecords,
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

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            CreateCookieSearch(search, sortAndPage);
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

            ViewBag.Type = ColumnSettingType.ProcessFunction;
            return View(new DocColumnSettingModel() { Type = (int)ColumnSettingType.ProcessFunction });
        }

        public ActionResult CreateReport()
        {
            ViewBag.Type = ColumnSettingType.Report;
            return View("Create", new DocColumnSettingModel() { Type = (int)ColumnSettingType.Report });
        }

        [HttpPost]
        public JsonResult Create(DocColumnSettingModel model)
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
                    var displayColumn = Json2.ParseAs<List<ColumnSetting>>(model.DisplayColumn);
                    var sortColumn = Json2.ParseAs<List<SortColumnModel>>(model.SortColumn);
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.Setting.Error"));
                    return Json(new { error = _resourceService.GetResource("Admin.DocColumnSetting.Setting.Error") });
                }

                try
                {
                    var docColumnSetting = model.ToEntity();
                    _docColumnSettingService.Create(docColumnSetting);
                    return Json(new { success = _resourceService.GetResource("Admin.DocColumnSetting.Created.Success") });
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.Created.Error"));
                    return Json(new { error = _resourceService.GetResource("Admin.DocColumnSetting.Created.Error") });
                }
            }

            return null;
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

            var docColumnSetting = _docColumnSettingService.Get(id);
            if (docColumnSetting == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.NotExist"));
                ErrorNotification(_resourceService.GetResource("Admin.DocColumnSetting.NotExist"));
                return RedirectToAction("Index");
            }

            ViewBag.Type = docColumnSetting.TypeEnum;

            return View(docColumnSetting.ToModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "AuthorizeEdit")]
        public JsonResult Edit(DocColumnSettingModel model)
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
                var docColumnSetting = _docColumnSettingService.Get(model.DocColumnSettingId);
                if (docColumnSetting == null)
                {
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.NotExist"));
                    return Json(new { error = _resourceService.GetResource("Admin.DocColumnSetting.NotExist") });
                }

                try
                {
                    var displayColumn = Json2.ParseAs<List<ColumnSetting>>(model.DisplayColumn);
                    var sortColumn = Json2.ParseAs<List<SortColumnModel>>(model.SortColumn);
                    
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.Setting.Error"));
                    return Json(new { error = _resourceService.GetResource("Admin.DocColumnSetting.Setting.Error") });
                }

                try
                {
                    docColumnSetting = model.ToEntity(docColumnSetting);
                    _docColumnSettingService.Update(docColumnSetting);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.Updated.Success"));
                    return Json(new { success = _resourceService.GetResource("Admin.DocColumnSetting.Updated.Success") });
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Admin.DocColumnSetting.Updated.Error"));
                    return Json(new { error = _resourceService.GetResource("Admin.DocColumnSetting.Updated.Error") });
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return null;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "AuthorizeDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.DocColumnSetting.NotPermissionDelete"));
            //    return RedirectToAction("Index");
            //}

            var docColumnSetting = _docColumnSettingService.Get(id);
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
                _docColumnSettingService.Delete(docColumnSetting);
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
    }
}
