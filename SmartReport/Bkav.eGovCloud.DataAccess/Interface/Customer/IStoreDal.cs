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
    /// Interface : IStoreDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Store trong CSDL
    /// </summary>
    public interface IStoreDal
    {
        /// <summary>
        /// Lấy ra danh sách sổ hồ sơ
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilter">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách sổ hồ sơ</returns>
        IEnumerable<Store> Gets(Expression<Func<Store, bool>> spec = null,
                                    Func<IQueryable<Store>, IQueryable<Store>> preFilter = null,
                                    params Func<IQueryable<Store>, IQueryable<Store>>[] postFilter);

        /// <summary>
        /// Lấy ra tất cả các sổ hồ sơ phù hợp với điều kiện truyền vào. Nếu điều kiện băng null thì sẽ lấy ra tất cả các sổ hồ sơ
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách sổ hồ sơ</returns>
        IEnumerable<Store> Gets(Expression<Func<Store, bool>> spec = null);

        /// <summary>
        /// Lấy ra tất cả các sổ hồ sơ phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Store, TOutput>> projector,
                                           Expression<Func<Store, bool>> spec = null);

        /// <summary>
        /// Lấy ra sổ hồ sơ theo id
        /// </summary>
        /// <param name="storeId">Id sổ hồ sơ</param>
        /// <returns>Entity sổ hồ sơ</returns>
        Store Get(int storeId);

        /// <summary>
        /// Tạo mới sổ hồ sơ
        /// </summary>
        /// <param name="store">Entity sổ hồ sơ</param>
        void Create(Store store);

        /// <summary>
        /// Cập nhật thông tin sổ hồ sơ
        /// </summary>
        /// <param name="store">Entity sổ hồ sơ</param>
        void Update(Store store);

        /// <summary>
        /// Xóa sổ hồ sơ
        /// </summary>
        /// <param name="store">Entity sổ hồ sơ</param>
        void Delete(Store store);

        /// <summary>
        /// Kiểm tra sự tồn tại của sổ hồ sơ phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 sổ hồ sơ phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Store, bool>> spec = null);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Store, bool>> spec = null);
    }
}
