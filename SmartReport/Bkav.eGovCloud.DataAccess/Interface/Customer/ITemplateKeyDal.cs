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
    /// <para> Class : ITemplateKeyDal - public - Core </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 130313</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm mở rộng xử lý mẫu key cho template </para>
    /// <para> ( TienBV@bkav.com - 130313) </para>
    /// </summary>
    public interface ITemplateKeyDal
    {

        /// <summary>
        /// <para> Trả về tất cả template key phù hợp với điều kiện truyền vào. </para>
        /// <para> Nếu điều kiện bằng null thì sẽ lấy ra tất cả templatekey.</para>
        /// <para> (Tienbv@bkav.com 130313)</para>
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các template key</returns>
        IEnumerable<TemplateKey> Gets(Expression<Func<TemplateKey, bool>> spec = null,
                                    Func<IQueryable<TemplateKey>, IQueryable<TemplateKey>> preFilter = null,
                                    params Func<IQueryable<TemplateKey>, IQueryable<TemplateKey>>[] postFilters);

        /// <summary>
        /// Trả về template key theo id
        /// </summary>
        /// <param name="id">The template key id</param>
        /// <returns></returns>
        TemplateKey Get(int id);

        /// <summary>
        /// Xóa 1 key.
        /// </summary>
        /// <param name="entity"></param>
        void Delelte(TemplateKey entity);

        /// <summary>
        /// Cập nhật key
        /// </summary>
        /// <param name="entity"></param>
        void Update(TemplateKey entity);

        /// <summary>
        /// Đếm số template key theo điều kiện kỹ thuật truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện kỹ thuật</param>
        /// <returns></returns>
        int Count(Expression<Func<TemplateKey, bool>> spec = null);

        /// <summary>
        /// Thêm key mới
        /// </summary>
        /// <param name="entity"></param>
        void Create(TemplateKey entity);

        /// <summary>
        /// Kiểm tra đã tồn tại theo điều kiện kỹ thuật.
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        bool Exist(Expression<Func<TemplateKey, bool>> spec);

        /// <summary>
        /// Trả về template key theo mã
        /// </summary>
        /// <param name="keyCode">mã key</param>
        /// <returns></returns>
        TemplateKey Get(string keyCode);

        /// <summary>
        /// Trả về giá trị của một key.
        /// </summary>
        /// <param name="templateKey">Key</param>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        IEnumerable<IDictionary<string, object>> GetValue(TemplateKey templateKey, params object[] parameters);
    }
}
