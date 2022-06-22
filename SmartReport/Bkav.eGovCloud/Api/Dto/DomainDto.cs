using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Api
{
    public class DomainDto
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id domain
        /// </summary>
        public int DomainId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên domain
        /// </summary>
        public string DomainName { get; set; }
    }
}