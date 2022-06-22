using System.Linq;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// BSO  - Phòng 2 - eGov
    /// Project: eGov Cloud - v1.0
    /// [Access Level(Interface)] : IDocumentCopyDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit   : [Class Name] 
    ///     * Implement : [Inteface Name], [Inteface Name], ...
    /// 
    /// Create Date : 121225
    /// Author      : TienBV
    /// Description : Quản lý lưu vệt documment: là các hướng xử lý của một document
    /// </summary>
    public interface IDocumentCopyDal
    {
        /// <summary> TienBV 121225
        /// <para> Lấy documentcopy theo điều kiện kỹ thuật truyền vào</para>
        /// <para> (Tienbv@bkav.com 251212)</para>
        /// </summary>
        /// <param name="spec">Điều kiện kỹ thuật</param>
        /// <returns>Document copy tương ứng</returns>
        DocumentCopy Get(Expression<Func<DocumentCopy, bool>> spec);

        /// <summary>
        /// <para> Lấy documentcopy theo điều kiện kỹ thuật truyền vào Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp</para>
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện kỹ thuật</param>
        /// <returns>Document copy tương ứng</returns>
        T GetAs<T>(Expression<Func<DocumentCopy, T>> projector, Expression<Func<DocumentCopy, bool>> spec);

        /// <summary>
        /// <para> Lấy một vệt document.</para>
        /// <para> (Tienbv@bkav.com 251212)</para>
        /// </summary>
        /// <param name="id">The document copy id.</param>
        /// <returns>Document copy object</returns>
        DocumentCopy Get(int id);

        /// <summary> GiangPN 07022013
        /// <para> Lấy ra tất cả các loại hồ sơ phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..</para>
        /// <para> (GiangPN@bkav.com 070213)</para>
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<DocumentCopy, TOutput>> projector, Expression<Func<DocumentCopy, bool>> spec = null, Func<IQueryable<DocumentCopy>, IQueryable<DocumentCopy>> preFilter = null, params Func<IQueryable<DocumentCopy>, IQueryable<DocumentCopy>>[] postFilters);

        /// <summary>
        /// <para> Tạo mới bản sao văn bản</para>
        /// <para> (Tienbv@bkav.com 251212)</para>
        /// </summary>
        /// <param name="documentCopy">Entity bản sao văn bản</param>
        void Create(DocumentCopy documentCopy);

        /// <summary>
        /// <para> Xóa 1 vệt văn bản</para>
        /// <para> (Tienbv@bkav.com 251212)</para>
        /// </summary>
        /// <param name="documentCopy">Vệt văn bản cần xóa</param>
        void Delete(DocumentCopy documentCopy);

        /// <summary>
        /// <para> Xóa nhiều vệt văn bản</para>
        /// <para> (Tienbv@bkav.com 251212)</para>
        /// </summary>
        /// <param name="documentCopys">Danh sách các vệt văn bản cần xóa</param>
        void Delete(IList<DocumentCopy> documentCopys);

        /// <summary>
        /// <para> Cập nhật document copy</para>
        /// <para> (Tienbv@bkav.com 251212)</para>
        /// </summary>
        /// <param name="documentCopy">Document copy entity</param>
        void Update(DocumentCopy documentCopy);

        /// <summary> 
        /// <para> Lấy ra danh sách tất cả id của các vệt con của vệt hiện tại.</para>
        /// <para> (Tienbv@bkav.com 251212)</para>
        /// </summary>
        /// <param name="documentCopyId">The current document copy id.</param>
        /// <returns>Danh sách các document copy id.</returns>
        IEnumerable<int> GetChildIds(int documentCopyId);

        /// <summary> 
        /// <para> Lấy ra danh sách tất cả các vệt con của vệt hiện tại.</para>
        /// <para> (Tienbv@bkav.com 251212)</para>
        /// </summary>
        /// <param name="documentCopyId">The current document copy id.</param>
        /// <returns>Danh sách document copy tương ứng</returns>
        IEnumerable<DocumentCopy> GetChilds(int documentCopyId);

        /// <summary>
        /// <para> Lấy danh sách các vệt theo điều  kiện kỹ thuật truyền vào</para>
        /// <para> (Tienbv@bkav.com 251212)</para>
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns></returns>
        IEnumerable<DocumentCopy> Gets(Expression<Func<DocumentCopy, bool>> spec = null, Func<IQueryable<DocumentCopy>, IQueryable<DocumentCopy>> preFilter = null, params Func<IQueryable<DocumentCopy>, IQueryable<DocumentCopy>>[] postFilters);

        /// <summary>
        /// <para> Lấy ra tất cả các bản ghi</para>
        /// <para> (Tienbv@bkav.com 251212)</para>
        /// </summary>
        /// <returns>Queryable</returns>
        IQueryable<DocumentCopy> Raw();

        /// <summary>
        /// <para> Trả về hướng xử lý chính của văn bản hiện tại</para>
        /// <para> (Tienbv@bkav.com 010313)</para>
        /// </summary>
        /// <param name="docId">Document id</param>
        /// <returns>Document copy</returns>
        DocumentCopy GetMain(Guid docId);
    }
}
