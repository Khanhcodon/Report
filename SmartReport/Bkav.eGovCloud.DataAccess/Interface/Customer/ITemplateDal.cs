using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : ITemplateDal - public - Core </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 130313</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Cung cấp các api thao tác với bảng Template </para>
    /// <para> ( TienBV@bkav.com - 130313) </para>
    /// </summary>
    public interface ITemplateDal
    {

        /// <summary>
        /// <para> Trả về tất cả template phù hợp với điều kiện truyền vào. </para>
        /// <para> Nếu điều kiện bằng null thì sẽ lấy ra tất cả template.</para>
        /// <para> (Tienbv@bkav.com 130313)</para>
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các template </returns>
        IEnumerable<Template> Gets(Expression<Func<Template, bool>> spec = null,
                                    Func<IQueryable<Template>, IQueryable<Template>> preFilter = null,
                                    params Func<IQueryable<Template>, IQueryable<Template>>[] postFilters);

        /// <summary>
        /// Trả về template theo id
        /// </summary>
        /// <param name="id">The template id</param>
        /// <returns></returns>
        Template Get(int id);

        /// <summary>
        /// Xóa template.
        /// </summary>
        /// <param name="entity"></param>
        void Delelte(Template entity);

        /// <summary>
        /// Cập nhật template.
        /// </summary>
        /// <param name="entity"></param>
        void Update(Template entity);

        /// <summary>
        /// Đếm số template theo điều kiện kỹ thuật truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện kỹ thuật</param>
        /// <returns></returns>
        int Count(Expression<Func<Template, bool>> spec = null);

        /// <summary>
        /// Thêm template mới
        /// </summary>
        /// <param name="entity"></param>
        void Create(Template entity);

        /// <summary>
        /// Kiểm tra đã tồn tại theo điều kiện kỹ thuật.
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        bool Exist(Expression<Func<Template, bool>> spec);
    }
}
