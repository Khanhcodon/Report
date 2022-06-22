//Phần này cho vào chỉ để giữ code của phần login SSO với bmail, còn sử dụng sẽ sử dụng DLL đã tạo.

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Configuration;
//using System.IO;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;

//namespace Bkav.SsoHelper
//{
//    public class EncryptData
//    {
//        //private readonly string _key = ConfigurationManager.AppSettings["bkavSSOSecretKey"];
//        private string _key;
//        private int _keyVersion;
//        public EncryptData(int keyVersion, string key)
//        {
//            _key = key;
//            _keyVersion = keyVersion;
//        }


//        /// <summary>
//        /// Thêm dữ liệu vào data cookies
//        /// Data có dạng: name=value.length:value;
//        /// </summary>
//        /// <param name="name">Tên</param>
//        /// <param name="value">Giá trị</param>
//        /// <param name="data">StringBuilder cần thêm dữ liệu</param>
//        public void AppendMetaData(string name, string value, StringBuilder data)
//        {
//            data.Append(name);
//            data.Append("=");
//            data.Append(value.Length);
//            data.Append(":");
//            data.Append(value);
//            data.Append(";");
//        }

//        /// <summary>
//        /// Convert string thành hexstring
//        /// </summary>
//        /// <param name="str">string truyền vào</param>
//        /// <returns>hexString</returns>
//        public string ToHexString(string str)
//        {
//            var hex = string.Empty;
//            foreach (var c in str)
//            {
//                int tmp = c;
//                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
//            }
//            return hex;
//        }

//        /// <summary>
//        /// Convert hex string thành string bình thường
//        /// Kiểu UTF8
//        /// </summary>
//        /// <param name="hexString">hexString</param>
//        /// <returns>string gốc</returns>
//        public string FromHexString(string hexString)
//        {
//            var bytes = new byte[hexString.Length / 2];
//            for (var i = 0; i < bytes.Length; i++)
//            {
//                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
//            }
//            return Encoding.UTF8.GetString(bytes);
//        }

//        /// <summary>
//        /// Decode 1 array char sang byte (làm theo Hex.decodeHex bên java)
//        /// </summary>
//        /// <param name="data">data dạng mảng char</param>
//        /// <returns>mảng byte[]</returns>
//        public byte[] DecodeHex(char[] data)
//        {
//            int len = data.Length;
//            if ((len & 0x01) != 0)
//            {
//                throw new Exception("Odd number of characters.");
//            }
//            byte[] result = new byte[len >> 1];
//            for (int i = 0, j = 0; j < len; i++)
//            {
//                int f = ToDigit(data[j]) << 4;
//                j++;
//                f = f | ToDigit(data[j]);
//                j++;
//                result[i] = (byte)(f & 0xFF);
//            }
//            return result;
//        }

//        /// <summary>
//        /// Trả về thứ tự trong bit16
//        /// </summary>
//        /// <param name="ch">char</param>
//        /// <returns></returns>
//        public int ToDigit(char ch)
//        {
//            try
//            {
//                int digit = Convert.ToByte(ch.ToString(), 16);
//                return digit;
//            }
//            catch
//            {
//                return -1;
//            }
//        }

//        /// <summary>
//        /// Sinh ra chuỗi HMACSHA1 từ string truyền vào
//        /// </summary>
//        /// <param name="data">chuỗi dữ liệu</param>
//        /// <returns>chuỗi HMAC SHA1</returns>
//        public string GenerateHmac(string data)
//        {
//            var enc = Encoding.UTF8;
//            var myhmacsha1 = new HMACSHA1(DecodeHex(_key.ToCharArray()));
//            myhmacsha1.Initialize();
//            byte[] byteArray = enc.GetBytes(data);
//            var stream = new MemoryStream(byteArray);
//            return myhmacsha1.ComputeHash(stream).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
//        }

//        /// <summary>
//        /// Tạo cookie string để lưu
//        /// </summary>
//        /// <param name="data">Data</param>
//        /// <returns>string đã mã hóa</returns>
//        public string CreateCookieString(string data)
//        {
//            var dataStr = ToHexString(data);
//            var hmac = GenerateHmac(dataStr);
//            return _keyVersion + "_" + hmac + "_" + dataStr;
//        }

//        /// <summary>
//        /// Tạo cookie string để lưu
//        /// </summary>
//        /// <param name="email">Email</param>
//        /// <param name="exp">exp date, dạng total milisecond từ 1/1/1970</param>
//        /// <returns>string đã mã hóa</returns>
//        public string CreateCookieString(string email, string exp)
//        {
//            var data = new StringBuilder();
//            AppendMetaData("user", email, data);
//            AppendMetaData("exp", exp, data);
//            return CreateCookieString(data.ToString());
//        }

//        /// <summary>
//        /// Lấy dữ liệu lúc đầu đưa vào
//        /// </summary>
//        /// <param name="encodedString">cookieString</param>
//        /// <returns>dictionary chứa name, value</returns>
//        public IDictionary GetOriginData(string cookieString)
//        {
//            var data = FromHexString(cookieString.Split('_').Last());
//            var result = new Dictionary<string, string>();
//            var datas = data.Split(';');
//            foreach (var d in datas)
//            {
//                if (!string.IsNullOrEmpty(d))
//                {
//                    result.Add(d.Split('=').First(), d.Split(':').Last());
//                }
//            }
//            return result;
//        }

//        /// <summary>
//        /// Kiểm tra xem cookieString có hợp lệ không
//        /// </summary>
//        /// <param name="cookieString">cookieString</param>
//        /// <returns>hợp lệ hay không</returns>
//        public bool IsValidCookie(string cookieString)
//        {
//            try
//            {
//                var version = cookieString.Split('_')[0];
//                var hmac = cookieString.Split('_')[1];
//                var data = cookieString.Split('_')[2];
//                if (GenerateHmac(data) == hmac)
//                {
//                    var originData = GetOriginData(cookieString);
//                    var exp = Convert.ToInt64(originData["exp"]);
//                    var time = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
//                    if (exp > time)
//                    {
//                        return true;
//                    }
//                }
//                return false;
//            }
//            catch
//            {
//                return false;
//            }
//        }
//    }
//}