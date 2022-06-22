using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class ActivityLogController : CustomController
    {
        private readonly ActivityLogBll _logService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly ResourceBll _resourceService;
        private const string DEFAULT_SORT_BY = "CreatedOnDate";

        public ActivityLogController(ActivityLogBll logService,
                                    AdminGeneralSettings generalSettings,
                                    ResourceBll resourceService)
            : base()
        {
            _logService = logService;
            _generalSettings = generalSettings;
            _resourceService = resourceService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ActivityLog.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.ActivityLog.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var search = new ActivityLogSearchModel();
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };

            var httpCookie = Request.Cookies[CookieName.SearchActivityLog];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<ActivityLogSearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            int totalRecords;
            var model = _logService.Gets(out totalRecords,
                      pageSize: sortAndPage.PageSize,
                      sortBy: sortAndPage.SortBy,
                      currentPage: sortAndPage.CurrentPage,
                      isDescending: sortAndPage.IsSortDescending,
                      from: string.IsNullOrEmpty(search.FromDate)
                          ? (DateTime?)null
                          : DateTime.Parse(search.FromDate,
                              System.Globalization.CultureInfo.
                                  GetCultureInfo("vi-VN").
                                  DateTimeFormat),
                      to: string.IsNullOrEmpty(search.ToDate)
                          ? (DateTime?)null
                          : DateTime.Parse(search.ToDate,
                              System.Globalization.CultureInfo.
                                  GetCultureInfo("vi-VN").
                                  DateTimeFormat).AddDays(1).AddSeconds(-1),
                       activityLogType: search.ActivityLogType,
                       findByUser: search.FindUser)
                      .ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;
            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            ViewBag.LogType = _resourceService.EnumToSelectList<ActivityLogType>();
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Logs = model;
            ViewBag.Search = search;
            return View(search);
        }

        private void CreateCookieSearch(ActivityLogSearchModel model, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", model }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchActivityLog];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchActivityLog, data.StringifyJs())
                {
                    Expires = DateTime.Now.AddDays(1),
                    Path = "/admin"
                };
            }
            Response.Cookies.Add(cookie);
        }

        public ActionResult Search(ActivityLogSearchModel search, int pageSize)
        {
            IEnumerable<ActivityLogModel> model = null;
            SortAndPagingModel sortAndPage = null;

            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    model = _logService.Gets(out totalRecords,
                       pageSize: pageSize, sortBy: DEFAULT_SORT_BY,
                       isDescending: true,
                       from: string.IsNullOrEmpty(search.FromDate)
                           ? (DateTime?)null
                           : DateTime.Parse(search.FromDate,
                               System.Globalization.CultureInfo.
                                   GetCultureInfo("vi-VN").
                                   DateTimeFormat),
                       to: string.IsNullOrEmpty(search.ToDate)
                           ? (DateTime?)null
                           : DateTime.Parse(search.ToDate,
                               System.Globalization.CultureInfo.
                                   GetCultureInfo("vi-VN").
                                   DateTimeFormat).AddDays(1).AddSeconds(-1),
                        activityLogType: search.ActivityLogType,
                        findByUser: search.FindUser)
                       .ToListModel();

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
            ViewBag.Logs = model;
            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            CreateCookieSearch(search, sortAndPage);
            return PartialView("_PartialList");
        }

        public ActionResult SortAndPaging(
            ActivityLogSearchModel search,
            string sortBy, bool isSortDesc,
             int page, int pageSize)
        {
            IEnumerable<ActivityLogModel> model = null;
            SortAndPagingModel sortAndPage = null;

            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                model = _logService.Gets(out totalRecords, pageSize: pageSize, sortBy: sortBy,
                                   isDescending: isSortDesc,
                                   from: string.IsNullOrEmpty(search.FromDate)
                                       ? (DateTime?)null
                                       : DateTime.Parse(search.FromDate,
                                           System.Globalization.CultureInfo.
                                               GetCultureInfo("vi-VN").
                                               DateTimeFormat),
                                   to: string.IsNullOrEmpty(search.ToDate)
                                       ? (DateTime?)null
                                       : DateTime.Parse(search.ToDate,
                                           System.Globalization.CultureInfo.
                                               GetCultureInfo("vi-VN").
                                               DateTimeFormat).AddDays(1).AddSeconds(-1),
                                   currentPage: page, activityLogType: search.ActivityLogType,
                                    findByUser: search.FindUser)
                                   .ToListModel();
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
            ViewBag.Logs = model;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            CreateCookieSearch(search, sortAndPage);
            return PartialView("_PartialList");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "ActivityLogDelete")]
        public ActionResult Delete(List<int> logids)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.ActivityLog.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            _logService.Delete(logids);
            SuccessNotification(_resourceService.GetResource("Customer.ActivityLog.Deleted"));
            CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.ActivityLog.Deleted"));
            return RedirectToAction("Index");
        }
    }
}
