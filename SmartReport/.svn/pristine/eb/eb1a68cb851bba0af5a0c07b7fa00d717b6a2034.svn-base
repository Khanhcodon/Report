using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using System.Text.RegularExpressions;
using Bkav.eGovCloud.Core.Caching;
using System.Data.SqlClient;
using System.Text;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : NotificationBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 18102016
    /// Author      : TrinhNVd
    /// Description : BLL tương ứng với bảng Notification trong CSDL
    /// </summary>
    public class NotificationBll : ServiceBase
    {
        private readonly IRepository<Notifications> _notificationsRepository;
        private readonly ResourceBll _resourceService;
        private readonly MemoryCacheManager _cache;
        private const string NO_AVATAR = "../AvatarProfile/noavatar.jpg";

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="resourceService"></param>
        /// <param name="cache"></param>
        public NotificationBll(IDbCustomerContext context, ResourceBll resourceService, MemoryCacheManager cache)
            : base(context)
        {
            _notificationsRepository = Context.GetRepository<Notifications>();
            _resourceService = resourceService;
            _cache = cache;
        }

        /// <summary>
        /// Tạo notify mới
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Notifications entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (!string.IsNullOrEmpty(entity.GroupId))
            {
                var similars = _notificationsRepository.Gets(false, n => n.GroupId == entity.GroupId);
                if (similars.Any())
                {
                    foreach (var notification in similars)
                    {
                        _notificationsRepository.Delete(notification);
                    }
                }
            }

            _notificationsRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo danh sách notify mới
        /// </summary>
        /// <param name="entities"></param>
        public void Create(IEnumerable<Notifications> entities)
        {
            foreach (var entity in entities)
            {
                Create(entity);
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void CreateIfNotExist(IEnumerable<Notifications> entities)
        {
            foreach (var notify in entities)
            {
                if (!_notificationsRepository.Exist(n => n.UserId == notify.UserId
                        && n.Title == notify.Title
                        && n.JsonData == notify.JsonData
                        && n.DateCreated == notify.DateCreated))
                {
                    Create(notify);
                }
            }
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		public Notifications GetByToken(Guid token)
		{
			var tokenString = token.ToString();
			return _notificationsRepository.Get(true, n => n.Token == tokenString);
		}

		/// <summary>
		/// Trả về notify theo id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Notifications Get(int id)
        {
            if (id <= 0)
            {
				return null;
            }

            return _notificationsRepository.Get(id);
        }

        /// <summary>
        /// Trả về danh sách các notify chưa được gửi đi. chỉ lấy mặc định pageSize bản ghi còn lại lấy thêm sẽ để loadmore scroll tiếp
        /// </summary>
        /// <param name="userId">Id người nhận; mặc định lấy tất cả các notify chưa được gửi đi.</param>
        /// <param name="typeNotification">Thêm bộ lọc thông báo trên mobile, với deskop chưa có value = 0 sẽ mặc định lấy tất cả</param>
        /// <param name="isReadOnly">Kết quả chỉ đọc</param>
        /// <param name="pageSize">Số bản ghi trên trang, do code trước lấy 30 bản ghi nên defaultvalue = 30 để tránh thay đổi code</param>
        /// <returns>Danh sách các notify chưa được gửi đi</returns>
        public IEnumerable<Notifications> GetNotSents(int userId = 0, int typeNotification = 0, int pageSize = 30, bool isReadOnly = true)
        {
            String clause = "\"NotificationType\":" + typeNotification;

			//!n.IsSent && bỏ điều kiện chưa gửi
			var notifications = _notificationsRepository.Gets(isReadOnly, n => !n.IsDeleted && (typeNotification == 0 || n.JsonData.Contains(clause))
                                && (userId == 0 || n.UserId == userId)).OrderByDescending(x => x.DateCreated).Take(pageSize);

            var avatarPattern = "/avatarprofile/(.*)jpg";

            foreach (var n in notifications)
            {
                var reg = new Regex(avatarPattern, RegexOptions.IgnoreCase);
                var avatar = reg.Match(n.Avatar);
                if (avatar != null && !string.IsNullOrEmpty(avatar.Value))
                {
                    var avatarPath = "~" + avatar.Value;
                    if (!System.IO.File.Exists(CommonHelper.MapPath(avatarPath)))
                    {
                        n.Avatar = NO_AVATAR;
                    }
                }
            }

            return notifications;
        }

        /// <summary>
        /// Trả về danh sách scroll
        /// </summary>
        /// <param name="userId">Id người nhận; mặc định lấy tất cả các notify chưa được gửi đi.</param>
        /// <param name="typeNotification">Thêm bộ lọc thông báo trên mobile, với deskop chưa có value = 0 sẽ mặc định lấy tất cả</param>
        /// <param name="pageIndex">mỗi lần scroll tăng cái này lên 1</param>
        /// <param name="pageSize">load bao nhiêu bản ghi mỗi lần scroll</param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        public IEnumerable<Notifications> GetDataToScroll(int userId = 0, int typeNotification = 0, int pageIndex = 0, int pageSize = 0, bool isReadOnly = true)
        {
            String clause = "\"NotificationType\":" + typeNotification;
            var notifications = _notificationsRepository.Gets(isReadOnly, n => !n.IsSent
                                && !n.IsDeleted && (n.JsonData.Contains(clause) || typeNotification == 0)
                                && (userId == 0 || n.UserId == userId)).
                                OrderBy(x => x.DateCreated)
                                .Skip(pageIndex * pageSize)
                                .Take(pageSize);
            return notifications;
        }

        /// <summary>
        /// Trả về danh sách notify theo user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số record cần lấy theo trang.</param>
        /// <param name="hasDeleted">Trạng thái có lấy notify đã delete ko</param>
        /// <returns>Danh sách các notify theo user; kết quả chỉ để đọc</returns>
        public IEnumerable<Notifications> Gets(int userId, int page = 1, int pageSize = 30, bool hasDeleted = false)
        {
            var skip = (page - 1) * pageSize;
            var notifications = _notificationsRepository
                        .GetsReadOnly(n => n.UserId.HasValue && n.UserId.Value == userId && (hasDeleted || !n.IsDeleted)).OrderByDescending(n => n.DateCreated)
						.Skip(skip).Take(pageSize);


            var avatarPattern = "/avatarprofile/(.*)jpg";

            foreach (var n in notifications)
            {
                var reg = new Regex(avatarPattern, RegexOptions.IgnoreCase);
                var avatar = reg.Match(n.Avatar);
                if (avatar != null && !string.IsNullOrEmpty(avatar.Value))
                {
                    var avatarPath = "~" + avatar.Value;
                    if (!System.IO.File.Exists(CommonHelper.MapPath(avatarPath)))
                    {
                        n.Avatar = NO_AVATAR;
                    }
                }
            }

            return notifications;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifies"></param>
        public void Queue(List<Business.Objects.Notify> notifies)
        {
            var cache = _cache.Get<IEnumerable<Objects.Notify>>(CacheParam.NotificationQueueKey, () => {
                return new List<Objects.Notify>();
            });
            
            cache = cache.Concat(notifies);
            _cache.Remove(CacheParam.NotificationQueueKey);
            _cache.Set(CacheParam.NotificationQueueKey, cache, CacheParam.NotificationQueueTimeOut);
        }

        /// <summary>
        /// Trả về danh sách notification theo Id
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        public IEnumerable<Notifications> Gets(IEnumerable<int> ids, bool isReadOnly = true)
        {
            return _notificationsRepository.Gets(isReadOnly, n => ids.Contains(n.NotificationId));
        }

        /// <summary>
        /// Update trạng thái đã gửi
        /// </summary>
        /// <param name="notificationId">Id</param>
        public void UpdateSent(int notificationId)
        {
            var notify = _notificationsRepository.Get(notificationId);
            if (notify != null)
            {
                notify.IsSent = true;
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật trạng thái đã gửi notify xuống client
        /// </summary>
        /// <param name="notifies">Danh sách notify cần cạp nhật</param>
        public void UpdateSent(IEnumerable<Notifications> notifies)
        {
            foreach (var notify in notifies)
            {
                notify.IsSent = true;
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật trạng thái đã xem notify theo user: đã xem khi click vào quả cầu hoặc click đóng trên notify popup
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="lastDate">Thời điểm notify gần nhất đã gửi xuống client</param>
        public void UpdateViewed(int userId, DateTime lastDate)
        {
            if (userId <= 0)
            {
                return;
            }

            var notifies = _notificationsRepository.Gets(false, n => n.UserId.HasValue && n.UserId.Value == userId && n.DateCreated <= lastDate);

            foreach (var notify in notifies)
            {
                notify.IsNew = false;
                notify.IsSent = true;
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật trạng thái đã xem notify
        /// </summary>
        /// <param name="notifies"></param>
        public void UpdateViewed(IEnumerable<Notifications> notifies)
        {
            foreach (var notify in notifies)
            {
                notify.IsNew = false;
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật trạng thái đã đọc notify: đã click vào xem
        /// </summary>
        /// <param name="notifies">Danh sách notify cần update</param>
        public void UpdateRead(IEnumerable<Notifications> notifies)
        {
            foreach (var notify in notifies)
            {
                notify.IsNew = false;
                notify.IsReaded = true;
                notify.IsSent = true;
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Remote tất cả notify của người dùng 
        /// </summary>
        /// <param name="lastId"></param>
        /// <param name="appName"></param>
        /// <param name="userId"></param>
        public void RemoveAll(int lastId, string appName, int userId)
        {
            if (userId == 0)
            {
                return;
            }

            var notifies = _notificationsRepository.Gets(false, n =>
                                    (lastId == 0 || n.NotificationId <= lastId)
                                    && n.UserId.HasValue
                                    && n.UserId == userId
                                    && (appName == "" || n.AppName.Equals(appName, StringComparison.OrdinalIgnoreCase)));
            if (notifies.Any())
            {
                foreach (var n in notifies)
                {
                    n.IsDeleted = true;
                }

                Context.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notify"></param>
        public void Remove(Notifications notify)
        {
            if (notify == null)
            {
                throw new ArgumentNullException("entity");
            }

            //_notificationsRepository.Delete(notify);
            notify.IsDeleted = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// Set tất cả notify là đã xem.
        /// </summary>
        /// <param name="lastId"></param>
        /// <param name="appName"></param>
        /// <param name="userId"></param>
        public void OldAll(int lastId, string appName, int userId)
        {
            if (userId == 0)
            {
                return;
            }

            var notifies = _notificationsRepository.Gets(false, n =>
                                    (lastId == 0 || n.NotificationId <= lastId)
                                    && n.UserId.HasValue && n.UserId == userId
                                    && n.IsNew
                                    && (appName == "" || n.AppName.Equals(appName, StringComparison.OrdinalIgnoreCase)));
            if (notifies.Any())
            {
                foreach (var n in notifies)
                {
                    n.IsNew = false;
                }

                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Set tẩt cả notify là đã đọc
        /// </summary>
        /// <param name="lastId"></param>
        /// <param name="appName"></param>
        /// <param name="userId"></param>
        public void ReadAll(int lastId, string appName, int userId)
        {
            if (userId == 0)
            {
                return;
            }

            var notifies = _notificationsRepository.Gets(false, n =>
                                    (lastId == 0 || n.NotificationId <= lastId)
                                    && n.UserId.HasValue && n.UserId == userId
                                    && !n.IsReaded
                                    && (appName == "" || n.AppName.Equals(appName, StringComparison.OrdinalIgnoreCase)));
            if (notifies.Any())
            {
                foreach (var n in notifies)
                {
                    n.IsReaded = true;
                }

                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Set đã xem
        /// </summary>
        /// <param name="id"></param>
        public void SetRead(int id)
        {
            var notify = Get(id);
            if (notify != null)
            {
                notify.IsReaded = true;
                notify.IsNew = false;

                Context.SaveChanges();
            }
        }
        /// <summary>
        /// Notify cảnh báo cho người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Objects.Notify GetWarningNotify(int userId)
        {
            var title = "Cảnh báo xử lý văn bản";
            var body = new StringBuilder();

            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("userId", userId));
            var result = Context.RawProcedure("notifyWarning", param.ToArray());
            foreach (var r in result)
            {
                if (r.Count == 0)
                {
                    continue;
                }

                body.AppendLine(string.Format("{0}: {1} văn bản", r.Name, r.Count));
            }

            if (body.ToString().IsEmpty()) return null;

            var notify = new Business.Objects.Notify()
            {
                Domain = "BkaveGov",
                UserId = userId,
                IsRealTime = true,
                NotificationBody = body.ToString(),
                NotificationTitle = title,
                TotalNotify = 1,
                AppName = "documents",
                DateCreated = DateTime.Now,
                JsonData = Json2.StringifyJs(new Notification
                {
                    Title = title,
                    NotificationType = (int)NotificationType.eGov,
                    Date = DateTime.Now,
                    DocumentCopyId = 0
                })
            };

            return notify;
        }

        #region Bỏ

        /*
        /// <summary>
        /// Tạo mới đối tượng gủi Notification 
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Notification entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Notification in null.");
            }

            _notificationsRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới nhiều đối tượng gửi Notification
        /// </summary>
        /// <param name="entities"></param>
        public void Create(IEnumerable<Notification> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("Notification in null.");
            }

            foreach (var entity in entities)
            {
                try
                {
                    _notificationsRepository.Create(entity);
                }
                catch (DbEntityValidationException dbEx)
                {
                    var msg = string.Empty;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                        }
                    }
                    var fail = new Exception(msg, dbEx);
                    throw fail;
                }
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật đối tượng Notification
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Notification entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Notification in null.");
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Cạp nhật nhiều đối tượng Notification
        /// </summary>
        /// <param name="entities"></param>
        public void Update(IEnumerable<Notification> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("Notification in null.");
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa 1 đối tượng Notification
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Notification entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Notification in null.");
            }

            _notificationsRepository.Delete(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa nhiều đối tượng Notification
        /// </summary>
        /// <param name="entities"></param>
        public void Delete(IEnumerable<Notification> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("Notification in null.");
            }

            foreach (var entity in entities)
            {
                _notificationsRepository.Delete(entity);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy đối tượng Notification theo id
        /// </summary>
        /// <param name="NotificationId">Id của đối tượng Notification</param>
        /// <returns></returns>
        public Notification Get(int NotificationId)
        {
            return _notificationsRepository.Get(NotificationId);
        }

        /// <summary>
        /// Lấy đối tượng Notification theo điều kiện
        /// </summary>
        /// <param name="isReadOnly">Đối tượng này chỉ đọc hay có thể vừa đọc vừa ghi</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        public Notification Get(Expression<Func<Notification, bool>> spec = null, bool isReadOnly = false)
        {
            return _notificationsRepository.Get(isReadOnly, spec);
        }

        /// <summary>
        /// Lấy danh sách đối tượng Notification chỉ có thể đọc ghi theo điều kiện truyền vào
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<Notification> Gets(Expression<Func<Notification, bool>> spec = null)
        {
            return _notificationsRepository.Gets(false, spec);
        }

        /// <summary>
        /// Trả về notify gần nhất cho người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="lastId"></param>
        /// <param name="isHidden"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public IEnumerable<Notification> GetsByType(int userId, int type, int lastId = 0, bool? isHidden = false, int? take = 30)
        {
            return _notificationsRepository.Gets(true, x => x.UserId == userId
                && x.NotificationType == type
                && x.NotificationId > lastId
                && x.Hidden == isHidden).OrderBy(n => n.Date).Take(take.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void HideNotify(int id)
        {
            var notify = Get(id);
            notify.Hidden = true;
            Update(notify);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        public void HideNotify(int userId, int type)
        {
            var notifications = Gets(x => x.UserId == userId
                && x.NotificationType == type
                && x.Hidden == false);

            if (notifications.Any())
            {
                foreach (var notification in notifications)
                {
                    notification.Hidden = true;
                }

                Update(notifications);
            }
        }

        /// <summary>
        /// Đặt trạng thái đã xem theo loại thông báo
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        public void SetViewd(int userId, int type)
        {
            var unreads = _notificationsRepository.Gets(false, x => x.UserId == userId
                && x.NotificationType == type
                && !x.Hidden && !x.ViewdDate.HasValue);
            if (unreads.Any())
            {
                foreach (var unread in unreads)
                {
                    unread.ViewdDate = DateTime.Now;
                }
                Update(unreads);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lastId"></param>
        /// <returns></returns>
        public IEnumerable<Notification> GetUnReads(int userId, int lastId)
        {
            return Gets(x => x.UserId == userId
            && x.NotificationId > lastId
            && x.NotificationType == 1
            && x.ViewdDate == null && x.Hidden == false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationType"></param>
        /// <param name="compendium"></param>
        /// <returns></returns>
        public string GetContent(eGovNotificationTypes notificationType, string compendium)
        {
            string result = string.Empty;
            string format = string.Empty;
            switch (notificationType)
            {
                case eGovNotificationTypes.eGovXuLyChinh:
                    format = _resourceService.GetResource("Common.Notify.eGovXuLyChinh");
                    break;
                case eGovNotificationTypes.eGovDongXuLy:
                    format = _resourceService.GetResource("Common.Notify.eGovDongXuLy");
                    break;
                case eGovNotificationTypes.eGovThongBao:
                    format = _resourceService.GetResource("Common.Notify.eGovThongBao");
                    break;
                case eGovNotificationTypes.eGovXinYKien:
                    format = _resourceService.GetResource("Common.Notify.eGovXinYKien");
                    break;
                case eGovNotificationTypes.eGovKetThuc:
                    format = _resourceService.GetResource("Common.Notify.eGovKetThuc");
                    break;
                case eGovNotificationTypes.eGovXinGiaHan:
                    format = _resourceService.GetResource("Common.Notify.eGovXinGiaHan");
                    break;
                default:
                    format = _resourceService.GetResource("Common.Notify.Default");
                    break;
            }

            result = string.Format(format, compendium);
            return result;
        }
        */

        #endregion
    }
}
