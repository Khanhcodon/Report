using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.History
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class UserReceivesBase
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của văn bản copy nhận văn bản
        /// </summary>
        [JsonProperty("DocumentCopyId")]
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của người nhận văn bản
        /// </summary>
        [JsonProperty("UserReceiveId")]
        public int UserReceiveId { get; set; }

        /// <summary>
        /// <para>Kiểu văn bản đang giữ: Xử lý chính, Đồng xử lý, Xin ý kiến, Thông báo...</para>
        /// </summary>
        [JsonProperty("DocumentCopyType")]
        public int DocumentCopyType { set; get; }

        /// <summary>
        /// Thời điểm thực hiện bàn giao văn bản. Các cán bộ cùng nhận văn bản trong 1 lần bàn giao thì có DateCreated giống hệt nhau.
        /// </summary>
        [JsonProperty("DateCreated")]
        public DateTime DateCreated { get; set; }
    }
}
