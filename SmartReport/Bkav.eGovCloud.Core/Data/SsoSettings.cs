using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Bkav.eGovCloud.Core.Utils
{
    /// <summary>
    /// Các thiết lập single sign on
    /// </summary>
    public class SsoSettings
    {
        private static readonly ReaderWriterLockSlim s_rwLock = new ReaderWriterLockSlim();
        private static SsoSettings s_current = null;
        private static Func<SsoSettings> s_settingsFactory = new Func<SsoSettings>(() => new SsoSettings());

        private const char SEPARATOR = ':';
        private const string FILENAME = "SsoSettings.txt";

        /// <summary>
        /// Thiếp lập hiện tại
        /// </summary>
        public static SsoSettings Instance
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
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool Save(string rootPath = "")
        {
            using (s_rwLock.GetWriteLock())
            {
                rootPath = string.IsNullOrWhiteSpace(rootPath) ? CommonHelper.MapPath("~/App_Data/") : rootPath;
                string filePath = Path.Combine(rootPath, FILENAME);
                if (!File.Exists(filePath))
                {
                    using (File.Create(filePath))
                    {
                        // we use 'using' to close the file after it's created
                    }
                }

                var text = SerializeSettings();
                File.WriteAllText(filePath, text);

                return true;
            }
        }

        #region Instance members

        /// <summary>
        /// Cookie chứa thông tin người dùng
        /// </summary>
        public string UserInfoCookie { get; set; }

        /// <summary>
        /// Domain cha sử dụng chung
        /// </summary>
        public string BkavSSOParentDomain { get; set; }

        /// <summary>
        /// Tên cookie SSO dùng chung
        /// </summary>
        public string BkavSSOCookieName { get; set; }

        /// <summary>
        /// Username đang đăng nhập dùng chung
        /// </summary>
        public string BkavSSOCookieUsername { get; set; }

        /// <summary>
        /// Version của secret key
        /// </summary>
        public int BkavSSOKeyVersion { get; set; }

        /// <summary>
        /// Secretkey dành cho việc giải mã cookie
        /// </summary>
        public string BkavSSOSecretKey { get; set; }

        /// <summary>
        /// Hạn sử dụng của coookie
        /// </summary>
        public int BkavSSOExpire { get; set; }

        ///// <summary>
        ///// Loại mail
        ///// </summary>
        //public string MailType { get; set; }

        ///// <summary>
        ///// Đường dẫn bmail
        ///// </summary>
        //public string BmailLink { get; set; }

        ///// <summary>
        ///// Đường dẫn eGov Chat
        ///// </summary>
        //public string ChatLink { get; set; }

        ///// <summary>
        ///// Đường dẫn KNTC
        ///// </summary>
        //public string KNTCLink { get; set; }

        #endregion

        #region Instance helpers

        private bool Load()
        {
            using (s_rwLock.GetWriteLock(-1))
            {
                string filePath = Path.Combine(CommonHelper.MapPath("~/App_Data/"), FILENAME);

                this.Reset();

                if (File.Exists(filePath))
                {
                    string text = File.ReadAllText(filePath);
                    var settings = ParseSettings(text);
                    if (settings.Any())
                    {
                        if (settings.ContainsKey("BkavSSOCookieName"))
                        {
                            this.BkavSSOCookieName = settings["BkavSSOCookieName"];
                        }
                        if (settings.ContainsKey("BkavSSOCookieUsername"))
                        {
                            this.BkavSSOCookieUsername = settings["BkavSSOCookieUsername"];
                        }
                        if (settings.ContainsKey("BkavSSOExpire"))
                        {
                            this.BkavSSOExpire = int.Parse(settings["BkavSSOExpire"]);
                        }
                        if (settings.ContainsKey("BkavSSOKeyVersion"))
                        {
                            this.BkavSSOKeyVersion = int.Parse(settings["BkavSSOKeyVersion"]);
                        }
                        if (settings.ContainsKey("BkavSSOParentDomain"))
                        {
                            this.BkavSSOParentDomain = settings["BkavSSOParentDomain"];
                        }
                        if (settings.ContainsKey("BkavSSOSecretKey"))
                        {
                            this.BkavSSOSecretKey = settings["BkavSSOSecretKey"];
                        }
                        if (settings.ContainsKey("UserInfoCookie"))
                        {
                            this.UserInfoCookie = settings["UserInfoCookie"];
                        }
                        //if (settings.ContainsKey("BmailLink"))
                        //{
                        //    this.BmailLink = settings["BmailLink"];
                        //}
                        //if (settings.ContainsKey("ChatLink"))
                        //{
                        //    this.ChatLink = settings["ChatLink"];
                        //}
                        //if (settings.ContainsKey("KNTCLink"))
                        //{
                        //    this.KNTCLink = settings["KNTCLink"];
                        //}
                        //if (settings.ContainsKey("MailType"))
                        //{
                        //    this.MailType = settings["MailType"];
                        //}
                        return true;
                    }
                }

                return false;
            }
        }

        private void Reset()
        {
            using (s_rwLock.GetWriteLock())
            {
                this.BkavSSOCookieName = "bkavAuthen";
                this.BkavSSOCookieUsername = "bkavUsername";
                this.BkavSSOExpire = 7;
                this.BkavSSOKeyVersion = 0;
                this.BkavSSOParentDomain = "bkav.com";
                this.BkavSSOSecretKey = "c58ba928fcddbf73f1b4e9a6fe4776700bcf013d570096ef2ea638f9ad7c2b34";
                this.UserInfoCookie = "eGovUserInfo";
                //this.BmailLink = "";
                //this.MailType = "BMail";
                //this.ChatLink = "";
                //this.KNTCLink = "";
            }
        }

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

        private string SerializeSettings()
        {
            //TrinhNVd: Đưa Environment.NewLine lên đầu, đánh giá trị là 0 sẽ không phải đánh lại index khi số thông tin quá lớn
            return string.Format(
                "UserInfoCookie: {1}{0}BkavSSOCookieName: {2}{0}BkavSSOCookieUsername: {3}{0}BkavSSOExpire: "
                + "{4}{0}BkavSSOKeyVersion: {5}{0}BkavSSOParentDomain: {6}{0}BkavSSOSecretKey: {7}{0}",
                //+ "{7}{0}BmailLink: {8}{0}ChatLink: {9}{0}KNTCLink: {10}{0}MailType: {11}{0}",
                new string[]{
                    Environment.NewLine, //0
                    this.UserInfoCookie,
                    this.BkavSSOCookieName,
                    this.BkavSSOCookieUsername,
                    this.BkavSSOExpire.ToString(), 
                    this.BkavSSOKeyVersion.ToString(),//5
                    this.BkavSSOParentDomain,
                    this.BkavSSOSecretKey,
                    //this.BmailLink,
                    //this.ChatLink, 
                    //this.KNTCLink = "", //10
                    //this.MailType
                });
        }

        #endregion
    }
}
