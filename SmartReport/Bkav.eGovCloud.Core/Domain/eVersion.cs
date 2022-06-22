using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Core
{
    /// <summary>
    /// Đại diện cho phiên bản của eGov
    /// </summary>
    public class eVersion
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="version"></param>
        /// <param name="updates"></param>
        /// <param name="description"></param>
        public eVersion(string version, List<string> updates = null, string description = "")
        {
            Version = new Version(version);
            ListUpdate = updates;
            Description = description;
        }

        /// <summary>
        /// Lấy hoặc thiết lập phiên bản.
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mô tả của phiên bản.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các sql update phiên bản.
        /// </summary>
        public List<string> ListUpdate { get; set; }
    }
}
