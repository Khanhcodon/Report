using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.DataAccess.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : ISettingDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 090812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Setting trong CSDL
    /// </summary>
    public interface ISettingDal
    {
        /// <summary>
        /// Lấy ra tất cả các cấu hình phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các cấu hình
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các cấu hình</returns>
        IEnumerable<Setting> Gets(Expression<Func<Setting, bool>> spec = null);

        /// <summary>
        /// Lấy ra cấu hình theo id
        /// </summary>
        /// <param name="id">Id của cấu hình</param>
        /// <returns>Entity cấu hình</returns>
        Setting Get(int id);

        /// <summary>
        /// Lấy ra cấu hình theo key
        /// </summary>
        /// <param name="resourceKey">Key của cấu hình</param>
        /// <returns>Entity cấu hình</returns>
        Setting Get(string resourceKey);

        /// <summary>
        /// Tạo mới cấu hình
        /// </summary>
        /// <param name="resource">Entity cấu hình</param>
        void Create(Setting resource);

        /// <summary>
        /// Cập nhật thông tin cấu hình
        /// </summary>
        /// <param name="resource">Entity cấu hình</param>
        void Update(Setting resource);

        /// <summary>
        /// Xóa cấu hình
        /// </summary>
        /// <param name="resource">Entity cấu hình</param>
        void Delete(Setting resource);

        /// <summary>
        /// Kiểm tra sự tồn tại của cấu hình phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 cấu hình phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Setting, bool>> spec);
    }
}
