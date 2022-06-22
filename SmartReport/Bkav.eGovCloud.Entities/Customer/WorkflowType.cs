using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Loại quy trình
    /// </summary>
    public class WorkflowType
    {
        /// <summary>
        /// Lấy hoặc thiết lập id loại quy trình
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên loại quy trình
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập hạn giữ của quy trình
        /// </summary>
        public int ExpireProcess { get; set; }
    }
}
