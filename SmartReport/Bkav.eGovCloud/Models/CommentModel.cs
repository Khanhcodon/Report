using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Models
{
    public class DiffModel
    {
        public DiffModel() { }

        public string Data { get; set; }
        public string Configs { get; set; }
    }

    public class CommentModel
    {
        private User _userSend;

        public CommentModel()
        {
            Children = new List<CommentModel>();
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id ý kiến
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản, hồ sơ liên quan
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Nội dung ý kiến
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 20200317 VuHQ lưu trữ json cấu hình/ data của handsontable khi tạo/ chuyển báo cáo
        /// </summary>
        public DiffModel Diff { get; set; }

        /// <summary>
        /// Vệt tương ứng
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập document copy của hướng nhận
        /// </summary>
        public int? DocumentCopyTargetId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày gửi ý kiến
        /// </summary>
        public DateTime DateCreated { get; set; }

        private string _dateCreatedString;
        public string DateCreatedString
        {
            get {
                var format = "HH:mm dd/MM";
                if (DateCreated.Year != DateTime.Now.Year)
                {
                    format = "HH:mm dd/MM/yy";
                }

                return DateCreated.ToString(format);
            }
            set { _dateCreatedString = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id người gửi ý kiến
        /// </summary>
        public int? UserSendId { get; set; }

        /// <summary>
        /// Người nhận ý kiến.
        /// </summary>
        public int? UserReceiveId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại ý kiến
        /// </summary>
        public byte CommentType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra ý kiến này đa được nén
        /// </summary>
        /// <value>
        /// 	<c>true</c> nếu ý kiến này đã được nén; ngược lại, <c>false</c>.
        /// </value>
        public bool IsCompressed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 
        /// </summary>
        public string ToDepartment { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Người gửi ý kiến
        /// </summary>
        public User UserSend
        {
            get
            {
                // Mục đích để chỉ trả về các thông tin cần thiết để trả về client sử dụng
                return _userSend == null ? null : new User
                    {
                        UserId = _userSend.UserId,
                        Username = _userSend.Username,
                        FullName = _userSend.FullName
                    };
            }
            set { _userSend = value; }
        }

        public List<CommentModel> Children { get; set; }

        /// <summary>
        /// Kết quả sử lý: sử dụng khi lấy ý kiến ý duyệt, bổ sung.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thông tin nội dung người ủy quyền xử lý văn bản
        /// </summary>
        public string Content2 { get; set; }
    }
}