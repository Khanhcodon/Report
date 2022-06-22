using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Business.Objects.CacheObjects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class StoreCached
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id tập hồ sơ, kho hồ sơ
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên tập hồ sơ, kho hồ sơ
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người phụ trách
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Phòng ban phụ trách
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>CuongNT@bkav.com - 120413</para>
        /// </summary>
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>CuongNT@bkav.com - 120413</para>
        /// </summary>
        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id người xem
        /// </summary>
        public List<int> ListUserViewIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id của mẫu sổ hồ sơ
        /// </summary>
        public IEnumerable<int> CodeIds { get; set; }
    }
}
