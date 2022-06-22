using System;

namespace Bkav.eGovCloud.Entities.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DomainAlias - public - Entity
    /// Access Modifiers: 
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng DomainAlias trong CSDL
    /// </summary>
    public class DomainAlias
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id đường dẫn
        /// </summary>
        public int DomainAliasId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id domain
        /// </summary>
        public int DomainId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đường dẫn
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra tên đường dẫn này là đường dẫn chính
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra đường dẫn này đang hoạt động
        /// </summary>
        public bool IsActivated { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public byte[] VersionByte { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Domain
        /// </summary>
        public virtual Domain Domain { get; set; }
    }
}
