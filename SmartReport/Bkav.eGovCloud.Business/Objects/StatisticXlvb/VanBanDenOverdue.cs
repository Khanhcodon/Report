using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects.StatisticXlvb
{
    /// <summary>
    /// 
    /// </summary>
    public class VanBanDenOverdue : StatisticsGroup
    {
        public int Index { get; set; }
        public int DocumentCopyId { get; set; }
        /// <summary>
        /// Số ký hiệu
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Người xử lý
        /// </summary>
        public string UserFullName { get; set; }

        /// <summary>
        /// Ngày nhận
        /// </summary>
        public string FromDate { get; set; }

        /// <summary>
        /// Ngày xử lý
        /// </summary>
        public string ToDate { get; set; }

        /// <summary>
        /// Hạn xử lý
        /// </summary>
        public string DateOverdue { get; set; }

        /// <summary>
        /// Phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Hạn xử lý
        /// </summary>
        public string StatusName { get; set; }


        /// <summary>
        /// Hạn xử lý
        /// </summary>
        public string StatusDXLName { get; set; }

    }
}
