using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : IExtensionTimeDal - public - Dal </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 290313</para>
    /// <para> Author : GiangPN@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> API tương tác với bảng gia hạn. </para>
    /// <para> ( GiangPN@bkav.com - 290313) </para>
    /// </summary>
    public interface IExtensionTimeDal
    {
        /// <summary>
        /// Thêm gia hạn
        /// </summary>
        /// <param name="entity">The entity</param>
        void Create(ExtendedTime entity);

        /// <summary>
        /// <para> Thêm nhiều gia hạn.</para>
        /// <para> (giangpn@bkav.com 290313)</para>
        /// </summary>
        /// <param name="entities">Entities</param>
        void Create(IEnumerable<ExtendedTime> entities);

        /// <summary>
        /// <para> Xóa các gia hạn.</para>
        /// <para> (giangpn@bkav.com 290313) </para>
        /// </summary>
        /// <param name="entities"></param>
        void Delete(IEnumerable<ExtendedTime> entities);

        /// <summary>
        /// <para> Lấy các gia hạn theo điều kiện truyền vào.</para>
        /// <para> ( giangpn@bkav.com 290313 )</para>
        /// </summary>
        /// <param name="spec">Spec</param>
        /// <returns></returns>
        IEnumerable<ExtendedTime> Gets(Expression<Func<ExtendedTime, bool>> spec);

        /// <summary>
        /// Kiểm tra sự tồn tại của ExtensionTime phù hợp với điều kiện truyền vào
        /// <para> (giangpn@bkav.com 290313) </para>
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 ExtensionTime phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<ExtendedTime, bool>> spec);
    }
}
