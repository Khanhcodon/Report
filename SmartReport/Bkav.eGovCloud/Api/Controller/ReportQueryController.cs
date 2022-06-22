using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Api.Controller
{
    //[OAuthAuthorizeAttribute(Scope.Form)]
    public class ReportQueryController : EgovApiBaseController
    {
        private readonly ReportQueryBll _reportQueryService;

        private readonly FormHelper _formHelper;

        public ReportQueryController()
        {
            _reportQueryService = DependencyResolver.Current.GetService<ReportQueryBll>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isReportQueryGroup"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public string GenerateQuery(bool isReportQueryGroup, int id)
        {
            if (isReportQueryGroup)
            {

            }
            else
            {
                
            }
            return string.Empty;
        }

        private string GenerateQueryByReportQueryId(int reportQueryId)
        {
            var query = string.Empty;
            var reportQuery = _reportQueryService.Get(reportQueryId);
            if (reportQuery != null)
            {
                var formCode = reportQuery.FormCode;
            }

            return query;
        }
    }
}