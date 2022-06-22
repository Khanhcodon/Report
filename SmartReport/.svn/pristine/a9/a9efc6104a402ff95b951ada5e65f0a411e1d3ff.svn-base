using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Bkav.eGovCloud.Business.Admin;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Api.Controller
{
    public class DomainController : EgovApiBaseController
    {
#if QuanTriTapTrungEdition
        private readonly DomainBll _domainService;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public DomainController()
        {

            _domainService = DependencyResolver.Current.GetService<DomainBll>();
        }

        /// <summary>
        /// Trả về danh sách các domain đang active của hệ thống
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        public IEnumerable<DomainDto> GetActives()
        {
            return _domainService.GetsAllWithCache().Where(d => d.IsActivated).Select(d => new DomainDto(){
                DomainId = d.DomainId,
                DomainName = d.DomainName
            });
        }
#else
        /// <summary>
        /// Khởi tạo
        /// </summary>
        public DomainController()
        {

        }

        /// <summary>
        /// Trả về danh sách các domain đang active của hệ thống
        /// </summary>
        /// <returns></returns>       
        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        public IEnumerable<Domain> GetActives()
        {
            var url = Request.RequestUri.Host;
            if (!Request.RequestUri.IsDefaultPort)
            {
                url += ":" + Request.RequestUri.Port;
            }

            return new List<Domain>(){
                new Domain()
                {
                    DomainId = 0,
                    DomainName = url
                }
            };
        }
#endif
    }
}