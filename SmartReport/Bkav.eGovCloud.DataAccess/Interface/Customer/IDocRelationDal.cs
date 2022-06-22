using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IDocRelationDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DocRelation trong CSDL
    /// </summary>
    public interface IDocRelationDal
    {
        /// <summary> TienBv 061212
        /// Thêm các hồ sơ, văn bản liên quan.
        /// </summary>
        /// <param name="relation"></param>
        void Create(DocRelation relation);

        /// <summary> TienBV 061212
        /// Lấy danh sách các hồ sơ, văn bản liên quan của một hồ sơ, văn bản.
        /// </summary>
        /// <param name="documentCopyId">The doc id.</param>
        /// <returns></returns>
        IEnumerable<DocRelation> Gets(int documentCopyId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentCopyId"> </param>
        /// <param name="relationId"></param>
        /// <param name="documentId"> </param>
        /// <param name="relationCopyId"> </param>
        /// <returns></returns>
        bool Exist( Guid documentId,int documentCopyId, Guid relationId, int relationCopyId);
    }
}
