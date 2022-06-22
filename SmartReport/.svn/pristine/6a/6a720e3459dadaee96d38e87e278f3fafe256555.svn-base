using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Category - public - Entity
    /// Access Modifiers: 
    /// Create Date : 25052020
    /// Author      : TuDv
    /// Description : Entity tương ứng với bảng IndicatorCatalog trong CSDL. Bảng IndicatorCatalog là bảng lưu trữ chỉ tiêu.
    /// </summary>
    public class InCatalogValue
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id 
        /// </summary>
        [Key]
        public Guid InCatalogValueId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id 
        /// </summary>
        public Guid? InCatalogId { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập Tên 
        /// </summary>
        public string InCatalogValueName { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập Tên 
        /// </summary>
        public string InCatalogValueCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh mục cha
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// Cấp lồng (nested) so với root
        /// </summary>
        public int? Level { get; set; }

        /// <summary>
        /// đơn vị tính
        /// </summary>
        public Guid? Unit { get; set; }

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
}
