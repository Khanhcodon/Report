using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(InterfaceConfigValidator))]
    public class InterfaceConfigModel : PacketModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập id  cấu hình giao diện
        /// </summary>
        public int InterfaceConfigId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên cấu hình giao diện
        /// </summary>
        [LocalizationDisplayName("InterfaceConfig.CreateOrEdit.Fields.InterfaceConfigName.Label")]
        public string InterfaceConfigName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mô tả
        /// </summary>
        [LocalizationDisplayName("InterfaceConfig.CreateOrEdit.Fields.Description.Label")]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu giao diện
        /// </summary>
        [LocalizationDisplayName("InterfaceConfig.CreateOrEdit.Fields.Template.Label")]
        public string Template { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Nghiệp vụ cho cấu hình giao diện
        /// </summary>
        public int? CategoryBusinessId { get; set; }
    }
}