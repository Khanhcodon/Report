using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bkav.eGovCloud.Entities.Customer.eDoc
{
    /// <summary>
    /// Thông tin hồ sơ liên thông
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Khởi tạ
        /// </summary>
        public Document()
        {
            PromulgationDate = DateTime.Now.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Id hồ sơ
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// Trích yếu hoặc tiêu đề hồ sơ
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Thông tin tổ chức gửi hồ sơ
        /// </summary>
        public Organization Sender {get; set;}

        /// <summary>
        /// Danh sách các tổ chức nhận hồ sơ
        /// </summary>
        public List<Organization> Receivers {get; set;}

        /// <summary>
        /// Số hồ sơ. Ví dụ: 12
        /// </summary>
        public string CodeNumber { get; set; }

        /// <summary>
        /// Ký hiệu hồ sơ. Ví dụ: BTTT-THH
        /// </summary>
        public string CodeNotation { get; set; }

        /// <summary>
        /// Nơi ban hành. Ví dụ: Hà Nội
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Ngày ban hành. Định dạng dd/MM/yyyy
        /// </summary>
        public string PromulgationDate { get; set; }

        /// <summary>
        /// Mã loại hồ sơ
        /// </summary>
        public string TypeCode { get; set; }
        
        /// <summary>
        /// Tên loại hồ sơ
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Tên người ký, chịu trách nhiệm ban hành hồ sơ
        /// </summary>
        public string SignerName { get; set; }

        /// <summary>
        /// Hạn xử lý hồ sơ. Định dạng dd/MM/yyyy. Mặc định là empty.
        /// </summary>
        public string DueDate { get; set; }

        /// <summary>
        /// Độ ưu tiên. Theo thứ tự Lớn dần từ 0 đến 3. Mặc 
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Danh sách hồ sơ liên quan
        /// </summary>
        public List<RelatedDocument> Relateds { get; set; }

        /// <summary>
        /// Ngày hẹn trả. Định dạng dd/MM/yyyy.
        /// </summary>
        public string DateAppointed { get; set; }

        /// <summary>
        /// Danh sách lệ phí
        /// </summary>
        public List<Fee> Fees { get; set; }

        /// <summary>
        /// Danh sách giấy tờ liên quan
        /// </summary>
        public List<DocumentPaper> DocumentPapers { get; set; }

        /// <summary>
        /// Thông tin công dân
        /// </summary>
        public Citizen CitizenInfo { get; set; }

        /// <summary>
        /// Danh sách đính kèm bao gồm cả form động (nếu có)
        /// </summary>
        public List<Attachment> Attachments { get; set; }
    }
}
