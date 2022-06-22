using System;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : DocExtenfieldBll - public - Bll </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 090413</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> ( TienBV@bkav.com - 090413) </para>
    /// </summary>
    public class DocExtendfieldBll : ServiceBase
    {
        private readonly IRepository<DocExtendField> _docExfieldRepository;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context"></param>
        public DocExtendfieldBll(IDbCustomerContext context)
            : base(context)
        {
            _docExfieldRepository = Context.GetRepository<DocExtendField>();
        }

        /// <summary>
        /// Trả về đối tượng extenfield in document theo form
        /// </summary>
        /// <param name="docId">Document Id</param>
        /// <param name="formId">Form Id</param>
        /// <param name="controlId">Extendfield Id</param>
        /// <returns></returns>
        public DocExtendField Get(Guid docId, Guid formId, Guid controlId)
        {
            var spec = DocExtendFieldQuery.GetExfieldInDoc(docId, formId, controlId);
            return _docExfieldRepository.Get(false, spec);
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="entity"></param>
        public void Update(DocExtendField entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.SaveChanges();
        }
    }
}
