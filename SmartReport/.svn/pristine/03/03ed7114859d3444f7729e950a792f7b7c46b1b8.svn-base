using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Api.Controller
{
    //[OAuthAuthorizeAttribute(Scope.Doctype)]
    public class DoctypeFormController : EgovApiBaseController
    {
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly FormBll _formService;

        public DoctypeFormController()
        {
            _doctypeFormService = DependencyResolver.Current.GetService<DocTypeFormBll>();
            _formService = DependencyResolver.Current.GetService<FormBll>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public DocTypeForm Get(int id)
        {
            return _doctypeFormService.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<Form> GetsByDoctypeId(Guid doctypeId)
        {
            var forms = new List<Form>();
            var doctypeForms = _doctypeFormService.GetsByDoctypeId(doctypeId);
            foreach (var doctypeForm in doctypeForms)
            {
                var form = _formService.Get(doctypeForm.FormId);
                if (form != null)
                {
                    forms.Add(form);
                }
            }
            return forms;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<DocTypeForm> GetsAll()
        {
            return _doctypeFormService.Gets(null);
        }

    }
}