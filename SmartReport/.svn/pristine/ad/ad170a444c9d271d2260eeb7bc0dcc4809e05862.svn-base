using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Api.Dto
{
    /// <summary>
    /// lịch sử bàn giao công văn
    /// </summary>
    public class DocumentTransferHistoryDto
    {
        public DocumentTransferHistoryDto()
        {
            UserTransferHistorys = new List<UserTransferHistory>();
        }
        /// <summary>
        /// Lỗi xảy ra
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// List những user đã bàn giao
        /// </summary>
        public List<UserTransferHistory> UserTransferHistorys { get; set; }
    }

    public class UserTransferHistory
    {
        /// <summary>
        /// Tên user
        /// </summary>
        public string UserSend { get; set; }

        /// <summary>
        /// Thời gian bàn giao
        /// </summary>
        public DateTime TransferTime { get; set; }

        /// <summary>
        /// Người nhận
        /// </summary>
        public string UserReiceived { get; set; }
    }
}
