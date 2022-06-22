using System;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.History
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : HistoryNode - public - Entity
    /// Access Modifiers: 
    /// Create Date : 260613
    /// Author      : GiangPN
    /// </author>
    /// <summary>
    /// <para>Định nghĩa Node lưu trữ thông tin người nhận văn bản.</para>
    /// (GiangPN@bkav.com - 221212)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class UserReceives : UserReceivesBase
    {
        /// <summary>
        /// Lấy hoặc thiết lập quy trình Id nhận văn bản
        /// </summary>
        [JsonProperty("WorkflowId")]
        public int WorkflowId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập trạng thái được chọn là Xlc trên form bàn giao văn bản</para>
        /// </summary>
        [JsonProperty("IsXlc")]
        public bool IsXlc { get; set; }
    }
}