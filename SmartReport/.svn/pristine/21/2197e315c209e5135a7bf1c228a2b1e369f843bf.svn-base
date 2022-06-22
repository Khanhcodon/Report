using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IDepartmentDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Modify Date: 080912
    /// Modifier: GiangPN
    /// Description : DAL tương ứng với bảng Department trong CSDL
    /// </summary>
    public interface IDepartmentDal
    {
        /// <summary>
        /// Lấy ra tất cả các phòng ban phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các phòng ban
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các phòng ban</returns>
        IEnumerable<Department> Gets(Expression<Func<Department, bool>> spec = null,
                                    Func<IQueryable<Department>, IQueryable<Department>> preFilter = null,
                                    params Func<IQueryable<Department>, IQueryable<Department>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các phòng ban phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Department, TOutput>> projector,
                                           Expression<Func<Department, bool>> spec = null);

        /// <summary>
        /// Lấy ra phòng ban theo id
        /// </summary>
        /// <param name="id">Id của phòng ban</param>
        /// <returns>Entity phòng ban</returns>
        Department Get(int id);

        /// <summary>
        /// Lấy ra phòng ban cấp cao nhất
        /// </summary>
        /// <returns>Entity phòng ban</returns>
        Department GetRoot();

        /// <summary>
        /// Tạo mới phòng ban
        /// </summary>
        /// <param name="department">Entity phòng ban</param>
        void Create(Department department);

        /// <summary>
        /// Cập nhật thông tin phòng ban
        /// </summary>
        /// <param name="department">Entity phòng ban</param>
        void Update(Department department);

        /// <summary>
        /// Xóa phòng ban
        /// </summary>
        /// <param name="department">Entity phòng ban</param>
        void Delete(Department department);

        /// <summary>
        /// Xóa nhiều phòng ban
        /// </summary>
        /// <param name="departments">Danh sách phòng ban cần xóa</param>
        void Delete(IEnumerable<Department> departments);

        /// <summary>
        /// Kiểm tra sự tồn tại của phòng ban phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 phòng ban phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Department, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Department, bool>> spec = null);
    }
}
