using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ConnectionDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IConnectionDal
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Connection trong CSDL
    /// </summary>
    public class ConnectionDal : DataAccessBase, IConnectionDal
    {
        private readonly IRepository<Connection> _connectionRepository;

        /// <summary>
        /// Khởi tạo class <see cref="ConnectionDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public ConnectionDal(IDbAdminContext context) : base(context)
        {
            _connectionRepository = Context.GetRepository<Connection>();
        }

        #pragma warning disable 1591

        public IEnumerable<Connection> Gets(Expression<Func<Connection, bool>> spec = null)
        {
            return _connectionRepository.Find(spec);
        }

        public Connection Get(int id)
        {
            return _connectionRepository.One(c => c.DomainId == id);
        }

        public void Create(Connection connection)
        {
            _connectionRepository.Create(connection);
        }

        public void Update(Connection connection)
        {
            _connectionRepository.Update(connection);
        }

        public void Delete(Connection connection)
        {
            _connectionRepository.Delete(connection);
        }
    }
}
