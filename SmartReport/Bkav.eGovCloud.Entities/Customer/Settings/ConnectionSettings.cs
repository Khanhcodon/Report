using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Settings liên kết với các trang khác
    /// </summary>
    public class ConnectionSettings : ISettings
    {

        /// <summary>
        /// Link iframe bmail
        /// </summary>
        public string BmailLink { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ChatLink { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string KNTCLink { get; set; }

        /// <summary>
        /// Kiểu mail: Bmail/Gmail/Yahoo/...
        /// </summary>
        public int MailType { get; set; }

        /// <summary>
        /// Các ứng dụng tích hợp
        /// </summary>
        public string Apps { get; set; }
    }
}
