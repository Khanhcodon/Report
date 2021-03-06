using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using System.Drawing;
using Microsoft.Office.Interop;
//using Syncfusion.Pdf;
//using Syncfusion.DocIO;
//using Syncfusion.DocIO.DLS;
//using Syncfusion.DocToPDFConverter;
//using GemBox.Spreadsheet;
using OfficeOpenXml;
using System.Text;
using Bkav.eGovCloud.Business;
using ClosedXML.Excel;
using OfficeOpenXml.Style;
using System.Text.RegularExpressions;

namespace Bkav.eGovCloud.Controllers
{
    public class AttachmentPreviewController : CustomerBaseController
    {
        private readonly string[] _readableExtensions = new string[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
        private readonly string[] _imageExtensions = new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
        private readonly AttachmentBll _attachmentService;
        private readonly UserBll _userService;
        private readonly SsoSettings _ssoSettings;

        private const int DEFAULT_PAGE = 5;

        public AttachmentPreviewController(AttachmentBll attachmentService, UserBll userService)
        {
            _attachmentService = attachmentService;
            _userService = userService;
            _ssoSettings = SsoSettings.Instance;
        }

        public ActionResult Index(Guid DocumentId, int AttachmentId)
        {
            var attachments = _attachmentService.Gets(DocumentId);

            attachments = attachments.Where(a => !a.IsDeleted);
            var model = attachments.ToListModel();

            ViewBag.CurrentFileId = AttachmentId;
            ViewBag.DocumentId = DocumentId;
            //var attachments1 = model.Stringify();
            ViewBag.Attachments = model.Stringify();
            ViewBag.ParentDomain = _ssoSettings.BkavSSOParentDomain;
            return View();
        }

        

        public JsonResult GetPages(Guid documentId, int attachmentId, int fromPage, string fileName, int? version = null)
        {
            var failResult = new FilePreviewParser() { IsSuccess = false, AttachmentId = attachmentId };
            var extension = Path.GetExtension(fileName).ToLower();
            if (!_readableExtensions.Contains(extension))
            {
                return Json(failResult, JsonRequestBehavior.AllowGet);
            }

            var temp = GetTempFolder(documentId);

            //var basePath = DirectoryUtil.ToAbsoluteAndEnsureExist(Path.Combine(ResourceLocation.Default.FileTemp, temp));

            var basePath = DirectoryUtil.ToAbsoluteAndEnsureExist(Path.Combine(Path.GetTempPath(), temp));
            if (!Directory.Exists(basePath) || fromPage < 1)
            {
                return Json(failResult, JsonRequestBehavior.AllowGet);
            }

            //var baseUrlPath = Url.Content(Path.Combine("../Temp/", temp));
            //Thay bang duong dan file temp he thong
            var baseUrlPath = Url.Content(Path.Combine(Path.GetTempPath(), temp));
            var attachFile = Path.Combine(basePath, fileName);
            Stream fileStream = null;

            // Nếu là file ảnh hoặc file khác nhưng chưa convert sang pdf.
            if (_imageExtensions.Contains(extension)
                || (!_imageExtensions.Contains(extension) && !System.IO.File.Exists(attachFile)))
            {
                var downloads = _attachmentService.GetAttachments(new List<int>() { attachmentId }, version);
                if (!downloads.Any())
                {
                    return Json(failResult, JsonRequestBehavior.AllowGet);
                }

                // lấy First do chỉ truyền vào 1 attachmentid;
                fileStream = downloads.First().Value;
            }

            if (_imageExtensions.Contains(extension))
            {
                var file = FileManager.Default.Create(fileStream, basePath, fileName, extension);
                return Json(new FilePreviewParser()
                {
                    IsSuccess = true,
                    Files = new List<string>() { Url.Content(Path.Combine(baseUrlPath, file.Name)) },
                    Total = 1,
                    AttachmentId = attachmentId,
                    PdfOutput = ""
                }, JsonRequestBehavior.AllowGet);
            }

            if (!System.IO.File.Exists(attachFile))
            {
                FileManager.Default.Create(fileStream, basePath, fileName);
            }

            var result = ParseFileTemp(attachFile, fileName, fromPage, basePath, temp);
            result.AttachmentId = attachmentId;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Print(string file, bool print)
        {
            ViewBag.FilePath = file;
            ViewBag.Print = print;
            return View();
        }

        public void DeleteTemp(Guid documentId)
        {
            // Tạm bỏ do ko hiểu sao sau khi xóa thì phải khởi tạo lại web từ đầu.
            //var temp = GetTempFolder(documentId);
            //var basePath = DirectoryUtil.ToAbsoluteAndEnsureExist(Path.Combine(ResourceLocation.Default.FileTemp, temp));
            //try
            //{
            //    Directory.Delete(basePath, recursive: true);
            //}
            //catch { }
        }

        public ActionResult PreviewSigned(string filePaths)
        {
            ViewBag.SignedFiles = Json2.ParseAs<IEnumerable<string>>(filePaths);
            return View();
        }

        private FilePreviewParser ParseFileTemp(string attachFile, string fileName, int fromPage, string basePath, string temp)
        {
            var files = new List<string>();
            var extension = Path.GetExtension(fileName).ToLower();
            var baseUrlPath = Url.Content(Path.Combine(Path.GetTempPath(), temp));
            var outputPdf = Path.GetFileNameWithoutExtension(fileName) + ".pdf";
            var pdfOutput = Path.Combine(basePath, outputPdf);
            int total;
            var isSuccess = true;
            var newListObjet = new List<object>();
            var kqHtml = "";
            try
            {
                switch (extension)
                {
                    case ".pdf":
                        files.AddRange(ConvertStreamPdfToImages(attachFile, fromPage, DEFAULT_PAGE, basePath, temp, out total));
                        break;
                    case ".doc":
                    case ".docx":
                        if (!System.IO.File.Exists(pdfOutput))
                        {
                            //DocxParser.ConvertToPdf(attachFile, pdfOutput);
                            //WordDocument wordDocument = new WordDocument(attachFile, FormatType.Docx);
                            //DocToPDFConverter converter = new DocToPDFConverter();
                            //PdfDocument pdfDocument = converter.ConvertToPDF(wordDocument);
                            //pdfDocument.Save(pdfOutput);
                            //pdfDocument.Close(true);
                            //wordDocument.Close(); 
                            System.Diagnostics.Process.Start(pdfOutput);
                        }
                        files.AddRange(ConvertStreamPdfToImages(pdfOutput, fromPage, DEFAULT_PAGE, basePath, temp, out total));
                        break;
                    case ".xls":
                    case ".xlsx":

                        //read file excel
                        total = 0; // check test
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelPackage xlPackage = new ExcelPackage(new System.IO.FileInfo(attachFile));
                        string html = "";
                        int workBooks = 0;

                        using (xlPackage)
                        {
                            var workbook = xlPackage.Workbook;
                            if (workbook != null)
                            {
                                if (workbook.Worksheets.Count > 1)
                                {
                                    workBooks = 1;
                                }
                                else
                                    workBooks = workbook.Worksheets.Count;

                                for (int j = 0; j < workBooks; j++)
                                {
                                    html += "<table border = '1' cellspacing = '0' class='table table-bordered table-striped table-main persist-area' style='border-collapse: collapse;font-family:arial;'>";
                                    var worksheet = workbook.Worksheets[j];

                                    for (int m = worksheet.Dimension.Start.Row; m <=
                                   worksheet.Dimension.End.Row; m++)
                                    {
                                        bool isRowEmpty = true;
                                        for (int k = worksheet.Dimension.Start.Column; k <= worksheet.Dimension.End.Column; k++)
                                        {
                                            if (worksheet.Cells[m, k].Value != null)
                                            {
                                                isRowEmpty = false;
                                                break;
                                            }
                                        }
                                        if (isRowEmpty)
                                        {
                                            worksheet.DeleteColumn(m);
                                            worksheet.DeleteRow(m);
                                        }
                                    }

                                    if (worksheet.Dimension == null) { continue; }
                                    int rowCount = 0;
                                    int maxColumnNumber = worksheet.Dimension.End.Column;
                                    var convertedRecords = new List<List<string>>(worksheet.Dimension.End.Row);
                                    var excelRows = worksheet.Cells.GroupBy(c => c.Start.Row).ToList();
                                    excelRows.ForEach(r =>
                                    {
                                        rowCount++;
                                        if (rowCount == 1) html += String.Format("<tbody>");
                                        //if (rowCount == 2) html += String.Format("<tbody>");
                                        html += String.Format("<tr>");
                                        var currentRecord = new List<string>(maxColumnNumber);
                                        var cells = r.OrderBy(cell => cell.Start.Column).ToList();
                                        for (int i = 1; i <= maxColumnNumber; i++)
                                        {
                                            Double rowHeight = worksheet.Row(i).Height;
                                            Double rowWidth = worksheet.Column(i).Width;
                                            var currentCell = cells.Where(c => c.Start.Column == i).FirstOrDefault();
                                            int colSpan = 1;
                                            int rowSpan = 1;
                                            var alginText = "";
                                            
                                            if (currentCell != null)
                                            {
                                                alginText = getFormat(currentCell.Style.HorizontalAlignment.ToString());
                                                bool boldText = currentCell.Style.Font.Bold;
                                                bool italicText = currentCell.Style.Font.Italic;
                                                var underLineText = currentCell.Style.Font.UnderLine.ToString();
                                                var border = currentCell.Style.Border;
                                                var borderBottom = border.Bottom.Style.ToString();
                                                var borderTop = border.Top.Style.ToString();
                                                var borderLeft = border.Left.Style.ToString();
                                                var borderRight = border.Right.Style.ToString();
                                                var checkLeftRight = currentCell.Style.HorizontalAlignment.ToString();
                                                var checkCenter = currentCell.Style.VerticalAlignment.ToString();

                                                #region
                                                if (checkLeftRight == "General") {
                                                    Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
                                                    double number;
                                                    Double.TryParse(currentCell.Text, out number);
                                                    if (regex.IsMatch(currentCell.Text) || number > 0) {
                                                        alginText += " text-alignRight";
                                                    }
                                                }
                                                if (borderBottom != "None")
                                                {
                                                    alginText += " border-bottom";
                                                }
                                                if (borderTop != "None")
                                                {
                                                    alginText += " border-top";
                                                }
                                                if(borderLeft != "None")
                                                {
                                                    alginText += " border-left";
                                                }
                                                if(borderRight != "None")
                                                {
                                                    alginText += " border-right";
                                                }
                                                if (boldText == true)
                                                {
                                                    alginText += " htBold";
                                                }
                                                if (italicText == true)
                                                {
                                                    alginText += " htItalic";
                                                }
                                                if (underLineText != "None")
                                                {
                                                    alginText += " htUnderline";
                                                }
                                                #endregion
                                                ExcelAddress cellAddress = new ExcelAddress(currentCell.Address);

                                                var mCellsResult = (from c in worksheet.MergedCells
                                                                    let addr = new ExcelAddress(c)
                                                                    where cellAddress.Start.Row >= addr.Start.Row &&
                                                                    cellAddress.End.Row <= addr.End.Row &&
                                                                    cellAddress.Start.Column >= addr.Start.Column &&
                                                                    cellAddress.End.Column <= addr.End.Column
                                                                    select addr);

                                                if (mCellsResult.Count() > 0)
                                                {
                                                    var mCells = mCellsResult.First();

                                                    if (mCells.Start.Address != cellAddress.Start.Address)
                                                        continue;

                                                    if (mCells.Start.Column != mCells.End.Column)
                                                    {
                                                        colSpan += mCells.End.Column - mCells.Start.Column;
                                                    }

                                                    if (mCells.Start.Row != mCells.End.Row)
                                                    {
                                                        rowSpan += mCells.End.Row - mCells.Start.Row;
                                                    }
                                                }
                                            }
                                            //load up data
                                            if(currentCell == null)
                                            {
                                                var strNull = "";
                                                if (rowCount == 1)
                                                    html += String.Format("<th width='{4}' height='{5}' class='{3}' colspan={0} rowspan={1}>{2}</th>", colSpan, rowSpan, strNull, alginText, rowWidth, rowHeight);
                                                else
                                                    html += String.Format("<td width='{4}' height='{5}' class='{3}' colspan={0} rowspan={1}>{2}</td>", colSpan, rowSpan, strNull, alginText, rowWidth, rowHeight);
                                            }else
                                            {
                                                if (rowCount == 1)
                                                    html += String.Format("<th width='{4}' height='{5}' class='{3}' colspan={0} rowspan={1}>{2}</th>", colSpan, rowSpan, currentCell.Value, alginText, rowWidth, rowHeight);
                                                else
                                                    html += String.Format("<td width='{4}' height='{5}' class='{3}' colspan={0} rowspan={1}>{2}</td>", colSpan, rowSpan, currentCell.Value, alginText, rowWidth, rowHeight);
                                            }    
                                        }
                                        html += String.Format("</tr>");
                                        if (rowCount == 1) html += String.Format("</thead>");

                                    });

                                    html += String.Format("</tbody></table>");
                                }
                            }
                        }
                        kqHtml = html;
                        //if (!System.IO.File.Exists(pdfOutput))
                        //{
                        //    XlsxLoadOptions loadOptions = new XlsxLoadOptions();
                        //    SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                        //    ExcelFile excel = ExcelFile.Load(attachFile, loadOptions);

                        //    PdfSaveOptions saveOptions = new PdfSaveOptions();
                        //    excel.Save(pdfOutput, saveOptions);
                        //    //XlsxParser.ConvertToPdf(attachFile, pdfOutput);

                        //}
                        //files.AddRange(ConvertStreamPdfToImages(pdfOutput, fromPage, DEFAULT_PAGE, basePath, temp, out total));
                        break;
                    default:
                        outputPdf = string.Empty;
                        total = 0;
                        isSuccess = false;
                        break;
                }
            }
            catch(Exception ex)
            {
                LogException(ex);
                total = 0;
                isSuccess = false;
            }

            var result = new FilePreviewParser()
            {
                IsSuccess = isSuccess,
                Files = files.Select(n => Url.Content(Path.Combine(baseUrlPath, n))),
                PdfOutput = outputPdf == "" ? "" : Path.Combine(baseUrlPath, outputPdf),
                Total = total,
                HtmlOutput = kqHtml,
                checkTypeFile = extension
            };

            return result;
        }


        public string getFormat(string align)
        {
            string result = "";
            switch (align)
            {
                case "Center":
                    result = "htCenter";
                    break;
                case "Left":
                    result = "htLeft";
                    break;
                case "Right":
                    result = "htRight";
                    break;
                default:
                    result = "htLeft";
                    break;
            }
            return result;
        }

        private static bool isFirstMergeRange(OfficeOpenXml.ExcelWorksheet sheet, string address, ref int colspan, ref int rowspan)
        {
            colspan = 1;
            rowspan = 1;
            foreach (var item in sheet.MergedCells)
            {
                var s = item.Split(':');
                if (s.Length > 0 && s[0].Equals(address))
                {

                    ExcelRange range = sheet.Cells[item];
                    colspan = range.End.Column - range.Start.Column;
                    rowspan = range.End.Row - range.Start.Row;
                    if (colspan == 0) colspan = 1;
                    if (rowspan == 0) rowspan = 1;
                    return true;
                }
            }
            return false;
        }

        private List<string> ConvertStreamPdfToImages(Stream stream, int fromPage, int pageSize, string basePath, string temp, out int total)
        {
            var pdfParser = new PdfParser(JPEGQuality: 8);
            var result = pdfParser.ConvertToImages(out total, stream, basePath, fromPage, pageSize);

            return result;
        }

        private List<string> ConvertStreamPdfToImages(string pdfPath, int fromPage, int pageSize, string basePath, string temp, out int total)
        {
            var pdfParser = new PdfParser(JPEGQuality: 8);
            var result = pdfParser.ConvertToImages(out total, pdfPath, basePath, fromPage, pageSize);
            return result;
        }

        private string GetTempFolder(Guid documentId)
        {
            return string.Format("{0}-{1}", documentId.ToString("N"), User.GetUserId());
        }
    }

    public class FilePreviewParser
    {
        public string checkTypeFile { get; set; }
        public string HtmlOutput { get; set;}

        public bool IsSuccess { get; set; }

        public string PdfOutput { get; set; }

        public int Total { get; set; }

        public int AttachmentId { get; set; }

        public IEnumerable<string> Files { get; set; }

        public IEnumerable<string> Base64OutputString
        {
            get
            {
                List<string> listBase64 = new List<string>();
                if(Files.Count() > 0 && Files.Any())
                {
                    foreach (string file in Files)
                    {
                        var basePath = Path.Combine(Path.GetTempPath(), file);
                        string base64String = "data:image/png;base64," + ConvertImageToBase64(basePath);
                        listBase64.Add(base64String);
                    }
                }
                return listBase64;
            }
        }

        private string ConvertImageToBase64(string path)
        {
            string base64String = "";
            using (Image image = Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                }
            }
            return base64String;
        }

    }
}