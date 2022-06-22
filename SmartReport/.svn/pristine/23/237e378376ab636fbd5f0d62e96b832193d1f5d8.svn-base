using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using Bkav.eGovCloud.Core.DynamicForm;
using Newtonsoft.Json;
using System.Web;

namespace Bkav.eGovCloud.Core.Utils
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StringExtension - public - Core
    /// Access Modifiers: 
    /// Create Date : 100912
    /// Author      : TrungVH
    /// </author>
    /// <summary>
    /// <para>1 thư viện ở rộng cho việc xử lý chuỗi</para>
    /// (TrungVH@bkav.com - 100912)
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Loại bỏ các ký tự tiếng việt
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <returns>Chuỗi đã được loại bỏ các ký tự tiếng việt</returns>
        public static string StripVietnameseChars(this string input)
        {
            string[] vietnameseChars =
            {
                "áàảãạăắằẳẵặâấầẩẫậ",
                "ÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ",
                "đ",
                "Đ",
                "éèẻẽẹêếềểễệ",
                "ÉÈẺẼẸÊẾỀỂỄỆ",
                "íìỉĩị",
                "ÍÌỈĨỊ",
                "óòỏõọơớờởỡợôốồổỗộ",
                "ÓÒỎÕỌƠỚỜỞỠỢÔỐỒỔỖỘ",
                "ưứừửữựúùủũụ",
                "ƯỨỪỬỮỰÚÙỦŨỤ",
                "ýỳỷỹỵ",
                "ÝỲỶỸỴ"
            };

            var latinChars = new[]
            {
                'a',
                'A',
                'd',
                'D',
                'e',
                'E',
                'i',
                'I',
                'o',
                'O',
                'u',
                'U',
                'y',
                'Y'
            };
            return input.ReplaceCharGroups(vietnameseChars, latinChars);
        }

        /// <summary>
        /// Loại bỏ các ký tự phân cách
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <returns>Chuỗi đã được lại bỏ các ký tự phân cách</returns>
        public static string StripDelimiters(this string input)
        {
            var delimiters = new[] { '|', '"', '\'', ';', ',', '.' };
            return input.StripChars(delimiters);
        }

        /// <summary>
        /// Loại bỏ các ký tự xác định.
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <param name="strips">Mảng các ký tự cần loại bỏ.</param>
        /// <returns></returns>
        public static string StripChars(this string input, params char[] strips)
        {
            if (strips == null)
            {
                throw new ArgumentNullException("strips");
            }

            var scanner = new StringBuilder(input);
            var builder = new StringBuilder(scanner.Length);
            for (var i = 0; i < scanner.Length; i++)
            {
                if (strips.Any(c => scanner[i].Equals(c)))
                {
                    continue;
                }
                builder.Append(scanner[i]);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Loại bỏ các ký tự đặc biệt trong câu truy vấn của solr
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns></returns>
        public static string StripSpecialCharactersForSolr(this string input)
        {
            var solrSpecialCharacters = new[] { "+", "-", "&", "|", "!", "(", ")", "{", "}", "[", "]", "^", "\"", "~", "*", "?", ":", "\\" };
            var solrReplacementCharacters = new[] { "\\+", "\\-", "\\&", "\\|", "\\!", "\\(", "\\)", "\\{", "\\}", "\\[", "\\]", "\\^", "\\\"", "\\~", "\\*", "\\?", "\\:", "\\\\" };
            return ReplaceCharGroups(input, solrSpecialCharacters, solrReplacementCharacters);
        }

        /// <summary>
        /// Thay thế các nhóm ký tự bằng một nhóm ký tự khác
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <param name="strips">Mảng các chữ cần loại bỏ.</param>
        /// <param name="replacements">Mảng các chữ cần thay thế.</param>
        /// <returns></returns>
        public static string ReplaceCharGroups(this string input, string[] strips, char[] replacements)
        {
            if (strips == null)
            {
                throw new ArgumentNullException("strips");
            }
            if (replacements == null)
            {
                throw new ArgumentNullException("replacements");
            }
            if (strips.Length > replacements.Length)
            {
                throw new ArgumentException("Length of replacement array must be larger than strip array");
            }

            var scanner = new StringBuilder(input);
            for (var i = 0; i < scanner.Length; i++)
            {
                for (var j = 0; j < strips.Length; j++)
                {
                    if (strips[j].IndexOf(scanner[i]) != -1)
                    {
                        scanner[i] = replacements[j];
                    }
                }

            }
            return scanner.ToString();
        }

        /// <summary>
        /// Thay thế các nhóm ký tự bằng một nhóm ký tự khác
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <param name="strips">Mảng các chữ cần loại bỏ.</param>
        /// <param name="replacements">Mảng các chữ cần thay thế.</param>
        /// <returns></returns>
        public static string ReplaceCharGroups(this string input, string[] strips, string[] replacements)
        {
            if (strips == null)
            {
                throw new ArgumentNullException("strips");
            }
            if (replacements == null)
            {
                throw new ArgumentNullException("replacements");
            }
            if (strips.Length > replacements.Length)
            {
                throw new ArgumentException("Length of replacement array must be larger than strip array");
            }

            var scanner = input.Select(i => i.ToString(CultureInfo.InvariantCulture)).ToArray();
            for (var i = 0; i < scanner.Length; i++)
            {
                for (var j = 0; j < strips.Length; j++)
                {
                    if (strips[j].IndexOf(scanner[i], StringComparison.Ordinal) != -1)
                    {
                        scanner[i] = replacements[j];
                    }
                }

            }
            return string.Join("", scanner);
        }

        /// <summary>
        /// Loại bỏ các ký tự html
        /// </summary>
        /// <param name="source">Chuỗi</param>
        /// <returns>Chuỗi đã được loại bỏ ký tự html</returns>
        public static string StripHtml(this string source)
        {
            try
            {
                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                var result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         RegexOptions.IgnoreCase);
                //result = Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         RegexOptions.IgnoreCase);

                // replace special characters:
                result = Regex.Replace(result,
                         @" ", " ",
                         RegexOptions.IgnoreCase);

                result = Regex.Replace(result,
                         @"&bull;", " * ",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&lsaquo;", "<",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&rsaquo;", ">",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&trade;", "(tm)",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&frasl;", "/",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&lt;", "<",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&gt;", ">",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&copy;", "(c)",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&reg;", "(r)",
                         RegexOptions.IgnoreCase);

                // Ký tự đặc biệt
                result = Regex.Replace(result,
                         @"&#234;", "ê",
                         RegexOptions.IgnoreCase);

                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         RegexOptions.IgnoreCase);

                // for testing
                //Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                var breaks = "\r\r\r";
                // Initial replacement target string for tabs
                var tabs = "\t\t\t\t\t";
                for (var index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                return result.Trim();
            }
            catch
            {
                return source;
            }
        }

        /// <summary>
        /// Hàm format lại chuỗi json thành các thông tin về catalog và exfield dạng: name:value
        /// dùng để tìm kiếm thông tin hồ sơ theo các catalog và exfield ứng với form của hồ sơ đó
        /// </summary>
        /// <param name="json">chuỗi json của hồ sơ</param>
        /// <returns>chuỗi kết quả dạng: name:value, name:value</returns>
        public static string GetStringDataFromJson(this string json)
        {
            var result = "";
            var fields = Json2.ParseAsJs<JsDocument>(json);//JsonConvert.DeserializeObject<JsDocument>(json);
            if (fields != null)
            {
                if (fields.DocFieldJson.Any())
                {
                    result += fields.Description + ": ";
                    foreach (var ctr in fields.DocFieldJson)
                    {
                        if (ctr.TypeId == 9)
                        {
                            result += ctr.DisplayName + ": ";
                            result += ctr.Value + " ";
                        }
                        else
                        {
                            if (ctr.CatalogSelectedObject != null)
                            {
                                result += ctr.DisplayName + ": ";
                                result += ctr.CatalogSelectedObject.Value + " ";
                            }
                        }
                        result += ", ";
                    }
                    result += "; ";
                }
            }
            return result;
        }

        /// <summary>
        /// Kiểm tra chuỗi có phải là địa chỉ email hay không
        /// </summary>
        /// <param name="input">Chuỗi</param>
        /// <returns></returns>
        public static bool IsEmailAddress(this string input)
        {
            var regex = new Regex("^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Kiểm tra chuỗi có phải là số điện thoại hay không
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            var pattern = @"^\+?[0-9]{8,14}$";
            return (System.Text.RegularExpressions.Regex.IsMatch(input, pattern));
        }

        /// <summary>
        /// Kiểm tra chuỗi có phải là số di động hay không.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobilePhoneNumber(this string input)
        {
            // Viettel: 086, 096, 097, 098, 0162, 0163, 0164, 0165, 0166, 0167, 0168, 0169
            // Mobi: 090, 093, 0120, 0121, 0122, 0126, 0128, 08966
            // Vina: 091, 094, 0123, 0124, 0125, 0127, 0129
            // VN mobile: 092, 0188 và 0186
            // GMobile: 099, 0199

            if (!input.IsPhoneNumber())
            {
                return false;
            }

            var pattern = @"[(\+84)0][35789][0-9]{8,14}";
            return (System.Text.RegularExpressions.Regex.IsMatch(input, pattern));
        }

        /// <summary>
        /// Đọc dung lượng file thành chuỗi
        /// </summary>
        /// <param name="filesize">Dung lượng</param>
        /// <returns></returns>
        public static string ReadFileSize(decimal filesize)
        {
            const decimal oneKiloByte = 1024;
            const decimal oneMegaByte = 1048576;
            const decimal oneGigaByte = 1073741824;
            if (filesize >= oneGigaByte)
            {
                return (filesize / oneGigaByte).ToString("0.00", CultureInfo.InvariantCulture) + " GB";
            }
            if (filesize >= oneMegaByte)
            {
                return (filesize / oneMegaByte).ToString("0.00", CultureInfo.InvariantCulture) + " MB";
            }
            if (filesize >= oneKiloByte)
            {
                return (filesize / oneKiloByte).ToString("0", CultureInfo.InvariantCulture) + " KB";
            }
            return filesize + " bytes";
        }

        /// <summary>
        /// Trả về giá trị xác định chuỗi có giá trị không.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasValue(this string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
    }
}
