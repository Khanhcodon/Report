using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Bkav.eGovCloud
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class CommonHelper
    {
        /// <summary>
        /// Trả về đường dẫn tuyệt đối từ đường dẫn tương đối.
        /// </summary>
        /// <param name="path">Đường dẫn tương đối. Ví dụ "~/bin"</param>
        /// <param name="findAppRoot">Giá trị xác định có tìm thư mục gốc</param>
        /// <returns>Đường dẫn tuyệt đối</returns>
        public static string MapPath(string path, bool findAppRoot = true)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            if (HostingEnvironment.IsHosted)
            {
                path = path.Replace("../", "~/");
                // hosted
                return HostingEnvironment.MapPath(path);
            }
            else
            {
                // not hosted
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Replace("~/", "").Replace("../", "").TrimStart('/').Replace('/', '\\');

                var testPath = Path.Combine(baseDirectory, path);

                if (findAppRoot)
                {
                    var dir = FindSolutionRoot(baseDirectory);

                    if (dir != null)
                    {
                        baseDirectory = Path.Combine(dir.FullName, "Bkav.eGovCloud");
                        testPath = Path.Combine(baseDirectory, path);
                    }
                }

                return testPath;
            }
        }

        /// <summary>
        /// Trả về giá trị xác định đang trong môi trường dev
        /// </summary>
        public static bool IsDevEnvironment
        {
            get
            {
                if (!HostingEnvironment.IsHosted)
                    return true;

                if (System.Diagnostics.Debugger.IsAttached)
                    return true;

                // Kiểm tra nếu file 'eGovCloud - 2013.sln' nằm trong thư mục root
                if (FindSolutionRoot(HostingEnvironment.MapPath("~/")) != null)
                    return true;

                return false;
            }
        }

        private static DirectoryInfo FindSolutionRoot(string currentDir)
        {
            var dir = Directory.GetParent(currentDir);
            while (true)
            {
                if (dir == null || IsSolutionRoot(dir))
                    break;

                dir = dir.Parent;
            }

            return dir;
        }

        private static bool IsSolutionRoot(DirectoryInfo dir)
        {
            return File.Exists(Path.Combine(dir.FullName, "eGovCloud - 2013.sln"));
        }

        /// <summary>
        /// Trả về AppSetting config theo key.
        /// </summary>
        /// <param name="key">Tên key</param>
        /// <param name="defValue">Giá trị mặc định</param>
        /// <returns></returns>
        public static string GetAppSetting(string key, string defValue = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            var setting = ConfigurationManager.AppSettings[key];

            if (setting == null)
            {
                return defValue;
            }

            return setting;
        }
    }
}
