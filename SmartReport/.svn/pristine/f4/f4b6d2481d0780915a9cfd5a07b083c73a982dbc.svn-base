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
    /// Interface : ICategoryDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Modify Date : 290412
    /// Editor      : GiangPN
    /// Resons:
    /// Bổ sung các hàm thêm, sửa, xóa.
    /// Description : DAL tương ứng với bảng Category trong CSDL
    /// </summary>
    public interface ICategoryDal
    {

        /// <summary>
        /// Lấy ra danh sách thể loại văn bản hồ sơ
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilter">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách thể loại văn bản hồ sơ</returns>
        IEnumerable<Category> Gets(Expression<Func<Category, bool>> spec = null,
                                    Func<IQueryable<Category>, IQueryable<Category>> preFilter = null,
                                    params Func<IQueryable<Category>, IQueryable<Category>>[] postFilter);
        

        /// <summary>
        /// Lấy ra thể loại văn bản theo id
        /// </summary>
        /// <param name="id">Id của thể loại văn bản</param>
        /// <returns>Entity thể loại văn bản</returns>
        Category Get(int id);

        /// <summary>
        /// Tạo mới thể loại văn bản
        /// </summary>
        /// <param name="category">Entity thể loại văn bản</param>
        void Create(Category category);

        /// <summary>
        /// Cập nhật thông tin thể loại văn bản
        /// </summary>
        /// <param name="category">Entity thể loại văn bản</param>
        void Update(Category category);

        /// <summary>
        /// Xóa thể loại văn bản
        /// </summary>
        /// <param name="category">Entity thể loại văn bản</param>
        void Delete(Category category);

        /// <summary>
        /// Xóa nhiều thể loại văn bản
        /// </summary>
        /// <param name="categorys">Danh sách thể loại văn bản cần xóa</param>
        void Delete(IEnumerable<Category> categorys);

        /// <summary>
        /// Kiểm tra sự tồn tại của thể loại văn bản phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 thể loại văn bản phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Category, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Category, bool>> spec = null);
    }
}
