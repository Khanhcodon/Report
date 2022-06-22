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
    [FluentValidation.Attributes.Validator(typeof(ReportValidator))]
    public class ReportModel
    {
        /// <summary>
        /// Key
        /// </summary>
        public int ReportId { get; set; }

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
        /// Lấy hoặc thiết lập cấu hình hiển thị báo cáo
        /// </summary>
        public int DocColumnId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các báo cáo nhóm [id, id,...]
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.GroupForTree.Label")]
        public string GroupForTree { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn dữ liệu cho báo cáo
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.QueryReport.Label")]
        public string QueryReport { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn dữ liệu cho báo cáo
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.QueryStatistics.Label")]
        public string QueryStatistics { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu query lấy tổng số record của báo cáo.
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.QueryTotal.Label")]
        public string QueryTotal { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập file crystalreport tương ứng
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.CrystalPath.Label")]
        public string CrystalPath { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập file crystalreport theo nhóm tương ứng
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.CrystalGroupPath.Label")]
        public string CrystalGroupPath { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người được xem báo cáo
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.UserPermission.Label")]
        public string UserPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập phòng ban được xem báo cáo
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.DeptPermission.Label")]
        public string DeptPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập chức vụ được xem báo cáo
        /// </summary>
        public string PositionPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái sử dụng báo cáo.
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.IsActive.Label")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái xem dạng thống kê hay báo cáo.
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.IsLabel.Label")]
        public bool IsLabel { get; set; }

        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.IsHSMC.Label")]
        public bool IsHSMC { get; set; }

        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.IsShowTotal.Label")]
        public bool IsShowTotal { get; set; }

        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.IsFile.Label")]
        public int IsFile { get; set; }

        public DateTime DateCreated { get; set; }

        public string ReportPath { get; set; }

        public string ReportGroupPath { get; set; } 

        public List<int> TreeGroupIds { get; set; }
        public List<int> UserPermissionIds { get; set; }
        public List<int> PositionPermissionIds { get; set; }
        public List<string> DepartmentPositionIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn lấy số văn bản quá hạn
        /// </summary>
         [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.QueryTotalDocumentIsOverdue.Label")]
        public string QueryTotalDocumentIsOverdue { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn lấy số văn bản đã xử lý
        /// </summary>
        [LocalizationDisplayName("Admin.Report.CreateOrEdit.Fields.QueryTotalDocumentProcessed.Label")]
        public string QueryTotalDocumentProcessed { get; set; }

        public string ColumnConfig { get; set; }
    }
}