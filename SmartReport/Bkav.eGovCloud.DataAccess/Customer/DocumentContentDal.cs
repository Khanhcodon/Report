using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para> Project: eGov Cloud v1.0</para>
    /// <para> Interface : DocumentContentDal - public - Dal</para>
    /// <para> Access Modifiers: IDocumentContentDal</para>
    /// <para> Create Date : 290113</para>
    /// <para> Author      : TienBV</para>
    /// </author>
    /// <summary>
    /// <para> Description : DAL tương ứng với bảng DocumentContent trong CSDL </para>
    /// </summary>
    public class DocumentContentDal : DataAccessBase, IDocumentContentDal
    {
        private readonly IRepository<DocumentContent> _documentContentRepository;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">The customer context</param>
        public DocumentContentDal(IDbCustomerContext context) : base(context)
        {
            _documentContentRepository = context.GetRepository<DocumentContent>();
        }

        #pragma warning disable 1591

        public DocumentContent Get(int id)
        {
            return _documentContentRepository.One(id);
        }

        public IEnumerable<DocumentContent> Gets(Expression<Func<DocumentContent, bool>> spec = null)
        {
            return _documentContentRepository.Find(spec);
        }

        public void Update(DocumentContent content)
        {
            _documentContentRepository.Update(content);
        }
    }
}
