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
    /// Interface : IFormGroupDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 011013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng DocTypeForm trong CSDL
    /// </summary>
    public interface IDocTypeFormDal
    {
        /// <summary>
        /// Lấy ra tất cả các nhóm biểu mẫu phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các nhảy số
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các nhảy số</returns>
        IEnumerable<DocTypeForm> Gets(Expression<Func<DocTypeForm, bool>> spec = null,
                                    Func<IQueryable<DocTypeForm>, IQueryable<DocTypeForm>> preFilter = null,
                                    params Func<IQueryable<DocTypeForm>, IQueryable<DocTypeForm>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các nhóm biểu mẫu phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<DocTypeForm, TOutput>> projector,
                                           Expression<Func<DocTypeForm, bool>> spec = null,
                                           Func<IQueryable<DocTypeForm>, IQueryable<DocTypeForm>> preFilter = null,
                                           params Func<IQueryable<DocTypeForm>, IQueryable<DocTypeForm>>[] postFilters);

        /// <summary>
        /// Tạo mới nhóm biểu mẫu
        /// </summary>
        /// <param name="formGroup">Entity nhóm biểu mẫu</param>
        void Create(DocTypeForm formGroup);
                       
        /// <summary>
        /// Xóa nhảy số
        /// </summary>
        /// <param name="formGroup">Entity nhóm biểu mẫu</param>
        void Delete(DocTypeForm formGroup);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="formGroup"></param>
        void Update(DocTypeForm formGroup);

        /// <summary>
        /// Trả về 1 doctype form theo id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        DocTypeForm Get(int id);
    }
}
