using System;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : JsCatalogItem - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Danh mục catalog item, là các giá trị dữ liệu cụ thể cho các danh mục <see cref="JsCatalog"/></para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class JsCatalogItem
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của CatalogItem
        /// </summary>
        /// <value>The key.</value>
        [JsonProperty("catalogid")]
        public string CatalogValueId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên của CatalogItem
        /// </summary>
        /// <value>The value.</value>
        [JsonProperty("catalogname")]
        public string Value { get; set; }

        /// <summary>
        /// Khởi tạo <see cref="JsCatalogItem"/>
        /// </summary>
        public JsCatalogItem()
        {
            CatalogValueId = new Guid().ToString("N");
            Value = "";
        }
    }
}
