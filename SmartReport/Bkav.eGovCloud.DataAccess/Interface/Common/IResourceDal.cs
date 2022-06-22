using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.DataAccess.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IResourceDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Resource trong CSDL
    /// </summary>
    public interface IResourceDal
    {
        /// <summary>
        /// Lấy ra tất cả các tài nguyên phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các tài nguyên
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các tài nguyên</returns>
        IEnumerable<Resource> Gets(Expression<Func<Resource, bool>> spec = null,
                                    Func<IQueryable<Resource>, IQueryable<Resource>> preFilter = null,
                                    params Func<IQueryable<Resource>, IQueryable<Resource>>[] postFilters);

        /// <summary>
        /// Lấy ra tài nguyên theo id
        /// </summary>
        /// <param name="id">Id của tài nguyên</param>
        /// <returns>Entity tài nguyên</returns>
        Resource Get(int id);

        /// <summary>
        /// Lấy ra tài nguyên theo key
        /// </summary>
        /// <param name="resourceKey">Key của tài nguyên</param>
        /// <returns>Entity tài nguyên</returns>
        Resource Get(string resourceKey);

        /// <summary>
        /// Tạo mới tài nguyên
        /// </summary>
        /// <param name="resource">Entity tài nguyên</param>
        void Create(Resource resource);

        /// <summary>
        /// Tạo mới nhiều tài nguyên
        /// </summary>
        /// <param name="resources">List các tài nguyên</param>
        void Create(IEnumerable<Resource> resources);

        /// <summary>
        /// Cập nhật thông tin tài nguyên
        /// </summary>
        /// <param name="resource">Entity tài nguyên</param>
        void Update(Resource resource);

        /// <summary>
        /// Xóa tài nguyên
        /// </summary>
        /// <param name="resource">Entity tài nguyên</param>
        void Delete(Resource resource);

        /// <summary>
        /// Kiểm tra sự tồn tại của tài nguyên phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 tài nguyên phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Resource, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Resource, bool>> spec = null);
    }
}
