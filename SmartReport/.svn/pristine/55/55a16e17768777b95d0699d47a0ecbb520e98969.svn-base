using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ReportModeValidator))]
    public class ReportModeModel
    {
        public int ReportModeId { get; set; }

        [LocalizationDisplayName("ReportMode.CreateOrEdit.Fields.Code.Label")]
        public string Code { get; set; }

        [LocalizationDisplayName("ReportMode.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        [LocalizationDisplayName("ReportMode.CreateOrEdit.Fields.Subject.Label")]
        public string Subject { get; set; }

        [LocalizationDisplayName("ReportMode.CreateOrEdit.Fields.IssueOrg.Label")]
        public string IssueOrg { get; set; }

        [LocalizationDisplayName("ReportMode.CreateOrEdit.Fields.IssueDate.Label")]
        public DateTime? IssueDate { get; set; }
        public string TmpIssueDate { get; set; }

        [LocalizationDisplayName("ReportMode.CreateOrEdit.Fields.Number.Label")]
        public string Number { get; set; }

        [LocalizationDisplayName("ReportMode.CreateOrEdit.Fields.Notation.Label")]
        public string Notation { get; set; }

        [LocalizationDisplayName("ReportMode.CreateOrEdit.Fields.RefNotation.Label")]
        public string RefNotation { get; set; }
        [LocalizationDisplayName("ReportMode.CreateOrEdit.Fields.ReportMode.Label")]
        public int ReportMode { get; set; }

        [LocalizationDisplayName("ReportMode.CreateOrEdit.Fields.File.Label")]
        public string Attachments { get; set; }

        public List<ObjFile> AFiles { get; set; }
        public List<string> RefNotations { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Guid> DocTypeIds { get; set; }
    }
    public class ObjFile
    {
        public string EmbryonicLocationName { get; set; }
        public string EmbryonicPath { get; set; }
    }

}