using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IDomainDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Domain trong CSDL
    /// </summary>
    public interface IDomainDal
    {
        /// <summary>
        /// Lấy ra tất cả các domain phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các domain
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách domain</returns>
        IEnumerable<Domain> Gets(Expression<Func<Domain, bool>> spec = null);

        /// <summary>
        /// Lấy ra tất cả các domain phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Domain, TOutput>> projector,
                                           Expression<Func<Domain, bool>> spec = null,
                                           Func<IQueryable<Domain>, IQueryable<Domain>> preFilter = null,
                                           params Func<IQueryable<Domain>, IQueryable<Domain>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các domain phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các domain. Kết quả sẽ được ánh xạ sang 1 dạng khác bằng cách sử dụng 1 mapper do người dùng tự định nghĩa
        /// </summary>
        /// <remarks>
        /// Nên sử dụng hàm này để lấy ra những thuộc tính cần thiết để tránh việc lấy ra quá nhiều dữ liệu thừa
        /// </remarks>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <typeparam name="TOutput">Kiểu dữ liệu được ánh xạ</typeparam>
        /// <returns>Danh sách kiểu dữ liệu được ánh xạ</returns>
        IEnumerable<TOutput> Gets<TOutput>(Expression<Func<Domain, TOutput>> projector,
                              Expression<Func<Domain, bool>> spec);

        /// <summary>
        /// Lấy ra domain theo id
        /// </summary>
        /// <param name="id">Id của domain</param>
        /// <returns>Entity domain</returns>
        Domain Get(int id);

        /// <summary>
        /// Tạo mới domain
        /// </summary>
        /// <param name="domain">Entity domain</param>
        /// <param name="user">Entity người dùng</param>
        /// <param name="connection">Connection tới db khách hàng</param>
        void Create(Domain domain, User user, DbConnection connection);

        /// <summary>
        /// Cập nhật thông tin domain
        /// </summary>
        /// <param name="domain"></param>
        void Update(Domain domain);

        /// <summary>
        /// Kiểm tra sự tồn tại của domain phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 domain phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Domain, bool>> spec);

        /// <summary>
        /// Kiểm tra connection xem có chính xác hay không
        /// </summary>
        /// <param name="connection">Entity connection</param>
        /// <returns>DbConnection nếu connection là chính xác, ngược lại sẽ trả ra null</returns>
        DbConnection TestConnection(Connection connection);
    }
}
