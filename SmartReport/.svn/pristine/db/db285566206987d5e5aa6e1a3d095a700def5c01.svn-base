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

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    public class FormRelationController: CustomController  
    {
        private readonly FormRelationBll _formRelationService;
        private readonly FormBll _formService;

        private readonly AdminGeneralSettings _generalSettings;
        private readonly ResourceBll _resourceService;

        private const string DEFAULT_SORT_BY = "CreatedOnDate";

        public FormRelationController (
            FormRelationBll formRelationService,
            FormBll formService,
            AdminGeneralSettings generalSettings,
			ResourceBll resourceService
            ) : base()
        {
            _formRelationService = formRelationService;
            _formService = formService;

            _generalSettings = generalSettings;
            _resourceService = resourceService;
        }

        public ActionResult Index()
        {
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.FormRelation.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.FormRelation.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };

            string search = "";
            var httpCookie = Request.Cookies[CookieName.SearchFormRelation];

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

            int totalRecords;
            var models = _formRelationService.GetsAs(out totalRecords,
                f => new FormRelationModel
                {
                    RelationId = f.RelationId,
                    RelationName = f.RelationName,
                    FromFormId = f.FromFormId,
                    ToFormId = f.ToFormId,
                    Json = f.Json,
                }, sortAndPage.CurrentPage,
                sortAndPage.PageSize,
                sortAndPage.SortBy,
                sortAndPage.IsSortDescending,
                search);
            sortAndPage.TotalRecordCount = totalRecords;

            foreach (FormRelationModel f in models)
            {
                f.FromForm = GetForm(f.FromFormId);
                f.ToForm = GetForm(f.ToFormId);
            }

            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            ViewBag.Search = search;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;

            return View(models);
        }

        public ActionResult Create()
        {
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.FormRelation.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.FormRelation.NotPermissionCreate"));
                return RedirectToAction("Index", "Welcome");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormRelation formRelation)
        {
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.FormRelation.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.FormRelation.NotPermissionCreate"));
                return RedirectToAction("Index", "Welcome");
            }
            _formRelationService.Create(formRelation);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.FormRelation.NotPermissionEdit"));
                ErrorNotification(_resourceService.GetResource("Customer.FormRelation.NotPermissionEdit"));
                return RedirectToAction("Index", "Welcome");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FormRelation formRelation)
        {
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.FormRelation.NotPermissionEdit"));
                ErrorNotification(_resourceService.GetResource("Customer.FormRelation.NotPermissionEdit"));
                return RedirectToAction("Index", "Welcome");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.FormRelation.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.FormRelation.NotPermissionDelete"));
                return RedirectToAction("Index", "Welcome");
            }
            var formRelation = _formRelationService.Get(id);
            if (formRelation != null)
            {
                try
                {
                    _formRelationService.Detele(formRelation);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }
        

        #region Support
        private void CreateCookieSearch(string search, SortAndPagingModel sortAndPage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortAndPage } };
            HttpCookie cookie = Request.Cookies[CookieName.SearchForm];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchForm, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Support: Hộ trợ tìm kiếm và phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên mỗi trang</param>
        /// <param name="page">lấy dữ liệu của page thứ n</param>
        /// <param name="isSortDescending">Sắp xếp</param>
        /// <param name="sortBy">Sắp xếp theo tiêu chí</param>
        /// <param name="search">Keyword tìm kiếm</param>
        /// <returns></returns>
        public ActionResult SortAndPaging
            (int pageSize,
            int page,
            bool isSortDescending,
            string sortBy,
            string search)
        {
            int totalRecords;
            var models = _formRelationService.GetsAs(out totalRecords,
                f => new FormRelationModel
                {
                    RelationId = f.RelationId,
                    RelationName = f.RelationName,
                    FromFormId = f.FromFormId,
                    ToFormId = f.ToFormId,
                    Json = f.Json,
                }, page,
                pageSize,
                sortBy,
                isSortDescending,
                search);

            foreach (FormRelationModel f in models)
            {
                f.FromForm = GetForm(f.FromFormId);
                f.ToForm = GetForm(f.ToFormId);
            }

            var sortAndPage = new SortAndPagingModel
            {
                PageSize = pageSize,
                CurrentPage = page,
                IsSortDescending = isSortDescending,
                SortBy = sortBy,
                TotalRecordCount = totalRecords
            };

            CreateCookieSearch(search, sortAndPage);

            ViewBag.Search = search;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;

            return PartialView("_PartialList", models);
        }

        /// <summary>
        /// Support: Hỗ trợ lấy biểu mẫu nguồn và biểu mẫu đích
        /// </summary>
        /// <param name="id">id của biểu mẫu (FormRelation: FromFormID và ToFormId)</param>
        /// <returns></returns>
        private Form GetForm (Guid id)
        {
            return _formService.Get(id);
        }
        #endregion
    }
}