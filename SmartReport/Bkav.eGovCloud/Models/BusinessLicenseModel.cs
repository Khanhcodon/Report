using System;
using Bkav.eGovCloud.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Models
{
    [FluentValidation.Attributes.Validator(typeof(BusinessLicenseValidator))]
    public class BusinessLicenseModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id giấy phép
        /// </summary>
        public int BusinessLicenseId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.BusinessId.Label")]
        public int BusinessId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên doanh nghiệp
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại hồ sơ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.DocTypeId.Label")]
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.DocumentId.Label")]
        public Guid? DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ copy
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.DocumentCopyId.Label")]
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id trạng thái giấy phép
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.LicenseStatusId.Label")]
        public int LicenseStatusId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã giấy phép
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.LicenseCode.Label")]
        public string LicenseCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số giấy phép
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.LicenseNumber.Label")]
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày đăng ký giấy phép
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.RegisDate.Label")]
        public DateTime RegisDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày cấp của giấy phép
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.IssueDate.Label")]
        public DateTime IssueDate { get; set; }

        public string IssueDateShort
        {
            get { return IssueDate.ToShortDateString(); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập ngày hết hạn của giấy phép
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.ExpireDate.Label")]
        public DateTime ExpireDate { get; set; }

        public string ExpireDateShort
        {
            get { return ExpireDate.ToShortDateString(); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập ngày thông báo hết hạn của giấy phép
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.ExpireAlertDate.Label")]
        public DateTime? ExpireAlertDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày thu hồi giấy phép
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.BusinessLicense.CreateOrEdit.Fields.RevocationDate.Label")]
        public DateTime? RevocationDate { get; set; }

        public string FilePath { get; set; }

        public BusinessLicenseModel()
        {
            RegisDate = DateTime.Now;
            IssueDate = DateTime.Now;
            ExpireDate = DateTime.Now;
        }
    }
}