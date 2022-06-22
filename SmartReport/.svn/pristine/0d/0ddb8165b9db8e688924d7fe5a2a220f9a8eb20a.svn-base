using System;
namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateDocumentCopy - public - Entity
    /// Access Modifiers: 
    /// Create Date : 071013
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng StorePrivateDocumentCopy trong CSDL
    /// </summary>
    public class StorePrivateDocumentCopy
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id quan hệ giữa hồ sơ cá nhân và văn bản
        /// </summary>
        public int StorePrivateDocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ cá nhân
        /// </summary>
        public int StorePrivateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id bản sao
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập Hồ sơ cá nhân
        ///// </summary>
        //public virtual StorePrivate StorePrivate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Bản sao
        /// </summary>
        public virtual DocumentCopy DocumentCopy { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Văn bản hồ sơ
        /// </summary>
        public virtual Document Document { get; set; }
    }
}
