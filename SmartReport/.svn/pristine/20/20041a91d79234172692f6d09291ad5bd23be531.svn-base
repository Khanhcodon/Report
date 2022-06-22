using System;
using System.IO;

namespace Bkav.eGovCloud.Core.FileSystem
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
    /// <para>1 thư viện tiện ích cho quản lý file</para>
    /// (TrungVH@bkav.com - 100912)
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// Lấy một tên file mặc định duy nhất sử dụng System.Guid.
        /// </summary>
        /// <param name="path">Đường dẫn</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns>Tên file có thể tạo</returns>
        public static string GetRandomGuidFile(string path, string extension)
        {
            string result;
            var ext = ParseExtension(extension);
            var ensurePath = DirectoryUtil.ToAbsoluteAndEnsureExist(path);
            do
            {
                var randomName = Guid.NewGuid().ToString("N") + ext;
                result = Path.Combine(ensurePath, randomName);
            } while (File.Exists(result));
            return result;
        }

        /// <summary>
        /// Lấy một tên file theo timestamp.
        /// </summary>
        /// <param name="path">Đường dẫn</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <param name="suffix">Hậu tố</param>
        /// <param name="datetimeFormat">Định dạng ngày tháng</param>
        /// <param name="prefix">Tiền tố</param>
        /// <returns>Tên file có thể tạo</returns>
        public static string GetRandomTimeFile(string path, string extension, string prefix, string suffix, string datetimeFormat)
        {
            string result;
            var ext = ParseExtension(extension);
            var ensurePath = DirectoryUtil.ToAbsoluteAndEnsureExist(path);
            do
            {
                var filename = string.Format("{0}{1}{2}", prefix, DateTime.Now.ToString(datetimeFormat), suffix) + ext;
                result = Path.Combine(ensurePath, filename);
            } while (File.Exists(result));
            return result;
        }

        /// <summary>
        /// Lấy địa chỉ tuyệt đối của một path.
        /// </summary>
        /// <param name="path">Đường dẫn</param>
        /// <param name="basePath">Đường dẫn cơ sở</param>
        /// <returns>Đường dẫn tuyệt đối</returns>
        public static string ToAbsolute(string path, string basePath)
        {
            return Path.Combine(string.IsNullOrEmpty(basePath) ? AppDomain.CurrentDomain.BaseDirectory : basePath,
                                path ?? string.Empty);
        }

        private static string ParseExtension(string extension)
        {
            return string.IsNullOrEmpty(extension)
                          ? string.Empty
                          : extension.StartsWith(".") ? extension : "." + extension;
        }

        /// <summary>
        /// Đảm bảo rằng tên file sẽ có đuôi file
        /// </summary>
        /// <param name="fileName">Tên file</param>
        /// <param name="extension">Tên đuôi file</param>
        /// <returns>Tên file đã có đuôi file</returns>
        public static string EnsureExtension(string fileName, string extension)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            if (String.IsNullOrEmpty(extension))
            {
                return fileName;
            }
            var ext = extension.StartsWith(".") ? extension : "." + extension;
            if (!fileName.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase))
            {
                return fileName + ext;
            }
            return fileName;
        }
    }
}
