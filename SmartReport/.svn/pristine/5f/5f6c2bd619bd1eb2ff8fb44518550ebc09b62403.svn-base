using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Interface : InfomationDal - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 170813
    /// <para></para> Author      : DungHV
    /// <para></para> Description : DAL tương ứng với bảng Infomation trong CSDL
    /// </summary>
    public class InfomationDal : DataAccessBase, IInfomationDal
    {
        private readonly IRepository<Infomation> _infomationRepository;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="context"></param>
        public InfomationDal(IDbCustomerContext context)
            : base(context)
        {
            _infomationRepository = context.GetRepository<Infomation>();
        }

        #pragma warning disable 1591

        #region IAddressDal Members

        public void Create(Infomation infomation)
        {
            _infomationRepository.Create(infomation);
        }

        public void Update(Infomation infomation)
        {
            _infomationRepository.Update(infomation);
        }

        public Infomation Get(int id)
        {
            return _infomationRepository.One(id);
        }

        public System.Collections.Generic.IEnumerable<Infomation> Gets()
        {
            return _infomationRepository.Find();
        }
        #endregion        
    }
}
