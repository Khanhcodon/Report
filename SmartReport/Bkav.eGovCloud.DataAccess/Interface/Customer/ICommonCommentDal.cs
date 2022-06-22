using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : ICommonCommentDal - public - Interface </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm tương tác với bảng commoncomment </para>
    /// <para> ( TienBV@bkav.com - 200213) </para>
    /// </summary>
    public interface ICommonCommentDal
    {
        /// <author>
        /// <para> Create Date : 200213</para>
        /// <para> Author : TienBV@bkav.com </para>
        /// </author>
        /// <summary>
        /// <para> Lấy danh sách ý kiến thường dùng </para>
        /// <para> ( TienBV@bkav.com - 200213) </para>
        /// </summary>
        IEnumerable<CommonComment> Gets(Expression<Func<CommonComment, bool>> spec = null);

        /// <author>
        /// <para> Create Date : 200213</para>
        /// <para> Author : TienBV@bkav.com </para>
        /// </author>
        /// <summary>
        /// <para> Tạo ý kiến thường dùng </para>
        /// <para> ( TienBV@bkav.com - 200213) </para>
        /// </summary>
        void Create(CommonComment comment);

        /// <author>
        /// <para> Create Date : 200213</para>
        /// <para> Author : TienBV@bkav.com </para>
        /// </author>
        /// <summary>
        /// <para> Xóa ý kiến thường dùng </para>
        /// <para> ( TienBV@bkav.com - 200213) </para>
        /// </summary>
        void Delete(CommonComment comment);

        /// <summary>
        /// Trả về ý kiến thường dùng theo id
        /// <para>(Tienbv@bkav.com 070313)</para>
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>Ý kiến thường dùng</returns>
        CommonComment Get(int id);
    }
}
