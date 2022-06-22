using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(WorkflowValidator))]
    public class WorkflowModel : PacketModel
    {
        public WorkflowModel()
            : base()
        {
            IsActivated = false;
            ExpireProcess = 1;
            CreatedOnDate = DateTime.Now;
            VersionDateTime = DateTime.Now;
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id luồng văn bản, hồ sơ
        /// </summary>
        public int WorkflowId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên luồng văn bản, hồ sơ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Workflow.CreateOrEdit.Fields.WorkflowName.Label")]
        public string WorkflowName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Luồng văn bản hổ sơ (kiểu json)
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Workflow.CreateOrEdit.Fields.Json.Label")]
        public string Json { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        public DateTime? CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người cập nhật cuối cùng
        /// </summary>
        public int? LastModifiedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày cập nhật cuối cùng
        /// </summary>
        public DateTime? LastModifiedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra luồng văn bản này đang được sử dụng
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Workflow.CreateOrEdit.Fields.IsActivated.Label")]
        public bool IsActivated { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập template giao diện mặc định cho quy trình
        ///// </summary>
        //public string Template { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập hạn xử lý luồng văn bản 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Workflow.CreateOrEdit.Fields.ExpireProcess.Label")]
        public int ExpireProcess { get; set; }

        /// <summary>
        /// Chuỗi json chứa loại workflow
        /// </summary>
        public string WorkflowTypeJson { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập id template giao diện mặc định cho quy trình
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Workflow.CreateOrEdit.Fields.InterfaceConfigId.Label")]
        public int? InterfaceConfigId { get; set; }
    }
}
