// 20191101 VUHQ REQ-02
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ConfigTypeValidator))]
    public class ConfigTypeModel : PacketModel
    {
        public ConfigTypeModel() : base() { }

        /// <summary>
        /// Lấy hoặc thiết lập Id của loại cấu hình bảng
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên của loại cấu hình bảng
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kích hoạt sử dụng hay không
        /// </summary>
        public bool IsActived { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập loại field của MySQL
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập cha của loại cấu hình
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập format hiển thị cấu hình 
        /// </summary>
        public string DisplayCode { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập RegX validate cho loại cấu hình
        /// </summary>
        public string PatternCode { get; set; }

        private List<ConfigType> _subConfigTypes;

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// </summary>
        public List<ConfigType> SubConfigTypes
        {
            get { return _subConfigTypes ?? (_subConfigTypes = new List<ConfigType>()); }
            set { _subConfigTypes = value; }
        }
    }
}