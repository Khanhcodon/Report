using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : CommentCommonBll - public - Bll </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm mở rộng xử lý ý kiến thường dùng </para>
    /// <para> ( TienBV@bkav.com - 200213) </para>
    /// </summary>
    public class CommonCommentBll : ServiceBase
    {
        private readonly IRepository<CommonComment> _commonCommentRepository;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">Context</param>
        public CommonCommentBll(IDbCustomerContext context)
            : base(context)
        {
            _commonCommentRepository = Context.GetRepository<CommonComment>();
        }

        /// <summary>
        /// Thêm ý kiến thường dùng.<br/>
        /// (TienBV@bkav.com - 200213)
        /// </summary>
        /// <param name="comment">ý kiến</param>
        public void Create(CommonComment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }
            _commonCommentRepository.Create(comment);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa ý kiến thường dùng.<br/>
        /// (TienBV@bkav.com - 200213)
        /// </summary>
        public void Delete(CommonComment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }
            _commonCommentRepository.Delete(comment);
            Context.SaveChanges();
        }

        /// <summary>
        /// Update ý kiến thường dùng.<br/>
        /// (HopCV@bkav.com - 250315)
        /// </summary>
        /// <param name="comment">ý kiến</param>
        public void Update(CommonComment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về ý kiến thường dùng theo id
        /// <para>(Tienbv@bkav.com 070313)</para>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="userId"> id nguoi dung</param>
        /// <returns>Ý kiến thường dùng</returns>
        public CommonComment Get(int id, int userId)
        {
            return _commonCommentRepository.Get(false, p => p.CommonCommentId == id && p.UserId == userId);
        }

        /// <summary>
        /// Trả về danh sách tất cả các ý kiến thường dùng. Kết quả chỉ để đọc<br/>
        /// (TienBV@bkav.com - 200213)
        /// </summary>
        public IEnumerable<CommonComment> Gets(Expression<Func<CommonComment, bool>> spec = null)
        {
            return _commonCommentRepository.GetsReadOnly(spec);
        }

    }
}
