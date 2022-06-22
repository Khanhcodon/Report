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
    /// Interface : IFormDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Form trong CSDL
    /// </summary>
    public interface IFormDal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        void Create(Form form);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Form One(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        void Delete(Form form);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        bool IsUsed(Guid formId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        void Update(Form form);

        /// <summary>
        /// 
        /// </summary>
        void UnActiveOthers(Guid doctypeId, Guid currentId);

        /// <summary> Tienbv 301012
        /// Kiểm tra đã có mẫu form chính nào của môt loại hồ sơ đang ở trạng thái lưu tạm ko.
        /// </summary>
        /// <param name="doctypeId">The doctype id.</param>
        /// <returns></returns>
        bool HasTmp(Guid doctypeId);

        /// <summary> Tienbv 081112
        /// Lấy ra danh sách các mẫu form theo đk truyền vào.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <returns></returns>
        IEnumerable<Form> Gets(Expression<Func<Form, bool>> spec);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Form, bool>> spec = null);

        /// <summary>
        /// Lấy ra tất cả các biểu mẫu phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các nhảy số
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <param name="preFilter">Bộ lọc trước: Thay đổi, lọc dữ liệu trước khi truy vấn</param>
        /// <param name="postFilters">Bộ lọc sau: Thay đổi, lọc dữ liệu sau khi truy vấn được thực hiện</param>
        /// <returns>Danh sách các nhảy số</returns>
        IEnumerable<Form> Gets(Expression<Func<Form, bool>> spec = null,
                                    Func<IQueryable<Form>, IQueryable<Form>> preFilter = null,
                                    params Func<IQueryable<Form>, IQueryable<Form>>[] postFilters);
    }
}
