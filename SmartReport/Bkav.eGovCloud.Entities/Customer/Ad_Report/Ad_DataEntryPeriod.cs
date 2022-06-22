using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// quản lý danh mục phân tổ
    /// </summary>
    public class Ad_DataEntryPeriod
    {
        /// <summary>
        /// id cua phan ra danh muc
        /// </summary>
        /// 
        [Key]
        public Guid DataEntryPeriodId { get; set; }

        /// <summary>
        /// id cua indicator
        /// </summary>
        public Guid TypePeriodId { get; set; }

        /// <summary>
        /// ten danh muc phan ra
        /// </summary>
        public string DataEntryPeriodCode { get; set; }

        /// <summary>
        /// mã danh mục
        /// </summary>
        public string DataEntryPeriodName { get; set; }

        /// <summary>
        /// kích hoạt
        /// </summary>
        public bool IsActive { get; set; }

        
        /// <summary>
        /// 
        /// </summary>
        public string Depcription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OrderType { get; set; }

    }
}
