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
    /// Interface : IPositionDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Modify Date: 050912
    /// Modifier: GiangPN
    /// Description : DAL tương ứng với bảng Position trong CSDL
    /// </summary>
    public interface IPositionDal
    {
        /// <summary>
        /// Lấy ra tất cả các chức vụ phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các chức vụ
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các chức vụ</returns>
        IEnumerable<Position> Gets(Expression<Func<Position, bool>> spec = null,
                                    Func<IQueryable<Position>, IQueryable<Position>> preFilter = null,
                                    params Func<IQueryable<Position>, IQueryable<Position>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các chức vụ phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Position, TOutput>> projector,
                                           Expression<Func<Position, bool>> spec = null);

        /// <summary>
        /// Lấy ra chức vụ theo id
        /// </summary>
        /// <param name="id">Id của chức vụ</param>
        /// <returns>Entity chức vụ</returns>
        Position Get(int id);

        /// <summary>
        /// Tạo mới chức vụ
        /// </summary>
        /// <param name="position">Entity chức vụ</param>
        void Create(Position position);

        /// <summary>
        /// Cập nhật thông tin chức vụ
        /// </summary>
        /// <param name="position">Entity chức vụ</param>
        void Update(Position position);

        /// <summary>
        /// Xóa chức vụ
        /// </summary>
        /// <param name="position">Entity chức vụ</param>
        void Delete(Position position);

        /// <summary>
        /// Xóa nhiều chức vụ
        /// </summary>
        /// <param name="positions">Danh sách chức vụ cần xóa</param>
        void Delete(IEnumerable<Position> positions);

        /// <summary>
        /// Kiểm tra sự tồn tại của chức vụ phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 chức vụ phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Position, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Position, bool>> spec = null);
    }
}
