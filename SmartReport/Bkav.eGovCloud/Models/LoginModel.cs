using System.ComponentModel.DataAnnotations;

namespace Bkav.eGovCloud.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Bạn phải nhập tên đăng nhập")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        public string Password { get; set; }

        public string OpenId { get; set; }

        public string Language { get; set; }

        public bool UseCaptcha { get; set; }

        public bool RememberMe { get; set; }

        public string MAC { get; set; }

        public bool IsQTTT { get; set; }
    }
}