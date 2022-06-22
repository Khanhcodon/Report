using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Admin;
namespace Bkav.eGovCloud.Business.Admin
{
    /// <summary>
    /// Quản lý các website
    /// </summary>
    public class SiteBll : ServiceBase
    {
        private readonly IRepository<Site> _siteRepository;
        private readonly ResourceBll _resourceService;

        ///<summary>
        /// Khởi tạo class <see cref="DomainBll"/>.
        ///</summary>
        ///<param name="context">Context</param>
        ///<param name="resourceService">BLL tương ứng với bảng Resource trong CSDL</param>
        public SiteBll(IDbAdminContext context, ResourceBll resourceService)
            : base(context)
        {
            _siteRepository = Context.GetRepository<Site>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Trả về tất cả các site trong hệ thống
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Site> Gets()
        {
            return _siteRepository.Gets(true);
        }

        /// <summary>
        /// Trả về website theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Site Get(int id)
        {
            return _siteRepository.Get(id);
        }

        /// <summary>
        /// Tạo site mới
        /// </summary>
        /// <param name="site"></param>
        public void Create(Site site)
        {
            if (site == null)
            {
                throw new ArgumentException("Site");
            }

            _siteRepository.Create(site);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa một web site
        /// </summary>
        /// <param name="site"></param>
        public void Delete(Site site)
        {
            _siteRepository.Delete(site);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin
        /// </summary>
        /// <param name="site"></param>
        public void Update(Site site)
        {
            Context.SaveChanges();
        }
    }
}
