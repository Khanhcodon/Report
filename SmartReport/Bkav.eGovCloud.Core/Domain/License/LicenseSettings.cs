using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bkav.eGovCloud.Core.License;

namespace Bkav.eGovCloud.Core.Domain.License
{
    /// <summary>
    /// Thông tin license hiện tại.
    /// </summary>
    public class LicenseSettings
    {
        private const string FILENAME = "~/License.egovkey";
        private static LicenseSettings s_current = null;
        private static Func<LicenseSettings> s_settingsFactory = new Func<LicenseSettings>(() => new LicenseSettings());

        #region Static members

        /// <summary>
        /// Trả về thông tin license hiện tại
        /// </summary>
        public static LicenseSettings Current
        {
            get
            {
                if (s_current == null)
                {
                    s_current = s_settingsFactory();
                    s_current.Load();
                }

                return s_current;
            }
        }

        #endregion

        #region Instance members

        /// <summary>
        /// Hạn license
        /// </summary>
        public DateTime Todate
        {
            get;
            set;
        }

        /// <summary>
        /// Tổng số user
        /// </summary>
        public int TotalUser
        {
            get;
            set;
        }
        
        /// <summary>
        /// Trả về trạng thái xác định là chế độ miễn phí
        /// </summary>
        /// <returns></returns>
        public bool IsFreeMode
        {
            get;
            set;
        }

        /// <summary>
        /// Thông tin license
        /// </summary>
        public LicenseInfo Infomation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool Load()
        {
            var path = CommonHelper.MapPath(FILENAME);
            var licenseInfo = LicenseHelper.ReadLicense(path);
            if (licenseInfo != null)
            {
                this.TotalUser = licenseInfo.TotalUser;
                this.Todate = licenseInfo.ToDate;
                this.IsFreeMode = licenseInfo.IsFreeMode;
                this.Infomation = licenseInfo;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Lưu license
        /// </summary>
        /// <returns></returns>
        public virtual void Save(string token)
        {
            LicenseHelper.Save(CommonHelper.MapPath(FILENAME), token);

            // Gán để load lại
            s_current = null;
        }

        #endregion
    }
}
