using System;

namespace Bkav.eGovCloud.Entities.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Log - public - Entity
    /// Access Modifiers: 
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Log trong CSDL
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của nhật ký
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại nhật ký
        /// </summary>
        public int LogType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại nhật ký
        /// </summary>
        public virtual LogType LogTypeInEnum
        {
            get
            {
                return (LogType)LogType;
            }
            set
            {
                LogType = (int)value;
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả ngắn gọn về nhật ký
        /// </summary>
        public string ShortMessage { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả chi tiết về nhật ký
        /// </summary>
        public string FullMessage { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thông tin về HttpRequest bao gồm: Servers, Forms, Cookies, QueryString
        /// </summary>
        public string RequestJson { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Địa chỉ IP của máy client
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo nhật ký
        /// </summary>
        public DateTime CreatedOnDate { get; set; }
    }
}
