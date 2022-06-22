using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IServerDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Server trong CSDL
    /// </summary>
    public interface IServerDal
    {
        /// <summary>
        /// Lấy ra tất cả các server phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các server
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách các server</returns>
        IEnumerable<Server> Gets(Expression<Func<Server, bool>> spec = null);

        /// <summary>
        /// Lấy ra server theo id
        /// </summary>
        /// <param name="id">Id của server</param>
        /// <returns></returns>
        Server Get(int id);

        /// <summary>
        /// Tạo mới server
        /// </summary>
        /// <param name="server">Entity server</param>
        void Create(Server server);

        /// <summary>
        /// Cập nhật thông tin server
        /// </summary>
        /// <param name="server">Entity server</param>
        void Update(Server server);

        /// <summary>
        /// Kiểm tra sự tồn tại của server phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 server phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<Server, bool>> spec);

        /// <summary>
        /// Xóa server
        /// </summary>
        /// <param name="server">Entity server</param>
        void Delete(Server server);
    }
}
