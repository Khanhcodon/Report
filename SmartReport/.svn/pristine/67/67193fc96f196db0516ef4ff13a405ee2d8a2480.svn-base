
using Bkav.eGovCloud.Entities.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Bkav.eGovCloud.Models
{
    /// <summary>
    /// Report group model
    /// </summary>
    public class ReportViewModel
    {
        /// <summary>
        /// Report Id
        /// </summary>
        [JsonProperty(PropertyName = "reportId")]
        public int ReportId { get; set; }

        /// <summary>
        /// Tên báo cáo
        /// </summary>
        [JsonProperty(PropertyName = "reportName")]
        public string ReportName { get; set; }

        /// <summary>
        /// Mô tả báo cáo
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Cấu hình các cột hiển thị
        /// </summary>
        [JsonProperty(PropertyName = "displaySettings")]
        public IEnumerable<ColumnSetting> DisplaySettings { get; set; }

        /// <summary>
        /// Danh sách các nhóm 
        /// </summary>
        [JsonProperty(PropertyName = "groupSettings")]
        public IEnumerable<GroupColumnModel> GroupSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "sortSettings")]
        public IEnumerable<SortColumnModel> SortSettings { get; set; }

        /// <summary>
        /// Số dòng dữ liệu
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        /// <summary>
        /// Dữ liệu trang đầu báo cáo
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public IEnumerable<IDictionary<string, object>> Data { get; set; }

        /// <summary>
        /// Thời điểm cập nhật báo cáo gần nhất
        /// </summary>
        [JsonProperty(PropertyName = "lastUpdate")]
        public string LastUpdate { get; set; }

        [JsonProperty(PropertyName = "header")]
        public string Header { get; set; }

        [JsonProperty(PropertyName = "footer")]
        public string Footer { get; set; }

        public string PivotConfig { get; set; }

        public string ColumnConfig { get; set; }

        public bool isHCMC { get; set; }

        public int isFile { get; set; }
    }

    public class ColumnSetting
    {
        /// <summary>
        /// Lấy hoặc thiết lập Tên cột trong câu truy vấn
        /// </summary>
        [JsonProperty(PropertyName = "columnName")]
        public string ColumnName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên hiển thị trên danh sách
        /// </summary>
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên cột sẽ được sắp sếp
        /// </summary>
        [JsonProperty(PropertyName = "sortName")]
        public string SortName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Độ rộng của cột
        /// </summary>
        [JsonProperty(PropertyName = "width")]
        public int? Width { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thứ tự cột
        /// </summary>
        [JsonProperty(PropertyName = "order")]
        public int Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra cột này được phép sắp xếp
        /// </summary>
        [JsonProperty(PropertyName = "enableSort")]
        public bool EnableSort { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị căn lề
        /// </summary>
        [JsonProperty(PropertyName = "justify")]
        public string Justify { get; set; }
    }

    public class SortColumnModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập tên cột sắp xếp
        /// </summary>
        [JsonProperty(PropertyName = "columnName")]
        public string ColumnName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập sắp xếp tăng hay gảm dần
        /// </summary>
        [JsonProperty(PropertyName = "isDesc")]
        public bool IsDesc { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số thứ tự
        /// </summary>
        [JsonProperty(PropertyName = "index")]
        public int Index { get; set; }
    }

    public class GroupColumnModel
    {
        /// <summary>
        /// Tên hiển thị
        /// </summary>
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Tên cột hiển thị
        /// </summary>
        [JsonProperty(PropertyName = "columnName")]
        public string ColumnName { get; set; }

        /// <summary>
        /// Tên cột giá trị
        /// </summary>
        [JsonProperty(PropertyName = "groupBy")]
        public string GroupBy { get; set; }

        /// <summary>
        /// Cho phép hiển thị số lượng
        /// </summary>
        [JsonProperty(PropertyName = "hasDisplayCount")]
        public bool HasDisplayCount { get; set; }
    }

    //show file
    public class ReportFileModel
    {
        /// <summary>
        /// Report Id
        /// </summary>
        [JsonProperty(PropertyName = "DocumentId")]
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Tên báo cáo
        /// </summary>
        [JsonProperty(PropertyName = "AttachmentId")]
        public int AttachmentId { get; set; }
    }

}