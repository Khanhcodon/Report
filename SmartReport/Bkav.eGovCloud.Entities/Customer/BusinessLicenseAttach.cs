using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessLicenseAttach - public - Entity
    /// Access Modifiers: 
    /// Create Date : 01102013
    /// Author      : DungHV
    /// Description : Entity tương ứng với bảng BusinessLicenseAttach trong CSDL
    /// </summary>
    public class BusinessLicenseAttach
    {
        /// <summary>
        /// Lấy hoặc thiết lập id file giấy phép tương ứng
        /// </summary>
        public int BusinessLicenseAttackId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id giấy phép tương ứng
        /// </summary>
        public int BusinessLicenseId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tệp đính kèm giấy phép tương ứng
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kiểu lưu file
        /// </summary>
        public int? FileLocationId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên file được lưu trữ trên server
        /// </summary>
        public string FileLocationName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục root lưu file
        /// </summary>
        public string FileLocationKey { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục tự tăng lưu file
        /// </summary>
        public string IdentityFolder { get; set; }
        
    }
}
