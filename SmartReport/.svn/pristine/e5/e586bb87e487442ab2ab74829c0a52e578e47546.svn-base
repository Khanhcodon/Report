using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using OpenPop.Mime;
using OpenPop.Pop3;
using S22.Imap;

namespace Bkav.eGovCloud.Core.Mail
{
    /// <summary>
    /// 
    /// </summary>
    public class MailPop3IMapUtil
    {
        private string _mailServer;
        private int _port;
        private bool _hasSSL;
        private bool _isIMAP;

        private Pop3Client _pop3Client;
        private ImapClient _imapClient;

        private bool _isAuthenticated;
        private int _startToGetMail = 50;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mailServer"></param>
        /// <param name="port"></param>
        /// <param name="hasSSL"></param>
        public MailPop3IMapUtil(string mailServer, int port, bool hasSSL)
        {
            _isIMAP = port == 143 || port == 993 || mailServer.ToLower().Contains("imap");
            _mailServer = mailServer;
            _port = port;
            _hasSSL = hasSSL;
        }

        /// <summary>
        /// Kiểm tra kết nối đến server
        /// </summary>
        /// <returns></returns>
        public bool TestConnect()
        {
            var result = false;
            _isAuthenticated = false;
            try
            {
                if (_isIMAP)
                {
                    _imapClient = NewIMAPClient();
                }
                else
                {
                    _pop3Client = NewPop3Client();
                }
                result = true;
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// Xác thực user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckLoginSuccess(string username, string password)
        {
            if (!_isAuthenticated)
            {
                try
                {
                    // Authenticate ourselves towards the server
                    if (_isIMAP)
                    {
                        _imapClient.Login(username, password, AuthMethod.Auto);
                    }
                    else
                    {
                        _pop3Client.Authenticate(username, password);
                    }
                    _isAuthenticated = true;
                }
                catch (Exception)
                {
                }
            }
            return _isAuthenticated;
        }

        /// <summary>
        /// Lấy ra message
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isGetBody"></param>
        /// <returns></returns>
        public MailInfo GetMessage(int id, bool? isGetBody = false)
        {
            try
            {
                var pop3Message = _pop3Client.GetMessage(id);
                var message = pop3Message.ToMailMessage();
                return new MailInfo
                {
                    Id = id,
                    Subject = message.Subject,
                    EmailSender = message.From.Address,
                    DisplayName = message.From.DisplayName,
                    Content = isGetBody.HasValue ? isGetBody.Value ? message.Body : "" : "",
                    Date = DateTime.Parse(pop3Message.Headers.Date)
                };
            }
            catch (Exception)
            {

            }
            return null;
        }

        /// <summary>
        /// Lấy danh sách thông tin Id, UId của mail
        /// </summary>
        /// <param name="identifyId"></param>
        /// <returns></returns>
        public IEnumerable<MessageInfo> GetMessageInfos(int identifyId)
        {
            return _pop3Client.GetMessageInfos().Where(x => x.Number > identifyId).OrderByDescending(x => x.Number).Take(_startToGetMail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastestMailId"></param>
        /// <returns></returns>
        public List<MailInfo> GetMessages(int lastestMailId = 0)
        {
            var result = new List<MailInfo>();
            if (_isIMAP)
            {

            }
            else
            {
                var messageInfos = GetMessageInfos(lastestMailId);
                foreach (var messageInfo in messageInfos)
                {
                    var mailInfo = GetMessage(messageInfo.Number);
                    if (mailInfo != null)
                    {
                        mailInfo.Size = messageInfo.Size;

                        result.Add(mailInfo);
                    }
                }
            }

            return result;
        }

        private ImapClient NewIMAPClient()
        {
            return new ImapClient(_mailServer, _port, _hasSSL);
        }

        private Pop3Client NewPop3Client()
        {
            var client = new Pop3Client();
            client.Connect(_mailServer, _port, _hasSSL, 60000, 60000,
            new RemoteCertificateValidationCallback(ValidateServerCertificate));
            return client;
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;  // force the validation of any certificate
        }

    }
}
