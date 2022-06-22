using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bkav.eGovCloud.Entities.Customer.eDoc
{
    /// <summary>
    /// Thông tin tổ chức
    /// </summary>
    public class Organization
    {
        /// <summary>
        /// Mã định danh liên thông
        /// </summary>
        public string OrganId { get; set; }

        /// <summary>
        /// Tên tổ chức
        /// </summary>
        public string OrganName { get; set; }

    }
}
