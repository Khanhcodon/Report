using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Search;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace Bkav.eGovCloud.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SmartReportController : CustomerBaseController
    {
        private readonly AdminGeneralSettings _generalSettings;
        private readonly EgovSearch _searchService;
        private readonly Helper.UserSetting _helperUserSetting;
        private readonly ISearchInDatabase _searchInDatabaseService;
        private readonly ISearchInSolr _searchInSolrService;
        private readonly TemplateKeyBll _templateKeyService;

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        /// <param name="generalSettings"></param>
        /// <param name="searchService"></param>
        /// <param name="searchInDatabaseService"></param>
        /// <param name="searchInSolrService"></param>
        public SmartReportController(AdminGeneralSettings generalSettings, EgovSearch searchService,
            ISearchInDatabase searchInDatabaseService, ISearchInSolr searchInSolrService,
                                Helper.UserSetting helperUserSetting, TemplateKeyBll templateKeyService)
        {
            _generalSettings = generalSettings;
            _searchService = searchService;
            _searchInDatabaseService = searchInDatabaseService;
            _searchInSolrService = searchInSolrService;
            _helperUserSetting = helperUserSetting;
            _templateKeyService = templateKeyService;
        }

        public ActionResult Forecast()
        {
            return View();
        }
        public JsonResult ForecastData(string timetype, string indicator, string locality)
        {
            var query = @"SELECT e.TypeData, e.Measure, e.TimeKey, e.TimeType, e.IndicatorKey, 
                        e.OrganizationKey from fact_eform_model e WHERE e.IndicatorKey = @indicator and e.LocalityKey = @locality 
                        and e.TimeType = @timetype";
            var param = new List<object>
            {
                new SqlParameter("@indicator", indicator),
                new SqlParameter("@locality", locality),
                new SqlParameter("@timetype", timetype)
            };

            var arrPara = param.ToArray();
            var dataThucHien = _templateKeyService.GetDataByQuery(query, arrPara);
            return Json(dataThucHien, JsonRequestBehavior.AllowGet);
        }

        public JsonResult test_spell(string Str)
        {
            var response = "{\n\t\"error\": \"" + Str + "\"\n}";
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public JsonResult test_grammar(string Str)
        {
            var response = "{\n\t\"error\": \"" + Str + "\"\n}";
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult spell(string str)
        //{
        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri("http://172.36.68.113:5000/check_spell");
        //    client.DefaultRequestHeaders.
        //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    client.Timeout = TimeSpan.FromSeconds(30);
        //    var content = new ObjectContent(data.GetType(), data, new JsonMediaTypeFormatter());
        //    var responseMessage = client.PostAsync("", content).Result;
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var result = JsonConvert.DeserializeObject<bool>(responseMessage.Content.ReadAsStringAsync().Result);
        //        return result;
        //    }
        //}
    }
}