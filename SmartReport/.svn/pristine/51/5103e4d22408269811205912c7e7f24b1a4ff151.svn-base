using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Models
{
    public class NotifyInfoModel
    {
        public NotifyInfoModel()
        {
            HasPlaySound = true;
            RemoveRead = false;
            HasShowDesktop = true;
            HasShowDocumentNotify = true;
            DocumentNotifyType = 1;
            HasShowMailNotify = true;
            HasShowChatNotify = true;
            MailFolderNotify = "inbox";
        }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép tự động xóa thông báo đã đọc.
        /// </summary>
        [JsonProperty("removeread")]
        public bool RemoveRead { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép hiển thị thông báo trên desktop
        /// </summary>
        [JsonProperty("hasshowdesktop")]
        public bool HasShowDesktop { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép bật âm thanh thông báo
        /// </summary>
        [JsonProperty("hasplaysound")]
        public bool HasPlaySound { get; set; }        

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép hiển thị thông báo văn bản
        /// </summary>
        [JsonProperty("hasshowdocumentnotify")]
        public bool HasShowDocumentNotify { get; set; }

        /// <summary>
        /// Thiết lập hiển thị notify cho người  dùng
        /// 1 - Chỉ hiển thị notify văn bản chờ xử lý.
        /// 2 - Notify tất cả văn bản liên quan
        /// </summary>
        [JsonProperty("documentnotifytype")]
        public byte DocumentNotifyType { get; set; }
                
        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép hiển thị thông báo mail
        /// </summary>
        [JsonProperty("hasshowmailnotify")]
        public bool HasShowMailNotify { get; set; }

        /// <summary>
        /// Danh sách mail được thiết lập xem thông báo
        /// </summary>
        [JsonProperty("mailfoldernotify")]
        public string MailFolderNotify { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập hồ sơ mặc đinh của người dùng
        /// </summary>
        [JsonProperty("maillastesttoken")]
        public string MailLastestToken { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép hiển thị thông báo chat
        /// </summary>
        [JsonProperty("hasshowchatnotify")]
        public bool HasShowChatNotify { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập hồ sơ mặc đinh của người dùng
        /// </summary>
        [JsonProperty("chatlastesttoken")]
        public string ChatLastestToken { get; set; }
        
        /// <summary>
        /// Danh sách các thiết bị kết nối 
        /// </summary>
        [JsonIgnore]
        public IEnumerable<MobileDevice> MobileDevices { get; set; }
    }

}