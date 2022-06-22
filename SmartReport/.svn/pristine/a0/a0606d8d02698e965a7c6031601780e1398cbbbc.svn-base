using Bkav.eGovCloud.Business.Admin;
using Bkav.eGovCloud.Areas.Admin.Models;

using System.Runtime.InteropServices;
using System.Web.Mvc;
using System.Collections.Generic;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Core.Utils;
using System.Linq;
using System;
using System.Web;
using Bkav.eGovOnline.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [ComVisible(false)]
    public class AccountController : CustomController
    {
        private readonly AccountBll _accountService;
        private readonly UserBll _userService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly DomainBll _domainService;

        private const string DEFAULT_SORT_BY = "Username";

        public AccountController(AccountBll accountService,
            AdminGeneralSettings generalSettings, DomainBll domainService, UserBll userService)
        {
            _accountService = accountService;
            _generalSettings = generalSettings;
            _domainService = domainService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            var searchName = string.Empty;
            var domainId = ParseDomainId();
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchAccount];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    searchName = data["Search"] == null ? string.Empty : data["Search"].ToString();                    
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch
                {
                    isInvalidCookie = true;
                }
            }

            var domains = GetAllDomain(domainId);
            if (domainId == 0)
            {
                domainId = Int32.Parse(domains.First().Value);
            }

            int totalRecords;
            var model = _accountService.Gets(out totalRecords,
                pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                name: searchName,
                domainId: domainId,
                currentPage: sortAndPage.CurrentPage).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.SearchName = searchName;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.AllDomain = GetAllDomain(domainId);

            if (isInvalidCookie)
            {
                CreateCookieSearch(searchName, sortAndPage, domainId);
            }
            return View(model);
        }

        public ActionResult Search(int domainId, string searchName)
        {
            IEnumerable<AccountModel> model = null;

            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    int totalRecords;
                    model = _accountService.Gets(out totalRecords, pageSize: 100,
                            sortBy: DEFAULT_SORT_BY, isDescending: true,
                            name: searchName, domainId: domainId).ToListModel();

                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = 100,
                        CurrentPage = 1,
                        IsSortDescending = true,
                        SortBy = DEFAULT_SORT_BY,
                        TotalRecordCount = totalRecords
                    };

                    CreateCookieSearch(searchName, sortAndPage, domainId);
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.SearchName = searchName;

                    ViewBag.ListPageSize = _generalSettings.ListPageSize;
                    ViewBag.DomainId = domainId;
                }
            }
            ViewBag.AllDomain = GetAllDomain(domainId);
            return PartialView("PartialList", model);
        }

        public ActionResult SortAndPaging(string searchName, string sortBy,
            bool isSortDesc, int page, int pageSize, int domainId = 0)
        {
            IEnumerable<AccountModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                model = _accountService.Gets(out totalRecords,
                    sortBy: sortBy, currentPage: page, pageSize: pageSize,
                    isDescending: isSortDesc, name: searchName, domainId: domainId).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                CreateCookieSearch(searchName, sortAndPage, domainId);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Name = searchName;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                ViewBag.DomainId = domainId;
            }
            return PartialView("PartialList", model);
        }


        public ActionResult Edit(int id)
        {
            var account = _accountService.Get(id);
            if (account == null)
            {
                return RedirectToAction("Index");
            }

            var domainId = ParseDomainId();
            var domains = GetAllDomain(domainId);
            if (domainId == 0)
            {
                domainId = Int32.Parse(domains.First().Value);
            }

            var accDomainIds = account.AccountDomains.Select(a => a.DomainId);
            var model = account.ToModel();
            model.Domains = string.Join(",", accDomainIds);

            ViewBag.AllDomain = domains;
            ViewBag.AccountDomains = model.DomainIds;
            return View(account.ToModel());
        }

        [HttpPost]
        public ActionResult Edit(AccountModel model)
        {
            var account = _accountService.Get(model.AccountId);
            if (account == null)
            {
                return RedirectToAction("Index");
            }

            account.IsActivated = model.IsActivated;
            account.HasViewReport = model.HasViewReport;
            var domainIds = model.DomainIds;

            _accountService.UpdateWithDomain(account, account.UsernameEmailDomain, model.DomainIds);

            return RedirectToAction("Index");
        }

        private void CreateCookieSearch(string name, SortAndPagingModel sortpage, int? domainId = null)
        {
            var data = new Dictionary<string, object> { { "Search", name }, { "SortAndPaging", sortpage }, { "DomainId", domainId } };
            var cookie = Request.Cookies[CookieName.SearchAccount];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchAccount, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            Response.Cookies.Add(cookie);
        }

        private IEnumerable<SelectListItem> GetAllDomain(int domainId)
        {
            var userName = _userService.CurrentUser.UsernameEmailDomain;
            var domains = _domainService.GetsByUser(userName);

            return domains.Select(d => new SelectListItem
            {
                Value = d.DomainId.ToString(),
                Text = d.DomainName,
                Selected = d.DomainId == domainId
            });
        }

        private int ParseDomainId()
        {
            var result = 0;

            var httpCookie = Request.Cookies[CookieName.SearchAccount];
            if (httpCookie == null)
            {
                return result;
            }

            var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
            if (data["DomainId"] != null)
            {
                result = Convert.ToInt32(data["DomainId"].ToString());
            }

            return result;
        }
    }
}