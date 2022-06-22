using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Cấu hình thông báo 
    /// </summary>
    public class NotificationSettings : ISettings
    {
        /// <summary>
        /// 
        /// </summary>
        public NotificationSettings()
        {
            MailActived = DocumentActived = ChatActived = true;
            MailNotifyType = DocumentNotifyType = 1;
            //MailNotifyType = (byte)Enum.MailNotifyType.Inbox;
            //DocumentNotifyType = (byte)Enum.DocumentNotifyType.ShowNotifyInProcess;
        }

        /// <summary>
        /// 
        /// </summary>
        public string NotifyUrl { get; set; }

        #region Mail
        /// <summary>
        /// 
        /// </summary>
        public bool MailActived { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte MailNotifyType { get; set; }

        /// <summary>
        /// Default time to check mail (1 min is default)
        /// </summary>
        public int TimeToCheckMail { get; set; }

        #endregion

        #region Documents

        /// <summary>
        /// 
        /// </summary>
        public bool DocumentActived { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte DocumentNotifyType { get; set; }

        #endregion

        #region Chat

        /// <summary>
        /// 
        /// </summary>
        public bool ChatActived { get; set; }

        #endregion

    }
}