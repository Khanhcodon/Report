using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : DocCatalogQuery - public - Util </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 13</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Query thao tac voi bang DocCatalog </para>
    /// <para> ( TienBV@bkav.com - 210213) </para>
    /// </summary>
    public static class DocCatalogQuery
    {
        /// <summary>
        /// DocumentId == docId and FormId == formId and CatalogId == catalogId
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="formId"></param>
        /// <param name="catalogId"></param>
        /// <returns></returns>
        public static Expression<Func<DocCatalog, bool>> GetCatalogInDoc(Guid docId, Guid formId, Guid catalogId)
        {
            return dc => dc.DocumentId == docId && dc.FormId == formId && dc.CatalogId == catalogId;
        }
    }
}
