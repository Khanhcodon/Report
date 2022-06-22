using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <author>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Interface : IDocTimelineDal - public - DAL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 190213</para>
    /// <para>Author      : GiangPN</para>
    /// </author>
    /// <summary>
    /// <para> API tương tác với bảng DocTimeline. </para>
    /// <para> ( GiangPN@bkav.com - 190213) </para>
    /// </summary>
    public interface IDocTimelineDal
    {
        /// <summary>
        /// Lấy ra tất cả các lĩnh vục phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các thời gian xử lý văn bản
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các thời gian xử lý văn bản</returns>
        IEnumerable<DocTimeline> Gets(Expression<Func<DocTimeline, bool>> spec = null,
                                    Func<IQueryable<DocTimeline>, IQueryable<DocTimeline>> preFilter = null,
                                    params Func<IQueryable<DocTimeline>, IQueryable<DocTimeline>>[] postFilters);

        /// <summary>
        /// Lấy ra thời gian xử lý văn bản theo id
        /// </summary>
        /// <param name="id">Id của thời gian xử lý văn bản</param>
        /// <returns>Entity thời gian xử lý</returns>
        DocTimeline Get(int id);

        /// <summary>
        /// Lấy ra thời gian xử lý văn bản theo văn bản, văn bản copy và user
        /// </summary>
        /// <param name="documentId">Id của văn bản</param>
        /// <param name="documentCopyId">Id văn bản copy</param>
        /// <param name="userId">Id user</param>
        /// <returns>Entity thời gian xử lý</returns>
        DocTimeline Get(Guid documentId, int documentCopyId, int userId);

        /// <summary>
        /// Tạo mới thời gian xử lý văn bản
        /// </summary>
        /// <param name="docTimeline">Entity thời gian xử lý văn bản</param>
        void Create(DocTimeline docTimeline);

        /// <summary>
        /// Cập nhật thông tin thời gian xử lý văn bản
        /// </summary>
        /// <param name="docTimeline">Entity thời gian xử lý văn bản</param>
        void Update(DocTimeline docTimeline);

        /// <summary>
        /// Xóa thời gian xử lý văn bản
        /// </summary>
        /// <param name="docTimeline">Entity thời gian xử lý văn bản</param>
        void Delete(DocTimeline docTimeline);

        /// <summary>
        /// Xóa nhiều thời gian xử lý văn bản
        /// </summary>
        /// <param name="docTimelines">Danh sách thời gian xử lý văn bản cần xóa</param>
        void Delete(IList<DocTimeline> docTimelines);

        /// <summary>
        /// Kiểm tra sự tồn tại của thời gian xử lý văn bản phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 thời gian xử lý văn bản phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<DocTimeline, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<DocTimeline, bool>> spec = null);
    }
}
