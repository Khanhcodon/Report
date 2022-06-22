using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Đổi tượng report
    /// </summary>
    public class ReportKeyObject
    {

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public ReportKeyObject()
        {
        }

        /// <summary>
        /// Report Id
        /// </summary>
        public int ReportKeyId { get; set; }

        /// <summary>
        /// Tên báo cáo
        /// </summary>
        public string ReportKeyName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Danh sách group đã get dữ liệu
        /// </summary>
        public Dictionary<string, IEnumerable<Bkav.EReport.Entity.GroupData>> GroupDatas { get; set; }

        /// <summary>
        /// Số dòng dữ liệu
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Dữ liệu báo cáo toàn bộ
        /// </summary>
        public IEnumerable<IDictionary<string, object>> Data { get; set; }

        /// <summary>
        /// Dữ liệu báo cáo đang trích suất: theo nhóm, ko theo nhóm, ....
        /// </summary>
        public IEnumerable<IDictionary<string, object>> Model { get; set; }

        /// <summary>
        /// Cache key
        /// </summary>
        public string CacheKey { get; set; }

        /// <summary>
        /// Thời điểm cập nhật cache
        /// </summary>
        public DateTime LastUpdate { get; set; }


        public string Header { get; set; }

        public string Footer { get; set; }

        public string PivotConfig { get; set; }
    }
}
