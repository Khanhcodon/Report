using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Đối tượng cache hồ sơ cá nhân
    /// </summary>
    [Serializable]
    public class StorePrivateCached
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ cá nhân
        /// </summary>
        public int StorePrivateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên hồ sơ cá nhân
        /// </summary>
        public string StorePrivateName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hồ sơ cá nhân cha
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả hồ sơ cá nhân
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo
        /// </summary>
        public int CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày tạo
        /// </summary>
        public DateTime CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị chỉ ra hồ sơ đã bị đóng
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị chỉ ra hồ sơ ở cấp mấy
        /// </summary>
        public byte? Level { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trường id mở rộng
        /// </summary>
        public string StorePrivateIdExt { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool HasShared { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<int> UserIdJoined { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<int> DeptIdJoined { get; set; }
    }
}
