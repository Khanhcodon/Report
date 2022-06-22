using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Comment - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Comment trong CSDL
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id ý kiến
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản, hồ sơ liên quan
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Vệt tương ứng
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập document copy của hướng nhận
        /// </summary>
        public int? DocumentCopyTargetId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nội dung ý kiến
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Nội dung chi tiết ý kiến dạng json
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại ý kiến
        /// </summary>
        public byte CommentType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người gửi ý kiến
        /// </summary>
        public int? UserSendId { get; set; }

        /// <summary>
        /// Người nhận ý kiến.
        /// </summary>
        public int? UserReceiveId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày gửi ý kiến
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Comment cha
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Phòng ban xử lý chính
        /// </summary>
        public string MainProcessDepartmentName { get; set; }

        /// <summary>
        /// Phòng phối hợp xử lý
        /// </summary>
        public string CoProcessDepartmentName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại ý kiến
        /// </summary>
        public CommentType CommentTypeInEnum
        {
            get { return (CommentType)CommentType; }
            set { CommentType = (byte)value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập thông tin nội dung người ủy quyền xử lý văn bản
        /// </summary>
        public string Content2 { get; set; }

        /// <summary>
        /// Danh sách các comment con
        /// </summary>
        public IEnumerable<Comment> Children { get; set; }

        /// <summary>
        /// VuHQ20200317
        /// Chứa nội dung data/ cấu hình của handsontable khi tạo/ chuyển báo cáo
        /// </summary>
        public string Diff { get; set; }
    }

}
