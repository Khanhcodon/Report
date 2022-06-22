using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FormTypeDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IFormTypeDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng FormType trong CSDL
    /// </summary>
    public class FormTypeDal : DataAccessBase, IFormTypeDal
    {
        private readonly IRepository<FormType> _formTypeRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public FormTypeDal(IDbCustomerContext context) : base(context)
        {
            _formTypeRepository = Context.GetRepository<FormType>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FormType> Gets()
        {
            return _formTypeRepository.Find();
        }
    }
}
