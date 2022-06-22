using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Renewals - public - Entity
    /// Access Modifiers: 
    /// Create Date : 290313
    /// Author      : GiangPN
    /// Description : Entity tương ứng với bảng Renewals(Gia hạn xử lý) trong CSDL
    /// </summary>
    public class Renewals
    {
        #region Fields

        #endregion

        #region Instance Properties

        #region Columns

        /// <summary>
        /// Lấy hoặc thiết lập Id gia hạn xử lý
        /// </summary>
        public int RenewalsId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id hướng xử lý đã duyệt gia hạn</para> 
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id hướng xử lý nhận duyệt gia hạn. VD: ;32;34;</para> 
        /// </summary>
        public string DocumentCopyIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian hẹn trả cũ (thời gian trước khi gia hạn xử lý)
        /// </summary>
        public DateTime? OldDateAppointed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số ngày gia hạn xử lý
        /// </summary>
        public int RenewalsDays { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số ngày gia hạn được duyệt
        /// </summary>
        public int? ApprovedRenewalsDays { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian hẹn trả mới(thời gian sau khi gia hạn xử lý)
        /// </summary>
        public DateTime? NewDateAppointed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người xin gia hạn (người thực hiện gia hạn)
        /// </summary>
        public int UserRequestedId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người duyệt gia hạn (Các user được cấp quyền duyệt gia hạn)
        /// </summary>
        public int? UserApprovedId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập lý do gia hạn (do người thực hiện gia hạn nhập)
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ý kiến của người duyệt
        /// </summary>
        public string ApprovedComment { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái 1 gia hạn: True - là gia hạn đã được duyệt, False - là gia hạn chưa được duyệt
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Kiểu gia hạn
        /// </summary>
        public int RenewalsType { get; set; }

        #endregion

        #endregion
    }
}