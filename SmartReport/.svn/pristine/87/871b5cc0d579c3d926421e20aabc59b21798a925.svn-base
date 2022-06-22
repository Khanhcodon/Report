using System.Linq;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CommentDal - public - DAL
    /// Access Modifiers:
    ///     * Inherit : DataAccessBase
    ///     * Implement : ICommentDal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Comment trong CSDL
    /// </summary>
    public class CommentDal : DataAccessBase, ICommentDal
    {
        #region private fields

        private readonly IRepository<Comment> _commentRepository;

        #endregion private fields

        #region c'tor

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public CommentDal(IDbCustomerContext context)
            : base(context)
        {
            _commentRepository = context.GetRepository<Comment>();
        }

        #endregion c'tor

#pragma warning disable 1591
        #region public methods

        public IEnumerable<Comment> Gets(Expression<Func<Comment, bool>> spec, Func<IQueryable<Comment>, IQueryable<Comment>> preFilter = null,
                                    params Func<IQueryable<Comment>, IQueryable<Comment>>[] postFilters)
        {
            return _commentRepository.Find(spec, preFilter, postFilters);
        }

        public void Create(Comment comment)
        {
            _commentRepository.Create(comment);
        }

        public void Delete(Comment comment)
        {
            _commentRepository.Delete(comment);
        }

        public void Delete(IEnumerable<Comment> comments)
        {
            foreach (var comment in comments)
            {
                _commentRepository.Delete(comment, false);
            }
            Context.SaveChanges();
        }

        #endregion public methods
    }
}