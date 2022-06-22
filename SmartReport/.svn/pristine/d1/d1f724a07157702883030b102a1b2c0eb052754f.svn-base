using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(DocColumnSettingModelValidator))]
    public class DocColumnSettingModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id ColumnSetting
        /// </summary>
        public int DocColumnSettingId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên ColumnSetting
        /// </summary>
        [LocalizationDisplayName("DocColumnSetting.TemplateKey.CreateOrEdit.Fields.DocColumnSettingName.Label")]
        public string DocColumnSettingName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập  các cột sắp xếp
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách nhóm
        /// </summary>
        public string GroupColumn { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các cột hiển thị
        /// </summary>
        public string DisplayColumn { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập căn lề
        /// </summary>
        public string Justify { get; set; }

        /// <summary>
        /// ColumnSettingType
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ColumnSettingType TypeEnum
        {
            get
            {
                return (ColumnSettingType)Type;
            }
        }
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
}