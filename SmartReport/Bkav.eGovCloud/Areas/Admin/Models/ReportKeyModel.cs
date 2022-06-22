using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    /// <summary>
    /// Report group model
    /// </summary>
    [FluentValidation.Attributes.Validator(typeof(ReportKeyValidator))]
    public class ReportKeyModel
    {
        /// <summary>
        /// Key
        /// </summary>
        public int ReportKeyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết  lập tên báo cáo
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mô tả báo cáo
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.Description.Label")]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục cha của báo cáo
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn dữ liệu cho báo cáo
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.QueryStatistics.Label")]
        public string Sql { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập trạng thái sử dụng báo cáo.
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.IsActive.Label")]
        public bool IsActive { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập mã key
        /// </summary>
        [LocalizationDisplayName("Common.TemplateKey.CreateOrEdit.Fields.Code.Label")]
        public string Code { get; set; }

    }
}