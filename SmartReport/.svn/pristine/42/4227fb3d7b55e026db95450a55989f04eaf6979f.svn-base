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
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class RequiredSupplementaryController : CustomController
    {
        private const string DEFAULT_SORT_BY = "Name";
        private readonly SupplementaryBll _supplementaryService;
        private readonly DocTypeBll _doctypeService;
        private readonly ResourceBll _resourceService;
        private readonly DocFieldBll _docfieldService;

        public RequiredSupplementaryController(
            SupplementaryBll supplementaryService,
            DocTypeBll doctypeService,
            ResourceBll resourceService,
            DocFieldBll docfieldService)
            : base()
        {
            _resourceService = resourceService;
            _supplementaryService = supplementaryService;
            _doctypeService = doctypeService;
            _docfieldService = docfieldService;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Supplementary.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Supplementary.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var search = new RequiredSupplementarySearchModel();
            var sortAndPage = new SortAndPagingModel
            {
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };

            var httpCookie = Request.Cookies[CookieName.SearchRequiredSupplementary];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<RequiredSupplementarySearchModel>(data["Search"].ToString());
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception)
                {
                    isInvalidCookie = true;
                }
            }

            var model = GetRequiredSupplementaryModels(search, sortAndPage);

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.Search = search;
            BindData();

            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            return View(model);
        }

        public ActionResult Search(RequiredSupplementarySearchModel search)
        {
            IEnumerable<RequiredSupplementaryModel> model = null;
            if (Request.IsAjaxRequest())
            {
                var sortAndPage = new SortAndPagingModel
                {
                    IsSortDescending = false,
                    SortBy = DEFAULT_SORT_BY
                };
                if (ModelState.IsValid)
                {
                    model = GetRequiredSupplementaryModels(search, sortAndPage);
                    CreateCookieSearch(search, sortAndPage);
                }
                ViewBag.SortAndPage = sortAndPage;
            }
            ViewBag.Search = search;
            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(RequiredSupplementarySearchModel search,
                                                string sortBy,
                                                bool isSortDesc)
        {
            IEnumerable<RequiredSupplementaryModel> model = null;
            if (Request.IsAjaxRequest())
            {
                var sortAndPage = new SortAndPagingModel
                {
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                };

                model = GetRequiredSupplementaryModels(search, sortAndPage);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.Search = search;
                CreateCookieSearch(search, sortAndPage);
            }

            return PartialView("_PartialList", model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.RequiredSupplementary.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.RequiredSupplementary.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            var search = new RequiredSupplementarySearchModel();
            var httpCookie = Request.Cookies[CookieName.SearchRequiredSupplementary];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    search = Json2.ParseAs<RequiredSupplementarySearchModel>(data["Search"].ToString());
                }
                catch (Exception)
                {
                    return View(new RequiredSupplementaryModel());
                }
            }
            BindData();
            return View(new RequiredSupplementaryModel { DocFieldId = search.DocFieldId, DocTypeId = search.DocTypeId });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "RequiredSupplementaryCreate")]
        public ActionResult Create(RequiredSupplementaryModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.RequiredSupplementary.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.RequiredSupplementary.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();
                try
                {
                    _supplementaryService.CreateRequired(entity);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.RequiredSupplementary.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.RequiredSupplementary.Created"));
                    ViewData = null;
                    BindData();
                    return View(new RequiredSupplementaryModel
                    {
                        DocFieldId = model.DocFieldId,
                        DocTypeId = model.DocTypeId
                    });
                }
                catch (EgovException ex)
                {
                    ErrorNotification(ex.Message);
                    BindData();
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
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.RequiredSupplementary.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.RequiredSupplementary.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var supplementary = _supplementaryService.GetRequired(id);
            if (supplementary == null)
            {
                return RedirectToAction("Index");
            }
            var model = supplementary.ToModel();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "RequiredSupplementaryEdit")]
        public ActionResult Edit(RequiredSupplementaryModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.RequiredSupplementary.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.RequiredSupplementary.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var supp = _supplementaryService.GetRequired(model.RequiredSupplementaryId);
                if (supp == null)
                {
                    return RedirectToAction("Index");
                }
                supp = model.ToEntity(supp);
                try
                {
                    _supplementaryService.UpdateRequired(supp);
                }
                catch (EgovException ex)
                {
                    ErrorNotification(ex.Message);
                    return View(model);
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.RequiredSupplementary.Updated"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.RequiredSupplementary.Updated"));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "RequiredSupplementaryDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.RequiredSupplementary.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.RequiredSupplementary.NotPermissionDelete"));
                return RedirectToAction("Index");
            }
            var supp = _supplementaryService.GetRequired(id);
            if (supp != null)
            {
                try
                {
                    _supplementaryService.DeleteRequired(supp);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.RequiredSupplementary.Deleted"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.RequiredSupplementary.Deleted"));
                }
                catch (EgovException ex)
                {
                    ErrorNotification(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        #region Private Method

        private void CreateCookieSearch(RequiredSupplementarySearchModel search, SortAndPagingModel sortAndPage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortAndPage } };
            HttpCookie cookie = Request.Cookies[CookieName.SearchRequiredSupplementary];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchRequiredSupplementary, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }

            cookie.Path = "/admin";
            Response.Cookies.Add(cookie);
        }

        private string GetAllDocFields()
        {
            return
                _docfieldService.GetsAs(d => new { d.DocFieldId, d.DocFieldName, d.CategoryBusinessId })
                    .OrderBy(d => d.DocFieldName)
                    .StringifyJs();
        }

        private string GetAllDocTypes()
        {
            return
                _doctypeService.GetsAs(d => new { d.DocTypeId, d.DocTypeName, d.DocFieldId, d.CategoryBusinessId })
                    .OrderBy(d => d.DocTypeName)
                    .StringifyJs();
        }

        private IEnumerable<RequiredSupplementaryModel> GetRequiredSupplementaryModels(RequiredSupplementarySearchModel search, SortAndPagingModel sortAndPagingModel)
        {
            return
                _supplementaryService.GetsAs(f => new
                {
                    f.RequiredSupplementaryId,
                    f.Name,
                    f.DocTypeId,
                    f.DocFieldId,
                    f.UserId
                }, search.DocTypeId).Select(
                    f =>
                        new RequiredSupplementaryModel
                        {
                            RequiredSupplementaryId = f.RequiredSupplementaryId,
                            Name = f.Name,
                            DocTypeId = f.DocTypeId,
                            DocFieldId = f.DocFieldId,
                            UserId = f.UserId
                        });
        }

        private void BindData()
        {
            ViewBag.AllDocFields = GetAllDocFields();
            ViewBag.AllDocTypes = GetAllDocTypes();
        }

        #endregion
    }
}
