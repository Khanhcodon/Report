using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : BusinessLicenseQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 251013
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng BusinessLicense
    /// </summary>
    public static class BusinessLicenseQuery
    {
        /// <summary>
        /// BusinessLicenseId == businessLicenseId
        /// </summary>
        /// <param name="businessLicenseId">Id của loại doanh nghiệp.</param>
        /// <returns></returns>
        public static Expression<Func<BusinessLicense, bool>> WithId(int businessLicenseId)
        {
            return s => s.BusinessLicenseId == businessLicenseId;
        }


        /// <summary>
        /// LicenseCode == licenseCode
        /// </summary>
        /// <param name="licenseCode">Tên của doanh nghiệp.</param>
        /// <returns></returns>
        public static Expression<Func<BusinessLicense, bool>> WithCode(string licenseCode)
        {
            return s => s.LicenseCode == licenseCode;
        }

        /// <summary>
        /// BusinessId == businessId
        /// </summary>
        /// <param name="businessId">Id của doanh nghiệp.</param>
        /// <returns></returns>
        public static Expression<Func<BusinessLicense, bool>> WithBusinessId(int? businessId)
        {
            return s => !businessId.HasValue || s.BusinessId == businessId;
        }

        /// <summary>
        /// DoctypeId == doctypeId
        /// </summary>
        /// <param name="doctypeId">Id của loại giấy phép.</param>
        /// <returns></returns>
        public static Expression<Func<BusinessLicense, bool>> WithDocTypeId(Guid? doctypeId)
        {
            return s => s.DocTypeId == doctypeId;
        }

        /// <summary>
        /// DocumentCopyId == docCopyId
        /// </summary>
        /// <param name="docCopyId">Id của hồ sơ copy.</param>
        /// <returns></returns>
        public static Expression<Func<BusinessLicense, bool>> WithDocCopyId(int docCopyId)
        {
            return s => s.DocumentCopyId == docCopyId;
        }
    }
}
