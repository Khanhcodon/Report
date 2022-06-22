using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
//using Bkav.eGovCloud.Oauth;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Api.Controller
{
    //[OAuthAuthorizeAttribute(Scope.Form)]
    public class FormController : EgovApiBaseController
    {
        private readonly FormBll _formService;
        private readonly DocTypeFormBll _doctypeFormService;

        private readonly FormHelper _formHelper;

        public FormController()
        {
            _formService = DependencyResolver.Current.GetService<FormBll>();
            _doctypeFormService = DependencyResolver.Current.GetService<DocTypeFormBll>();
            _formHelper = DependencyResolver.Current.GetService<FormHelper>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public Form Get(Guid id)
        {
            return _formService.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<Form> GetsByDoctypeId(Guid doctypeId)
        {
            var doctypeForms = _doctypeFormService.GetsByDoctypeId(doctypeId).Where(dt => dt.IsPrimary);
            if (!doctypeForms.Any())
            {
                return new List<Form>();
            }

            var formIds = doctypeForms.Select(d => d.FormId).ToList();
            var forms =_formService.Gets(x => formIds.Contains(x.FormId));
            foreach(var form in forms){
                form.Json = _formHelper.ParseFormModel(form).StringifyJs();
            }

            return forms;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<Form> GetsAll()
        {
            return _formService.Gets(null);
        }

    }
}