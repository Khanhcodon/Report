using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Core.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebHelper
    {
        /// <summary>
        /// Khởi động lại ứng dụng
        /// </summary>
        public static void RestartApplication()
        {
            TryWriteBinFolder();
        }

        /// <summary>
        /// Thử ghi vào thư mục Bin để restart ứng dụng.
        /// </summary>
        /// <returns></returns>
        public static bool TryWriteBinFolder(string binPath = "")
        {
            try
            {
                binPath = string.IsNullOrEmpty(binPath) ? CommonHelper.MapPath("~/bin") : binPath;
                var binMarker = Path.Combine(binPath, "HostRestart");
                Directory.CreateDirectory(binMarker);

                using (var stream = System.IO.File.CreateText(Path.Combine(binMarker, "marker.txt")))
                {
                    stream.WriteLine("Restart on '{0}'", DateTime.UtcNow);
                    stream.Flush();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
