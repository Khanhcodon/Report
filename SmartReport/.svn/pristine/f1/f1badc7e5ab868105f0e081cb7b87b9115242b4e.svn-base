using System;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;
using System.Collections.Generic;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IAddressDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 15082013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng Address trong CSDL
    /// </summary>
    public interface IAddressDal
    {
        /// <summary>
        /// Thêm cơ quan ngoài
        /// </summary>
        /// <param name="address">đối tượng cơ quan ngoài</param>
        void Create(Address address);

        /// <summary>
        /// Xóa cơ quan ngoài
        /// </summary>
        /// <param name="address">đối tượng cơ quan ngoài</param>
        void Delete(Address address);

        /// <summary>
        /// Thay đổi thông tin cơ quan ngoài
        /// </summary>
        /// <param name="address">đối tượng cơ quan ngoài</param>
        void Update(Address address);

        /// <summary>
        /// Trả về đối tượng cơ quan ngoài theo Id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>Cơ quan ngoài tương ứng</returns>
        Address Get(int id);

        /// <summary> 
        /// <para>Trả về danh sách tất cả các đối tượng cơ quan ngoài. </para>
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các đối tượng cơ quan ngoài</returns>
        IEnumerable<Address> Gets(Expression<Func<Address, bool>> spec = null,
                                    Func<IQueryable<Address>, IQueryable<Address>> preFilter = null,
                                    params Func<IQueryable<Address>, IQueryable<Address>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các cơ quan phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Address, TOutput>> projector,
                                           Expression<Func<Address, bool>> spec = null,
                                           Func<IQueryable<Address>, IQueryable<Address>> preFilter = null,
                                           params Func<IQueryable<Address>, IQueryable<Address>>[] postFilters);

    }
}
