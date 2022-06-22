
using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Mail;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business
{
    /// <summary>
    /// TrinhNVd: Lớp chứa các phương thức xác thực mail thông qua IMAP/POP3
    /// </summary>
    public class MailHelper
    {
        private MailPop3IMapUtil _mailPop3Util;
        private static UserBll _userServices;
        private static NotificationBll _notificationServices;
        private bool _isAuthenticated;
        /// <summary>
        /// Ctor
        /// </summary>
        public MailHelper(UserBll userServices, NotificationBll notificationServices)
        {
            _userServices = userServices;
            _notificationServices = notificationServices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pop3ServerUrl"></param>
        /// <param name="pop3Port"></param>
        /// <param name="hasSSL"></param>
        public void ServerDefine(string pop3ServerUrl, int pop3Port, bool hasSSL)
        {
            _mailPop3Util = new MailPop3IMapUtil(pop3ServerUrl, pop3Port, hasSSL);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsAuthenticated(string username, string password)
        {
            if (!_isAuthenticated)
            {
                if (_mailPop3Util.TestConnect())
                {
                    _isAuthenticated = _mailPop3Util.CheckLoginSuccess(username, password);
                    return _isAuthenticated;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastestMailId"></param>
        /// <returns></returns>
        public List<Notification> GetLastestMail(int lastestMailId = 0)
        {
            var result = new List<Notification>();
            if (_isAuthenticated)
            {
                var newMails = _mailPop3Util.GetMessages(lastestMailId);
                foreach (var newMail in newMails)
                {
                    result.Add(new Notification
                    {
                        MailId = newMail.Id.ToString(), //Comment
                        NotificationType = (int)NotificationType.Mail,
                        Date = DateTime.Now,
                        ReceiveDate = newMail.Date,
                        SenderUserName = newMail.EmailSender,
                        SenderFullName = newMail.DisplayName,
                        Title = newMail.Subject,
                        Content = newMail.Content
                    });
                }
            }
            return result;
        }
    }

}
