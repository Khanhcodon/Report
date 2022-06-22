using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Admin;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    public class DocumentRelatedController : CustomController
    {
        private readonly DocumentRelatedBll _docRelatedService;
        private readonly ResourceBll _resourceService;
        private const string DEFAULT_SORT_BY = "Name";
        private readonly AdminGeneralSettings _generalSettings;
        public DocumentRelatedController(DocumentRelatedBll docRelatedService, ResourceBll resourceService, AdminGeneralSettings generalSettings)
        {
            _docRelatedService = docRelatedService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
        }

        #region Action

        public ActionResult Index()
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin,
                    _resourceService.GetResource("Customer.DocumentRelated.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.DocumentRelated.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };

            var search = string.Empty;
            var isInvalidCookie = false;
            var httpCookie = Request.Cookies[CookieName.SearchDocRelated];
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

            var model = _docRelatedService.GetSorts(sortAndPage.SortBy, sortAndPage.IsSortDescending).ToListModel();
            sortAndPage.TotalRecordCount = model.Count();
            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.DocRelatedKey = "";
            //var model = new DocumentRelatedModel { List = (_docRelatedService.Gets().ToListModel()).ToList() };
            return View(model);
        }
        private void CreateCookieSearch(string search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchDocRelated];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchDocRelated, data.StringifyJs())
                {
                    Expires = DateTime.Now.AddDays(1), Path = "/admin"
                };
            }

            Response.Cookies.Add(cookie);
        }
        public ActionResult SortAndPaging(string key,
            string sortBy, bool isSortDesc,
            int page, int pageSize)
        {
            IEnumerable<DocumentRelatedModel> model = null;
            SortAndPagingModel sortAndPage = null;
            if (Request.IsAjaxRequest())
            {
                model = _docRelatedService.GetSorts(sortBy, isSortDesc).ToListModel();
                sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy
                };
            }

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.DocRelatedKey = key;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            CreateCookieSearch(key, sortAndPage);
            return PartialView("_PartialList", model);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var reportQuery = _docRelatedService.Get(id);
                _docRelatedService.Delete(reportQuery);
            }
            catch (Exception ex)
            {
                LogException(ex);
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocumentRelated.Deleted.Error"));
            }

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocumentRelated.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.DocumentRelated.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var model = new DocumentRelatedModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DocumentRelatedModel model)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin,
                    _resourceService.GetResource("Customer.DocumentRelated.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.DocumentRelated.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var checkName = _docRelatedService.Gets(p => p.Name == model.Name);
                if (checkName.Any())
                {
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocumentRelatedName.Exist"));
                    CreateActivityLog(ActivityLogType.Admin,
                        _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocumentRelatedName.Exist"));
                    ModelState.AddModelError("Name",
                        _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocumentRelatedName.Exist"));
                    return View(model);
                }

                var docRelated = model.ToEntity();
                docRelated.CreatedBy = User.GetUserId();
                docRelated.CreatedAt = DateTime.Now;
                try
                {
                    _docRelatedService.Create(docRelated);

                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocumentRelated.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocumentRelated.Created"));
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.DocumentRelated.NotPermissionEdit"));
                ErrorNotification(_resourceService.GetResource("Customer.DocumentRelated.NotPermissionEdit"));
                return RedirectToAction("Index");
            }

            var docRelated = _docRelatedService.Get(id);
            if (docRelated == null)
            {
                ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocumentRelated.NotExist"));
                return RedirectToAction("Index");
            }

            return View(docRelated.ToModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(DocumentRelatedModel model)
        {
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin,
                    _resourceService.GetResource("Customer.DocumentRelated.NotPermissionEdit"));
                ErrorNotification(_resourceService.GetResource("Customer.DocumentRelated.NotPermissionEdit"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var docRelated = _docRelatedService.Get(model.DocumentRelatedId);
                if (docRelated == null)
                {
                    ErrorNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.DocumentRelated.NotExist"));
                    return RedirectToAction("Index");
                }

                docRelated = model.ToEntity(docRelated);
                docRelated.UpdatedAt = DateTime.Now;
                docRelated.UpdatedBy = User.GetUserId();
                _docRelatedService.Update(docRelated);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        #endregion
    }
}