using Bkav.eGovCloud.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Admin.ParseQuery
{
    public interface IParseQuery
    {
        string ParseQueryTemplateGetValue(string formula, string fieldName, string datatype, string queryString);

        string ParseQueryGetValues(string fieldName, string tabaleName);

        string GetDistrictFieldNameByFormula(string formula, string fieldName, string datatype);

        string GetDistrictFieldNameByFormulaTemplate(string formula, string fieldName, string datatype);

        string AppendQuery(string select, string from, string where, string groupBy, string orderBy, string limitQuery);
    }
}
