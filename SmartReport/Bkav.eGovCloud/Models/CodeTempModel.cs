using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Models
{
    public class CodeTempModel
    {
        /// <summary>
        /// Key dùng để xóa
        /// </summary>
        public int CodeTempId { get; set; }
        /// <summary>
        /// Mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Full name người cấp code
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public string DateCreated { get; set; }
    }
}