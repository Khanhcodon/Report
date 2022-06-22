using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Api.Controller
{
    //[OAuthAuthorizeAttribute(Scope.Docfield)]
    public class DocFieldController : EgovApiBaseController
    {
        private readonly DocFieldBll _docfieldService;

        public DocFieldController()
        {
            _docfieldService = DependencyResolver.Current.GetService<DocFieldBll>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public DocField Get(int id)
        {
            return _docfieldService.Get(id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<DocField> GetsAll()
        {
            return _docfieldService.Gets(x => x.CategoryBusinessId == 4 && x.IsActivated == true);
        }
    }
}