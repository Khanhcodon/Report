using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Linq.Expressions;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IDocExtendFieldDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DocExtendField trong CSDL
    /// </summary>
    public interface IDocExtendFieldDal
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        void Update(DocExtendField entity);

        /// <summary>
        /// Return a record by the spec
        /// </summary>
        /// <param name="spec">The spec</param>
        /// <returns></returns>
        DocExtendField Get(Expression<Func<DocExtendField, bool>> spec);
    }
}
