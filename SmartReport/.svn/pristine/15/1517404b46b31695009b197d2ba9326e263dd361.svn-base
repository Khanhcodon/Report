using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Category - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Category trong CSDL. Bảng Category là bảng lưu trữ các thể loại văn bản.
    /// </summary>
    public class Category
    {
        private ICollection<CategoryCode> _categoryCodes;

        /// <summary>
        /// Lấy hoặc thiết lập Id thể loại văn bản, hồ sơ
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên thể loại văn bản, hồ sơ
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các mapping giữa Thể loại văn bản - mẫu văn bản
        /// </summary>
        public ICollection<CategoryCode> CategoryCodes
        {
            get { return _categoryCodes ?? (_categoryCodes = new List<CategoryCode>()); }
            set { _categoryCodes = value; }
        }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>GiangPN@bkav.com - 290513</para>
        /// </summary>
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>GiangPN@bkav.com - 290513</para>
        /// </summary>
        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }

        /// <summary>
        /// Get set codeids
        /// </summary>
        public List<int> CodeIds { get; set; }

        ///// <summary>
        ///// Get set codeids
        ///// </summary>
        //public List<bool> IsDefault { get; set; }
    }
}
