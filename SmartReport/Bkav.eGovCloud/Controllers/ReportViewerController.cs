using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using CrystalDecisions.Shared;
using Bkav.EReport.Entity;
using Bkav.eGovCloud.Business.Objects;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Logging;
using DataTable = System.Data.DataTable;

namespace Bkav.eGovCloud.Controllers
{
    public class ReportViewerController : BaseController
    {
        private readonly ReportHelper _reportHelper;
        private readonly ReportBll _reportService;
        private readonly ReportKeyBll _reportKeyService;
        private readonly ReportGroupBll _reportGroupService;
        private readonly UserBll _userService;
        private readonly DepartmentBll _departmentService;
        private readonly ResourceBll _resourceService;
        private readonly DocumentCopyBll _docCopyService;
        private readonly TemplateKeyBll _templateKeyService;
        private readonly SettingBll _settingService;
        private readonly AttachmentBll _attachmentService;
        private readonly DocumentBll _documentService;
        private readonly ActionLevelBll _actionLevelService;

        private SsoSettings _ssoSettings;
        private const string DEFAULT_GROUP_VALUE = "DEFAULT";

        public ReportViewerController(ReportBll reportService,
            ReportHelper reportHelper, UserBll userService,
            ResourceBll resourceService,
            DepartmentBll departmentService,
            DocumentCopyBll documentCopyService,
            ReportGroupBll reportGroupService,
            ReportKeyBll reportKeyService,
            TemplateKeyBll templateKeyService,
            SettingBll settingService,
            AttachmentBll attachmentService,
            DocumentBll documentService,
            ActionLevelBll actionLevelService)
        {
            _reportService = reportService;
            _reportKeyService = reportKeyService;
            _reportHelper = reportHelper;
            _userService = userService;
            _resourceService = resourceService;
            _departmentService = departmentService;
            _docCopyService = documentCopyService;
            _reportGroupService = reportGroupService;
            _templateKeyService = templateKeyService;
            _settingService = settingService;
            _attachmentService = attachmentService;
            _documentService = documentService;
            _actionLevelService = actionLevelService;
        }

        public ActionResult Index()
        {
            _ssoSettings = SsoSettings.Instance;
            ViewBag.Domain = _ssoSettings.BkavSSOParentDomain;
            return View();
        }

        public ActionResult ReportGeneral()
        {
            if (Request.Cookies["sso_state"] != null && Request.Cookies["sso_code"] != null)
            {
                var sso_state = Request.Cookies["sso_state"].Value;
                var sso_code = Request.Cookies["sso_code"].Value;
                ViewBag.iframeBMM = "https://ioc.mof.gov.vn:7070/callback?id_token=" + sso_code + "&session_state=" + sso_state;
            }
            _ssoSettings = SsoSettings.Instance;
            ViewBag.Domain = _ssoSettings.BkavSSOParentDomain;
            //BMM
            if (_settingService.Get("chatsettings.isactive") != null)
            {
                ViewBag.ValaToken = _settingService.Get("chatsettings.valatoken").SettingValue.ToString();
                ViewBag.ValaService = _settingService.Get("chatsettings.valaservice").SettingValue.ToString();
                ViewBag.MessageService = _settingService.Get("chatsettings.messageservice").SettingValue.ToString();
                ViewBag.ValaId = _settingService.Get("chatsettings.valaid").SettingValue.ToString();
                ViewBag.ValaFactoryId = _settingService.Get("chatsettings.valafactoryid").SettingValue.ToString();
                ViewBag.IsActive = _settingService.Get("chatsettings.isactive").SettingValue.ToString();
            }
            else {
                ViewBag.ValaToken = "";
                ViewBag.ValaService = "";
                ViewBag.MessageService = "";
                ViewBag.ValaId = "";
                ViewBag.ValaFactoryId = "";
                ViewBag.IsActive = "";
            }

            // chọn kỳ báo cáo

            var listActionLevel = _actionLevelService.Gets();

            var descriptions = new List<SelectListItem>();
            foreach (var listViewActionLevel in listActionLevel)
            {
                descriptions.Add(new SelectListItem() { Selected = false, Text = listViewActionLevel.ActionLevelName, Value = Convert.ToString(listViewActionLevel.ActionLevelId) });
            }
            descriptions.Add(new SelectListItem() { Selected = true, Text = "Tùy Chọn Kỳ Báo Cáo", Value = "TuyChon" });

            ViewBag.Description = descriptions;
            return View();
        }

        public ActionResult ReportUsed()
        {
            _ssoSettings = SsoSettings.Instance;
            ViewBag.Domain = _ssoSettings.BkavSSOParentDomain;
            return View();
        }

        public JsonResult GetAllTableName()
        {
            var tables = _reportService.GetTableName();
            return Json(tables, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReportConfig()
        {
            _ssoSettings = SsoSettings.Instance;
            ViewBag.Domain = _ssoSettings.BkavSSOParentDomain;
            return View();
        }

        public ActionResult StatisticalYearbook()
        {
            _ssoSettings = SsoSettings.Instance;
            ViewBag.Domain = _ssoSettings.BkavSSOParentDomain;
            return View();
        }

        public JsonResult GetReport(ReportCriteriaModel model)
        {
            var report = GetReportObject(model);
            var reportId_ = report.ReportId;
            var reportIsFile = _reportService.Get(reportId_);
            report.isFile = reportIsFile.IsFile;
            if (report == null)
            {
                return null;
            }
            //var templateKeyNameDecodeFT = HttpUtility.HtmlDecode(report.Footer);
            //if (string.IsNullOrWhiteSpace(templateKeyNameDecodeFT))
            //    return Json(new { Success = false, data = (IEnumerable<dynamic>)null }, JsonRequestBehavior.AllowGet);
            var templateKeyNameDecode = HttpUtility.HtmlDecode(report.Header);
            //if (string.IsNullOrWhiteSpace(templateKeyNameDecode))
            //    return Json(new { Success = false, data = (IEnumerable<dynamic>)null }, JsonRequestBehavior.AllowGet);
            var _templateKeyNameDecode = string.IsNullOrEmpty(templateKeyNameDecode) ? "" : TemplateKeyName(templateKeyNameDecode);
            #region

            //if (templateKeyNameDecode.IndexOf("@@", StringComparison.Ordinal) >= 0)
            //{
            //    if (templateKeyNameDecode.Contains("@@Phòng ban hiện tại@@"))
            //    {
            //        var department = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
            //        var deparmentName = department.DepartmentName.ToLower();
            //        templateKeyNameDecode = templateKeyNameDecode.Replace("@@Phòng ban hiện tại@@", deparmentName);
            //    }
            //    if (templateKeyNameDecode.Contains("@@PHÒNG BAN HIỆN TẠI@@"))
            //    {
            //        var department = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
            //        var deparmentName = department.DepartmentName.ToUpper();
            //        templateKeyNameDecode = templateKeyNameDecode.Replace("@@PHÒNG BAN HIỆN TẠI@@", deparmentName);
            //    }
            //    if (templateKeyNameDecode.Contains("@@dd@@"))
            //    {
            //        var timeNow = DateTime.Now.ToString("dd");
            //        templateKeyNameDecode = templateKeyNameDecode.Replace("@@dd@@", timeNow);
            //    }
            //    if (templateKeyNameDecode.Contains("@@MM@@"))
            //    {
            //        var timeNow = DateTime.Now.ToString("MM");
            //        templateKeyNameDecode = templateKeyNameDecode.Replace("@@MM@@", timeNow);
            //    }
            //    if (templateKeyNameDecode.Contains("@@yyyy@@"))
            //    {
            //        var timeNow = DateTime.Now.ToString("yyyy");
            //        templateKeyNameDecode = templateKeyNameDecode.Replace("@@yyyy@@", timeNow);
            //    }
            //    if (templateKeyNameDecode.Contains("@@dd-MM-yyyy@@"))
            //    {
            //        var timeNow = DateTime.Now.ToString("dd-MM-yyyy");
            //        templateKeyNameDecode = templateKeyNameDecode.Replace("@@dd-MM-yyyy@@", timeNow);
            //    }
            //    if (templateKeyNameDecode.Contains("@@dd-MM-yyyy hh:mm@@"))
            //    {
            //        var timeNow = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
            //        templateKeyNameDecode = templateKeyNameDecode.Replace("@@dd-MM-yyyy hh:mm@@", timeNow);
            //    }
            //    if (templateKeyNameDecode.Contains("@@"))
            //    {
            //        templateKeyNameDecode = templateKeyNameDecode.Replace("@@", "").Replace("@@", "");
            //    }
            //}

            #endregion
            #region
            //if (templateKeyNameDecodeFT.IndexOf("@@", StringComparison.Ordinal) >= 0)
            //{
            //    if (templateKeyNameDecodeFT.Contains("@@Phòng ban hiện tại@@"))
            //    {
            //        var department = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
            //        var deparmentName = department.DepartmentName.ToLower();
            //        templateKeyNameDecodeFT = templateKeyNameDecodeFT.Replace("@@Phòng ban hiện tại@@", deparmentName);
            //    }
            //    if (templateKeyNameDecodeFT.Contains("@@PHÒNG BAN HIỆN TẠI@@"))
            //    {
            //        var department = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
            //        var deparmentName = department.DepartmentName.ToUpper();
            //        templateKeyNameDecodeFT = templateKeyNameDecodeFT.Replace("@@PHÒNG BAN HIỆN TẠI@@", deparmentName);
            //    }
            //    if (templateKeyNameDecodeFT.Contains("@@dd@@"))
            //    {
            //        var timeNow = DateTime.Now.ToString("dd");
            //        templateKeyNameDecodeFT = templateKeyNameDecodeFT.Replace("@@dd@@", timeNow);
            //    }
            //    if (templateKeyNameDecodeFT.Contains("@@MM@@"))
            //    {
            //        var timeNow = DateTime.Now.ToString("MM");
            //        templateKeyNameDecodeFT = templateKeyNameDecodeFT.Replace("@@MM@@", timeNow);
            //    }
            //    if (templateKeyNameDecodeFT.Contains("@@yyyy@@"))
            //    {
            //        var timeNow = DateTime.Now.ToString("yyyy");
            //        templateKeyNameDecodeFT = templateKeyNameDecodeFT.Replace("@@yyyy@@", timeNow);
            //    }
            //    if (templateKeyNameDecodeFT.Contains("@@dd-MM-yyyy@@"))
            //    {
            //        var timeNow = DateTime.Now.ToString("dd-MM-yyyy");
            //        templateKeyNameDecodeFT = templateKeyNameDecodeFT.Replace("@@dd-MM-yyyy@@", timeNow);
            //    }
            //    if (templateKeyNameDecodeFT.Contains("@@dd-MM-yyyy hh:mm@@"))
            //    {
            //        var timeNow = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
            //        templateKeyNameDecodeFT = templateKeyNameDecodeFT.Replace("@@dd-MM-yyyy hh:mm@@", timeNow);
            //    }
            //    if (templateKeyNameDecodeFT.Contains("@@"))
            //    {
            //        templateKeyNameDecodeFT = templateKeyNameDecodeFT.Replace("@@", "").Replace("@@", "");
            //    }
            //}
            #endregion
            var result = new ReportViewModel()
            {
                ReportId = report.ReportId,
                Description = report.Description,
                ReportName = report.ReportName,
                LastUpdate = report.LastUpdate.ToString("hh:mm dd/MM/yyyy"),
                DisplaySettings = Json2.ParseAs<IEnumerable<ColumnSetting>>(report.ColumnSettings.DisplayColumn),
                GroupSettings = Json2.ParseAs<IEnumerable<GroupColumnModel>>(report.ColumnSettings.GroupColumn),
                SortSettings = Json2.ParseAs<IEnumerable<SortColumnModel>>(report.ColumnSettings.SortColumn),
                Total = report.Total,
                Header = _templateKeyNameDecode,
                Footer = report.Footer,
                PivotConfig = report.PivotConfig,
                ColumnConfig = report.ColumnConfig,
                isHCMC = report.isHCMC,
                isFile = report.isFile
            };
            return Json(result, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult GetDataEform(ReportCriteriaModel model)
        {
            var report = GetReportObject(model);
            return Json(report.Data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetDocumentDetail(Guid id)
        {
            var result = _documentService.Get(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetDocumentExport(List<Guid> model)
        {
            var documentModel = new List<DocumentModel>();

            foreach (var item in model)
            {
                var result = _documentService.Get(item).ToModel();
                documentModel.Add(result);
            }
            return Json(documentModel, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// hiển thị file đính kèm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult GetFileData(ReportCriteriaModel model)
        {
            var attachmentId = model.TreeGroupValue;

            var reportFile = _attachmentService.Get(Int32.Parse(attachmentId));
            if(reportFile == null)
            {
                return null;
            }

            var result = new ReportFileModel()
            {
                DocumentId = reportFile.DocumentId,
                AttachmentId = reportFile.AttachmentId
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public string TemplateKeyName(string str)
        {
            if (str.IndexOf("@@", StringComparison.Ordinal) >= 0)
            {
                if (str.Contains("@@Phòng ban hiện tại@@"))
                {
                    var department = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
                    var deparmentName = department.DepartmentName.ToLower();
                    str = str.Replace("@@Phòng ban hiện tại@@", deparmentName);
                }
                if (str.Contains("@@PHÒNG BAN HIỆN TẠI@@"))
                {
                    var department = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(p => p.UserId == User.GetUserId()).OrderBy(p => p.IsPrimary).FirstOrDefault();
                    var deparmentName = department.DepartmentName.ToUpper();
                    str = str.Replace("@@PHÒNG BAN HIỆN TẠI@@", deparmentName);
                }
                if (str.Contains("@@dd@@"))
                {
                    var timeNow = DateTime.Now.ToString("dd");
                    str = str.Replace("@@dd@@", timeNow);
                }
                if (str.Contains("@@MM@@"))
                {
                    var timeNow = DateTime.Now.ToString("MM");
                    str = str.Replace("@@MM@@", timeNow);
                }
                if (str.Contains("@@yyyy@@"))
                {
                    var timeNow = DateTime.Now.ToString("yyyy");
                    str = str.Replace("@@yyyy@@", timeNow);
                }
                if (str.Contains("@@dd-MM-yyyy@@"))
                {
                    var timeNow = DateTime.Now.ToString("dd-MM-yyyy");
                    str = str.Replace("@@dd-MM-yyyy@@", timeNow);
                }
                if (str.Contains("@@dd-MM-yyyy hh:mm@@"))
                {
                    var timeNow = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
                    str = str.Replace("@@dd-MM-yyyy hh:mm@@", timeNow);
                }
                if (str.Contains("@@"))
                {
                    str = str.Replace("@@", "").Replace("@@", "");
                }
            }
            return str;
        }
        public JsonResult GetReportFile(int reportId)
        {
            var report = _reportService.Get(reportId);
            if (report == null)
            {
                return null;
            }

            var fileLocationName = "/CrystalReport/" + report.FileLocationNameGroup;

            return Json(new { path = fileLocationName }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReportData(ReportCriteriaModel model)
        {
            var report = GetReportObject_New(model);
            if (report == null)
            {
                return null;
            }

            var result = report.Model.Skip((model.Page - 1) * model.PageSize).Take(model.PageSize);

            return Json(report, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetReportKeyData(ReportCriteriaModel model)
        {
            var report = GetReportKeyObject(model);
            if (report == null)
            {
                return null;
            }

            var result = report.Model.Skip((model.Page - 1) * model.PageSize).Take(model.PageSize);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGroups(ReportCriteriaModel model)
        {
            var report = GetReportObject(model);
            if (report == null)
            {
                return null;
            }


            var groupBy = model.GroupBy;
            IEnumerable<GroupData> result;
            if (!report.GroupDatas.ContainsKey(groupBy))
            {
                report = ParseGroup(report, groupBy);
            }

            result = report.GroupDatas[groupBy].Select(g => new GroupData()
            {
                GroupName = string.IsNullOrEmpty(g.GroupName) ? "-" : g.GroupName,
                GroupValue = g.GroupValue,
                Count = g.Count
            });

            return Json(result.OrderBy(g => g.GroupName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGroupData(ReportCriteriaModel model)
        {
            var groupBy = model.GroupBy;
            var groupValue = model.GroupValue ?? "";
            var report = GetReportObject(model);
            if (report == null)
            {
                return null;
            }

            if (!report.GroupDatas.ContainsKey(groupBy))
            {
                report = ParseGroup(report, groupBy);
            }

            var groupDatas = report.GroupDatas[groupBy];
            if (groupDatas == null)
            {
                return null;
            }

            var data = groupDatas.SingleOrDefault(g => g.GroupValue == groupValue);

            var result = data == null ? null : data.Datas;

            if (!string.IsNullOrEmpty(model.SortBy))
            {
                result = model.IsDesc ? result.OrderByDescending(d => d[model.SortBy])
                                            : result.OrderBy(d => d[model.SortBy]);
            }

            return Json(new { total = data == null ? 0 : data.Datas.Count(), data = result.Skip((model.Page - 1) * model.PageSize).Take(model.PageSize) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAll()
        {
            var currentUser = _userService.CurrentUser;
            var result = _reportService.Gets(currentUser, null);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReportByParent(int id)
        {
            var reports = _reportService.Gets(_userService.CurrentUser, id);
            return Json(reports, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetsSearchTree()
        {
            var reports = _reportService.GetsSearch();
            return Json(reports, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<IDictionary<string, object>> GetGroupDataExport(ReportCriteriaModel model, ReportObject report)
        {
            var groupBy = model.GroupBy;
            var groupValue = model.GroupValue ?? "";

            if (!report.GroupDatas.ContainsKey(groupBy))
            {
                report = ParseGroup(report, groupBy);
            }

            var groupDatas = report.GroupDatas[groupBy];
            if (groupDatas == null)
            {
                return null;
            }

            var data = groupDatas.SingleOrDefault(g => g.GroupValue == groupValue);

            var result = data == null ? null : data.Datas;

            if (!string.IsNullOrEmpty(model.SortBy))
            {
                result = model.IsDesc ? result.OrderByDescending(d => d[model.SortBy])
                                            : result.OrderBy(d => d[model.SortBy]);
            }

            return result;
        }

        public void Export(ReportCriteriaModel model)
        {
            var reportData = GetReportObject(model);
            if (reportData == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(model.GroupValue))
            {
                reportData.Model = GetGroupDataExport(model, reportData);
                model.GroupBy = "";
            }

            DateTime from, to;
            GetTime(model.Time, model.FromDate, model.ToDate, out from, out to);


            var report = _reportService.Get(model.ReportId);
            var data = ParseDataSource(reportData.Model, from: from, to: to);

            if (model.ExportTypeEnum != ExportFormatType.NoFormat)
            {
                var settings = Json2.ParseAs<IEnumerable<ColumnSetting>>(reportData.ColumnSettings.DisplayColumn);

                var reportTable = data.Tables[0];

                var settingNames = settings.Select(s => s.ColumnName).ToArray();
                var columnNames = reportTable.Columns.Cast<DataColumn>()
                                     .Select(x => x.ColumnName).ToArray();
                var exceptColumns = columnNames.Except(settingNames);

                foreach (var exceptColumn in exceptColumns)
                {
                    reportTable.Columns.Remove(exceptColumn);
                }
                var index = 0;
                foreach (var settingName in settings)
                {
                    // thực hiện sắp xếp các cột datatable theo thứ tự cấu hình
                    reportTable.Columns[settingName.ColumnName].SetOrdinal(index);
                    index++;
                    // đổi tên header
                    reportTable.Columns[settingName.ColumnName].ColumnName = settingName.DisplayName;

                }

                if (model.ExportTypeEnum == ExportFormatType.PortableDocFormat)
                {
                    var pathTemplate = Path.Combine(ResourceLocation.Default.CrystalReport, report.FileLocationNameGroup);
                    ExportPdf(reportTable, reportData.ReportName, pathTemplate);
                }

                if (model.ExportTypeEnum == ExportFormatType.ExcelWorkbook)
                {
                    ExportToXlsx(reportTable, data.Tables[1], reportData.ReportName);

                    //ExporttoExcel(reportTable, data.Tables[1], reportData.ReportName);
                }
                else
                {
                    //ExportToDocx(reportTable, data.Tables[1], reportData.ReportName);
                    var pathTemplate = Path.Combine(ResourceLocation.Default.CrystalReport, report.FileLocationNameGroup);
                    ExportDocxTemplate(reportTable, reportData.ReportName, pathTemplate);
                    //ExporttoWord(reportTable, data.Tables[1], reportData.ReportName);
                }
                return;
            }

            //var strPath = LoadCrystalFile(report, model.GroupBy);
            //var rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //rd.Load(strPath);
            //rd.SetDataSource(data);
            //rd.ExportToHttpResponse(model.ExportTypeEnum, System.Web.HttpContext.Current.Response, true, report.Name);
            //rd.Close();
        }

        [HttpPost]
        [ValidateInput(false)]
        public string ExportDocxToHTML(string name, string content)
        {
            var fileName = GetFileName(name, 2);
            var path = Path.Combine(ResourceLocation.Default.FileTemp, fileName);
            HTMLtoDOCX.CreateFileFromHTML(content, path);
            return "/Temp/" + fileName;
        }

        [HttpPost]
        [ValidateInput(false)]
        public string ExportDocxHtml(string name, string content)
        {
            var fileName = GetFileName(name, 2);
            var path = Path.Combine(ResourceLocation.Default.FileTemp, fileName);
            content = HttpUtility.HtmlDecode(content);
            HTMLtoDOCX.CreateFileFromHTML(content, path);
            return "/Temp/" + fileName;
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ExportDocxHtmlSign(string name, string content)
        {
            var key = Guid.NewGuid().ToString();
            key = key.Replace("-", "");
            var fileName = GetFileName(name, 2);
            var path = Path.Combine(ResourceLocation.Default.FileUploadTemp, key);
            content = HttpUtility.HtmlDecode(content);
            HTMLtoDOCX.CreateFileFromHTML(content, path);
            return Json(new { fileName = fileName, key = key });
        }

        [HttpPost]
        [ValidateInput(false)]
        public string ExportPdfHtml(string name, string content)
        {
            var fileNamePdf = GetFileName(name, 1);
            var pdfPath = Path.Combine(ResourceLocation.Default.FileTemp, fileNamePdf);
            content = HttpUtility.HtmlDecode(content);
            var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(content, PdfSharp.PageSize.A4);
            pdf.Save(pdfPath);
            return $"/Temp/{fileNamePdf}";
        }

        private ReportObject ParseGroup(ReportObject report, string groupBy)
        {
            var GroupSettings = Json2.ParseAs<IEnumerable<GroupColumnModel>>(report.ColumnSettings.GroupColumn);

            var groupSetting = GroupSettings.SingleOrDefault(g => g.GroupBy == groupBy);
            var groupName = groupSetting == null ? "" : groupSetting.ColumnName;

            var groups = report.Model.Where(r => r.ContainsKey(groupBy)).GroupBy(d => d[groupBy]);

            report.GroupDatas.Add(groupBy, groups.Select(g => new GroupData
            {
                GroupName = g.First()[groupName] == null ? "" : g.First()[groupName].ToString(),
                GroupValue = g.Key == null ? "Khác" : g.Key.ToString(),
                Datas = g.ToList(),
                GroupValues = null,
                Count = g.Count()
            }).OrderBy(g => g.GroupName).ToList());

            _reportService.UpdateCache(report.CacheKey, report);

            return report;
        }

        private string LoadCrystalFile(Entities.Customer.Report report, string groupBy = "")
        {
            //string filename;
            //var stream = _reportService.Download(out filename, report);
            //var tempPath = ResourceLocation.Default.FileUploadTemp;
            //var temp = FileManager.Default.Create(stream, tempPath, null, ".rpt");
            //return temp.FullName;
            var fileLocationName = report.FileLocationName;
            if (!string.IsNullOrEmpty(groupBy) && groupBy != "default")
            {
                fileLocationName = report.FileLocationNameGroup;
            }
            return Path.Combine(ResourceLocation.Default.CrystalReport, fileLocationName);
        }

        private DataSet ParseDataSource(IEnumerable<IDictionary<string, object>> documents, DateTime from, DateTime to)
        {
            var dataColumns = documents.First().Keys;
            var result = new DataSet();

            var reportData = new DataTable("ReportData");

            foreach (var col in dataColumns)
            {
                reportData.Columns.Add(col, typeof(string));
            }

            foreach (var doc in documents)
            {
                var row = reportData.NewRow();

                foreach (var col in dataColumns)
                {
                    row[col] = doc[col] == null ? "" : doc[col].ToString();
                }

                reportData.Rows.Add(row);
            }

            result.Tables.Add(reportData);

            var specials = _reportService.GetSpecialTable(CurrentUserId(), from: from, to: to);
            specials.TableName = "Special";
            result.Tables.Add(specials);

            return result;
        }

        private ReportObject GetReportObject(ReportCriteriaModel model)
        {
            var reportId = model.ReportId;
            var userId = CurrentUserId();

            CreateCookie(model);
            DateTime from, to;
            GetTime(model.Time, model.FromDate, model.ToDate, out from, out to);

            var report = _reportService.GetReportDetailCache(reportId, userId, model.Time, from, to,
                                                model.GroupId, model.TreeGroupValue, model.GroupBy, model.TimeKey);

            if (!string.IsNullOrEmpty(model.SortBy))
            {
                report.Model = model.IsDesc ? report.Model.OrderByDescending(d => d[model.SortBy] == null ? "" : d[model.SortBy].ToString(), new NaturalComparer(NaturalComparerOptions.RomanNumbers))
                                            : report.Model.OrderBy(d => d[model.SortBy] == null ? "" : d[model.SortBy].ToString(), new NaturalComparer(NaturalComparerOptions.RomanNumbers));
            }
            else
            {
                if (!string.IsNullOrEmpty(report.ColumnSettings.SortColumn))
                {
                    var sortSettings = Json2.ParseAs<IEnumerable<SortColumnModel>>(report.ColumnSettings.SortColumn);
                    if (sortSettings.Count() == 0)
                    {
                        return report;
                    }
                    sortSettings = sortSettings.OrderBy(d => d.Index).ToList();
                    var sortOrderBy = sortSettings.First();

                    report.Model = sortOrderBy.IsDesc ? report.Model.OrderByDescending(d => d[sortOrderBy.ColumnName] == null ? "" : d[sortOrderBy.ColumnName].ToString(), new NaturalComparer(NaturalComparerOptions.RomanNumbers))
                                            : report.Model.OrderBy(d => d[sortOrderBy.ColumnName] == null ? "" : d[sortOrderBy.ColumnName].ToString(), new NaturalComparer(NaturalComparerOptions.RomanNumbers));
                }
            }

            return report;
        }

        private ReportObject GetReportObject_New(ReportCriteriaModel model)
        {
            var reportId = model.ReportId;
            var userId = CurrentUserId();

            CreateCookie(model);
            DateTime from, to;
            GetTime(model.Time, model.FromDate, model.ToDate, out from, out to);

            var report = _reportService.GetReportDetailCache(reportId, userId, model.Time, from, to,
                                                model.GroupId, model.TreeGroupValue, model.GroupBy, model.TimeKey);

            if (!string.IsNullOrEmpty(model.SortBy))
            {
                report.Model = model.IsDesc ? report.Model.OrderByDescending(d => d[model.SortBy] == null ? "" : d[model.SortBy].ToString(), new NaturalComparer(NaturalComparerOptions.RomanNumbers))
                                            : report.Model.OrderBy(d => d[model.SortBy] == null ? "" : d[model.SortBy].ToString(), new NaturalComparer(NaturalComparerOptions.RomanNumbers));
            }
            else
            {
                if (!string.IsNullOrEmpty(report.ColumnSettings.SortColumn))
                {
                    var sortSettings = Json2.ParseAs<IEnumerable<SortColumnModel>>(report.ColumnSettings.SortColumn);
                    if (sortSettings.Count() == 0)
                    {
                        return report;
                    }
                    sortSettings = sortSettings.OrderBy(d => d.Index).ToList();
                    var sortOrderBy = sortSettings.First();

                    report.Model = sortOrderBy.IsDesc ? report.Model.OrderByDescending(d => d[sortOrderBy.ColumnName] == null ? "" : d[sortOrderBy.ColumnName].ToString(), new NaturalComparer(NaturalComparerOptions.RomanNumbers))
                                            : report.Model.OrderBy(d => d[sortOrderBy.ColumnName] == null ? "" : d[sortOrderBy.ColumnName].ToString(), new NaturalComparer(NaturalComparerOptions.RomanNumbers));
                }
            }

            return report;
        }


        private ReportKeyObject GetReportKeyObject(ReportCriteriaModel model)
        {
            StaticLog.Log(new List<string>()
            {
                "Đã nhận" + model.Stringify()
            });


            var reportId = model.ReportId;
            var userId = CurrentUserId();

            CreateCookie(model);
            DateTime from, to;
            GetTime(model.Time, model.FromDate, model.ToDate, out from, out to);

            var report = _reportKeyService.GetReportDetailCache(reportId, userId, model.Time, from, to,
                                                model.GroupId, model.TreeGroupValue, model.GroupBy);
            if (report == null) return null;
            StaticLog.Log(new List<string>()
            {
                "Đã get model"
            });

            if (!string.IsNullOrEmpty(model.SortBy))
            {
                report.Model = model.IsDesc ? report.Model.OrderByDescending(d => d[model.SortBy] == null ? "" : d[model.SortBy].ToString(), new NaturalComparer(NaturalComparerOptions.RomanNumbers))
                                            : report.Model.OrderBy(d => d[model.SortBy] == null ? "" : d[model.SortBy].ToString(), new NaturalComparer(NaturalComparerOptions.RomanNumbers));
            }

            //else
            //{
            //if (!string.IsNullOrEmpty(report.ColumnSettings.SortColumn))
            //{
            //    var sortSettings = Json2.ParseAs<IEnumerable<SortColumnModel>>(report.ColumnSettings.SortColumn);
            //    if (sortSettings.Count() == 0)
            //    {
            //        return report;
            //    }
            //    sortSettings = sortSettings.OrderBy(d => d.Index).ToList();
            //    var sortOrderBy = sortSettings.First();

            //    report.Model = sortOrderBy.IsDesc ? report.Model.OrderByDescending(d => d[sortOrderBy.ColumnName] == null ? "" : d[sortOrderBy.ColumnName].ToString(), new NaturalComparer(NaturalComparerOptions.RomanNumbers))
            //                            : report.Model.OrderBy(d => d[sortOrderBy.ColumnName] == null ? "" : d[sortOrderBy.ColumnName].ToString(), new NaturalComparer(NaturalComparerOptions.RomanNumbers));
            //}
            //}

            return report;
        }
        private int CurrentUserId()
        {
            return _userService.CurrentUser.UserId;
        }

        private static void GetTime(DateTimeReport time, DateTime? fromDate, DateTime? toDate, out DateTime from, out DateTime to)
        {
            var now = DateTime.Now;
            var dayOfWeek = now.DayOfWeek;
            var month = now.Month;
            switch (time)
            {
                case DateTimeReport.TatCa:
                    from = new DateTime(1973, 1, 1);
                    to = now.AddDays(1);
                    break;

                case DateTimeReport.TrongNgay:
                    from = new DateTime(now.Year, now.Month, now.Day);
                    to = now;
                    break;

                case DateTimeReport.TrongTuan:
                    var temDateTuan = now.AddDays(-1 * (int)dayOfWeek);
                    from = new DateTime(temDateTuan.Year, temDateTuan.Month, temDateTuan.Day, 0, 0, 0, 1);
                    to = now;
                    break;

                case DateTimeReport.TuanTruoc:
                    var temFromDateTuanTruoc = now.AddDays(-1 * ((int)dayOfWeek + 7));
                    var temToDateTuanTruoc = now.AddDays(-1 * ((int)dayOfWeek + 1));
                    from = new DateTime(temFromDateTuanTruoc.Year, temFromDateTuanTruoc.Month, temFromDateTuanTruoc.Day, 0, 0, 0);
                    to = new DateTime(temToDateTuanTruoc.Year, temToDateTuanTruoc.Month, temToDateTuanTruoc.Day, 23, 59, 59);
                    break;

                case DateTimeReport.TrongThang:
                    from = new DateTime(now.Year, month, 1, 0, 0, 0);
                    to = now;
                    break;

                case DateTimeReport.ThangTruoc:
                    now = now.AddMonths(-1);
                    from = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
                    to = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 59);
                    break;

                case DateTimeReport.Quy1:
                    from = new DateTime(now.Year, 1, 1, 0, 0, 0);
                    to = new DateTime(now.Year, 3, 31, 23, 59, 59);
                    break;

                case DateTimeReport.Quy2:
                    from = new DateTime(now.Year, 4, 1, 0, 0, 0);
                    to = new DateTime(now.Year, 6, 30, 23, 59, 59);
                    break;

                case DateTimeReport.Quy3:
                    from = new DateTime(now.Year, 7, 1, 0, 0, 0);
                    to = new DateTime(now.Year, 9, 30, 23, 59, 59);
                    break;

                case DateTimeReport.Quy4:
                    from = new DateTime(now.Year, 10, 1, 0, 0, 0);
                    to = new DateTime(now.Year, 12, 31, 23, 59, 59);
                    break;

                case DateTimeReport.TrongNam:
                    from = new DateTime(now.Year, 1, 1, 0, 0, 0);
                    to = now;
                    break;

                case DateTimeReport.NamTruoc:
                    from = new DateTime(now.Year - 1, 1, 1, 0, 0, 0);
                    to = new DateTime(now.Year - 1, 12, 31, 23, 59, 59);
                    break;

                case DateTimeReport.TuyChon:
                    from = fromDate.HasValue ? fromDate.Value : now;
                    to = toDate.HasValue ? toDate.Value : now;
                    from = new DateTime(from.Year, from.Month, from.Day, 0, 0, 0);
                    to = new DateTime(to.Year, to.Month, to.Day, 23, 59, 59);
                    break;
                default:
                    from = fromDate.HasValue ? fromDate.Value : DateTime.MinValue;
                    to = toDate.HasValue ? toDate.Value : now;
                    from = new DateTime(from.Year, from.Month, from.Day, 0, 0, 0);
                    to = new DateTime(to.Year, to.Month, to.Day, 23, 59, 59);
                    break;
            }
        }

        private void CreateCookie(ReportCriteriaModel model)
        {
            var data = new Dictionary<string, object> { { CookieName.ReportCriteria, model } };
            var cookie = Request.Cookies[CookieName.ReportCriteria];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = data.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.ReportCriteria, data.StringifyJs()) { Expires = DateTime.Now.AddDays(1) };
            }
            Response.Cookies.Add(cookie);
        }

        private void ExporttoExcel(DataTable datatable, DataTable special, string reportName)
        {
            var gv = new GridView();
            gv.DataSource = datatable;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + reportName + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            var template = GetTemplateHeader(special, reportName, datatable.Columns.Count);
            objHtmlTextWriter.Write(template);

            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
        }

        private void ExportToXlsx(DataTable datatable, DataTable special, string reportName, string pathTemplate = "")
        {
            var fileName = GetFileName(reportName, 3);
            var folderPath = Path.Combine(ResourceLocation.Default.FileTemp, fileName);
            //XlsxParser.SaveToDatatable(datatable, special, folderPath);
            Response.Redirect("/Temp/" + fileName);
        }

        private void ExportToDocx(DataTable datatable, DataTable special, string reportName, string pathTemplate = "")
        {
            var fileName = GetFileName(reportName, 2);
            var folderPath = Path.Combine(ResourceLocation.Default.FileTemp, fileName);
            DocxDataParser.ExportDocx(folderPath, datatable);
            Response.Redirect("/Temp/" + fileName);
        }

        private void ExportPdf(DataTable datatable, string reportName, string pathTemplate = "")
        {
            var fileName = GetFileName(reportName, 2);
            var folderPath = Path.Combine(ResourceLocation.Default.FileTemp, fileName);
            //Docx4Net.ExportDocx(pathTemplate, datatable, folderPath);

            var fileNamePdf = GetFileName(reportName, 1);
            var pdfPath = Path.Combine(ResourceLocation.Default.FileTemp, fileName);
            DocxParser.ConvertToPdf(folderPath, pdfPath);

            Response.Redirect("/Temp/" + fileNamePdf);
        }

        private void ExportDocxTemplate(DataTable datatable, string reportName, string pathTemplate = "")
        {
            var fileName = GetFileName(reportName, 2);
            var folderPath = Path.Combine(ResourceLocation.Default.FileTemp, fileName);
            //Docx4Net.ExportDocx(pathTemplate, datatable, folderPath);
            Response.Redirect("/Temp/" + fileName);
        }

        private string GetFileName(string name, int type)
        {
            var fileExtension = type == 1 ? "pdf" : (type == 2 ? "docx" : "xlsx");
            return string.Format("{0}_{1}.{2}", name.Trim().StripVietnameseChars(), DateTime.Now.ToString("ddMMyyhhmm"), fileExtension);
        }


        private void ExporttoWord(DataTable datatable, DataTable special, string reportName)
        {
            GridView gv = new GridView();
            gv.AllowPaging = false;
            gv.DataSource = datatable;
            //gv.Font = ("Times New Roman", 15);

            gv.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
                "attachment;filename=" + reportName + ".doc");
            Response.ContentType = "application/vnd.ms-word ";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            var template = GetTemplateHeader(special, reportName, datatable.Columns.Count);
            hw.Write(template);

            gv.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        private string GetTemplateHeader(DataTable special, string reportName, int countColumn)
        {
            var template = "<table style='width:100%'>" +
            "<tr>" +
            "<td style='text-align:center' colspan='" + countColumn / 2 + "'><b>" + special.Rows[0]["OfficeParentName"] + "</b></td>" +
            "<td style='text-align:center'></td>" +
            "<td style='text-align:center'colspan='" + (countColumn - 1 - countColumn / 2) + "'><b>Cộng hòa xã hội chủ nghĩa Việt Nam</b></td>" +
            "</tr>" +
            "<tr>" +
            "<td style='text-align:center' colspan='" + countColumn / 2 + "'><b>" + special.Rows[0]["OfficeName"] + "</b></td>" +
            "<td style='text-align:center'></td>" +
            "<td style='text-align:center' colspan='" + (countColumn - 1 - countColumn / 2) + "'><b>Độc lập - Tự do - Hạnh phúc</b></td>" +
            "</tr>" +
            "</table>" +
            "<h1 style='text-align:center'> " + reportName + "</h1>" +
            "<br>" +
            "<table style='width:100%'>" +
            "<tr>" +
            "<td style='text-align:center' colspan='" + countColumn + "'> Từ " + special.Rows[0]["TuThoiGian"] + "  Đến Từ " + special.Rows[0]["DenThoiGian"] + "</td>" +
            "</tr>" +
            "</table>" +
            "<br>" +
            "<style type=\"text/css\">.gridView caption {color: black;font-size:14px;}</style>";
            return template;
        }

        #region Cũ - xem bỏ

        //public ActionResult View(int id, string treeGroupValue, string treeGroupName)
        //{
        //    ReportCriteriaModel reportCriteria;
        //    GetCookie(out reportCriteria);

        //    var report = _reportService.Get(id);
        //    ViewBag.ReportId = id;
        //    ViewBag.TreeGroupValue = treeGroupValue;
        //    ViewBag.TreeGroupName = treeGroupName;
        //    ViewBag.IsExport = true;

        //    var group = _reportGroupService.GetGroup(reportCriteria.GroupId);
        //    ViewBag.GroupName = @group != null ? @group.Name : _resourceService.GetResource("Report.GroupName.Default");

        //    return View(reportCriteria);
        //}

        //public ActionResult Report(ReportCriteriaModel model)
        //{
        //    var report = _reportService.Get(model.ReportId);
        //    if (report == null)
        //    {
        //        ViewBag.Report = _resourceService.GetResource("Report.Message.NotExist");
        //        return PartialView("_ReportViewer");
        //    }

        //    string groupName = string.Empty,
        //        groupDisplay = string.Empty;

        //    CreateCookie(model);
        //    DateTime from, to;
        //    GetTime(model.Time, model.FromDate, model.ToDate, out from, out to);

        //    ViewBag.ReportId = report.ReportId;
        //    ViewBag.TreeGroupName = model.TreeGroupName;
        //    ViewBag.TreeGroupValue = model.TreeGroupValue;
        //    var userId = CurrentUserId();

        //    ViewBag.ReportData = _reportService.GetDataForReport(report.QueryStatistics,
        //        userId, model.Time, @from, to, model.TreeGroupValue);

        //    ViewBag.Group = groupName;
        //    return PartialView("_Report");
        //}

        //#region Export

        //public void ExportToExcel(int reportId, DateTimeReport time,
        //    DateTime? fromDate, DateTime? toDate, int groupId, string treeGroupValue, string treeGroupName, string treeDisplayName)
        //{
        //    ExportToFile(reportId, time, fromDate, toDate, groupId, treeGroupValue, treeGroupName, treeDisplayName, ExportFormatType.Excel);
        //}

        //public void ExportToWord(int reportId, DateTimeReport time,
        //    DateTime? fromDate, DateTime? toDate, int groupId, string treeGroupValue, string treeGroupName, string treeDisplayName)
        //{
        //    ExportToFile(reportId, time, fromDate, toDate, groupId, treeGroupValue, treeGroupName, treeDisplayName, ExportFormatType.WordForWindows);
        //}

        //public ActionResult ExportToCrystal(int reportId, DateTimeReport time,
        //    DateTime? fromDate, DateTime? toDate, int groupId, string treeGroupValue, string treeGroupName, string treeDisplayName)
        //{
        //    ExportToFile(reportId, time, fromDate, toDate, groupId, treeGroupValue, treeGroupName, treeDisplayName, ExportFormatType.CrystalReport);
        //    return View();
        //}

        //public void ExportToPdf(int reportId, DateTimeReport time,
        //    DateTime? fromDate, DateTime? toDate, int groupId, string treeGroupValue, string treeGroupName, string treeDisplayName)
        //{
        //    ExportToFile(reportId, time, fromDate, toDate, groupId, treeGroupValue, treeGroupName, treeDisplayName, ExportFormatType.PortableDocFormat);
        //}

        //#endregion

        //public JsonResult GotoPage(int reportId, DateTimeReport time,
        //    DateTime? fromDate, DateTime? toDate,
        //    int page, int pageSize, string sortBy, bool isDesc,
        //    int groupId, string groupValue, string treeGroupName, string treeGroupValue)
        //{
        //    var report = _reportService.Get(reportId);
        //    DateTime from, to;
        //    GetTime(time, fromDate, toDate, out from, out to);
        //    var group = _reportGroupService.GetGroup(groupId);
        //    var groupBy = string.Empty;

        //    if (group != null)
        //    {
        //        groupBy = group.FieldName;
        //    }

        //    if (string.Equals(groupValue, DEFAULT_GROUP_VALUE, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        groupValue = string.Empty;
        //    }

        //    var result = _reportHelper.GotoPage(report, CurrentUserId(),
        //        time, from, to, page, pageSize, groupBy,
        //        groupValue, sortBy, isDesc, treeGroupName,
        //        treeGroupValue);
        //    return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        //}


        #region Private Methods

        private string GetExportContent(Entities.Customer.Report report, DateTimeReport time, DateTime? fromDate, DateTime? toDate, int groupId)
        {
            DateTime from, to;
            GetTime(time, fromDate, toDate, out from, out to);
            var group = _reportGroupService.GetGroup(groupId);
            string outFile;
            return _reportHelper.LoadForExport(report, CurrentUserId(), time, from, to, group, out outFile);
        }

        private string GetExportFile(Entities.Customer.Report report, DateTimeReport time, DateTime? fromDate, DateTime? toDate, int groupId)
        {
            DateTime from, to;
            GetTime(time, fromDate, toDate, out from, out to);
            var group = _reportGroupService.GetGroup(groupId);
            string folderImg;
            var content = _reportHelper.LoadForExport(report, CurrentUserId(), time, from, to, group, out folderImg);
            var file = FileManager.Default.Create(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)), ResourceLocation.Default.FileTemp, report.Name, "doc");
            var result = Path.Combine(ResourceLocation.Default.FileTemp, report.Name + ".zip");
            DirectoryUtil.Zip(new[] { folderImg, file.FullName }, result, true);
            return result;
        }

        private void GetCookie(out ReportCriteriaModel reportCriteria)
        {
            #region Xử lý cookie

            reportCriteria = new ReportCriteriaModel { IsDesc = false };

            var httpCookie = Request.Cookies[CookieName.ReportCriteria];
            if (httpCookie != null)
            {
                try
                {
                    var data = Json2.ParseAs<Dictionary<string, object>>(httpCookie.Value);
                    reportCriteria = Json2.ParseAs<ReportCriteriaModel>(data[CookieName.ReportCriteria].ToString());
                }
                catch (Exception)
                {
                    reportCriteria = new ReportCriteriaModel();
                }
            }

            #endregion Xử lý cookie
        }

        private void ExportToFile(int reportId, DateTimeReport time,
              DateTime? fromDate, DateTime? toDate, int groupId, string treeGroupValue, string treeGroupName, string treeDisplayName, ExportFormatType exportType)
        {
            var report = _reportService.Get(reportId);
            if (report == null)
            {
                Response.Write("<H2>" + _resourceService.GetResource("Report.Message.NotExist") + "</H2>");
            }
            else
            {
                DateTime from, to;
                var strPath = LoadCrystalFile(report);
                GetTime(time, fromDate, toDate, out from, out to);
                var group = _reportGroupService.GetGroup(groupId);
                var datasource = _reportService.GetDataForCrystal(report, group, CurrentUserId(), time, from, to, treeGroupValue, treeGroupName, treeDisplayName);
                var rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                rd.Load(strPath);
                rd.SetDataSource(datasource);
                rd.ExportToHttpResponse(exportType, System.Web.HttpContext.Current.Response, true, report.Name);
                rd.Close();
            }
        }

        #endregion Private Methods

        #region Chart

        //public ActionResult Chart(ReportCriteriaModel model, bool isDeptChart)
        //{
        //    CreateCookie(model);
        //    DateTime from, to;
        //    GetTime(model.Time, model.FromDate, model.ToDate, out from, out to);
        //    ViewBag.IsDeptChart = isDeptChart;
        //    var deptList = string.Empty;
        //    List<dynamic> dataChart = null;

        //    if (isDeptChart)
        //    {
        //        dataChart = DeptChart(model.Time, model.FromDate, model.ToDate);
        //    }
        //    else
        //    {
        //        var currentUser = _userService.CurrentUser;
        //        //Lấy danh sách phòng ban trực thuộc của người dùng
        //        var depts = _departmentService.GetAllDepartmentUserAccess(currentUser.UserId);
        //        int deptId = 0;
        //        if (depts != null && depts.Any())
        //        {
        //            deptId = depts.First().DepartmentId;
        //            deptList = depts.Select(u => new
        //            {
        //                Value = u.DepartmentId.ToString(),
        //                Text = u.DepartmentPath
        //            }).Stringify();
        //        }

        //        if (deptId > 0)
        //        {
        //            dataChart = UserChart(deptId, model.Time, model.FromDate, model.ToDate);
        //        }
        //    }

        //    ViewBag.DeptList = deptList;
        //    ViewBag.IsDeptChart = isDeptChart;
        //    ViewBag.DataChart = dataChart == null ? "[]" : dataChart.Stringify();

        //    return PartialView("_ChartViewer");
        //}

        //public ActionResult ChartView(bool isDeptChart)
        //{
        //    ReportCriteriaModel reportCriteria;
        //    GetCookie(out reportCriteria);
        //    ViewBag.IsDeptChart = isDeptChart;
        //    return View(reportCriteria);
        //}

        //private List<dynamic> UserChart(int deptId, DateTimeReport time, DateTime? fromDate, DateTime? toDate)
        //{
        //    var users = _userService.GetUserByDept(deptId);
        //    if (users == null || !users.Any())
        //        return null;

        //    DateTime from, to;
        //    GetTime(time, fromDate, toDate, out from, out to);
        //    IEnumerable<DocumentCopy> docCopys = null;
        //    var userIds = users.Select(p => p.UserId);
        //    var docCopyOverdues = _docCopyService.GetDocumentCopyOverDueByListuser(userIds, from, to, out docCopys);

        //    List<dynamic> json = new List<dynamic>();
        //    foreach (var user in users)
        //    {
        //        var docCopysDept = docCopys.Where(p => user.UserId == p.UserCurrentId);
        //        int docDuThao = 0, docKetThuc = 0, docLoaiBo = 0, docDungXuLy = 0, dangXuLy = 0;
        //        foreach (var doc in docCopysDept)
        //        {
        //            if (doc.Status == (int)DocumentStatus.DuThao)        //Văn bản dự thảo
        //            {
        //                docDuThao++;
        //            }
        //            else if (doc.Status == (int)DocumentStatus.KetThuc)   //Văn bản đã kết thúc.
        //            {
        //                docKetThuc++;
        //            }
        //            else if (doc.Status == (int)DocumentStatus.LoaiBo)  //văn bản Loại bỏ.
        //            {
        //                docLoaiBo++;
        //            }
        //            else if (doc.Status == (int)DocumentStatus.DungXuLy) //Văn bản dừng xử lý,
        //            {
        //                docDungXuLy++;
        //            }
        //            else if (doc.Status == (int)DocumentStatus.DangXuLy) //Văn bản đang xử lý.
        //            {
        //                dangXuLy++;
        //            }
        //        }

        //        //Văn bản quá hạn.
        //        var docOverDue = docCopyOverdues == null ? 0 : docCopyOverdues.Count(p => user.UserId == p.UserCurrentId);
        //        //Văn bản đang xử lý.
        //        var docSapToiHan = dangXuLy - docOverDue;

        //        json.Add(new
        //        {
        //            info = new
        //            {
        //                value = user.UserId,
        //                name = user.FullName + " - " + user.Username
        //            },
        //            label = new
        //            {
        //                quaHan = new
        //                {
        //                    name = "Van ban qua han",
        //                    number = docOverDue,
        //                    color = "red"
        //                },
        //                duThao = new
        //                {
        //                    name = "Van ban du thao",
        //                    number = docDuThao,
        //                    color = "red"
        //                },
        //                ketThuc = new
        //                {
        //                    name = "Van ban du thao",
        //                    number = docKetThuc,
        //                    color = "red"
        //                },
        //                loaiBo = new
        //                {
        //                    name = "Van ban du thao",
        //                    number = docLoaiBo,
        //                    color = "red"
        //                },
        //                dungXuLy = new
        //                {
        //                    name = "Van ban du thao",
        //                    number = docDungXuLy,
        //                    color = "red"
        //                },
        //                sapToiHan = new
        //                {
        //                    name = "Van ban du thao",
        //                    number = docSapToiHan,
        //                    color = "yellow"
        //                }
        //            }
        //        });
        //    }

        //    return json;
        //}

        //private List<dynamic> DeptChart(DateTimeReport time, DateTime? fromDate, DateTime? toDate)
        //{
        //    var currentUser = _userService.CurrentUser;
        //    //Lấy danh sách phòng ban trực thuộc của người dùng
        //    var depts = _departmentService.GetAllDepartmentUserAccess(currentUser.UserId);
        //    if (depts == null || !depts.Any())
        //    {
        //        return null;
        //    }

        //    var deptIds = depts.Select(u => u.DepartmentId);
        //    //Lấy nhân viên cấp dưới cảu người dùng
        //    var underUsers = _userService.GetNhanVienCapDuois(deptIds);
        //    if (underUsers == null || !underUsers.Any())
        //    {
        //        return null;
        //    }

        //    var underUserIds = underUsers.Select(u => u.UserId);
        //    DateTime from, to;
        //    GetTime(time, fromDate, toDate, out from, out to);
        //    IEnumerable<DocumentCopy> docCopys = null;
        //    var docCopyOverdues = _docCopyService.GetDocumentCopyOverDueByListuser(underUserIds, from, to, out docCopys);

        //    List<dynamic> json = new List<dynamic>();
        //    foreach (var dept in depts)
        //    {
        //        //Lay danh sach id cuar phongf ban=> lay thoong tin van ban theo userid da co => 
        //        var userIdInDepts = underUsers.Where(p => p.UserDepartmentJobTitless.Any(c => c.DepartmentId == dept.DepartmentId)).Select(p => p.UserId);

        //        //Văn bản quá hạn.
        //        var docOverDue = docCopyOverdues == null ? 0 : docCopyOverdues.Count(p => userIdInDepts.Contains(p.UserCurrentId));

        //        var docCopysDept = docCopys.Where(p => userIdInDepts.Contains(p.UserCurrentId));
        //        int docDuThao = 0, docKetThuc = 0, docLoaiBo = 0, docDungXuLy = 0, dangXuLy = 0;

        //        if (docCopysDept != null && docCopysDept.Any())
        //        {
        //            foreach (var doc in docCopysDept)
        //            {
        //                if (doc.Status == (int)DocumentStatus.DuThao)         //Văn bản dự thảo
        //                {
        //                    docDuThao++;
        //                }
        //                else if (doc.Status == (int)DocumentStatus.KetThuc)   //Văn bản đã kết thúc.
        //                {
        //                    docKetThuc++;
        //                }
        //                else if (doc.Status == (int)DocumentStatus.LoaiBo)  //văn bản Loại bỏ.
        //                {
        //                    docLoaiBo++;
        //                }
        //                else if (doc.Status == (int)DocumentStatus.DungXuLy) //Văn bản dừng xử lý,
        //                {
        //                    docDungXuLy++;
        //                }
        //                else if (doc.Status == (int)DocumentStatus.DangXuLy) //Văn bản đang xử lý.
        //                {
        //                    dangXuLy++;
        //                }
        //            }
        //        }

        //        ////Văn bản đang xử lý.
        //        var docSapToiHan = dangXuLy - docOverDue;

        //        json.Add(new
        //        {
        //            info = new
        //            {
        //                value = dept.DepartmentId,
        //                name = dept.DepartmentName
        //            },
        //            label = new
        //            {
        //                quaHan = new
        //                {
        //                    name = "Van ban qua han",
        //                    number = docOverDue,
        //                    color = "red"
        //                },
        //                duThao = new
        //                {
        //                    name = "Van ban du thao",
        //                    number = docDuThao,
        //                    color = "red"
        //                },
        //                ketThuc = new
        //                {
        //                    name = "Van ban du thao",
        //                    number = docKetThuc,
        //                    color = "red"
        //                },
        //                loaiBo = new
        //                {
        //                    name = "Van ban du thao",
        //                    number = docLoaiBo,
        //                    color = "red"
        //                },
        //                dungXuLy = new
        //                {
        //                    name = "Van ban du thao",
        //                    number = docDungXuLy,
        //                    color = "red"
        //                },
        //                sapToiHan = new
        //                {
        //                    name = "Van ban du thao",
        //                    number = docSapToiHan,
        //                    color = "yellow"
        //                }
        //            }
        //        });
        //    }

        //    return json;
        //}

        #endregion

        #endregion
    }
}