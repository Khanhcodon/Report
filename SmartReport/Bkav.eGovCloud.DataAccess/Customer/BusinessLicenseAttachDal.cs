using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BusinessLicenseAttachDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IBusinessLicenseAttachDal
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng BusinessLicenseAttach trong CSDL
    /// </summary>
    public class BusinessLicenseAttachDal : DataAccessBase, IBusinessLicenseAttachDal
    {
        private readonly IRepository<BusinessLicenseAttach> _businessLicenseattachRepository;
        /// <summary>
        /// Khởi tạo class <see cref="CodeDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public BusinessLicenseAttachDal(IDbCustomerContext context)
            : base(context)
        {
            _businessLicenseattachRepository = Context.GetRepository<BusinessLicenseAttach>();
        }

        #pragma warning disable 1591
        public BusinessLicenseAttach Get(int id)
        {
            return _businessLicenseattachRepository.One(a => a.BusinessLicenseId == id);
        }

        public void Create(BusinessLicenseAttach businessLicenseattach)
        {
            _businessLicenseattachRepository.Create(businessLicenseattach);
        }
        
        public void Delete(BusinessLicenseAttach businessLicenseattach)
        {
            _businessLicenseattachRepository.Delete(businessLicenseattach);
        }
    }
}
