using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class LevelModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập key cấp hành chính
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên cấp hành chính
        /// </summary>
        public string Name { get; set; }

        ///// <summary>
        ///// Lấy  hoặc thiết lập rằng buộc Office 
        ///// </summary>
        //public ICollection<Office> Office { get; set; }

        ///// <summary>
        ///// Lấy  hoặc thiết lập rằng buộc loại thủ tục hành chính 
        ///// </summary>
        //public ICollection<DocType> DocType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kiểu cấp hành chính
        /// </summary>
        public int Type { get; set; }
    }
}