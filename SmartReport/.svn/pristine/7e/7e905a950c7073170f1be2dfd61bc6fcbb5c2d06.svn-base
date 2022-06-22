using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(PaperValidator))]
    public class PaperModel : PacketModel
    {
        public PaperModel()
            : base()
        {
            Amount = 1;
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id của giấy tờ
        /// </summary>
        public int PaperId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của loại giấy tờ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.PaperTypeId.Label")]
        public int PaperTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên của loại giấy tờ
        /// </summary>
        public string PaperTypeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của loại hồ sơ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.DocTypeId.Label")]
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên của giấy tờ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.PaperName.Label")]
        public string PaperName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số lượng của giấy tờ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.Amount.Label")]
        public int Amount { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thứ tự của giấy tờ
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái bắt buộc: Có (true), Không (false)
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.IsRequierd.Label")]
        public bool IsRequired { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id lĩnh vực
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.DocFieldId.Label")]
        public int? DocFieldId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>DungHV - 200813</para>
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Paper.CreateOrEdit.Fields.CategoryBusinessCode.Label")]
        public int CategoryBusinessId { get; set; }

        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }
    }
}