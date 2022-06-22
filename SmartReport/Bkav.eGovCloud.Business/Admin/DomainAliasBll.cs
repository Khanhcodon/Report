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
    /// Class : DomainAliasBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : BLL tương ứng với bảng DomainAlias trong CSDL
    /// </summary>
    public class DomainAliasBll : ServiceBase
    {
        private readonly IRepository<DomainAlias> _domainAliasRepository;
        private readonly ResourceBll _resourceService;

        ///<summary>
        /// Khởi tạo class <see cref="DomainAliasBll"/>.
        ///</summary>
        ///<param name="context">Context</param>
        ///<param name="resourceService">BLL tương ứng với bảng Resource trong CSDL</param>
        public DomainAliasBll(IDbAdminContext context, ResourceBll resourceService)
            : base(context)
        {
            _domainAliasRepository = Context.GetRepository<DomainAlias>();
            _resourceService = resourceService;
        }

        /////<summary>
        ///// Khởi tạo class <see cref="DomainAliasBll"/>.
        /////</summary>
        /////<param name="context">Context</param>
        /////<param name="resourceService">BLL tương ứng với bảng Resource trong CSDL</param>
        //public DomainAliasBll(IDbCustomerContext context, ResourceBll resourceService)
        //    : base(context)
        //{
        //    _domainAliasRepository = Context.GetRepository<DomainAlias>();
        //    _resourceService = resourceService;
        //}

        /// <summary>
        /// Lấy ra tất cả đường dẫn phù hợp với các điều kiện truyền vào. Nếu tất cả các điều kiện đều là null thì sẽ lấy ra tất cả các đường dẫn
        /// </summary>
        /// <param name="domainId">Id của domain</param>
        /// <param name="alias">Đường dẫn</param>
        /// <param name="isPrimary">Là đường dẫn chính hay phụ (Đường dẫn chính: true, ngược lại: false)</param>
        /// <returns>Danh sách đường dẫn phù hợp với điều kiện</returns>
        public IEnumerable<DomainAlias> Gets(int? domainId = null, string alias = null, bool? isPrimary = null)
        {
            var spec = domainId.HasValue
                        ? DomainAliasQuery.WithId(domainId.Value)
                        : null;
            spec = spec != null
                        ? !string.IsNullOrWhiteSpace(alias)
                            ? spec.And(DomainAliasQuery.WithAlias(alias))
                            : spec
                        : !string.IsNullOrWhiteSpace(alias)
                            ? DomainAliasQuery.WithAlias(alias)
                            : null;
            spec = spec != null
                        ? isPrimary.HasValue
                            ? spec.And(DomainAliasQuery.WithIsPrimary(isPrimary.Value))
                            : spec
                        : isPrimary.HasValue
                            ? DomainAliasQuery.WithIsPrimary(isPrimary.Value)
                            : null;
            return _domainAliasRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra đường dẫn theo id
        /// </summary>
        /// <param name="domainId">Id của đường dẫn</param>
        /// <returns>Entity đường dẫn</returns>
        public DomainAlias Get(int domainId)
        {
            DomainAlias result = null;
            if (domainId > 0)
            {
                result = _domainAliasRepository.Get(domainId);
            }
            return result;
        }

        /// <summary>
        /// Lấy ra đường dẫn theo alias
        /// </summary>
        /// <param name="alias">Http alias của đường dẫn</param>
        /// <returns>Entity đường dẫn</returns>
        public DomainAlias Get(string alias)
        {
            DomainAlias result = null;
            if (!string.IsNullOrWhiteSpace(alias))
            {
                result = _domainAliasRepository.Get(false, a => a.Alias == alias);
            }
            return result;
        }

        /// <summary>
        /// Tạo mới đường dẫn
        /// </summary>
        /// <param name="domainAlias">Entity đường dẫn</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity đường dẫn truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi tên đường dẫn đã tồn tại</exception>
        public void Create(DomainAlias domainAlias)
        {
            if (domainAlias == null)
            {
                throw new ArgumentNullException("domainAlias");
            }
            if (_domainAliasRepository.Exist(DomainAliasQuery.WithAlias(domainAlias.Alias)))
            {
                throw new Exception(string.Format(_resourceService.GetResource("DomainAlias.CreateOrEdit.Fields.Alias.Exist"), domainAlias.Alias));
            }
            _domainAliasRepository.Create(domainAlias);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin đường dẫn
        /// </summary>
        /// <param name="domainAlias">Entity đường dẫn</param>
        /// <param name="oldAlias">Tên đường dẫn trước khi cập nhật</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity đường dẫn truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi tên đường dẫn đã tồn tại</exception>
        public void Update(DomainAlias domainAlias, string oldAlias)
        {
            if (domainAlias == null)
            {
                throw new ArgumentNullException("domainAlias");
            }
            if (_domainAliasRepository.Exist(DomainAliasQuery.WithAlias(domainAlias.Alias).And(a => a.Alias.ToLower() != oldAlias.ToLower())))
            {
                throw new Exception(string.Format(_resourceService.GetResource("DomainAlias.CreateOrEdit.Fields.Alias.Exist"), domainAlias.Alias));
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa đường dẫn
        /// </summary>
        /// <param name="domainAlias">Entity đường dẫn</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity đường dẫn truyền vào bị null</exception>
        public void Delete(DomainAlias domainAlias)
        {
            if (domainAlias == null)
            {
                throw new ArgumentNullException("domainAlias");
            }
            _domainAliasRepository.Delete(domainAlias);
            Context.SaveChanges();
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của đường dẫn với điều kiện truyền vào
        /// </summary>
        /// <param name="alias">Tên đường dẫn cần kiểm tra</param>
        /// <returns></returns>
        public bool Exist(string alias)
        {
            var result = false;
            if (!string.IsNullOrWhiteSpace(alias))
            {
                result = _domainAliasRepository.Exist(DomainAliasQuery.WithAlias(alias));
            }
            return result;
        }
    }
}
