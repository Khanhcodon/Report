namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// QuangP
    /// Class tạm phục vụ cho việc chuyển file từ hệ thống khác sang (VD: Chuyển file đính kèm từ mail sang)
    /// </summary>
    public class FileTemp
    {
        /// <summary>
        /// Tên file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Link download
        /// </summary>
        public string Url { get; set; }
    }
}