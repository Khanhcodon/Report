using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para> Project: eGov Cloud v1.0</para>
    /// <para> Class : Approver - public - Entity</para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 240113</para>
    /// <para> Author      : TienBV</para>
    /// </author>
    /// <summary> 
    /// <para>Bảng quản lý yêu cầu/ tiếp nhận bổ sung</para>
    /// </summary>
    public class Supplementary
    {
        /// <summary>
        /// Get or set the key
        /// </summary>
        public int SupplementaryId { get; set; }

        /// <summary>
        /// Get or set the document id.
        /// </summary>
        [JsonIgnore]
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hướng chuyển đã Tiếp nhận bổ sung/Cập nhật kết quả dừng xử lý.
        /// </summary>
        public int? DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id cán bộ yêu cầu bổ sung/dừng xử lý
        /// </summary>
        public int UserSendId { get; set; }

        /// <summary>
        /// <para>Danh sách các hướng chuyển nhận yêu cầu dừng xử lý (documentCopyId), dạng: ;23;18;584; </para>
        /// </summary>
        public string DocumentCopyIds { get; set; }

        /// <summary>
        /// Get or set the request comment
        /// </summary>
        public string CommentSend { get; set; }

        /// <summary>
        /// Get or set the request date.
        /// </summary>
        public DateTime DateSend { get; set; }

        /// <summary>
        /// Get or set the receive user id.
        /// </summary>
        public int? UserReceivedId { get; set; }

        /// <summary>
        /// Get or set the receive comment.
        /// </summary>
        public string CommentReceived { get; set; }

        /// <summary>
        /// Get or set the receive date.
        /// </summary>
        public DateTime? DateReceived { get; set; }

        /// <summary>
        /// Get or set the date process
        /// </summary>
        public DateTime? DateBeginProcess { get; set; }

        /// <summary>
        /// Get or set the supplement type.
        /// </summary>
        public int SupplementType { get; set; }

        /// <summary>
        /// Get or set the offset day: only used when SupplementType = SupplementType.AddDay.
        /// </summary>
        public int OffsetDay { get; set; }

        /// <summary>
        /// Get or set the result: success or no.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái yêu cầu bổ sung đã được xử lý hay chưa
        /// </summary>
        public bool IsReceived { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập lần yêu cầu bổ sung
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách giấy tờ yêu cầu bổ sung
        /// </summary>
        public string PaperIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách lệ phí yêu cầu bổ sung
        /// </summary>
        public string FeeIds { get; set; }

        /// <summary>
        /// Danh sách giấy tờ
        /// </summary>
        public string Papers { get; set; }

        /// <summary>
        /// Danh sách lệ phí
        /// </summary>
        public string Fees { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày hẹn trả cũ
        /// </summary>
        public DateTime? OldDateAppointed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày hẹn trả mới
        /// </summary>
        public DateTime? NewDateAppointed { get; set; }

        /// <summary>
        /// Ngày cập nhật với eGov Online gần nhất
        /// </summary>
        public DateTime? DateOnlineUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserSendName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserReceiveName { get; set; }

        /// <summary>
        /// Danh sách các yêu cầu bổ sung chi tiết
        /// </summary>
        public IEnumerable<SupplementaryDetail> SupplementaryDetail { get; set; }

        /// <summary>
        /// eGov SupplementaryId - dùng khi đồng bộ eGov Online
        /// </summary>
        public int LocalId { get; set; }

        /// <summary>
        /// Nội dung bổ sung - dùng khi đồng bộ eGov Online
        /// </summary>
        public string Details { get; set; }
    }
}
