using System;
using Bkav.eGovCloud.Entities.Enum;
using CrystalDecisions.Shared;

namespace Bkav.eGovCloud.Models
{
    public class ReportCriteriaModel
    {
        public int ReportId { get; set; }

        /// <summary>
        /// Thời điểm bắt đầu so sánh dữ liệu báo cáo
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Thời điểm kết thúc so sánh dữ liệu báo cáo
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Các mốc thời gian fix sẵn
        /// </summary>
        public DateTimeReport Time { get; set; }

        /// <summary>
        /// Nhóm báo cáo 
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Tên trường dữ liệu so sánh của node là nhóm được chọn trên cây báo cáo
        /// <para>
        /// Sử dụng cho các node nhóm trên cây báo cáo được sinh tự động theo cấu hình
        /// </para>
        /// </summary>
        public string TreeGroupName { get; set; }

        /// <summary>
        /// Giá trị nhóm trên cây báo cáo của node được chọn
        /// </summary>
        public string TreeGroupValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDesc { get; set; }

        /// <summary>
        /// Nhóm theo
        /// </summary>
        public string GroupBy { get; set; }

        /// <summary>
        /// Giá trị nhóm
        /// </summary>
        public string GroupValue { get; set; }

        /// <summary>
        /// Loaij file export
        /// </summary>
        public int ExportType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TimeKey { get; set; }

        public ExportFormatType ExportTypeEnum
        {
            get
            {
                return (ExportFormatType)ExportType;
            }
        }
    }
}