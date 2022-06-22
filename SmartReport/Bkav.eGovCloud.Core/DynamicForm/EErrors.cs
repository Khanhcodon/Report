namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : EErrors - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Đối tượng chứa danh sách lỗi khi validate trên biểu mẫu động.</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class EErrors
    {
        #region Instance Properties

        /// <summary>
        /// Từ khóa lỗi.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Giá trị lỗi.
        /// </summary>
        public string Value { get; set; }

        #endregion
    }
}