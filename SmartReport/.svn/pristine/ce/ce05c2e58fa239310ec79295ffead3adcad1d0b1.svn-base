using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using Tesseract;
using System.Text.RegularExpressions;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    /// Convert image to texxt
    /// </summary>
    public class ImageToText
    {
        private IDictionary<string, string> specials;
        private IDictionary<string, string> organizationMaps;
        private List<string> Categories = new List<string>() { "BÁO CÁO", "TỜ TRÌNH", "CHỈ THỊ", "KẾ HOẠCH", "THÔNG BÁO", "ĐỀ NGHỊ", "QUYẾT ĐỊNH", "QUY ĐỊNH", "HỢP ĐỒNG",  
                                                                "ĐĂNG KÝ", "BIÊN BẢN", "QUY TRÌNH", "HƯỚNG DẪN", "KIẾN NGHỊ", "THANH TRA", "GIẤY MỜI", "THÔNG TƯ"};

        private string _DATA = CommonHelper.MapPath(@"~/tessdata", false);
        private string _CONFIG = CommonHelper.MapPath(@"~/tessdata/config.json", false);
        private const string _LANGUAGE = "vie";

        /// <summary>
        /// Khởi tạo.
        /// </summary>
        public ImageToText()
        {
            specials = TesseractConfig["Specicals"];
            organizationMaps = TesseractConfig["OrganizationMaps"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPath"></param>
        /// <param name="configPath"></param>
        public ImageToText(string dataPath, string configPath)
        {
            _DATA = dataPath;
            _CONFIG = configPath;
            specials = TesseractConfig["Specicals"];
            organizationMaps = TesseractConfig["OrganizationMaps"];
        }

        private Dictionary<string, Dictionary<string, string>> TesseractConfig
        {
            get
            {
                var config = System.IO.File.ReadAllText(_CONFIG);
                if (config == "")
                {
                    config = "{}";
                }

                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(config);
                return result;
            }
        }

        /// <summary>
        /// Trả về text từ ảnh OCR.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="hasDeleteAfterParse">Xóa file sau khi lấy text</param>
        /// <returns></returns>
        public string GetText(string imagePath, bool hasDeleteAfterParse = true)
        {
            string result = "";
            Bitmap image = CropImage(imagePath);
            using (var engine = new TesseractEngine(_DATA, _LANGUAGE, EngineMode.Default))
            {
                using (var img = PixConverter.ToPix(image))
                {
                    using (var page = engine.Process(img))
                    {
                        result = page.GetText();
                        page.Dispose();
                    }

                    img.Dispose();
                }

                engine.Dispose();
            }

            if (hasDeleteAfterParse)
            {
                try
                {
                    System.IO.File.Delete(imagePath);
                }
                catch
                {

                }
            }

            return result;
        }

        /// <summary>
        /// Cắt ảnh gốc, chỉ lấy 1/3 trang giấy phần trên có các thông tin về Trích yếu, cqbh, số, ngày ban hành.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private Bitmap CropImage(string filePath)
        {
            var image = new Bitmap(filePath);

            var cropRect = new Rectangle(new Point(0, 0), new Size(2467, 1000));
            var src = Image.FromFile(filePath) as Bitmap;
            var result = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(image, new Rectangle(0, 0, result.Width, result.Height),
                                cropRect,
                                GraphicsUnit.Pixel);
            }

            return result;
        }

        /// <summary>
        /// Trả về thông tin văn bản lấy từ ảnh
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public DocumentInfoFromImage GetDocumentInfo(string imagePath)
        {
            var imageText = GetText(imagePath);

            return ParseDocumentInfo(imageText);
        }

        private DocumentInfoFromImage ParseDocumentInfo(string imageText)
        {
            var lines = SafeInput(imageText);
            var result = new DocumentInfoFromImage();

            result.Organization = GetCQBH(lines);
            result.DocCode = GetDocCode(lines);
            result.Compendium = GetCompendium(lines);
            result.DatePublished = GetDatePublish(lines);

            return result;
        }

        private DateTime? GetDatePublish(IEnumerable<string> lines)
        {
            DateTime? result = null;
            var regex = new Regex(", ngày [0-9]* tháng [0-9]* năm [0-9]*");
            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (string.IsNullOrWhiteSpace(match.ToString()))
                {
                    continue;
                }

                var dates = match.ToString().Split(new string[] { "ngày", "tháng", "năm" }, StringSplitOptions.RemoveEmptyEntries);
                result = new DateTime(int.Parse(dates[3]), int.Parse(dates[2]), int.Parse(dates[1]));

                break;
            }
            return result;
        }

        private string GetCompendium(IEnumerable<string> lines)
        {
            var result = "";
            var compendium = new List<string>();
            var hasCompendium = false;
            var lineCount = 0;
            foreach (var line in lines)
            {
                var uLine = line.ToLower();
                bool isCategory;
                if (IsCompendium(line, out isCategory))
                {
                    if (!isCategory)
                    {
                        compendium.Add(line);
                    }

                    hasCompendium = true;
                    continue;
                }

                if (!hasCompendium)
                {
                    continue;
                }

                if (uLine.Contains("kính gửi:")
                         || uLine.StartsWith("căn cứ", StringComparison.OrdinalIgnoreCase)
                         || uLine.StartsWith("thực hiện công", StringComparison.OrdinalIgnoreCase)
                         || uLine.StartsWith("thực hiện chỉ đạo", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (line.StartsWith("line--"))
                {
                    if (compendium.Count == 0)
                    {
                        continue;
                    }

                    lineCount++;
                    if (lineCount > 1)
                    {
                        break;
                    }
                    continue;
                }
                else
                {
                    lineCount = 0;
                }

                compendium.Add(line);
            }

            return result + string.Join(" ", compendium);
        }

        private bool IsCompendium(string line, out bool isCategory)
        {
            isCategory = false;
            // Công văn
            var result = line.StartsWith("v/v", StringComparison.OrdinalIgnoreCase)
                    || line.StartsWith("về việc", StringComparison.OrdinalIgnoreCase)
                    || line.StartsWith("vê việc", StringComparison.OrdinalIgnoreCase)
                    || line.StartsWith("vv", StringComparison.OrdinalIgnoreCase);

            if (result)
            {
                return result;
            }

            // Thể loại khác
            foreach (var category in Categories)
            {
                if (line.StartsWith(category))
                {
                    result = true;
                    isCategory = true;
                    break;
                }
            }

            return result;
        }

        private string GetCQBH(IEnumerable<string> lines)
        {
            var result = "";
            var cqbh = new List<string>();
            var regex = new Regex(@"[ -&\p{L}]*");
            foreach (var line in lines)
            {
                if (IsDocCode(line))
                {
                    break;
                }

                if (line.StartsWith("line--", StringComparison.OrdinalIgnoreCase)
                    || line.StartsWith("--", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var uline = line.Replace("_", "").Replace("*", "").Replace(".", "").Trim();
                if (string.IsNullOrEmpty(uline) || !IsAllUpper(uline))
                {
                    continue;
                }

                cqbh.Add(MapCQBH(uline));
            }

            if (cqbh.Count == 0)
            {
                return result;
            }

            if (cqbh.Count > 1)
            {
                // Xử lý các trường hợp:
                // - Cơ quan ban hành là cấp con của Sở, Huyện.
                // - Cơ quan ban hành từ trung ương.
                var firstLine = cqbh.First().ToLower();
                if (firstLine.StartsWith("ban") || firstLine.StartsWith("bộ") || firstLine.StartsWith("hội") || firstLine.StartsWith("btl "))
                {
                    result += " " + cqbh[0];
                }
                else if (firstLine.StartsWith("sở") || firstLine.Contains("ubnd"))
                {
                    cqbh.RemoveAt(0);
                    result += string.Join(" ", cqbh);
                }
                else
                {
                    result += string.Join(" ", cqbh);
                }
            }
            else
            {
                result += " " + cqbh[0];
            }

            return MapCQBH(result.Trim());
        }

        bool IsAllUpper(string input)
        {
            return input == input.ToUpper();
        }

        string MapCQBH(string cqbh)
        {
            var uCqbh = cqbh.Trim().StripVietnameseChars();

            foreach (var key in organizationMaps.Keys)
            {
                if (key.StripVietnameseChars().Equals(uCqbh))
                {
                    return organizationMaps[key];
                }
            }

            return cqbh;
        }

        private string GetDocCode(IEnumerable<string> lines)
        {
            var result = "";
            foreach (var line in lines)
            {
                if (IsDocCode(line))
                {
                    var docCodeTemp = line.Replace(".", "").Replace("'", "").Replace(" f", "/");
                    var doccode = ParseDocCode(docCodeTemp);
                    result += doccode;
                    break;
                }
            }

            return result;
        }

        private bool IsDocCode(string line)
        {
            if (line.ToLower().StartsWith("sở") || line.ToLower().StartsWith("sơ") || line.ToLower().StartsWith("sớ"))
            {
                return false;
            }

            var uline = line.StripVietnameseChars().ToLower();
            return uline.StartsWith("so ", StringComparison.OrdinalIgnoreCase)
                    || uline.StartsWith("so:", StringComparison.OrdinalIgnoreCase);
        }

        private string ParseDocCode(string line)
        {
            var regex = new Regex(@"[0-9-a-zA-Z]*/[-&\p{L}]*");
            var result = regex.Match(line);
            if (result != null)
            {
                return result.Value;
            }

            return "";
        }

        private List<string> SafeInput(string input)
        {
            var result = new List<string>();
            input = input.Replace("\n", "\rline--------------------------------\n");
            var lines = input.Split(new[] { '\r', '\n' }).ToList();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var l = line;
                foreach (var str in specials)
                {
                    l = l.Replace(str.Key, str.Value);
                }
                l = l.Trim();
                l = RemoveCommonWord(l.Trim(), "doc la");
                if (string.IsNullOrEmpty(l))
                {
                    continue;
                }
                l = RemoveCommonWord(l.Trim(), "cong hoa");
                if (string.IsNullOrEmpty(l))
                {
                    continue;
                }
                l = RemoveCommonWord(l.Trim(), "doan tncs");
                if (string.IsNullOrEmpty(l))
                {
                    continue;
                }
                l = RemoveCommonWord(l.Trim(), "dang cong san");
                if (string.IsNullOrEmpty(l))
                {
                    continue;
                }

                result.Add(l);
            }
            return result;
        }

        private string RemoveCommonWord(string input, string word)
        {
            var ul = input.StripVietnameseChars().ToLower();

            var chIdx = ul.IndexOf(word);
            if (chIdx == 0)
            {
                return "";
            }

            if (chIdx > 0)
            {
                return input.Substring(0, chIdx);
            }

            return input;
        }
    }

    /// <summary>
    /// Đối tượng thông tin lấy từ ảnh scan
    /// </summary>
    public class DocumentInfoFromImage
    {
        /// <summary>
        /// Cơ quan ban hành
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// Số ký hiệu
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Ngày ban hành
        /// </summary>
        public DateTime? DatePublished { get; set; }
    }
}
