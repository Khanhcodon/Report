using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public static class DocFinishQuery
    {
        /// <summary>
        /// DocumentId == docId and DocumentCopyId == documentCopyId and UserId == userId
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="documentCopyId"></param>
        /// <param name="userId"></param>
        /// <param name="type"> </param>
        /// <returns></returns>
        public static Expression<Func<DocFinish, bool>> Exits(Guid docId, int documentCopyId, int userId, DocFinishType type)
        {
            return df => df.DocumentCopyId == documentCopyId
                        && df.DocumentId == docId
                        && df.UserId == userId
                        && df.DocFinishType == (int)type;
        }

        /// <summary>
        /// DocumentCopyId == documentCopyId and UserId == userId
        /// </summary>
        /// <param name="docCopyId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Expression<Func<DocFinish, bool>> WithDocCopyAndUser(int docCopyId, int userId)
        {
            return df => df.DocumentCopyId == docCopyId && df.UserId == userId;
        }
    }
}
