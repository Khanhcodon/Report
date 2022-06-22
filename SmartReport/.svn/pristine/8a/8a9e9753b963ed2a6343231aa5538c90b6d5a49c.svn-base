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
    /// Interface : IAuthorizeDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng Authorize trong CSDL
    /// </summary>
    public interface IAuthorizeDal
    {
        /// <summary>
        /// Lấy ra tất cả các ủy quyền
        /// </summary>
        /// <returns>Danh sách các ủy quyền</returns>
        IEnumerable<Authorize> Gets(Expression<Func<Authorize, bool>> spec = null,
                                    Func<IQueryable<Authorize>, IQueryable<Authorize>> preFilter = null,
                                    params Func<IQueryable<Authorize>, IQueryable<Authorize>>[] postFilters);

        /// <summary>
        /// Lấy ra ủy quyền theo id
        /// </summary>
        /// <param name="id">Id ủy quyền</param>
        /// <returns>Entity ủy quyền</returns>
        Authorize Get(int id);        

        /// <summary>
        /// Tạo mới ủy quyền
        /// </summary>
        /// <param name="authorize">Entity ủy quyền</param>
        void Create(Authorize authorize);

        /// <summary>
        /// Cập nhật thông tin ủy quyền
        /// </summary>
        /// <param name="authorize">Entity ủy quyền</param>
        void Update(Authorize authorize);

        /// <summary>
        /// Xóa ủy quyền
        /// </summary>
        /// <param name="authorize">Entity ủy quyền</param>
        void Delete(Authorize authorize);

        /// <summary>
        /// Lấy ra tổng số bản ghi với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Tổng số bản ghi</returns>
        int Count(Expression<Func<Authorize, bool>> spec = null);
    }
}
