using System;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : JsCatalogSelected - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Là các giá trị dữ liệu cụ thể được chọn khi sử dụng biểu mẫu động của các danh mục <see cref="JsCatalog"/></para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class JsCatalogSelected
    {
        /// <summary>
        /// Là CatalogId
        /// </summary>
        /// <value>The key.</value>
        [JsonProperty("catalogid")]
        public string Key { get; set; }

        /// <summary>
        /// Là CatalogName
        /// </summary>
        /// <value>The value.</value>
        [JsonProperty("catalogname")]
        public string Value { get; set; }

        /// <summary>
        /// Là GlobalCode của CatalogValue.
        /// </summary>
        /// <value>The global code.</value>
        [JsonProperty("globalcode")]
        public Guid GlobalCode { get; set; }

        /// <summary>
        /// Khởi tạo <see cref="JsCatalogSelected"/>
        /// </summary>
        public JsCatalogSelected()
        {
            Key = new Guid().ToString("N");
            Value = "";
        }
    }
}
