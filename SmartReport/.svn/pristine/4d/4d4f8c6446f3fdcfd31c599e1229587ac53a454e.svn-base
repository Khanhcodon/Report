using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ResourceBll - public - BLL
    /// Access Modifiers:
    /// Create Date : 150414
    /// Author      : TienBV
    /// Description : BLL tương ứng với bảng UserActivityLog trong CSDL
    /// </summary>
    public class UserActivityLogBll : ServiceBase
    {
        private readonly IRepository<UserActivityLog> _logRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly UserBll _userService;
        private readonly ResourceBll _resourceService;
        private readonly NotificationBll _notificationService;
        private readonly DocumentCopyBll _docCopyService;

        /// <summary>
        /// Khởi tạo class <see cref="ActivityLogBll"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        /// <param name="userService">Bll tương ứng với bảng User trong CSDL</param>
        /// <param name="resourceService">Bll tương ứng với bảng Resources trong CSDL</param>
        /// <param name="notificationService">Bll tương ứng với bảng Notification trong CSDL</param>
        /// <param name="docCopyService"></param>
        public UserActivityLogBll(IDbCustomerContext context,
            AdminGeneralSettings generalSettings,
            UserBll userService,
            ResourceBll resourceService,
            NotificationBll notificationService,
            DocumentCopyBll docCopyService)
            : base(context)
        {
            _logRepository = Context.GetRepository<UserActivityLog>();
            _generalSettings = generalSettings;
            _userService = userService;
            _resourceService = resourceService;
            _notificationService = notificationService;
            _docCopyService = docCopyService;
        }

        /// <summary>
        /// Lấy ra một nhật ký
        /// </summary>
        /// <param name="logId">Id của nhật ký</param>
        /// <returns>Entity nhật ký</returns>
        public UserActivityLog Get(int logId)
        {
            UserActivityLog log = null;
            if (logId > 0)
            {
                log = _logRepository.Get(logId);
            }

            return log;
        }

        /// <summary>
        /// Trả về danh sách các thông báo chưa đọc
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="userId">UserId</param>
        /// <param name="docCopyId">docCopyId</param>
        /// <returns></returns>
        public UserActivityLog Get(int userId, int docCopyId)
        {
            var result = _logRepository.GetReadOnly(
                log => log.DocumentCopyId == docCopyId
                    && log.UserReceiveId == userId);
            return result;
        }

        /// <summary>
        /// Lấy 1 bản ghỉ theo điều kiện
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        public UserActivityLog Get(Expression<Func<Entities.Customer.UserActivityLog, bool>> spec, bool isReadOnly = true)
        {
            return _logRepository.Get(isReadOnly, spec);
        }

        /// <summary>
        /// Thêm mới nhật ký
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Thực thể nhật ký</returns>
        public void Create(UserActivityLog entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _logRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới nhiều notify
        /// </summary>
        /// <param name="entitys">Danh sách đối tượng notify</param>
        public void Create(IEnumerable<UserActivityLog> entitys)
        {
            if (entitys == null)
            {
                throw new ArgumentNullException("notifications is null.");
            }

            foreach (var entity in entitys)
            {
                _logRepository.Create(entity);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Update nhật ký
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Thực thể nhật ký</returns>
        public void Update(UserActivityLog entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Update nhiều notify
        /// </summary>
        /// <param name="entitys">Danh sách đối tượng notify</param>
        public void Update(IEnumerable<UserActivityLog> entitys)
        {
            if (entitys == null)
            {
                throw new ArgumentNullException("notifications is null.");
            }
            Context.SaveChanges();
        }

        ///// <summary>
        ///// Tạo mới và cập nhật notify
        ///// </summary>
        ///// <param name="createEntitys">Danh sách tạo mới</param>
        ///// <param name="updateEntitys">Danh sách cập nhật</param>
        //public List<Notification> CreateAndUpdate(IEnumerable<UserActivityLog> createEntitys, IEnumerable<UserActivityLog> updateEntitys)
        //{
        //    var notifications = new List<Notification>();
        //    if (createEntitys == null)
        //    {
        //        throw new ArgumentNullException("notifications is null.");
        //    }
        //    foreach (var entity in createEntitys)
        //    {
        //        _logRepository.Create(entity);
        //    }
        //    Context.SaveChanges();
        //    var userActivitiesLog = createEntitys.Concat(updateEntitys);
        //    if (userActivitiesLog.Any())
        //    {
        //        foreach (var userActivitieLog in userActivitiesLog)
        //        {
        //            notifications.Add(new Notification
        //            {
        //                Title = userActivitieLog.Compendium,
        //                NotificationType = (int)NotificationType.eGov,
        //                Date = DateTime.Now,
        //                DocumentCopyId = userActivitieLog.DocumentCopyId,
        //                ReceiveDate = userActivitieLog.SentDate,
        //                UserId = userActivitieLog.UserReceiveId,
        //                SenderUserName = userActivitieLog.UserNameSend,
        //                SenderFullName = userActivitieLog.FullNameSend,
        //                Content = GetContent(userActivitieLog.NotificationType, userActivitieLog.Compendium)
        //            });
        //        }
        //        _notificationService.Create(notifications);
        //    }
        //    return notifications;
        //}

        /// <summary>
        /// Xóa nhật ký
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Thực thể nhật ký</returns>
        public void Delete(UserActivityLog entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _logRepository.Delete(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa nhiều notify
        /// </summary>
        /// <param name="entitys">Danh sách đối tượng notify</param>
        public void Delete(IEnumerable<UserActivityLog> entitys)
        {
            if (entitys == null)
            {
                throw new ArgumentNullException("notifications is null.");
            }

            foreach (var entity in entitys)
            {
                _logRepository.Delete(entity);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy ra danh sách notify theo diều kiện
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        public IEnumerable<UserActivityLog> Gets(
            Expression<Func<Entities.Customer.UserActivityLog, bool>> spec = null,
            bool isReadOnly = true)
        {
            return _logRepository.Gets(isReadOnly, spec);
        }

        /// <summary>
        /// Lấy ra danh sach những notify của người dung chưa xem
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<UserActivityLog> GetUnReads(int userId)
        {
            var result = Gets(
                            log => !log.IsViewed
                                && log.UserReceiveId == userId
                                && log.UserSendId != userId)
                                .GroupBy(p => new
                                {
                                    p.DocumentCopyId,
                                    p.DocumentCopyType
                                }, p => p, (key, g) => new { value = g.OrderByDescending(c => c.SentDate).FirstOrDefault() })
                                .OrderByDescending(p => p.value.SentDate)
                                .Select(p => { return p.value; });
            return result;
        }

        /// <summary>
        /// Lấy ra danh sach những notify của người dung chưa xem
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IEnumerable<UserActivityLog> GetUnReads(string username)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                var user = _userService.GetByUserName(username);
                if (user != null)
                {
                    return
                        Gets(log =>
                            !log.IsViewed
                            && log.UserReceiveId == user.UserId
                            && log.UserSendId != user.UserId)
                            .GroupBy(p => new
                            {
                                p.DocumentCopyId,
                                p.DocumentCopyType
                            }, p => p, (key, g) =>
                                new { value = g.OrderByDescending(c => c.SentDate).FirstOrDefault() })
                            .OrderByDescending(p => p.value.SentDate)
                            .Select(p => { return p.value; });
                }
            }

            return null;
        }

        ///// <summary>
        ///// Trả về danh sách các notification cũ hơn
        ///// </summary>
        ///// <param name="userId">User id</param>
        ///// <param name="lastId">log Id gần nhất đã load</param>
        ///// <returns></returns>
        //public IEnumerable<UserActivityLog> GetOlders(int userId, int lastId)
        //{
        //    var result = Gets(
        //        log => log.UserReceiveId == userId
        //            && log.UserSendId != userId
        //            && (lastId == 0
        //            || log.UserActivityLogId < lastId))
        //            .OrderByDescending(c => c.UserActivityLogId)
        //            .Take(_generalSettings.DisplayNumberNotifyFirst);

        //    return result;
        //}

        ///// <summary>
        ///// Lấy danh sách notify cũ hơn
        ///// </summary>
        ///// <param name="notifyId">Id notify so sánh</param>
        ///// <returns></returns>
        //public IEnumerable<UserActivityLog> GetOldNotify(int notifyId)
        //{
        //    var user = _userService.CurrentUser;
        //    var results = Gets(
        //        p => p.UserActivityLogId < notifyId
        //            && p.UserReceiveId == user.UserId
        //            && p.IsViewed)
        //            .GroupBy(p => new
        //            {
        //                p.DocumentCopyId,
        //                p.NotificationType
        //            }, p => p, (key, g) => new { value = g.OrderByDescending(c => c.SentDate).FirstOrDefault() })
        //            .OrderByDescending(p => p.value.SentDate)
        //            .Select(p => { return p.value; })
        //            .Take(_generalSettings.DisplayNumberNotifyFirst);
        //    return results;
        //}

        ///// <summary>
        ///// Lấy danh sách notify của người dùng hiện tại
        ///// Nếu danh sách nhỏ hơn hiện tại thì lấy ra danh sách những notify đã đọc
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<UserActivityLog> GetNotifyByCurrentUser()
        //{
        //    var user = _userService.CurrentUser;
        //    var results = new List<UserActivityLog>();
        //    results.AddRange(GetUnReads(user.UserId));
        //    if (results.Count() < _generalSettings.DisplayNumberNotifyFirst)
        //    {
        //        var count = _generalSettings.DisplayNumberNotifyFirst - results.Count();
        //        var readeds = Gets(
        //            p => (p.UserReceiveId == user.UserId
        //                && p.UserSendId != user.UserId)
        //                && p.IsViewed).GroupBy(p => new
        //                {
        //                    p.DocumentCopyId,
        //                    p.NotificationType
        //                }, p => p, (key, g) => new { value = g.OrderByDescending(c => c.SentDate).FirstOrDefault() })
        //                .OrderByDescending(p => p.value.SentDate)
        //                .Select(p => { return p.value; })
        //                .Take(count);
        //        results.AddRange(readeds);
        //    }
        //    return results;
        //}

        /// <summary>
        /// Lấy danh sách notify của người dùng hiện tại
        /// Nếu danh sách nhỏ hơn hiện tại thì lấy ra danh sách những notify đã đọc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserActivityLog> GetAllNotifyByCurrentUser()
        {
            var user = _userService.CurrentUser;
            return GetAllNotifyByUser(user.UserId);
        }

        /// <summary>
        /// Trả về 50 notify gần nhất của người dùng
        /// Nếu danh sách nhỏ hơn hiện tại thì lấy ra danh sách những notify đã đọc
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<UserActivityLog> GetAllNotifyByUser(int userId)
        {

            var results = _logRepository.GetsReadOnly(p => p.UserReceiveId == userId && p.UserSendId != userId)
                    .OrderByDescending(l => l.SentDate)
                    .Take(50)
                    .GroupBy(p => new
                    {
                        p.DocumentCopyId,
                        p.DocumentCopyType
                    }, p => p, (key, g) => new { value = g.OrderByDescending(c => c.SentDate).FirstOrDefault() })
                    .OrderByDescending(p => p.value.SentDate)
                    .Select(p => { return p.value; });

            return results;
        }

        ///// <summary>
        ///// Tạo mới notify 
        ///// </summary>
        ///// <param name="documentCopys">Danh sách văn bản chuyển vào</param>
        ///// <param name="userId">Người nhận notify</param>
        ///// <param name="outNotifys">Danh sách notify trả ra</param>
        //private void CreateDocumentToNotify(IEnumerable<DocumentCopy> documentCopys, int userId, out List<UserActivityLog> outNotifys)
        //{
        //    if (documentCopys == null || !documentCopys.Any())
        //    {
        //        outNotifys = new List<UserActivityLog>();
        //        return;
        //    }

        //    var notifys = new List<UserActivityLog>();
        //    var allCacheUser = _userService.GetCacheAllUsers(true);
        //    foreach (var documentCopy in documentCopys)
        //    {
        //        if (!documentCopy.LastUserIdComment.HasValue)
        //        {
        //            continue;
        //        }

        //        int notificationType = 0;
        //        switch (documentCopy.DocumentCopyType)
        //        {
        //            case (int)DocumentCopyTypes.XuLyChinh:
        //                notificationType = (int)eGovNotificationTypes.eGovXuLyChinh;
        //                break;

        //            case (int)DocumentCopyTypes.DongXuLy:
        //                notificationType = (int)eGovNotificationTypes.eGovDongXuLy;
        //                break;

        //            case (int)DocumentCopyTypes.XinYKien:
        //                notificationType = (int)eGovNotificationTypes.eGovXinYKien;
        //                break;

        //            case (int)DocumentCopyTypes.ThongBao:
        //                notificationType = (int)eGovNotificationTypes.eGovThongBao;
        //                break;
        //        }

        //        var user = allCacheUser.FirstOrDefault(p => p.UserId == documentCopy.LastUserIdComment.Value);
        //        if (user == null)
        //        {
        //            continue;
        //        }

        //        notifys.Add(new UserActivityLog
        //        {
        //            Compendium = documentCopy.Document.Compendium,
        //            DocumentCopyType = documentCopy.DocumentCopyType,
        //            NotificationType = notificationType,
        //            IsViewed = false,
        //            UserSendId = user.UserId,
        //            UserNameSend = user.Username,
        //            FullNameSend = user.FullName,
        //            SentDate = documentCopy.DateReceived,
        //            UserReceiveId = userId,
        //            DocumentCopyId = documentCopy.DocumentCopyId,
        //            HasDisplayNumberInBell = true
        //        });
        //    }

        //    Create(notifys);

        //    outNotifys = notifys;
        //}

        /// <summary>
        /// Set trang thái đã đọc
        /// </summary>
        /// <param name="documentCopyId"></param>
        /// <param name="userId"></param>
        public void SetViewed(int documentCopyId, int userId)
        {
            var notifys = Gets(p => p.UserReceiveId == userId && p.DocumentCopyId == documentCopyId && !p.IsViewed, false);
            if (notifys != null)
            {
                foreach (var item in notifys)
                {
                    item.IsViewed = true;
                    item.HasDisplayNumberInBell = false;
                }
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Set trang thái đã đọc
        /// </summary>
        /// <param name="documentCopyIds"></param>
        /// <param name="userId"></param>
        public void SetViewed(List<int> documentCopyIds, int userId)
        {
            if (documentCopyIds == null && !documentCopyIds.Any())
            {
                throw new ArgumentNullException("documentCopyIds is null.");
            }

            var notifys = Gets(p => p.UserReceiveId == userId && documentCopyIds.Contains(p.DocumentCopyId) && !p.IsViewed, false);
            if (notifys != null)
            {
                foreach (var item in notifys)
                {
                    item.IsViewed = true;
                    item.HasDisplayNumberInBell = false;
                }
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Set trang thái đã hiển thị Notify
        /// </summary>
        /// <param name="documentCopyIds"></param>
        /// <param name="userId"></param>
        public void SetNotified(List<int> documentCopyIds, int userId)
        {
            if (documentCopyIds == null && !documentCopyIds.Any())
            {
                throw new ArgumentNullException("documentCopyIds is null.");
            }

            var notifys = Gets(p => p.UserReceiveId == userId && documentCopyIds.Contains(p.DocumentCopyId) && !p.IsViewed, false);
            if (notifys != null)
            {
                foreach (var item in notifys)
                {
                    item.IsNotified = true;
                }
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Set trang thai đã đọc notify
        /// </summary>
        /// <param name="notifyId"></param>
        public void SetViewed(int notifyId)
        {
            var notify = Get(notifyId);
            if (notify != null)
            {
                notify.IsViewed = true;
                notify.HasDisplayNumberInBell = false;
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationType"></param>
        /// <param name="compendium"></param>
        /// <returns></returns>
        public string GetContent(int notificationType, string compendium)
        {
            string result = string.Empty;
            string format = string.Empty;
            switch (notificationType)
            {
                case (int)eGovNotificationTypes.eGovXuLyChinh:
                    format = _resourceService.GetResource("Common.Notify.eGovXuLyChinh");
                    break;
                case (int)eGovNotificationTypes.eGovDongXuLy:
                    format = _resourceService.GetResource("Common.Notify.eGovDongXuLy");
                    break;
                case (int)eGovNotificationTypes.eGovThongBao:
                    format = _resourceService.GetResource("Common.Notify.eGovThongBao");
                    break;
                case (int)eGovNotificationTypes.eGovXinYKien:
                    format = _resourceService.GetResource("Common.Notify.eGovXinYKien");
                    break;
                case (int)eGovNotificationTypes.eGovKetThuc:
                    format = _resourceService.GetResource("Common.Notify.eGovKetThuc");
                    break;
                case (int)eGovNotificationTypes.eGovXinGiaHan:
                    format = _resourceService.GetResource("Common.Notify.eGovXinGiaHan");
                    break;
                default:
                    format = _resourceService.GetResource("Common.Notify.Default");
                    break;
            }

            result = string.Format(format, compendium);
            return result;
        }
    }
}