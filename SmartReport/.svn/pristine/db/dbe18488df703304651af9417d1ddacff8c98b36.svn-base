using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para> Project: eGov Cloud v1.0</para>
    /// <para> Interface : IDocumentContentDal - public - Interface</para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 290113</para>
    /// <para> Author      : TienBV</para>
    /// </author>
    /// <summary>
    /// <para> Description : DAL tương ứng với bảng DocumentContent trong CSDL </para>
    /// </summary>
    public interface IDocumentContentDal
    {
        /// <summary>
        /// <para>Lấy nội dung hồ sơ theo id</para>
        /// (Tienbv@bkav.com - 290113)
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        DocumentContent Get(int id);

        /// <summary>
        /// Lấy ra danh sách nội dung
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách nội dung</returns>
        IEnumerable<DocumentContent> Gets(Expression<Func<DocumentContent, bool>> spec = null);

        /// <summary>
        /// update
        /// </summary>
        /// <param name="content"></param>
        void Update(DocumentContent content);
    }
}
