using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : DistrictQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 221013
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng District
    /// </summary>
    public static class DistrictQuery
    {
        /// <summary>
        /// DistrictId == districtId
        /// </summary>
        /// <param name="districtId">Id của quận/huyện.</param>
        /// <returns></returns>
        public static Expression<Func<District, bool>> WithId(int districtId)
        {
            return s => s.DistrictId == districtId;
        }


        /// <summary>
        /// DistrictName == districtname
        /// </summary>
        /// <param name="districtname">Tên của quận/huyện.</param>
        /// <returns></returns>
        public static Expression<Func<District, bool>> WithName(string districtname)
        {
            return s => s.DistrictName == districtname;
        }

        /// <summary>
        /// DistrictCode == districtcode
        /// </summary>
        /// <param name="districtcode">Mã của quận/huyện.</param>
        /// <returns></returns>
        public static Expression<Func<District, bool>> WithCode(string districtcode)
        {
            return s => s.DistrictCode == districtcode;
        }

        /// <summary>
        /// CityCode == citycode
        /// </summary>
        /// <param name="citycode">Mã của tỉnh/thành phố.</param>
        /// <returns></returns>
        public static Expression<Func<District, bool>> WithCityCode(string citycode)
        {
            return s => s.CityCode == citycode;
        }
    }
}
