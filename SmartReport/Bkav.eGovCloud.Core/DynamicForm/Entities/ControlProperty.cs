namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ControlProperty - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT@bkav.com
    /// </author>
    /// <summary>
    /// <para>Đối tượng quan hệ giữa Control mẫu và các thuộc tính.</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class ControlProperty
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của control mẫu. Dropdownlist: 10, Textbox: 9, Label: 1.
        /// </summary>
        public int ControlId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của thuộc tính mẫu.
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thứ tự thuộc tính trong một control mẫu.
        /// </summary>
        public int Orders { get; set; }
    }
}