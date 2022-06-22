using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Paper - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Paper trong CSDL
    /// </summary>
    public class Paper
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id giấy tờ
        /// </summary>
        public int PaperId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại giấy tờ
        /// </summary>
        public int PaperTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại giấy tờ
        /// </summary>
        public PaperType PaperTypeIdInEnum
        {
            get { return (PaperType)PaperTypeId; }
            set { PaperTypeId = (int) value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại hồ sơ
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên giấy tờ
        /// </summary>
        public string PaperName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số lượng
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra giấy tờ này là bắt buộc
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thứ tự
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        public DateTime? CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người cập nhật cuối cùng
        /// </summary>
        public int? LastModifiedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày cập nhật cuối cùng
        /// </summary>
        public DateTime? LastModifiedOnDate { get; set; }
    
        ///// <summary>
        ///// Lấy hoặc thiết lập Loại hồ sơ
        ///// </summary>
        //public virtual DocType DocType { get; set; }

        ///// <summary>
        ///// Lấy  hoặc thiết lập rằng buộc thủ tục hành chính giấy tờ
        ///// </summary>
        //public ICollection<DoctypePaper> DoctypePaper { get; set; }
    }
}
