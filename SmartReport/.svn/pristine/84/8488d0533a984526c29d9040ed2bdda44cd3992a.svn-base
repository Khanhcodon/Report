using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Linq.Expressions;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocExtendFieldDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDocExtendFieldDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DocExtendField trong CSDL
    /// </summary>
    public class DocExtendFieldDal : DataAccessBase, IDocExtendFieldDal
    {
        private IRepository<DocExtendField> _docExfieldRepository;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context"></param>
        public DocExtendFieldDal(IDbCustomerContext context) : base(context)
        {
            _docExfieldRepository = context.GetRepository<DocExtendField>();
        }
#pragma warning disable 1591
        public void Update(Entities.Customer.DocExtendField entity)
        {
            _docExfieldRepository.Update(entity);
        }

        public Entities.Customer.DocExtendField Get(Expression<Func<Entities.Customer.DocExtendField, bool>> spec)
        {
            return _docExfieldRepository.One(spec);
        }
    }
}
