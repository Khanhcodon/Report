using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Domain - public - Entity
    /// Access Modifiers: 
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Domain trong CSDL
    /// </summary>
    [Serializable]
    public class Domain
    {
        private ICollection<DomainAlias> _domainAliass;
        private ICollection<Domain> _domainChildren;
        private ICollection<AccountDomain> _accountDomains;

        /// <summary>
        /// Lấy hoặc thiết lập Id domain
        /// </summary>
        public int DomainId { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập serverId
        ///// </summary>
        //public int ServerId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id domain cha
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên domain
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên khách hàng
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại khách hàng 
        /// </summary>
        public bool CustomerType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id kết nối cơ sở dữ liệu
        /// </summary>
        public int ConnectionId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại khách hàng 
        /// </summary>
        public CustomerType CustomerTypeInEnum
        {
            get { return (CustomerType)Convert.ToInt32(CustomerType); }
            set { CustomerType = Convert.ToBoolean((int)value); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Tỉnh thành phố
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Quận, huyện
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Phường, xã
        /// </summary>
        public string Commune { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Sở, ban, ngành
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra Domain này đang hoạt động
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
        public byte[] Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách các id của domain con
        /// </summary>
        public int[] DomainIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Kết nối
        /// </summary>
        public virtual Connection Connection { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Các đường dẫn liên quan
        /// </summary>
        public virtual ICollection<DomainAlias> DomainAliass
        {
            get { return _domainAliass ?? (_domainAliass = new List<DomainAlias>()); }
            set { _domainAliass = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Các domain con
        /// </summary>
        public virtual ICollection<Domain> DomainChildren
        {
            get { return _domainChildren ?? (_domainChildren = new List<Domain>()); }
            set { _domainChildren = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Domain cha
        /// </summary>
        public virtual Domain DomainParent { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các mapping giữa người dùng và domain
        /// </summary>
        public ICollection<AccountDomain> AccountDomains
        {
            get { return _accountDomains ?? (_accountDomains = new List<AccountDomain>()); }
            set { _accountDomains = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các mapping giữa người dùng và domain
        /// </summary>
        public string DomainUsers
        {
            get;
            set;
        }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái xác định là cơ quan chính
        /// </summary>
        public bool IsPrimary { get; set; }
    }
}
