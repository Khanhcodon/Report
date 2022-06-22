using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// HopCV:211215
    /// Cấu hình xuất lịch sử sao lưu, phục hồi ra tệp
    /// </summary>
    public class HistoryFile
    {
        /// <summary>
        ///  Lấy hoặc thiết lập ngày tạo
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        ///  Lấy hoặc thiết lập nơi lưu trữ file
        /// </summary>
        public int FileLocationId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên thư mục gốc chứa file
        /// </summary>
        public string FileLocationKey { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên thư mục tự tăng chứa file
        /// </summary>
        public string IdentityFolder { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên thư mục gốc chứa file
        /// </summary>
        public string RealFileName { get; set; }
    }
}
