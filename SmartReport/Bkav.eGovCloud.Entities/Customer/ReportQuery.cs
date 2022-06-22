using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportQuery
    {
        private List<ReportQueryFilter> _filters;
        /// <summary>
        /// 
        /// </summary>
        public int ReportQueryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReportQueryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ActionLevelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? DataTableId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? FormulaDataColumnId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string  FormCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual List<ReportQueryFilter> Filters
        {
            get { return _filters ?? (_filters = new List<ReportQueryFilter>()); }
            set { _filters = value; }
        }
    }
}
