using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;

namespace Bkav.eGovCloud.Api.Controller
{
    //[OAuthAuthorizeAttribute(Scope.Law)]
    public class LawController : EgovApiBaseController
    {
        private readonly LawBll _lawService;
        private readonly DocTypeBll _doctypeService;

        public LawController()
        {
            _lawService = DependencyResolver.Current.GetService<LawBll>();
            _doctypeService = DependencyResolver.Current.GetService<DocTypeBll>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public Law Get(string numberSign)
        {
            return _lawService.Get(numberSign);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<Law> GetsByDoctypeId(Guid doctypeId)
        {
            var laws = _doctypeService.GetsDoctypeLaw(doctypeId).Select(x => x.Law);
            foreach (var law in laws)
            {
                if (law != null)
                {
                    foreach (var file in law.Files)
                    {
                        law.FileIds += file.FileId + ",";
                    }
                }
                
            }
            return laws;
        }

    }
}