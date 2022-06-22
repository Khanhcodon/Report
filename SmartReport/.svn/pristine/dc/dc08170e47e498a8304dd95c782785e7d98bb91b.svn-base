using System;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class ActivityLogModel
    {
        private readonly ResourceBll _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

        /// <summary>
        /// Lấy hoặc thiết lập Id nhật ký hành động
        /// </summary>
        public int ActivityLogId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại nhật ký hành động
        /// </summary>
        public byte ActivityLogType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại nhật ký hành động
        /// </summary>
        public string ActivityLogTypeString
        {
            get { return _resourceService.GetEnumDescription<ActivityLogType>((ActivityLogType)ActivityLogType); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập người dùng
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Nội dung
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ip
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        public DateTime CreatedOnDate { get; set; }
    }
}