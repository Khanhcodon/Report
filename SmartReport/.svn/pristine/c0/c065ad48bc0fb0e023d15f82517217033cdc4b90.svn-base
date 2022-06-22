using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Enum;

namespace Bkav.eGovCloud.Models
{
    public class RenewalsModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id gia hạn xử lý
        /// </summary>
        public int RenewalsId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id vệt  - văn bản copy
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id vệt  - văn bản copy
        /// </summary>
        public int DocumentCopyIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian hẹn trả cũ(thời gian trước khi gia hạn xử lý)
        /// </summary>
        public DateTime? OldDateAppointed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số ngày gia hạn xử lý
        /// </summary>
        public int RenewalsDays { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số ngày gia hạn được duyệt
        /// </summary>
        public int ApprovedRenewalsDays { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian hẹn trả mới(thời gian sau khi gia hạn xử lý)
        /// </summary>
        public DateTime NewDateAppointed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người xử lý gia hạn(người thực hiện gia hạn)
        /// </summary>
        public int UserRequestedId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người duyệt gia hạn xử lý(Các user được cấp quyền duyệt gia hạn)
        /// </summary>
        public int UserApprovedId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập lý do gia hạn(do người thực hiện gia hạn nhập)
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
        /// Lấy hoặc thiết lập danh sách người duyệt gia hạn
        /// </summary>
        public List<int> UserApprovedIds { get; set; }

        public int RenewalsType { get; set; }

        public RenewalsType RenewalsTypeInEnum
        {
            get
            {
                return (RenewalsType)RenewalsType;
            }
        }
    }
}