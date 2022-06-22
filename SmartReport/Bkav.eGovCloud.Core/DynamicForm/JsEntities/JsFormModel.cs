using System.Collections.Generic;

namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : JsFormModel - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Là đối tượng biểu mẫu động, lưu trữ đầy đủ cấu trúc của một biểu mẫu khai thông tin thực sự. </para>
    /// <para>Được sử dụng để parse ra Json và đẩy về client và hiển thị thành biểu mẫu để khai.</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class JsFormModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string FormId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MaxRow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<JsCatalog> JssCatalog { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<JsControl> JssForm { get; set; }
    }
}