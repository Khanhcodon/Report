using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : JsCatalog - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Danh mục catalog để load dữ liệu lên form động khi tiếp nhận hồ sơ.</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class JsCatalog
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [JsonProperty("Id")]
        public string CatalogId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("Name")]
        public string CatalogName { get; set; }

        /// <summary>
        /// Gets or sets the LST ext values.
        /// </summary>
        /// <remarks>
        /// Không được sửa thành LstItem vì dùng tương ứng với biến js
        /// </remarks>
        [JsonProperty("lstItem")]
        public List<JsCatalogItem> CatalogValues { get; set; }

        /// <summary>
        /// Khởi tạo <see cref="JsCatalog"/>
        /// </summary>
        public JsCatalog()
        {
            CatalogId = new Guid().ToString("N");
            CatalogName = "";
            CatalogValues = new List<JsCatalogItem>();
        }
    }
}
