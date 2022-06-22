using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Business.Admin.ParseQuery;

namespace Bkav.eGovCloud.Business.BI.ParseQuery
{
    public class ParseQueryMySQL : IParseQuery
    {
        // 20191017 VuHQ REQ-05-0x
        private List<string> _TIMEKEY = new List<string>() { "yearkey", "halfkey", "monthkey", "weekkey", "datekey", "hourkey", "minutekey", "secondkey" };

        // 20191017 VuHQ REQ-05-0x
        private const string STR_LUY_KE = "LŨY KẾ";
        private const string STR_CUNG_KY = "CÙNG KỲ";
        private const string STR_CUNG_KY_PERCENT = "CÙNG KỲ (%)";
        private const string STR_KY_TRUOC = "TĂNG TRƯỞNG";
        private const string STR_KY_TRUOC_PERCENT = "TĂNG TRƯỞNG (%)";
        private const string STR_SO_TONG_SO = "SUMTOTAL";
        private const string STR_SO_TONG_SO_PERCENT = "SUMTOTAL (%)";

        public string AppendQuery(string select, string from, string where, string groupBy, string orderBy,
            string limitQuery)
        {
            var result = new StringBuilder();
            result.Append("SELECT ");
            result.AppendLine(select);
            result.AppendLine(from);
            result.AppendLine(where);
            result.AppendLine(groupBy);
            result.AppendLine(orderBy);
            result.AppendLine(limitQuery);
            result.Append(";");

            return result.ToString();
        }

        public string GetDistrictFieldNameByFormula(string formula, string fieldName, string datatype)
        {
            string districtFieldName = string.Format("{0}({1})", formula, fieldName);

            List<string> dateTypeList = new List<string>() { "DATE", "DATETIME" };
            if (dateTypeList.IndexOf(datatype.ToUpper()) >= 0)
            {
                switch (formula)
                {
                    case "QUARTER":
                        {
                            districtFieldName = string.Format("CONCAT({0}({1}),'/', YEAR({1}))", formula, fieldName);
                            break;
                        }
                    case "MONTH":
                        {
                            districtFieldName = string.Format("DATE_FORMAT({0}, '%m/%Y')", fieldName);
                            break;
                        }
                    case "WEEK":
                        {
                            districtFieldName = string.Format("DATE_FORMAT({0},'%v/%Y')", fieldName);
                            break;
                        }
                    case "DAY":
                        {
                            districtFieldName = string.Format("DATE_FORMAT({0},'%d/%m/%Y')", fieldName);
                            break;
                        }
                    case "HOUR":
                        {
                            districtFieldName = string.Format("DATE_FORMAT({0}, '%d/%m/%Y %h')", fieldName);
                            break;
                        }
                    case "MINUTE":
                        {
                            districtFieldName = string.Format("DATE_FORMAT({0},  '%d/%m/%Y %h:%i')", fieldName);
                            break;
                        }
                    case "SECOND":
                        {
                            districtFieldName = string.Format("DATE_FORMAT({0},  '%d/%m/%Y %h:%i:%s')", fieldName);
                            break;
                        }
                }
            }

            return districtFieldName;
        }

        public string GetDistrictFieldNameByFormulaTemplate(string formula, string fieldName, string datatype)
        {
            string districtFieldName = string.Format("{0}(template.`{1}`)", formula, fieldName);

            List<string> dateTypeList = new List<string>() { "DATE", "DATETIME" };
            if (dateTypeList.IndexOf(datatype.ToUpper()) >= 0)
            {
                switch (formula)
                {
                    case "QUARTER":
                        {
                            districtFieldName = string.Format("CONCAT({0}(template.`{1}`),'/', YEAR(template.`{1}`))", formula, fieldName);
                            break;
                        }
                    case "MONTH":
                        {
                            districtFieldName = string.Format("DATE_FORMAT(template.`{0}`, '%m/%Y')", fieldName);
                            break;
                        }
                    case "WEEK":
                        {
                            districtFieldName = string.Format("DATE_FORMAT(template.`{0}`,'%v/%Y')", fieldName);
                            break;
                        }
                    case "DAY":
                        {
                            districtFieldName = string.Format("DATE_FORMAT(template.`{0}`,'%d/%m/%Y')", fieldName);
                            break;
                        }
                    case "HOUR":
                        {
                            districtFieldName = string.Format("DATE_FORMAT(template.`{0}`, '%d/%m/%Y %h')", fieldName);
                            break;
                        }
                    case "MINUTE":
                        {
                            districtFieldName = string.Format("DATE_FORMAT(template.`{0}`,  '%d/%m/%Y %h:%i')", fieldName);
                            break;
                        }
                    case "SECOND":
                        {
                            districtFieldName = string.Format("DATE_FORMAT(template.`{0}`,  '%d/%m/%Y %h:%i:%s')", fieldName);
                            break;
                        }
                }
            }

            return districtFieldName;
        }

        public string ParseQueryTemplateGetValue(string formula, string fieldName, string datatype, string queryString)
        {
            string query = string.Empty;
            if (!string.IsNullOrEmpty(formula))
            {
                string districtFieldName = GetDistrictFieldNameByFormulaTemplate(formula, fieldName, datatype);
                query = string.Format("SELECT DISTINCT {0} AS FieldValue FROM ({2}) AS template ORDER BY template.{1}, {0};", districtFieldName, fieldName, queryString);
            }
            else
            {
                query = string.Format("SELECT DISTINCT template.{0} AS FieldValue FROM ({1}) AS template ORDER BY template.{0};", fieldName, queryString);
            }

            return query;
        }

        public string ParseQueryGetValues(string fieldName, string tabaleName)
        {
            string query = string.Empty;
            
            query = string.Format("SELECT DISTINCT {0} AS FieldValue FROM `{1}` ORDER BY {0};", fieldName, tabaleName);

            return query;
        }

        private bool IsTimeKey(string key)
        {
            return _TIMEKEY.Contains(key);
        }

        #region Generate Query dạng 2 chiều

        private string _ParseFrom(string maintableName, List<Relation> relations)
        {
            var result = new StringBuilder();

            result.AppendLine(string.Format("FROM {0}", maintableName));

            foreach (var relation in relations)
            {
                if (relation.JoinType == (int)DatabaseRelationType.Blend)
                {
                    result.Append("," + relation.TargetName);
                    continue;
                }
                var joinType = EnumHelper<DatabaseRelationType>.GetDescription((DatabaseRelationType)relation.JoinType);
                var targetColumn = relation.JoinExpression.Equals("like") ? String.Format("Concat('%', {0}, '%')", relation.TargetColumn) : relation.TargetColumn;
                result.AppendLine(string.Format("{0} {1} on {5}.{2} {3} {1}.{4}", joinType, relation.TargetName, relation.SourceColumn, relation.JoinExpression, targetColumn, maintableName));
            }

            return result.ToString();
        }

        /// <summary>
        /// Lấy số để tính ra cùng kỳ năm trước.
        /// </summary>
        /// <param name="timeKey"></param>
        /// <returns></returns>
        private int GetNumber4SubFromTimeKey(string timeKey)
        {
            int number4Sub = 0;
            switch (timeKey)
            {
                case "yearkey":
                    number4Sub = 1;
                    break;
                case "halfkey":
                    number4Sub = 10;
                    break;
                case "quarterkey":
                    number4Sub = 10;
                    break;
                case "monthkey":
                    number4Sub = 100;
                    break;
                case "weekkey":
                    number4Sub = 100;
                    break;
                case "datekey":
                    number4Sub = 100;
                    break;
                case "minutekey":
                    number4Sub = 100000000;
                    break;
            }

            return number4Sub;
        }

        #endregion
    }
}
