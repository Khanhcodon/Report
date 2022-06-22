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
    public class WardController : CustomController//BaseController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly CityBll _cityService;
        private readonly DistrictBll _districtService;
        private readonly WardBll _wardService;
        private readonly ResourceBll _resourceService;
        private const string DEFAULT_SORT_BY = "WardName";

        public WardController(
            DistrictBll districtService,
            CityBll cityService,
            WardBll wardService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings)
            : base()
        {
            _districtService = districtService;
            _cityService = cityService;
            _wardService = wardService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
        }

        public ActionResult Index()
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Ward.NotPermission"));
                ErrorNotification(_resourceService.GetResource("Customer.Ward.NotPermission"));
                return RedirectToAction("Index", "Welcome");
            }

            var model = BindIndex(null, _generalSettings.DefaultPageSize);
            return View(model);
        }

        public ActionResult Create(string districtCode)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Ward.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Ward.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            WardSearchModel search = null;
            if (!string.IsNullOrEmpty(districtCode))
            {
                var cityCode = _districtService.Get(districtCode).CityCode;
                search = new WardSearchModel
                             {
                                 CityCode = cityCode,
                                 DistrictCode = districtCode
                             };
            }
            BindCreate(search);
            return View(new WardModel());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "WardCreate")]
        public ActionResult Create(WardModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Ward.NotPermissionCreate"));
                ErrorNotification(_resourceService.GetResource("Customer.Ward.NotPermissionCreate"));
                return RedirectToAction("Index");
            }

            WardSearchModel search = null;
            if (ModelState.IsValid)
            {
                var ward = model.ToEntity();
                try
                {
                    _wardService.Create(ward);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Ward.Created"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Ward.Created"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                    BindCreate(search);
                    return View(model);
                }
                ViewData = null;
                BindCreate(search);
                return View(new WardModel
                {
                    WardName = ""
                });
            }
            BindCreate(search);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Ward.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Ward.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            var ward = _wardService.Get(id);
            if (ward == null)
            {
                return RedirectToAction("Index");
            }
            var model = ward.ToModel();
            WardSearchModel search = null;
            BindCreate(search);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "WardEdit")]
        public ActionResult Edit(WardModel model)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Ward.NotPermissionUpdate"));
                ErrorNotification(_resourceService.GetResource("Customer.Ward.NotPermissionUpdate"));
                return RedirectToAction("Index");
            }

            WardSearchModel search = null;
            if (ModelState.IsValid)
            {
                var ward = _wardService.Get(model.WardId);
                if (ward != null)
                {
                    var oldWardName = ward.WardName;
                    try
                    {
                        model.DistrictCode = ward.DistrictCode;
                        _wardService.Update(model.ToEntity(ward), oldWardName);
                        CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Ward.Updated"));
                        SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Ward.Updated"));
                        return RedirectToAction("Index");
                    }
                    catch (EgovException ex)
                    {
                        LogException(ex);
                        ErrorNotification(ex.Message);
                        BindCreate(search);
                        return View(model);
                    }
                }
                CreateActivityLog(ActivityLogType.Admin, "Xã/Phường không tồn tại!.");
                ErrorNotification("Xã/Phường không tồn tại!.");
                return RedirectToAction("Index");
            }
            BindCreate(search);
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken(Salt = "WardDelete")]
        public ActionResult Delete(int id)
        {
            //Hopcv: 190614
            //Kiểm tra quyền
            if (!HasPermission())
            {
                CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Customer.Ward.NotPermissionDelete"));
                ErrorNotification(_resourceService.GetResource("Customer.Ward.NotPermissionDelete"));
                return RedirectToAction("Index");
            }

            var ward = _wardService.Get(id);
            if (ward != null)
            {
                try
                {
                    _wardService.Delete(ward);
                    CreateActivityLog(ActivityLogType.Admin, _resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Ward.Deleted"));
                    SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Ward.Deleted"));
                }
                catch (EgovException ex)
                {
                    LogException(ex);
                    ErrorNotification(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Search(WardSearchModel search, int pageSize)
        {
            if (Request.IsAjaxRequest())
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index");
                }
                var model = BindIndex(search, pageSize);

                return PartialView("_PartialList", model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult SortAndPaging(string districtCode,
                                        string cityCode,
                                        string sortBy,
                                        bool isSortDesc,
                                        int page,
                                        int pageSize)
        {
            IEnumerable<WardModel> model = null;
            if (Request.IsAjaxRequest())
            {
                int totalRecords;
                model = _wardService.Gets(out totalRecords, pageSize: pageSize,
                                                            sortBy: sortBy, isDescending: isSortDesc,
                                                            districtCode: districtCode, currentPage: page).ToListModel();
                var sortAndPage = new SortAndPagingModel
                {
                    PageSize = pageSize,
                    CurrentPage = page,
                    IsSortDescending = isSortDesc,
                    SortBy = sortBy,
                    TotalRecordCount = totalRecords
                };
                var search = new WardSearchModel { CityCode = cityCode, DistrictCode = districtCode };
                BindFilter(ref search, pageSize, sortAndPage);
                ViewBag.SortAndPage = sortAndPage;
                ViewBag.ListPageSize = _generalSettings.ListPageSize;
                ViewBag.Search = search;
            }

            return PartialView("_PartialList", model);
        }

        public JsonResult CityChange(string cityCode)
        {
            var result = Json(
                           new
                           {
                               AllDistrict = GetAllDistrict(cityCode).Select(d => new { d.DistrictCode, d.DistrictName }).StringifyJs()
                           }, JsonRequestBehavior.AllowGet);
            WardSearchModel search;
            SortAndPagingModel sortAndPage;
            GetCookie(out search, out sortAndPage);
            search.CityCode = cityCode;
            BindCreate(search);
            return result;
        }

        #region Private Method

        private IEnumerable<WardModel> BindIndex(WardSearchModel search, int pageSize)
        {
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = pageSize,
                CurrentPage = 1,
                SortBy = DEFAULT_SORT_BY
            };
            var httpCookie = Request.Cookies[CookieName.SearchWard];
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
            }
            BindFilter(ref search, pageSize, sortAndPage);
            int totalRecords;
            var model = _wardService.Gets(out totalRecords, pageSize: sortAndPage.PageSize,
                                                                sortBy: sortAndPage.SortBy, isDescending: sortAndPage.IsSortDescending,
                                                                districtCode: search.DistrictCode).ToListModel();
            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Search = search;
            return model;
        }

        private void BindFilter(ref WardSearchModel search, int? pageSize, SortAndPagingModel sortAndPage)
        {
            IEnumerable<DistrictModel> districtModels;
            var cityModels = _cityService.Gets().ToListModel();

            if (search != null)
            {
                var districtcode = search.DistrictCode;
                districtModels = GetAllDistrict(search.CityCode);
                if (districtModels.Any())
                {
                    var district = districtModels.Where(d => d.DistrictCode == districtcode).Select(d => d);
                    if (!district.Any())
                    {
                        search.DistrictCode = districtModels.First().DistrictCode;
                    }
                }
                else
                {
                    search.DistrictCode = "0";
                }
                CreateCookieSearch(search, sortAndPage);
            }
            else
            {
                GetCookie(out search, out sortAndPage);
                if (search == null || sortAndPage == null)
                {
                    districtModels = GetAllDistrict(cityModels.First().CityCode);
                    search = new WardSearchModel
                    {
                        CityCode = cityModels.First().CityCode,
                        DistrictCode = districtModels.First().DistrictCode
                    };
                    sortAndPage = new SortAndPagingModel
                    {
                        CurrentPage = 1,
                        IsSortDescending = false,
                        SortBy = DEFAULT_SORT_BY
                    };
                    if (pageSize.HasValue)
                    {
                        sortAndPage.PageSize = pageSize.Value;
                    }
                    CreateCookieSearch(search, sortAndPage);
                }
                else
                {
                    districtModels = GetAllDistrict(search.CityCode);
                    CreateCookieSearch(search, sortAndPage);
                }
            }
            ViewBag.AllCity = cityModels;
            ViewBag.AllDistrict = districtModels;
        }

        private void BindCreate(WardSearchModel search)
        {
            var sortAndPage = new SortAndPagingModel
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DEFAULT_SORT_BY
            };
            BindFilter(ref search, null, sortAndPage);
            ViewBag.Search = search;
        }

        private void CreateCookieSearch(WardSearchModel search, SortAndPagingModel sortpage)
        {
            var data = new Dictionary<string, object> { { "Search", search }, { "SortAndPaging", sortpage } };
            var cookie = Request.Cookies[CookieName.SearchWard];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchWard, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            Response.Cookies.Add(cookie);
        }

        private IEnumerable<DistrictModel> GetAllDistrict(string cityCode)
        {
            return _districtService.Gets(d => d.CityCode == cityCode).OrderBy(d => d.DistrictName).ToListModel();
        }

        public void DistrictChange(string districtCode)
        {
            WardSearchModel search;
            SortAndPagingModel sortAndPage;
            GetCookie(out search, out sortAndPage);
            search.DistrictCode = districtCode;
            BindCreate(search);
        }

        public void GetCookie(out WardSearchModel search, out SortAndPagingModel sortAndPage)
        {
            search = null;
            sortAndPage = null;
            var httpCookie = Request.Cookies[CookieName.SearchWard];
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                search = Json2.ParseAs<WardSearchModel>(data["Search"].ToString());
                sortAndPage = Json2.ParseAs<SortAndPagingModel>(data["SortAndPaging"].ToString());
            }
        }

        #endregion
    }
}
