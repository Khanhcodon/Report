using System;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateAttachment - public - Entity
    /// Access Modifiers: 
    /// Create Date : 140414
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng StorePrivateAttachment trong CSDL
    /// </summary>
    public class StorePrivateAttachment
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id tài liệu
        /// </summary>
        public int StorePrivateAttachmentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của hồ sơ cá nhân
        /// </summary>
        public int StorePrivateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên tài liệu
        /// </summary>
        public string AttachmentName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Kích thước của tài liệu
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Kích thước của tài liệu
        /// </summary>
        public string SizeString
        {
            get
            {
                return StringExtension.ReadFileSize(Size);
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Người tạo tệp đính kèm
        /// </summary>
        public int CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Người tạo tệp đính kèm
        /// </summary>
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày tạo
        /// </summary>
        public DateTime CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id vị trí lưu file
        /// </summary>
        public int FileLocationId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên thư mục tự sinh
        /// </summary>
        public string IdentityFolder { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của cấu hình thư mục gốc phía service
        /// </summary>
        public string FileLocationKey { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập Tệp đính kèm liên quan
        ///// </summary>
        //public virtual StorePrivate StorePrivate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vị trí lưu file
        /// </summary>
        public virtual FileLocation FileLocation { get; set; }
    }
}
