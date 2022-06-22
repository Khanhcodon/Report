using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Api.Dto
{
    public class DepartmentApi
    {
        /// <summary>
        /// Tên công dân
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Địa chỉ công dân
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Số CMT
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Thư điện tử
        /// </summary>
        public string Email { get; set; }
    }
}