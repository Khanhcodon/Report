using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : JobTitlesQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 131012
    /// Author      : GiangPN
    /// Description : Các điều kiện truy vấn cho bảng JobTitles
    /// </summary>
    public static class JobTitlesQuery
    {
        /// <summary>
        /// JobTitlesId == jobTitlesId
        /// </summary>
        /// <param name="jobTitlesId">Id của chức danh.</param>
        /// <returns></returns>
        public static Expression<Func<JobTitles, bool>> WithId(int jobTitlesId)
        {
            return s => s.JobTitlesId == jobTitlesId;
        }

        /// <summary>
        /// JobTitlesName == jobTitlesName
        /// </summary>
        /// <param name="jobTitlesName">Tên chức danh.</param>
        /// <returns></returns>
        public static Expression<Func<JobTitles, bool>> WithJobTitlesName(string jobTitlesName)
        {
            return s => s.JobTitlesName == jobTitlesName;
        }

        /// <summary>
        /// JobTitlesName == jobTitlesName
        /// </summary>
        /// <param name="jobTitlesName">Tên chức danh</param>
        /// <returns></returns>
        public static Expression<Func<JobTitles, bool>> ContainsKey(string jobTitlesName)
        {
            return s => s.JobTitlesName.ToLower().Contains(jobTitlesName.ToLower());
        }
    }
}
