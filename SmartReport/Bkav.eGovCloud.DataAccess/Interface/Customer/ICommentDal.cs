using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : ICommentDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Comment trong CSDL
    /// </summary>
    public interface ICommentDal
    {
        /// <summary> TienBV 121225
        /// Lấy ra danh sách các comment theo đk kỹ thuật truyền vào.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns></returns>
        IEnumerable<Comment> Gets(Expression<Func<Comment, bool>> spec, Func<IQueryable<Comment>, IQueryable<Comment>> preFilter = null,
                                    params Func<IQueryable<Comment>, IQueryable<Comment>>[] postFilters);

        /// <summary> Tienbv 121225
        /// Thêm ý kiến xử lý.
        /// </summary>
        /// <param name="comment">The comment.</param>
        void Create(Comment comment);

        /// <summary>
        /// Xóa 1 bản ghi Comment
        /// <para>GiangPN@bkav.com - 050613</para>
        /// </summary>
        /// <param name="comment"></param>
        void Delete(Comment comment);

        /// <summary>
        /// Xóa nhiều comment
        /// </summary>
        /// <param name="comments">Danh sách comments cần xóa</param>
        void Delete(IEnumerable<Comment> comments);
    }
}
