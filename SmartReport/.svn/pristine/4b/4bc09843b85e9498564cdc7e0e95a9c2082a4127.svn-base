using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Property - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Danh sách các thuộc tính mẫu của control mẫu trong form động.</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Khởi tạo.
        /// </summary>
        public Property()
        {
            UIName = "";
            UIDescription = "";
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id của Property
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên thuộc tính.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên hiển trị trên giao diện của thuộc tính.
        /// </summary>
        /// <example>
        /// Name: Họ và tên<para></para>
        /// UIName: Họ và tên bên A, Họ và tên bên B...
        /// </example>
        public string UIName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mô tả về thuộc tính.
        /// </summary>
        public string UIDescription { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện thuộc tính chứa nhiều giá trị lựa chọn hay không.
        /// </summary>
        public bool IsMultivalue { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị mặc định của thuộc tính nếu có.
        /// </summary>
        [JsonIgnore]
        public PropertyValue DefaultValue { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các giá trị thuộc tính của thuộc tính hiện tại.
        /// </summary>
        [JsonIgnore]
        public List<PropertyValue> PropertyValues { get; set; }

    }
}