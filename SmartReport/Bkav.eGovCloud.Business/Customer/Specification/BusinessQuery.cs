using System;
using System.Linq.Expressions;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : BusinessQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng Business
    /// </summary>
    public static class BusinessQuery
    {
        /// <summary>
        /// BusinessId == businessId
        /// </summary>
        /// <param name="businessId">Id của doanh nghiệp.</param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.Business, bool>> WithId(int businessId)
        {
            return s => s.BusinessId == businessId;
        }

        /// <summary>
        /// BusinessName == businessname
        /// </summary>
        /// <param name="businessName">Tên của doanh nghiệp.</param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.Business, bool>> WithName(string businessName)
        {
            return s => s.BusinessName == businessName;
        }

        /// <summary>
        /// BusinessTypeID == businessTypeID
        /// </summary>
        /// <param name="businessTypeId">Id của loại hình doanh nghiệp.</param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.Business, bool>> WithTypeId(int? businessTypeId)
        {
            return s => !businessTypeId.HasValue || s.BusinessTypeId == businessTypeId;
        }

        /// <summary>
        /// CityCode == cityCode
        /// </summary>
        /// <param name="cityCode">Mã tỉnh thành.</param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.Business, bool>> WithCityCode(string cityCode)
        {
            return s => s.CityCode == cityCode;
        }

        /// <summary>
        /// DistrictCode == districtCode
        /// </summary>
        /// <param name="districtCode">Mã quận huyện.</param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.Business, bool>> WithDistrictCode(string districtCode)
        {
            return s => s.DistrictCode == districtCode;
        }

        /// <summary>
        /// WardId == wardId
        /// </summary>
        /// <param name="wardId">Id của xã phường.</param>
        /// <returns></returns>
        public static Expression<Func<Entities.Customer.Business, bool>> WithWardId(int? wardId)
        {
            return s => !wardId.HasValue || s.WardId == wardId;
        }
    }
}
