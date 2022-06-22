using System;
using System.Collections.Generic;
using System.IO;
using DebenuPDFLibraryDLL1011;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Bkav.eGovCloud.Core.Utils;
using System.Drawing;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    ///
    /// </summary>
    public class PdfParser
    {
        private PDFLibrary _converter;
        int DPLVer = 1011;

        //0 = BMP output
        //1 = JPEG output
        //2 = WMF output
        //3 = EMF output
        //4 = EPS output
        //5 = PNG output
        //6 = GIF output
        //7 = TIFF (LZW) output
        //8 = EMF+ output
        //9 = HTML5 output
        //10 = TIFF (G4) output
        private int OUTPUT_OPTION = 0;

        private int IMAGE_DPI = 96;
        private const string OUTPUT_EXTENSION = ".png";

        /// <summary>
        /// Default
        /// </summary>
        public PdfParser()
        {

        }

        /// <summary>
        /// Contructer, truyền vào config khi render ảnh
        /// </summary>
        /// <param name="JPEGQuality"></param>
        public PdfParser(int JPEGQuality)
        {
            _converter = GetPdfParser();
            if (_converter != null)
            {
                _converter.SetJPEGQuality(JPEGQuality);
            }

            //_converter.RenderingThreads = 1;
            //_converter.TextAlphaBit = 4;
            //_converter.GraphicsAlphaBit = 4;
            //_converter.OutputToMultipleFile = true;
            //_converter.FitPage = true;
            //_converter.JPEGQuality = 48;
            //_converter.OutputFormat = "jpeg";
        }

        #region Extract Text

        /// <summary>
        ///
        /// </summary>
        /// <param name="inFileName"></param>
        /// <returns></returns>
        public string ExtractText(string inFileName)
        {
            return ExtractText(new PdfReader(inFileName));
        }

        /// <summary>
        /// Extracts a text from a PDF file.
        /// </summary>
        /// <param name="file">Stream pdf file.</param>
        /// <returns>the extracted text</returns>
        public string ExtractText(Stream file)
        {
            return ExtractText(new PdfReader(file));
        }
        
        #endregion ExtractText

        #region Extract Image

        /// <summary>
        /// Convert pdf stream ra ảnh và trả về danh sách đường dẫn các ảnh vừa được sinh ra.
        /// </summary>
        /// <param name="pdfPath">Đường dẫn file pdf</param>
        /// <param name="savePath">Đường dẫn thư mục lưu các file ảnh được sinh ra</param>
        /// <param name="numberOfStartPages">Trang bắt đầu convert</param>
        /// <param name="numberOfEndPage">Trang kết thúc convert</param>
        /// <param name="dpi">Độ phân giải</param>
        /// <param name="outputOption">
        /// Loại file:
        /// <para> 0 = BMP output </para>
        /// <para> 1 = JPEG output </para>
        /// <para> 2 = WMF output </para>
        /// <para>  3 = EMF output </para>
        /// <para> 4 = EPS output </para>
        /// <para> 5 = PNG output </para>
        /// <para> 6 = GIF output </para>
        /// <para> 7 = TIFF (LZW) output </para>
        /// <para> 8 = EMF+ output </para>
        /// <para> 9 = HTML5 output </para>
        /// <para> 10 = TIFF (G4) output </para>
        /// </param>
        /// <returns></returns>
        public List<string> ConvertToImages(string pdfPath, string savePath, int numberOfStartPages, int numberOfEndPage, int dpi = 96, int outputOption = 5)
        {
            var result = new List<string>();

            IMAGE_DPI = dpi;
            OUTPUT_OPTION = outputOption;

            _converter.LoadFromFile(pdfPath, Password: "");
            result = RenderImages(savePath, numberOfStartPages, numberOfEndPage);
            return result;
        }

        /// <summary>
        /// Convert pdf stream ra ảnh và trả về danh sách đường dẫn các ảnh vừa được sinh ra.
        /// </summary>
        /// <param name="stream">Stream của pdf</param>
        /// <param name="savePath">Đường dẫn thư mục lưu các file ảnh được sinh ra</param>
        /// <param name="numberOfStartPages">Trang bắt đầu convert</param>
        /// <param name="numberOfEndPage">Trang kết thúc convert</param>
        /// <param name="totalPages">Tổng số trang của file</param>
        /// <returns></returns>
        public List<string> ConvertToImages(Stream stream, string savePath, out int totalPages, int numberOfStartPages = 1, int? numberOfEndPage = null)
        {
            var result = new List<string>();
            if (_converter == null)
            {
                totalPages = 0;
                return result;
            }

            _converter.LoadFromString(stream.ToByte(), Password: "");
            totalPages = _converter.PageCount();

            if (!numberOfEndPage.HasValue)
            {
                // Nếu không gán endpage thì convert tất cả
                numberOfStartPages = totalPages;
                numberOfEndPage = 0;
            }

            result = RenderImages(savePath, numberOfStartPages, numberOfEndPage.Value);
            return result;
        }

        /// <summary>
        /// Convert pdf stream ra ảnh và trả về danh sách đường dẫn các ảnh vừa được sinh ra.
        /// </summary>
        /// <param name="totalPages">Tổng số trang của file</param>
        /// <param name="stream">Stream của pdf</param>
        /// <param name="savePath">Đường dẫn thư mục lưu các file ảnh được sinh ra</param>
        /// <param name="fromPage">Convert từ trang</param>
        /// <param name="pageSize">Số trang convert</param>
        /// <returns></returns>
        public List<string> ConvertToImages(out int totalPages, Stream stream, string savePath, int fromPage, int pageSize = 5)
        {
            var result = new List<string>();
            _converter = GetPdfParser();
            if (_converter == null)
            {
                totalPages = 0;
                return result;
            }

            var toPage = fromPage + pageSize;
            var name = Guid.NewGuid().ToString();
            var output = string.Format("{0}\\{1}", savePath, name + OUTPUT_EXTENSION);

            _converter.LoadFromString(stream.ToByte(), Password: "");

            totalPages = _converter.PageCount();
            if (toPage > totalPages)
            {
                toPage = totalPages;
            }

            var rendered = _converter.RenderDocumentToFile(IMAGE_DPI, fromPage, toPage, OUTPUT_OPTION, output);
            if (rendered == 0)
            {
                throw new Exception("Cannot convert");
            }

            for (var i = fromPage; i <= toPage; i++)
            {
                result.Add(name + i + OUTPUT_EXTENSION);
            }

            return result;
        }

        /// <summary>
        /// Convert pdf stream ra ảnh và trả về danh sách đường dẫn các ảnh vừa được sinh ra.
        /// </summary>
        /// <param name="totalPages">Tổng số trang của file</param>
        /// <param name="pdfPath">Đường dẫn của pdf</param>
        /// <param name="savePath">Đường dẫn thư mục lưu các file ảnh được sinh ra</param>
        /// <param name="fromPage">Convert từ trang</param>
        /// <param name="pageSize">Số trang convert</param>
        /// <returns></returns>
        public List<string> ConvertToImages(out int totalPages, string pdfPath, string savePath, int fromPage, int pageSize = 5)
        {
            var result = new List<string>();
            if (_converter == null)
            {
                totalPages = 0;
                return result;
            }

            var toPage = fromPage + pageSize;
            var name = Guid.NewGuid().ToString();
            var output = string.Format("{0}\\{1}", savePath, name + OUTPUT_EXTENSION);

            _converter.LoadFromFile(pdfPath, Password: "");

            totalPages = _converter.PageCount();
            if (toPage > totalPages)
            {
                toPage = totalPages;
            }

            _converter.RenderDocumentToFile(IMAGE_DPI, fromPage, toPage, OUTPUT_OPTION, output);
            for (var i = fromPage; i <= toPage; i++)
            {
                result.Add(name + i + OUTPUT_EXTENSION);
            }

            return result;
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Trả về thư viện pdf parser theo nhân hệ điều hành
        /// </summary>
        /// <returns></returns>
        public PDFLibrary GetPdfParser()
        {
            var DLLprefix = "DebenuPDFLibraryDLL"; // 32bit
            var DLL64prefix = "DebenuPDFLibrary64DLL"; // 64bit

            string dllName;

            // Check to see if IntPtr size is 4.
            // If 4 then it's 32-bit, if 8 then
            // it is 64-bit.
            if (IntPtr.Size == 4)
            {
                dllName = DLLprefix + DPLVer.ToString("D4") + ".DLL"; // 32 bits 
            }
            else
            {
                dllName = DLL64prefix + DPLVer.ToString("D4") + ".DLL"; // 64 bits 
            }

            // Load the library
            var path = CommonHelper.MapPath("/Content/Debenu");
            var mapPath = path + "//";
            var result = new PDFLibrary(mapPath + dllName);
            int iResult = result.UnlockKey("jb3fh4ja8ro87f4ux6373u38y");
            if (iResult == 1)
            {
                return result;
            }

            return null;
        }

        private List<string> RenderImages(string savePath, int numberOfStartPages, int numberOfEndPage)
        {
            var result = new List<string>();
            var name = Guid.NewGuid().ToString();
            var output = string.Format("{0}\\{1}", savePath, name + OUTPUT_EXTENSION);

            var pageNumb = _converter.PageCount();
            if (numberOfStartPages >= 1)
            {
                if (numberOfStartPages > pageNumb)
                {
                    numberOfStartPages = pageNumb;
                }

                // Chi tiet tai: http://www.debenu.com/docs/pdf_library_reference/RenderPageToFile.php
                _converter.RenderDocumentToFile(IMAGE_DPI, 1, numberOfStartPages, OUTPUT_OPTION, output);
                for (var i = 1; i <= numberOfStartPages; i++)
                {
                    result.Add(name + i + OUTPUT_EXTENSION);
                }
            }

            if (numberOfEndPage >= 1)
            {
                if (numberOfEndPage > pageNumb)
                {
                    numberOfEndPage = pageNumb;
                }

                var startEndPage = pageNumb - numberOfEndPage;
                if (startEndPage <= numberOfStartPages)
                {
                    startEndPage = numberOfStartPages + 1;
                }

                if (startEndPage <= pageNumb)
                {
                    _converter.RenderDocumentToFile(IMAGE_DPI, startEndPage, pageNumb, OUTPUT_OPTION, output);
                    for (var i = startEndPage; i <= pageNumb; i++)
                    {
                        result.Add(name + i + OUTPUT_EXTENSION);
                    }
                }
            }

            return result;
        }

        private static string ExtractText(PdfReader reader)
        {
            try
            {
                var result = string.Empty;
                for (var page = 1; page <= reader.NumberOfPages; page++)
                {
                    result += PdfTextExtractor.GetTextFromPage(reader, page) + " ";
                }
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
    }
}