using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ReportQueryFilterValidator))]
    public class ReportQueryFilterModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int ReportQueryFilterId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ReportQueryId { get; set; }

        /// <summary>
        /// DataFieldId
        /// </summary>
        public int? DataFieldId { get; set; }

        /// <summary>
        /// DataFieldId
        /// </summary>
        public string DataFieldName { get; set; }

        /// <summary>
        /// FilterValue, FilterFromValue
        /// </summary>
        public string FilterValue { get; set; }

        /// <summary>
        /// FilterToValue
        /// </summary>
        public string FilterToValue { get; set; }

        /// <summary>
        /// FilterValues
        /// </summary>
        public string FilterValues { get; set; }

        /// <summary>
        /// Condition:
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// DataFieldType:
        /// 1: DataField table
        /// 2: DataField math
        /// 3: DataField template
        /// </summary>
        public int DataFieldType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSummary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFilter { get; set; }
    }
}