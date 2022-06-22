using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : CodeQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 200912
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng Code
    /// </summary>
    public static class CodeQuery
    {
        /// <summary>
        /// CodeId == codeId
        /// </summary>
        /// <param name="codeId">Id của code.</param>
        /// <returns></returns>
        public static Expression<Func<Code, bool>> WithId(int codeId)
        {
            return s => s.CodeId == codeId;
        }

        /// <summary>
        /// CodeName == codename
        /// </summary>
        /// <param name="codename">Tên của nhảy số.</param>
        /// <returns></returns>
        public static Expression<Func<Code, bool>> WithName(string codename)
        {
            return s => s.CodeName == codename;
        }

        /// <summary>
        /// CodeName == codename
        /// </summary>
        /// <param name="codename">Tên của nhảy số.</param>
        /// <returns></returns>
        public static Expression<Func<Code, bool>> ContainsName(string codename)
        {
            return s => s.CodeName.ToLower().Contains(codename.ToLower());
        }
    }
}
