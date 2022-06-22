using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ContentEntity - public - Entity
    /// Access Modifiers: 
    /// Create Date : 120413
    /// Author      : GiangPN
    /// Description : Entity tương ứng đối tượng Content trong bảng Comment
    /// </summary>
    public class ContentEntity
    {
        /// <summary>
        /// Nội dung ý kiến
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Nội dung ý kiến rút gọn
        /// </summary>
        public string SubContent { get; set; }

        /// <summary>
        /// Hạn xử lý
        /// </summary>
        public DateTime? DateOverdue { get; set; }

        /// <summary>
        /// Hướng chuyển
        /// </summary>
        public List<CommentTransfer> Transfers { get; set; }

        /// <summary>
        /// Nội dung xin ý kiến
        /// </summary>
        public string ContentConSult { get; set; }
    }
}
