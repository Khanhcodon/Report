using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Đổi tượng report
    /// </summary>
    public class ReportObject
    {	
        /// <summary>
        /// Khởi tạo
        /// </summary>
        public ReportObject()
        {
            GroupDatas = new Dictionary<string, IEnumerable<EReport.Entity.GroupData>>();
        }

        /// <summary>
        /// Report Id
        /// </summary>
        public int ReportId { get; set; }

        /// <summary>
        /// Tên báo cáo
        /// </summary>
        public string ReportName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Cấu hình các cột hiển thị
        /// </summary>
        public DocColumnSetting ColumnSettings { get; set; }

        /// <summary>
        /// Danh sách các nhóm 
        /// </summary>
        public Dictionary<string, string> Groups { get; set; }

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

        public string ColumnConfig { get; set; }

        public bool isHCMC { get; set; }

        public int isFile { get; set; }

    }
}
