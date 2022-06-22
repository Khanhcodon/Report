using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : IncreaseQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 140912
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng Increase
    /// </summary>
    public static class IncreaseQuery
    {
        /// <summary>
        /// IncreaseId == incraseId
        /// </summary>
        /// <param name="incraseId">Id của Increase.</param>
        /// <returns></returns>
        public static Expression<Func<Increase, bool>> WithId(int incraseId)
        {
            return s => s.IncreaseId == incraseId;
        }

        /// <summary>
        /// Name == name
        /// </summary>
        /// <param name="name">Tên của nhảy số.</param>
        /// <returns></returns>
        public static Expression<Func<Increase, bool>> WithName(string name)
        {
            return s => s.Name == name;
        }

        /// <summary>
        /// Name == name
        /// </summary>
        /// <param name="name">Tên của nhảy số.</param>
        /// <returns></returns>
        public static Expression<Func<Increase, bool>> ContainsName(string name)
        {
            return s => s.Name.ToLower().Contains(name.ToLower());
        }
    }
}
