using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : DocFeeDal - public - Dal </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm mở rộng xử lý lệ phí của hs </para>
    /// <para> ( TienBV@bkav.com - 200213) </para>
    /// </summary>
    public class DocFeeDal : DataAccessBase, IDocFeeDal
    {
        #region private fields

        private readonly IRepository<DocFee> _docFeeRepository;

        #endregion private fields

        #region c'tor

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public DocFeeDal(IDbCustomerContext context)
            : base(context)
        {
            _docFeeRepository = context.GetRepository<DocFee>();
        }

        #endregion c'tor

#pragma warning disable 1591
        #region public methods

        public void Update(DocFee docFee) {
            _docFeeRepository.Update(docFee);
        }

        public void Delete(DocFee docFee)
        {
            _docFeeRepository.Delete(docFee);
        }

        public void Create(DocFee fee)
        {
            _docFeeRepository.Create(fee);
        }

        public DocFee Get(int id)
        {
            return _docFeeRepository.One(id);
        }
        #endregion public methods
    }
}