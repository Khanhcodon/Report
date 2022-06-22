using System;
using System.IO;
using System.Net;
using System.Text;

namespace Bkav.eGovCloud.Core.Utils
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : HasBase64 - public - Core
    /// Access Modifiers: 
    /// Create Date : 190316
    /// Author      : TrinhNVd - Chuyển của HopCV sang
    /// </author>
    /// <summary>
    /// <para>Thư viện hỗ trợ mã hóa và giải mã base64...</para>
    /// (TrinhNVd@bkav.com - 190316)
    /// </summary>
    public static class HasBase64
    {
        /// <summary>
        /// Mã hóa base 64
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Base64Encode(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var outPut = System.Text.Encoding.UTF8.GetBytes(input);
            return System.Convert.ToBase64String(outPut);
        }

        /// <summary>
        /// Giải mã base 64
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Base64Decode(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            try
            {
                var outPut = System.Convert.FromBase64String(input);
                return System.Text.Encoding.UTF8.GetString(outPut);
            }
            catch (Exception)
            {
                return input;
            }

        }

        /// <summary>
        /// Convert ảnh về chuỗi Base64
        /// </summary>
        /// <param name="url">Image Url</param>
        /// <returns></returns>
        public static String ConvertImageURLToBase64(String url)
        {
            StringBuilder _sb = new StringBuilder();

            Byte[] _byte = GetImage(url);

            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return _sb.ToString();
        }

        private static byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception)
            {
                buf = null;
            }

            return (buf);
        }
    }
}