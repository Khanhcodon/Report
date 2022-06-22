using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Linq.Expressions;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IDocCatalogDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DocCatalog trong CSDL
    /// </summary>
    public interface IDocCatalogDal
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        void Update(DocCatalog entity);

        /// <summary>
        /// Return a record by the spec
        /// </summary>
        /// <param name="spec">the spec</param>
        /// <returns></returns>
        DocCatalog Get(Expression<Func<DocCatalog, bool>> spec);
    }
}
