using System.Collections.Generic;

namespace Bkav.eGovCloud.Api.Dto
{
    /// <summary>
    /// Văn bản quá hạn
    /// </summary>
    public class OverdueDocument
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
        /// Người đang giữ văn bản
        /// </summary>
        public string CurrentUser { get; set; }

        /// <summary>
        /// Bộ phận người đang giữ văn bản
        /// </summary>
        public List<string> CurrentDepartment { get; set; }

        /// <summary>
        /// Thời gian node đã giữ văn bản
        /// </summary>
        public int CurrentNodeKeepTime { get; set; }

        /// <summary>
        /// Thời gian node được giữ văn bản
        /// </summary> 
        public float CurrentNodePermitTime { get; set; }

        /// <summary>
        /// Tổng thời gian đã xử lý của văn bản
        /// </summary>
        public int TotalKeepTime { get; set; }

        /// <summary>
        /// Tỏng thời gian cho phép của cả văn bản
        /// </summary>
        public int TotalPermitTime { get; set; }

        /// <summary>
        /// Người tạo văn bản
        /// </summary>
        public string UserCreated { get; set; }
    }
}