using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : InterfaceConfigBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 261015
    /// Author      : HopCV
    /// Description : BLL tương ứng với bảng InterfaceConfig trong CSDL
    /// </summary>
    public class InterfaceConfigBll : ServiceBase
    {
        private readonly IRepository<InterfaceConfig> _interfaceConfigRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly MemoryCacheManager _cacheManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cacheManager"></param>
        /// <param name="generalSettings"></param>
        public InterfaceConfigBll(IDbCustomerContext context,
            AdminGeneralSettings generalSettings,
            MemoryCacheManager cacheManager)
            : base(context)
        {
            _interfaceConfigRepository = Context.GetRepository<InterfaceConfig>();
            _generalSettings = generalSettings;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Tạo mới mẫu giao diện
        /// </summary>
        /// <param name="entity"></param>
        public void Create(InterfaceConfig entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity in null.");
            }

            _interfaceConfigRepository.Create(entity);
            Context.SaveChanges();
            ClearCache();
        }

        /// <summary>
        /// Tạo mới mẫu giao diện
        /// </summary>
        /// <param name="entities"></param>
        public void Create(IEnumerable<InterfaceConfig> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException("entities in null.");
            }

            foreach (var entity in entities)
            {
                if (!Exist(x => x.InterfaceConfigName.Equals(entity.InterfaceConfigName, StringComparison.OrdinalIgnoreCase)))
                {
                    _interfaceConfigRepository.Create(entity);
                }
            }

            Context.SaveChanges();
            ClearCache();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<InterfaceConfig, bool>> spec)
        {
            return _interfaceConfigRepository.Exist(spec);
        }

        /// <summary>
        /// Cập nhật đối tượng mẫu giao diện
        /// </summary>
        /// <param name="entity"></param>
        public void Update(InterfaceConfig entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("InterfaceConfig in null.");
            }

            Context.SaveChanges();
            ClearCache();
        }

        /// <summary>
        /// Cập nhật giao diện cho nghiệp vụ
        /// </summary>
        /// <param name="interfaceConfig"></param>
        /// <param name="template"></param>
        /// <param name="categoryBusiness"></param>
        public void UpdateTempCategoryBussiness(InterfaceConfig interfaceConfig, string template, CategoryBusinessTypes categoryBusiness)
        {
            if (interfaceConfig == null)
            {
                throw new ArgumentNullException("InterfaceConfig in null.");
            }

            if (string.IsNullOrEmpty(template))
            {
                throw new ArgumentNullException("template");
            }

            var exist = Gets(p => p.InterfaceConfigId != interfaceConfig.InterfaceConfigId && p.CategoryBusinessId == (int)categoryBusiness);
            if (exist != null && exist.Any())
            {
                foreach (var item in exist)
                {
                    item.CategoryBusinessId = null;
                }
            }

            interfaceConfig.CategoryBusinessId = (int)categoryBusiness;
            interfaceConfig.Template = template;
            Context.SaveChanges();
            ClearCache();
        }

        /// <summary>
        /// Xóa 1 đối tượng mẫu giao diện
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(InterfaceConfig entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("InterfaceConfig in null.");
            }

            if (entity.CategoryBusinessId.HasValue)
            {
                throw new Exception("InterfaceConfig is a categofy bussiness.");
            }

            _interfaceConfigRepository.Delete(entity);
            Context.SaveChanges();
            ClearCache();
        }

        /// <summary>
        /// Lấy đối tượng mẫu giao diện theo id
        /// </summary>
        /// <param name="id">Id của đối tượng mẫu giao diện</param>
        /// <returns></returns>
        public InterfaceConfig Get(int id)
        {
            return _interfaceConfigRepository.Get(id);
        }

        /// <summary>
        /// Lấy đối tượng mẫu giao diện theo điều kiện
        /// </summary>
        /// <param name="isReadOnly">Đối tượng này chỉ đọc hay có thể vừa đọc vừa ghi</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        public InterfaceConfig Get(Expression<Func<InterfaceConfig, bool>> spec = null, bool isReadOnly = false)
        {
            return _interfaceConfigRepository.Get(isReadOnly, spec);
        }

        /// <summary>
        /// Lấy danh sách đôi tượng mẫu giao diện(chỉ đọc) theo điều kiện truyền vào 
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns></returns>
        public IEnumerable<InterfaceConfig> GetsReadOnly(Expression<Func<InterfaceConfig, bool>> spec = null)
        {
            return _interfaceConfigRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy danh sách đối tượng mẫu giao diện chỉ có thể đọc ghi theo điều kiện truyền vào
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<InterfaceConfig> Gets(Expression<Func<InterfaceConfig, bool>> spec = null)
        {
            return _interfaceConfigRepository.Gets(false, spec);
        }

        /// <summary>
        /// Lấy ra danh sách tất cả các người dùng (Danh sách này sẽ được cache lại trong 60 phút)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InterfaceConfig> GetCacheAllInterfaceConfigs()
        {
            var result = _cacheManager.Get(CacheParam.InterfaceConfigAllKey, CacheParam.InterfaceConfigAllCacheTimeOut, () => 
                {
                    var data = GetsReadOnly();
                    return AutoMapper.Mapper.Map<IEnumerable<InterfaceConfig>, IEnumerable<InterfaceConfigCached>>(data);
                });

            return AutoMapper.Mapper.Map<IEnumerable<InterfaceConfigCached>, IEnumerable<InterfaceConfig>>(result);
        }

        /// <summary>
        /// Lấy mẫu theo id
        /// </summary>
        /// <param name="interfaceConfigId">Id của mẫu cấu hình</param>
        /// <returns></returns>
        public string GetTemplateFromCache(int interfaceConfigId)
        {
            var result = string.Empty;
            var allCfgs = GetCacheAllInterfaceConfigs();
            if (allCfgs != null && allCfgs.Any())
            {
                var cfg = allCfgs.FirstOrDefault(p => p.InterfaceConfigId == interfaceConfigId);
                if (cfg != null)
                {
                    result = cfg.Template;
                }
            }

            return result;
        }

        /// <summary>
        /// Lấy tất cả danh sách cơ quan ngoài. Kết quả sẽ được ánh xạ sang 1 dạng khác do người dùng cung cấp
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="projector"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<InterfaceConfig, T>> projector, Expression<Func<InterfaceConfig, bool>> spec = null)
        {
            return _interfaceConfigRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Lấy ra tất cả nhật ký phù hợp với điều kiện truyền vào. 
        /// Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..Kết quả chỉ để đọc
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="findText"></param>
        /// <returns>Danh sách nhật ký</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(out int totalRecords,
            Expression<Func<InterfaceConfig, TOutput>> projector,
            int currentPage = 1, int? pageSize = null, string sortBy = "", bool isDescending = true,
           string findText = null)
        {
            Expression<Func<InterfaceConfig, bool>> spec =
                               p => string.IsNullOrEmpty(findText)
                                   || (!string.IsNullOrEmpty(findText) && p.InterfaceConfigName.Contains(findText));

            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _interfaceConfigRepository.Count(spec);
            var sort = Context.Filters.CreateSort<InterfaceConfig>(isDescending, sortBy);

            return _interfaceConfigRepository.GetsAs(projector, spec, sort, Context.Filters.Page<InterfaceConfig>(currentPage, pageSize.Value));
        }

        private void ClearCache()
        {
            _cacheManager.Remove(CacheParam.InterfaceConfigAllKey);
        }
    }
}
