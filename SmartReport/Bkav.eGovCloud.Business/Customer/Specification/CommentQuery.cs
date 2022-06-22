using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommentQuery
    {
        /// <summary>
        /// DocumentCopyId == documentcopyId
        /// </summary>
        /// <param name="documentcopyId">The document copy id.</param>
        /// <returns></returns>
        public static Expression<Func<Comment, bool>> WithDocumentCopyId(int documentcopyId)
        {
            return c => c.DocumentCopyId == documentcopyId;
            //&& c.UserSendId != c.UserReceiveId; // loai bo truong hop xin y kien
        }

        /// <summary>
        /// DocumentCopyId = documentcopyId and Datacreated $lt; datecreated
        /// </summary>
        /// <param name="documentcopyId"></param>
        /// <param name="dateCreated"></param>
        /// <returns></returns>
        public static Expression<Func<Comment, bool>> WithDocumentCopyIdAndDateCreated(int documentcopyId, DateTime dateCreated)
        {
            return c => c.DocumentCopyId == documentcopyId && c.DateCreated.CompareTo(dateCreated) <= 0;
        }

        /// <summary>
        /// ParentId = documentcopyId and UserReceive = userId and CommentType = CommentType.Consulted
        /// </summary>
        /// <param name="documentcopyIds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Expression<Func<Comment, bool>> IsCoProcessor(IEnumerable<int> documentcopyIds, int userId)
        {
            return c => documentcopyIds.Contains(c.DocumentCopyId) && c.CommentType == (int)CommentType.Consulted; //&& c.UserReceiveId == userId 
        }

        /// <summary>
        /// DocumentCopyTargetId = documentCopyId And CommentTypeInEnum = CommentType.Consulted
        /// </summary>
        /// <param name="documentCopyId"></param>
        /// <returns></returns>
        public static Expression<Func<Comment, bool>> IsCoProcessor(int documentCopyId)
        {
            return c => c.DocumentCopyTargetId == documentCopyId && c.CommentType == (int)CommentType.Consulted;
        }

        /// <summary>
        /// ParentId = documentcopyId and UserReceive = userId and CommentType = CommentType.Contribution
        /// </summary>
        /// <param name="documentCopyIds"></param>
        /// <returns></returns>
        public static Expression<Func<Comment, bool>> IsContribution(IEnumerable<int> documentCopyIds)
        {
            return c => documentCopyIds.Contains(c.DocumentCopyId) && c.CommentType.Equals((int)CommentType.Contribution);
        }

    }
}
