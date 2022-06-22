using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bkav.eGovCloud.Entities.Customer
{
    
    /// <summary>
    /// 
    /// </summary>
    public class dataType
    {
       /* private List<CategoryDisaggregations> _categoryDisaggregations;*/

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid dataTypeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nameID { get; set; }
        /// <summary>
        /// nên số liệu
        /// </summary>
        public string dataTypeName { get; set; }

        /// <summary>
        /// mô tả số liệu
        /// </summary>
        public string dataTypeDescription { get; set; }
        /// <summary>
        ///  phân loại số liệu
        /// </summary>
        public string distribute { get; set; }
        /// <summary>
        /// trạng thái sử dựng
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Tên các giá trị của danh mục.
        /// </summary>
        public List<string> InCategoryDisaggregationNames { get; set; }


      /*  public virtual List<CategoryDisaggregations> InCategoryDisaggregations
        {
            get { return _categoryDisaggregations ?? (_categoryDisaggregations = new List<CategoryDisaggregations>()); }
            set { _categoryDisaggregations = value; }
        }*/
    }
}
