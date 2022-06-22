using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ReportQueryValidator))]
    public class ReportQueryModel
    {
        public int ReportQueryId { get; set; }

        [LocalizationDisplayName("ReportQuery.CreateOrEdit.Fields.ReportQueryName.Label")]
        public string ReportQueryName { get; set; }

        [LocalizationDisplayName("ReportQuery.CreateOrEdit.Fields.TemplateKeyCategoryId.Label")]
        public int? ActionLevelId { get; set; }

        [LocalizationDisplayName("ReportQuery.CreateOrEdit.Fields.TemplateKeyCategoryId.Label")]
        public string ActionLevelName { get; set; }

        [LocalizationDisplayName("ReportQuery.CreateOrEdit.Fields.TableName.Label")]
        public int? DataTableId { get; set; }

        [LocalizationDisplayName("ReportQuery.CreateOrEdit.Fields.TableName.Label")]
        public string DataTableName { get; set; }

        [LocalizationDisplayName("ReportQuery.CreateOrEdit.Fields.TableName.Label")]
        public string DataTableDescription { get; set; }

        [LocalizationDisplayName("ReportQuery.CreateOrEdit.Fields.FormulaColumnName.Label")]
        public string FormulaDataColumnName { get; set; }

        [LocalizationDisplayName("ReportQuery.CreateOrEdit.Fields.FormulaColumnName.Label")]
        public int? FormulaDataColumnId { get; set; }

        public string FormCode { get; set; }

        [LocalizationDisplayName("ReportQuery.CreateOrEdit.Fields.Query.Label")]
        public string Query { get; set; }

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
        public List<ReportQueryFilterModel> Filters { get; set; }
    }
}