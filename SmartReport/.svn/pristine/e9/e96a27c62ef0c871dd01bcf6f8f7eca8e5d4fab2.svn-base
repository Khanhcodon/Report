using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Fee - public - Entity
    /// Access Modifiers:
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Fee trong CSDL
    /// </summary>
    public class Fee
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id lệ phí
        /// </summary>
        public int FeeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại lệ phí
        /// </summary>
        public int FeeTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại lệ phí
        /// </summary>
        public FeeType FeeTypeIdInEnum
        {
            get { return (FeeType)FeeTypeId; }
            set { FeeTypeId = (int)value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Loại hồ sơ
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên lệ phí
        /// </summary>
        public string FeeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giá tiền
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra lệ phí này là bắt buộc
        /// </summary>
        public bool IsRequired { get; set; }

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
        ///// Lấy hoặc thiết lập Loại hồ sơ
        ///// </summary>
        //public virtual ICollection<DoctypeFee> DoctypeFees { get; set; }
    }
}