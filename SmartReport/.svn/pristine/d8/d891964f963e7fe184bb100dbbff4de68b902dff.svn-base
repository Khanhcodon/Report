using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Đánh dấu vi phạm
    /// </summary>
    public class CheckInfringe
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public int CheckInfringeId { get; set; }

        /// <summary>
        /// Ngày vi phạm
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Nội dung vi phạm
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// Id User vi phạm
        /// </summary>
        public int InfringeUserId { get; set; }

        /// <summary>
        /// Id người lập vi phạm
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// ID tiêu chí vi phạm
        /// </summary>
        public int RateEmployeeId { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public bool IsActived { get; set; }

        /// <summary>
        /// Lý do
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual RateEmployee RateEmployee { get; set; }
    }
}
