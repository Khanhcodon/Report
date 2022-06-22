using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportModes
    {
        /// <summary>
        /// 
        /// </summary>
        public int ReportModeId { get; set; }

        /// <summary>
        /// Mã chế độ báo cáo
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Tên chế độ báo cáo
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nội dung yêu cầu báo cáo
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Cơ quan ban hành chế độ báo cáo
        /// </summary>
        public string IssueOrg { get; set; }
        /// <summary>
        /// Ngày phát hành văn bản quy định chế độ báo cáo
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Số văn bản quy định chế độ báo cáo 
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Ký hiệu văn bản quy định chế độ báo cáo 
        /// </summary>
        public string Notation { get; set; }

        /// <summary>
        /// Số ký hiệu văn bản làm căn cứ ban hành chế độ báo cáo
        /// </summary>
        public string RefNotation { get; set; }

        /// <summary>
        /// Loại chế độ báo cáo:
        /// </summary>
        public int ReportMode { get; set; }

        /// <summary>
        /// Phần chứa các tập tin đính kèm 
        /// </summary>
        public string Attachments { get; set; }
       
    }
}
