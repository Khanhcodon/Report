using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Mvc.Mailer;

namespace Bkav.eGovCloud.Mailers
{
    public sealed class UserMailer : MailerBase, IUserMailer, IDisposable
    {
        private readonly EmailSettings _emailSettings;
        private readonly ResourceBll _resourceService;
        private ISmtpClient _smtpClient;

        private MailMessage DefaultMailMessage
        {
            get
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SmtpUsername, _emailSettings.DisplayName, Encoding.UTF8)
                };
                return mailMessage;
            }
        }

        public UserMailer(EmailSettings emailSettings, ResourceBll resourceService)
        {
            MasterName = "_LayoutEmail";
            _emailSettings = emailSettings;
            _resourceService = resourceService;
        }

        public ISmtpClient SmtpClient
        {
            get
            {
                if (_smtpClient != null)
                {
                    return _smtpClient;
                }

                if (string.IsNullOrWhiteSpace(_emailSettings.SmtpServer)
                    || string.IsNullOrWhiteSpace(_emailSettings.SmtpPassword)
                    || string.IsNullOrWhiteSpace(_emailSettings.SmtpUsername)
                    || _emailSettings.SmtpPort <= 0)
                {
                    _smtpClient = new SmtpClientWrapper();
                    return _smtpClient;
                }
                var smtpClient = new SmtpClient
                                     {
                                         EnableSsl = _emailSettings.EnableSsl,
                                         Host = _emailSettings.SmtpServer,
                                         Port = _emailSettings.SmtpPort,

                                         Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword.Base64Decode())
                                     };
                _smtpClient = new SmtpClientWrapper(smtpClient);
                return _smtpClient;
            }
        }

        public MailMessage Test()
        {
            var mailMessage = DefaultMailMessage;
            mailMessage.Subject = "Test";
            PopulateBody(mailMessage, "Test");

            return mailMessage;
        }

        public MailMessage NotifyCreateUser(string fullName, string username, string password)
        {
            var mailMessage = DefaultMailMessage;
            mailMessage.Subject = _resourceService.GetResource("Email.NotifyCreateUser.Subject");

            ViewBag.FullName = fullName;
            ViewBag.Username = username;
            ViewBag.Password = password;

            PopulateBody(mailMessage, "NotifyCreateUser");

            return mailMessage;
        }

        public MailMessage ResetPassword(string fullName, string username, string password)
        {
            var mailMessage = DefaultMailMessage;
            mailMessage.Subject = _resourceService.GetResource("Email.ResetPassword.Subject");

            ViewBag.FullName = fullName;
            ViewBag.Username = username;
            ViewBag.Password = password;

            PopulateBody(mailMessage, "ResetPassword");

            return mailMessage;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_smtpClient != null)
                {
                    _smtpClient.Dispose();
                }
            }
        }

        #endregion IDisposable Members
    }
}