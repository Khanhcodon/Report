using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.NotificationService;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class NotificationController : BaseController
    {
        #region properties

        private readonly UserBll _userService;
        private readonly ResourceBll _resourceService;
        private readonly NotificationBll _notificationService;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly ConnectionSettings _connectionSettings;
        private readonly NotificationSettings _notificationSettings;
        private readonly NotificationHelper _notificationHelper;

        private int _currentUserId;

        #endregion

        #region constructor

        public NotificationController(
            UserBll userService,
            ResourceBll resourceService,
            Helper.UserSetting helperUserSetting,
            ConnectionSettings connectionSettings,
            NotificationSettings notificationSettings,
            NotificationBll notificationSerice,
            NotificationHelper notificationHelper
            )
        {
            _userService = userService;
            _resourceService = resourceService;
            _helperUserSetting = helperUserSetting;
            _notificationService = notificationSerice;
            _connectionSettings = connectionSettings;
            _notificationSettings = notificationSettings;

            _currentUserId = _userService.CurrentUser.UserId;
            _notificationHelper = notificationHelper;
        }

        #endregion

        public JsonResult GetConfig()
        {
            string notifyInfo = _userService.CurrentUser.NotifyInfo;
            if (notifyInfo == null || notifyInfo.Length == 0)
            {
                notifyInfo = "{"
                    + "\"removeread\":false,"
                    + "\"hasshowdesktop\":true,"
                    + "\"hasplaysound\":true,"
                    + "\"hasshowdocumentnotify\":true,"
                    + "\"documentnotifytype\":1,"
                    + "\"hasshowmailnotify\":true,"
                    + "\"hasshowchatnotify\":true}";
            }
            var notifyConfig = _helperUserSetting.GetNotifyInfo(notifyInfo);
            return Json(notifyConfig, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNotSents(int typeNotification = 0, int pageSize = 30)
        {
            var results = _notificationService.GetNotSents(_currentUserId, typeNotification, pageSize);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetForMobile(int typeNotification = 0, int pageSize = 30)
        {
            var results = _notificationService.Gets(_currentUserId);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData(int pageIndex, int pageSize, int typeNotification = 0)
        {
            var results = _notificationService.GetDataToScroll(_currentUserId, typeNotification, pageIndex, pageSize);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(string addeds, string removeds, string vieweds)
        {
            var result = true;
            var readNotifications = new List<Notifications>();
            if (!string.IsNullOrEmpty(addeds))
            {
                var currentUser = _userService.CurrentUser;
                var newNotifications = Json2.ParseAs<IEnumerable<Notifications>>(addeds);
                foreach (var notify in newNotifications)
                {
					if (string.IsNullOrEmpty(notify.Title))
					{
						notify.Title = currentUser.FullName;
					}

					if (string.IsNullOrEmpty(notify.Avatar))
					{
						notify.Avatar = currentUser.Avatar;
					}

                    var userReceiveId = notify.UserId ?? currentUser.UserId;
                    if (notify.IsReaded || !notify.IsNew)
                    {
                        notify.UserId = userReceiveId;
                        notify.DateCreated = DateTime.Now;
                        readNotifications.Add(notify);
                    }
                    else
                    {
                        _notificationHelper.PushNotifyMessage(userReceiveId, notify.AppName,
                                                    notify.Title, notify.Body, notify.Avatar, notify.GroupId, notify.JsonData);
                    }
                }
            }

            if (readNotifications.Any())
            {
                _notificationService.Create(readNotifications);
            }

            if (!string.IsNullOrEmpty(removeds))
            {
                var removedNotificationIds = Json2.ParseAs<IEnumerable<int>>(removeds);
                var removedNotifications = _notificationService.Gets(removedNotificationIds, isReadOnly: false);
                _notificationService.UpdateRead(removedNotifications);
            }

            if (!string.IsNullOrEmpty(vieweds))
            {
                var viewedNotificationIds = Json2.ParseAs<IEnumerable<int>>(vieweds);
                var viewedNotifications = _notificationService.Gets(viewedNotificationIds, isReadOnly: false);
                _notificationService.UpdateViewed(viewedNotifications);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveAll(int lastId, string appName)
        {
            var userId = _currentUserId;
            if (userId == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            _notificationService.RemoveAll(lastId, appName, userId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            if (id <= 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var notify = _notificationService.Get(id);
            if (notify != null)
            {
                _notificationService.Remove(notify);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Read(int id)
        {
            _notificationService.SetRead(id);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OldAll(int lastId, string appName)
        {
            var userId = _currentUserId;
            if (userId == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            _notificationService.OldAll(lastId, appName, userId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReadAll(int lastId, string appName)
        {
            var userId = _currentUserId;
            if (userId == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            _notificationService.ReadAll(lastId, appName, userId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLastestMails()
        {
            if (string.IsNullOrWhiteSpace(_connectionSettings.BmailLink)
                        || _connectionSettings.MailType != 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            var bkavAuthenCookie = Request.Cookies["bkavAuthen"];
            var user = _userService.CurrentUser;

            if (bkavAuthenCookie != null && user != null)
            {
                var mailtoken = bkavAuthenCookie.Value;

                //Mail notify cho bmail
                if (!string.IsNullOrWhiteSpace(mailtoken))
                {
                    var userSetting = _helperUserSetting.GetNotifyInfo(user.NotifyInfo);
                    if (!userSetting.HasShowMailNotify)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }

                    var bmailNotificationHelper = new BmailNotification(user, _connectionSettings.BmailLink);
                    var results = bmailNotificationHelper.GetLastestMail(mailtoken, userSetting.MailFolderNotify);
                    if (results.Any())
                    {
                        _notificationService.Create(results);
                    }
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBase64FromImageUrl(string account)
        {
            var imageUrl = _helperUserSetting.GetUserAvatar(account.Split('@')[0]);
            return Json(HasBase64.ConvertImageURLToBase64(imageUrl), JsonRequestBehavior.AllowGet);
        }

        private int CurrentUserId()
        {
            return _userService.CurrentUser.UserId;
        }
    }
}