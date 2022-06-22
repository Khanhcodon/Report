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
    /// Interface : IDailyProcessDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 241113
    /// Author      : TienBV
    /// Resons:
    /// Bổ sung các hàm thêm, sửa, xóa.
    /// Description : DAL tương ứng với bảng DailyProcess trong CSDL
    /// </summary>
    public interface IDailyProcessDal
    {
        /// <summary>
        /// Lấy ra danh sách các xử lý
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilter">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách thể loại văn bản hồ sơ</returns>
        IEnumerable<DailyProcess> Gets(Expression<Func<DailyProcess, bool>> spec = null,
                                    Func<IQueryable<DailyProcess>, IQueryable<DailyProcess>> preFilter = null,
                                    params Func<IQueryable<DailyProcess>, IQueryable<DailyProcess>>[] postFilter);
        
        /// <summary>
        /// Tạo mới xử lý
        /// </summary>
        /// <param name="process">Entity</param>
        void Create(DailyProcess process);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="processes">Danh sách cần xóa</param>
        void Delete(IEnumerable<DailyProcess> processes);

        /// <summary>
        /// Kiểm tra tồn tại theo đk truyền vào
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool Exist(Expression<Func<DailyProcess, bool>> expression);
    }
}
