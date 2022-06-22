using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IDocFinishDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DocFinish - bảng lưu trữ thông tin những User tham gia xử lý văn bản trong CSDL
    /// </summary>
    public interface IDocFinishDal
    {
        /// <summary>
        /// Lấy ra tất cả các DocFinish phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các DocFinish
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các DocFinish</returns>
        IEnumerable<DocFinish> Gets(Expression<Func<DocFinish, bool>> spec = null,
                                    Func<IQueryable<DocFinish>, IQueryable<DocFinish>> preFilter = null,
                                    params Func<IQueryable<DocFinish>, IQueryable<DocFinish>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các DocFinish phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<DocFinish, TOutput>> projector,
                                           Expression<Func<DocFinish, bool>> spec = null);

        /// <summary>
        /// Lấy ra DocFinish theo id
        /// </summary>
        /// <param name="id">Id của DocFinish</param>
        /// <returns>Entity DocFinish</returns>
        DocFinish Get(int id);

        /*
        /// <summary>
        /// Tạo mới DocFinish
        /// </summary>
        /// <param name="docFinish">Entity DocFinish</param>
        void Create(DocFinish docFinish);

        /// <summary>
        /// Cập nhật thông tin DocFinish
        /// </summary>
        /// <param name="docFinish">Entity DocFinish</param>
        void Update(DocFinish docFinish);

         */
        /// <summary>
        /// Xóa DocFinish
        /// </summary>
        /// <param name="docFinish">Entity DocFinish</param>
        void Delete(DocFinish docFinish);

        /// <summary>
        /// Xóa nhiều DocFinish
        /// </summary>
        /// <param name="docFinishs">Danh sách DocFinish cần xóa</param>
        void Delete(IEnumerable<DocFinish> docFinishs);

        /// <summary>
        /// Kiểm tra sự tồn tại của DocFinish phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 DocFinish phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<DocFinish, bool>> spec);

        /// <summary>
        /// <para>Thêm vào danh sách công văn người tham gia</para>
        /// <para>(tienbv@bkav.com - 240113)</para>
        /// </summary>
        /// <param name="entity">entity</param>
        void Create(DocFinish entity);

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="entity"></param>
        void Update(DocFinish entity);

        /// <summary>
        /// Trả về bản ghi theo docCOpy và user
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        DocFinish Get(Expression<Func<DocFinish, bool>> spec);

        /// <summary>
        /// <para> Lấy ra tất cả các bản ghi</para>
        /// </summary>
        /// <returns>Queryable</returns>
        IQueryable<DocFinish> Raw();
    }
}
