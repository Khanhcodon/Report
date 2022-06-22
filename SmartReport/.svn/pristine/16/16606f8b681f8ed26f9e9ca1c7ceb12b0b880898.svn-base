using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : BusinessTypeBll - public - BLL</para>
    /// <para>Create Date : 171013</para>
    /// <para>Author      : DungHV</para>
    /// <para>Description : BLL tương ứng với bảng BusinessType trong CSDL</para>
    /// </summary>
    public class BusinessTypeBll : ServiceBase
    {
        private readonly IRepository<BusinessType> _businessTypeRepository;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Khởi tạo class <see cref="BusinessTypeBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="resourceService"></param>
        public BusinessTypeBll(IDbCustomerContext context, ResourceBll resourceService)
            : base(context)
        {
            _businessTypeRepository = Context.GetRepository<BusinessType>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Xóa 1 loại doanh nghiệp
        /// </summary>
        /// <param name="businessType">Thực thể loại doanh nghiệp</param>
        public void Delete(BusinessType businessType)
        {
            if (businessType == null)
            {
                throw new ArgumentNullException("businessType");
            }
            //TODO: Cần kiểm tra ràng buộc dữ liệu trong các bảng: category_code, store_category
            //var isUsed = _documentBll.Gets(d => d.CategoryId == category.CategoryId).Any();
            //if(isUsed)
            //{
            //    throw new EgovException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.Delete.Exception.Document"));
            //}
            _businessTypeRepository.Delete(businessType);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lất ra tất cả loại doanh nghiệp theo điều kiện. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<BusinessType> Gets(Expression<Func<BusinessType, bool>> spec = null)
        {
            return _businessTypeRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra một loại doanh nghiệp
        /// </summary>
        /// <param name="businessTypeId">Id của loại doanh nghiệp</param>
        /// <returns>Entity loại doanh nghiệp</returns>
        public BusinessType Get(int businessTypeId)
        {
            BusinessType businessType = null;
            if (businessTypeId > 0)
            {
                businessType = _businessTypeRepository.Get(businessTypeId);
            }
            return businessType;
        }

        /// <summary>
        /// Thêm mới loại doanh nghiệp
        /// </summary>
        /// <param name="businessType">Thực thể loại doanh nghiệp</param>
        /// <returns></returns>
        public void Create(BusinessType businessType)
        {
            if (businessType == null)
            {
                throw new ArgumentNullException("businessType");
            }
            if (_businessTypeRepository.Exist(BusinessTypeQuery.WithName(businessType.BusinessTypeName)))
            {
                throw new EgovException(string.Format("Thể loại văn bản ({0}) đã tồn tại!", businessType.BusinessTypeName));
            }
            _businessTypeRepository.Create(businessType);
            Context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới loại doanh nghiệp
        /// </summary>
        /// <param name="businessTypes">Danh sach doi tuwong loai hinh doanh nghiep</param>
        /// <param name="ignoreExist">True:Bo qua neu ton tai:Fa</param>
        public void Create(IEnumerable<BusinessType> businessTypes, bool ignoreExist)
        {
            if (businessTypes == null)
            {
                throw new ArgumentNullException("businessType");
            }

            var names = businessTypes.Select(x => x.BusinessTypeName);
            var exist = _businessTypeRepository.GetsAs(p => p.BusinessTypeName, p => names.Contains(p.BusinessTypeName));
            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == businessTypes.Count())
                {
                    throw new EgovException(_resourceService.GetResource("BusinessType.Create.Exist"));
                }

                var list = businessTypes.Where(p => !exist.Contains(p.BusinessTypeName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("BusinessType.Create.Exist"));
                }

                Create(list);
            }
            else
            {
                Create(businessTypes);
            }
        }

        private void Create(IEnumerable<BusinessType> businessTypes)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var item in businessTypes)
            {
                _businessTypeRepository.Create(item);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin loại doanh nghiệp
        /// </summary>
        /// <param name="businessType">Entity loại doanh nghiệp</param>
        /// <param name="oldbusinessTypeName">Tên loại doanh nghiệp trước khi cập nhật</param>
        public void Update(BusinessType businessType, string oldbusinessTypeName)
        {
            if (businessType == null)
            {
                throw new ArgumentNullException("businessType");
            }
            if (_businessTypeRepository.Exist(BusinessTypeQuery.WithName(businessType.BusinessTypeName.Trim()).And(r => r.BusinessTypeName.ToLower() != oldbusinessTypeName.Trim().ToLower())))
            {
                throw new EgovException(string.Format("Thể loại văn bản ({0}) đã tồn tại!", businessType.BusinessTypeName));
            }
            Context.SaveChanges();
        }
    }
}
