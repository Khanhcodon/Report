using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.History
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class HistoryThongbao : HistoryBase
    {
        private List<UserReceiveThongbaos> _userReceives;

        /// <summary>
        /// Lấy hoặc thiết lập danh sách user nhận văn bản (Bao gom ca Ban giao, Thong bao, Xin y kien).
        /// </summary>
        [JsonProperty("UserReceives")]
        public List<UserReceiveThongbaos> UserReceives
        {
            get
            {
                return _userReceives ?? (_userReceives = new List<UserReceiveThongbaos>());
            }
            set { _userReceives = value; }
        }
    }
}
