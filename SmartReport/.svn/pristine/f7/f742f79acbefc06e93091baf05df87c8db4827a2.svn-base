using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : ActionLevelQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 190620
    /// Author      : SuBD
    /// Description : Các điều kiện truy vấn cho bảng ActionLevel
    /// </summary>
    public static class ActionLevelQuery
    {
        /// <summary>
        /// ActionLevelId == actionLevelId
        /// </summary>
        /// <param name="actionLevelId">Id kỳ báo cáo.</param>
        /// <returns></returns>
        public static Expression<Func<ActionLevel, bool>> WithId(int actionLevelId)
        {
            return s => s.ActionLevelId == actionLevelId;
        }

        /// <summary>
        /// ActionLevelName == actionLevelName
        /// </summary>
        /// <param name="actionLevelName">Tên kỳ báo cáo.</param>
        /// <returns></returns>
        public static Expression<Func<ActionLevel, bool>> WithName(string actionLevelName)
        {
            return s => s.ActionLevelName == actionLevelName;
        }

        /// <summary>
        /// ActionLevelCode == actionLevelCode
        /// </summary>
        /// <param name="actionLevelCode">Mã kỳ báo cáo.</param>
        /// <returns></returns>
        public static Expression<Func<ActionLevel, bool>> WithCode(string actionLevelCode)
        {
            return s => s.ActionLevelCode == actionLevelCode;
        }

        /// <summary>
        /// ActionLevelName LIKE %actionLevelName%
        /// </summary>
        /// <param name="codename">Tên kỳ báo cáo.</param>
        /// <returns></returns>
        public static Expression<Func<ActionLevel, bool>> ContainsName(string actionLevelName)
        {
            return s => s.ActionLevelName.ToLower().Contains(actionLevelName.ToLower());
        }
    }
}
