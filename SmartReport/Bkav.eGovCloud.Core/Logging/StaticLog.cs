using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Data;

namespace Bkav.eGovCloud.Core.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public class StaticLog
    {
        private static readonly string _basePath = @"~\Logs";
        private static readonly string _lienthongPath = @"~\Logs\LienThong";
        private static string _filePath;
        
        /// <summary>
        /// Log message
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="logfileName">Tên file log (nếu cần)</param>
        public static void Log(IEnumerable<string> messages, string logfileName = "log")
        {
            _filePath = Path.Combine(_basePath, string.Format("{0}_{1}.{2}", logfileName, DateTime.Now.ToString("yyyy.MM.dd"), "log"));
            _filePath = System.Web.Hosting.HostingEnvironment.MapPath(_filePath);

            if (!System.IO.File.Exists(_filePath))
            {
                using (var file = System.IO.File.Create(_filePath))
                {

                }
            }

            var sb = new StringBuilder();
            if (!messages.Any())
            {
                sb.AppendLine(DateTime.Now.ToString("hh:mm:ss ddMMyyyy") + "=======================================================================");
            }
            else
            {
                foreach (var message in messages)
                {
                    sb.AppendLine(message);
                }
            }

            sb.AppendLine("-----------------------------------------------------------------------");
            sb.AppendLine();
            sb.AppendLine();

            System.IO.File.AppendAllText(_filePath, sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="logfileName"></param>
        public static void LogLienThong(IEnumerable<string> messages, string logfileName = "log")
        {
            _filePath = Path.Combine(_lienthongPath, string.Format("{0}_{1}.{2}", logfileName, DateTime.Now.ToString("yyyy.MM.dd"), "log"));
            _filePath = System.Web.Hosting.HostingEnvironment.MapPath(_filePath);

            if (!System.IO.File.Exists(_filePath))
            {
                using (var file = System.IO.File.Create(_filePath))
                {
                    
                }
            }

            var sb = new StringBuilder();
            if (!messages.Any())
            {
                sb.AppendLine(DateTime.Now.ToString("hh:mm:ss ddMMyyyy") + "=======================================================================");
            }
            else
            {
                foreach (var message in messages)
                {
                    sb.AppendLine(message);
                }
            }

            sb.AppendLine("-----------------------------------------------------------------------");
            sb.AppendLine();
            sb.AppendLine();

            System.IO.File.AppendAllText(_filePath, sb.ToString());
        }
    }
}
