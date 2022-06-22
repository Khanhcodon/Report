using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(RequiredSupplementaryValidator))]
    public class RequiredSupplementaryModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của giấy tờ
        /// </summary>
        public int RequiredSupplementaryId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên của loại giấy tờ
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của loại hồ sơ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.RequiredSupplementary.CreateOrEdit.Fields.DocTypeId.Label")]
        public Guid? DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id lĩnh vực
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.RequiredSupplementary.CreateOrEdit.Fields.DocFieldId.Label")]
        public int? DocFieldId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id lĩnh vực
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.RequiredSupplementary.CreateOrEdit.Fields.DocFieldId.Label")]
        public int? UserId { get; set; }
    }
}