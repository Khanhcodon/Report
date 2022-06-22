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
    /// Interface : IDocFieldDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Modify Date: 080912
    /// Modifier: GiangPN
    /// Description : DAL tương ứng với bảng DocField trong CSDL
    /// </summary>
    public interface IDocFieldDal
    {
        /// <summary>
        /// Lấy ra tất cả các lĩnh vục phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các lĩnh vực
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các lĩnh vực</returns>
        IEnumerable<DocField> Gets(Expression<Func<DocField, bool>> spec = null,
                                    Func<IQueryable<DocField>, IQueryable<DocField>> preFilter = null,
                                    params Func<IQueryable<DocField>, IQueryable<DocField>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các lĩnh vực. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <typeparam name="T">Kiểu đầu ra.</typeparam>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các lĩnh vực</returns>
        IEnumerable<T> GetsAs<T>(Expression<Func<DocField, T>> projector = null,
                                    Expression<Func<DocField, bool>> spec = null,
                                    Func<IQueryable<DocField>, IQueryable<DocField>> preFilter = null,
                                    params Func<IQueryable<DocField>, IQueryable<DocField>>[] postFilters);

        /// <summary>
        /// Lấy ra lĩnh vực theo id
        /// </summary>
        /// <param name="id">Id của lĩnh vực</param>
        /// <returns>Entity phòng ban</returns>
        DocField Get(int id);

        /// <summary>
        /// Tạo mới lĩnh vực
        /// </summary>
        /// <param name="docField">Entity lĩnh vực</param>
        void Create(DocField docField);

        /// <summary>
        /// Cập nhật thông tin lĩnh vực
        /// </summary>
        /// <param name="docField">Entity lĩnh vực</param>
        void Update(DocField docField);

        /// <summary>
        /// Xóa lĩnh vực
        /// </summary>
        /// <param name="docField">Entity lĩnh vực</param>
        void Delete(DocField docField);

        /// <summary>
        /// Xóa nhiều lĩnh vực
        /// </summary>
        /// <param name="docFields">Danh sách lĩnh vực cần xóa</param>
        void Delete(IList<DocField> docFields);

        /// <summary>
        /// Kiểm tra sự tồn tại của lĩnh vực phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 lĩnh vực phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<DocField, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<DocField, bool>> spec = null);
    }
}
