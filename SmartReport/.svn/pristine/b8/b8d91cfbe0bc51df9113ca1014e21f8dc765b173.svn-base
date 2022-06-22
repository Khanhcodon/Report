using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : JsDocumentItem - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Là đối tượng lưu trữ dữ liệu đã nhập của một control <see cref="JsControl"/> trên biểu mẫu động <see cref="JsFormModel"/>.<br/>
    /// <see cref="JsDocument"/> chứa đối tượng này, lưu trữ toàn bộ thông tin đã nhập của một biểu mẫu động.</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class JsDocumentItem
    {
        /// <summary>
        /// Loại control: 9:Textbox, 10: dropdown.
        /// </summary>
        /// <value>The type id.</value>
        public int TypeId { get; set; }

        /// <summary>
        /// MetaId(ctzCatalogInDoc) hoặc ExtFieldId (ctzExtValue).
        /// </summary>
        /// <value>The control id.</value>
        public Guid ControlId { get; set; }

        /// <summary>
        /// Giá trị nhập liệu của control.
        /// CatalogId(ctzCatalogInDoc) hoặc NVarcharValue(ctzExtValue).
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }

        /// <summary>
        /// Danh sách các thuộc tính (Align, Color...).<para></para>
        /// </summary>
        /// <value>Danh sách các thuộc tính.</value>
        /// <remarks>
        /// 
        /// </remarks>
        public List<JsProperty> Properties { get; set; }

        /// <summary>
        /// GlobalCode của ExField hoặc CatalogMeta.
        /// </summary>
        /// <value>The global code.</value>
        public string GlobalCode { get; set; }

        /// <summary>
        /// <para>Chi tiết thông tin về CatalogValue được chọn. </para>
        /// <para>Lưu dạng json: {"catalogid":136,"catalogname":"Tiền mặt","globalcode":"E020341E-9EB9-4A18-B8C4-7926B1BF3D75"}</para>
        /// </summary>
        /// <value>The catalog selected.</value>
        public string CatalogSelected { get; set; }

        /// <summary>
        /// null: nếu CatalogSeleted is null or empty. Còn lại trả về Object.
        /// </summary>
        /// <value>The catalog selected object.</value>
        [JsonIgnore]
        public JsCatalogSelected CatalogSelectedObject
        {
            get
            {
                return string.IsNullOrEmpty(CatalogSelected) ? null : Json2.ParseAsJs<JsCatalogSelected>(CatalogSelected); //JsonConvert.DeserializeObject<JsCatalogSelected>(CatalogSelected, new JsonSerializerSettings { Culture = CultureInfo.InvariantCulture });
            }
        }

        /// <summary>
        /// Tên hiển thị ở giao diện.
        /// MetaName(ctzCatalogInDoc) hoặc ExtFieldName(ctzExtValue).
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Loại input control (dạng CMND, email, phone, ...).
        /// </summary>
        /// <value>The display name.</value>
        public string MaskType { get; set; }

        /// <summary>
        /// Control này có bắt buộc nhập hay không.
        /// </summary>
        /// <value>The display name.</value>
        public bool IsRequired { get; set; }
    }
}
