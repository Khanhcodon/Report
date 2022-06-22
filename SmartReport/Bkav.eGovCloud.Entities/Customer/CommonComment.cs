namespace Bkav.eGovCloud.Entities.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : CommonComment - public - Entity </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các ý kiến xử lý thường dùng </para>
    /// <para> ( TienBV@bkav.com - 200213) </para>
    /// </summary>
    public class CommonComment
    {
        /// <summary>
        /// Get or set key
        /// </summary>
        public int CommonCommentId { get; set; }

        /// <summary>
        /// Get or set Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Get or set key
        /// </summary>
        public int UserId { get; set; }
    }
}
