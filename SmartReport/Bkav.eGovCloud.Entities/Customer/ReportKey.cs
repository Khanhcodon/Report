using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Quản lý các báo cáo
    /// </summary>
    public class ReportKey
    {
        //private List<IDictionary<string, int>> _listDepartmentPositionHasPermission;
        //private List<int> _listPositionHasPermission;
        //private List<int> _listUserHasPermission;

        /// <summary>
        /// Key
        /// </summary>
        public int ReportKeyId { get; set; }


        /// <summary>
        /// Lấy hoặc thiết lập mô tả báo cáo
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết  lập tên báo cáo
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục cha của báo cáo
        /// </summary>
        public int? ParentId { get; set; }


        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn dữ liệu cho báo cáo
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái sử dụng báo cáo.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập mã key
        /// </summary>
        public string Code { get; set; }
    }
}
