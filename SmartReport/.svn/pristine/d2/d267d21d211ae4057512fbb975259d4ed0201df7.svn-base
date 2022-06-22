using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class DownloadController : BaseController
    {
        private readonly AttachmentBll _attachmentService;

        public DownloadController(AttachmentBll attachmentService)
        {
            _attachmentService = attachmentService;
        }

        public ActionResult Main()
        {
            return View();
        }

        public FileResult TestDownload()
        {
            string fileName;
            var lastAttactment = _attachmentService.Gets().LastOrDefault();
            var stream = _attachmentService.TestDownloadAttachment(out fileName, lastAttactment.AttachmentId);
            return File(stream, MediaTypeNames.Application.Octet, fileName);
        }

        public FileResult Test1Download()
        {
            string fileName;
            var lastAttactment = _attachmentService.Gets().LastOrDefault();
            var path = _attachmentService.GetAttachmentPath(out fileName, lastAttactment.AttachmentId);

            return File(path, MediaTypeNames.Application.Octet, fileName);
        }

        public FileResult Test2Download()
        {
            string fileName;
            var lastAttactment = _attachmentService.Gets().LastOrDefault();
            var path = _attachmentService.GetAttachmentPath(out fileName, lastAttactment.AttachmentId);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            return File(stream, MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        [ValidateInput(false)]
        public string Export(int type, string name, string content)
        {
            var fileName = "";

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

            }

            return "/Temp/" + fileName;
        }

        private string ExportToDocx(string name, string content)
        {
            var fileName = GetFileName(name, 2);
            var path = Path.Combine(ResourceLocation.Default.FileTemp, fileName);
            HTMLtoDOCX.CreateFileFromHTML(content, path);

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

        private string GetFileName(string name, int type)
        {
            var fileExtension = type == 1 ? "pdf" : (type == 2 ? "docx" : "xlsx");
            return string.Format("{0}_{1}.{2}", name.Trim().StripVietnameseChars(), DateTime.Now.ToString("ddMMyyhhmm"), fileExtension);
        }

        public FileResult EOfficePlusPlugin()
        {
            // CuongNT - 02/08/2016: Sửa để cho phép tải trực tiếp file exe về sẽ tiện hơn khi click vào là có thể cài luôn.
            return File(FileManager.Default.Open(Server.MapPath("~/Content/Plugin/BkaveGov_FFPlugin_ChromNativeApp-1.0.2.exe")),
                        System.Net.Mime.MediaTypeNames.Application.Octet, "BkaveGov_FFPlugin_ChromNativeApp-1.0.2.exe");
        }
    }
}