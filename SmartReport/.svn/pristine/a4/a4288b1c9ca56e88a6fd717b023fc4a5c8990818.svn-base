using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(FeeValidator))]
    public class FeeModel : PacketModel
    {
        public FeeModel() : base() { }

        /// <summary>
        /// Lấy hoặc thiết lập Id của lệ phí
        /// </summary>
        public int FeeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của loại lệ phí
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.FeeTypeId.Label")]
        public int FeeTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên loại lệ phí
        /// </summary>
        public string FeeTypeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id lĩnh vực
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.DocFieldId.Label")]
        public int? DocFieldId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của loại hồ sơ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.DocTypeId.Label")]
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên của lệ phí
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.FeeName.Label")]
        public string FeeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá của lệ phí
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.Price.Label")]
        public int Price { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái bắt buộc: Có (true), Không (false)
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.IsRequierd.Label")]
        public bool IsRequired { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>DungHV - 200813</para>
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Fee.CreateOrEdit.Fields.CategoryBusinessCode.Label")]
        public int CategoryBusinessId { get; set; }

        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }
    }
}