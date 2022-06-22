using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Api.Dto
{
    public class SupplementaryDto
    {
        /// <summary>
        /// Get or set the key
        /// </summary>
        public int SupplementaryId { get; set; }

        /// <summary>
        /// Get or set the request comment
        /// </summary>
        public string CommentSend { get; set; }

        /// <summary>
        /// Get or set the request date.
        /// </summary>
        public DateTime DateSend { get; set; }

        /// <summary>
        /// Get or set the receive comment.
        /// </summary>
        public string CommentReceived { get; set; }

        /// <summary>
        /// Get or set the receive date.
        /// </summary>
        public DateTime? DateReceived { get; set; }

        /// <summary>
        /// Get or set the result: success or no.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Yêu cầu bổ sung đã tiêp nhận hay chưa
        /// </summary>
        public bool IsReceived { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập lần yêu cầu bổ sung
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Tên cán bộ gửi yêu cầu bổ sung
        /// </summary>
        public string UserSendName { get; set; }

        /// <summary>
        /// Tên người tiếp nhận yêu cầu bổ sung
        /// </summary>
        public string UserReceiveName { get; set; }

        /// <summary>
        /// Danh sách giấy tờ yêu cầu bổ sung
        /// </summary>
        public IEnumerable<PaperDto> Papers { get; set; }

        /// <summary>
        /// Danh sách lệ phí yêu cầu bổ sung
        /// </summary>
        public IEnumerable<FeeDto> Fees { get; set; }
    }
}