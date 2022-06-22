using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using System.Threading.Tasks;
using System.Web;

namespace Bkav.eGovCloud.Helper
{
    public class NotificationHelper
    {
        private readonly UserBll _userService;
        private readonly MobileDeviceBll _mobileDeviceService;
        private readonly UserConnectionBll _userConnectionService;
        private readonly NotificationSettings _notificationSettings;
        private readonly UserSetting _helperUserSetting;
        private readonly NotificationBll _notificationService;

        public NotificationHelper(UserBll userService, MobileDeviceBll mobileDeviceService,
                    UserConnectionBll userConnectionService, 
                    NotificationSettings notificationSettings, UserSetting helperUserSetting, 
                    NotificationBll notificationService)
        {
            _userService = userService;
            _mobileDeviceService = mobileDeviceService;
            _userConnectionService = userConnectionService;
            _notificationSettings = notificationSettings;
            _helperUserSetting = helperUserSetting;
            _notificationService = notificationService;
        }

        public void PushNotifyVote(Vote vote)
        {
            var currentUser = _userService.CurrentUser;
            var usersVote = vote.ListUserVote();
            usersVote.Remove(currentUser.UserId);

            var notifies = new List<Business.Objects.Notify>();

            foreach (var userId in usersVote)
            {
                var connections = _userConnectionService.Gets(userId).Select(c => c.UserConnectionId);
                var notify = new Business.Objects.Notify()
                {
                    UserId = userId,
                    ConnectionIds = connections,
                    DeviceIds = new List<string>(),
                    IsRealTime = true,
                    NotificationBody = Truncate(vote.Title, 100),
                    NotificationTitle = currentUser.FullName + ": đã tạo 1 cuộc trưng cầu",
                    SenderAvatar = _helperUserSetting.GetUserAvatar(currentUser.Username),
                    TotalNotify = 1,
                    AccessLink = BuildNotifyLink(0),
                    AppName = "documents",
                    DateCreated = DateTime.Now,
                    JsonData = Json2.StringifyJs(new Notification
                    {
                        Title = currentUser.FullName,
                        NotificationType = (int)NotificationType.Vote,
                        Date = DateTime.Now,
                        DocumentCopyId = vote.VoteId,
                        ReceiveDate = DateTime.Now,
                        UserId = userId,
                        SenderUserName = currentUser.Username,
                        SenderFullName = currentUser.FullName,
                        Content = "abc"
                    })
                };

                notifies.Add(notify);
            }
            _notificationService.Queue(notifies);
        }

        /// <summary>
        /// Đẩy notify message đến người nhận văn bản, hồ sơ: bao gồm người nhận xử lý chính, đồng xử lý, và thông báo
        /// </summary>
        /// <param name="userReceiveds">Danh sách người nhận notify</param>
        /// <param name="documentCopy">Document Copy</param>
        /// <param name="body">Trích yếu</param>
        /// <param name="dateCreated">Ngày tạo</param>
        /// <param name="isCreatingDocument">Notify khi tạo document</param>
		/// <param name="isAdmin">Notify khi là admin</param>
        public void PushNotifyMessage(IEnumerable<int> userReceiveds, DocumentCopy documentCopy, string body,
                                        DateTime dateCreated, bool isCreatingDocument = false, bool isAdmin = false)
        {
            //Gửi cho những người từng tham gia vào xử lý văn bản
            //Lấy ra danh sách những người từng tham gia xử lý trước
            if (!isCreatingDocument)
            {
                var userThamGiaIds = documentCopy.UserThamGias();
                userReceiveds = userReceiveds.Concat(CheckNotifySettingsForUserThamGia(userThamGiaIds));
            }

            var currentUser = _userService.CurrentUser;

            //Loại bỏ người dùng hiện tại
            userReceiveds = userReceiveds.Where(p => isAdmin || p != currentUser.UserId).Distinct().ToList();
            userReceiveds = CheckNotifySettingsByUser(userReceiveds);

            if (!userReceiveds.Any())
            {
                return;
            }
            
            //Lây danh sách connections của người nhận văn bản
            var userConnections = _userConnectionService.Gets(userReceiveds).Select(
                p => new
                {
                    connectionId = p.UserConnectionId,
                    userId = p.UserId
                }
            );

            var notifies = new List<Business.Objects.Notify>();
            var domainName = HttpContext.Current == null ? "BkaveGov" : HttpContext.Current.Request.GetDomainName();
#if !QuanTriTapTrungEdition
            domainName = "BkaveGov";
#endif

            foreach (var userId in userReceiveds)
            {
                var deviceTokens = _mobileDeviceService.GetsFromCache(userId).Select(d => d.Token);
                var connections = userConnections.Where(c => c.userId == userId).Select(c => c.connectionId);

                var notify = new Business.Objects.Notify()
                {
                    Domain = domainName,
                    ConnectionIds = connections,
                    DeviceIds = deviceTokens,
                    UserId = userId,
                    IsRealTime = true,
                    NotificationBody = body,
                    NotificationTitle = isAdmin ? "Quản trị" : currentUser.FullName,
                    SenderAvatar = isAdmin ? null : _helperUserSetting.GetUserAvatar(currentUser.Username),
                    TotalNotify = 1,
                    AccessLink = BuildNotifyLink(documentCopy.DocumentCopyId),
					ActionUrl = BuildNotifyLink(documentCopy.DocumentCopyId),
					AppName = "documents",
                    DateCreated = documentCopy.DateReceived,
                    JsonData = Json2.StringifyJs(new Notification
                    {
                        Title = isAdmin ? "Quản trị" : currentUser.FullName,
                        NotificationType = (int)NotificationType.eGov,
                        Date = DateTime.Now,
                        DocumentCopyId = documentCopy.DocumentCopyId,
                        ReceiveDate = documentCopy.DateReceived,
                        UserId = userId,
                        SenderUserName = isAdmin ? "admin" : currentUser.Username,
                        SenderFullName = isAdmin ? "Quản trị" : currentUser.FullName,
                        Content = body
                    })
                };

                notifies.Add(notify);
            }

            _notificationService.Queue(notifies);            
        }

        private string BuildNotifyLink(int documentCopyId)
        {
			return _notificationSettings.NotifyUrl;

			// var result = "";

			// result = string.Format("{0}/{1}", _notificationSettings.NotifyUrl, documentCopyId);
			// return result;
        }

        public void PushNotifyMessage(int userReceivedId, string appName, string title, string body, string avatar, string groupId, string jsonData)
        {
            var notifies = new List<Business.Objects.Notify>();

            var deviceTokens = _mobileDeviceService.Gets(d => d.IsActive && d.UserId == userReceivedId).Select(d => d.Token);

            var connections = _userConnectionService.Gets(userReceivedId).Select(c => c.UserConnectionId);

			var domainName = HttpContext.Current == null ? "BkaveGov" : HttpContext.Current.Request.GetDomainName();
#if !QuanTriTapTrungEdition
            domainName = "BkaveGov";
#endif

			var notify = new Business.Objects.Notify()
            {
                ConnectionIds = connections,
                DeviceIds = deviceTokens,
                IsRealTime = true,
                NotificationBody = body,
                NotificationTitle = title,
                SenderAvatar = avatar,
				Domain = domainName,
				TotalNotify = 1,
                AccessLink = BuildNotifyLink(1),
                AppName = appName,
                UserId = userReceivedId,
                GroupId = groupId,
                DateCreated = DateTime.Now,
                JsonData = jsonData
            };

            notifies.Add(notify);

            _notificationService.Queue(notifies);
        }

        /// <summary>
        /// Kiểm tra theo thiết lập notify của người hiện tại để gửi thông báo
        /// </summary>
        /// <param name="userReceiveds"></param>
        /// <returns></returns>
        private IEnumerable<int> CheckNotifySettingsByUser(IEnumerable<int> userReceiveds)
        {
            var users = _userService.GetAllCached(true)
                            .Where(u => userReceiveds.Contains(u.UserId) && u.NotifyInfoModel.HasShowDocumentNotify
                                && (u.NotifyInfoModel.DocumentNotifyType != (int)DocumentNotifyType.Hide));
            return users.Select(u => u.UserId);
        }

        /// <summary>
        /// Check kiểm tra người dùng nhận thông báo văn bản khi đã tham gia xử lý.
        /// </summary>
        /// <param name="userReceiveds"></param>
        /// <returns></returns>
        public IEnumerable<int> CheckNotifySettingsForUserThamGia(IEnumerable<int> userReceiveds)
        {
            var users = _userService.GetAllCached(true)
                            .Where(u => userReceiveds.Contains(u.UserId) && u.NotifyInfoModel.HasShowDocumentNotify
                                    && u.NotifyInfoModel.DocumentNotifyType == (int)DocumentNotifyType.All);
            return users.Select(u => u.UserId);
        }

        private string Truncate(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }
    }
}
