using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Api.Dto;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.NotificationService;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Api.Controller
{
    //[OAuthAuthorizeAttribute(Scope.Doctype)]
    public class NotificationController : EgovApiBaseController
    {
        private readonly ResourceBll _resourceService;
        private readonly NotificationBll _notificationService;
        private readonly UserBll _userService;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly ConnectionSettings _connectionSettings;

        public NotificationController()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            _notificationService = DependencyResolver.Current.GetService<NotificationBll>();
            _userService = DependencyResolver.Current.GetService<UserBll>();
            _helperUserSetting = DependencyResolver.Current.GetService<Helper.UserSetting>();
            _generalSettings = DependencyResolver.Current.GetService<AdminGeneralSettings>();
            _connectionSettings = DependencyResolver.Current.GetService<ConnectionSettings>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public object Get(string account = "")
        {
            return account + "_123";
        }

        [System.Web.Http.HttpGet]
        public bool Test(string token, string title = "", string message = "")
        {
            var success = false;
            if (string.IsNullOrWhiteSpace(title))
            {
                title = "Test message";
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "This is only test message";
            }

            if (!string.IsNullOrWhiteSpace(token))
            {
                try
                {
                    var uri = Request.RequestUri;
                    var port = "";
                    if (uri.Port != 443 && uri.Port != 80)
                    {
                        port = ":" + uri.Port;
                    }
                    var url = string.Format("{0}://{1}{2}/n", uri.Scheme, uri.Host, port);
                    //var fcmClient = new FCMClient();
                    //var notify = new FCMNotification
                    //{
                    //    AccessLink = url,
                    //    NotificationTitle = title,
                    //    NotificationBody = message,
                    //    SenderAvatar = "http://r.ddmcdn.com/s_f/o_1/cx_633/cy_0/cw_1725/ch_1725/w_720/APL/uploads/2014/11/too-cute-doggone-it-video-playlist.jpg",
                    //    TotalNotify = 9
                    //};

                    //success = fcmClient.SendNotifyAndroid(notify, token);
                }
                catch (Exception)
                {
                }
            }
            return success;
        }



        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //[System.Web.Http.HttpGet]
        //public object GetDetailDocumentNotifications(string account = "")
        //{
        //    IEnumerable<UserActivityLog> models;
        //    if (!string.IsNullOrWhiteSpace(account))
        //    {
        //        models = _userActivityLogService.GetUnReads(account);
        //    }
        //    else
        //    {
        //        models = _userActivityLogService.GetAllNotifyByCurrentUser();
        //    }
        //    models = models.Where(x => x.IsNotified == null || x.IsNotified == false);
        //    var notifies = models.ToListModel().Select(p => new
        //    {
        //        title = p.Compendium,
        //        content = p.Content,
        //        date = p.SentDateFomat,
        //        id = p.UserActivityLogId,
        //        docCopyId = p.DocumentCopyId,
        //        avatar = p.UserSendAvatar,
        //        userName = p.UserNameSend,
        //        fullName = p.FullNameSend,
        //        isviewed = p.IsViewed
        //    });

        //    return notifies;
        //}

        //[System.Web.Http.HttpGet]
        //public object GetNotifications(string account = "", int? lastId = 0, string mailtoken = "")
        //{
        //    var lastNotifyId = lastId ?? 0;
        //    User user;
        //    var documentNotifies = new List<Notification>();

        //    if (!string.IsNullOrWhiteSpace(account))
        //    {
        //        user = _userService.GetByUserName(account);
        //    }
        //    else
        //    {
        //        user = _userService.CurrentUser;
        //    }
        //    var mailNotifies = new List<Notification>();
        //    //Mail notify cho bmail
        //    if (!string.IsNullOrWhiteSpace(mailtoken)
        //        && !string.IsNullOrWhiteSpace(_connectionSettings.BmailLink)
        //        && _connectionSettings.MailType == 1)
        //    {
        //        var notifyInfo = _helperUserSetting.GetNotifyInfo(user);
        //        var notifyType = 1;
        //        if (notifyInfo != null)
        //        {
        //            notifyType = notifyInfo.BmailNotifyType;
        //        }
        //        var bmailNotificationHelper = new BmailNotification(user, _connectionSettings.BmailLink);
        //        mailNotifies = bmailNotificationHelper.GetLastestMail(mailtoken, notifyType, notifyInfo.MailFolderNotify);
        //    }

        //    documentNotifies = _notificationService.GetUnReads(user.UserId, lastNotifyId).ToList();
        //    var notifyList = documentNotifies.Concat(mailNotifies);
        //    var lastData = notifyList.OrderBy(x => x.ReceiveDate).LastOrDefault();
        //    var type = 1;
        //    dynamic lastNotify = null;
        //    if (lastData != null)
        //    {
        //        var lastModel = lastData.ToModel();
        //        if (string.IsNullOrWhiteSpace(lastModel.SenderAvatar))
        //        {
        //            lastModel.SenderAvatar = string.Format(_generalSettings.Avatar, lastModel.SenderUserName);
        //        }
        //        lastNotify = new
        //        {
        //            userAvatar = lastModel.SenderAvatar,
        //            title = lastModel.Title,
        //            content = lastModel.Content,
        //            notifyId = lastModel.NotificationId
        //        };
        //        type = lastData.NotificationType;
        //    }
        //    var mailNotifyNumber = mailNotifies.Count();
        //    var documentCount = documentNotifies.ToList().Count;
        //    var message = MakeMessageTotal(documentCount, mailNotifyNumber);

        //    dynamic result = new
        //    {
        //        lastNotify = lastNotify,
        //        message = message,
        //        count = documentCount + mailNotifyNumber,
        //        type = type
        //    };
        //    return result;
        //}

        private Notification GetLastNotification(Notification lastDocument, Notification lastMail)
        {
            return lastDocument.ReceiveDate > lastMail.ReceiveDate ? lastDocument : lastMail;
        }

        private DateTime ParseMailDate(double mailDate)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddMilliseconds(mailDate)
                .ToLocalTime();
        }

        private string MakeMessageTotal(int documentCount, int mailCount)
        {
            var message = "";
            var hasDocument = documentCount > 0;
            var hasMail = mailCount > 0;
            if (hasDocument || hasMail)
            {
                message = "Bạn có ";
                if (hasDocument)
                {
                    message += documentCount + " văn bản thông báo";
                    if (hasMail)
                    {
                        message += " và ";
                    }
                }
                if (hasMail)
                {
                    message += mailCount + " thư mới";
                }
            }

            return message;
        }

        /// <summary>
        /// Tạo mới đối tượng Notification
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="count">Số lượng</param>
        /// <returns></returns>
        private NotificationDto CreateNotification(eGovNotificationTypes type, int count)
        {
            return new NotificationDto
            {
                Type = type,
                Count = count,
                Message = string.Format(GetMessage(type), count)
            };
        }

        /// <summary>
        /// Trả về format của notification Message từ bảng Resource
        /// </summary>
        /// <param name="type">Notification Type</param>
        /// <returns></returns>
        private string GetMessage(eGovNotificationTypes type)
        {
            var value = "";
            switch (type)
            {
                case eGovNotificationTypes.eGovXuLyChinh:
                    value = _resourceService.GetResource("Common.Notification.ProcessDocument");
                    break;
                case eGovNotificationTypes.eGovThongBao:
                    value = _resourceService.GetResource("Common.Notification.Alert");
                    break;
                case eGovNotificationTypes.eGovUyQuyenXuLy:
                    value = _resourceService.GetResource("Common.Notification.Authorize");
                    break;
                default:
                    break;
            }
            return value;
        }

    }
}