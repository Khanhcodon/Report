using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Core.ReadFile
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ToolFile
    {
        private static readonly ReaderWriterLockSlim s_rwLock = new ReaderWriterLockSlim();
        private static ToolFile s_current = null;
        private static Func<ToolFile> s_settingsFactory = new Func<ToolFile>(() => new ToolFile());
        private static bool? s_installed = null;
        private static bool s_TestMode = false;

        private const char SEPARATOR = ':';
        private const string FILENAME = "configTool.txt";

        #region Static members

        /// <summary>
        /// Trả về thiết lập tới cơ sở dữ liệu hiện tại
        /// </summary>
        public static ToolFile Current
        {
            get
            {
                using (s_rwLock.GetUpgradeableReadLock())
                {
                    if (s_current == null)
                    {
                        using (s_rwLock.GetWriteLock())
                        {
                            if (s_current == null)
                            {
                                s_current = s_settingsFactory();
                                s_current.Load();
                            }
                        }
                    }
                }

                return s_current;
            }
        }

        /// <summary>
        /// Trả về giá trị xác định đã thiết lập database hay chưa
        /// </summary>
        /// <returns></returns>
        public static bool DatabaseIsInstalled()
        {
            if (s_TestMode)
                return false;

            if (!s_installed.HasValue)
            {
                s_installed = Current.IsValid();
            }

            return s_installed.Value;
        }

        /// <summary>
        /// Xét chế độ testing
        /// </summary>
        /// <param name="isTestMode"></param>
        internal static void SetTestMode(bool isTestMode)
        {
            s_TestMode = isTestMode;
        }

        /// <summary>
        /// Xóa thiết lập cơ sở dữ liệu
        /// </summary>
        public static void Delete()
        {
            string filePath = Path.Combine(CommonHelper.MapPath("~/App_Data/"), FILENAME);
            File.Delete(filePath);
            s_current = null;
            s_installed = null;
        }

        #endregion

        #region Instance members

        /// <summary>
        /// Version hiện tại
        /// </summary>
        public string InputConnection
        {
            get;
            set;
        }

        /// <summary>
        /// Kiểu kết nối cơ sở dữ liệu
        /// </summary>
        public string OutputConnection
        {
            get;
            set;
        }

        /// <summary>
        /// Chuỗi kết nối
        /// </summary>
        public string DataConnection
        {
            get;
            set;
        }

        /// <summary>
        /// Lấy hoặc thiết lập chế độ build: 1. Hệ thống riêng, 2. Hệ thống tập trung
        /// </summary>
        public string DatabaseMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return this.OutputConnection.HasValue() && this.InputConnection.HasValue() && this.DataConnection.HasValue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool Load()
        {
            using (s_rwLock.GetWriteLock(-1))
            {
                string filePath = Path.Combine(CommonHelper.MapPath("~/App_Data/"), FILENAME);

                this.Reset();

                if (File.Exists(filePath))
                {
                    string text = File.ReadAllText(filePath);
                    var settings = Json2.ParseAs<ConfigDataTool>(text);
                }

                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            using (s_rwLock.GetWriteLock())
            {
                this.OutputConnection = null;
                this.InputConnection = null;
                this.DataConnection = null;
                s_installed = null;
            }
        }

        #endregion

        #region Instance helpers

        private IDictionary<string, string> ParseSettings(string text)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (text.IsEmpty())
                return result;

            var settings = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                    settings.Add(str);
            }

            foreach (var setting in settings)
            {
                var separatorIndex = setting.IndexOf(SEPARATOR);
                if (separatorIndex == -1)
                {
                    continue;
                }
                string key = setting.Substring(0, separatorIndex).Trim();
                string value = setting.Substring(separatorIndex + 1).Trim();

                if (key.HasValue() && value.HasValue())
                {
                    result.Add(key, value);
                }
            }

            return result;
        }

        #endregion
    }
}