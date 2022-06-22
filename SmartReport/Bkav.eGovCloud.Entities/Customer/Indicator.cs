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
    public class Indicator
    {
        private List<CategoryDisaggregations> _categoryDisaggregations;

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid IndicatorId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IndicatorName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IndicatorDesctiption { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Tên các giá trị của danh mục.
        /// </summary>
        public List<string> InCategoryDisaggregationNames { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Các giá trị của chi tieu
        /// </summary>
        public virtual List<CategoryDisaggregations> InCategoryDisaggregations
        {
            get { return _categoryDisaggregations ?? (_categoryDisaggregations = new List<CategoryDisaggregations>()); }
            set { _categoryDisaggregations = value; }
        }
    }
}
