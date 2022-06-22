using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using System;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ActionLevelValidator))]
    public class ActionLevelModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id kỳ báo cáo
        /// </summary>
        public int ActionLevelId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên kỳ báo cáo
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.ActionLevelName.Label")]
        public string ActionLevelName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mã kỳ báo cáo
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.ActionLevelCode.Label")]
        public string ActionLevelCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thời gian bắt đầu
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.StartTime.Label")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thời gian kết thúc
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.EndTime.Label")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key lưu trữ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.TemplateKey.Label")]
        public string TemplateKey { get; set; }
    }
}