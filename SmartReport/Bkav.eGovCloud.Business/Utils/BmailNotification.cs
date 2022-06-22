using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Bmail.SOAP;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Newtonsoft.Json;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Business.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class BmailNotification
    {
        private string _bmailLink;
        private CurrentUserCached _user;

        private NotificationBll _notificationService;
        private AdminGeneralSettings _generalSettings;
        private LogBll _logService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bmailLink"></param>
        public BmailNotification(CurrentUserCached user, string bmailLink)
        {
            _notificationService = DependencyResolver.Current.GetService<NotificationBll>();
            _logService = DependencyResolver.Current.GetService<LogBll>();
            _generalSettings = DependencyResolver.Current.GetService<AdminGeneralSettings>();
            _bmailLink = bmailLink;
            _user = user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailtoken"></param>
        /// <param name="foldersCheck"></param>
        /// <returns></returns>
        public List<Notifications> GetLastestMail(string mailtoken, string foldersCheck)
        {
            var results = new List<Notifications>();
            if (string.IsNullOrWhiteSpace(mailtoken))
            {
                return results;
            }

            var mailSoapHelper = new MailSoapHelper(_bmailLink);
            var folderCheck = new string[] { "inbox" };

            if (!string.IsNullOrWhiteSpace(foldersCheck))
            {
                folderCheck = foldersCheck.Split(',');
            }

            var currnetMailNotifications = _notificationService.Gets(_user.UserId, 1, 50, hasDeleted: true);
            DateTime? lastDateUpdate = null;
            if (currnetMailNotifications.Any())
            {
                lastDateUpdate = currnetMailNotifications.Last().DateCreated;
            }

            var viewDate = DateTime.Now;

            foreach (var folderPath in folderCheck)
            {
                if (string.IsNullOrWhiteSpace(folderPath))
                {
                    continue;
                }
                var folderName = folderPath.Split('/').Last();

                try
                {
                    var mailSearchResponse = mailSoapHelper.CallSearchRequest(_user.UsernameEmailDomain, mailtoken, folderPath);
                    if (string.IsNullOrEmpty(mailSearchResponse))
                    {
                        continue;
                    }

                    var currentmailNotifies = new List<Notifications>();
                    mailSearchResponse = Regex.Replace(mailSearchResponse, @"\\x.{1,2}", "");
                    var responseParse = JsonConvert.DeserializeObject<MailSoapSearchResponse>(mailSearchResponse);
                    var mails = responseParse.Body.SearchResponse.m;

                    if (mails == null && !mails.Any())
                    {
                        continue;
                    }

                    mails = mails.Where(x => x.f == "u").OrderBy(x => x.id).ToList();
                    foreach (var mail in mails)
                    {
                        var mailId = mail.id;
                        var folderId = mail.l;
                        var date = mail.d;
                        var sender = mail.e.FirstOrDefault(x => x.t == "f");
                        var title = mail.su;
                        var content = string.IsNullOrWhiteSpace(mail.fr) ? "" : mail.fr;
                        var dateMail = ParseMailDate(date);

                        if ((lastDateUpdate.HasValue && dateMail <= lastDateUpdate.Value) || currnetMailNotifications.Any(m => m.GroupId == mail.id))
                        {
                            continue;
                        }

                        if (title.Contains("[Spam]"))
                        {
                            continue;
                        }

                        var avatar = GetUserSendAvatar(sender.a);

                        var notification = new Notifications
                        {
                            AppName = "bmail",
                            Body = title,
                            Title = string.Format("{1} đã gửi vào {0}", folderName, string.IsNullOrEmpty(sender.p) ? sender.a : sender.p),
                            GroupId = mailId,
                            DateCreated = dateMail,
                            IsSystemNotify = false,
                            IsReaded = false,
                            UserId = _user.UserId,
                            IsNew = true,
                            Avatar = avatar,
                            JsonData = Json2.Stringify(new BmailDataInfo()
                            {
                                MailId = mailId,
                                FolderId = folderId,
                                FolderLocation = folderPath,
                                Date = viewDate,
                                ReceiveDate = dateMail,
                                SenderUserName = sender.a,
                                SenderFullName = sender.p,
                                Content = content,
                                Title = title,
                            })
                        };

                        results.Add(notification);
                    }
                }
                catch (Exception ex)
                {
                    _logService.Error("Lấy mail lỗi " + ex.Message, ex);
                }
            }

            _logService.Information("Lấy mail: " + results.Stringify());

            return results;
        }

        private string GetUserSendAvatar(string mailSent)
        {
            var result = "../../AvatarProfile/noavatar.jpg";
            if (!mailSent.Contains("@"))
            {
                return result;
            }

            var currentDomain = _user.DomainName;
            var mailSentName = mailSent.Split('@')[0];
            var mailSentDomain = mailSent.Split('@')[1];
            if (currentDomain.Equals(mailSentDomain, StringComparison.OrdinalIgnoreCase))
            {
                result = string.Format(_generalSettings.Avatar, mailSentName);
            }

            return result;
        }

        private DateTime ParseMailDate(double mailDate)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddMilliseconds(mailDate)
                .ToLocalTime();
        }
    }

    /// <summary>
    /// Thông tin mail từ bmail
    /// </summary>
    public class BmailDataInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string MailId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FolderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FolderLocation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SenderUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SenderFullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }
    }
}