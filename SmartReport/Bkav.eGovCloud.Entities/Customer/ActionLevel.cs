using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActionLevel - public - Entity
    /// Access Modifiers: 
    /// Create Date : 190620
    /// Author      : SuBD
    /// Description : Entity tương ứng với bảng ActionLevel trong CSDL
    /// </summary>
    public class ActionLevel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id kỳ báo cáo
        /// </summary>
        public int ActionLevelId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên kỳ báo cáo
        /// </summary>
        public string ActionLevelName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mã kỳ báo cáo
        /// </summary>
        public string ActionLevelCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thời gian bắt đầu
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thời gian kết thúc
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key lưu trữ
        /// </summary>
        public string TemplateKey { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo ra kỳ báo cáo này
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo kỳ báo cáo
        /// </summary>
        public DateTime? CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người thay đổi cuối cùng
        /// </summary>
        public int? LastModifiedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày thay đổi cuối cùng
        /// </summary>
        public DateTime? LastModifiedOnDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] VersionByte { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }
    }
}
