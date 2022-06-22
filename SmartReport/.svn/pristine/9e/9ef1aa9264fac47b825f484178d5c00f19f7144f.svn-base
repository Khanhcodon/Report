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
    /// Interface : IDocumentDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 140912
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Document trong CSDL
    /// </summary>
    public interface IDocumentDal
    {
        /// <summary>
        /// Lấy ra tất cả các văn bản, hồ sơ phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các văn bản, hồ sơ
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các người dùng</returns>
        IEnumerable<Document> Gets(Expression<Func<Document, bool>> spec = null,
                                   Func<IQueryable<Document>, IQueryable<Document>> preFilter = null,
                                   params Func<IQueryable<Document>, IQueryable<Document>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các văn bản, hồ sơ phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Document, TOutput>> projector,
                                           Expression<Func<Document, bool>> spec = null,
                                           Func<IQueryable<Document>, IQueryable<Document>> preFilter = null,
                                           params Func<IQueryable<Document>, IQueryable<Document>>[] postFilters);

        /// <summary>
        /// Lấy ra văn bản, hồ sơ theo id
        /// </summary>
        /// <param name="id">Id của văn bản, hồ sơ</param>
        /// <returns>Entity văn bản, hồ sơ</returns>
        Document Get(Guid id);

        /// <summary>
        /// Lấy ra văn bản, hồ sơ theo id
        /// </summary>
        /// <param name="categoryId">Id của thể loại văn bản</param>
        /// <returns>Entity văn bản, hồ sơ</returns>
        Document Get(int categoryId);

        /// <summary>
        /// Lấy ra văn bản, hồ sơ theo mã
        /// </summary>
        /// <param name="docCode">Mã văn bản, hồ sơ</param>
        /// <returns>Entity văn bản, hồ sơ</returns>
        Document Get(string docCode);

        /// <summary>
        /// Tạo mới văn bản, hồ sơ
        /// </summary>
        /// <param name="document">Entity văn bản hồ sơ</param>
        void Create(Document document);

        /// <summary>
        /// Cập nhật thông tin văn bản, hồ sơ
        /// </summary>
        /// <param name="document">Entity văn bản hồ sơ</param>
        void Update(Document document);

        /// <summary>
        /// Xóa văn bản, hồ sơ
        /// </summary>
        /// <param name="document">Entity văn bản, hồ sơ</param>
        void Delete(Document document);

        /// <summary>
        /// Kiểm tra sự tồn tại của văn bản, hồ sơ phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 văn bản hồ sơ phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Document, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Document, bool>> spec = null);

        /// <summary>
        /// Lấy ra tất cả các bản ghi
        /// </summary>
        /// <returns>Queryable</returns>
        IQueryable<Document> Raw();
    }
}
