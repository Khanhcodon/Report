using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Models
{
    public class UserActivityLogModel
    {
        private readonly ResourceBll _resourceService;

        public UserActivityLogModel()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id nhật ký hành động
        /// </summary>
        public int UserActivityLogId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người gửi
        /// </summary>
        public int UserSendId { get; set; }

        /// <summary>
        /// UserName của người gửi
        /// </summary>
        public string UserNameSend { get; set; }

        /// <summary>
        /// Họ tên người gửi
        /// </summary>
        public string FullNameSend { get; set; }

        /// <summary>
        /// Avartar người nhận
        /// </summary>
        public string UserSendAvatar
        {
            get
            {
                return string.Format(_resourceService.GetResource("Common.Avatar.Path"), UserNameSend);
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập người nhận
        /// </summary>
        public int UserReceiveId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trích yếu văn bản
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Tiêu đề nofity 
        /// </summary>
        public string Content
        {
            get
            {
                string result = string.Empty;
                string format = string.Empty;
                switch (NotificationType)
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

                result = string.Format(format, Compendium);
                return result;
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập hướng văn bản
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại hướng chuyển
        /// </summary>
        public int DocumentCopyType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày gửi
        /// </summary>
        public DateTime SentDate { get; set; }

        /// <summary>
        /// Lấy ngày nhận theo định dạng
        /// </summary>
        public string SentDateFomat
        {
            get
            {
                return SentDate.ToString("HH:mm:ss dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái đã xem hay chưa
        /// </summary>
        public bool IsViewed { get; set; }

        /// <summary>
        /// Đã hiển thị dưới dạng thông báo hay chưa
        /// </summary>
        public bool? IsNotified { get; set; }

        /// <summary>
        /// Đã hiển thị dưới dạng thông báo hay chưa
        /// </summary>
        public bool IsNew
        {
            get
            {
                return IsNotified.HasValue ? (IsNotified == false) : true;
            }
        }

        private bool _hasDisplayNumberInBell;
        /// <summary>
        /// Lấy hoặc thiết lập trạng thái khi IsViewed = true có hiển thị số lượng trên cái chuong hay không
        /// </summary>
        public bool HasDisplayNumberInBell
        {
            get
            {
                return _hasDisplayNumberInBell;
            }
            set
            {
                if (IsViewed)
                {
                    value = false;
                }
                _hasDisplayNumberInBell = value;
            }
        }

        /// <summary>
        /// Kiểu notify
        /// </summary>
        public int NotificationType { get; set; }
    }
}