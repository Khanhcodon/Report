using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class LienThongDto
    {
        /// <summary>
        /// Lấy hoặc thiết lập DocumentCopyID
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên công dân
        /// </summary>
        public string CitizenName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã hồ sơ, số ký hiệu
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cơ quan nhận
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày liên thông
        /// </summary>
        public string DatePublished { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày phản hòio
        /// </summary>
        public string DateAppointed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị xác định hồ sơ đã dc phản hồi chưa
        /// </summary>
        public bool IsResponsed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày phản hồi
        /// </summary>
        public string DateResponsed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ghi chú: trạng thái
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các liên thông con: 1 gửi liên thông cho nhiều
        /// </summary>
        public List<LienThongDto> Children { get; set; }
    }
}