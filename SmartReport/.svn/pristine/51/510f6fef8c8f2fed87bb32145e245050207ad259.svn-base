using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : CityQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 221013
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng City
    /// </summary>
    public static class CityQuery
    {
        /// <summary>
        /// CityId == cityId
        /// </summary>
        /// <param name="cityId">Id của tỉnh/thành phố.</param>
        /// <returns></returns>
        public static Expression<Func<City, bool>> WithId(int cityId)
        {
            return s => s.CityId == cityId;
        }


        /// <summary>
        /// CityName == cityname
        /// </summary>
        /// <param name="cityname">Tên của tỉnh/thành phố.</param>
        /// <returns></returns>
        public static Expression<Func<City, bool>> WithName(string cityname)
        {
            return s => s.CityName == cityname;
        }

        /// <summary>
        /// CityCode == citycode
        /// </summary>
        /// <param name="citycode">Mã của tỉnh/thành phố.</param>
        /// <returns></returns>
        public static Expression<Func<City, bool>> WithCode(string citycode)
        {
            return s => s.CityCode == citycode;
        }

        /// <summary>
        /// CityName == cityname
        /// </summary>
        /// <param name="cityname">Tên tỉnh/thành phố.</param>
        /// <returns></returns>
        public static Expression<Func<City, bool>> ContainsName(string cityname)
        {
            return s => s.CityName.ToLower().Contains(cityname.ToLower());
        }
    }
}
