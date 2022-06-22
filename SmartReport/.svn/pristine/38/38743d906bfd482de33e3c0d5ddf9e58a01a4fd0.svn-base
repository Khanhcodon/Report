using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentDetail - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng AttachmentDetail trong CSDL
    /// </summary>
    public class AttachmentDetail
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        public AttachmentDetail()
        {
            AttachLink = string.Empty;
            IsLink = false;
        }
        /// <summary>
        /// Lấy hoặc thiết lập Id thông tin chi tiết tệp đính kèm
        /// </summary>
        public int AttachmentDetailId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của tệp đính kèm
        /// </summary>
        public int AttachmentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Phiên bản chi tiết tệp đính kèm
        /// </summary>
        public int VersionAttachmentDetail { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo tệp đính kèm
        /// </summary>
        public DateTime CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Người tạo tệp đính kèm
        /// </summary>
        public int CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Người tạo tệp đính kèm
        /// </summary>
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Kích thước tệp đính kèm
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên file
        /// </summary>
        public string FileName { get; set; }

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

        /// <summary>
        /// Trả về trạng thái xác định attachment là dạng link hay ko
        /// </summary>
        public bool IsLink { get; set; }

        /// <summary>
        /// Link download attachment
        /// </summary>
        public string AttachLink { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tệp đính kèm liên quan
        /// </summary>
        public virtual Attachment Attachment { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vị trí lưu file
        /// </summary>
        public virtual FileLocation FileLocation { get; set; }

        /// <summary>
        /// <para>Tra ve key duy nhat xac dinh mot phien ban file dinh kem tren Bkav eGov</para>
        /// <para>CuongNT@bkav.com - 240713</para>
        /// </summary>
        public string IdentityAttachmentDetail
        {
            get { return CreatedOnDate.ToString("yyyyMMdd") + FileName; }
        }
    }
}
