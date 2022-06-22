using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Mobile;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Core.Utils;
using System;
using System.Configuration;

namespace Bkav.eGovCloud.Controllers
{
    //[RequireHttps]
    public class MobileController : BaseController
    {
        #region properties

        private readonly UserBll _userService;
        private readonly ResourceBll _resourceService;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly TreeGroupBll _treeGroupSerice;
        private readonly UserActivityLogBll _userActivityLogService;
        private readonly OnlineRegistrationSettings _onlineRegistrationSettings;
        private readonly UserConnectionBll _userConnectionService;
        private readonly ConnectionSettings _connectionSettings;
        private readonly NotificationBll _notificationSerice;

        private readonly DocumentMobileBll _documentService;

        private readonly IHubContext _hubContext;

        #endregion properties

        #region constructor

        public MobileController(
            UserBll userService,
            ResourceBll resourceService,
            Helper.UserSetting helperUserSetting,
            OnlineRegistrationSettings onlineRegistrationSettings,
            UserActivityLogBll userActivityLogService,
            TreeGroupBll treeGroupSerice,
            ConnectionSettings connectionSettings,
            NotificationBll notificationSerice,
            UserConnectionBll userConnectionService,
            DocumentMobileBll documentService
            )
        {
            _userService = userService;
            _resourceService = resourceService;
            _helperUserSetting = helperUserSetting;
            _treeGroupSerice = treeGroupSerice;
            _userActivityLogService = userActivityLogService;
            _onlineRegistrationSettings = onlineRegistrationSettings;
            _userConnectionService = userConnectionService;
            _connectionSettings = connectionSettings;
            _notificationSerice = notificationSerice;

            _documentService = documentService;

            _hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHubs>();
        }

        #endregion constructor

        public ActionResult Main(bool? isShowNotify = false)
        {
            ViewBag.IsShowNotify = isShowNotify;
            return View();
        }

        public ActionResult Index()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var urlOnlyOffice = appSettings["urlOnlyOffice"] ?? "";
            ViewBag.UrlOnlyOffice = urlOnlyOffice;
            ViewBag.Notification = "{}";
            
            return View();
        }

        public ActionResult NotifyIndex(string id = "")
        {
			Guid token;
			Notifications notification = null;
			if (Guid.TryParse(id, out token))
			{
				notification = _notificationSerice.GetByToken(token);
			}
            ViewBag.Notification = notification == null? "{}" : Json2.Stringify(notification);
            return View("Index");
        }

        private void InitData()
        {
            var user = _userService.CurrentUser;

            var notifyInfo = _helperUserSetting.GetNotifyInfo(user.NotifyInfo);

            ViewBag.Name = user.FirstName;
            ViewBag.FullName = user.FullName;
            ViewBag.Username = user.Username;

            ViewBag.MailFolderNotify = notifyInfo.MailFolderNotify;
            ViewBag.AvatarPath = _helperUserSetting.GetAvaterPath();
            ViewBag.Avatar = _helperUserSetting.GetUserAvatar(user.Username); // Đang để tạm, cần đưa vào thiết lập
            ViewBag.IsUseBmail = !string.IsNullOrWhiteSpace(_connectionSettings.BmailLink) && _connectionSettings.MailType == 1;
            ViewBag.HasOnlineRegistration = _onlineRegistrationSettings.ToModel().HasPermisson(user.UserId);
            ViewBag.AllTreeGroups = _treeGroupSerice.GetCacheAllTreeGroups(true).ToListModel();
        }

        public ActionResult LoginMobile()
        {
            return View();
        }

        #region DocumentTree

        public JsonResult GetCountDocuments()
        {
            var userId = User.GetUserId();
            var result = _documentService.GetCountDocuments(userId);

            return Json(new
            {
                DenChoXL = result.ElementAt(0),
				DenChoXL_seen = result.ElementAt(1),
				DenChoXL_new = result.ElementAt(2),
				DiChoXL = result.ElementAt(3),
				DiChoXL_Seen = result.ElementAt(4),
				DiChoXL_New = result.ElementAt(5),
				TheoDoi = result.ElementAt(6),
                UyQuyen = result.ElementAt(7),
				Thongbao = result.ElementAt(8)
			}, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Document List

        /// <summary>
        /// Trả về danh sách văn bản, hs chờ xử lý theo loại
        /// </summary>
        /// <param name="c">Nghiệp vụ: văn bản đến, đi, hsmc</param>
        /// <returns></returns>
        public JsonResult GetProcessings(int c = 1, int? v = null)
        {
            var userId = User.GetUserId();
            var model = _documentService.GetDocumentProcessing((Entities.CategoryBusinessTypes)c, userId, v);
            return Json(new { result = model }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Trả về danh sách văn bản đang dự thảo
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDrafts()
        {
            var userId = User.GetUserId();
            var model = _documentService.GetDrafts(userId);
            return Json(new { result = model }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Trả về danh sách văn bản đang theo dõi
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSents()
        {
            var userId = User.GetUserId();
            var model = _documentService.GetSents(userId);
            return Json(new { result = model }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAnnoucements()
        {
            var userId = User.GetUserId();
            var model = _documentService.GetAnnoucements(userId);
            return Json(new { result = model }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFinished()
        {
            var userId = User.GetUserId();
            var model = _documentService.GetFinished(userId);
            return Json(new { result = model }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAuthorized()
        {
            var userId = User.GetUserId();
            var model = _documentService.GetAuthorized(userId);
            return Json(new { result = model }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Notifications

        public int GetNotificationsCount()
        {
            return _userActivityLogService.GetAllNotifyByCurrentUser().Where(x => !x.IsNotified.HasValue || x.IsNotified.Value == false).ToList().Count;
        }

        public JsonResult GetNotifications()
        {
            var model = _userActivityLogService.GetAllNotifyByCurrentUser().ToListModel();
            var notifies = model.Select(p => new
            {
                title = p.Compendium,
                content = p.Content,
                date = p.SentDateFomat,
                id = p.UserActivityLogId,
                docCopyId = p.DocumentCopyId,
                avatar = p.UserSendAvatar,
                userName = p.UserNameSend,
                fullName = p.FullNameSend,
                type = 0,
                isViewed = p.IsViewed,
                isNew = p.IsNew
            });

            return Json(notifies, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNotify(int notifyId)
        {
            return Json(_userActivityLogService.Get(notifyId).ToModel(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetNotified(List<int> docCopyIds)
        {
            _userActivityLogService.SetNotified(docCopyIds, _userService.CurrentUser.UserId);

            return Json(true);
        }

        [HttpPost]
        public JsonResult RemoveNotify(int docCopyId)
        {
            if (docCopyId > 0)
            {
                var notifies = _userActivityLogService.Gets(p => p.DocumentCopyId == docCopyId && p.UserReceiveId == _userService.CurrentUser.UserId, false);
                if (notifies != null && notifies.Any())
                {
                    _userActivityLogService.Delete(notifies);
                }
            }

            return Json(true);
        }

        [HttpPost]
        public JsonResult RemoveAllNotify(List<int> docCopyIds)
        {
            foreach (var docCopyId in docCopyIds)
            {
                if (docCopyId > 0)
                {
                    var notifies = _userActivityLogService.Gets(x => x.DocumentCopyId == docCopyId && x.UserReceiveId == _userService.CurrentUser.UserId);
                    if (notifies != null && notifies.Any())
                    {
                        _userActivityLogService.Delete(notifies);
                    }
                }
            }

            UpdateViewNotify();

            return Json(true);
        }

        private void UpdateViewNotify()
        {
            var connections = _userConnectionService.Gets(_userService.CurrentUser.UserId);
            if (connections != null && connections.Any())
            {
                foreach (var connection in connections)
                {
                    _hubContext.Clients.Client(connection.UserConnectionId).updateViewNotify();
                }
            }
        }

        #endregion
    }
}