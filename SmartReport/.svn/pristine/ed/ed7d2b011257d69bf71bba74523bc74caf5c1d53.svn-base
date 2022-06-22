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
    /// <para>Interface : IProcessFunctionTypeDal - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : DAL tương ứng với bảng ProcessFunctionType trong CSDL</para>
    /// </summary>
    public interface IProcessFunctionTypeDal
    {
        /// <summary>
        /// Lấy ra tất cả các loại function phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các loại function
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các loại function</returns>
        IEnumerable<ProcessFunctionType> Gets(Expression<Func<ProcessFunctionType, bool>> spec = null,
                                    Func<IQueryable<ProcessFunctionType>, IQueryable<ProcessFunctionType>> preFilter = null,
                                    params Func<IQueryable<ProcessFunctionType>, IQueryable<ProcessFunctionType>>[] postFilters);

        /// <summary>
        /// Lấy ra loại function theo id
        /// </summary>
        /// <param name="id">Id của loại function</param>
        /// <returns>Entity</returns>
        ProcessFunctionType Get(int id);

        /// <summary>
        /// Tạo mới loại function
        /// </summary>
        /// <param name="functionType">Entity</param>
        void Create(ProcessFunctionType functionType);

        /// <summary>
        /// Cập nhật thông tin loại function
        /// </summary>
        /// <param name="functionType">Entity</param>
        void Update(ProcessFunctionType functionType);

        /// <summary>
        /// Xóa loại function
        /// </summary>
        /// <param name="functionType">Entity</param>
        void Delete(ProcessFunctionType functionType);

        /// <summary>
        /// Kiểm tra sự tồn tại của loại function phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 loại function phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<ProcessFunctionType, bool>> spec);

        /// <summary>
        /// Lấy ra danh sách các item của loại function dựa vào câu query của loại function
        /// </summary>
        /// <param name="functionType">Entity</param>
        /// <param name="parameters">Tham số</param>
        /// <returns>Danh sách các Dictionary, mỗi 1 dictionary là 1 row trong CSDL, dictionary có dạng: 'columnName:columnValue'</returns>
        IEnumerable<IDictionary<string, object>> GetItemsOfType(ProcessFunctionType functionType, params object[] parameters);
    }
}
