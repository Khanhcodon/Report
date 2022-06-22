using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportQueryGroup
    {
        private List<ReportQueryGroupReportQuery> _querys;
        /// <summary>
        /// 
        /// </summary>
        public int ReportQueryGroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReportQueryGroupName { get; set; }

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
        public virtual List<ReportQueryGroupReportQuery> Querys
        {
            get { return _querys ?? (_querys = new List<ReportQueryGroupReportQuery>()); }
            set { _querys = value; }
        }
    }
}
