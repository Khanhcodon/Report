using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    public class NotificationSettingsModel
    {
        public NotificationSettingsModel()
        {
            MailActived = DocumentActived = ChatActived = true;
            MailNotifyType = DocumentNotifyType = 1;
            //DocumentActived = true;
            //DocumentNotifyType = (byte)Entities.Enum.DocumentNotifyType.ShowNotifyInProcess;
            TimeToCheckMail = 1;
            //MailNotifyType = (byte)Entities.Enum.MailNotifyType.Inbox;
            //MailActived = true;
        }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Setting.Notification.Fields.NotifyUrl.Label")]
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>

        [LocalizationDisplayName("Setting.Notification.Fields.DomainUrl.Label")]
        public string DomainUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Setting.Notification.Fields.NotifyPathUrl.Label")]
        public string NotifyPathUrl { get; set; }

        #region Mail
        [LocalizationDisplayName("Setting.Notification.Fields.Active.Label")]
        public bool MailActived { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Setting.Notification.Fields.MailNotifyType.Label")]
        public byte MailNotifyType { get; set; }

        /// <summary>
        /// Default time to check mail (1 min is default)
        /// </summary>
        [LocalizationDisplayName("Setting.Notification.Fields.TimeToCheckMail.Label")]
        public int TimeToCheckMail { get; set; }

        #endregion

        #region Documents

        /// <summary>
        /// Thiết lập hiển thị notify cho người  dùng
        /// 0 - Không hiển thị notify văn bản
        /// 1 - Chỉ hiển thị notify văn bản chờ xử lý.
        /// 2 - Notify tất cả văn bản liên quan
        /// </summary>
        [LocalizationDisplayName("Setting.Notification.Fields.DocumentNotifyType.Label")]
        public byte DocumentNotifyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Setting.Notification.Fields.Active.Label")]
        public bool DocumentActived { get; set; }
        #endregion

        #region Chat
        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("Setting.Notification.Fields.Active.Label")]
        public bool ChatActived { get; set; }
        #endregion
    }
}