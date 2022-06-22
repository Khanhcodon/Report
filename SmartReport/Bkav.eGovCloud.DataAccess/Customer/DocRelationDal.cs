using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;
using System;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocRelationDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDocReasonDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DocRelation trong CSDL
    /// </summary>
    public class DocRelationDal : DataAccessBase, IDocRelationDal
    {
        private readonly IRepository<DocRelation> _docrelationRep;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public DocRelationDal(IDbCustomerContext context)
            : base(context)
        {
            _docrelationRep = context.GetRepository<DocRelation>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relation"></param>
        public void Create(DocRelation relation)
        {
            _docrelationRep.Create(relation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentCopyId"></param>
        /// <returns></returns>
        public IEnumerable<DocRelation> Gets(int documentCopyId)
        {
            return _docrelationRep.Find(d => d.DocumentCopyId == documentCopyId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentId"> </param>
        /// <param name="documentCopyId"> </param>
        /// <param name="relationId"></param>
        /// <param name="relationCopyId"> </param>
        /// <returns></returns>
        public bool Exist(Guid documentId, int documentCopyId, Guid relationId, int relationCopyId)
        {
            return _docrelationRep.Any(r => r.DocumentId == documentId && r.DocumentCopyId == documentCopyId &&
                r.RelationId == relationId && r.RelationCopyId == relationCopyId);
        }
    }
}
