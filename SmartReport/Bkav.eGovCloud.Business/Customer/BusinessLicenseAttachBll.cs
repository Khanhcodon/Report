using System;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : BusinessLicenseBll - public - BLL</para>
    /// <para>Create Date : 251013</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng BusinessLicenseAttach trong CSDL</para>
    /// </summary>
    public class BusinessLicenseAttachBll : ServiceBase
    {
        private readonly IRepository<BusinessLicenseAttach> _businessLicenseAttachRepository;

        /// <summary>
        /// Khởi tạo class <see cref="BusinessLicenseAttachBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        public BusinessLicenseAttachBll(IDbCustomerContext context) : base(context)
        {
            _businessLicenseAttachRepository = Context.GetRepository<BusinessLicenseAttach>();
        }

        /// <summary>
        /// Lấy ra một tệp đính kèm giấy phép
        /// </summary>
        /// <param name="businessLicenseAttachId">Id của tệp đính kèm giấy phép</param>
        /// <returns>Entity tệp đính kèm giấy phép</returns>
        public BusinessLicenseAttach Get(int businessLicenseAttachId)
        {
            BusinessLicenseAttach businessLicense = null;
            if (businessLicenseAttachId > 0)
            {
                businessLicense = _businessLicenseAttachRepository.Get(businessLicenseAttachId);
            }
            return businessLicense;
        }

        /// <summary>
        /// Xóa 1 tệp đính kèm giấy phép
        /// </summary>
        /// <param name="businessLicenseAttach">Thực thể tệp đính kèm giấy phép</param>
        public void Delete(BusinessLicenseAttach businessLicenseAttach)
        {
            if (businessLicenseAttach == null)
            {
                throw new ArgumentNullException("businessLicenseAttach");
            }
            _businessLicenseAttachRepository.Delete(businessLicenseAttach);
            Context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới tệp đính kèm giấy phép
        /// </summary>
        /// <param name="businessLicenseAttach">Thực thể tệp đính kèm giấy phép</param>
        /// <returns></returns>
        public void Create(BusinessLicenseAttach businessLicenseAttach)
        {
            if (businessLicenseAttach == null)
            {
                throw new ArgumentNullException("businessLicenseAttach");
            }
            _businessLicenseAttachRepository.Create(businessLicenseAttach);
            Context.SaveChanges();
        }
    }
}
