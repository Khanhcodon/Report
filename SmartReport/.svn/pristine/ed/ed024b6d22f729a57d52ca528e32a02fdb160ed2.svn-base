using System;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.History
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class HistoryBase
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id bản sao văn bản cấp cha
        /// </summary>
        [JsonProperty("ParentId")]
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian bàn giao
        /// </summary>
        [JsonProperty("CreateDate")]
        public DateTime DateCreated { set; get; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của người gửi văn bản
        /// </summary>
        [JsonProperty("UserSendId")]
        public int UserSendId { get; set; }
    }
}
