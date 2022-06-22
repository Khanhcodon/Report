using System.Collections.Generic;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IFormTypeDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng FormType trong CSDL
    /// </summary>
    public interface IFormTypeDal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<FormType> Gets();
    }
}
