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
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class KeyWordController : CustomController// BaseController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly KeyWordBll _keywordService;
        private readonly ResourceBll _resourceService;
        private const string DEFAULT_SORT_BY = "KeyWordName";

        public KeyWordController(
            KeyWordBll keywordService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings)
            : base()
        {
            _keywordService = keywordService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.KeyWord.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.KeyWord.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var keywordname = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchKeyWord];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    keywordname = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }
            int totalRecords;
            var model = _keywordService.Gets(out totalRecords, pageSize: sortAndPage.PageSize,
                                                                sortBy: sortAndPage.SortBy, isDescending: sortAndPage.IsSortDescending,
                                                                keywordname: keywordname).ToListModel();
            sortAndPage.TotalRecordCount = totalRecords;
            if (isInvalidCookie)
            {
                CreateCookieSearch(keywordname, sortAndPage);
            }
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.KeyWordName = keywordname;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;

            return View(model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.KeyWord.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.KeyWord.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            return View(new KeyWordModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt="KeyWordCreate")]
        public ActionResult Create(KeyWordModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.KeyWord.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.KeyWord.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var keyWord = model.ToEntity();
                try
                {
                    if (model.HasCreatePacket)
                    {
                        var names = model.KeyWordName.Split(';').Distinct();
                        var list = new List<KeyWord>();
                        foreach (var name in names)
                        {
                            var item = keyWord.Clone();
                            item.KeyWordName = name;
                            list.Add(item);
                        }
                        _keywordService.Create(list, model.IgnoreExist);
                    }
                    else
                    {
                        _keywordService.Create(keyWord);
                    }

                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.KeyWord.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.KeyWord.Created"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
                return RedirectToAction("Create");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.KeyWord.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.KeyWord.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var keyWord = _keywordService.Get(id);
            if (keyWord == null)
            {
                return RedirectToAction("Index");
            }
            var model = keyWord.ToModel();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "KeyWordEdit")]
        public ActionResult Edit(KeyWordModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.KeyWord.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.KeyWord.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var keyWord = _keywordService.Get(model.KeyWordId);
                if (keyWord != null)
                {
                    var oldKeyWordName = keyWord.KeyWordName;
                    try
                    {
                        _keywordService.Update(model.ToEntity(keyWord), oldKeyWordName);
                    }
                    catch (EgovException ex)
                    {
                        LogException(ex);
                        ErrorNotification(ex.Message);
                        return View(model);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.KeyWord.Updated"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.KeyWord.Updated"));
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "KeyWordDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.KeyWord.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.KeyWord.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var keyWord = _keywordService.Get(id);
            if (keyWord != null)
            {
                try
                {
                    _keywordService.Delete(keyWord);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.KeyWord.Deleted"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.KeyWord.Deleted"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Search(string keywordName, int pageSize)
        {
            IEnumerable<KeyWordModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = pageSize,
                        CurrentPage = 1,
                        SortBy = DEFAULT_SORT_BY
                    };
                    var httpCookie = Request.Cookies[CookieName.SearchKeyWord];
                    if (httpCookie != null)
                    {
                        var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                        sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                    }
                    if (!string.IsNullOrWhiteSpace(keywordName))
                    {
                        keywordName = keywordName.Trim();
                    }
                    int totalRecords;
                    model = _keywordService.Gets(out totalRecords, pageSize: pageSize,
                                                                sortBy: DEFAULT_SORT_BY, isDescending: sortAndPage.IsSortDescending,
                                                                keywordname: keywordName).ToListModel();
                    sortAndPage.TotalRecordCount = totalRecords;
                    CreateCookieSearch(keywordName, sortAndPage);
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.KeyWordName = keywordName;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;
                }
            }

            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(
            string keywordName,
            string sortBy,
            bool isSortDesc,
            int page,
            int pageSize)
        {
            IEnumerable<KeyWordModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrWhiteSpace(keywordName))
                {
                    keywordName = keywordName.Trim();
                }
                int totalRecords;
                model = _keywordService.Gets(out totalRecords, pageSize: pageSize,
                                                            sortBy: sortBy, isDescending: isSortDesc,
                                                            keywordname: keywordName, currentPage: page).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                CreateCookieSearch(keywordName, sortAndPage);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.KeyWordName = keywordName;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
            }

            return PartialView("_PartialList", model);
        }

        private void CreateCookieSearch(string keywordName, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", keywordName }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchKeyWord];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchKeyWord, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }
    }
}
