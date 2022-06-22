using Bkav.eGovCloud.Business.Objects.StatisticXlvb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Tối tượng xử lý giám sát xử lý văn bản
    /// </summary>
    public class StatisticTimeline : StatisticsGroup
    {
        /// <summary>
        /// Document Id
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Document Copy Id
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Doctyuep Id
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Tên thủ tục, loại văn bản
        /// </summary>
        public string DocTypeName { get; set; }

        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime? DateFinished { get; set; }

        /// <summary>
        /// Trạng thái xử lys
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Người giữ
        /// </summary>
        public int UserCurrentId { get; set; }

        /// <summary>
        /// Category id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Hình thức văn bản
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Người xử lý
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Người xử lý
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Người xử lý
        /// </summary>
        public string UserFullName { get; set; }

        /// <summary>
        /// Phòng ban xử lý
        /// </summary>
        public string DepartmentExt { get; set; }

        /// <summary>
        /// Phòng ban xử lý
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Ngày nhận văn bản
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Ngày chuyển văn bản
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Hạn xử lý trên node
        /// </summary>
        public DateTime? DateOverdue { get; set; }

        /// <summary>
        /// Thời gian được phép xử lý trên node (giờ)
        /// </summary>
        public int TimeInNode { get; set; }

        /// <summary>
        /// Node id
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// Workflow id
        /// </summary>
        public int WorkflowId { get; set; }

        /// <summary>
        /// Trạng thái đang xử lý
        /// </summary>
        public bool IsWorkingTime { get; set; }

        /// <summary>
        /// Trạng thái quá hạn
        /// </summary>
        public bool IsOverdue { get; set; }

        /// <summary>
        /// Trạng thái xác định đã xử lý
        /// </summary>
        public bool IsProcess
        {
            get
            {
                return ToDate.HasValue;
            }
        }

        /// <summary>
        /// Phongf ban giu
        /// </summary>
        public string CurrentDepartmentExt { get; set; }
        
        /// <summary>
        /// Teen nguoi giu
        /// </summary>
        public string UserCurrentName { get; set; }
    }
}
