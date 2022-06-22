using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects.StatisticXlvb
{
    /// <summary>
    /// Nhoms
    /// </summary>
    public class StatisticsGroup
    {
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
    }
}
