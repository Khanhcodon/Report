using System;
using System.Collections.Generic;
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
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : PermissionSettingBll - public - Bll</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 241115</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : Bll tương ứng với bảng ProcessFunction trong CSDL, dùng để cấu hình các node trong cây văn bản đang xử lý trên trang chủ</para>
    /// </summary>
    public class PermissionSettingBll : ServiceBase
    {
        private readonly IRepository<PermissionSetting> _permissionSettingRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly MemoryCacheManager _cacheManager;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings"></param>
        /// <param name="cacheManager"></param>
        public PermissionSettingBll(IDbCustomerContext context,
            AdminGeneralSettings generalSettings,
            MemoryCacheManager cacheManager)
            : base(context)
        {
            _permissionSettingRepository = Context.GetRepository<PermissionSetting>();
            _generalSettings = generalSettings;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Lấy ra tất cả các funtion
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách funtion</returns>
        public IEnumerable<PermissionSetting> Gets(Expression<Func<PermissionSetting, bool>> spec = null)
        {
            return _permissionSettingRepository.Gets(false, spec);
        }

        /// <summary>
        /// Lấy ra tất cả các funtion
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách funtion</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<PermissionSetting, T>> projector, Expression<Func<PermissionSetting, bool>> spec = null)
        {
            return _permissionSettingRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Lấy ra danh sách tất cả PermissionSettings(Danh sách này sẽ được cache lại trong 60 phút)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PermissionSetting> GetCacheAllPermissionSettings()
        {
            var allPermissionSettings = _cacheManager.Get(CacheParam.PermissionSettingAllKey,
                CacheParam.PermissionSettingAllCacheTimeOut,
                () => {
                    var result = _permissionSettingRepository.Gets(true);
                    return AutoMapper.Mapper.Map<IEnumerable<PermissionSetting>, IEnumerable<PermissionSettingCached>>(result);
                });

            return AutoMapper.Mapper.Map<IEnumerable<PermissionSettingCached>, IEnumerable<PermissionSetting>>(allPermissionSettings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="totalRecords"></param>
        /// <param name="projector"></param>
        /// <param name="spec"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(out int totalRecords,
                                       Expression<Func<PermissionSetting, T>> projector,
                                       Expression<Func<PermissionSetting, bool>> spec = null,
                                       int currentPage = 1,
                                       int? pageSize = null,
                                       string sortBy = "",
                                       bool isDescending = false)
        {
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _permissionSettingRepository.Count(spec);
            var sort = Context.Filters.CreateSort<PermissionSetting>(isDescending, sortBy);
            var paging = Context.Filters.Page<PermissionSetting>(currentPage, pageSize.Value);
            return _permissionSettingRepository.GetsAs(projector, spec, sort, paging);
        }

        /// <summary>
        /// Lấy ra funtion theo id
        /// </summary>
        /// <returns>Funtion</returns>
        public PermissionSetting Get(int id)
        {
            PermissionSetting result = null;
            if (id > 0)
            {
                result = _permissionSettingRepository.Get(id);
            }
            return result;
        }

        /// <summary>
        /// Tạo mới 1 setting
        /// </summary>
        /// <param name="setting"></param>
        public void Create(PermissionSetting setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException("setting");
            }

            _permissionSettingRepository.Create(setting);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.PermissionSettingAllKey);
        }

        /// <summary>
        /// Cập nhật 1 setting
        /// </summary>
        /// <param name="setting"></param>
        public void Update(PermissionSetting setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException("setting");
            }

            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.PermissionSettingAllKey);
        }

        /// <summary>
        /// Xóa 1 function
        /// </summary>
        /// <param name="setting">Entity</param>
        public void Delete(PermissionSetting setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException("setting");
            }

            _permissionSettingRepository.Delete(setting);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.PermissionSettingAllKey);
        }
    }
}
