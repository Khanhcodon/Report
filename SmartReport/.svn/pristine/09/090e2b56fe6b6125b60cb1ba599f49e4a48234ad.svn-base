using Bkav.eGovCloud.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Models
{
    [FluentValidation.Attributes.Validator(typeof(ChangePasswordValidator))]
    public class ChangePasswordModel
    {
        /// <summary>
        /// Mật khẩu hiện tại
        /// </summary>
        [LocalizationDisplayName("Account.ChangePassword.Fields.CurrentPassword.Label")]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// mật khẩu mới
        /// </summary>
        [LocalizationDisplayName("Account.ChangePassword.Fields.NewPassword.Label")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Nhập lại mật khẩu mới
        /// </summary>
        [LocalizationDisplayName("Account.ChangePassword.Fields.ConfirmPassword.Label")]
        public string ConfirmPassword { get; set; }
    }
}