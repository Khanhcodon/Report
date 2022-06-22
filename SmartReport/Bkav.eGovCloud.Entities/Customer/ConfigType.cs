using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Class : ConfigType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 20191101
    /// Author      : VuHQ
    /// Description : Entity tương ứng với bảng config_type trong CSDL
    /// </summary>
    public class ConfigType
    {
        /// <summary>
        /// Lấy hoặc thiết lập id loại cấu hình
        /// </summary>
        [Key]
        public int TypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên loại cấu hình
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên kiểu của field trong MYSQL
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại cấu hình cha của field
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập format hiển thị của loại cấu hình
        /// </summary>
        public string DisplayCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập RegX validate cho loại cấu hình
        /// </summary>
        public string PatternCode { get; set; }
    }
}
