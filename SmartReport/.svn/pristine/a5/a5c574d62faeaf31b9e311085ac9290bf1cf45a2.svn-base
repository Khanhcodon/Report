using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Admin.Models;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using Bkav.eGovCloud.Core.FileSystem;
using System.Text;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Business;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Formatting;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using ActiveQueryBuilder.Core;
using ActiveQueryBuilder.Web.Server;
using ActiveQueryBuilder.Core.QueryTransformer;

using MySql.Data;
using MySql.Data.MySqlClient;


namespace Bkav.eGovCloud.Areas.Admin.Controllers
{
    [EgovAuthorize]
    //[RequireHttps]
    public class QueryController : CustomController
    {
        private readonly DocTypeBll _docTypeService;
        private readonly FormBll _formService;
        private readonly DocFieldBll _docfieldService;
        private readonly DocTypeFormBll _doctypeFormService;
        private readonly CatalogBll _catalogService;
        private readonly CatalogValueBll _catalogValueService;
        private readonly ExtendFieldBll _exfieldService;
        private readonly ResourceBll _resourceService;
        private readonly FormGroupBll _formgroupService;
        private readonly ConfigTypeBll _configTypeService;
        private readonly DepartmentBll _departmentService;

        private readonly ReportBll _reportService;
        private readonly ReportKeyBll _reportKeyService;


        private readonly AdminGeneralSettings _generalSettings;
        private readonly FileUploadSettings _fileUploadSettings;

        private const string DEFAULT_SORT_BY = "IsActivated";
        private const int FORM_CATEGORY_SUMMARY = 3;
        public QueryController(
            DocTypeBll docTypeBll,
            FormBll formService,
            DocFieldBll docfieldService,
            CatalogBll catalogService,
            ExtendFieldBll exfieldService,
            ResourceBll resourceService,
            FormGroupBll formgroupservice,
            DocTypeFormBll doctypeFormService,
            AdminGeneralSettings generalSettings,
            FileUploadSettings fileUploadSettings,
            ConfigTypeBll configTypeService,
            DepartmentBll departmentService,
            ReportBll reportService,
            ReportKeyBll reportKeyService)
            : base()
        {
            _docTypeService = docTypeBll;
            _formService = formService;
            _docfieldService = docfieldService;
            _catalogService = catalogService;
            _exfieldService = exfieldService;
            _resourceService = resourceService;
            _formgroupService = formgroupservice;
            _generalSettings = generalSettings;
            _fileUploadSettings = fileUploadSettings;
            _doctypeFormService = doctypeFormService;
            _configTypeService = configTypeService;
            _departmentService = departmentService;
            _reportService = reportService;
            _reportKeyService = reportKeyService;
        }
        

        public ActionResult ConfigReport(int id)
        {
            var report = _reportService.Get(id);
            var qb = QueryBuilderStore.Get("BootstrapTheming");

            if (qb == null)
                qb = CreateQueryBuilderReport(report);

            return View(qb);
        }
        public ActionResult ConfigReportKey(int id)
        {
            var reportKey = _reportKeyService.Get(id);
            var qb = CreateQueryBuilderReportKey(reportKey);
            return View("ConfigReport",qb);
        }
        private QueryBuilder CreateQueryBuilderReportKey(ReportKey report)
        {

            var connection = new MySqlConnection(_generalSettings.DashboardConnection);
            var qb = QueryBuilderStore.Get("BootstrapTheming");
            var queryBuilder = qb ?? QueryBuilderStore.Factory.MySql("BootstrapTheming");
            queryBuilder.MetadataProvider = new MySQLMetadataProvider
            {
                Connection = connection
            };
            if (report == null || string.IsNullOrEmpty(report.Sql)) return queryBuilder;
            queryBuilder.SQL = report.Sql.Replace("dashboard:", "");
            return queryBuilder;
        }
        public ActionResult ConfigDocType(Guid formId)
        {

            var form = _formService.Get(formId);
            var qb = CreateQueryBuilderDoc(form);
            return View(qb);
        }
        private QueryBuilder CreateQueryBuilderDoc(Form form)
        {
            var connection = new MySqlConnection(_generalSettings.DashboardConnection);
            var qb = QueryBuilderStore.Get("BootstrapTheming");
            var queryBuilder = qb ?? QueryBuilderStore.Factory.MySql("BootstrapTheming");
            queryBuilder.MetadataProvider = new MySQLMetadataProvider
            {
                Connection = connection
            };
            if (form == null || string.IsNullOrEmpty(form.FormCodeCompilation) ||
                form.FormCategoryId != FORM_CATEGORY_SUMMARY) return queryBuilder;
            var d = JsonConvert.DeserializeObject<dynamic>(form.FormCodeCompilation);
            var config = d.summaryConfigJsonForm;
            var query = new StringBuilder();
            if (config.schema.Form_sql != null)
                query = new StringBuilder(config.schema.Form_sql["default"].ToString());
            queryBuilder.SQL = query.ToString().Replace("dashboard:", "");
            return queryBuilder;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConfigReportForm()
        {
            var qb = QueryBuilderStore.Get("BootstrapTheming");

            if (qb == null)
                qb = CreateQueryBuilder();

            return View(qb);
        }

        public ActionResult ConfigChart()
        {
            var qb = QueryBuilderStore.Get("BootstrapTheming");

            if (qb == null)
                qb = CreateQueryBuilder();

            return View(qb);
        }


        [HttpPost]
        public JsonResult GetDataConfigReport(string sql)
        {
            var data = Execute(sql);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        private IEnumerable<IDictionary<string, object>> Execute(string sql)
        {
            var data = _formService.GetDataForm(sql, null);
            return data;
        }

        private QueryBuilder CreateQueryBuilderReport(Report report)
        {
            MySqlConnection connection = new MySqlConnection(_generalSettings.DashboardConnection);

            // Create an instance of the QueryBuilder object
            var queryBuilder = QueryBuilderStore.Factory.MySql("BootstrapTheming");

            // Denies metadata loading requests from the metadata provider
            //queryBuilder.MetadataLoadingOptions.OfflineMode = true;
            queryBuilder.MetadataProvider = new MySQLMetadataProvider
            {
                Connection = connection
            };
            if (report != null && string.IsNullOrEmpty(report.QueryStatistics))
            {
                queryBuilder.SQL = report.QueryStatistics.Replace("dashboard:", "");
            }
            //Set default query

            return queryBuilder;
        }

        private QueryBuilder CreateQueryBuilder(string sql = null)
        {
            MySqlConnection connection = new MySqlConnection(_generalSettings.DashboardConnection);

            // Create an instance of the QueryBuilder object
            var queryBuilder = QueryBuilderStore.Factory.MySql("BootstrapTheming");

            // Denies metadata loading requests from the metadata provider
            //queryBuilder.MetadataLoadingOptions.OfflineMode = true;
            queryBuilder.MetadataProvider = new MySQLMetadataProvider
            {
                Connection = connection
            };

            sql = string.IsNullOrEmpty(sql) ? @"Select fact_baocaonongnghiep.giatri As giatri ,
                                  fact_baocaonongnghiep.chitiet As chitiet ,
                                  fact_baocaonongnghiep.chitieu As chitieu ,
                                  fact_baocaonongnghiep.timekey As timekey ,
                                  dim_time.month_name As month_name ,
                                  dim_time.year_name As year_name ,
                                  dim_time.quarter As quarter ,
                                  dim_organize.level4_name As level4_name ,
                                  dim_organize.level3_name As level3_name ,
                                  dim_organize.level2_name As level2_name ,
                                  dim_organize.level1_name As level1_name 
                                  From fact_baocaonongnghiep
                                  Inner Join dim_organize On fact_baocaonongnghiep.organizekey =
                                  dim_organize.organizekey
                                  Inner Join dim_time On fact_baocaonongnghiep.timekey = dim_time.id" : sql;
            //Set default query
            queryBuilder.SQL = sql;

            return queryBuilder;
        }
        private QueryTransformer CreateQueryTransformer(SQLQuery query)
        {
            var qt = QueryTransformerStore.Create("BootstrapTheming");

            qt.QueryProvider = query;
            qt.AlwaysExpandColumnsInQuery = true;

            return qt;
        }
    }
}
