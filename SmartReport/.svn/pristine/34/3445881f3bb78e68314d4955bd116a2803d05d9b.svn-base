using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : WardQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 221013
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng Ward
    /// </summary>
    public static class WardQuery
    {
        /// <summary>
        /// WardId == wardId
        /// </summary>
        /// <param name="wardId">Id của xã/phường.</param>
        /// <returns></returns>
        public static Expression<Func<Ward, bool>> WithId(int wardId)
        {
            return s => s.WardId == wardId;
        }


        /// <summary>
        /// WardName == wardname
        /// </summary>
        /// <param name="wardname">Tên của xã/phường.</param>
        /// <returns></returns>
        public static Expression<Func<Ward, bool>> WithName(string wardname)
        {
            return s => s.WardName == wardname;
        }

        /// <summary>
        /// Districtcode == districtcode
        /// </summary>
        /// <param name="districtcode">Mã của quận/huyện.</param>
        /// <returns></returns>
        public static Expression<Func<Ward, bool>> WithDistrictCode(string districtcode)
        {
            return s => s.DistrictCode == districtcode;
        }
    }
}
