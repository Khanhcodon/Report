using System;
using System.Collections.Generic;
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
    public class CityController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly CityBll _cityService;
        private readonly ResourceBll _resourceService;
        private const string DEFAULT_SORT_BY = "CityName";

        public CityController(CityBll cityService,
                            ResourceBll resourceService,
                            AdminGeneralSettings generalSettings)
            : base()
        {
            _cityService = cityService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.City.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.City.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var cityName = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchCity];
            var isInvalidCookie = false;
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    cityName = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }
            int totalRecords;
            var model = _cityService.Gets(out totalRecords,
                pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                currentPage: sortAndPage.CurrentPage,
                isDescending: sortAndPage.IsSortDescending,
                cityname: cityName).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.CityName = cityName;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;

            if (isInvalidCookie)
            {
                CreateCookieSearch(cityName, sortAndPage);
            }
            return View(model);
        }

        public ActionResult Create()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.City.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.City.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            return View(new CityModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "CityCreate")]
        public ActionResult Create(CityModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.City.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.City.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var city = model.ToEntity();
                try
                {
                    _cityService.Create(city);
                    CreateActivityLog(ActivityLogType.Admin,_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.City.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.City.Created"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
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
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.City.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.City.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var city = _cityService.Get(id);
            if (city == null)
            {
                return RedirectToAction("Index");
            }
            var model = city.ToModel();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "CityEdit")]
        public ActionResult Edit(CityModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.City.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.City.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var city = _cityService.Get(model.CityId);
                if (city == null)
                {
                    return RedirectToAction("Index");
                }
                var oldCityName = city.CityName;
                try
                {
                    _cityService.Update(model.ToEntity(city), oldCityName);
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    return View(model);
                }
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.City.Updated"));
                SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.City.Updated"));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "CityDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.City.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.City.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var city = _cityService.Get(id);
            if (city != null)
            {
                try
                {
                    _cityService.Delete(city);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.City.Deleted"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.City.Deleted"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Search(string cityName, int pageSize)
        {
            IEnumerable<CityModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {


                    int totalRecords;
                    model = _cityService.Gets(out totalRecords,
                        pageSize: pageSize,
                        sortBy: DEFAULT_SORT_BY,
                        isDescending: false,
                        cityname: cityName).ToListModel();

                    var sortAndPage = new SortAndPagingModel
                                       {
                                           PageSize = pageSize,
                                           CurrentPage = 1,
                                           SortBy = DEFAULT_SORT_BY,
                                           IsSortDescending = false,
                                           TotalRecordCount = totalRecords
                                       };
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.CityName = cityName;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;

                    CreateCookieSearch(cityName, sortAndPage);
                }
            }

            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(
            string cityName, string sortBy,
            bool isSortDesc, int page, int pageSize)
        {
            IEnumerable<CityModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                model = _cityService.Gets(out totalRecords,
                    pageSize: pageSize,
                    sortBy: sortBy,
                    isDescending: isSortDesc,
                    cityname: cityName,
                    currentPage: page).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };

                ViewBag.SortAndPage = sortAndPage;
                ViewBag.CityName = cityName;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;

                CreateCookieSearch(cityName, sortAndPage);
            }

            return PartialView("_PartialList", model);
        }

        private void CreateCookieSearch(string cityName, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", cityName }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchCity];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchCity, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
                cookie.Path = "/admin";
            }
            Response.Cookies.Add(cookie);
        }
    }
}
