using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : ILuceneDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Lucene trong CSDL
    /// </summary>
    public interface ILuceneDal
    {
        /// <summary>
        /// Lấy ra danh sách các nội dung cần đánh index
        /// </summary>
        /// <returns>Danh sách file đính kèm</returns>
        IEnumerable<Lucene> Gets(Expression<Func<Lucene, bool>> spec = null);

        /// <summary>
        /// Lấy ra nội dung cần đánh index
        /// </summary>
        /// <param name="id">Id của nội dung cần đánh index</param>
        /// <returns>Entity nội dung cần đánh index</returns>
        Lucene Get(int id);

        /// <summary>
        /// Lấy ra nội dung cần đánh index
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Entity nội dung cần đánh index</returns>
        Lucene Get(Expression<Func<Lucene, bool>> spec);

        /// <summary>
        /// Tạo mới nội dung cần đánh index
        /// </summary>
        /// <param name="lucene">Entity nội dung cần đánh index</param>
        void Create(Lucene lucene);

        /// <summary>
        /// Tạo mới nội dung cần đánh index
        /// </summary>
        /// <param name="lucene">Entity nội dung cần đánh index</param>
        void Create(IEnumerable<Lucene> lucene);

        /// <summary>
        /// Cập nhật thông tin nội dung cần đánh index
        /// </summary>
        /// <param name="lucene">Entity nội dung cần đánh index</param>
        void Update(Lucene lucene);

        /// <summary>
        /// Xóa nội dung cần đánh index
        /// </summary>
        /// <param name="lucene">Entity nội dung cần đánh index</param>
        void Delete(Lucene lucene);
    }
}
