using System;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eGov team
    /// Project: eGov Cloud v1.0
    /// Class : ContentEntity - public - Entity
    /// Access Modifiers:
    /// Create Date : 30122014
    /// Author      : QuangP
    /// Description : Model tương ứng đối tượng EgovJob
    /// </summary>
    public class EgovJobModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên timer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Loại timerjob, định nghĩa trong EgovTimerJobEnum
        /// </summary>
        public int JobType { get; set; }

        /// <summary>
        /// JobInEnum
        /// </summary>
        public EgovJobEnum JobTypeInEnum
        {
            get
            {
                return (EgovJobEnum)JobType;
            }
        }

        /// <summary>
        /// Khoảng cách mỗi lần chạy
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Lần chạy cuối cùng
        /// </summary>
        public DateTime? LastRun { get; set; }

        /// <summary>
        /// Lần chạy tiếp theo
        /// </summary>
        public DateTime? NextRun { get; set; }

        /// <summary>
        /// Lần sửa cuối cùng
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Có kích hoạt không
        /// </summary>
        public bool IsActivated { get; set; }
    }
}