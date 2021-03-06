using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivate - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng StorePrivate trong CSDL
    /// </summary>
    public class StorePrivate
    {
        private ICollection<StorePrivateUser> _storePrivateUsers;
        private ICollection<StorePrivateAttachment> _storePrivateAttachments;

        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ cá nhân
        /// </summary>
        public int StorePrivateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên hồ sơ cá nhân
        /// </summary>
        public string StorePrivateName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ cá nhân cha
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả hồ sơ cá nhân
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo
        /// </summary>
        public int CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày tạo
        /// </summary>
        public DateTime CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị chỉ ra hồ sơ đã bị đóng
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị chỉ ra hồ sơ ở cấp mấy
        /// </summary>
        public byte? Level { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trường id mở rộng
        /// </summary>
        public string StorePrivateIdExt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<StorePrivateUser> StorePrivateUsers
        {
            get { return _storePrivateUsers ?? (_storePrivateUsers = new List<StorePrivateUser>()); }
            set { _storePrivateUsers = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Các tài liệu trong hồ sơ cá nhân
        /// </summary>
        public virtual ICollection<StorePrivateAttachment> StorePrivateAttachments
        {
            get { return _storePrivateAttachments ?? (_storePrivateAttachments = new List<StorePrivateAttachment>()); }
            set { _storePrivateAttachments = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public bool HasShared { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public IEnumerable<int> UserIdJoined { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public IEnumerable<int> DeptIdJoined { get; set; }
    }
}
