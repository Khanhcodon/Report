using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Báo báo phat hành
    /// </summary>
    public class StatisticPublish
    {
        /// <summary>
        /// Ngày phát hành
        /// </summary>
        public string DatePublished { get; set; }

        /// <summary>
        /// Mã hồ sơ
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Người ký
        /// </summary>
        public string UserSuccess { get; set; }

        /// <summary>
        /// Nơi nhân
        /// </summary>
        public string ProcessInfo { get; set; }

        /// <summary>
        /// Nơi lưu
        /// </summary>
        public string InOutPlace { get; set; }

        /// <summary>
        /// HÌnh thức
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Là nhóm
        /// </summary>
        public bool IsGroup { get; set; }

        /// <summary>
        /// Tên nhóm
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Số lượng của nhóm
        /// </summary>
        public int GroupCount { get; set; }

        /// <summary>
        /// Người soạn
        /// </summary>
        public string UserCreatedName { get; set; }
    }
}
