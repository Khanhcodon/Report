using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer
{/// <summary>
/// 
/// </summary>
   public class Ad_Unit
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Key]
        public Guid IdUnit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Unit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public string Exchange { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public string OriginalUnit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public bool? Use { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public List<string> InCategoryDisaggregationNames { get; set; }
    }
}

