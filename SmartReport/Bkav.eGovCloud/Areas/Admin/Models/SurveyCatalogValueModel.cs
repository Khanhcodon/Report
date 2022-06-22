using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(SurveyCatalogValueValidator))]
    public class SurveyCatalogValueModel : PacketModel
    {
        public SurveyCatalogValueModel() : base() { }
        /// <summary>
        /// Lấy hoặc thiết lập Id của danh mục trên form
        /// </summary>
        public Guid CatalogValueId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên danh mục
        /// </summary>
        [LocalizationDisplayName("SurveyCatalogValue.CreateOrEdit.Fields.SurveyCatalogValueName.Label")]
        public string Value { get; set; }

        [LocalizationDisplayName("SurveyCatalogValue.CreateOrEdit.Fields.ParentId.Label")]
        public Guid? ParentId { get; set; }

        [LocalizationDisplayName("SurveyCatalogValue.CreateOrEdit.Fields.SurveyCatalogValueCode.Label")]
        public string CatalogKey { get; set; }

        public Guid CatalogId { get; set; }

        public int Level { get; set; }

    }

    public class ImportSurveyCatalogModel
    {
        public string tenchitieu { get; set; }
        public string machitieu { get; set; }
        public string duongdan { get; set; }
        public string danhmuc { get; set; }
    }
}