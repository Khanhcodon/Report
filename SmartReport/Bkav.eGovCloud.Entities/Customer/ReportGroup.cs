namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Quản trị các kiểu báo cáo nhóm.
    /// </summary>
    public class ReportGroup
    {
        /// <summary>
        /// Key
        /// </summary>
        public int ReportGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên báo cáo nhóm
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn lấy dữ liệu bind lên tree
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên trường dữ liệu hiển thị
        /// </summary>
        public string FieldDisplay { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên trường dữ liệu để so sánh
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập lấy cho báo cáo hay thống kê
        /// </summary>
        public bool IsReport { get; set; }
    }
}
