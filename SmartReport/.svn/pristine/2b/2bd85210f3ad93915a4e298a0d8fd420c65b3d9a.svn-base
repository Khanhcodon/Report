using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Models
{
    public class AttachmentPreviewModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của tệp đính kèm
        /// </summary>
        public int AttachmentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên tệp đính kèm
        /// </summary>
        public string AttachmentName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của văn bản, hồ sơ chứa tệp đính kèm
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Phiên bản của tệp đính kèm
        /// </summary>
        public int VersionAttachment { get; set; }

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
        /// Danh sách file preview của tệp đính kèm.
        /// </summary>
        public List<string> PreviewFiles { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Các thông tin chi tiết về tệp đính kèm
        /// </summary>
        public IEnumerable<AttachmentDetailModel> AttachmentDetails { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái xác định đang xem file PDF
        /// </summary>
        public bool IsPdfFile { get; set; }
    }

    public class AttachmentDetailModel
    {
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
    }
}