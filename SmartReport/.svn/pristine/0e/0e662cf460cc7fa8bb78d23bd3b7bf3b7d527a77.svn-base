using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Interface : IBusinessLicenseAttachDal - public - DAL
    /// Access Modifiers: 
    /// Create Date : 011113
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng BusinessLicenseAttach trong CSDL
    /// </summary>
    public interface IBusinessLicenseAttachDal
    {
        /// <summary>
        /// Lấy ra tệp đính kèm cho giấy phép theo id
        /// </summary>
        /// <param name="businesslicenseId">Id của giấy phép</param>
        /// <returns>Entity giấy phép</returns>
        BusinessLicenseAttach Get(int businesslicenseId);

        /// <summary>
        /// Tạo mới tệp đính kèm cho giấy phép doanh nghiệp
        /// </summary>
        /// <param name="businesslicenseattach">Entity tệp đính kèm cho giấy phép doanh nghiệp</param>
        void Create(BusinessLicenseAttach businesslicenseattach);

        /// <summary>
        /// Xóa tệp đính kèm cho giấy phép doanh nghiệp
        /// </summary>
        /// <param name="businesslicenseattach">Entity tệp đính kèm cho giấy phép doanh nghiệp</param>
        void Delete(BusinessLicenseAttach businesslicenseattach);
    }
}
