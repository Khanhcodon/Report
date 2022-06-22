using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;
namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Template key Query
    /// </summary>
    public class TemplateKeyQuery
    {
        /// <summary>
        /// Name contains templateKeySearch
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        public static Expression<Func<TemplateKey, bool>> ContainsKey(string keySearch)
        {
            // 20200210 VuHQ Phase 2 REQ-0
            return t => t.Name.ToLower().Contains(keySearch.ToLower());
            //return t => !string.IsNullOrEmpty(keySearch) ||  t.Name.ToLower().Contains(keySearch.ToLower());
        }

        public static Expression<Func<DocType, bool>> WithActionLevel(int? actionLevel)
        {
            return s => !actionLevel.HasValue || s.ActionLevel == actionLevel;
        }

        /// <summary>
        /// Code == code
        /// </summary>
        /// <param name="code">Mã key</param>
        /// <returns></returns>
        public static Expression<Func<TemplateKey, bool>> ExistCode(string code)
        {
            return t => t.Code.ToLower().Equals(code.ToLower());
        }

        /// <summary>
        /// 20200210 VuHQ Phase 2 - REQ 0
        /// Type == type
        /// </summary>
        /// <param name="categoryBusinessId">Id của thể loại văn bản.</param>
        /// <returns></returns>
        public static Expression<Func<TemplateKey, bool>> WithType(int? type)
        {
            return s => !type.HasValue || s.Type == type;
        }
    }
    /// <summary>
    /// Template key Query
    /// </summary>
    public class TemplateKeyCategoryQuery
    {
        /// <summary>
        /// Name contains templateKeySearch
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        public static Expression<Func<TemplateKeyCategory, bool>> ContainsKey(string keySearch)
        {
            // 20200210 VuHQ Phase 2 REQ-0
            return t => t.Name.ToLower().Contains(keySearch.ToLower());
            //return t => !string.IsNullOrEmpty(keySearch) ||  t.Name.ToLower().Contains(keySearch.ToLower());
        }

        public static Expression<Func<DocType, bool>> WithActionLevel(int? actionLevel)
        {
            return s => !actionLevel.HasValue || s.ActionLevel == actionLevel;
        }
    }
}
