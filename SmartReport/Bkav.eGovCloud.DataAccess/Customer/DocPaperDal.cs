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
    public class DocPaperDal : DataAccessBase, IDocPaperDal
    {
        #region private fields

        private readonly IRepository<DocPaper> _docPaperRepository;

        #endregion private fields

        #region c'tor

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public DocPaperDal(IDbCustomerContext context)
            : base(context)
        {
            _docPaperRepository = context.GetRepository<DocPaper>();
        }

        #endregion c'tor

#pragma warning disable 1591
        #region public methods

        public void Update(DocPaper docPaper) {
            _docPaperRepository.Update(docPaper);
        }

        public void Delete(DocPaper docPaper)
        {
            _docPaperRepository.Delete(docPaper);
        }

        public void Create(DocPaper docPaper)
        {
            _docPaperRepository.Create(docPaper);
        }

        public DocPaper Get(int id)
        {
            return _docPaperRepository.One(id);
        }
        #endregion public methods


    }
}