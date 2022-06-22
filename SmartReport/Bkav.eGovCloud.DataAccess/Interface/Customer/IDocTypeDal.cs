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
    /// Interface : IDocTypeDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DocType trong CSDL
    /// </summary>
    public interface IDocTypeDal
    {
        /// <summary>
        /// Lấy ra tất cả các loại hồ sơ phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các loại hồ sơ
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>danh sách loại hồ sơ</returns>
        IEnumerable<DocType> Gets(Expression<Func<DocType, bool>> spec = null,
                                    Func<IQueryable<DocType>, IQueryable<DocType>> preFilter = null,
                                    params Func<IQueryable<DocType>, IQueryable<DocType>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các loại hồ sơ phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<DocType, TOutput>> projector,
                                           Expression<Func<DocType, bool>> spec = null);

        /// <summary>
        /// Lấy ra loại hồ sơ theo id
        /// </summary>
        /// <param name="id">Id của loại hồ sơ</param>
        /// <returns>Entity loại hồ sơ</returns>
        DocType Get(Guid id);

        /// <summary>
        /// Lấy ra loại hồ sơ theo id
        /// </summary>
        /// <param name="categoryId">Id của thể loại hồ sơ</param>
        /// <returns>Entity loại hồ sơ</returns>
        DocType Get(int categoryId);

        /// <summary>
        /// Thêm mới loại hồ sơ.
        /// </summary>
        /// <param name="docType">Loại hồ sơ.</param>
        /// <returns><c>true</c> nếu thành công. <c>false</c> nếu lỗi.</returns>
        void Create(DocType docType);
        /// <summary>
        /// Cập nhật trạng thái hoạt động của loại hồ sơ.
        /// </summary>
        /// <param name="docType">Entity loại hồ sơ.</param>
        void Update(DocType docType);
        /// <summary>
        /// Xóa loại hồ sơ.
        /// </summary>
        /// <param name="docType">Entity loại hồ sơ</param>
        /// <returns></returns>
        void Delete(DocType docType);

        /// <summary>
        /// Kiểm tra sự tồn tại của loại hồ sơ phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 loại hồ sơ phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<DocType, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<DocType, bool>> spec = null);
    }
}
