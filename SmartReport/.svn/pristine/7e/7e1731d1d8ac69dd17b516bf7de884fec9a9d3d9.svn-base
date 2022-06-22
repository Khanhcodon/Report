using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    public class SSOSettingsModel
    {
        private string _CallBackUrl;
        public SSOSettingsModel()
        {
            //IsActive = true;
        }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Setting.SSO.Fields.IsActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Setting.SSO.Fields.ApiUrl")]
        public string ApiUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Setting.SSO.Fields.ClientID")]
        public string ClientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Setting.SSO.Fields.SecretKey")]
        public string SecretKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Setting.SSO.Fields.CallBackUrl")]
        public string CallBackUrl {
            get
            {
                return string.IsNullOrWhiteSpace(_CallBackUrl)
                        ? "/Account/LoginSSOCallBack?returnUrl"
                        : _CallBackUrl;
            }
            set
            {
                _CallBackUrl = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Setting.SSO.Fields.Type")]
        public string Type { get; set; }
    }
}