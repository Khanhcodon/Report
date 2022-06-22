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
    /// Description : DAL tương ứng với bảng FormGroup trong CSDL
    /// </summary>
    public interface IFormGroupDal
    {
        /// <summary>
        /// Lấy ra tất cả các nhóm biểu mẫu phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các nhảy số
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các nhảy số</returns>
        IEnumerable<FormGroup> Gets(Expression<Func<FormGroup, bool>> spec = null,
                                    Func<IQueryable<FormGroup>, IQueryable<FormGroup>> preFilter = null,
                                    params Func<IQueryable<FormGroup>, IQueryable<FormGroup>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các nhóm biểu mẫu phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="T">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các nhảy số</returns>
        IEnumerable<T> GetsAs<T>(Expression<Func<FormGroup, T>> projector, Expression<Func<FormGroup, bool>> spec = null);

        /// <summary>
        /// Lấy ra nhảy số theo id
        /// </summary>
        /// <param name="formGroupId">Id của nhóm biểu mẫu</param>
        /// <returns>Entity nhóm biểu mẫu</returns>
        FormGroup Get(int formGroupId);

        /// <summary>
        /// Tạo mới nhóm biểu mẫu
        /// </summary>
        /// <param name="formGroup">Entity nhóm biểu mẫu</param>
        void Create(FormGroup formGroup);
                       
        /// <summary>
        /// Xóa nhảy số
        /// </summary>
        /// <param name="formGroup">Entity nhóm biểu mẫu</param>
        void Delete(FormGroup formGroup);

        /// <summary>
        /// Kiểm tra sự tồn tại của nhóm biểu mẫu phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 nhóm biểu mẫu phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<FormGroup, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<FormGroup, bool>> spec = null);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="formGroup"></param>
        void Update(FormGroup formGroup);
    }
}
