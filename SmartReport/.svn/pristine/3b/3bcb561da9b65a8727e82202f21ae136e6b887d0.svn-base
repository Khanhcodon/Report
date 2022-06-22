using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;
using System.Collections.Generic;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : CommonCommentDal - public - Dal </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm mở rộng xử lý các ý kiến thường dùng </para>
    /// <para> ( TienBV@bkav.com - 200213) </para>
    /// </summary>
    public class CommonCommentDal : DataAccessBase, ICommonCommentDal
    {
        #region private fields

        private readonly IRepository<CommonComment> _commentRepository;

        #endregion private fields

        #region c'tor

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public CommonCommentDal(IDbCustomerContext context)
            : base(context)
        {
            _commentRepository = context.GetRepository<CommonComment>();
        }

        #endregion c'tor

#pragma warning disable 1591
        #region public methods

        public void Create(CommonComment comment)
        {
            _commentRepository.Create(comment);
        }

        public void Delete(CommonComment comment)
        {
            _commentRepository.Delete(comment);
        }

        public CommonComment Get(int id)
        {
            return _commentRepository.One(id);
        }

        public IEnumerable<CommonComment> Gets(Expression<Func<CommonComment, bool>> spec = null)
        {
            return _commentRepository.Find(spec);
        }
        #endregion public methods
    }
}