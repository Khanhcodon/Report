using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ReportQueryGroupValidator))]
    public class ReportQueryGroupModel
    {
        public int ReportQueryGroupId { get; set; }

        [LocalizationDisplayName("ReportQuery.CreateOrEdit.Fields.ReportQueryGroupName.Label")]
        public string ReportQueryGroupName { get; set; }

        public string Description { get; set; }

        [LocalizationDisplayName("ReportQuery.CreateOrEdit.Fields.CreatedAt.Label")]
        public DateTime CreatedAt { get; set; }

        [LocalizationDisplayName("ReportQuery.CreateOrEdit.Fields.CreatedBy.Label")]
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
        public List<ReportQuery> ReportQuerys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<int> ReportQueryIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Query { get; set; }
    }
}