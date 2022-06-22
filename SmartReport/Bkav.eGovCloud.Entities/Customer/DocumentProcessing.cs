using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bảng tra cứu tiến độ xử lý hồ sơ
    /// </summary>
    public class DocumentProcessing
    {
        /// <summary>
        /// Danh sách người giữ
        /// </summary>
        public IEnumerable<UserProcessing> Users { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsReturned { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsSupplemented { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DateAppoint { get; set; }

        /// <summary>
        /// Trạng thái xử lý
        /// </summary>
        public int Status { get; set; }

    }

    /// <summary>
    /// Người giữ
    /// </summary>
    public class UserProcessing
    {
        /// <summary>
        /// Id DocumentCopy
        /// </summary>
        public int DocumentCopyId { get; set; }

        /// <summary>
        /// Người giữ
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Phòng ban giữ
        /// </summary>
        public IEnumerable<string> Departments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CommentDate { get; set; }
    }
}
