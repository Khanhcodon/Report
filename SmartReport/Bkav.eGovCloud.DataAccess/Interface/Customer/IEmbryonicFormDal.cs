using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IEmbryonicFormDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 030314
    /// Author      : HopCV
    /// Description : DAL tương ứng với bảng EmbryonicForm trong CSDL
    /// </summary>
    public interface IEmbryonicFormDal
    {
        /// <summary>
        /// Lấy ra danh sách các mẫu phôi
        /// </summary>
        /// <returns>Danh sách mẫu phôi</returns>
        IEnumerable<EmbryonicForm> Gets(Expression<Func<EmbryonicForm, bool>> spec = null);

        /// <summary> 
        /// <para>Trả về danh sách tất cả các đối tượng cơ quan ngoài. </para>
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các đối tượng cơ quan ngoài</returns>
        IEnumerable<EmbryonicForm> Gets(Expression<Func<EmbryonicForm, bool>> spec = null,
                                    Func<IQueryable<EmbryonicForm>, IQueryable<EmbryonicForm>> preFilter = null,
                                    params Func<IQueryable<EmbryonicForm>, IQueryable<EmbryonicForm>>[] postFilters);

        /// <summary>
        /// Lấy ra tất cả các cơ quan phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện. </param>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<EmbryonicForm, TOutput>> projector,
                                           Expression<Func<EmbryonicForm, bool>> spec = null,
                                           Func<IQueryable<EmbryonicForm>, IQueryable<EmbryonicForm>> preFilter = null,
                                           params Func<IQueryable<EmbryonicForm>, IQueryable<EmbryonicForm>>[] postFilters);

        /// <summary>
        /// Lấy ra mẫu phôi
        /// </summary>
        /// <param name="id">Id của mẫu phôi</param>
        /// <returns>Entity mẫu phôi</returns>
        EmbryonicForm Get(int id);

        /// <summary>
        /// Tạo mới mẫu phôi
        /// </summary>
        /// <param name="embryonicForm">Entity mẫu phôi</param>
        void Create(EmbryonicForm embryonicForm);

        /// <summary>
        /// Cập nhật thông tin  mẫu phôi
        /// </summary>
        /// <param name="embryonicForm">Entity mẫu phôi</param>
        void Update(EmbryonicForm embryonicForm);

        /// <summary>
        /// Xóa mẫu phôi
        /// </summary>
        /// <param name="embryonicForm">Entity mẫu phôi</param>
        void Delete(EmbryonicForm embryonicForm);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<EmbryonicForm, bool>> spec = null);

        /// <summary>
        /// Lấy dữ liệu qua câu truy vẫn ở trong mẫu phôi
        /// </summary>
        /// <param name="embryonicForm"> đối tượng mẫu phôi</param>
        /// <param name="parameters">danh sách các tham số truyền vào</param>
        /// <returns></returns>
        DataTable GetDataByEmbryonicForm(EmbryonicForm embryonicForm, params object[] parameters);
    }
}
