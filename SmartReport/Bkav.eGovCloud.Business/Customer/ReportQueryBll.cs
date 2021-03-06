using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Entities.Common;
using MySql.Data.MySqlClient;
using Bkav.eGovCloud.Business.BI.ParseQuery;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using ActionLevel = Bkav.eGovCloud.Entities.Customer.ActionLevel;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// </summary>
    public class ReportQueryBll : ServiceBase
    {
        private readonly IRepository<ReportQuery> _reportQueryRepository;
        private readonly IRepository<ReportQueryFilter> _reportQueryFilterRepository;
        private readonly IRepository<E_DataTable> _dataTableRepository;
        private readonly IRepository<DataField> _dataFieldRepository;
        private readonly IRepository<ReportQueryGroup> _reportQueryGroupRepository;
        private readonly IRepository<ReportQueryGroupReportQuery> _reportQueryGroupReportQueryRepository;
        private readonly IRepository<Relation> _relationRepository;

        private readonly IRepository<TemplateKeyCategory> _templateKeyCategoryRepository;
        private readonly IRepository<ActionLevel> _actionLevelRepository;

        private string[] AggregateStandardFunctions = new string[] { "AVG", "COUNT", "MIN", "MAX", "SUM" };
        private string[] AggregateCustomFunctions = new string[] { "Lũy kế", "Cùng kỳ", "SS Cùng kỳ", "SS Cùng kỳ (%)", "Kỳ trước", "SS Kỳ trước", "SS Kỳ trước (%)" };

        private string XOAY_COT_NAME = "Cột";
        private string XOAY_GIATRI_NAME = "Giá trị";
        private enum COLUMN_INDEX
        {
            VISIBLE = 1,
            FUNCTION = 2,
            FIELD_NAME = 3,
            FIELD_ALIAS = 4,
            GROUP = 5,
            FILTER = 6,
            ORDER = 7,
            XOAY_SPECIFIC = 8,
            XOAY_ALIAS = 9
        }

        private readonly AdminGeneralSettings _generalSettings;

        private StringBuilder tempStringBuilder = new StringBuilder();
        private readonly IRepository<DataSource> _dataSourceRepository;

        /// <summary>
        /// Khởi tạo class <see cref="DepartmentBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="userService">Bll tương ứng với bảng User trong CSDL</param>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="generalSettings"></param>
        //TODO: Anh GiangPN - Tham số đầu vào IPositionDal positionDal nên thay thành PositionBll và IJobTitlesDal jobTitlesDal nên thay thành JobTitleBll
        public ReportQueryBll(IDbCustomerContext context,
                    AdminGeneralSettings generalSettings)
            : base(context)
        {
            _reportQueryRepository = Context.GetRepository<ReportQuery>();
            _reportQueryFilterRepository = Context.GetRepository<ReportQueryFilter>();
            _dataFieldRepository = Context.GetRepository<DataField>();
            _dataTableRepository = Context.GetRepository<E_DataTable>();
            _templateKeyCategoryRepository = Context.GetRepository<TemplateKeyCategory>();
            _reportQueryGroupRepository = Context.GetRepository<ReportQueryGroup>();
            _reportQueryGroupReportQueryRepository = Context.GetRepository<ReportQueryGroupReportQuery>();
            _relationRepository = Context.GetRepository<Relation>();
            _generalSettings = generalSettings;
            _dataSourceRepository = Context.GetRepository<DataSource>();
            _actionLevelRepository = Context.GetRepository<ActionLevel>();
        }

        #region ReportQuery
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportQueryId"></param>
        /// <returns></returns>
        public ReportQuery Get(int reportQueryId)
        {
            ReportQuery reportQuery = null;
            if (reportQueryId > 0)
            {
                reportQuery = _reportQueryRepository.Get(reportQueryId);
            }

            return reportQuery;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportQuery"></param>
        public void Create(ReportQuery reportQuery)
        {
            if (reportQuery == null)
            {
                throw new ArgumentNullException("ReportQuery");
            }

            _reportQueryRepository.Create(reportQuery);
            Context.SaveChanges();
        }

        public void Update(ReportQuery reportQuery, List<ReportQueryFilter> filters)
        {
            if (reportQuery == null)
            {
                throw new ArgumentNullException("ReportQuery");
            }

            var index = 0;
            foreach (var filter in reportQuery.Filters)
            {
                filter.IsFilter = filters.ElementAt(index).IsFilter;
                filter.IsSummary = filters.ElementAt(index).IsSummary;
                index++;
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportQuery"></param>
        public void Delete(ReportQuery reportQuery)
        {
            if (reportQuery == null)
            {
                throw new ArgumentNullException("ReportQuery");
            }

            var relations = _reportQueryGroupReportQueryRepository.Gets(true, p => p.ReportQueryId == reportQuery.ReportQueryId);

            foreach (var relation in relations)
            {
                _reportQueryGroupReportQueryRepository.Delete(relation);
            }

            _reportQueryRepository.Delete(reportQuery);
            Context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<ReportQuery> Gets(Expression<Func<ReportQuery, bool>> spec = null)
        {
            return _reportQueryRepository.GetsReadOnly(spec);
        }
        #endregion ReportQuery

        #region ReportQueryFilter
        public IEnumerable<ReportQueryFilter> GetFilters(Expression<Func<ReportQueryFilter, bool>> spec = null)
        {
            return _reportQueryFilterRepository.GetsReadOnly(spec);
        }

        #endregion ReportQueryFilter

        #region GenerateQuery
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GenerateQueryByGroup(List<ReportQuery> reportQuerys)
        {
            var query = new StringBuilder();

            var index = 0;
            foreach (var reportQuery in reportQuerys)
            {
                if (index != 0)
                    query.Append(" UNION ALL ");
                query.Append(GenerateQuery(reportQuery));
                index++;
            }

            return query.ToString(); ;
        }

        #endregion

        #region private method

        private string generateSelectQuery(List<ReportQueryFilter> filters, string strFormCode, string formulaColumnName, string tableName,
            string tableKeyName, string timeKeyFieldName,
            int actionLevelId, int subNumber,
            out JArray xoayCot,
            out List<JArray> xoayGiaTris)
        {
            xoayCot = new JArray();
            xoayGiaTris = new List<JArray>();
            var query = new StringBuilder();
            dynamic formCode = JsonConvert.DeserializeObject(strFormCode);
            var index = 0;
            var roundInfos = RoundInfos();

            foreach (var dataDetail in formCode.data)
            {
                if (dataDetail[(int)COLUMN_INDEX.VISIBLE] == true)
                {
                    if (dataDetail.Count > 8)
                    {
                        if (dataDetail[(int)COLUMN_INDEX.XOAY_SPECIFIC].ToString() == XOAY_COT_NAME)
                        {
                            xoayCot = dataDetail;
                            continue;
                        }
                        else if (dataDetail[(int)COLUMN_INDEX.XOAY_SPECIFIC].ToString() == XOAY_GIATRI_NAME)
                        {
                            xoayGiaTris.Add(dataDetail);
                            continue;
                        }
                    }

                    if (index != 0)
                        query.Append(",");

                    if (dataDetail[(int)COLUMN_INDEX.FUNCTION] == "None")
                        query.AppendFormat(" {0} as '{1}'", dataDetail[(int)COLUMN_INDEX.FIELD_NAME], dataDetail[(int)COLUMN_INDEX.FIELD_ALIAS]);
                    else if (AggregateCustomFunctions.Any(p => p == (string)dataDetail[(int)COLUMN_INDEX.FUNCTION]))
                    {
                        if (dataDetail[(int)COLUMN_INDEX.FUNCTION] == "Lũy kế")
                        {
                            query.AppendFormat(" (SELECT");
                            if (!string.IsNullOrEmpty(formulaColumnName))
                            {
                                query.AppendFormat(" (CASE WHEN LOWER({0}) = LOWER('MAX') THEN MAX(bc.{1})", formulaColumnName, ((string)dataDetail[(int)COLUMN_INDEX.FIELD_NAME]).Split('.')[1]);
                                query.AppendFormat(" WHEN LOWER(bc.{0}) = LOWER('MIN') THEN MIN(bc.{1})", formulaColumnName, ((string)dataDetail[(int)COLUMN_INDEX.FIELD_NAME]).Split('.')[1]);
                                query.AppendFormat(" WHEN LOWER(bc.{0}) = LOWER('AVG') THEN AVG(bc.{1})", formulaColumnName, ((string)dataDetail[(int)COLUMN_INDEX.FIELD_NAME]).Split('.')[1]);
                                query.AppendFormat(" WHEN LOWER(bc.{0}) = LOWER('SUM') THEN SUM(bc.{1})", formulaColumnName, ((string)dataDetail[(int)COLUMN_INDEX.FIELD_NAME]).Split('.')[1]);
                                query.AppendFormat(" WHEN LOWER(bc.{0}) = LOWER('COUNT') THEN COUNT(bc.{1})", formulaColumnName, ((string)dataDetail[(int)COLUMN_INDEX.FIELD_NAME]).Split('.')[1]);
                                query.AppendFormat(" ELSE");
                            }

                            query.AppendFormat(" SUM(bc.{0}) ", ((string)dataDetail[(int)COLUMN_INDEX.FIELD_NAME]).Split('.')[1]);
                            if (!string.IsNullOrEmpty(formulaColumnName))
                                query.AppendFormat(" END)");

                            query.AppendFormat(" as '{0}'", (string)dataDetail[(int)COLUMN_INDEX.FIELD_ALIAS]);
                            query.AppendFormat(" FROM {0} bc", tableName);
                            query.AppendFormat(" Inner Join @LuyKeKey as {0} On {0}.id = bc.{1}", tableKeyName, timeKeyFieldName);
                            query.AppendFormat(" Where bc.madinhdanh = {0}.madinhdanh", tableName);
                            query.AppendFormat(" And (bc.organizekey @OrganizeKeyRep) And (bc.timekey @TimeKeySS )");
                            query.AppendFormat(" Group By bc.madinhdanh)");
                            query.AppendFormat(" as '{0}'", (string)dataDetail[(int)COLUMN_INDEX.FIELD_ALIAS]);
                        }
                        else if (dataDetail[(int)COLUMN_INDEX.FUNCTION] == "Kỳ trước" || dataDetail[(int)COLUMN_INDEX.FUNCTION] == "SS Kỳ trước" || dataDetail[(int)COLUMN_INDEX.FUNCTION] == "SS Kỳ trước (%)")
                        {
                            var subQuery = new StringBuilder();

                            subQuery.AppendFormat(" SUM((Select bc.{0}", ((string)dataDetail[(int)COLUMN_INDEX.FIELD_NAME]).Split('.')[1]);
                            subQuery.AppendFormat(" From {0} bc", tableName);
                            subQuery.AppendFormat(" Where bc.madinhdanh = {0}.madinhdanh", tableName);
                            subQuery.AppendFormat(" And (bc.organizekey @OrganizeKeyRep) ");

                            subQuery.AppendFormat(" And GetKyTruoc(@TimeKeyValue, {0}) = bc.{1}))", actionLevelId, timeKeyFieldName);

                            if (dataDetail[(int)COLUMN_INDEX.FUNCTION] == "SS Kỳ trước (%)")
                            {
                                query = query.AppendFormat(" ((SUM({0}) - {1})/ {1}) * 100", (string)dataDetail[(int)COLUMN_INDEX.FIELD_NAME], subQuery);
                            }
                            else if (dataDetail[(int)COLUMN_INDEX.FUNCTION] == "SS Kỳ trước")
                            {
                                query = query.AppendFormat(" (SUM({0}) - {1})", (string)dataDetail[(int)COLUMN_INDEX.FIELD_NAME], subQuery);
                            }
                            else
                            {
                                query.AppendFormat(" {0}", subQuery);
                            }

                            query.AppendFormat(" as '{0}'", (string)dataDetail[(int)COLUMN_INDEX.FIELD_ALIAS]);
                        }
                        else if (dataDetail[(int)COLUMN_INDEX.FUNCTION] == "Cùng kỳ" || dataDetail[(int)COLUMN_INDEX.FUNCTION] == "SS Cùng kỳ" || dataDetail[(int)COLUMN_INDEX.FUNCTION] == "SS Cùng kỳ (%)")
                        {
                            var subQuery = new StringBuilder();

                            subQuery.AppendFormat(" SUM((Select bc.{0}", ((string)dataDetail[(int)COLUMN_INDEX.FIELD_NAME]).Split('.')[1]);
                            subQuery.AppendFormat(" From {0} bc", tableName);
                            subQuery.AppendFormat(" Where bc.madinhdanh = {0}.madinhdanh", tableName);
                            subQuery.AppendFormat(" And (bc.organizekey @OrganizeKeyRep)");

                            subQuery.AppendFormat(" AND {0}.{2} - {1} = bc.{2}))", tableName, subNumber, timeKeyFieldName);

                            if (dataDetail[(int)COLUMN_INDEX.FUNCTION] == "SS Cùng kỳ (%)")
                            {
                                query = query.AppendFormat(" ((SUM({0}) - {1})/ {1}) * 100", (string)dataDetail[(int)COLUMN_INDEX.FIELD_NAME], subQuery);
                            }
                            else if (dataDetail[(int)COLUMN_INDEX.FUNCTION] == "SS Cùng kỳ")
                            {
                                query = query.AppendFormat(" (SUM({0}) - {1})", (string)dataDetail[(int)COLUMN_INDEX.FIELD_NAME], subQuery);
                            }
                            else
                            {
                                query.AppendFormat(" {0}", subQuery);
                            }

                            query.AppendFormat(" as '{0}'", (string)dataDetail[(int)COLUMN_INDEX.FIELD_ALIAS]);
                        }
                    }
                    else
                    {
                        // AggregateStandardFunctions
                        query.AppendFormat(" {0}({1})", dataDetail[(int)COLUMN_INDEX.FUNCTION], dataDetail[(int)COLUMN_INDEX.FIELD_NAME]);
                        query.AppendFormat(" as '{0}'", (string)dataDetail[(int)COLUMN_INDEX.FIELD_ALIAS]);
                    }

                    index++;
                }
            }

            return query.ToString();
        }

        private string MultipleReplace(string text, Dictionary<string, string> replacements)
        {
            return Regex.Replace(text,
                                    "(" + String.Join("|", replacements.Keys.ToArray()) + ")",
                                    delegate (Match m) { return replacements[m.Value]; }
                                    );
        }

        private DataTable getDistinctValuesByField(JArray xoayCot, JArray xoayGiaTri, string tableName, out string errMsg)
        {
            var query = new StringBuilder();
            errMsg = string.Empty;

            query.AppendFormat("SELECT DISTINCT {0} FROM {1} ORDER BY {0}", (string)xoayCot[(int)COLUMN_INDEX.FIELD_NAME], tableName);
            var dt = GetReportQueryData(query.ToString(), out errMsg);
            return dt;
        }

        private string generateXoaySelect(JArray xoayCot, List<JArray> xoayGiaTris, string tableName)
        {
            var query = new StringBuilder();
            var errMsg = string.Empty;
            var aliasNames = new List<string>();

            foreach (var xoayGiaTri in xoayGiaTris)
            {
                aliasNames = new List<string>();
                var dtDistinctValues = getDistinctValuesByField(xoayCot, xoayGiaTri, tableName, out errMsg);
                var startFormulaStr = string.Empty;
                var endFormulaStr = string.Empty;
                if (xoayGiaTri[(int)COLUMN_INDEX.FUNCTION].ToString() != "None")
                {
                    startFormulaStr = xoayGiaTri[(int)COLUMN_INDEX.FUNCTION].ToString() + "(";
                    endFormulaStr = ")";
                }

                if (xoayGiaTri[(int)COLUMN_INDEX.XOAY_ALIAS].ToString() != string.Empty)
                {
                    dynamic xoayConfig = JsonConvert.DeserializeObject((string)xoayGiaTri[(int)COLUMN_INDEX.XOAY_ALIAS]);
                    aliasNames = JsonConvert.DeserializeObject<List<string>>(xoayConfig.AliasNames.ToString());
                }

                string template = ", {4}IF( {0}= '{1}', IF({2} is null, 0, {2}), 0){5} as `{3}`";
                var index = 0;
                var aliasName = string.Empty;

                foreach (var dataRow in dtDistinctValues.Rows)
                {
                    if (aliasNames.Count() > index && aliasNames.ElementAt(index) != string.Empty)
                        aliasName = aliasNames.ElementAt(index);
                    else
                        aliasName = ((DataRow)dataRow).ItemArray[0].ToString();
                    query.AppendFormat(template, (string)xoayCot[(int)COLUMN_INDEX.FIELD_NAME], ((DataRow)dataRow).ItemArray[0], (string)xoayGiaTri[(int)COLUMN_INDEX.FIELD_NAME], 
                        aliasName, startFormulaStr, endFormulaStr);
                    index++;
                }
            }

            return query.ToString();
        }

        private StringBuilder replaceParams(List<ReportQueryFilter> filters, StringBuilder query, string tableName, string tableKeyName, string timeKeyFieldName, int subNumber,
            JArray xoayCot, List<JArray> xoayGiaTris)
        {
            var resultStr = query.ToString();
            var replacements = new Dictionary<string, string>();
            var replacementCustoms = new Dictionary<string, string>();
            var timeKeyFilter = filters.ElementAt(0);
            var organizeFilter = filters.ElementAt(1);
            tempStringBuilder = new StringBuilder();

            // Replace @LuyKeKey
            if (!timeKeyFilter.IsFilter)
                replacementCustoms.Add("@LuyKeKey", tableKeyName);
            else
                replacementCustoms.Add("@LuyKeKey", tempStringBuilder.AppendFormat("(SELECT * FROM {0} WHERE id @TimeKeyRep)", tableKeyName, subNumber).ToString());

            // Replace @Xoay
            if (xoayCot.Count > 0 && xoayGiaTris.Count() > 0)
                replacementCustoms.Add("@Xoay", generateXoaySelect(xoayCot, xoayGiaTris, tableName));

            // Replace khi click vào [Sử dụng giá trị filter]
            // @TimeKey
            if (timeKeyFilter.DataFieldId != null && timeKeyFilter.IsFilter)
            {
                if (timeKeyFilter.DataFieldId.HasValue && timeKeyFilter.DataFieldId.Value != 0)
                {
                    var timeKeyFilterDataField = _dataFieldRepository.Get((int)timeKeyFilter.DataFieldId);
                    if (timeKeyFilterDataField != null)
                    {
                        tempStringBuilder = new StringBuilder();
                        if (!string.IsNullOrEmpty(timeKeyFilter.Condition) && !string.IsNullOrEmpty(timeKeyFilter.FilterValue))
                        {
                            tempStringBuilder.AppendFormat("{0} '{1}'", timeKeyFilter.Condition, timeKeyFilter.FilterValue);

                            // @TimeKeyValue: TimeKey sử dụng cho GetKyTruoc của Cùng Kỳ
                            replacements.Add("@TimeKeyValue", timeKeyFilter.FilterValue);
                            replacements.Add("@TimeKeySS", " <="+timeKeyFilter.FilterValue);
                        }
                        else
                        {
                            // @TimeKeyValue: TimeKey sử dụng cho GetKyTruoc của Cùng Kỳ
                            replacements.Add("@TimeKeyValue", "@TimeKey");
                        }

                        if (!string.IsNullOrEmpty(timeKeyFilter.FilterValues) && timeKeyFilter.FilterValues != "[]")
                        {
                            dynamic filterValues = JsonConvert.DeserializeObject(timeKeyFilter.FilterValues);
                            List<string> listFilterValues = new List<string>();

                            foreach (var filterValue in filterValues)
                            {
                                listFilterValues.Add("'" + (string)filterValue + "'");
                            }

                            var strFilterValues = String.Join(",", listFilterValues);

                            if (!string.IsNullOrEmpty(strFilterValues))
                            {
                                if (tempStringBuilder.ToString() != string.Empty)
                                    tempStringBuilder.AppendFormat(" AND {0}.{1}", tableName, timeKeyFilterDataField.FieldName);

                                tempStringBuilder.AppendFormat(" IN ({0})", strFilterValues);
                            }
                        }
                        replacements.Add("@TimeKeyRep", tempStringBuilder.ToString());
                    }
                }
            }
            else
            {
                replacements.Add("@TimeKeySS", "<= @TimeKey");
                replacements.Add("@TimeKeyRep", "= @TimeKey");
                replacements.Add("@TimeKeyValue", "@TimeKey");
            }

            // @OrganizeKey
            if (organizeFilter.DataFieldId != null && organizeFilter.IsFilter)
            {
                if (organizeFilter.DataFieldId.HasValue && organizeFilter.DataFieldId.Value != 0)
                {
                    var organizeFilterDataField = _dataFieldRepository.Get((int)organizeFilter.DataFieldId);
                    if (organizeFilterDataField != null)
                    {
                        tempStringBuilder = new StringBuilder();
                        if (!string.IsNullOrEmpty(organizeFilter.Condition) && !string.IsNullOrEmpty(organizeFilter.FilterValue))
                        {
                            tempStringBuilder.AppendFormat("{1} '{2}'", organizeFilterDataField.FieldName, organizeFilter.Condition, organizeFilter.FilterValue);
                        }

                        if (!string.IsNullOrEmpty(organizeFilter.FilterValues) && organizeFilter.FilterValues != "[]")
                        {
                            dynamic filterValues = JsonConvert.DeserializeObject(organizeFilter.FilterValues);
                            List<string> listFilterValues = new List<string>();

                            foreach (var filterValue in filterValues)
                            {
                                listFilterValues.Add("'" + (string)filterValue + "'");
                            }

                            var strFilterValues = String.Join(",", listFilterValues);

                            if (tempStringBuilder.ToString() != string.Empty)
                                tempStringBuilder.AppendFormat(" AND {0}.{1}", tableName, organizeFilterDataField.FieldName);

                            tempStringBuilder.AppendFormat(" IN ({0})", strFilterValues);
                        }

                        replacements.Add("@OrganizeKeyRep", tempStringBuilder.ToString());

                    }
                }
            }
            else
            {
                replacements.Add("@OrganizeKeyRep", "= @OrganizeKey");
            }

            // START Các điều kiện các field trong handsontable
            var otherFilter = new ReportQueryFilter();
            for (var index = 0; index < filters.Count(); index++)
            {
                if (index != (int)KeyName.OrganizeKey && index != (int)KeyName.TimeKey)
                {
                    otherFilter = filters.ElementAt(index);
                    if (otherFilter.DataFieldId != null)
                    {
                        if (otherFilter.DataFieldId.HasValue && otherFilter.DataFieldId.Value != 0)
                        {
                            var otherFilterDataField = _dataFieldRepository.Get((int)otherFilter.DataFieldId);
                            if (otherFilterDataField != null)
                            {
                                tempStringBuilder = new StringBuilder();
                                if (!string.IsNullOrEmpty(otherFilter.Condition) && !string.IsNullOrEmpty(otherFilter.FilterValue))
                                {
                                    tempStringBuilder.AppendFormat("{1} '{2}'", otherFilterDataField.FieldName, otherFilter.Condition, otherFilter.FilterValue);
                                }

                                if (!string.IsNullOrEmpty(otherFilter.FilterValues) && otherFilter.FilterValues != "[]")
                                {
                                    dynamic filterValues = JsonConvert.DeserializeObject(otherFilter.FilterValues);
                                    List<string> listFilterValues = new List<string>();

                                    foreach (var filterValue in filterValues)
                                    {
                                        listFilterValues.Add("'" + (string)filterValue + "'");
                                    }

                                    var strFilterValues = String.Join(",", listFilterValues);

                                    if (tempStringBuilder.ToString() != string.Empty)
                                        tempStringBuilder.AppendFormat(" AND {0}.{1}", tableName, otherFilterDataField.FieldName);

                                    tempStringBuilder.AppendFormat(" IN ({0})", strFilterValues);
                                }

                                replacements.Add("@OtherKeyRep", tempStringBuilder.ToString());

                            }
                        }
                    }
                }
            }
            // END Các điều kiện các field trong handsontable

            // Replace các key custom
            resultStr = MultipleReplace(resultStr, replacementCustoms);

            // Replace OrganizeKey, TimeKey
            resultStr = MultipleReplace(resultStr, replacements);

            return new StringBuilder(resultStr);
        }

        private string generateGroupQuery(List<ReportQueryFilter> filters, string strFormCode, int? templateKeyCategoryId)
        {
            var query = new StringBuilder();
            dynamic formCode = JsonConvert.DeserializeObject(strFormCode);
            var index = 0;
            var timeKeyFilter = filters.ElementAt(0);

            foreach (var dataDetail in formCode.data)
            {
                if (dataDetail[(int)COLUMN_INDEX.GROUP] == true)
                {
                    if (index != 0)
                        query.Append(",");

                    query.AppendFormat(" {0}", dataDetail[(int)COLUMN_INDEX.FIELD_NAME]);
                    index++;
                }
            }

            if (timeKeyFilter.IsSummary && timeKeyFilter.DataFieldId != null)
            {
                var dataField = _dataFieldRepository.Get(timeKeyFilter.DataFieldId);
                var summaryTableName = GetSummaryTableNameByTimeKey(dataField.FieldName);

                if (index != 0)
                    query.Append(",");

                switch (templateKeyCategoryId)
                {
                    case (int)TemplateKeyCategoryId.Year:
                        // Năm
                        query.AppendFormat("{0}.`year`", summaryTableName);
                        break;
                    case (int)TemplateKeyCategoryId.HalfYear:
                        // 6 tháng
                        query.AppendFormat("{0}.`year`, {0}.`half_year`", summaryTableName);
                        break;
                    case (int)TemplateKeyCategoryId.Quarter:
                        // Quý
                        query.AppendFormat("{0}.`year`, {0}.`quarter`", summaryTableName);
                        break;
                    default:
                        // các trường hợp khác không xác định ==> bỏ dấu ',' cuối
                        if (query.Length != 0)
                            query.Remove(query.Length - 1, 1);
                        break;
                }
            }

            return query.ToString();
        }

        private string generateWhereQuery(string tableName, string strFormCode, List<ReportQueryFilter> filters, int? templateKeyCategoryId)
        {
            var query = new StringBuilder();
            dynamic formCode = JsonConvert.DeserializeObject(strFormCode);

            // Điều kiện các row
            foreach (var dataDetail in formCode.data)
            {
                if (dataDetail[(int)COLUMN_INDEX.FILTER] != null && dataDetail[(int)COLUMN_INDEX.FILTER] != string.Empty)
                {
                    var filter = new ReportQueryFilter();
                    if (dataDetail[(int)COLUMN_INDEX.FILTER].ToString() != "None") {
                        filter = (ReportQueryFilter)JsonConvert.DeserializeObject<ReportQueryFilter>(dataDetail[(int)COLUMN_INDEX.FILTER].ToString());
                        if (!filter.FilterValues.Equals("[]") || !string.IsNullOrEmpty(filter.FilterValue))
                        {
                            if (!string.IsNullOrEmpty(query.ToString()))
                                query.Append(" AND");
                            filters.Add(filter);
                        }
                    }  
                }
            }

            // Điều kiện của filter
            var filterIndex = 0;
            foreach (var filter in filters)
            {
                if (filter.DataFieldId == null)
                {
                    filterIndex++;
                    continue;
                }

                var dataField = _dataFieldRepository.Get((int)filter.DataFieldId);
                if (dataField != null)
                {
                    // tổng hợp
                    if (filter.IsSummary)
                    {
                        var summaryTableName = GetSummaryTableNameByTimeKey(dataField.FieldName);
                        if (!string.IsNullOrEmpty(query.ToString()))
                            query.Append(" AND");

                        if (filterIndex == (int)KeyName.TimeKey)
                        {
                            switch (templateKeyCategoryId)
                            {
                                case (int)TemplateKeyCategoryId.Year:
                                    // Năm
                                    query.AppendFormat(" CONVERT(CONCAT({0}.`year`), SIGNED INTEGER) = @TimeKeyValue", summaryTableName);
                                    break;
                                case (int)TemplateKeyCategoryId.HalfYear:
                                    // 6 tháng
                                    query.AppendFormat(" CONVERT(CONCAT({0}.`year` , {0}.half_year), SIGNED INTEGER) = @TimeKeyValue", summaryTableName);
                                    break;
                                case (int)TemplateKeyCategoryId.Quarter:
                                    // Quý
                                    query.AppendFormat(" CONVERT(CONCAT({0}.`year`,{0}.`quarter`), SIGNED INTEGER) = @TimeKeyValue", summaryTableName);
                                    break;
                                case (int)TemplateKeyCategoryId.Month:
                                    // Tháng
                                    query.AppendFormat(" CONVERT(CONCAT({0}.`year`, LPAD({0}.`month`, 2, 0)), SIGNED INTEGER) = @TimeKeyValue", summaryTableName);
                                    break;
                                case (int)TemplateKeyCategoryId.Week:
                                    // Tháng
                                    query.AppendFormat(" CONVERT(CONCAT({0}.`year`,{0}.`week`), SIGNED INTEGER) = @TimeKeyValue", summaryTableName);
                                    break;
                            }
                        }
                        else if (filterIndex == (int)KeyName.OrganizeKey)
                        {
                            query.AppendFormat(" dim_organize_standard.parent @OrganizeKeyRep", tableName, dataField.FieldName);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(query.ToString()))
                            query.Append(" AND");

                        if (filterIndex == (int)KeyName.TimeKey)
                            query.AppendFormat(" {0}.{1}  @TimeKeyRep", tableName, dataField.FieldName);
                        else if (filterIndex == (int)KeyName.OrganizeKey)
                            query.AppendFormat(" {0}.{1} @OrganizeKeyRep", tableName, dataField.FieldName);
                        else
                            query.AppendFormat(" {0}.{1} @OtherKeyRep", tableName, dataField.FieldName);
                    }
                }

                filterIndex++;
            }

            return query.ToString();
        }

        private enum KeyName
        {
            TimeKey,       // 0
            OrganizeKey    // 1
        }

        private enum TemplateKeyCategoryId
        {
            None,       // 0
            Year,       // 1
            HalfYear,   // 2
            Quarter,    // 3
            Month,      // 4
            Week,       // 5
            Date,       // 6
            Minute      // 7
        }

        private string generateFromQuery(E_DataTable e_DataTable, List<ReportQueryFilter> filters)
        {
            var query = new StringBuilder();
            query.AppendFormat(" {0}", e_DataTable.Name);

            if (filters != null & filters.Count() == 2)
            {
                // tổng hợp cho TimeKey
                var timeKeyFilter = filters.ElementAt(0);
                if (timeKeyFilter.IsSummary && timeKeyFilter.DataFieldId != null)
                {
                    var dataField = _dataFieldRepository.Get((int)timeKeyFilter.DataFieldId);
                    var summaryTableName = GetSummaryTableNameByTimeKey(dataField.FieldName);

                    if (dataField != null)
                    {
                        query.AppendFormat(" INNER JOIN {2} ON {0}.{1} = {2}.id", e_DataTable.Name, dataField.FieldName, summaryTableName);
                    }
                }

                // tổng hợp cho OrganizeKey
                var organizeFilter = filters.ElementAt(1);
                if (organizeFilter.IsSummary && organizeFilter.DataFieldId != null)
                {
                    var dataField = _dataFieldRepository.Get((int)organizeFilter.DataFieldId);
                    query.AppendFormat(" INNER JOIN dim_organize_standard ON {0}.{1} = dim_organize_standard.organizekey", e_DataTable.Name, dataField.FieldName);
                }
            }

            // START relation
            var relationDataTables = new List<E_DataTable>();
            var mainRelations = _relationRepository.Gets(true, p => p.SourceTableId == e_DataTable.DataTableId && p.JoinOperators == null);

            if (mainRelations != null)
            {
                foreach (var mainRelation in mainRelations)
                {
                    query.AppendFormat(" INNER JOIN {0} ON {0}.{1} {2} {3}.{4}", mainRelation.TargetName, mainRelation.TargetColumn, mainRelation.JoinExpression, e_DataTable.Name, mainRelation.SourceColumn);
                    var subRelations = _relationRepository.Gets(true, p => p.JoinId == mainRelation.JoinId && p.JoinOperators != null);
                    foreach (var subRelation in subRelations)
                    {
                        query.AppendFormat(" {0} {1}.{2} {3} {4}.{5}", subRelation.JoinOperators, subRelation.TargetName, subRelation.TargetColumn, subRelation.JoinExpression, e_DataTable.Name, subRelation.SourceColumn);
                    }
                }
            }

            // END relation

            return query.ToString();
        }

        private string generateOrderByQuery(string strFormCode)
        {
            var query = new StringBuilder();
            dynamic formCode = JsonConvert.DeserializeObject(strFormCode);
            var index = 0;

            foreach (var dataDetail in formCode.data)
            {
                if (dataDetail[(int)COLUMN_INDEX.VISIBLE] == true && dataDetail[(int)COLUMN_INDEX.ORDER] != "None")
                {
                    if (index != 0)
                        query.Append(",");

                    query.AppendFormat(" {0} {1}", dataDetail[(int)COLUMN_INDEX.FIELD_NAME], dataDetail[(int)COLUMN_INDEX.ORDER]);

                    index++;
                }
            }
            return query.ToString();

        }
        private string GenerateQuery(ReportQuery reportQuery)
        {
            var query = new StringBuilder();

            if (reportQuery == null)
                return string.Empty;

            var formCode = reportQuery.FormCode;
            if (formCode == null || formCode == string.Empty)
                return string.Empty;

            var formulaDataField = _dataFieldRepository.Get((int)reportQuery.FormulaDataColumnId);
            if (formulaDataField == null)
                formulaDataField = new DataField();

            var dataTable = _dataTableRepository.Get((int)reportQuery.DataTableId);
            if (dataTable == null)
                return string.Empty;

            var timeKeyFieldName = GetTimeKeyByActionLevelId(reportQuery.ActionLevelId);
            var tableKeyName = GetTableKeyByActionLevelId(reportQuery.ActionLevelId);

            int subNumber = 0;
            switch (reportQuery.ActionLevelId)
            {
                case 1:
                    subNumber = 1;
                    break;
                case 2:
                    subNumber = 10;
                    break;
                case 3:
                    subNumber = 10;
                    break;
                case 4:
                    subNumber = 100;
                    break;
            }

            var xoayCot = new JArray();
            var xoayGiaTris = new List<JArray>();

            var selectQuery = generateSelectQuery(reportQuery.Filters, formCode, formulaDataField.FieldName, dataTable.Name, tableKeyName, 
                timeKeyFieldName, reportQuery.ActionLevelId.Value, subNumber, out xoayCot, out xoayGiaTris);
            var whereQuery = generateWhereQuery(dataTable.Name, formCode, reportQuery.Filters, reportQuery.ActionLevelId);
            var groupQuery = generateGroupQuery(reportQuery.Filters, formCode, reportQuery.ActionLevelId);
            var fromQuery = generateFromQuery(dataTable, reportQuery.Filters);
            var orderbyQuery = generateOrderByQuery(formCode);

            if (xoayCot.Count > 0 && xoayGiaTris.Count() > 0)
            {
                selectQuery += " @Xoay";
            }

            query.AppendFormat("SELECT {0} FROM {1} {2} {3} {4}", selectQuery, fromQuery,
                string.IsNullOrEmpty(whereQuery) ? string.Empty : ("WHERE " + whereQuery),
                string.IsNullOrEmpty(groupQuery) ? string.Empty : ("GROUP BY " + groupQuery),
                string.IsNullOrEmpty(orderbyQuery) ? string.Empty : ("ORDER BY " + orderbyQuery)
            );

            query = replaceParams(reportQuery.Filters, query, dataTable.Name, tableKeyName, timeKeyFieldName, subNumber, xoayCot, xoayGiaTris);

            return query.ToString();
        }

        
        #endregion

        #region DataField

        public List<object> GetValuesByDataField(int id, out string type)
        {
            type = "";
            var result = new List<object>();
            var dataField = _dataFieldRepository.Get(id);

            if (dataField == null)
            {
                return result;
            }

            type = dataField.Datatype;

            var dataTable = _dataTableRepository.Get(dataField.DataTableId);
            if (dataTable == null)
            {
                return result;
            }

            var command = string.Empty;
            try
            {
                var parseQuery = new ParseQueryMySQL();
                var query = parseQuery.ParseQueryGetValues(dataField.FieldName, dataTable.Name);
                using (var dbConn = new MySqlConnection(_generalSettings.DashboardConnection))
                {
                    var cmd = dbConn.CreateCommand();
                    cmd.CommandText = query;
                    var dt = new DataTable();
                    try
                    {
                        dbConn.Open();
                        using (var da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                        result = Json2.ParseAs<List<object>>(Json2.Stringify(dt));
                    }
                    catch (Exception e)
                    {
                        //
                    }
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                var msg = new List<string>();
                msg.Add(ex.Message);
                msg.Add(ex.StackTrace);
                result = null;
            }

            return result;
        }

        public DataTable GetReportQueryData(string query, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(query))
            {
                return dt;
            }

            try
            {
                var parseQuery = new ParseQueryMySQL();
                using (var dbConn = new MySqlConnection(_generalSettings.DashboardConnection))
                {
                    var cmd = dbConn.CreateCommand();
                    cmd.CommandText = query;
                    try
                    {
                        dbConn.Open();
                        using (var da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    catch (Exception e)
                    {
                        errMsg = e.Message.ToString();
                    }
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }

            return dt;
        }
        public DataTable GetReportQueryData(string query,int tableId, out string errMsg)
        {
            errMsg = string.Empty;
            var dt = new DataTable();
            if (string.IsNullOrEmpty(query) || tableId == 0)
            {
                return dt;
            }
            try
            {
                var dataTable = _dataTableRepository.Get(tableId);
                if (dataTable == null) return dt;
                var dataSource = _dataSourceRepository.Get(dataTable.DataSourceId);
                if (dataSource == null) return dt;
                var connection = dataSource.ConnectionString;

                if (!string.IsNullOrWhiteSpace(connection))
                {
                    using (var dbConn = new MySqlConnection(connection))
                    {
                        var cmd = dbConn.CreateCommand();
                        cmd.CommandText = query;
                        try
                        {
                            dbConn.Open();
                            using (var da = new MySqlDataAdapter(cmd))
                            {
                                da.Fill(dt);
                            }
                        }
                        catch (Exception e)
                        {
                            errMsg = e.Message.ToString();
                        }
                        dbConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }

            return dt;
        }
        public string GetName(int reportQueryId)
        {
            var reportQueryName = string.Empty;

            var reportQuery = _reportQueryRepository.Get(reportQueryId);
            if (reportQuery != null)
                return reportQuery.ReportQueryName;

            return reportQueryName;
        }

        public string GetCategoryName(int reportQueryId)
        {
            var categoryName = string.Empty;

            var reportQuery = _reportQueryRepository.Get(reportQueryId);
            if (reportQuery != null)
            {
                var category = GetActionLevels(null).Where(q => q.Value == reportQuery.ActionLevelId.ToString()).First();
                if (category != null)
                {
                    return category.Text;
                }
            }

            return categoryName;
        }

        public List<SelectListItem> GetActionLevels(int? actionLevelId)
        {
            var actionLevels = _actionLevelRepository.Gets(true);
            
            return actionLevels.Select(a => new SelectListItem
            {
                Text = a.ActionLevelName,
                Value = a.ActionLevelCode,
                Selected = actionLevelId.HasValue && actionLevelId.Value.ToString() == a.ActionLevelCode,
            }).ToList();
        }

        private string[,] GetArrayDimession()
        {
            return new string[,] { { "1", "2", "3", "4", "5", "6", "7" },
                { "yearkey", "halfkey", "quarterkey", "monthkey", "weekkey", "datekey", "minutekey" },
                { "", "dim_halfyear", "dim_quarter", "dim_month", "dim_week", "dim_date", "dim_minute" }
            };
        }
        private string GetTimeKeyByActionLevelId(int? actionLevelId)
        {
            string timeKey = string.Empty;

            if (!actionLevelId.HasValue)
                return timeKey;

            var arrDimesion = GetArrayDimession();

            for (int i = 0; i < 7; i++)
            {
                if (arrDimesion[0, i].Equals(actionLevelId.ToString()))
                    timeKey = arrDimesion[1, i];
            }

            return timeKey;
        }

        private string GetTableKeyByActionLevelId(int? actionLevelId)
        {
            string tableKey = string.Empty;

            if (!actionLevelId.HasValue)
                return tableKey;

            var arrDimesion = GetArrayDimession();

            for (int i = 0; i < 7; i++)
            {
                if (arrDimesion[0, i].Equals(actionLevelId.ToString()))
                    tableKey = arrDimesion[2, i];
            }

            return tableKey;
        }

        private string GetSummaryTableNameByTimeKey(string timeKey)
        {
            string summaryTableName = string.Empty;

            if (string.IsNullOrEmpty(timeKey))
                return summaryTableName;

            var arrDimesion = GetArrayDimession();

            for (int i = 0; i < 7; i++)
            {
                if (arrDimesion[1, i].Equals(timeKey))
                    summaryTableName = arrDimesion[2, i];
            }

            return summaryTableName;
        }

        private Dictionary<int, string> RoundInfos()
        {
            // Round display: '1,23456 ~ 1,2346', '1,23456 ~ 1,235', '1,23456 ~ 1,23', '1,23456 ~ 1,2', '1,23456 ~ 1'
            // Round number : 4, 3, 2, 1, 0
            var result  = new Dictionary<int, string>();
            result.Add(4, "1,23456 ~ 1,2346");
            result.Add(3, "1,23456 ~ 1,235");
            result.Add(2, "1,23456 ~ 1,23");
            result.Add(1, "1,23456 ~ 1,2");
            result.Add(0, "1,23456 ~ 1");
            return result;
        }
        #endregion DataField
    }
}
