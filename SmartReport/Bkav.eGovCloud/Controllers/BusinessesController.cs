using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class BusinessesController : BaseController
    {
        private readonly BusinessesBll _businessService;
        private readonly BusinessTypeBll _businesstypeService;
        private readonly CityBll _cityService;
        private readonly DistrictBll _districtService;
        private readonly WardBll _wardService;

        public BusinessesController(BusinessesBll businessService,
                                    BusinessTypeBll businesstypeService,
                                    CityBll citySerrvice,
                                    DistrictBll districtSerrvice,
                                    WardBll wardSerrvice)
        {
            _businessService = businessService;
            _businesstypeService = businesstypeService;
            _cityService = citySerrvice;
            _districtService = districtSerrvice;
            _wardService = wardSerrvice;
        }

        #region Private Method

        private void CreateCookieSearch(BusinessSearchModel search)
        {
            var data = new Dictionary<string, object> { { "Search", search } };
            var cookie = Request.Cookies[CookieName.SearchBusiness];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchBusiness, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            Response.Cookies.Add(cookie);
        }

        private void GetCookie(out BusinessSearchModel search)
        {
            search = null;
            var httpCookie = Request.Cookies[CookieName.SearchBusiness];
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                search = Json2.ParseAs<BusinessSearchModel>(data["Search"].ToString());
            }
        }

        private IEnumerable<DistrictModel> GetAllDistrict(string cityCode)
        {
            return _districtService.Gets(d => d.CityCode == cityCode).OrderBy(d => d.DistrictName).ToListModel();
        }

        private IEnumerable<WardModel> GetAllWard(string districtCode)
        {
            return _wardService.Gets(d => d.DistrictCode == districtCode).OrderBy(d => d.WardName).ToListModel();
        }

        #endregion Private Method

        public ActionResult CreateQuickBusiness(string businessName, string businessTypeId)
        {
            var citizen = new CitizenModel();
            return View(new BusinessModel
            {
                BusinessName = businessName,
                BusinessTypeId = 1
            });
        }

        public ActionResult CreateBusiness(string businessName, string businessTypeId)
        {
            BusinessSearchModel search = null;
            GetCookie(out search);
            IEnumerable<DistrictModel> districtModels;
            IEnumerable<WardModel> wardModels;
            var cityModels = _cityService.Gets().ToListModel();
            var businessTypeModels = _businesstypeService.Gets().ToListModel();
            if (search != null)
            {
                districtModels = GetAllDistrict(search.CityCode);
                wardModels = GetAllWard(search.DistrictCode);
                search.BusinessTypeId = string.IsNullOrEmpty(businessTypeId)
                                            ? businessTypeModels.First().BusinessTypeId
                                            : int.Parse(businessTypeId);
            }
            else
            {
                districtModels = GetAllDistrict(cityModels.First().CityCode);
                wardModels = GetAllWard(districtModels.First().DistrictCode);
                search = new BusinessSearchModel
                {
                    BusinessTypeId = string.IsNullOrEmpty(businessTypeId) ? businessTypeModels.First().BusinessTypeId : int.Parse(businessTypeId),
                    CityCode = cityModels.First().CityCode,
                    DistrictCode = districtModels.First().DistrictCode,
                    WardId = wardModels.First().WardId
                };
                CreateCookieSearch(search);
            }
            ViewBag.AllBusinessType = businessTypeModels;
            ViewBag.AllCity = cityModels;
            ViewBag.AllDistrict = districtModels;
            ViewBag.AllWard = wardModels;
            ViewBag.Search = search;
            return View(new BusinessModel
                            {
                                BusinessName = businessName,
                                BusinessTypeId = string.IsNullOrEmpty(businessTypeId) ? businessTypeModels.First().BusinessTypeId : int.Parse(businessTypeId)
                            });
        }

        [HttpPost]
        // [ValidateAntiForgeryToken(Salt = "BusinessesCreate")]
		[ValidateAntiForgeryToken]
		public JsonResult Create(string businessinfo)
        {
            try
            {
                var business = Json2.ParseAsJs<BusinessModel>(businessinfo);
                var businessentity = business.ToEntity();
                _businessService.Create(businessentity);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Có lỗi trong quá trình thêm mới doanh nghiệp. Vui lòng thử lại." });
            }

            return Json(new { success = "Thêm mới doanh nghiệp thành công." });
        }

        //public ActionResult Edit(int id)
        //{
        //    var businessType = _businessService.Get(id);
        //    if (businessType == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    BusinessSearchModel search = null;
        //    BindCreate(search);
        //    var model = businessType.ToModel();
        //    return View(model);
        //}

        //[HttpPost]
        // [ValidateAntiForgeryToken(Salt = "BusinessesEdit")]
        //public ActionResult Edit(BusinessModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var business = _businessService.Get(model.BusinessId);
        //        if (business != null)
        //        {
        //            var oldBusinessName = business.BusinessName;
        //            try
        //            {
        //                _businessService.Update(model.ToEntity(business), oldBusinessName);
        //            }
        //            catch (EgovException ex)
        //            {
        //                ErrorNotification(ex.Message);
        //                return View(model);
        //            }
        //            SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Business.Updated"));
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        // [ValidateAntiForgeryToken(Salt = "BusinessesDelete")]
        //public ActionResult Delete(int id)
        //{
        //    var business = _businessService.Get(id);
        //    if (business != null)
        //    {
        //        try
        //        {
        //            _businessService.Delete(business);
        //            SuccessNotification(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Business.Deleted"));
        //        }
        //        catch (EgovException ex)
        //        {
        //            ErrorNotification(ex.Message);
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}

        public JsonResult CityChange(string cityCode)
        {
            var allDistrict = GetAllDistrict(cityCode);
            var allWard = allDistrict.Any() ? GetAllWard(allDistrict.First().DistrictCode) : new List<WardModel>();
            var result = Json(
                           new
                           {
                               AllDistrict = allDistrict.Select(d => new { d.DistrictCode, d.DistrictName }).StringifyJs(),
                               AllWard = allWard.Select(d => new { d.WardId, d.WardName }).StringifyJs()
                           }, JsonRequestBehavior.AllowGet);
            BusinessSearchModel search = null;
            GetCookie(out search);
            search.CityCode = cityCode;
            search.DistrictCode = allDistrict.Any() ? allDistrict.First().DistrictCode : "0";
            search.WardId = allWard.Any() ? allWard.First().WardId : 0;
            CreateCookieSearch(search);
            return result;
        }

        public JsonResult DistrictChange(string districtCode)
        {
            var allWard = GetAllWard(districtCode);
            var result = Json(
                           new
                           {
                               AllWard = allWard.Select(d => new { d.WardId, d.WardName }).StringifyJs()
                           }, JsonRequestBehavior.AllowGet);
            BusinessSearchModel search = null;
            GetCookie(out search);
            search.DistrictCode = districtCode;
            search.WardId = allWard.Any() ? allWard.First().WardId : 0;
            CreateCookieSearch(search);
            return result;
        }

        public void WardChange(int wardId)
        {
            BusinessSearchModel search = null;
            GetCookie(out search);
            search.WardId = wardId;
            CreateCookieSearch(search);
        }

        public void BusinessTypeChange(int businessTypeId)
        {
            BusinessSearchModel search = null;
            GetCookie(out search);
            search.BusinessTypeId = businessTypeId;
            CreateCookieSearch(search);
        }
    }
}