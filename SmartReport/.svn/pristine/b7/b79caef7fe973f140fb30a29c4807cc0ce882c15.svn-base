using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Interface : IExtendFieldDal - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 010812
    /// <para></para> Author      : TrungVH
    /// <para></para> Description : DAL tương ứng với bảng ExtendField trong CSDL
    /// </summary>
    public interface IExtendFieldDal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        ExtendField One(Expression<Func<ExtendField, bool>> spec = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exfield"></param>
        void Add(ExtendField exfield);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exfield"></param>
        void Update(ExtendField exfield);

        /// <summary> TienBV 011112
        /// Lấy ra danh sách các extend field theo điều kiện truyền vào.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        IEnumerable<ExtendField> Gets(Expression<Func<ExtendField, bool>> spec = null);

        /// <summary> TienBV 071112
        /// Lấy ra danh sách các extend field của loại hồ sơ.
        /// </summary>
        /// <param name="doctypeId">The doctype guid id.</param>
        /// <returns></returns>
        IEnumerable<ExtendField> Gets(Guid doctypeId);

        /// <summary>
        /// Lấy ra dánh sách các extendfield danh sách id.
        /// </summary>
        /// <param name="Ids">danh sách id</param>
        /// <returns></returns>
        IEnumerable<ExtendField> Gets(IEnumerable<Guid> Ids);

        /// <summary> Tienbv 081112
        /// <para></para> Tạo trường mở rộng mới.
        /// <para></para> Được sử dụng khi tạo mới hoặc update form động.
        /// </summary>
        /// <param name="newItm">The extendfield obj.</param>
        void Create(ExtendField newItm);
    }
}
