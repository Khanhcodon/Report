using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models.Settings
{
    [FluentValidation.Attributes.Validator(typeof(PasswordPolicySettingsValidator))]
    public class PasswordPolicySettingsModel
    {
        private int? _minimumLowerCase;
        private int? _minimumUpperCase;
        private int? _minimumNumbers;
        private int? _minimumSymbols;

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống cho phép kiểm tra cú pháp mật khẩu theo cấu hình
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.EnableSyntaxChecking.Label")]
        public bool EnableSyntaxChecking { get ; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Độ dài tối thiểu của mật khẩu
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.MinimumLength.Label")]
        public int MinimumLength { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số lượng tối thiểu của các chữ thường trong mật khẩu
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.MinimumLowerCase.Label")]
        public int? MinimumLowerCase
        {
            get
            {
                if (!_minimumLowerCase.HasValue)
                {
                    _minimumLowerCase = 0;
                }
                return _minimumLowerCase;
            }
            set { _minimumLowerCase = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Số lượng tối thiểu của các chữ hoa trong mật khẩu
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.MinimumUpperCase.Label")]
        public int? MinimumUpperCase
        {
            get
            {
                if(!_minimumUpperCase.HasValue)
                {
                    _minimumUpperCase = 0;
                }
                return _minimumUpperCase;
            }
            set { _minimumUpperCase = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Số lượng tối thiểu của các chữ số trong mật khẩu
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.MinimumNumbers.Label")]
        public int? MinimumNumbers
        {
            get
            {
                if (!_minimumNumbers.HasValue)
                {
                    _minimumNumbers = 0;
                }
                return _minimumNumbers;
            }
            set { _minimumNumbers = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Số lượng tối thiểu của các ký tự đặc biệt trong mật khẩu
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.MinimumSymbols.Label")]
        public int? MinimumSymbols
        {
            get
            {
                if (!_minimumSymbols.HasValue)
                {
                    _minimumSymbols = 0;
                }
                return _minimumSymbols;
            }
            set { _minimumSymbols = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Thời gian tối đa mà 1 mật khẩu được sử dụng (tính theo giây)
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.MaximumAge.Label")]
        public int? MaximumAge { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra trước thời gian hết hạn mật khẩu bao lâu thì hệ thống sẽ thông báo cho người dùng biết (tính theo giây)
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.WarningTime.Label")]
        public int? WarningTime { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống bắt người dùng sẽ phải thay đổi mật khẩu trong 1 khoảng thời gian nhất định
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.EnableExpiration.Label")]
        public bool EnableExpiration { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống sẽ khóa người dùng trong 1 khoảng thời gian nhất định khi đăng nhập nhiều lần không thành công
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.EnableLockout.Label")]
        public bool EnableLockout { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số lần đăng nhập không thành công tối đa
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.MaximumLogonFailure.Label")]
        public int? MaximumLogonFailure { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thời gian khóa người dùng (tính bằng giây)
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.LockoutDuration.Label")]
        public int? LockoutDuration { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống sẽ lưu lại mật khẩu cũ của người dùng và ngăn cản người dùng không sử dụng mật khẩu cũ
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.EnableHistory.Label")]
        public bool EnableHistory { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số lượng mật khẩu cũ được giữ lại để kiểm tra
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.HistoryCount.Label")]
        public int? HistoryCount { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mật khẩu mặc định để tạo mới hàng loạt
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.DefaultCreatePassword.Label")]
        public string DefaultCreatePassword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mật khẩu mặc định để Reset
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.DefaultResetPassword.Label")]
        public string DefaultResetPassword { get; set; }

        /// <summary>
        /// Sử dụng Captcha để đăng nhập nếu đăng nhập quá số lần đăng nhập không thành công
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.EnableCaptcha.Label")]
        public bool EnableCaptcha { get; set; }

        /// <summary>
        /// Kiểu captcha
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.CaptchaType.Label")]
        public CaptchaType CaptchaType { get; set; }

        /// <summary>
        /// Số ký tự capcha
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.CaptchaKeyNumbers.Label")]
        public int CaptchaKeyNumbers { get; set; }

        /// <summary>
        /// Số tên đăng nhập được nhập tối đa đăng nhập trong 1 phút
        /// </summary>
        [LocalizationDisplayName("Setting.PasswordPolicy.Fields.MaximumUserInput.Label")]
        public int MaximumUserInput { get; set; }
        
    }
}