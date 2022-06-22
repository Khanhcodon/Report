using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PasswordPolicySettings - public - Entity
    /// Access Modifiers:
    ///     * Implement: ISettings
    /// Create Date : 300812
    /// Author      : TrungVH
    /// Description : Entity cho phần cấu hình chính sách mật khẩu
    /// </summary>
    public class PasswordPolicySettings : ISettings
    {
        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống cho phép kiểm tra cú pháp mật khẩu theo cấu hình
        /// </summary>
        public bool EnableSyntaxChecking { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Độ dài tối thiểu của mật khẩu
        /// </summary>
        public int MinimumLength { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số lượng tối thiểu của các chữ thường trong mật khẩu
        /// </summary>
        public int MinimumLowerCase { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số lượng tối thiểu của các chữ hoa trong mật khẩu
        /// </summary>
        public int MinimumUpperCase { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số lượng tối thiểu của các chữ số trong mật khẩu
        /// </summary>
        public int MinimumNumbers { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số lượng tối thiểu của các ký tự đặc biệt trong mật khẩu
        /// </summary>
        public int MinimumSymbols { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thời gian tối đa mà 1 mật khẩu được sử dụng (tính theo giây)
        /// </summary>
        public int MaximumAge { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra trước thời gian hết hạn mật khẩu bao lâu thì hệ thống sẽ thông báo cho người dùng biết (tính theo giây)
        /// </summary>
        public int WarningTime { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống bắt người dùng sẽ phải thay đổi mật khẩu trong 1 khoảng thời gian nhất định
        /// </summary>
        public bool EnableExpiration { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống sẽ khóa người dùng trong 1 khoảng thời gian nhất định khi đăng nhập nhiều lần không thành công
        /// </summary>
        public bool EnableLockout { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số lần đăng nhập không thành công tối đa
        /// </summary>
        public int MaximumLogonFailure { get; set; }

        /// <summary>
        /// Số tên đăng nhập được nhập tối đa đăng nhập trong 1 phút
        /// </summary>
        public int MaximumUserInput { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thời gian khóa người dùng (tính bằng giây)
        /// </summary>
        public int LockoutDuration { get; set; }

        /// <summary>
        /// Sử dụng Captcha để đăng nhập nếu đăng nhập quá số lần đăng nhập không thành công
        /// </summary>
        public bool EnableCaptcha { get; set; }

        /// <summary>
        /// Kiểu captcha
        /// </summary>
        public CaptchaType CaptchaType { get; set; }

        /// <summary>
        /// Số ký tự capcha
        /// </summary>
        public int CaptchaKeyNumbers { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống sẽ lưu lại mật khẩu cũ của người dùng và ngăn cản người dùng không sử dụng mật khẩu cũ
        /// </summary>
        public bool EnableHistory { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số lượng mật khẩu cũ được giữ lại để kiểm tra
        /// </summary>
        public int HistoryCount { get; set; }
       
        /// <summary>
        /// Lấy hoặc thiết lập mật khẩu mặc định khi tạo mới hàng loạt
        /// </summary>
        public string DefaultCreatePassword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mật khẩu mặc định để Reset
        /// </summary>
        public string DefaultResetPassword { get; set; }

        /// <summary>
        /// Lấy ra chuỗi regular expression cho mật khẩu
        /// </summary>
        /// <returns>Chuỗi regular expression cho mật khẩu</returns>
        public string PasswordExpression
        {
            get
            {
                return @"("
                    + (MinimumNumbers > 0
                        ? @"(?=(.*\d){" + MinimumNumbers + @",})"
                        : string.Empty)
                    + (MinimumLowerCase > 0
                        ? "(?=(.*[a-z]){" + MinimumLowerCase + @",})"
                        : string.Empty)
                    + (MinimumUpperCase > 0
                        ? "(?=(.*[A-Z]){" + MinimumUpperCase + @",})"
                        : string.Empty)
                    + (MinimumSymbols > 0
                        ? @"(?=(.*[ \\\-~!@#$%^*()_+{}:|""?`;',./[\]]){" + MinimumSymbols + @",})"
                        : string.Empty)
                    + (MinimumLength > 0
                        ? @".{" + MinimumLength + @",}"
                        : string.Empty)
                    + ")";
            }
        }
    }
}