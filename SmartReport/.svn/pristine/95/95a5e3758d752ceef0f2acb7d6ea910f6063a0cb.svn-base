using Bkav.eGovCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CategoryCached
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id thể loại văn bản, hồ sơ
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên thể loại văn bản, hồ sơ
        /// </summary>
        public string CategoryName { get; set; }

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
    }
}
