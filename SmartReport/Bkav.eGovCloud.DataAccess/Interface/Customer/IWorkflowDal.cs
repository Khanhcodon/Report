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
    /// Interface : IWorkflowDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 241012
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng Workflow trong CSDL.Bảng lưu trữ luồng công văn của từng loại văn bản(hồ sơ).
    /// </summary>
    public interface IWorkflowDal
    {
        /// <summary>
        /// Lấy ra tất cả các luồng văn bản(hồ sơ) phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các luồng văn bản(hồ sơ)
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các luồng văn bản(hồ sơ)</returns>
        IEnumerable<Workflow> Gets(Expression<Func<Workflow, bool>> spec = null);

        /// <summary>
        /// Lấy ra tất cả các luồng văn bản(hồ sơ) phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Workflow, TOutput>> projector,
                                           Expression<Func<Workflow, bool>> spec = null);

        /// <summary>
        /// Lấy ra luồng văn bản(hồ sơ) theo id
        /// </summary>
        /// <param name="id">Id của luồng văn bản(hồ sơ)</param>
        /// <returns>Entity luồng văn bản(hồ sơ)</returns>
        Workflow Get(int id);

        /// <summary>
        /// Tạo mới luồng văn bản(hồ sơ)
        /// </summary>
        /// <param name="workflow">Entity luồng văn bản(hồ sơ)</param>
        void Create(Workflow workflow);

        /// <summary>
        /// Cập nhật thông tin luồng văn bản(hồ sơ)
        /// </summary>
        /// <param name="workflow">Entity luồng văn bản(hồ sơ)</param>
        void Update(Workflow workflow);

        /// <summary>
        /// Xóa luồng văn bản(hồ sơ)
        /// </summary>
        /// <param name="workflow">Entity luồng văn bản(hồ sơ)</param>
        void Delete(Workflow workflow);

        /// <summary>
        /// Kiểm tra sự tồn tại của luồng văn bản(hồ sơ) phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 luồng văn bản(hồ sơ) phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Workflow, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Workflow, bool>> spec = null);
    }
}
