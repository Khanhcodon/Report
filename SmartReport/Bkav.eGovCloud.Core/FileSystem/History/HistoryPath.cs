using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Core.History
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : HistoryPath - public - Entity
    /// Access Modifiers: 
    /// Create Date : 221212
    /// Author      : GiangPN
    /// Description : Định nghĩa 1 Path lưu trữ các quy trình khác nhau của 1 công văn.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class HistoryPath
    {
        private List<UserReceives> _userReceives;

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

        /// <summary>
        /// Lấy hoặc thiết lập Id của người nhận văn bản
        /// </summary>
        [JsonProperty("UserReceiveId")]
        public int UserReceiveId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id Node gửi văn bản
        /// </summary>
        [JsonProperty("NodeSendId")]
        public int NodeSendId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id Node nhận văn bản
        /// </summary>
        [JsonProperty("NodeReceiveId")]
        public int NodeReceiveId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id Quy trình gửi văn bản
        /// </summary>
        [JsonProperty("WorkflowSendId")]
        public int WorkflowSendId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id Quy trình nhận văn bản
        /// </summary>
        [JsonProperty("WorkflowReceiveId")]
        public int WorkflowReceiveId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách user nhận văn bản (Bao gom ca Ban giao, Thong bao, Xin y kien).
        /// </summary>
        [JsonProperty("UserReceives")]
        public List<UserReceives> UserReceives
        {
            get
            {
                return _userReceives ?? (_userReceives = new List<UserReceives>());
            }
            set { _userReceives = value; }
        }
    }
}