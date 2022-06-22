using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    /// <summary>
    /// Report group model
    /// </summary>
    [FluentValidation.Attributes.Validator(typeof(StatisticsValidator))]
    public class StatisticsModel
    {
        public StatisticsModel()
        {
            DateCreated = DateTime.Now;
        }
        /// <summary>
        /// Key
        /// </summary>
        public int StatisticsId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên thống kê
        /// </summary>
        [LocalizationDisplayName("Admin.Statistics.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mô tả báo cáo
        /// </summary>
        [LocalizationDisplayName("Admin.Statistics.CreateOrEdit.Fields.Description.Label")]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục cha của báo cáo
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các báo cáo nhóm báo cáo
        /// </summary>
        [LocalizationDisplayName("Admin.Statistics.CreateOrEdit.Fields.ReportGroupId.Label")]
        public string ReportGroup { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn lấy dữ liệu thống kê
        /// </summary>
        [LocalizationDisplayName("Admin.Statistics.CreateOrEdit.Fields.Query.Label")]
        public string Query { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian tạo 
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người được xem báo cáo
        /// <para> Được lưu theo dạng: [id1,id2,...]</para>
        /// </summary>
        [LocalizationDisplayName("Admin.Statistics.CreateOrEdit.Fields.UserPermission.Label")]
        public string UserPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập phòng ban được xem báo cáo
        /// <para> Được lưu theo dạng: [{"DepartmentId" : id1, "PositionId" : id2}]</para>
        /// </summary>
        [LocalizationDisplayName("Admin.Statistics.CreateOrEdit.Fields.DeptPermission.Label")]
        public string DeptPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập chức vụ được xem báo cáo
        /// <para> Được lưu theo dạng: [id1,id2,...]</para>
        /// </summary>
        [LocalizationDisplayName("Admin.Statistics.CreateOrEdit.Fields.PositionPermission.Label")]
        public string PositionPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái sử dụng báo cáo.
        /// </summary>
        [LocalizationDisplayName("Admin.Statistics.CreateOrEdit.Fields.IsActive.Label")]
        public bool IsActive { get; set; }

        public List<int> ReportGroupIds { get; set; }

        public List<int> UserPermissionIds { get; set; }

        public List<int> PositionPermissionIds { get; set; }

        public List<string> DepartmentPositionIds { get; set; }
    }
}