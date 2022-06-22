using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StoreDoc - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng StoreDoc trong CSDL
    /// </summary>
    public class StoreDoc
    {
        /// <summary>
        /// 
        /// </summary>
        public int StoreDocId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id tập hồ sơ, kho hồ sơ
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản, hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Văn bản, hồ sơ
        /// </summary>
        public virtual Document Document { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tập hồ sơ, kho hồ sơ
        /// </summary>
        public virtual Store Store { get; set; }
    }
}
