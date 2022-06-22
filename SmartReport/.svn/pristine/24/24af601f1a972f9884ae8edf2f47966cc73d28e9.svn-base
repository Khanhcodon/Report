using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Models
{
    [FluentValidation.Attributes.Validator(typeof(BusinessValidator))]
    public class BusinessModel
    {
        private ICollection<BusinessLicenseModel> _businessLicenses;

        /// <summary>
        /// Lấy hoặc thiết lập Id của doanh nghiệp
        /// </summary>
        public int BusinessId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.BusinessName.Label")]
        public string BusinessName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của loại hình doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.BusinessTypeId.Label")]
        public int BusinessTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên nước ngoài của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.ForeignName.Label")]
        public string ForeignName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên viết tắt của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.AbbreviationName.Label")]
        public string AbbreviationName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã đăng ký kinh doanh của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.BusinessCode.Label")]
        public string BusinessCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nơi cấp mã đăng ký kinh doanh của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.IssueCodeby.Label")]
        public string IssueCodeby { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vốn điều lệ của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.Capital.Label")]
        public long? Capital { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vốn pháp định của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.LegalCapital.Label")]
        public long? LegalCapital { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số điện thoại của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.Phone.Label")]
        public string Phone { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số fax của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.Fax.Label")]
        public string Fax { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ thư điện tử của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.Email.Label")]
        public string Email { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập website của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.Website.Label")]
        public string Website { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.Address.Label")]
        public string Address { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id địa chỉ xã/phường của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.WardId.Label")]
        public int? WardId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id địa chỉ quận/huyện của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.DistricCode.Label")]
        public string DistrictCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id địa chỉ tỉnh/thành phố của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.CityCode.Label")]
        public string CityCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày cấp mã đăng ký kinh doanh của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.IssueDate.Label")]
        public DateTime IssueDate { get; set; }

        public string IssueDateShort
        {
            get { return IssueDate.ToShortDateString(); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập ngày hết hạn giấy phép ĐKKD của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.ExpireDate.Label")]
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày thu hồi giấy phép ĐKKD của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.RevocationDate.Label")]
        public DateTime? RevocationDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên người đại diện của doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.UserName.Label")]
        public string UserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giới tính: Nam (true), Nữ (false)
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.Gender.Label")]
        public bool Gender { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày sinh của người đại diện doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.Birthday.Label")]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ thường trú của người đại diện doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.PermanentAddress.Label")]
        public string PermanentAddress { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ tạm trú của người đại diện doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.TemporaryAddress.Label")]
        public string TemporaryAddress { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số điện thoại của người đại diện doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.UserPhone.Label")]
        public string UserPhone { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập email của người đại diện doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.UserEmail.Label")]
        public string UserEmail { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số cmtndi của người đại diện doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.IdCard.Label")]
        public string IdCard { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày cấp cmtnd của người đại diện doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.IdCardDate.Label")]
        public DateTime? IdCardDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nơi cấp cmtnd của người đại diện doanh nghiệp
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Business.CreateOrEdit.Fields.IdCardPlace.Label")]
        public string IdCardPlace { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Các giấy phép
        /// </summary>
        public virtual ICollection<BusinessLicenseModel> BusinessLicenses
        {
            get { return _businessLicenses ?? (_businessLicenses = new List<BusinessLicenseModel>()); }
            set
            {
                value = _businessLicenses;
            }
        }

        public BusinessModel()
        {
            IssueDate = DateTime.Now;
            ExpireDate = DateTime.Now;
            IdCardDate = DateTime.Now;
        }
    }
}