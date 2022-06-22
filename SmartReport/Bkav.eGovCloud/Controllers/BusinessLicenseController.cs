using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class BusinessLicenseController : BaseController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly BusinessLicenseBll _businessLicenseService;
        private readonly ResourceBll _resourceService;
        private readonly UserBll _userService;
        private readonly BusinessesBll _businessService;
        private readonly CitizenBll _citizenService;
        private readonly BusinessTypeBll _businessTypeService;
        private readonly DocFieldBll _docFiledService;
        private readonly DocTypeBll _docTypeService;
        private readonly DocumentCopyBll _docCopyService;
        private const string DefaultSortBy = "LicenseCode";

        public BusinessLicenseController(BusinessLicenseBll businessLicenseService,
                                            BusinessesBll businessService,
                                            BusinessTypeBll businessTypeService,
                                            DocFieldBll docFiledService,
                                            DocTypeBll docTypeService,
                                            ResourceBll resourceService,
                                            AdminGeneralSettings generalSettings,
                                            DocumentCopyBll documentCopyService,
                                            CitizenBll citizenService,
                                            UserBll userService
                                            )
        {
            _businessLicenseService = businessLicenseService;
            _resourceService = resourceService;
            _generalSettings = generalSettings;
            _businessTypeService = businessTypeService;
            _businessService = businessService;
            _docFiledService = docFiledService;
            _docTypeService = docTypeService;
            _docCopyService = documentCopyService;
            _citizenService = citizenService;
            _userService = userService;
        }

        #region Private Method

        private void CreateCookieSearch(BusinessSearchModel search)
        {
            var data = new Dictionary<string, object> { { "Search", search } };
            var cookie = Request.Cookies[CookieName.SearchBusinessLicense];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchBusinessLicense, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            Response.Cookies.Add(cookie);
        }

        private void GetCookie(out BusinessSearchModel search)
        {
            search = null;
            var httpCookie = Request.Cookies[CookieName.SearchBusinessLicense];
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                search = Json2.ParseAs<BusinessSearchModel>(data["Search"].ToString());
            }
        }

        private string GetAllBusiness(int businessTypeId)
        {
            var allBusiness = _businessService.Gets(b => b.BusinessTypeId == businessTypeId).OrderBy(d => d.BusinessName);
            return allBusiness.Select(
                                u =>
                                new
                                {
                                    value = u.BusinessId,
                                    label = u.BusinessName,
                                    firstpositionid = 0
                                }).StringifyJs();
        }

        #endregion Private Method

        //public ActionResult Create(int docCopyId, string businessName)
        //{
        //    BusinessSearchModel search = null;
        //    string businesses;
        //    var businessTypeModels = _businessTypeService.Gets().ToListModel();
        //    GetCookie(out search);
        //    if (search != null)
        //    {
        //        businesses =
        //            GetAllBusiness(search.BusinessTypeId.HasValue
        //                               ? search.BusinessTypeId.Value
        //                               : businessTypeModels.First().BusinessTypeId);
        //    }
        //    else
        //    {
        //        businesses = GetAllBusiness(businessTypeModels.First().BusinessTypeId);
        //        search = new BusinessSearchModel
        //        {
        //            BusinessTypeId = businessTypeModels.First().BusinessTypeId
        //        };
        //        CreateCookieSearch(search);
        //    }

        //    ViewBag.AllBusinessType = businessTypeModels;
        //    ViewBag.AllBusiness = businesses;
        //    var docCopy = _docCopyService.Get(docCopyId);
        //    var doctype = _docTypeService.Get(docCopy.DocTypeId).ToModel();
        //    ViewBag.DocCopyId = docCopyId;
        //    ViewBag.DocType = doctype;
        //    ViewBag.AllLicenseStatus = _resourceService.EnumToSelectList<LicenseStatus>();
        //    ViewBag.Search = search;
        //    var businessId = 0;
        //    if (!String.IsNullOrEmpty(businessName))
        //    {
        //        var business = _businessService.Get(businessName);
        //        if (business != null)
        //        {
        //            businessId = business.BusinessId;
        //        }
        //    }
        //    // chuyển dùng Get, code không cho phép có 2 giấy phép cùng 1 doccopy
        //    var licenses = _businessLicenseService.Gets(b => b.DocumentCopyId == docCopyId);
        //    if (licenses.Any())
        //    {
        //        var businessLicense = licenses.FirstOrDefault();
        //        var model = businessLicense.ToModel();
        //        var business = _businessService.Get(model.BusinessId);
        //        model.BusinessName = business.BusinessName;
        //        return View(model);
        //    }
        //    return View(new BusinessLicenseModel
        //                    {
        //                        BusinessId = businessId,
        //                        BusinessName = businessName
        //                    });
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docCopyId"></param>
        /// <param name="businessName"></param>
        /// <returns></returns>
        public ActionResult Create(int docCopyId, string businessName)
        {

            ViewBag.AllBusiness = _citizenService.Filter().Select(
                                u =>
                                new
                                {
                                    value = u.Id,
                                    label = u.CitizenName,
                                    firstpositionid = 0
                                }).StringifyJs(); ;
            var docCopy = _docCopyService.Get(docCopyId);
            var doctype = _docTypeService.Get(docCopy.DocTypeId).ToModel();
            ViewBag.DocCopyId = docCopyId;
            ViewBag.DocType = doctype;
            ViewBag.AllLicenseStatus = _resourceService.EnumToSelectList<LicenseStatus>();
            var businessId = 0;
            if (!String.IsNullOrEmpty(businessName))
            {
                var business = _businessService.Get(businessName);
                if (business != null)
                {
                    businessId = business.BusinessId;
                }
            }
            // chuyển dùng Get, code không cho phép có 2 giấy phép cùng 1 doccopy
            var licenses = _businessLicenseService.Gets(b => b.DocumentCopyId == docCopyId);
            if (licenses.Any())
            {
                var businessLicense = licenses.FirstOrDefault();
                var model = businessLicense.ToModel();
                var business = _citizenService.GetById(model.BusinessId);
                model.BusinessName = business.CitizenName;
                return View(model);
            }
            return View(new BusinessLicenseModel
            {
                BusinessId = businessId,
                BusinessName = businessName
            });
        }

        [HttpPost]
        public JsonResult CreateLicense(int docCopyId, string licenseinfo)
        {
            try
            {
                var license = Json2.ParseAsJs<BusinessLicenseModel>(licenseinfo);
                Dictionary<string, string> temFile = null;
                if (!string.IsNullOrEmpty(license.FilePath))
                {
                    temFile = Json2.ParseAs<Dictionary<string, string>>(license.FilePath);
                }
                var businessLicense = license.ToEntity();
                businessLicense.DocumentCopyId = docCopyId;
                businessLicense.CreateByUserId = _userService.CurrentUser.UserId;

                // kiểm soát Business tồn tại hay ko => ko thì thông báo lỗi.
                if (businessLicense.BusinessId == 0)
                {
                    return Json(new { error = "Doanh nghiệp chưa tồn tại trên hệ thống. Vui lòng thêm trước khi cấp phép." });
                }
                // kiểm tra tồn tại của giấy phép theo docCopyId
                var isHas = _businessLicenseService.Exist(b => b.DocumentCopyId == docCopyId);
                if (isHas)
                {
                    _businessLicenseService.Update(businessLicense, temFile);
                }
                else
                {
                    _businessLicenseService.Create(businessLicense, temFile);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                return Json(new { error = "Có lỗi trong quá trình cấp phép. Vui lòng thử lại." });
            }

            return Json(new { success = "Cấp phép thành công." });
        }

        public JsonResult BusinessTypeChange(string businessTypeId)
        {
            var result = Json(
                           new
                           {
                               AllBusiness = GetAllBusiness(int.Parse(businessTypeId))
                           }, JsonRequestBehavior.AllowGet);
            BusinessSearchModel search = null;
            search = new BusinessSearchModel
            {
                BusinessTypeId = int.Parse(businessTypeId)
            };
            CreateCookieSearch(search);
            return result;
        }

        public JsonResult BusinessLicenses(int docCopyId)
        {
            var businessLicenses = _businessLicenseService.GetByDocCopy(docCopyId);
            var result = Json(businessLicenses, JsonRequestBehavior.AllowGet);

            return result;
        }

        [HttpPost]
        public JsonResult RemoveLicenses(int businessLicenseId)
        {
            var businessLicenses = _businessLicenseService.Get(businessLicenseId);
            if (businessLicenses != null)
            {
                _businessLicenseService.Delete(businessLicenses);
            }
            var result = Json(new { sucsess = "Xóa thành công!" }, JsonRequestBehavior.AllowGet);

            return result;
        }

        [HttpPost]
        public JsonResult CreateCitizen(string citizenConvert)
        {
            var citizenModel = JsonConvert.DeserializeObject<CitizenModelAdd>(citizenConvert);

            var citizenCheck = _citizenService.GetByEmail(citizenModel.Email);
           
            if (citizenCheck != null){
                return Json(new { error = "Người dùng đã được đăng kí trong hệ thống" }, JsonRequestBehavior.AllowGet);
            }
            var citizen = new Citizen()
            {
                CitizenName = citizenModel.CitizenName,
                Phone = citizenModel.PhoneNumber,
                Email = citizenModel.Email,
                Address = citizenModel.Address
            };
            _citizenService.Create(citizen);

            return Json(new { sucsess = "Thêm mới thành công!" }, JsonRequestBehavior.AllowGet);;
        }

        //[HttpPost]
        //public void UploadTempAsync(HttpPostedFileBase files)
        //{
        //    AsyncManager.OutstandingOperations.Increment();
        //    var tempPath = ResourceLocation.Default.FileUploadTemp;
        //    var task =
        //            Task.Factory.StartNew(() =>
        //            {
        //                var length = files.InputStream.Length;
        //                if (length > _fileUploadSettings.FileUploadMaximumSizeBytes)
        //                {
        //                    return
        //                        new
        //                        {
        //                            key = "",
        //                            size = length,
        //                            name = files.FileName,
        //                            extension = "",
        //                            error = "Tệp đính kèm quá dung lượng quy định"
        //                        };
        //                }
        //                var ext = Path.GetExtension(files.FileName);
        //                //if (ext != ".rpt")
        //                //{
        //                //    return
        //                //        new
        //                //        {
        //                //            key = "",
        //                //            size = length,
        //                //            name = files.FileName,
        //                //            extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
        //                //            error = "Chỉ cho phép tải lên tệp .rpt"
        //                //        };
        //                //}
        //                var fileName = files.FileName;
        //                var temp = FileManager.Default.Create(files.InputStream, tempPath);
        //                return
        //                    new
        //                    {
        //                        key = temp.Name,
        //                        size = temp.Length,
        //                        name = fileName,
        //                        extension = string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", ""),
        //                        error = ""
        //                    };
        //            });
        //    Task.Factory.ContinueWhenAll(new Task[] { task }, tasks =>
        //    {
        //        var listFile = tasks.Select(item => item);
        //        AsyncManager.Parameters["data"] = listFile;
        //        AsyncManager.OutstandingOperations.Decrement();
        //    });
        //}

        //public JsonResult UploadTempCompleted(dynamic data)
        //{
        //    return Json(data);
        //}
    }
}