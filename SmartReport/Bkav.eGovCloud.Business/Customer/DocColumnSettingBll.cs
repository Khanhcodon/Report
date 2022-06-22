using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : DocColumnSettingBll - public - Bll</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 241115</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : Bll tương ứng với bảng DocColumnSetting trong CSDL</para>
    /// </summary>
    public class DocColumnSettingBll : ServiceBase
    {
        private readonly IRepository<DocColumnSetting> _docColumnSettingRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly ResourceBll _resourceService;
        private readonly MemoryCacheManager _cacheManager;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings"></param>
        /// <param name="resourceService"></param>
        /// <param name="cacheManager"></param>
        public DocColumnSettingBll(IDbCustomerContext context
            , AdminGeneralSettings generalSettings
            , ResourceBll resourceService
            , MemoryCacheManager cacheManager)
            : base(context)
        {
            _docColumnSettingRepository = Context.GetRepository<DocColumnSetting>();
            _generalSettings = generalSettings;
            _resourceService = resourceService;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Lấy ra tất cả các funtion
        /// </summary>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách funtion</returns>
        public IEnumerable<DocColumnSetting> Gets(Expression<Func<DocColumnSetting, bool>> spec = null)
        {
            return _docColumnSettingRepository.Gets(false, spec);
        }

        /// <summary>
        /// Lấy ra tất cả các funtion
        /// </summary>
        /// <returns>Danh sách funtion</returns>
        public IEnumerable<DocColumnSetting> GetAllCaches()
        {
            var allDocColumns = _cacheManager.Get(CacheParam.DocColumnSettingAllKey,
              CacheParam.DocColumnSettingKeyAllCacheTimeOut, () =>
              {
                  var result = _docColumnSettingRepository.Gets(true);
                  return AutoMapper.Mapper.Map<IEnumerable<DocColumnSetting>, IEnumerable<DocColumnSettingCached>>(result);
              });

            return AutoMapper.Mapper.Map<IEnumerable<DocColumnSettingCached>, IEnumerable<DocColumnSetting>>(allDocColumns); ;
        }

        /// <summary>
        /// Lấy ra tất cả các funtion
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách funtion</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<DocColumnSetting, T>> projector, Expression<Func<DocColumnSetting, bool>> spec = null)
        {
            return _docColumnSettingRepository.GetsAs(projector, spec);
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
                                       Expression<Func<DocColumnSetting, T>> projector,
                                       Expression<Func<DocColumnSetting, bool>> spec = null,
                                       int currentPage = 1,
                                       int? pageSize = null,
                                       string sortBy = "",
                                       bool isDescending = false)
        {
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _docColumnSettingRepository.Count(spec);
            var sort = Context.Filters.CreateSort<DocColumnSetting>(isDescending, sortBy);
            var paging = Context.Filters.Page<DocColumnSetting>(currentPage, pageSize.Value);
            return _docColumnSettingRepository.GetsAs(projector, spec, sort, paging);
        }


        /// <summary>
        /// Lấy ra funtion theo id
        /// </summary>
        /// <returns>Funtion</returns>
        public DocColumnSetting Get(int id)
        {
            DocColumnSetting result = null;
            if (id > 0)
            {
                result = _docColumnSettingRepository.Get(id);
            }
            return result;
        }

        /// <summary>
        /// Tạo mới 1 setting
        /// </summary>
        /// <param name="setting"></param>
        public void Create(DocColumnSetting setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException("setting");
            }

            _docColumnSettingRepository.Create(setting);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.DocColumnSettingAllKey);
        }

        /// <summary>
        /// Tạo mới  danh sách cấu hình cột hiển thị trên danh sách văn bản
        /// </summary>
        /// <param name="docColumnSettings">Danh sách cột hiển thị</param>
        /// <param name="ignoreExist">True: Bỏ qua những cấu hình đã tồn tại; False: validate khi danh sách đã tồn tại</param>
        public void Create(IEnumerable<DocColumnSetting> docColumnSettings, bool ignoreExist)
        {
            if (docColumnSettings == null || !docColumnSettings.Any())
            {
                throw new ArgumentNullException("docColumnSettings");
            }

            var names = docColumnSettings.Select(x => x.DocColumnSettingName);
            var exist = _docColumnSettingRepository.GetsAs(p => p.DocColumnSettingName, p => names.Contains(p.DocColumnSettingName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == docColumnSettings.Count())
                {
                    throw new EgovException(_resourceService.GetResource("DocColumnSetting.Create.Exist"));
                }

                var list = docColumnSettings.Where(p => !exist.Contains(p.DocColumnSettingName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("DocColumnSetting.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(docColumnSettings);
            }
        }

        private void Create(IEnumerable<DocColumnSetting> docColumnSettings)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var docColumnSetting in docColumnSettings)
            {
                _docColumnSettingRepository.Create(docColumnSetting);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.DocColumnSettingAllKey);
        }

        /// <summary>
        /// Cập nhật 1 setting
        /// </summary>
        /// <param name="setting"></param>
        public void Update(DocColumnSetting setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException("setting");
            }

            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.DocColumnSettingAllKey);
        }

        /// <summary>
        /// Xóa 1 function
        /// </summary>
        /// <param name="setting">Entity</param>
        public void Delete(DocColumnSetting setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException("setting");
            }

            _docColumnSettingRepository.Delete(setting);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.DocColumnSettingAllKey);
        }
    }
}
