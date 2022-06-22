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
    public class Ad_Locality
    {
       // private List<CategoryDisaggregations> _categoryDisaggregations;

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid LocalityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        

        public string LocalityName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
            
        public int Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LocalityParent { get; set; }


        //public List<string> InCategoryDisaggregationNames { get; set; }


        //public virtual List<CategoryDisaggregations> InCategoryDisaggregations
        //{
        //    get { return _categoryDisaggregations ?? (_categoryDisaggregations = new List<CategoryDisaggregations>()); }
        //    set { _categoryDisaggregations = value; }
        //}
    }
}
