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
    public class MailManagerController : CustomController
    {
        private readonly ResourceBll _resourceService;
        private readonly MailBll _mailService;
        private readonly AdminGeneralSettings _generalSettings;
        private const string DEFAULT_SORT_BY = "DateCreated";

        public MailManagerController(
            ResourceBll resourceService,
            MailBll mailService,
            AdminGeneralSettings generalSettings)
            : base()
        {
            _resourceService = resourceService;
            _mailService = mailService;
            _generalSettings = generalSettings;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Mail.NotPermission"));
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
            var httpCookie = Request.Cookies[CookieName.SearchMail];
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
            var model = _mailService.GetsAs(out totalRecords,
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
                                                    isSent: search.IsSent,
                                                    findText: search.FindText);
            sortAndPage.TotalRecordCount = totalRecords;

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Mails = model.ToListModel();
            ViewBag.Search = search;

            return View(search);
        }

        private void CreateCookieSearch(SmsOrMailSearchModel search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchMail];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchMail, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
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
                    var model = _mailService.GetsAs(out totalRecords,
                                                    l => l,
                                                    pageSize: pageSize,
                                                    sortBy: DEFAULT_SORT_BY,
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
                                                    isSent: search.IsSent,
                                                    findText: search.FindText);
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        IsSortDescending = true,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };
                    CreateCookieSearch(search, sortAndPage);
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.Mails = model.ToListModel();
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
                    ViewBag.Mails = null;
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
                var model = _mailService.GetsAs(out totalRecords,
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
                                                isSent: search.IsSent,
                                                findText: search.FindText);
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
                ViewBag.Mails = model.ToListModel();
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
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
            //    ErrorNotification(_resourceService.GetResource("Customer.Mail.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            if (ids == null || !ids.Any())
            {
                return RedirectToAction("Index");
            }

            var models = _mailService.Gets(p => ids.Contains(p.MailId));
            if (models == null || !models.Any())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Mail.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Mail.NotExist"));
                return RedirectToAction("Index");
            }

            try
            {
                _mailService.Delete(models);
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Mail.Deleted"));
                SuccessNotification(_resourceService.GetResource("Customer.Mail.Deleted"));
            }
            catch (EgovException ex)
            {
                ErrorNotification(ex.Message);
                LogException(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    ErrorNotification(_resourceService.GetResource("Customer.Mail.NotPermission"));
            //    return RedirectToAction("Index", "Welcome");
            //}

            var model = _mailService.Get(id);
            if (model == null)
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Mail.NotExist"));
                ErrorNotification(_resourceService.GetResource("Customer.Mail.NotExist"));
                return RedirectToAction("Index");
            }

            return View(model.ToModel());
        }
    }
}
