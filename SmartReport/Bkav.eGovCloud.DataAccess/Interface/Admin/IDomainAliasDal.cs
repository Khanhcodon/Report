using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IDomainAliasDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng DomainAlias trong CSDL
    /// </summary>
    public interface IDomainAliasDal
    {
        /// <summary>
        /// Lấy ra tất cả các đường dẫn phù hợp với điều kiện truyền vào. Nếu điều kiện bằng null thì sẽ lấy ra tất cả các đường dẫn
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách đường dẫn</returns>
        IEnumerable<DomainAlias> Gets(Expression<Func<DomainAlias, bool>> spec = null);

        /// <summary>
        /// Lấy ra đường dẫn theo Id
        /// </summary>
        /// <param name="id">Id của đường dẫn</param>
        /// <returns>Entity đường dẫn</returns>
        DomainAlias Get(int id);

        /// <summary>
        /// Lấy ra đường dẫn theo alias
        /// </summary>
        /// <param name="alias">Http alias</param>
        /// <returns>Entity đường dẫn</returns>
        DomainAlias Get(string alias);

        /// <summary>
        /// Tạo mới đường dẫn
        /// </summary>
        /// <param name="domainAlias">Entity đường dẫn</param>
        void Create(DomainAlias domainAlias);

        /// <summary>
        /// Cập nhật thông tin đường dẫn
        /// </summary>
        /// <param name="domainAlias">Entity đường dẫn</param>
        void Update(DomainAlias domainAlias);

        /// <summary>
        /// Xóa đường dẫn
        /// </summary>
        /// <param name="domainAlias">Entity đường dẫn</param>
        void Delete(DomainAlias domainAlias);

        /// <summary>
        /// Kiểm tra sự tồn tại của đường dẫn phù hợp với điều kiện truyền vào
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>true: nếu có tồn tại ít nhất 1 đường dẫn phù hợp, ngược lại: false</returns>
        bool Exist(Expression<Func<DomainAlias, bool>> spec);
    }
}
