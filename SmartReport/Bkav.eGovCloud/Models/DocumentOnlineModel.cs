using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Validator;

namespace Bkav.eGovCloud.Models
{
    [FluentValidation.Attributes.Validator(typeof(DocumentOnlineValidator))]
    public class DocumentOnlineModel
    {
        public DocumentOnlineModel()
        {
            this.Files = new List<File>();
            this.PostedFiles = new List<HttpPostedFileBase>();
            this.Forms = new List<Form>();
            this.Status = 1;
            this.IsViewed = false;
        }

        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Mã hồ sơ
        /// </summary>
        public string DocCode
        {
            get
            {
                var random = new Random((int)DateTime.Now.Ticks);
                var builder = new StringBuilder();
                char ch;
                for (int i = 0; i < 8; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Id loại hồ sơ tương ứng
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Id hồ sơ khi trực tiếp nhận sang egov
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Ngày nhận đăng ký qua mạng
        /// </summary>
        public DateTime DateReceived
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Ngày hẹn trả
        /// </summary>
        public DateTime DateAppoint { get; set; }

        /// <summary>
        /// Trạng thái xử lý hồ sơ
        /// 1. Chờ duyệt - Default
        /// 2. Đang xử lý
        /// 3. Chờ bổ sung
        /// 4. Chờ thanh toán lệ phí
        /// 5. Chờ trả kết quả
        /// 6. Đã trả kết quả
        /// 7. Bị từ chối
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Thông tin công dân,doanh nghiệp
        /// </summary>
        public string PersonInfo { get; set; }

        /// <summary>
        /// Số CMT
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Chuỗi Json chứa thông tin tờ khai
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JsonForm { get; set; }

        /// <summary>
        /// Doctype
        /// </summary>
        public DocType DocType { get; set; }

        /// <summary>
        /// Forms
        /// </summary>
        public List<Form> Forms { get; set; }

        /// <summary>
        /// File
        /// </summary>
        public ICollection<File> Files { get; set; }

        public string Comment { get; set; }

        public List<HttpPostedFileBase> PostedFiles { get; set; }

        public bool IsViewed { get; set; }
    }
}