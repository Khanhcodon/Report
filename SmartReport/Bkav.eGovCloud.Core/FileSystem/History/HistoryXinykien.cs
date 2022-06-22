using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.History
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class HistoryXinykien : HistoryBase
    {
        private List<UserReceiveXinykiens> _userReceives;

        /// <summary>
        /// Lấy hoặc thiết lập Id Node gửi văn bản
        /// </summary>
        [JsonProperty("NodeSendId")]
        public int NodeSendId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id Quy trình gửi văn bản
        /// </summary>
        [JsonProperty("WorkflowSendId")]
        public int WorkflowSendId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách user nhận văn bản (Bao gom ca Ban giao, Thong bao, Xin y kien).
        /// </summary>
        [JsonProperty("UserReceives")]
        public List<UserReceiveXinykiens> UserReceives
        {
            get
            {
                return _userReceives ?? (_userReceives = new List<UserReceiveXinykiens>());
            }
            set { _userReceives = value; }
        }
    }
}
