using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(InCatalogValueValidator))]
    public class InCatalogValueModel : PacketModel
    {
        public InCatalogValueModel() : base() { }
        /// <summary>
        /// Lấy hoặc thiết lập Id của danh mục trên form
        /// </summary>
        public Guid InCatalogValueId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên danh mục
        /// </summary>
        [LocalizationDisplayName("InCatalogValue.CreateOrEdit.Fields.InCatalogValueName.Label")]
        public string InCatalogValueName { get; set; }

        [LocalizationDisplayName("InCatalogValue.CreateOrEdit.Fields.ParentId.Label")]
        public Guid? ParentId { get; set; }

        [LocalizationDisplayName("InCatalogValue.CreateOrEdit.Fields.InCatalogValueCode.Label")]
        public string InCatalogValueCode { get; set; }

        public Guid? InCatalogId { get; set; }

        public int? Level { get; set; }

        public Guid? Unit { get; set; }

        public int? Order { get; set; }

        /// <summary>
        /// Loại chỉ số
        /// </summary>
        public Guid? Type { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// kỳ công bố
        /// </summary>
        public string PeriodTypeIds { get; set; }

        /// <summary>
        /// Tiêu thức phân tổ chủ yếu
        /// </summary>
        public string DisTypeIds { get; set; }

        /// <summary>
        /// sử dụng
        /// </summary>
        public bool? Active { get; set; }

        /// <summary>
        /// chỉ tiêu phụ thuộc
        /// </summary>
        public string RegressionIds { get; set; }

        /// <summary>
        /// Giới hạn nhỏ nhất
        /// </summary>
        public string Threshold_min { get; set; }

        /// <summary>
        /// Giới hạn lớn nhất
        /// </summary>
        public string Threshold_max { get; set; }

        /// <summary>
        /// Cho phép tổng hợp theo địa bàn/đơn vị
        /// </summary>
        public bool? AllowAggregation { get; set; }

        /// <summary>
        /// hàm tổng hợp
        /// </summary>
        public string AggregationFormula { get; set; }

        /// <summary>
        /// Số kỳ: Là số lượng các kỳ trước để lấy trung bình cộng số liệu dùng thay thế số liệu kỳ tổng hợp nếu kỳ đó ko có số liệu
        /// </summary>
        public string NumberPeriodReplace { get; set; }

        /// <summary>
        /// Tổng hợp theo kỳ
        /// </summary>
        public bool? AllowAggregationByPeriod { get; set; }

        /// <summary>
        /// Lựa chọn được nhiều danh mục phân tổ
        /// </summary>
        public string InCatalogIds { get; set; }

        /// <summary>
        /// ten don vi tinh
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Tên loại dữ liệu
        /// </summary>
        public string TypeName { get; set; }
    }

    public class ImportInCatalogModel
    {
        public string tenchitieu { get; set; }
        public string machitieu { get; set; }
        public string duongdan { get; set; }
        public string danhmuc { get; set; }
    }
}