using System;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;

namespace Bkav.eGovCloud.Models
{
    public class DailyProcessModel
    {
        /// <summary>
        /// Key
        /// </summary>
        public int DailyProcessId { get; set; }

        /// <summary>
        /// Get or set document copy id
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Get or set document id
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Get or set kiểu xử lý hồ sơ:
        /// <para> - 1: Tiếp nhận</para>
        /// <para> - 2: Bàn giao</para>
        /// <para> - 3: Ký duyệt</para>
        /// <para> - 4: Trả kết quả</para>
        /// <para> - 5: Tiếp nhận bổ sung</para>
        /// </summary>
        public int ProcessType { get; set; }

        /// <summary>
        /// Get or set date created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Get or set Tên công dân: lưu thừa để lấy dữ liệu 
        /// </summary>
        public string CitizenName { get; set; }

        /// <summary>
        /// Get or set danh sách người nhận bàn giao: lưu thừa để lấy dữ liệu
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// Get or set note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Get document
        /// </summary>
        public virtual Document Document { get; set; }

        /// <summary>
        /// Get process type in enum
        /// </summary>
        public DocumentProcessType ProcessTypeEnum
        {
            get
            {
                return (DocumentProcessType)ProcessType;
            }
        }
    }
}