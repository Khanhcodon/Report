using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Core.ReadFile;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.Text;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Business.Admin;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Business.Objects.StatisticXlvb;

namespace Bkav.eGovCloud.Controllers
{
    public class StatisticsController : CustomerBaseController
    {
        private readonly MemoryCacheManager _cacheManager;
        private readonly UserBll _userService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly StatisticBll _statisticService;
        private readonly StoreBll _storeService;
        private readonly InfomationBll _infomationService;
        private SsoSettings _ssoSettings;
        private readonly SsoSettings _connectionSettings;

        public StatisticsController()
        {
            _cacheManager = DependencyResolver.Current.GetService<MemoryCacheManager>();
            _userService = DependencyResolver.Current.GetService<UserBll>();
            _departmentService = DependencyResolver.Current.GetService<DepartmentBll>();
            _docCopyService = DependencyResolver.Current.GetService<DocumentCopyBll>();
            _infomationService = DependencyResolver.Current.GetService<InfomationBll>();
            _positionService = DependencyResolver.Current.GetService<PositionBll>();
            _statisticService = DependencyResolver.Current.GetService<StatisticBll>();
            _storeService = DependencyResolver.Current.GetService<StoreBll>();
            _connectionSettings = SsoSettings.Instance;
        }

        public ActionResult Index()
        {
            GetIndexData();
            ViewBag.IsHSMC = false;
            _ssoSettings = SsoSettings.Instance;
            ViewBag.Domain = _ssoSettings.BkavSSOParentDomain;
            var allStore = _storeService.GetsByUser(User.GetUserId());
            ViewBag.StoreVbDen = allStore.Where(d => d.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDen);
            ViewBag.StoreVbDi = allStore.Where(d => d.CategoryBusinessIdInEnum == CategoryBusinessTypes.VbDi);
            return View();
        }

        public ActionResult OverviewXLVB()
        {
            GetIndexData();
            ViewBag.IsHSMC = false;
            return View();
        }

        public ActionResult HSMC()
        {
            GetIndexData();
            ViewBag.IsHSMC = true;
            return View();
        }

        private void GetIndexData()
        {
            var officies = GetOffice();
            var currentUser = _userService.CurrentUser;
            ViewBag.CurrentUser = currentUser;
            ViewBag.Offices = officies;
            ViewBag.IsQuanTriTapTrung = false;

#if QuanTriTapTrungEdition
            ViewBag.IsQuanTriTapTrung = true;
#endif
        }

        #region XLVB
        public JsonResult Xlvb_den_normal(StatisticsCriteriaModel model)
        {
            var result = _statisticService.GiamSatTong_XlvbNormal(model.From, model.To, model.HasOldDocument, model.ViewType);

            var groups = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(model.ParentName))
            {
               var list = result.Where(r => r.ParrentName == model.ParentName);
                if (list != null && list.Any())
                {
                    result = list.ToList();
                }
            }
            groups.Add("Department", "Phòng ban");
            groups.Add("User", "Người dùng");

            return Json(new
            {
                data = result,
                template = "#xlvb_overview",
                groups = groups,
                hasShowTotal = false,
                hasGetOldDocument = true,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_den_normal_basic(StatisticsCriteriaModel model)
        {
            var result = _statisticService.GiamSatTong_XlvbNormal(model.From, model.To, model.HasOldDocument, model.ViewType);

            var groups = new Dictionary<string, string>();

            groups.Add("Department", "Phòng ban");
            groups.Add("User", "Người dùng");

            return Json(new
            {
                data = result,
                template = "#xlvb_overview_basic",
                groups = groups,
                hasShowTotal = false,
                hasGetOldDocument = true,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_di_normal(StatisticsCriteriaModel model)
        {
            var result = _statisticService.GiamSatTong_XlvbNormal(model.From, model.To, model.HasOldDocument, model.ViewType, 2);

            var groups = new Dictionary<string, string>();

            groups.Add("Department", "Phòng ban");
            groups.Add("User", "Người dùng");

            return Json(new
            {
                data = result,
                template = "#xlvb_overview_basic",
                groups = groups,
                hasShowTotal = false,
                hasGetOldDocument = true,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_den_overdue_normal(StatisticsCriteriaModel model)
        {
            if (string.IsNullOrEmpty(model.GroupBy))
            {
                // Mặc định
                model.GroupBy = "Department";
            }

            var result = _statisticService.VanBanDenOverdueNormal(model.From, model.To, model.HasOldDocument, model.IsProcess, model.GroupBy, overdues: model.overdues, storeId: model.StoreId);

            var groups = new Dictionary<string, string>();
            groups.Add("Department", "Phòng ban");
            groups.Add("User", "Người dùng");

            return Json(new
            {
                data = result,
                template = "#xlvb_overview_detail",
                groups = groups,
                hasShowTotal = true,
                hasGetOldDocument = true,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_den_all_normal(StatisticsCriteriaModel model)
        {
            if (string.IsNullOrEmpty(model.GroupBy))
            {
                // Mặc định
                model.GroupBy = "UserName";
            }

            var result = _statisticService.VanBanDenByUser(model.From, model.To, model.UserId);

            var groups = new Dictionary<string, string>();
            groups.Add("Department", "Phòng ban");
            groups.Add("User", "Người dùng");

            return Json(new
            {
                data = result,
                template = "#xlvb_overview_detail_1",
                groups = groups,
                hasShowTotal = true,
                hasGetOldDocument = true,
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Xlvb_di_overdue_normal(StatisticsCriteriaModel model)
        {
            if (string.IsNullOrEmpty(model.GroupBy))
            {
                // Mặc định
                model.GroupBy = "Department";
            }

            var result = _statisticService.VanBanDenOverdueNormal(model.From, model.To, model.HasOldDocument, model.IsProcess, model.GroupBy, 2);

            var groups = new Dictionary<string, string>();
            groups.Add("Department", "Phòng ban");
            groups.Add("User", "Người dùng");

            return Json(new
            {
                data = result,
                template = "#xlvb_overview_detail",
                groups = groups,
                hasShowTotal = true,
                hasGetOldDocument = true,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_den(StatisticsCriteriaModel model)
        {
            var result = _statisticService.GiamSatTong_Xlvb(model.From, model.To, model.HasOldDocument, model.ViewType);

            var groups = new Dictionary<string, string>();

            groups.Add("Department", "Phòng ban");
            groups.Add("User", "Người dùng");

            return Json(new
            {
                data = result,
                template = "#xlvb_overview",
                groups = groups,
                hasShowTotal = false,
                hasGetOldDocument = true,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_den_overdue(StatisticsCriteriaModel model)
        {
            if (string.IsNullOrEmpty(model.GroupBy))
            {
                // Mặc định
                model.GroupBy = "Department";
            }

            var result = _statisticService.VanBanDenOverdue(model.From, model.To, model.HasOldDocument, model.IsProcess, model.GroupBy);

            var groups = new Dictionary<string, string>();
            groups.Add("Department", "Phòng ban");
            groups.Add("User", "Người dùng");

            return Json(new
            {
                data = result,
                template = "#xlvb_overview_detail",
                groups = groups,
                hasShowTotal = true,
                hasGetOldDocument = true,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_di(StatisticsCriteriaObject model)
        {
            var total = 0;
            var result = _statisticService.GiamSatTong_PhatHanh(model, out total);

            var groups = new Dictionary<string, string>();

            groups.Add("NonGroup", "Không nhóm");
            groups.Add("Category", "Loại văn bản");
            groups.Add("UserSuccess", "Người ký");
            groups.Add("ProcessInfo", "Nơi nhận");

            return Json(new
            {
                data = result,
                template = "#xlvb_phathanh",
                total = total,
                groups = groups,
                hasShowTotal = true,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Xlvb_di_user(StatisticsCriteriaObject model)
        {
            var total = 0;
            var result = _statisticService.GiamSatTong_PhatHanh_User(model, out total);

            var groups = new Dictionary<string, string>();

            groups.Add("NonGroup", "Không nhóm");

            return Json(new
            {
                data = result,
                template = "#xlvb_phathanh",
                total = total,
                groups = groups,
                hasShowTotal = true,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_all_hoibao(StatisticsCriteriaObject model)
        {
            if (string.IsNullOrEmpty(model.GroupBy))
            {
                // Mặc định
                model.GroupBy = "UserName";
            }
            var total = 0;
            var result = _statisticService.Vbdi_HB_Department(model,out total);

            var groups = new Dictionary<string, string>();
            groups.Add("Department", "Phòng ban");
            groups.Add("User", "Người dùng");

            return Json(new
            {
                data = result,
                template = "#xlvb_hoibao_di_1",
                groups = groups,
                hasShowTotal = true,
                hasGetOldDocument = true,
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Xlvb_di_th(StatisticsCriteriaObject model)
        {
            var total = 0;
            var result = _statisticService.GiamSatTong_Vbdi(model, out total);

            var groups = new Dictionary<string, string>();

            groups.Add("NonGroup", "Không nhóm");

            return Json(new
            {
                data = result,
                template = "#xlvb_tong_di_hoibao",
                total = total,
                groups = groups,
                hasShowTotal = false,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_di_th_hb(StatisticsCriteriaObject model)
        {
            var total = 0;
            var result = _statisticService.GiamSatTong_Vbdi(model, out total);

            var groups = new Dictionary<string, string>();

            groups.Add("NonGroup", "Không nhóm");

            return Json(new
            {
                data = result,
                template = "#xlvb_tong_hb",
                total = total,
                groups = groups,
                hasShowTotal = false,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_di_hb_status(StatisticsCriteriaObject model)
        {
            var total = 0;
            var result = _statisticService.Vbdi_HB_Status(model, out total);

            var groups = new Dictionary<string, string>();

            groups.Add("NonGroup", "Không nhóm");
            groups.Add("Department", "Phòng ban");

            return Json(new
            {
                data = result,
                template = "#xlvb_hoibao_di",
                total = total,
                groups = groups,
                hasShowTotal = false,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult Xlvb_hoibao_th(StatisticsCriteriaObject model)
        {
            var total = 0;
            var result = _statisticService.GiamSatTong_HoiBao(model, out total);

            var groups = new Dictionary<string, string>();

            groups.Add("NonGroup", "Không nhóm");

            return Json(new
            {
                data = result,
                template = "#xlvb_tong_hoibao",
                total = total,
                groups = groups,
                hasShowTotal = false,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_di_hoibao(StatisticsCriteriaObject model)
        {
            var total = 0;
            var result = _statisticService.GiamSatTong_PhatHanh_HoiBao(model, out total);

            var groups = new Dictionary<string, string>();

            groups.Add("NonGroup", "Không nhóm");
            groups.Add("Category", "Loại văn bản");
            groups.Add("ProcessInfo", "Cơ quan ban hành");

            return Json(new
            {
                data = result,
                template = "#xlvb_hoibao",
                total = total,
                groups = groups,
                hasShowTotal = true,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_di_hoibao_addressname(StatisticsCriteriaObject model)
        {
            var total = 0;
            var result = _statisticService.Vbdi_HB_Department(model, out total, "addressName");

            var groups = new Dictionary<string, string>();

            groups.Add("NonGroup", "Không nhóm");

            return Json(new
            {
                data = result,
                template = "#xlvb_hoibao_1",
                total = total,
                groups = groups,
                hasShowTotal = true,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_den_tw(StatisticsCriteriaObject model)
        {
            var total = 0;
            var result = _statisticService.GiamSatTong_Den_TW(model, out total);

            var groups = new Dictionary<string, string>();

            groups.Add("NonGroup", "Không nhóm");
            groups.Add("Category", "Loại văn bản");
            groups.Add("Organization", "Cơ quan ban hành");

            return Json(new
            {
                data = result,
                template = "#xlvb_sovbden",
                groups = groups,
                total = total,
                hasShowTotal = true,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_lienthong_den(StatisticsCriteriaObject model)
        {
            var total = 0;
            var result = _statisticService.GiamSatTong_LienThong_Den(model, out total);
                
            var groups = new Dictionary<string, string>();

            groups.Add("NonGroup", "Không nhóm");
            groups.Add("Category", "Loại văn bản");
            groups.Add("Organization", "Cơ quan ban hành");
            groups.Add("ProcessInfo", "Nơi nhận");

            return Json(new
            {
                data = result,
                template = "#xlvb_sovbden",
                total = total,
                groups = groups,
                hasShowTotal = true,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_StoreVbDen(StatisticsCriteriaModel model)
        {
            var result = _statisticService.GiamSatTong_SoVanBanDen(model.StoreId, model.From, model.To, model.GroupBy);

            var groups = new Dictionary<string, string>();
            groups.Add("NonGroup", "Không nhóm");
            groups.Add("Category", "Loại văn bản");
            groups.Add("Organization", "Cơ quan ban hành");

            return Json(new
            {
                data = result,
                template = "#xlvb_sovbden",
                groups = groups,
                hasShowTotal = true,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Xlvb_StoreVbDi(StatisticsCriteriaObject model)
        {
            var total = 0;
            var result = _statisticService.GS_SoVanBanDi(model, out total);
            var groups = new Dictionary<string, string>();
            groups.Add("NonGroup", "Không nhóm");
            groups.Add("Category", "Loại văn bản");
            groups.Add("ProcessInfo", "Nơi nhận");

            return Json(new
            {
                data = result,
                template = "#xlvb_sovbdi",
                total = total,
                groups = groups,
                hasShowTotal = true,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetsSoVbDen(StatisticsCriteriaObject model)
        {
            var total = 0;
            var result = _statisticService.GS_SoVanBanDen(model, out total);

            var groups = new Dictionary<string, string>();
            groups.Add("NonGroup", "Không nhóm");
            groups.Add("Category", "Loại văn bản");
            groups.Add("Organization", "Cơ quan ban hành");

            return Json(new
            {
                data = result,
                template = "#xlvb_sovbden",
                groups = groups,
                total = total,
                hasShowTotal = true,
                hasGetOldDocument = false,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public string ExportXlvb(int type, string name, string content, string statisticName = "", StatisticsCriteriaObject model = null , bool isPaging = false)
        {
            var fileName = "";
            if (isPaging)
            {
                var docs = _statisticService.GetDocExport(model, statisticName);
                if (type == 2)
                {
                    fileName = ExportToDocx(name, content, isPaging, docs);
                }
                else
                {
                    fileName = ExportToXlsx(name, content, isPaging, docs);
                }
            }else
            if (type == 2)
            {
                fileName = ExportToDocx(name, content);
            }
            else if (type == 1)
            {
                fileName = ExportToPdf(name, content);
            }
            else
            {
                fileName = ExportToXlsx(name, content);
            }

            return "/Temp/" + fileName;
        }

        private string ExportToDocx(string name, string content, bool isPaging = false, List<StoreDocumentIn> docs = null)
        {
            var fileName = GetFileName(name, 2);
            var path = Path.Combine(ResourceLocation.Default.FileTemp, fileName);
            if (isPaging)
            {
                var header = XlsxParser.GetHeaderForHtml(content);
                var contentData = ConvertData(docs, header);
                DocxDataParser.ExportDocx(path, contentData);
            }
            else
            {
                HTMLtoDOCX.CreateFileFromHTML(content, path);
            }

            return fileName;
        }

        private string ExportToXlsx(string name, string content, bool isPaging = false, List<StoreDocumentIn> docs = null)
        {
            var fileName = GetFileName(name, 3);
            var folderPath = Path.Combine(ResourceLocation.Default.FileTemp, fileName);

            if (isPaging)
            {
                var header = XlsxParser.GetHeaderForHtml(content);
                var contentData = ConvertData(docs, header);
                XlsxParser.SaveXlsx(content, folderPath, isPaging, contentData);
            }
            else
            {
                XlsxParser.SaveXlsx(content, folderPath);
            }

            ////Codes for the Closed XML
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition",
            //"attachment; filename=" + fileName);
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.BinaryWrite(contentXlsx.ToArray());
            //Response.End();
            return fileName;
        }

        private string ExportToPdf(string name, string content)
        {
            var docxName = GetFileName(name, 2);
            var docsPath = Path.Combine(ResourceLocation.Default.FileTemp, docxName);
            HTMLtoDOCX.CreateFileFromHTML(content, docsPath);

            var fileName = GetFileName(name, 1);
            var pdfPath = Path.Combine(ResourceLocation.Default.FileTemp, fileName);
            DocxParser.ConvertToPdf(docsPath, pdfPath);

            return fileName;
        }

        private DataTable ConvertData(List<StoreDocumentIn> docs, List<Header> headers)
        {
            var reportData = new DataTable("ReportData");

            foreach (var col in headers)
            {
                reportData.Columns.Add(col.Name, typeof(string));
            }
            var index = 0;
            
            foreach (var doc in docs)
            {
                var row = reportData.NewRow();

                if (!doc.IsGroup)
                {
                    foreach (var col in headers)
                    {
                        row[col.Name] = GetPropValue(doc, col.Name);
                    }
                }
                else
                {
                    row[0] = doc.GroupName + " (" + doc.GroupCount + " )";
                }
                

                reportData.Rows.Add(row);
            }

            foreach (var col in headers)
            {
                // thực hiện sắp xếp các cột datatable theo thứ tự cấu hình
                reportData.Columns[col.Name].SetOrdinal(index);
                index++;
                // đổi tên header
                reportData.Columns[col.Name].ColumnName = col.Title;

            }

            return reportData;
        }

        public string GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null) == null? "" : src.GetType().GetProperty(propName).GetValue(src, null).ToString();
        }

        private string GetFileName(string name, int type)
        {
            var fileExtension = type == 1 ? "pdf" : (type == 2 ? "docx" : "xlsx");
            return string.Format("{0}_{1}.{2}", name.Trim().StripVietnameseChars(), DateTime.Now.ToString("ddMMyyhhmm"), fileExtension);
        }

        #endregion

        #region Export

        [HttpPost]
        public void Export(string data, string fromDate, string toDate, string reportName)
        {
            var datasource = ParseDataSource(JsonConvert.DeserializeObject<List<dynamic>>(data), fromDate, toDate, reportName);
            Session["ReportResource"] = datasource;
        }

        [HttpPost]
        public void ExportDetail(string data, string fromDate, string toDate, string reportName)
        {
            var datasource = ParseDataSourceDetail(JsonConvert.DeserializeObject<List<dynamic>>(data), fromDate, toDate, reportName);
            Session["ReportDetailResource"] = datasource;
        }

        [HttpPost]
        public void ExportCustomerInfo(string data, string fromDate, string toDate, string reportName)
        {
            var datasource = ParseDataSourceCustomerInfo(JsonConvert.DeserializeObject<List<dynamic>>(data), fromDate, toDate, reportName);
            Session["ReportDetailCustomerInfo"] = datasource;
        }

        [HttpPost]
        public void ExportOverdueByWorkflow(string data, string fromDate, string toDate, string reportName)
        {
            var datasource = ParseDataSourceDetail(JsonConvert.DeserializeObject<List<dynamic>>(data), fromDate, toDate, reportName);
            Session["ReportOverdueByWorkflow"] = datasource;
        }

        public void ExportLienThong(string data, string fromDate, string toDate)
        {
            var datasource = ParseDataSourceLT(JsonConvert.DeserializeObject<List<dynamic>>(data), fromDate, toDate);
            Session["ReportLienThongData"] = datasource;
        }

        public FileResult ExportFile(bool isDetail, int type)
        {
            var rd = new ReportDocument();
            var strPath = GetReportFile(isDetail);
            rd.Load(strPath, OpenReportMethod.OpenReportByTempCopy);
            var datasource = (DataSet)Session["ReportResource"];
            rd.SetDataSource(datasource);

            var exportType = type == 1 ? ExportFormatType.PortableDocFormat
                : type == 2 ? ExportFormatType.WordForWindows : ExportFormatType.ExcelWorkbook;

            rd.ExportToHttpResponse(exportType, System.Web.HttpContext.Current.Response, false, "Thống kê tình trạng xử lý hồ sơ");
            rd.Close();

            return null;
        }

        public FileResult ExportFileDetail(int type)
        {
            var rd = new ReportDocument();
            var strPath = Server.MapPath("~/CrystalReport/Statistic/BaoCao_TiepNhanHoSo.rpt");
            rd.Load(strPath, OpenReportMethod.OpenReportByTempCopy);

            var datasource = (DataSet)Session["ReportDetailResource"];
            rd.SetDataSource(datasource);
            var exportType = type == 1 ? ExportFormatType.PortableDocFormat
                : type == 2 ? ExportFormatType.WordForWindows : ExportFormatType.ExcelWorkbook;

            rd.ExportToHttpResponse(exportType, System.Web.HttpContext.Current.Response, false, "Thống kê tình trạng xử lý hồ sơ");
            rd.Close();
            return null;
        }

        public FileResult ExportFileCustomerInfoDetail(int type)
        {
            var rd = new ReportDocument();
            var strPath = Server.MapPath("~/CrystalReport/Statistic/BaoCao_Khachhangdangki.rpt");
            rd.Load(strPath, OpenReportMethod.OpenReportByTempCopy);

            var datasource = (DataSet)Session["ReportDetailCustomerInfo"];
            rd.SetDataSource(datasource);

            var exportType = type == 1 ? ExportFormatType.PortableDocFormat
                : type == 2 ? ExportFormatType.WordForWindows
                : ExportFormatType.ExcelWorkbook;

            rd.ExportToHttpResponse(exportType, System.Web.HttpContext.Current.Response, false, "Thống kê thông tin khách hàng");
            rd.Close();
            return null;
        }

        public FileResult ExportFileLT(int type)
        {
            var rd = new ReportDocument();
            var strPath = Server.MapPath("~/CrystalReport/Statistic/BaoCao_HoSoLienThong.rpt");
            rd.Load(strPath, OpenReportMethod.OpenReportByTempCopy);

            var datasource = (DataSet)Session["ReportLienThongData"];
            rd.SetDataSource(datasource);


            var exportType = type == 1 ? ExportFormatType.PortableDocFormat
                : type == 2 ? ExportFormatType.WordForWindows : ExportFormatType.ExcelWorkbook;

            rd.ExportToHttpResponse(exportType, System.Web.HttpContext.Current.Response, false, "Hồ sơ liên thông");
            rd.Close();
            return null;
        }

        public FileResult ExportOverdueByWorkflow(int type)
        {
            var rd = new ReportDocument();
            var strPath = Server.MapPath("~/CrystalReport/Statistic/BaoCao_XLVB_User.rpt");
            rd.Load(strPath, OpenReportMethod.OpenReportByTempCopy);

            var datasource = (DataSet)Session["ReportOverdueByWorkflow"];
            rd.SetDataSource(datasource);


            var exportType = type == 1 ? ExportFormatType.PortableDocFormat
                : type == 2 ? ExportFormatType.WordForWindows : ExportFormatType.ExcelWorkbook;

            rd.ExportToHttpResponse(exportType, System.Web.HttpContext.Current.Response, false, "Thống kê quá hạn");
            rd.Close();
            return null;
        }

        #endregion

        #region Private Methods

        private object ParseDataSourceCustomerInfo(List<dynamic> clientData, string fromDate, string toDate, string reportName)
        {
            var result = new DataSet("eGov");
            var data = new DataTable("Customer");

            data.Columns.Add("DocCode", typeof(string));
            data.Columns.Add("CitizenName", typeof(string));
            data.Columns.Add("DoctypeName", typeof(string));
            data.Columns.Add("DateCreated", typeof(string));
            data.Columns.Add("DateAppointed", typeof(string));
            data.Columns.Add("Phone", typeof(string));
            data.Columns.Add("Address", typeof(string));

            for (var i = 0; i < clientData.Count; i++)
            {
                var resource = clientData[i];
                var row = data.NewRow();
                row["DocCode"] = resource["DocCode"].Value;
#if HoSoMotCuaEdition
                row["CitizenName"] = resource["CitizenName"].Value;
#else
                row["CitizenName"] = resource["Compendium"].Value;
#endif
                row["DoctypeName"] = resource["DoctypeName"].Value;
                row["DateCreated"] = resource["DateCreated"].Value;
                row["DateAppointed"] = resource["DateAppointed"].Value;
                row["Phone"] = resource["Phone"].Value;
                row["Address"] = resource["Address"].Value;
                data.Rows.Add(row);
            }

            result.Tables.Add(data);
            var specials = GetSpecialTable(fromDate, toDate, reportName);
            result.Tables.Add(specials);
            return result;
        }

        private List<Office> GetOffice()
        {
            var domains = new List<Domain>();
#if QuanTriTapTrungEdition

            var userName = _userService.CurrentUser.UsernameEmailDomain;

            var _domain = DependencyResolver.Current.GetService<DomainBll>();
            domains.AddRange(_domain.GetsByUser(userName, true));
#endif
            if (!domains.Any())
            {
                var user = _userService.CurrentUser;
                user.HasViewReport = true;
                if (user.HasViewReport == true)
                {
                    domains.Add(new Domain()
                    {
                        DomainId = 1,
                        CustomerName = _infomationService.GetCurrentOfficeName(),
                        DomainName = GetDomainName(Request.Url.Host, Request.Url.Port)
                    });
                }
            }

            return domains.Select(d => new Office()
            {
                OfficeId = d.DomainId,
                OfficeName = d.CustomerName,
                ServiceUrl = "http://" + d.DomainName
            }).ToList();
        }

        private string GetDomainName(string host, int port)
        {
            if (port == 80 || port == 443)
            {
                return host;
            }
            else
            {
                return host + ":" + port;
            }
        }

        private DataSet ParseDataSource(List<dynamic> clientData, string fromDate, string toDate, string reportName)
        {
            var result = new DataSet();
            var data = new DataTable("ReportData");
            data.Columns.Add("Stt", typeof(int));
            data.Columns.Add("LoaiHoSo", typeof(string));
            data.Columns.Add("TonKyTruoc", typeof(Int32));
            data.Columns.Add("NhanTrongKy", typeof(Int32));
            data.Columns.Add("Tong", typeof(Int32));
            data.Columns.Add("DaGiaiQuyet", typeof(Int32));
            data.Columns.Add("DungHen", typeof(Int32));
            data.Columns.Add("TiLeDungHen", typeof(Single));
            data.Columns.Add("TreHen", typeof(Int32));
            data.Columns.Add("TiLeTreHen", typeof(Single));
            data.Columns.Add("ChuaGiaiQuyet", typeof(Int32));
            data.Columns.Add("TrongHan", typeof(Int32));
            data.Columns.Add("TileTrongHan", typeof(Single));
            data.Columns.Add("QuaHan", typeof(Int32));
            data.Columns.Add("TiLeQuaHan", typeof(Single));
            data.Columns.Add("GhiChu", typeof(string));

            for (var i = 0; i < clientData.Count; i++)
            {
                var resource = clientData[i];
                var row = data.NewRow();
                row["Stt"] = i + 1;
                row["LoaiHoSo"] = resource["LoaiHoSo"].Value;
                row["TonKyTruoc"] = resource["TonKyTruoc"].Value;
                row["NhanTrongKy"] = resource["NhanTrongKy"].Value;
                row["Tong"] = resource["Tong"].Value;
                row["DaGiaiQuyet"] = resource["DaGiaiQuyet"].Value;
                row["DungHen"] = resource["DungHen"].Value;
                row["TiLeDungHen"] = resource["TiLeDungHen"].Value;
                row["TreHen"] = resource["TreHen"].Value;
                row["TiLeTreHen"] = resource["TiLeTreHen"].Value;
                row["ChuaGiaiQuyet"] = resource["ChuaGiaiQuyet"].Value;
                row["TrongHan"] = resource["TrongHan"].Value;
                row["TileTrongHan"] = resource["TileTrongHan"].Value;
                row["QuaHan"] = resource["QuaHan"].Value;
                row["TiLeQuaHan"] = resource["TiLeQuaHan"].Value;
                row["GhiChu"] = resource["GhiChu"].Value;

                data.Rows.Add(row);
            }

            result.Tables.Add(data);
            var specials = GetSpecialTable(fromDate, toDate, reportName);
            result.Tables.Add(specials);
            return result;
        }

        private DataSet ParseDataSourceDetail(List<dynamic> clientData, string fromDate, string toDate, string reportName)
        {
            var result = new DataSet();
            var data = new DataTable("ReportData");

            data.Columns.Add("DocCode", typeof(string));
            data.Columns.Add("CitizenName", typeof(string));
            data.Columns.Add("DateCreated", typeof(string));
            data.Columns.Add("DateAppointed", typeof(string));
            data.Columns.Add("DateReturned", typeof(string));
            data.Columns.Add("DateSuccess", typeof(string));
            data.Columns.Add("CurrentUser", typeof(string));
            data.Columns.Add("GroupName", typeof(string));
            data.Columns.Add("Deadline", typeof(string));

            for (var i = 0; i < clientData.Count; i++)
            {
                var resource = clientData[i];
                var row = data.NewRow();
                row["DocCode"] = resource["DocCode"].Value;
#if HoSoMotCuaEdition
                row["CitizenName"] = resource["CitizenName"].Value;
#else
                row["CitizenName"] = resource["Compendium"].Value;
#endif
                row["DateCreated"] = resource["DateCreated"].Value;
                row["DateAppointed"] = resource["DateAppointed"].Value;
                row["DateReturned"] = resource["DateFinished"].Value;
                row["DateSuccess"] = resource["DateSuccess"].Value;
                row["CurrentUser"] = resource["CurrentUser"].Value;
                row["Deadline"] = resource["Deadline"].Value + " ngày";
                row["GroupName"] = resource["GroupName"] == null ? "" : resource["GroupName"].Value;

                data.Rows.Add(row);
            }

            result.Tables.Add(data);
            var specials = GetSpecialTable(fromDate, toDate, reportName);
            result.Tables.Add(specials);
            return result;
        }

        private DataSet ParseDataSourceLT(List<dynamic> clientData, string fromDate, string toDate)
        {
            var result = new DataSet();
            var data = new DataTable("ReportData");
            data.Columns.Add("DocCode", typeof(string));
            data.Columns.Add("CitizenName", typeof(string));
            data.Columns.Add("Compendium", typeof(string));
            data.Columns.Add("AddressName", typeof(string));
            data.Columns.Add("DatePublished", typeof(string));
            data.Columns.Add("DateResponsed", typeof(string));

            for (var i = 0; i < clientData.Count; i++)
            {
                var resource = clientData[i];
                var row = data.NewRow();
                row["DocCode"] = resource["DocCode"].Value;
                row["CitizenName"] = resource["CitizenName"].Value;
                row["Compendium"] = resource["Compendium"].Value;
                row["AddressName"] = resource["Address"].Value;
                row["DatePublished"] = resource["DatePublished"].Value;
                row["DateResponsed"] = resource["DateResponsed"].Value;

                data.Rows.Add(row);
            }

            result.Tables.Add(data);
            var specials = GetSpecialTable(fromDate, toDate, "");
            result.Tables.Add(specials);
            return result;
        }

        private DataTable GetSpecialTable(string fromDate, string toDate, string reportName)
        {
            var result = new DataTable("Special");
            result.Columns.Add("OfficeName", typeof(string));
            result.Columns.Add("TuThoiGian", typeof(string));
            result.Columns.Add("DenThoiGian", typeof(string));
            result.Columns.Add("ReportName", typeof(string));

            var row = result.NewRow();
            row["OfficeName"] = _infomationService.Gets().First().Name;
            row["TuThoiGian"] = fromDate;
            row["DenThoiGian"] = toDate;
            row["ReportName"] = reportName;
            result.Rows.Add(row);
            return result;
        }

        private string GetReportFile(bool isDetail)
        {
            var file = isDetail ? "~/CrystalReport/Statistic/TongHopTinhTrangXuLy.rpt" : "~/CrystalReport/Statistic/TongHopTinhTrangXuLy_donvi.rpt";
            return Server.MapPath(file);
        }

        #endregion

        public class Office
        {
            public int OfficeId { get; set; }

            public string OfficeName { get; set; }

            public string ServiceUrl { get; set; }
        }
    }
}