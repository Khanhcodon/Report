using System;

namespace Bkav.eGovCloud.Models
{
    public class DocumentContentDetailModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id phiên bản nội dung chi tiết của vb,hs
        /// </summary>
        public int DocumentContentDetailId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id nội dung của vb,hs
        /// </summary>
        public int DocumentContentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo nội dung của vb,hs
        /// </summary>
        public int CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập của người tạo nội dung của vb,hs
        /// </summary>
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        public DateTime CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Nội dung của vb,hs
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Phiên bản
        /// </summary>
        public int Version { get; set; }
    }
}