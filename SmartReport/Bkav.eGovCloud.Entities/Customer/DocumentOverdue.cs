using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Lấy các thông tin về văn bản quá hạn
    /// </summary>
    public class DocumentOverdue
    {
        /// <summary>
        /// DocumentCopyId
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        ///  Người đang giữ văn bản
        /// </summary>
        public int UserCurrentId { get; set; }

        /// <summary>
        /// Người đang giữ văn bản
        /// </summary>
        public string UserCurrentName { get; set; }

        /// <summary>
        /// Phòng ban hiện tại
        /// </summary>
        public string CurrentDepartmentName { get; set; }

        /// <summary>
        /// Ngày nhận
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Ngày xử lý
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời hạn xử lý trên node hiện tại (theo giờ)
        /// </summary>
        public int TimeInNode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập hạn xử lý mà người giao đặt cho người nhận, nó khác với hạn giữ trong node
        /// </summary>
        public DateTime? DateOverdue { get; set; }

        /// <summary>
        /// Người tạo văn bản
        /// </summary>
        public string UserCreatedName { get; set; }

        /// <summary>
        /// Người tạo văn bản
        /// </summary>
        public int UserCreatedId { get; set; }

        /// <summary>
        /// Nghiệp vụ
        /// </summary>
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// Hạn xử lý
        /// </summary>
        public string DateAppointed { get; set; }

        /// <summary>
        /// Loại văn bản
        /// </summary>
        public Guid DoctypeId { get; set; }

        /// <summary>
        /// Tên loại văn bản
        /// </summary>
        public string DocTypeName { get; set; }

        /// <summary>
        /// Hình thức văn bản
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Số ký hiệu
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Số đến
        /// </summary>
        public string InOutCode { get; set; }

        /// <summary>
        /// Là quá hạn theo node
        /// </summary>
        public bool IsOverdue { get; set; }

        /// <summary>
        /// Tên công dân
        /// </summary>
        public string CitizenName { get; set; }

        /// <summary>
        /// Ngày xử lý
        /// </summary>
        public string DateFinished { get; set; }

        /// <summary>
        /// Ngày duyệt
        /// </summary>
        public string DateSuccess { get; set; }

        /// <summary>
        /// Ngày duyệt
        /// </summary>
        public DateTime? DatePublished { get; set; }

        /// <summary>
        /// Trạng thái đang xử lý hay dừng xử lý
        /// </summary>
        public bool IsWorkingTime { get; set; }

        /// <summary>
        /// Quy trình
        /// </summary>
        public int WorkflowId { get; set; }

        /// <summary>
        /// Node giữ
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// Ngày tiếp nhận
        /// </summary>
        public string DateCreated { get; set; }

        /// <summary>
        /// Vết xử lý
        /// </summary>
        public string Traces { get; set; }

        /// <summary>
        /// Hanj xuwr lys
        /// </summary>
        public int Deadline { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StatusName
        {
            get; set;
          
        }
        /// <summary>
        /// 
        /// </summary>
        public string StatusDXLName
        {
            get; set;
        }
        /// <summary>
        /// Tên công dân
        /// </summary>
        public string KyBaoCao { get; set; }
    }

    /// <summary>
    /// Chi tiết giám sát văn bản, hs quá hạn
    /// </summary>
    public class DocumentOverdueDetail
    {
        /// <summary>
        /// Tên loại văn bản
        /// </summary>
        public string DocTypeName { get; set; }

        /// <summary>
        /// Hình thức văn bản
        /// </summary> 
        public string DocCode { get; set; }

        /// <summary>
        /// Tên công dân
        /// </summary>
        public string CitizenName { get; set; }

        /// <summary>
        /// Danh sách các vết xử lý
        /// </summary>
        public IEnumerable<DocumentOverdue> ListTimeLine { get; set; }
    }
}
