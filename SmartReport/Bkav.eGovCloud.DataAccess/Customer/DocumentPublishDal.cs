using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Interface : DocumentPublishDal - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 150813
    /// <para></para> Author      : TienBV
    /// <para></para> Description : DAL tương ứng với bảng DocumentPublish trong CSDL
    /// </summary>
    public class DocumentPublishDal : DataAccessBase, IDocumentPublishDal
    {
        private readonly IRepository<DocumentPublish> _documentPublishRepository;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="context"></param>
        public DocumentPublishDal(IDbCustomerContext context)
            : base(context)
        {
            _documentPublishRepository = context.GetRepository<DocumentPublish>();
        }

        #pragma warning disable 1591
        public void Create(DocumentPublish documentPublish)
        {
            _documentPublishRepository.Create(documentPublish);
        }
    }
}
