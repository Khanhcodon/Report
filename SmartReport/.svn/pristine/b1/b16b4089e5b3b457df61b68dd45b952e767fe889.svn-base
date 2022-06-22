using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IConnectionDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Connection trong CSDL
    /// </summary>
    public interface IConnectionDal
    {

        /// <summary>
        /// Lấy ra tất cả các connection phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các connection
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các connection</returns>
        IEnumerable<Connection> Gets(Expression<Func<Connection, bool>> spec = null);

        /// <summary>
        /// Lấy ra connetion theo domain id
        /// </summary>
        /// <param name="id">Id của domain</param>
        /// <returns>Entity connection</returns>
        Connection Get(int id);

        /// <summary>
        /// Tạo mới connection
        /// </summary>
        /// <param name="connection">Entity connection</param>
        void Create(Connection connection);

        /// <summary>
        /// Cập nhật thông tin connection
        /// </summary>
        /// <param name="connection">Entity connection</param>
        void Update(Connection connection);

        /// <summary>
        /// Xóa connection
        /// </summary>
        /// <param name="connection">Entity connection</param>
        void Delete(Connection connection);
    }
}
