namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bảng liên kết giữa bộ lọc và cây văn bản
    /// </summary>
    public class ProcessFunctionAndFilter
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id
        /// </summary>
        public int ProcessFunctionAndFilterId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id bộ lọc
        /// </summary>
        public int ProcessFunctionFilterId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id function
        /// </summary>
        public int ProcessFunctionId { get; set; }
    }
}
