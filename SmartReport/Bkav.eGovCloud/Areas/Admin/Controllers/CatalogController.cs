using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class CatalogController : CustomController
    {
        private readonly CatalogBll _catalogService;
        private readonly ResourceBll _resourceService;
        private readonly CatalogValueBll _catalogValueService;
        private readonly AdminGeneralSettings _generalSettings;
        private const string DEFAULT_SORT_BY = "CatalogName";

        public CatalogController(
            CatalogBll catalogBll,
            ResourceBll resourceBll,
            CatalogValueBll catalogValueBll,
            AdminGeneralSettings generalSettings)
            : base()
        {
            _catalogService = catalogBll;
            _resourceService = resourceBll;
            _catalogValueService = catalogValueBll;
            _generalSettings = generalSettings;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Catalog.NotPermission"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Catalog.NotPermission"));
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
            var httpCookie = Request.Cookies[CookieName.SearchCatalog];
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

            var model = _catalogService.Gets(sortAndPage.SortBy, sortAndPage.IsSortDescending).ToListModel();
            sortAndPage.TotalRecordCount = model.Count();
            if (isInvalidCookie)
            {
                CreateCookieSearch(search, sortAndPage);
            }

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ResourceKey = "";
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            return View(model);
        }

        private void CreateCookieSearch(string search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchCatalog];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchCatalog, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
                cookie.Path = "/admin";
            }

            Response.Cookies.Add(cookie);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Catalog.NotPermissionCreate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Catalog.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            ViewBag.CatalogValues = new List<CatalogValueModel>().StringifyJs();
            return View(new CatalogModel());
        }

        [HttpPost]
        public ActionResult Create(CatalogModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Catalog.NotPermissionCreate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Catalog.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.ToEntity();
                    var order = 1;
                    //entity.CatalogId = Guid.NewGuid();
                    //foreach (var obj in model.ListCatalogV)
                    //{
                    //    entity.CatalogValues.Add(new CatalogValue
                    //    {
                    //        Value = obj.Value,
                    //        Order = order++,
                    //        CatalogValueId = Guid.NewGuid(),
                    //        CatalogKey = obj.CatalogKey,
                    //        CatalogId = entity.CatalogId
                    //    });
                    //}
                    _catalogService.Create(entity);
                    var mess = string.Format(_resourceService.GetResource("Common.Create.Success"),
                        _resourceService.GetResource("Common.Catalog.Name"));
                    SuccessNotification(mess);
                    CreateActivityLog(ActivityLogType.Admin, mess);
                    return View(new CatalogModel { CatalogName = string.Empty });
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }

            ViewBag.CatalogValues = model.ValueNames.Select(v => new CatalogValueModel { Value = v.ToString() }).ToList().StringifyJs();
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Catalog.NotPermissionUpdate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Catalog.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var catalogId = Guid.Parse(id);
            var catalog = _catalogService.Get(catalogId);
            if (catalog == null)
            {
                ErrorNotification(_resourceService.GetResource("Custommer.Catalog.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Custommer.Catalog.NotExist"));
                return RedirectToAction("Index");
            }

            ViewBag.CatalogValues = _catalogValueService.Gets(catalogId).StringifyJs();
            catalog.CatalogKey = "";
            return View(catalog.ToModel());
        }

        [HttpPost]
        public ActionResult Edit(CatalogModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Catalog.NotPermissionUpdate"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Catalog.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var catalog = _catalogService.Get(model.CatalogId);
                    if (catalog == null)
                    {
                        ErrorNotification(_resourceService.GetResource("Custommer.Catalog.NotExist"));
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Custommer.Catalog.NotExist"));
                        return RedirectToAction("Index");
                    }
                    catalog = model.ToEntity(catalog);
                    //catalog.CatalogValues = new List<CatalogValue>();
                    //foreach (var obj in model.ListCatalogV)
                    //{
                    //    catalog.CatalogValues.Add(new CatalogValue
                    //    {
                    //        Value = obj.Value,
                    //        Order = obj.Order,
                    //        CatalogValueId = model.CatalogValueIds[obj.Order ?? 0],
                    //        CatalogKey = obj.CatalogKey,
                    //        CatalogId = catalog.CatalogId
                    //    });
                    //}
                    _catalogService.Update(catalog, model.CatalogValueIds, model.ValueNames);
                    var mess = string.Format(_resourceService.GetResource("Common.Edit.Success"), _resourceService.GetResource("Common.Catalog.Name"));
                    SuccessNotification(mess);
                    CreateActivityLog(ActivityLogType.Admin, mess);
                    return RedirectToAction("Index");
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                }
            }
            ViewBag.CatalogValues = model.ValueNames
                                    .Select(v => new CatalogValueModel
                                    {
                                        Value = v.ToString(),
                                        Catalogkey = "aaa"
                                    }).ToList().StringifyJs();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                ErrorNotification(_resourceService.GetResource("Customer.Catalog.NotPermissionDelete"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Catalog.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var catalog = _catalogService.Get(Guid.Parse(id));
            if (catalog == null)
            {
                ErrorNotification(_resourceService.GetResource("Custommer.Catalog.NotExist"));
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Custommer.Catalog.NotExist"));
            }
            else
            {
                try
                {
                    _catalogService.Detele(catalog);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeValue(Guid id, string value)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Catalog.NotPermissionChangeValue"));
            //    return Json(new { Data = id, msg = _resourceService.GetResource("Customer.Catalog.NotPermissionChangeValue") });
            //}

            var catalogValue = _catalogValueService.Get(id);
            if (catalogValue == null)
                return new JsonResult { Data = id };
            catalogValue.Value = value;
            _catalogValueService.Update(catalogValue);
            return new JsonResult { Data = id };
        }

        [HttpPost]
        public JsonResult ChangeValueKey(Guid id, string value)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            //if (!HasPermission())
            //{
            //    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Catalog.NotPermissionChangeValue"));
            //    return Json(new { Data = id, msg = _resourceService.GetResource("Customer.Catalog.NotPermissionChangeValue") });
            //}

            var catalogValue = _catalogValueService.Get(id);
            if(catalogValue == null)
                return new JsonResult { Data = id };
            catalogValue.CatalogKey = value;
            _catalogValueService.Update(catalogValue);
            return new JsonResult { Data = id };
        }

        [HttpPost]
        public JsonResult DeleteValue(Guid id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Catalog.NotPermissionDelete"));
                return Json(new { Data = id, msg = _resourceService.GetResource("Customer.Catalog.NotPermissionDelete") });
            }

            var catalogValue = _catalogValueService.Get(id);
            _catalogValueService.Delete(catalogValue);
            return new JsonResult { Data = id };
        }

        public ActionResult SortAndPaging(string catalogKey,
            string sortBy, bool isSortDesc,
            int page, int pageSize)
        {
            IEnumerable<CatalogModel> model = null;
            SortAndPagingModel sortAndPage = null;
            if (Request.IsAjaxRequest())
            {
                model = _catalogService.Gets(sortBy, isSortDesc).ToListModel();
                sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy
                };
            }

            ViewBag.SortAndPage = sortAndPage;
            ViewBag.CatalogKey = catalogKey;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            CreateCookieSearch(catalogKey, sortAndPage);
            return PartialView("_PartialList", model);
        }
    }
}
