using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class SmsManagerController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly SmsBll _smsService;
        private readonly AdminGeneralSettings _generalSettings;
        private const string DEFAULT_SORT_BY = "DateCreated";

        public SmsManagerController(
            ResourceBll resourceService,
            SmsBll smsService,
            AdminGeneralSettings generalSettings)
            : base()
        {
            _resourceService = resourceService;
            _smsService = smsService;
            _generalSettings = generalSettings;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Sms.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = true,
                SortBy = DEFAULT_SORT_BY
            };

            var search = new SmsOrMailSearchModel();
            var httpCookie = Request.Cookies[CookieName.SearchSms];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<SmsOrMailSearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch { }
            }

            int totalRecords;
            var model = _smsService.GetsAs(out totalRecords,
                                            l => l,
                                            pageSize: sortAndPage.PageSize,
                                            sortBy: sortAndPage.SortBy,
                                            isDescending: sortAndPage.IsSortDescending,
                                            currentPage: sortAndPage.CurrentPage,
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
                                            findText: search.FindText,
                                            isSent: search.IsSent);

            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Smss = model.ToListModel();
            ViewBag.Search = search;

            return View(search);
        }

        private void CreateCookieSearch(SmsOrMailSearchModel search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchSms];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchSms, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        public ActionResult Search(SmsOrMailSearchModel search, int pageSize)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    var model = _smsService.GetsAs(out totalRecords,
                                                    l => l,
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
                                                    findText: search.FindText,
                                                    isSent: search.IsSent);
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = true,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };

                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.Smss = model.ToListModel();
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
                    ViewBag.Smss = null;
                }
            }

            ViewBag.Search = search;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return PartialView("_PartialList");
        }

        public ActionResult SortAndPaging(SmsOrMailSearchModel search, string sortBy, bool isSortDesc, int page, int pageSize)
        {
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                var model = _smsService.GetsAs(out totalRecords,
                                                l => l, pageSize: pageSize, sortBy: sortBy,
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
                                                currentPage: page,
                                                findText: search.FindText,
                                                isSent: search.IsSent);
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
                ViewBag.Smss = model.ToListModel();
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                CreateCookieSearch(search, sortAndPage);
            }

            return PartialView("_PartialList");
        }

        [HttpPost]
        public ActionResult Delete(List<int> ids)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Sms.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            if (ids == null || !ids.Any())
            {
                return RedirectToAction("Index");
            }

            var models = _smsService.Gets(p => ids.Contains(p.SmsId));
            if (models == null || !models.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Sms.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Sms.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _smsService.Delete(models);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Sms.Deleted"));
                SuccessNotification(_resourceService.GetResource("Customer.Sms.Deleted"));
            }
            catch (EgovException ex)
            {
                LogException(ex);
                ErrorNotification(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Sms.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var model = _smsService.Get(id);
            if (model == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Sms.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Sms.NotExist"));
                return RedirectToAction("Index");
            }

            return View(model.ToModel());
        }
    }
}
