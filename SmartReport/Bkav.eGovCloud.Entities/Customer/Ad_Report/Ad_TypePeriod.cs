using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Quản lý chỉ tiểu phân tổ 
    /// </summary>   
    public class Ad_TypePeriod
    {
      //  private List<Ad_DataEntryPeriod> _dataEntryPeriods;

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid TypePeriodId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TypePeriodName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Classify { get; set; }

        /// <summary>
        /// 
        /// </summary>
        

        public bool IsActive { get; set; }

        /// <summary>
        /// Tên các giá trị của danh mục.
        /// </summary>


        /// <summary>
        /// 
        /// </summary>
        public string DepcriptionTypePeriod { get; set; }

        //public List<string> InCategoryDisaggregationNames { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập Các giá trị của chi tieu
        ///// </summary>
        //public virtual List<CategoryDisaggregations> InCategoryDisaggregations
        //{
        //    get { return _dataEntryPeriods ?? (_dataEntryPeriods = new List<Ad_DataEntryPeriod>()); }
        //    set { _dataEntryPeriods = value; }
        //}
    }
}
