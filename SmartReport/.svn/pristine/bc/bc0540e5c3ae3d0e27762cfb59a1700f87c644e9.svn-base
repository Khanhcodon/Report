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
    public class DistrictController : CustomController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly DistrictBll _districtService;
        private readonly CityBll _cityService;
        private readonly ResourceBll _resourceService;
        private const string DEFAULT_SORT_BY = "DistrictName";

        public DistrictController(DistrictBll districtService,
                                  CityBll cityService,
                                  ResourceBll resourceService,
                                  AdminGeneralSettings generalSettings)
            : base()
        {
            _districtService = districtService;
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
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.District.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.District.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var cityCode = string.Empty;
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchDistrict];
            var isInvalidCookie = false;

            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    cityCode = data["Search"] == null ? string.Empty : data["Search"].ToString();
                    sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    isInvalidCookie = true;
                }
            }

            int totalRecords;
            var model = _districtService.Gets(out totalRecords,
                pageSize: sortAndPage.PageSize,
                sortBy: sortAndPage.SortBy,
                isDescending: sortAndPage.IsSortDescending,
                currentPage: sortAndPage.CurrentPage,
                cityCode: cityCode).ToListModel();

            sortAndPage.TotalRecordCount = totalRecords;

            BindCityDropdown(cityCode);
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;

            if (isInvalidCookie)
            {
                CreateCookieSearch(cityCode, sortAndPage);
            }
            return View(model);
        }

        public ActionResult Create(string cityCode)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.District.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.District.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            var httpCookie = Request.Cookies[CookieName.SearchDistrict];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    cityCode = data["Search"] == null ? string.Empty : data["Search"].ToString();
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    BindCityDropdown();
                }
            }
            BindCityDropdown(cityCode);
            return View(new DistrictModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "DistrictCreate")]
        public ActionResult Create(DistrictModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.District.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.District.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var district = model.ToEntity();
                try
                {
                    _districtService.Create(district);
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.District.Created"));
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.District.Created"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    CreateActivityLog(ActivityLogType.Admin, ex.Message);
                    ErrorNotification(ex.Message);
                    BindCityDropdown(model.CityCode);
                    return View(model);
                }
                ViewData = null;
                BindCityDropdown(model.CityCode);
                return View(new DistrictModel
                {
                    DistrictName = "",
                    DistrictCode = ""
                });
            }
            BindCityDropdown(model.CityCode);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.District.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.District.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var district = _districtService.Get(id);
            if (district == null)
            {
                return RedirectToAction("Index");
            }
            var model = district.ToModel();
            BindCityDropdown(model.CityCode);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "DistrictEdit")]
        public ActionResult Edit(DistrictModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.District.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.District.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var district = _districtService.Get(model.DistrictId);
                if (district != null)
                {
                    var oldDistrictName = district.DistrictName;
                    try
                    {
                        _districtService.Update(model.ToEntity(district), oldDistrictName);
                    }
                    catch (EgovException ex)
                    {
                        LogException(ex);
                        ErrorNotification(ex.Message);
                        BindCityDropdown(model.CityCode);
                        return View(model);
                    }
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.District.Updated"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.District.Updated"));
                }
                return RedirectToAction("Index");
            }
            BindCityDropdown(model.CityCode);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "DistrictDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.District.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.District.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var district = _districtService.Get(id);
            if (district != null)
            {
                try
                {
                    _districtService.Delete(district);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.District.Deleted"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.District.Deleted"));
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

        public ActionResult GetByCity(string cityCode)
        {
            IEnumerable<DistrictModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    var sortAndPage = new SortAndPagingModel
                    {
                        PageSize = _generalSettings.DefaultPageSize,
                        CurrentPage = 1,
                        SortBy = DEFAULT_SORT_BY,
                        IsSortDescending = false,
                    };

                    if (!string.IsNullOrWhiteSpace(cityCode))
                    {
                        cityCode = cityCode.Trim();
                    }

                    int totalRecords;
                    model = _districtService.Gets(out totalRecords, pageSize: sortAndPage.PageSize,
                                                                sortBy: DEFAULT_SORT_BY,
                                                                isDescending: sortAndPage.IsSortDescending,
                                                                cityCode: cityCode).ToListModel();

                    sortAndPage.TotalRecordCount = totalRecords;
                    ViewBag.SortAndPage = sortAndPage;
                    ViewBag.ListPageSize = _generalSettings.ListPageSize;

                    CreateCookieSearch(cityCode, sortAndPage);
                }
            }

            return PartialView("_PartialList", model);
        }

        public ActionResult SortAndPaging(string cityCode,
            string sortBy, bool isSortDesc,
            int page, int pageSize)
        {
            IEnumerable<DistrictModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                model = _districtService.Gets(out totalRecords,
                    pageSize: pageSize,
                    sortBy: sortBy,
                    isDescending: isSortDesc,
                    cityCode: cityCode,
                    currentPage: page).ToListModel();

                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };

                ViewBag.AllCity = _cityService.Gets().ToListModel();
                ViewBag.Selected = cityCode;
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                CreateCookieSearch(cityCode, sortAndPage);
            }

            return PartialView("_PartialList", model);
        }

        private void CreateCookieSearch(string cityCode, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", cityCode }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchDistrict];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "/admin";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchDistrict, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
                cookie.Path = "/admin";
            }
            Response.Cookies.Add(cookie);
        }

        private void BindCityDropdown(string selected = "")
        {
            ViewBag.CityCode =
                _cityService.GetsAs(
                    c => new { c.CityCode, c.CityName })
                    .Select(
                        c =>
                            new SelectListItem
                            {
                                Selected = selected == c.CityCode,
                                Text = c.CityName,
                                Value = c.CityCode
                            });
        }

        public void GetCookie()
        {
            var allcity = _cityService.Gets().ToListModel();
            var cityCode = string.Empty;
            var httpCookie = Request.Cookies[CookieName.SearchDistrict];
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                cityCode = data["Search"] == null ? string.Empty : data["Search"].ToString();
            }
            ViewBag.AllCity = allcity;
            ViewBag.Selected = cityCode;
        }
    }
}
