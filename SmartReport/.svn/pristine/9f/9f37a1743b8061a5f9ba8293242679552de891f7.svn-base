using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Api.Controller
{
    //[OAuthAuthorizeAttribute(Scope.Doctype)]
    public class DoctypeTemplateController : EgovApiBaseController
    {
        private readonly DoctypeTemplateBll _doctypeTemplateService;

        public DoctypeTemplateController()
        {
            _doctypeTemplateService = DependencyResolver.Current.GetService<DoctypeTemplateBll>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public DoctypeTemplate Get(int id)
        {
            return _doctypeTemplateService.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<DoctypeTemplate> GetsByDoctypeId(Guid doctypeId)
        {
            var doctypeTemplates = _doctypeTemplateService.GetsByDoctypeId(doctypeId);
            foreach (var doctypeTemplate in doctypeTemplates)
            {
                if (doctypeTemplate.OnlineTemplate != null)
                {
                    doctypeTemplate.FileId = doctypeTemplate.OnlineTemplate.FileId;
                }
            }
            return doctypeTemplates;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<DoctypeTemplate> GetsAll()
        {
            return _doctypeTemplateService.Gets(true, null);
        }

    }
}