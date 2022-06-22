namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PropertyValue - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Danh sách các giá trị của thuộc tính mẫu của control mẫu trong form động.</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class PropertyValue
    {
        /// <summary>
        /// Khởi tạo <see cref="PropertyValue"/>
        /// </summary>
        public PropertyValue()
        {
            Val = "";
            UIVal = "";
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id tự tăng trong bảng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id của Property
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giá trị của Property
        /// </summary>
        public string Val { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên thể hiện trên giao diện của giá trị
        /// </summary>
        /// <example>
        /// Val: normal.
        /// UIVal: Kiểu chữ thường.
        /// </example>
        public string UIVal { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Là giá trị mặc định của Property
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thứ tự hiển thị giá trị của Property
        /// </summary>
        public int Orders { get; set; }
    }
}