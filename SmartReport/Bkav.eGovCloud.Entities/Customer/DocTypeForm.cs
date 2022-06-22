using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTypeForm - public - Entity
    /// Access Modifiers: 
    /// Create Date : 01102013
    /// Author      : DungHV
    /// Description : Entity tương ứng với bảng DocTypeForm trong CSDL
    /// </summary>
    public class DocTypeForm
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản biểu mẫu
        /// </summary>
        public int DocTypeFormId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id biểu mẫu
        /// </summary>
        public Guid FormId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hồ sơ đã được kích hoạt
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra biểu mẫu là mẫu chính
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập đối tượng Form tương ứng
        /// </summary>
        public Form Form { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập đối tượng doctype tương ứng
        /// </summary>
        public DocType DocType { get; set; }
    }
}
