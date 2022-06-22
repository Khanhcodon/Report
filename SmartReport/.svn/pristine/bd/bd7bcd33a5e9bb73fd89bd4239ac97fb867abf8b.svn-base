using System.Collections.Generic;

namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : JsDocument - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Là đối tượng lưu trữ thông tin đã khai trên 1 biểu mẫu động <see cref="JsFormModel"/>.</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class JsDocument
    {
        /// <summary>
        /// Khởi tạo <see cref="JsDocument"/>
        /// </summary>
        public JsDocument()
        {
            DocFieldJson = new List<JsDocumentItem>();
        }

        /// <summary>
        /// FormId của mẫu form trong CtzFormTemplate.
        /// </summary>
        /// <value>The form id.</value>
        public string FormId { get; set; }

        /// <summary>
        /// Mô tả về biểu mẫu nếu có.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Danh sách các giá trị được nhập trên form động.
        /// </summary>
        /// <value>The doc field json.</value>
        public List<JsDocumentItem> DocFieldJson { get; set; }
    }
}
