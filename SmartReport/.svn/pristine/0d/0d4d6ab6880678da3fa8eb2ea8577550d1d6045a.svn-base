using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Interface : ISupplementaryDal - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 240113
    /// <para></para> Author      : TienBV
    /// <para></para> Description : DAL tương ứng với bảng Supplementary trong CSDL
    /// </summary>
    public interface ISupplementaryDal
    {

        /// <summary>
        /// Trả về danh sách các yêu cầu bổ sung theo điều kiện truyền vào.
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        IEnumerable<Supplementary> Gets(Expression<Func<Supplementary, bool>> spec = null);

        /// <summary>
        /// <para> Thêm yêu cầu bổ sung</para>
        /// <para> (Tienbv@bkav.com - 260213)</para>
        /// </summary>
        /// <param name="entity"></param>
        void Create(Supplementary entity);

        /// <summary>
        /// <para> Cập nhật yêu cầu, kêt quả bổ sung.</para>
        /// <para> (Tienbv@bkav.com - 260213)</para>
        /// </summary>
        /// <param name="entity"></param>
        void Update(Supplementary entity);

        /// <summary>
        /// <para> Trả về các yêu cầu bổ sung chưa được bàn giao của doc copy tương ứng.</para>
        /// <para> (Tienbv@bkav.com - 260213)</para>
        /// </summary>
        /// <param name="docCopyId">Document copy id</param>
        /// <returns></returns>
        Supplementary GetByDocCopy(int docCopyId);

        /// <summary>
        /// <para> Trả về yêu cầu bổ sung theo id</para>
        /// <para> ( Tienbv@bkav.com - 260213)</para>
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Supplementary Get(int id);

        /// <summary>
        /// Trả về yêu cầu bổ sung chưa đc tiếp nhận.
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="docCopyId"></param>
        /// <returns></returns>
        Supplementary Get(Guid docId, int docCopyId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        bool Exist(Expression<Func<Supplementary, bool>> spec);
    }
}
