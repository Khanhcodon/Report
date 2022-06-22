using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Models
{
    public class SupplementaryModel
    {
        /// <summary>
        /// Get or set the key
        /// </summary>
        public int SupplementaryId { get; set; }

        /// <summary>
        /// Get or set the document id.
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Id của hướng chuyển đang thực hiện tiếp nhận yêu cầu bổ sung
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Get or set the request user id
        /// </summary>
        public int UserSendId { get; set; }

        /// <summary>
        /// <para>Gửi cho nhiều người: ;32;352;84; </para>
        /// Gửi cho 1 người hoặc sau khi xác nhận xử lý: docCopyId
        /// </summary>
        public string DocumentCopyIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CommentSend { get; set; }

        /// <summary>
        /// Get or set the request date.
        /// </summary>
        public DateTime DateSend { get; set; }

        public string DateSendStr
        {
            get
            {
                return DateSend.ToString("hh:mm dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Get or set the receive user id.
        /// </summary>
        public int UserReceivedId { get; set; }

        /// <summary>
        /// Get or set the receive comment.
        /// </summary>
        public string CommentReceived { get; set; }

        /// <summary>
        /// Get or set the receive date.
        /// </summary>
        public DateTime DateReceived { get; set; }

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
        /// Get or set the new date appointed
        /// </summary>
        public string NewDateAppointed { get; set; }

        public List<SupplementaryDetail> SupplementaryDetail { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách giấy tờ yêu cầu bổ sung
        /// </summary>
        public string PaperIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách lệ phí yêu cầu bổ sung
        /// </summary>
        public string FeeIds { get; set; }
    }
}