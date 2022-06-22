using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessLicense - public - Entity
    /// Access Modifiers: 
    /// Create Date : 161013
    /// Author      : DungHV
    /// Description : Entity tương ứng với bảng BusinessLicense trong CSDL
    /// </summary>
    public class BusinessLicense
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id giấy phép
        /// </summary>
        public int BusinessLicenseId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id doanh nghiệp
        /// </summary>
        public int BusinessId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ
        /// </summary>
        public Guid? DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ copy
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại hồ sơ
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id trạng thái giấy phép
        /// </summary>
        public int LicenseStatusId { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập mã giấy phép
        /// </summary>
        public string LicenseCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số giấy phép
        /// </summary>
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày đăng ký giấy phép
        /// </summary>
        public DateTime RegisDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày cấp của giấy phép
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày hết hạn của giấy phép
        /// </summary>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày thông báo hết hạn của giấy phép
        /// </summary>
        public DateTime? ExpireAlertDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày thu hồi giấy phép
        /// </summary>
        public DateTime? RevocationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CreateByUserId { get; set; }
    }
}
