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
    /// Interface : IJobTitlesDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 131012
    /// Author      : GiangPN
    /// Description : DAL tương ứng với bảng JobTitles trong CSDL
    /// </summary>
    public interface IJobTitlesDal
    {
        /// <summary>
        /// Lấy ra tất cả các chức danh phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các chức danh
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các chức danh</returns>
        IEnumerable<JobTitles> Gets(Expression<Func<JobTitles, bool>> spec = null,
                                    Func<IQueryable<JobTitles>, IQueryable<JobTitles>> preFilter = null,
                                    params Func<IQueryable<JobTitles>, IQueryable<JobTitles>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các chức danh phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<JobTitles, TOutput>> projector,
                                           Expression<Func<JobTitles, bool>> spec = null);

        /// <summary>
        /// Lấy ra chức danh theo id
        /// </summary>
        /// <param name="id">Id của chức danh</param>
        /// <returns>Entity chức danh</returns>
        JobTitles Get(int id);

        /// <summary>
        /// Tạo mới chức danh
        /// </summary>
        /// <param name="jobTitles">Entity chức danh</param>
        void Create(JobTitles jobTitles);

        /// <summary>
        /// Cập nhật thông tin chức danh
        /// </summary>
        /// <param name="jobTitles">Entity chức danh</param>
        void Update(JobTitles jobTitles);

        /// <summary>
        /// Xóa chức danh
        /// </summary>
        /// <param name="jobTitles">Entity chức danh</param>
        void Delete(JobTitles jobTitles);

        /// <summary>
        /// Xóa nhiều chức danh
        /// </summary>
        /// <param name="jobTitless">Danh sách chức danh cần xóa</param>
        void Delete(IEnumerable<JobTitles> jobTitless);

        /// <summary>
        /// Kiểm tra sự tồn tại của chức danh phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 chức danh phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<JobTitles, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<JobTitles, bool>> spec = null);
    }
}
