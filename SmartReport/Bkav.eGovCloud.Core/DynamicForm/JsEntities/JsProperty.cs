namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : JsProperty - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Đối tượng một Property của control trên form động.</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class JsProperty
    {
        /// <summary>
        /// Khởi tạo.
        /// </summary>
        public JsProperty()
        {
            Id = 0;
            Value = "";
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id của thuộc tính control.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của thuộc tính control.
        /// </summary>
        public string Value { get; set; }
    }
}