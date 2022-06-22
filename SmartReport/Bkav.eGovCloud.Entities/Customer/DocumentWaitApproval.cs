using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentWaitApproval - public - Entity
    /// Access Modifiers: 
    /// Create Date : 220113
    /// Author      : GiangPN
    /// Description : Entity tương ứng với bảng DocumentWaitApproval(Văn bản chờ duyệt) trong CSDL
    /// </summary>
    public class DocumentWaitApproval
    {
        #region Fields

        #endregion

        #region Instance Properties

        #region Columns

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản chờ duyệt
        /// </summary>
        public int DocumentWaitApprovalId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản, hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người gửi
        /// </summary>
        public int UserSendId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người nhận(người đang giữ văn bản)
        /// </summary>
        public int UserReceiveId { get; set; }

        /// <summary>
        /// Đánh dấu văn bản đã được xem.
        /// </summary>
        public bool IsViewed { get; set; }

        /// <summary>
        /// <para></para> Lấy hoặc thiết lập Id vệt
        /// </summary>
        public int DocumentCopyId { get; set; }

        #endregion

        #endregion
    }
}