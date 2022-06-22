using Bkav.eGovCloud.Entities.Enum;

namespace Bkav.eGovCloud.Models
{
    /// <summary>
    /// Model search in
    /// </summary>
    public class PrintSearchModel
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        public PrintSearchModel()
        {
            ProcessType = DocumentProcessType.TiepNhan;
        }

        /// <summary>
        /// Loại xử lý với hồ sơ
        /// </summary>
        public DocumentProcessType ProcessType { get; set; }

        /// <summary>
        /// Thời gian lấy: 0 (cả ngày); 1 (30 phút); 2 (1 tiếng); 3(2 tiếng); 4 (buổi sáng); 5 (buổi chiều)
        /// </summary>
        public DailyProcessTimeForSearch Time { get; set; }

        /// <summary>
        /// Get or set số lượng hồ sơ gần nhất
        /// </summary>
        public int DocCount { get; set; }
    }
}