using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Interface : IProcessFunctionDal - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : DAL tương ứng với bảng ProcessFunction trong CSDL</para>
    /// </summary>
    public interface IProcessFunctionDal
    {
        /// <summary>
        /// Lấy ra tất cả các function phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các function
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các function</returns>
        IEnumerable<ProcessFunction> Gets(Expression<Func<ProcessFunction, bool>> spec = null,
                                    Func<IQueryable<ProcessFunction>, IQueryable<ProcessFunction>> preFilter = null,
                                    params Func<IQueryable<ProcessFunction>, IQueryable<ProcessFunction>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các function phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<ProcessFunction, TOutput>> projector,
                                           Expression<Func<ProcessFunction, bool>> spec = null);

        /// <summary>
        /// Lấy ra function theo id
        /// </summary>
        /// <param name="id">Id của function</param>
        /// <returns>Entity process function</returns>
        ProcessFunction Get(int id);

        /// <summary>
        /// Tạo mới function
        /// </summary>
        /// <param name="function">Entity process function</param>
        void Create(ProcessFunction function);

        /// <summary>
        /// Cập nhật thông tin function
        /// </summary>
        /// <param name="function">Entity process function</param>
        void Update(ProcessFunction function);

        /// <summary>
        /// Xóa function
        /// </summary>
        /// <param name="function">Entity function</param>
        void Delete(ProcessFunction function);

        /// <summary>
        /// Xóa nhiều function
        /// </summary>
        /// <param name="functions">Danh sách function cần xóa</param>
        void Delete(IEnumerable<ProcessFunction> functions);

        /// <summary>
        /// Kiểm tra sự tồn tại của function phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 function phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<ProcessFunction, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<ProcessFunction, bool>> spec = null);

        /// <summary>
        /// Lấy ra danh sách các văn bản hổ sơ theo đúng cấu hình của function
        /// </summary>
        /// <param name="function">Entity function</param>
        /// <param name="parameters">Các parameter</param>
        /// <returns>Danh sách các Dictionary, mỗi 1 dictionary là 1 row trong trong kết quả lấy được, dictionary có dạng: 'columnName:columnValue'</returns>
        IEnumerable<IDictionary<string, object>> GetDocumentLatestByFunction(ProcessFunction function, params object[] parameters);

        /// <summary>
        /// Lấy ra danh sách các DocumentCopyId bị xóa khỏi danh sách văn bản hổ sơ hiện tại
        /// </summary>
        /// <param name="function">Entity function</param>
        /// <param name="currentDocumentCopyIds">Các DocumentCopyId trên danh sách hiện tại</param>
        /// <param name="parameters">Các parameter</param>
        /// <returns>Danh sách các Dictionary, mỗi 1 dictionary là 1 row trong trong kết quả lấy được, dictionary có dạng: 'columnName:columnValue'</returns>
        IEnumerable<int> GetDocumentCopyIdsRemove(ProcessFunction function, IEnumerable<int> currentDocumentCopyIds, params object[] parameters);

        /// <summary>
        /// Lấy ra danh sách các văn bản hổ sơ cũ hơn theo đúng cấu hình của function
        /// </summary>
        /// <param name="function">Entity function</param>
        /// <param name="parameters">Các parameter</param>
        /// <returns>Danh sách các Dictionary, mỗi 1 dictionary là 1 row trong trong kết quả lấy được, dictionary có dạng: 'columnName:columnValue'</returns>
        IEnumerable<IDictionary<string, object>> GetDocumentOlderByFunction(ProcessFunction function, params object[] parameters);

        /// <summary>
        /// Lấy ra danh sách các văn bản hổ sơ phân trang theo đúng cấu hình của function
        /// </summary>
        /// <param name="function">Entity function</param>
        /// <param name="parameters">Các parameter</param>
        /// <returns>Danh sách các Dictionary, mỗi 1 dictionary là 1 row trong trong kết quả lấy được, dictionary có dạng: 'columnName:columnValue'</returns>
        IEnumerable<IDictionary<string, object>> GetDocumentPagingByFunction(ProcessFunction function, params object[] parameters);

        /// <summary>
        /// Lấy ra tổng số các văn bản, hồ sơ chưa được đọc tương ứng với từng node
        /// </summary>
        /// <param name="function">Entity function</param>
        /// <param name="parameters">Các parameter</param>
        /// <returns>Tổng số văn bản, hồ sơ chưa được đọc</returns>
        int GetTotalDocumentUnread(ProcessFunction function, params object[] parameters);

        /// <summary>
        /// Lấy ra tổng số các văn bản, hồ sơ tương ứng với từng node
        /// </summary>
        /// <param name="function">Entity function</param>
        /// <param name="parameters">Các parameter</param>
        /// <returns>Tổng số văn bản, hồ sơ chưa được đọc</returns>
        int GetTotalDocument(ProcessFunction function, params object[] parameters);
    }
}
