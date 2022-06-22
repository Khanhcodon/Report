#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using QRCoder;
using System.Drawing;


#endregion

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class PrintController : CustomerBaseController
    {
        #region Readonly & Static Fields

        private readonly DocumentCopyBll _docCopyService;
        private readonly DocumentBll _documentService;
        private readonly TemplateHelper _templateHelper;
        private readonly TemplateBll _templateService;
        private readonly DailyProcessBll _dailyProcessService;
        private readonly DocumentPermissionHelper _documentPermissionHelper;
        private readonly ResourceBll _resourceService;
        private readonly PrinterBll _printerService;
        private readonly DepartmentBll _departmentService;
        private readonly UserBll _userService;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly CommentBll _commentService;
        private readonly DocTypeBll _docTypeService;
        private readonly AddressBll _addressService;

        #endregion

        #region C'tors

        public PrintController(
                    TemplateHelper templateHelper,
                    DocumentCopyBll documentCopyService,
                    TemplateBll templateService,
                    DocumentPermissionHelper documentPermissionHelper,
                    DailyProcessBll dailyProcessService,
                    DocumentBll documentService,
                    ResourceBll resourceService,
                    PrinterBll printerService,
                    Helper.UserSetting helperUserSetting,
                    DepartmentBll departmentService,
                    UserBll userService,
                    CommentBll commentService,
                    DocTypeBll docTypeService,
                    AddressBll addressService)
        {
            _templateHelper = templateHelper;
            _docCopyService = documentCopyService;
            _templateService = templateService;
            _documentPermissionHelper = documentPermissionHelper;
            _dailyProcessService = dailyProcessService;
            _documentService = documentService;
            _resourceService = resourceService;
            _printerService = printerService;
            _departmentService = departmentService;
            _helperUserSetting = helperUserSetting;
            _userService = userService;
            _commentService = commentService;
            _docTypeService = docTypeService;
            _addressService = addressService;
        }

        #endregion

        #region Instance Methods

        #region Express Print

        /// <summary>
        /// Trang in nhanh
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpressPrint()
        {
            var search = new PrintSearchModel();
            HttpCookie httpCookie = Request.Cookies[CookieName.SearchPrintExpress];
            if (httpCookie != null)
            {
                try
                {
                    search = Json2.ParseAs<PrintSearchModel>(httpCookie.Value);
                }
                catch (Exception)
                {
                }
            }
            DateTime from, to;
            SetTimeForSearch(search.Time, out from, out to);
            var model = _dailyProcessService.Gets(CurrentUserId(), search.ProcessType, search.DocCount, from, to).ToListModel();
            ViewBag.Search = search;
            return View(model);
        }

        public ActionResult Search(PrintSearchModel search)
        {
            DateTime from, to;
            SetTimeForSearch(search.Time, out from, out to);
            var model = _dailyProcessService.Gets(CurrentUserId(), search.ProcessType, search.DocCount, from, to).ToListModel();
            CreateCookieSearch(search);
            return PartialView("_ExpressPrintList", model);
        }

        public ActionResult SearchDocCode(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("ExpressPrint");
            }
            var documents = _documentService.Gets(true, d => d.DocCode.ToLower().Contains(searchTerm.ToLower()));
            return PartialView("_DocumentPrint", documents.ToListPrintModel());
        }

        private void CreateCookieSearch(PrintSearchModel search)
        {
            HttpCookie cookie = Request.Cookies[CookieName.SearchPrintExpress];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Value = search.StringifyJs();
            }
            else
            {
                cookie = new HttpCookie(CookieName.SearchPrintExpress, search.StringifyJs()) { Expires = DateTime.Now.AddDays(365) };
            }
            Response.Cookies.Add(cookie);
        }

        private void SetTimeForSearch(DailyProcessTimeForSearch time, out DateTime from, out DateTime to)
        {
            var now = DateTime.Now;
            to = now;
            switch (time)
            {
                case DailyProcessTimeForSearch.AllDay:
                    from = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                    break;

                case DailyProcessTimeForSearch.ThirtyMinutes:
                    from = now.AddMinutes(-30);
                    break;

                case DailyProcessTimeForSearch.OneHour:
                    from = now.AddHours(-1);
                    break;

                case DailyProcessTimeForSearch.TwoHour:
                    from = now.AddHours(-2);
                    break;

                case DailyProcessTimeForSearch.Am:
                    from = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                    to = new DateTime(now.Year, now.Month, now.Day, 12, 0, 0);
                    break;

                case DailyProcessTimeForSearch.Pm:
                    from = new DateTime(now.Year, now.Month, now.Day, 12, 0, 0);
                    break;

                default:
                    from = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                    break;
            }
        }

        #endregion

        /// <summary>
        ///   Trả về danh sách các mẫu phiếu in có thể xuất.
        ///   <para> (Tienbv@bkav.com 080413) </para>
        /// </summary>
        /// <param name="docCopyId"> Document copy tương ứng </param>
        /// <returns> </returns>
        public JsonResult GetPrints(int docCopyId)
        {
            if (docCopyId <= 0)
            {
                return Json(new { success = new List<Template>().StringifyJs() }, JsonRequestBehavior.AllowGet);
            }
            var documentCopy = _docCopyService.Get(docCopyId);
            if (documentCopy == null)
            {
                throw new Exception("Hồ sơ không tồn tại");
            }

            // Todo: tính phần ủy quyền xử lý sau.
            var userId = CurrentUserId();

            var result = _templateService.GetAvaiablePrints(documentCopy, userId);
            return Json(new { prints = result, embryonicForms = new { } }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///   Trả về danh sách các mẫu phiếu in có thể xuất.
        ///   <para> (Tienbv@bkav.com 080413) </para>
        /// </summary>
        /// <param name="docCopyIds"> Document copy tương ứng </param>
        /// <returns> </returns>
        public JsonResult GetPrintByDocCopys(string docCopyIds, Guid? docTypeId = null)
        {
//#if !HoSoMotCuaEdition
//            return Json(new { }, JsonRequestBehavior.AllowGet);
//#endif
            var dCopyIds = Json2.ParseAs<List<int>>(docCopyIds);
            if (dCopyIds.Count == 0)
            {
                return Json(new { success = new List<Template>().StringifyJs() }, JsonRequestBehavior.AllowGet);
            }

            var prints = new List<Template>();
            if (dCopyIds.Count == 1 && dCopyIds.First() == 0 && docTypeId.HasValue)
            {
                var docType = _docTypeService.GetFromCache(docTypeId.Value);

                if (docType != null && docType.CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc)
                {
                    prints.AddRange(_templateService.GetTiepNhanHsmcPrint(docType.ToEntity()));
                    var newHsmcResult = prints.Select(p => new
                    {
                        Name = p.Name,
                        Id = p.TemplateId
                    });
                    return Json(newHsmcResult, JsonRequestBehavior.AllowGet);
                }
            }

            var docCopyXlvbs = _docCopyService.Gets(dc => dCopyIds.Contains(dc.DocumentCopyId) && dc.Document.CategoryBusinessId != (int)CategoryBusinessTypes.Hsmc).ToList();

            if (docCopyXlvbs.Any())
            {
                prints.AddRange(_templateService.GetXlvbPrint());
                var newHsmcResult = prints.Select(p => new
                {
                    Name = p.Name,
                    Id = p.TemplateId
                });
                return Json(newHsmcResult, JsonRequestBehavior.AllowGet);
            }

            // var docCopys = _docCopyService.Gets(dCopyIds, isIncludeDocument: true).ToList();
            var docCopys = _docCopyService.Gets(dc => dCopyIds.Contains(dc.DocumentCopyId) && dc.Document.CategoryBusinessId == (int)CategoryBusinessTypes.Hsmc).ToList();

            // Todo: tính phần ủy quyền xử lý sau.
            var userId = CurrentUserId();

            prints = _templateService.GetAvaiablePrints(docCopys, userId).ToList();
            var result = prints.Select(p => new
            {
                Name = p.Name,
                Id = p.TemplateId
            });
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Trả về danh sách các mẫu phiếu in có thể sử dụng
        /// </summary>
        /// <param name="processType"></param>
        /// <returns></returns>
        public JsonResult GetPrintTemplates(int processType)
        {
            var result = _templateService.Gets((DocumentProcessType)processType);
            return Json(new { success = result.StringifyJs() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="docCopyIds"></param>
        /// <returns></returns>
        public ActionResult Index(int id, string docCopyIds, string paperAddIds, string feeAddIds)
        {
            var result = new Dictionary<string, string>();
            var documentCopies = _docCopyService.Gets(Json2.ParseAs<List<int>>(docCopyIds), true);


            foreach (var docCopy in documentCopies)
            {
                // CuongNT@bkav.com - 210613: Ủy quyền xử lý
                int userSendId;
                if (!_documentPermissionHelper.CheckForUyQuyenVaXuLyVanBan(docCopy, CurrentUserId(), out userSendId))
                {
                    userSendId = CurrentUserId();
                    if (!_documentPermissionHelper.CheckForQuyenXem(docCopy, userSendId))
                    {
                        throw new Exception("Không có quyền xem văn bản");
                    }
                }
                Template template = null;
                if (documentCopies.Count() == 1 && id == 0)
                {
                    template = _templateService.GetTiepNhanHsmcPrint(documentCopies.First());
                }
                else
                {
                    template = _templateService.GetTemplateForPrint(id, docCopy.Document.DocTypeId.Value, docCopy.Document.ListDocFieldId);
                }

                if (template != null)
                {
                    result.Add(docCopy.Document.Compendium,
                               _templateHelper.ParseContentNew(template, userSendId, docCopy.Document.DocumentId, null,
                                                            paperAddIds, feeAddIds));
                }
            }
            ViewBag.Result = result;
            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="changes"></param>
        /// <returns></returns>
        public JsonResult SaveChange(Guid docId, string changes)
        {
            try
            {
                var changeKeys = Json2.ParseAs<Dictionary<string, string>>(changes);
                _templateHelper.UpdateKey(docId, changeKeys);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Print()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Print trên server
        /// </summary>
        /// <param name="docCopyIds"></param>
        /// <param name="templateId"></param>
        /// <param name="printerId"></param>
        /// <param name="commonTemplate"></param>
        /// <returns></returns>
        public JsonResult QuickPrint(string docCopyIds, int templateId = 0, int suppId = 0, int printerId = 0, short copies = 1, bool landscape = true, int commonTemplate = 0)
        {
            try
            {
                var documentCopyIds = Json2.ParseAs<List<int>>(docCopyIds);
                if (!documentCopyIds.Any())
                {
                    throw new ArgumentNullException(_resourceService.GetResource("Document.NotExist"));
                }

                if (documentCopyIds.Count() == 1 && templateId == 0)
                {
                    var doc = _docCopyService.Get(documentCopyIds.First());
                    if (!(doc.NodeCurrentPermission.HasValue
                        && (EnumHelper<NodePermissions>.ContainFlags((NodePermissions)doc.NodeCurrentPermission, NodePermissions.QuyenKhoiTao)
                        || EnumHelper<NodePermissions>.ContainFlags((NodePermissions)doc.NodeCurrentPermission, NodePermissions.QuyenTraKetQua))))
                    {
                        throw new Exception(_resourceService.GetResource("Document.NotHavePermission"));
                    }
                }

                if (templateId == 0 && commonTemplate != 0)
                {
                    var template = _templateService.Gets(t => t.IsActive && t.CommonTemplate.HasValue && t.CommonTemplate == commonTemplate).FirstOrDefault();
                    templateId = template.TemplateId;
                }

                var rd = GetReport(documentCopyIds, templateId, suppId);
                var printer = GetDefaultPrinter(printerId);
                PrintToPrinter(rd, printer.PrinterName, copies, landscape);
                return Json(new { success = true, printerName = printer.ShareName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Preview
        /// </summary>
        /// <param name="docCopyIds"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public void PreviewPrint(string docCopyIds, int templateId = 0, int suppId = 0)
        {
            try
            {
                var documentCopyIds = Json2.ParseAs<List<int>>(docCopyIds);
                if (!documentCopyIds.Any())
                {
                    throw new ArgumentNullException(_resourceService.GetResource("Document.NotExist"));
                }

                var rd = GetReport(documentCopyIds, templateId, suppId);
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, Guid.NewGuid().ToString());
                rd.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        /// <summary>
        /// Lấy danh sách máy in 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetActivePrinters()
        {
            var printers = _printerService.Gets(p => p.IsActivated == true && p.IsShared == true);
            return Json(printers, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="docCopyIds"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        #endregion

        /// <summary>
        /// Lấy danh sách các máy in
        /// <para> (HopCV@bkav.com 2800314) </para>
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPrinters()
        {
            var printers = _printerService.Gets(p => p.IsActivated == true && p.IsShared == true);
            var model = printers.ToListModel();
            return View(model);
        }

        /// <summary>
        /// Lấy máy in mặc đinh để in trên server theo thứ tự ưu tiên:
        /// - máy in cán bộ tự cấu hình
        /// - máy in admin cấu hình cho người dùng/phòng ban
        /// - máy in chung cho cả hệ thống
        /// - máy in đầu tiên tìm được
        /// </summary>
        /// <returns></returns>
        private Printer GetDefaultPrinter(int printerId = 0)
        {
            if (printerId != 0)
            {
                return _printerService.Get(printerId);
            }
            var userSettingModel = _helperUserSetting.GetUserCurrentSetting();
            if (userSettingModel.PrinterId != 0)
            {
                return _printerService.Get(userSettingModel.PrinterId);
            }
            return _printerService.GetDefaultPrinter();
        }

        private ReportDocument GetReport(List<int> docCopyIds, int templateId = 0, int suppId = 0)
        {
            Template template;
            if (!docCopyIds.Any())
            {
                throw new ArgumentNullException();
            }
            //var fileName = DateTime.Now.ToString("ddMMyyyy_hhmmss") + Guid.NewGuid().ToString() + ".jpg";
            var lookup = "http://dichvucong.baria-vungtau.gov.vn/tra-cuu-tien-do?doccode=";
            var documentCopyId = docCopyIds.First();
            var docCopy = _docCopyService.Get(documentCopyId);
            lookup = lookup + docCopy.Document.DocCode;
            var seach = generateQrCode(lookup);
            //SaveImage(PicQrCode(lookup), fileName);


            template = _templateService.GetTemplateForPrint(templateId, docCopy.DocTypeId, docCopy.Document.ListDocFieldId);

            if (template == null || string.IsNullOrWhiteSpace(template.ContentFileLocalName))
            {
                throw new ArgumentNullException(_resourceService.GetResource("Document.NotExistTemplate"));
            }
             
            var rptPath = System.IO.Path.Combine(ResourceLocation.Default.CrystalReport, template.ContentFileLocalName);
            if (!System.IO.File.Exists(rptPath))
            {
                throw new ArgumentNullException(_resourceService.GetResource("Document.NotExistTemplate"));
            }

            var rd = new ReportDocument();
            rd.Load(rptPath);
            var ds = _templateHelper.CreateDataSet(rd, template, docCopyIds, CurrentUserId(), suppId, seach);

            rd.SetDataSource(ds);

            return rd;
        }

        /// <summary>
        /// In ra máy
        /// </summary>
        /// <param name="rd">ReportDocument</param>
        /// <param name="printerName">tên máy in</param>
        private void PrintToPrinter(ReportDocument rd, string printerName, short copies = 1, bool landscape = true)
        {
            var printLayout = new PrintLayoutSettings();
            var printerSettings = new PrinterSettings();
            printerSettings.PrinterName = printerName;
            printerSettings.Copies = copies;
            var pageSettings = new PageSettings(printerSettings);
            pageSettings.Landscape = landscape;
            rd.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
            rd.PrintOptions.PrinterDuplex = PrinterDuplex.Simplex;
#if DEBUG
            var tempPath = System.IO.Path.Combine(ResourceLocation.Default.FileTemp, "Print");
            var stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            FileManager.Default.Create(stream, tempPath, null, ".pdf");
#else
            //cho ra file pdf để test xem nội dung
            rd.PrintToPrinter(printerSettings, pageSettings, false, printLayout);
#endif
            rd.Close();
        }

        private int CurrentUserId()
        {
            return _userService.CurrentUser.UserId;
        }


        private byte[] generateQrCode(string text)
        {
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(0);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {

                        var image = qrCode.GetGraphic(20, Color.Black, Color.White,
                            GetIconBitmap(), (int)0);
                        var byteImages = ImageToByte(image);
                        return byteImages;
                    }
                }
            }
        }

        private Bitmap PicQrCode(string text)
        {
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(0);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {

                        var image = qrCode.GetGraphic(20, Color.Black, Color.White,
                            GetIconBitmap(), (int)0);
                        return image;
                    }
                }
            }
        }

        private Bitmap GetIconBitmap()
        {
            Bitmap img = null;
            
            return img;
        }

        public byte[] ImageToByte(Bitmap img)
        {
            if (img != null)
            {
                MemoryStream fs = new MemoryStream();
                img.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] retval = fs.ToArray();
                fs.Dispose();
                return retval;
            }
            return null;
        }
        private void SaveImage(Bitmap img, string filename)
        {
            var folderPath = System.IO.Path.Combine(ResourceLocation.Default.FileTemp, filename);
            img.Save(folderPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}