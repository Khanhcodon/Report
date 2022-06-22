using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Models.Binder
{
    public class GetDocumentParametersBinder : IModelBinder
    {
        private readonly Regex _paramRegex = new Regex("^@", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static IDictionary<string, string> NameValueCollectionToDict(NameValueCollection nv)
        {
            var dict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var k in nv.AllKeys)
            {
                dict[k] = nv[k];
            }

            return dict;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var qsDict = NameValueCollectionToDict(controllerContext.HttpContext.Request.QueryString);
            var formDict = NameValueCollectionToDict(controllerContext.HttpContext.Request.Form);
            foreach (var key in formDict.Keys)
            {
                if (qsDict.ContainsKey(key))
                {
                    if (!string.IsNullOrEmpty(formDict[key]))
                    {
                        qsDict[key] = formDict[key];
                    }
                }
                else
                {
                    qsDict.Add(key, formDict[key]);
                }
            }

            var gp = new GetDocumentParameters
            {
                LastDate = null,
                Params = qsDict.Where(k => _paramRegex.IsMatch(k.Key)).ToDictionary(k => k.Key, k => k.Value),
                QuickSearch = string.Empty,
                Page = null,
                PageSize = null
            };
            if (qsDict.ContainsKey("lastDate"))
            {
                DateTime lastDate;
                if (DateTime.TryParse(qsDict["lastDate"], out lastDate))
                {
                    gp.LastDate = lastDate;
                }
            }
            if (qsDict.ContainsKey("quickSearch"))
            {
                gp.QuickSearch = qsDict["quickSearch"];
            }
            if (qsDict.ContainsKey("page"))
            {
                int page;
                if (int.TryParse(qsDict["page"], out page))
                {
                    gp.Page = page;
                }
            }
            if (qsDict.ContainsKey("pagesize"))
            {
                int pagesize;
                if (int.TryParse(qsDict["pagesize"], out pagesize))
                {
                    gp.PageSize = pagesize;
                }
            }
            return gp;
        }
    }
}