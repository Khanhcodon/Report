using System;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class CatalogValueModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id giá trị danh mục
        /// </summary>
        public Guid CatalogValueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid CatalogId { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập Id danh mục liên quan đến giá trị
        ///// </summary>
        //public string CatalogGuidId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giá trị danh mục
        /// </summary>
        public string Value { get; set; }
        public string Catalogkey { get; set; }

        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        public int Order { get; set; }
    }
}