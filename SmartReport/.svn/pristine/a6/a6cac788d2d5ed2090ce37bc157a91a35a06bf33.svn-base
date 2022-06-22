using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class BusinessReportController : BaseController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly BusinessesBll _businessService;
        private readonly BusinessLicenseBll _businessLicenseService;
        private readonly BusinessTypeBll _businesstypeService;
        private readonly CityBll _cityService;
        private readonly DistrictBll _districtService;
        private readonly WardBll _wardService;
        private readonly DocFieldBll _docfieldService;
        private readonly DocTypeBll _doctypeService;
        private const string DefaultSortBy = "BusinessName";

        public BusinessReportController(BusinessesBll businessService,
                                    BusinessTypeBll businesstypeService,
                                    AdminGeneralSettings generalSettings,
                                    CityBll citySerrvice,
                                    DistrictBll districtSerrvice,
                                    WardBll wardSerrvice,
                                    DocFieldBll docfieldService,
                                    DocTypeBll doctypeService,
                                    BusinessLicenseBll businessLicenseService)
        {
            _businessService = businessService;
            _generalSettings = generalSettings;
            _businesstypeService = businesstypeService;
            _cityService = citySerrvice;
            _districtService = districtSerrvice;
            _wardService = wardSerrvice;
            _docfieldService = docfieldService;
            _doctypeService = doctypeService;
            _businessLicenseService = businessLicenseService;
        }

        #region Private Method

        private IEnumerable<BusinessModel> BindIndex(BusinessSearchModel search, int? expireday, int? docfieldid, Guid? doctypeid)
        {
            var sortAndPage = new SortAndPagingModel()
            {
                PageSize = _generalSettings.DefaultPageSize,
                CurrentPage = 1,
                IsSortDescending = false,
                SortBy = DefaultSortBy
            };
            BindFilter(ref search);
            int totalRecords;
            var businesses = _businessService.Gets(out totalRecords, pageSize: sortAndPage.PageSize,
                                                   sortBy: sortAndPage.SortBy,
                                                   isDescending: sortAndPage.IsSortDescending,
                                                   businessTypeId: search.BusinessTypeId, cityCode: search.CityCode,
                                                   districtCode: search.DistrictCode, wardId: search.WardId).ToListModel();

            var sNgayKt = "";
            var sNgayBd = "";
            IEnumerable<int> businessIds = null;
            if (search.Timezone.HasValue)
            {
                GetDay(search.Timezone.Value, out sNgayBd, out sNgayKt);
                var ngayBd = DateTime.ParseExact(sNgayBd, "dd/MM/yyyy", null);
                var ngayKt = DateTime.ParseExact(sNgayKt, "dd/MM/yyyy", null);
                businessIds = _businessLicenseService.GetsAs(b => b.BusinessId, b => (b.DocTypeId == search.DocTypeId) && (b.RegisDate >= ngayBd) && (b.RegisDate <= ngayKt)).Distinct();
            }
            else
            {
                if (expireday.HasValue)
                {
                    if (expireday.Value == 0)
                    {
                        businessIds =
                            _businessLicenseService.GetsAs(b => b.BusinessId,
                                b => (b.DocTypeId == search.DocTypeId) && (b.ExpireDate <= DateTime.Now));
                    }
                    else
                    {
                        var experidate = DateTime.Now.AddDays(expireday.Value);
                        var experidateold = DateTime.Now.AddDays(expireday.Value - 9);
                        businessIds =
                            _businessLicenseService.GetsAs(b => b.BusinessId,
                                b => (b.DocTypeId == search.DocTypeId) && (b.ExpireDate <= experidate) && (b.ExpireDate >= experidateold));
                    }
                    search.ExpireDay = expireday.Value;
                    CreateCookieSearch(search);
                }
                else
                {
                    if (docfieldid.HasValue)
                    {
                        var doctypeids = GetAllDocType(docfieldid.Value).Select(d => d.DocTypeId);
                        businessIds =
                                _businessLicenseService.GetsAs(b => b.BusinessId, b => doctypeids.Contains(b.DocTypeId));
                        search.DocFieldId = docfieldid.Value;
                        CreateCookieSearch(search);
                    }
                    else
                    {
                        if (doctypeid.HasValue)
                        {
                            businessIds =
                                _businessLicenseService.GetsAs(b => b.BusinessId, b => b.DocTypeId == doctypeid.Value);
                            search.DocTypeId = doctypeid.Value;
                            CreateCookieSearch(search);
                        }
                        else
                        {
                            businessIds =
                               _businessLicenseService.GetsAs(b => b.BusinessId);
                        }
                    }
                }
            }
            //var datebg = DateTime.ParseExact(DateTime.Now.Date, "dd/MM/yyyy", null);
            var model = businesses.Where(b => businessIds.Contains(b.BusinessId)).Select(b => new BusinessModel
                                                                                                  {
                                                                                                      BusinessName = b.BusinessName,
                                                                                                      UserName = b.UserName,
                                                                                                      UserEmail = b.UserEmail,
                                                                                                      UserPhone = b.UserPhone,
                                                                                                      Phone = b.Phone,
                                                                                                      BusinessLicenses = GetLicense(b.BusinessId)
                                                                                                  });
            sortAndPage.TotalRecordCount = totalRecords;
            ViewBag.SortAndPage = sortAndPage;
            ViewBag.ListPageSize = _generalSettings.ListPageSize;
            ViewBag.Search = search;
            return model;
        }

        private List<BusinessLicenseModel> GetLicense(int businessid)
        {
            var result = new List<BusinessLicenseModel>();
            var license = _businessLicenseService.Gets(l => l.BusinessId == businessid);
            foreach (var bl in license)
            {
                result.Add(new BusinessLicenseModel
                               {
                                   LicenseCode = bl.LicenseCode,
                                   LicenseNumber = bl.LicenseNumber,
                                   IssueDate = bl.IssueDate,
                                   ExpireDate = bl.ExpireDate
                               });
            }
            return result;
        }

        private void BindFilter(ref BusinessSearchModel search)
        {
            IEnumerable<DistrictModel> districtModels;
            IEnumerable<WardModel> wardModels;
            IEnumerable<DocTypeModel> doctypeModels;
            var cityModels = _cityService.Gets().ToListModel();
            var businessTypeModels = _businesstypeService.Gets().ToListModel();
            var docfieldModels = _docfieldService.Gets().ToListModel();
            if (search != null)
            {
                var districtcode = search.DistrictCode;
                var wardId = search.WardId;
                var doctypeId = search.DocTypeId;
                districtModels = GetAllDistrict(search.CityCode);
                if (districtModels.Any())
                {
                    var district = districtModels.Where(d => d.DistrictCode == districtcode).Select(d => d);
                    if (!district.Any())
                    {
                        search.DistrictCode = districtModels.First().DistrictCode;
                    }
                }
                wardModels = GetAllWard(search.DistrictCode);
                if (wardModels.Any())
                {
                    var ward = wardModels.Where(w => w.WardId == wardId).Select(w => w);
                    if (!ward.Any())
                    {
                        search.WardId = wardModels.First().WardId;
                    }
                }
                doctypeModels = GetAllDocType(search.DocFieldId.HasValue ? search.DocFieldId.Value : docfieldModels.First().DocFieldId);
                //if (doctypeModels.Any())
                //{
                //    var doctype = doctypeModels.Where(d => d.DocTypeId == doctypeId).Select(d => d);
                //    if (!doctype.Any())
                //    {
                //        search.DocTypeId = doctypeModels.First().DocTypeId;
                //    }
                //}
                CreateCookieSearch(search);
            }
            else
            {
                GetCookie(out search);
                if (search == null)
                {
                    districtModels = GetAllDistrict(cityModels.First().CityCode);
                    wardModels = GetAllWard(districtModels.First().DistrictCode);
                    doctypeModels = GetAllDocType(docfieldModels.First().DocFieldId);
                    search = new BusinessSearchModel
                    {
                        BusinessTypeId = businessTypeModels.First().BusinessTypeId,
                        CityCode = cityModels.First().CityCode,
                        DistrictCode = districtModels.First().DistrictCode,
                        WardId = wardModels.First().WardId,
                        DocFieldId = docfieldModels.First().DocFieldId,
                        DocTypeId = doctypeModels.First().DocTypeId
                    };
                }
                else
                {
                    districtModels = GetAllDistrict(search.CityCode);
                    wardModels = GetAllWard(search.DistrictCode);
                    doctypeModels = GetAllDocType(search.DocFieldId.HasValue ? search.DocFieldId.Value : docfieldModels.First().DocFieldId);
                }
            }
            ViewBag.AllBusinessType = businessTypeModels;
            ViewBag.AllCity = cityModels;
            ViewBag.AllDistrict = districtModels;
            ViewBag.AllWard = wardModels;
            ViewBag.AllDocField = docfieldModels;
            ViewBag.AllDocType = doctypeModels;
        }

        private void CreateCookieSearch(BusinessSearchModel search)
        {
            var data = new Dictionary<string, object> { { "Search", search } };
            var cookie = Request.Cookies[CookieName.SearchBusinessReport];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
                cookie.Path = "";
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchBusinessReport, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            Response.Cookies.Add(cookie);
        }

        private void GetCookie(out BusinessSearchModel search)
        {
            search = null;
            var httpCookie = Request.Cookies[CookieName.SearchBusinessReport];
            if (httpCookie != null)
            {
                var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                search = Json2.ParseAs<BusinessSearchModel>(data["Search"].ToString());
            }
        }

        private void ClearCookie(string cookiename)
        {
            if (Request.Cookies[cookiename] != null)
            {
                HttpCookie myCookie = new HttpCookie(cookiename);
                myCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(myCookie);
                Response.Cookies.Remove(cookiename);
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

        private IEnumerable<DocTypeModel> GetAllDocType(int docFieldId)
        {
            return _doctypeService.Gets(b => b.DocFieldId == docFieldId).OrderBy(d => d.DocTypeName).ToListModel();
        }

        private static string CreateMsContent(string content)
        {
            var styleTagPosition = content.IndexOf("<style", StringComparison.Ordinal);
            var stypeStr = styleTagPosition >= 0 ? content.Substring(styleTagPosition) : "";
            var divContent = @"<head>
                                 <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>" +
                                     stypeStr +
                                 "</head>";
            if (stypeStr != "")
            {
                divContent += content.Replace(stypeStr, "");
            }
            else
            {
                divContent += content;
            }
            return divContent;
        }

        //private string CreatePdfFile(string content)
        //{
        //    var tmpfile = Server.MapPath(@"~\Working\ConvertTmp\tmpHtml.html");
        //    if (!Directory.Exists(Path.GetDirectoryName(tmpfile)))
        //    {
        //        Directory.CreateDirectory(Path.GetDirectoryName(tmpfile));
        //    }
        //    File.WriteAllText(tmpfile, content.Trim(), Encoding.UTF8);

        //    var pdfPath = Server.MapPath(@"~\Working\ConvertTmp\Baocao.pdf");
        //    var toolPath = Server.MapPath(@"~\Asset\tools\wkhtmltopdf\wkhtmltopdf.exe");
        //    var pdfUtil = new Pdf();
        //    pdfUtil.SetWkhtmltopdfPath(toolPath);
        //    try
        //    {
        //        pdfUtil.SaveFromHtml(tmpfile, pdfPath);
        //    }
        //    catch
        //    {
        //        Response.Write(@"<SCRIPT LANGUAGE=""JavaScript"">alert(""Không thể xuất báo cáo ra dạng pdf."")</SCRIPT>");
        //    }
        //    return pdfPath;
        //}

        private void GetDay(int timezone, out string sNgayBd, out string sNgayKt)
        {
            sNgayBd = "";
            sNgayKt = "";
            TimeSpan diff1;
            DateTime minDay, maxDay;
            minDay = GetMonday(DateTime.Now.AddDays(-7));
            maxDay = minDay.AddDays(6);
            diff1 = new TimeSpan(1, 0, 0, 0, 0);
            int iYear, iMonth;
            switch (timezone)
            {
                case 1:      // Hom nay
                    sNgayBd = ToDdmmyyyy(DateTime.Now);
                    sNgayKt = ToDdmmyyyy(DateTime.Now);
                    break;

                case 2:		// hom qua
                    sNgayBd = ToDdmmyyyy(DateTime.Now - diff1);
                    sNgayKt = ToDdmmyyyy(DateTime.Now - diff1);
                    break;

                case 3:		// tuan nay
                    sNgayBd = ToDdmmyyyy(minDay);
                    sNgayKt = ToDdmmyyyy(maxDay);
                    break;

                case 4:		// tuan truoc
                    sNgayBd = ToDdmmyyyy(minDay.AddDays(-7));
                    sNgayKt = ToDdmmyyyy(maxDay.AddDays(-7));
                    break;

                case 5:		// thang nay
                    sNgayBd = "01" + "/" + ((DateTime.Now.Month.ToString().Length == 1) ? ("0" + DateTime.Now.Month.ToString()) : DateTime.Now.Month.ToString()) + "/" + DateTime.Now.Year.ToString();
                    sNgayKt = DayEndMonth(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString()) + "/" + ((DateTime.Now.Month.ToString().Length == 1) ? ("0" + DateTime.Now.Month.ToString()) : DateTime.Now.Month.ToString()) + "/" + DateTime.Now.Year.ToString();
                    break;

                case 6:		// thang truoc
                    iMonth = DateTime.Now.Month;
                    iYear = DateTime.Now.Year;
                    if (iMonth == 1) { iMonth = 12; iYear--; }
                    else { iMonth--; }
                    sNgayBd = "01" + "/" + iMonth.ToString() + "/" + iYear.ToString();
                    sNgayKt = DayEndMonth(iMonth.ToString(), iYear.ToString()) + "/" + iMonth.ToString() + "/" + iYear.ToString();
                    break;

                case 7:		// Quý 1
                    iYear = DateTime.Now.Year;
                    sNgayBd = "01/01/" + iYear.ToString();
                    sNgayKt = DayEndMonth("3", iYear.ToString()) + "/" + "03" + "/" + iYear.ToString();
                    break;

                case 8:		// Quý 2
                    iYear = DateTime.Now.Year;
                    sNgayBd = "01/04/" + iYear.ToString();
                    sNgayKt = DayEndMonth("6", iYear.ToString()) + "/" + "06" + "/" + iYear.ToString();
                    break;

                case 9:		// Quý 3
                    iYear = DateTime.Now.Year;
                    sNgayBd = "01/07/" + iYear.ToString();
                    sNgayKt = DayEndMonth("9", iYear.ToString()) + "/" + "09" + "/" + iYear.ToString();
                    break;

                case 10:		// Quý 4
                    iYear = DateTime.Now.Year;
                    sNgayBd = "01/10/" + iYear.ToString();
                    sNgayKt = "31/12/" + iYear.ToString();
                    break;

                case 11:		// nam nay
                    iYear = DateTime.Now.Year;
                    sNgayBd = "01/01/" + iYear.ToString();
                    sNgayKt = "31/12/" + iYear.ToString();
                    break;

                case 12:		// nam ngoai
                    iYear = DateTime.Now.Year - 1;
                    sNgayBd = "01/01/" + iYear.ToString();
                    sNgayKt = "31/12/" + iYear.ToString();
                    break;

                case 13:		// thong ke toan bo
                    sNgayBd = "01/01/1900";
                    sNgayKt = "31/12/2099";
                    break;

                default: break;
            }
        }

        private DateTime GetMonday(DateTime date)
        {
            DateTime ngaythuhai = DateTime.Now;
            int ngayHN = DateTime.Now.Day;
            if (date.DayOfWeek.ToString() == "Monday")
                ngaythuhai = date.AddDays(7);
            if (date.DayOfWeek.ToString() == "Tuesday")
                ngaythuhai = date.AddDays(6);
            if (date.DayOfWeek.ToString() == "Wednesday")
                ngaythuhai = date.AddDays(5);
            if (date.DayOfWeek.ToString() == "Thursday")
                ngaythuhai = date.AddDays(4);
            if (date.DayOfWeek.ToString() == "Friday")
                ngaythuhai = date.AddDays(3);
            if (date.DayOfWeek.ToString() == "Saturday")
                ngaythuhai = date.AddDays(2);
            if (date.DayOfWeek.ToString() == "Sunday")
                ngaythuhai = date.AddDays(1);
            return ngaythuhai;
        }

        private static string ToDdmmyyyy(DateTime date)
        {
            return ((date.Day.ToString().Length == 1) ? ("0" + date.Day.ToString()) : date.Day.ToString())
                + "/"
                + ((date.Month.ToString().Length == 1) ? ("0" + date.Month.ToString()) : date.Month.ToString())
                + "/"
                + date.Year.ToString();
        }

        private static string DayEndMonth(string sThang, string sNam)
        {
            if ((sThang == "1") || (sThang == "3") || (sThang == "5") || (sThang == "7") || (sThang == "8") || (sThang == "10") || (sThang == "12"))
            {
                return "31";
            }
            int iNam = Convert.ToInt32(sNam);
            if (sThang == "2")
            {
                if ((iNam % 4) == 0) return "29";  //nam nhuan
                else return "28";
            }
            else return "30";
        }

        #endregion Private Method

        public ActionResult Index(int? docfieldid, Guid? doctypeid)
        {
            ClearCookie(CookieName.SearchBusinessReport);
            var model = BindIndex(null, null, docfieldid, doctypeid);
            return View(model);
        }

        public ActionResult Expire(int? expireday)
        {
            ClearCookie(CookieName.SearchBusinessReport);
            var model = BindIndex(null, expireday, null, null);
            return View(model);
        }

        public ActionResult Search(BusinessSearchModel search)
        {
            IEnumerable<BusinessModel> model = null;
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    BusinessSearchModel businesssearch = null;
                    GetCookie(out businesssearch);
                    model = BindIndex(search, businesssearch.ExpireDay, businesssearch.DocFieldId, businesssearch.DocTypeId);
                }
            }
            return PartialView("_PartialList", model);
        }

        [ValidateInput(false)]
        [HttpPost]
        // [ValidateAntiForgeryToken(Salt = "BusinessReportExportWord")]
		[ValidateAntiForgeryToken]
		public void ExportWord(string reportContent)
        {
            var contenttmp = Microsoft.JScript.GlobalObject.unescape(reportContent);
            Response.Buffer = false;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-word";
            Response.AddHeader("Content-Disposition", "attachment; filename=Baocao.doc");
            var wordContent = CreateMsContent(contenttmp);
            Response.Write(wordContent);

            Response.End();
        }
    }
}