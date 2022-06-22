using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ServerDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IServerDal
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Server trong CSDL
    /// </summary>
    public class ServerDal : DataAccessBase, IServerDal
    {
        private readonly IRepository<Server> _serverRepository;

        /// <summary>
        /// Khởi tạo class <see cref="ServerDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public ServerDal(IDbAdminContext context)
            : base(context)
        {
            _serverRepository = Context.GetRepository<Server>();
        }

        #pragma warning disable 1591

        public IEnumerable<Server> Gets(Expression<Func<Server, bool>> spec = null)
        {
            return _serverRepository.Find(spec);
        }

        public Server Get(int id)
        {
            return _serverRepository.One(s => s.ServerId == id);
        }

        public void Create(Server server)
        {
            _serverRepository.Create(server);
        }

        public void Update(Server server)
        {
            _serverRepository.Update(server);
        }

        public bool Exist(Expression<Func<Server, bool>> spec)
        {
            return _serverRepository.Any(spec);
        }

        public void Delete(Server server)
        {
            _serverRepository.Delete(server);
        }
    }
}
