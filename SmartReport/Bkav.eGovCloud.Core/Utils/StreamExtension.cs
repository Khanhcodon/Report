using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace Bkav.eGovCloud.Core.Utils
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StreamExtension - public - Core
    /// Access Modifiers: 
    /// Create Date : 060616
    /// Author      : TrinhNVd@bkav.com
    /// </author>
    /// <summary>
    /// <para>Thư viện hỗ trợ stream và base64</para>
    /// <para>(TrinhNVd@bkav.com - 060616)</para>
    /// </summary>
    public static class StreamExtension
    {

        /// <summary>
        /// Convert stream sang chuỗi Base64
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="bufferLength">Độ dài buffer (default: 4096)</param>
        /// <returns></returns>
        public static string ToBase64String(this Stream stream, long? bufferLength = null)
        {
            return ToBase64String(stream.ToByte(bufferLength));
        }

        /// <summary>
        /// Chuyển sang chuỗi base64
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string ToBase64String(byte[] buffer)
        {
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// Chuyển sang chuỗi base64
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static string ToBase64String(this MemoryStream ms)
        {
            if (ms == null)
            {
                return "";
            }
            return ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// Tạo zip stream cho các file
        /// </summary>
        /// <param name="files">(Name-StreamValue)</param>
        /// <returns></returns>
        public static MemoryStream ZipAttachment(Dictionary<string, Stream> files)
        {
            var outputStream = new System.IO.MemoryStream();
            using (var zip = new ZipFile())
            {
                foreach (var file in files)
                {
                    zip.AddEntry(file.Key, file.Value);
                }
                zip.Save(outputStream);
            }
            outputStream.Position = 0;
            return outputStream;
        }

        /// <summary>
        /// Đọc stream ra byte
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="bufferLength">Độ dài buffer, mặc định 4096</param>
        /// <returns></returns>
        public static byte[] ToByte(this Stream stream, long? bufferLength = null)
        {
            using (stream)
            {
                byte[] result = null;
                var byteLeng = (int)(bufferLength ?? 4096);
                if (stream != null && byteLeng > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        int count = 0;
                        do
                        {
                            byte[] buf = new byte[byteLeng];
                            count = stream.Read(buf, 0, byteLeng);
                            ms.Write(buf, 0, count);
                        } while (stream.CanRead && count > 0);

                        result = ms.ToArray();
                    }
                }
                return result;
            }
        }

    }
}
