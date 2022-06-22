using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Control - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT@bkav.com
    /// </author>
    /// <summary>
    /// <para>Đối tượng control mẫu trong biểu mẫu động: Dropdownlist, Checkboxlist, Textbox</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class Control
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của control mẫu.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên control mẫu.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các thuộc tính của control mẫu.
        /// </summary>
        /// <remarks>
        /// Không được sửa thành LstProperties vì dùng tương ứng với biến js
        /// </remarks>
        [JsonIgnore]
        [JsonProperty("lstProperties")]
        public List<Property> Properties { get; set; }

    }
}