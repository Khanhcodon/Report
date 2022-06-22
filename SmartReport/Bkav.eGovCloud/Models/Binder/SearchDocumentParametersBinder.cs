using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Models.Binder
{
    public class SearchDocumentParametersBinder : IModelBinder
    {
        public const int DefaultPageSize = 10;
        private readonly Regex _facetRegex = new Regex("^f_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly Regex _facetTextRegex = new Regex("^ft_", RegexOptions.Compiled | RegexOptions.IgnoreCase);

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
            var qs = controllerContext.HttpContext.Request.QueryString;
            var qsDict = NameValueCollectionToDict(qs);
            int page;
            int pageSize;
            if (!int.TryParse(qs["page"], out page))
            {
                page = 1;
            }
            if (!int.TryParse(qs["pageSize"], out pageSize))
            {
                pageSize = DefaultPageSize;
            }
            var sp = new SearchDocumentParameters
            {
                FreeSearch = qs["query"],
                Page = page,
                PageSize = pageSize,
                Facets = qsDict.Where(k => _facetRegex.IsMatch(k.Key))
                    .Select(k => new KeyValuePair<string, string>(_facetRegex.Replace(k.Key, ""), k.Value))
                    .ToDictionary(k => k.Key, k => k.Value),
                FacetsText = qsDict.Where(k => _facetTextRegex.IsMatch(k.Key))
                    .Select(k => new KeyValuePair<string, string>(_facetTextRegex.Replace(k.Key, ""), k.Value))
                    .ToDictionary(k => k.Key, k => k.Value)
            };
            return sp;
        }
    }
}