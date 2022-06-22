using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;

namespace Bkav.eGovCloud.Api.Controller
{
    //[OAuthAuthorizeAttribute(Scope.Doctype)]
    public class DoctypePaperController : EgovApiBaseController
    {
        private readonly DocTypeBll _doctypeService;
        private readonly PaperBll _paperService;
        private readonly DoctypePaperBll _doctypePaperService;

        public DoctypePaperController()
        {
            _paperService = DependencyResolver.Current.GetService<PaperBll>();
            _doctypeService = DependencyResolver.Current.GetService<DocTypeBll>();
            _doctypePaperService = DependencyResolver.Current.GetService<DoctypePaperBll>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<DoctypePaper> GetsByDoctypeId(Guid doctypeId)
        {
            var results = new List<DoctypePaper>();
            var doctype = _doctypeService.Get(doctypeId);

            if (doctype != null)
            {
                foreach (var doctypePaper in doctype.DoctypePapers)
                {
                    var item = new DoctypePaper();
                    var paper = _paperService.Get(doctypePaper.PaperId);
                    if (paper != null)
                    {
                        item.PaperName = paper.PaperName;
                        item.DocTypeId = doctypePaper.DocTypeId;
                        item.IsRequired = doctypePaper.IsRequired.HasValue && doctypePaper.IsRequired.Value;
                        item.Amount = 1;

                        results.Add(item);
                    }
                }
            }

            return results;
        }

    }
}