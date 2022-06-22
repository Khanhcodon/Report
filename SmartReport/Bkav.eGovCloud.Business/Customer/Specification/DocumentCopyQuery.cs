using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : DocumentCopyQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 251212
    /// Author      : GiangPN
    /// Description : Các điều kiện truy vấn cho bảng DocumentCopy
    /// </summary>
    public static class DocumentCopyQuery
    {
        /// <summary>
        /// DocumentCopyId == DocumentCopyId
        /// </summary>
        /// <param name="documentCopyId">Id của bản sao văn bản.</param>
        /// <returns></returns>
        public static Expression<Func<DocumentCopy, bool>> WithId(int documentCopyId)
        {
            return s => s.DocumentCopyId == documentCopyId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="documentCopyTypes"></param>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        public static Expression<Func<DocumentCopy, bool>> IsChildWithType(int parentId, DocumentCopyTypes documentCopyTypes, int userId = 0)
        {
            var documentCopyTypesTmp = (int)documentCopyTypes;
            return dc => dc.ParentId == parentId
                    && (documentCopyTypesTmp == dc.DocumentCopyType)
                    && (userId == 0 || (documentCopyTypes == DocumentCopyTypes.XinYKien) && dc.LastUserIdComment == userId);
        }
    }
}
