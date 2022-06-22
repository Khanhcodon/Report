using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Attachment - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Attachment trong CSDL
    /// </summary>
    public class Attachment
    {
        private ICollection<AttachmentDetail> _attachmentDetail;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public Attachment() {
            IsDeleted = false;
            AttachmentDetails = new List<AttachmentDetail>();
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id của tệp đính kèm
        /// </summary>
        public int AttachmentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên tệp đính kèm
        /// </summary>
        public string AttachmentName { get; set; }

        /// <summary>
        /// Lấy ra đuôi tệp đính kèm
        /// </summary>
        public string Extension
        {
            get
            {
                var ext = Path.GetExtension(AttachmentName);
                return string.IsNullOrWhiteSpace(ext) ? string.Empty : ext.Replace(".", "");
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id của văn bản, hồ sơ chứa tệp đính kèm
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Phiên bản của tệp đính kèm
        /// </summary>
        public int VersionAttachment { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra tệp đính kèm này đã bị xóa
        /// </summary>
        /// <value>
        /// 	<c>true</c> nếu tệp đính kèm này đã được xóa; ngược lại, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Kích thước tệp đính kèm
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Lấy ra kích thước tệp đính kèm dạng chuỗi
        /// </summary>
        public string SizeString
        {
            get
            {
                return StringExtension.ReadFileSize(Size);
            }
        }
        
        /// <summary>
        /// Lấy hoặc thiết lập Các thông tin chi tiết về tệp đính kèm
        /// </summary>
        public virtual ICollection<AttachmentDetail> AttachmentDetails
        {
            get { return _attachmentDetail ?? (_attachmentDetail = new List<AttachmentDetail>()); }
            set { _attachmentDetail = value; }
        }

        /// <summary>
        /// Người xóa văn bản
        /// </summary>
        public string UserDeleted { get; set; }

        /// <summary>
        /// Ngày xóa
        /// </summary>
        public DateTime? DeletedDate { get; set; }
    }
}
