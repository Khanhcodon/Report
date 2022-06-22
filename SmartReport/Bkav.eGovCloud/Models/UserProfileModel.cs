using Bkav.eGovCloud.Validator;
using Bkav.eGovCloud.Web.Framework;
using System.ComponentModel.DataAnnotations;

namespace Bkav.eGovCloud.Models
{
    [FluentValidation.Attributes.Validator(typeof(UserProfileValidator))]
    public class UserProfileModel
    {
        ///// <summary>
        ///// Lấy hoặc thiết lập Họ và tên
        ///// </summary>
        //[LocalizationDisplayName("User.CreateOrEdit.Fields.FullName.Label")]
        //public string FullName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.FirstName.Label")]
        public string FirstName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Họ và tên đệm
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.LastName.Label")]
        public string LastName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giới tính: Nam (true), Nữ (false)
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.Gender.Label")]
        public bool Gender { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số điện thoại
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.Phone.Label")]
        public string Phone { get; set; }

        [LocalizationDisplayName("Email")]
        public string Email { get; set; }

        /// <summary>
        /// Ma code nguoi dung nhap vao
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Fax
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.Fax.Label")]
        public string Fax { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Địa chỉ
        /// </summary>
        [LocalizationDisplayName("User.CreateOrEdit.Fields.Address.Label")]
        public string Address { get; set; }

        /// <summary>
        /// Xac dinh co show code hay khong
        /// </summary>
        public bool IsShowCode { get; set; }
    }
}