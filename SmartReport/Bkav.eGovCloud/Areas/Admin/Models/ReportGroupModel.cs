using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    /// <summary>
    /// Report group model
    /// </summary>
    [FluentValidation.Attributes.Validator(typeof(ReportGroupValidator))]
    public class ReportGroupModel
    {
        public ReportGroupModel()
        {
            IsReport = true;
        }

        /// <summary>
        /// Key
        /// </summary>
        public int ReportGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên báo cáo nhóm
        /// </summary>
        [LocalizationDisplayName("Admin.ReportGroup.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên trường dữ liệu sẽ nhóm theo
        /// </summary>
        [LocalizationDisplayName("Admin.ReportGroup.CreateOrEdit.Fields.FieldName.Label")]
        public string FieldName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên trường dữ liệu sẽ nhóm theo
        /// </summary>
        [LocalizationDisplayName("Admin.ReportGroup.CreateOrEdit.Fields.FieldDisplay.Label")]
        public string FieldDisplay { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn lấy dữ liệu bind lên tree
        /// </summary>
        [LocalizationDisplayName("Admin.ReportGroup.CreateOrEdit.Fields.Query.Label")]
        public string Query { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập lấy cho báo cáo hay thống kê
        /// </summary>
        [LocalizationDisplayName("Admin.ReportGroup.CreateOrEdit.Fields.IsReport.Label")]
        public bool IsReport { get; set; }
    }
}