using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.Business.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ServerBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 270712
    /// Author      : TrungVH
    /// Description : BLL tương ứng với bảng Server trong CSDL
    /// </summary>
    public class ServerBll : ServiceBase
    {
        private readonly IRepository<Server> _serverRepository;
        private readonly ResourceBll _resourceService;

        ///<summary>
        /// Khởi tạo class <see cref="ServerBll"/> class.
        ///</summary>
        ///<param name="context">Context</param>
        ///<param name="resourceService">BLL tương ứng với bảng Resource trong CSDL</param>
        public ServerBll(IDbAdminContext context, ResourceBll resourceService)
            : base(context)
        {
            _serverRepository = Context.GetRepository<Server>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Lấy ra tất cả các server
        /// </summary>
        /// <returns>Danh sách các server</returns>
        public IEnumerable<Server> Gets()
        {
            return _serverRepository.GetsReadOnly();
        }

        /// <summary>
        /// Lấy ra server theo id
        /// </summary>
        /// <param name="id">Id của server</param>
        /// <returns>Entity server</returns>
        public Server Get(int id)
        {
            Server result = null;
            if (id > 0)
            {
                result = _serverRepository.Get(id);
            }
            return result;
        }

        /// <summary>
        /// Tạo mới server
        /// </summary>
        /// <param name="server">Entity server</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity server truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi public domain đã tồn tại</exception>
        public void Create(Server server)
        {
            if (server == null)
            {
                throw new ArgumentNullException("server");
            }
            if (_serverRepository.Exist(ServerQuery.WithPublicDomain(server.PublicDomain)))
            {
                throw new Exception(string.Format(_resourceService.GetResource("Server.CreateOrEdit.Fields.PublicDomain.Exist"), server.PublicDomain));
            }
            _serverRepository.Create(server);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin domain
        /// </summary>
        /// <param name="server">Entity server</param>
        /// <param name="oldPublicDomain">Public domain trước khi cập nhật</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity server truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi public domain đã tồn tại</exception>
        public void Update(Server server, string oldPublicDomain)
        {
            if (server == null)
            {
                throw new ArgumentNullException("server");
            }
            var spec = ServerQuery.WithPublicDomain(server.PublicDomain).And(s => s.PublicDomain.ToLower() != oldPublicDomain.ToLower());
            if (_serverRepository.Exist(spec))
            {
                throw new Exception(string.Format(_resourceService.GetResource("Server.CreateOrEdit.Fields.PublicDomain.Exist"), server.PublicDomain));
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa server
        /// </summary>
        /// <param name="server">Entity server</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity server truyền vào bị null</exception>
        public void Delete(Server server)
        {
            if (server == null)
            {
                throw new ArgumentNullException("server");
            }
            _serverRepository.Delete(server);
            Context.SaveChanges();
        }
    }
}
