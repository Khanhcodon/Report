using System.Net.Mail;
using Mvc.Mailer;

namespace Bkav.eGovCloud.Mailers
{
    public interface IUserMailer
    {
        ISmtpClient SmtpClient { get; }

        /// <summary>
        /// Gửi thử một email test
        /// </summary>
        /// <returns>MailMessage</returns>
        MailMessage Test();

        /// <summary>
        /// Gửi email thông báo tạo mới người dùng thành công
        /// </summary>
        /// <param name="fullName">Họ và tên</param>
        /// <param name="username">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns>MailMessage</returns>
        MailMessage NotifyCreateUser(string fullName, string username, string password);

        /// <summary>
        /// Gửi email thông báo reset mật khẩu thành công
        /// </summary>
        /// <param name="fullName">Họ và tên</param>
        /// <param name="username">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns>MailMessage</returns>
        MailMessage ResetPassword(string fullName, string username, string password);
    }
}