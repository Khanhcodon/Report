using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Business.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ResourceBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 300712
    /// Author      : TrungVH
    /// Description : BLL tương ứng với bảng Resource trong CSDL
    /// </summary>
    public class ResourceBll : ServiceBase
    {
        private readonly IRepository<Resource> _resourceRepository;
        private readonly LogBll _logService;
        private readonly MemoryCacheManager _cacheManager;
        private readonly AdminGeneralSettings _generalSettings;

        /////<summary>
        ///// Khởi tạo class <see cref="ResourceBll"/>.
        /////</summary>
        /////<param name="context">Admin context</param>
        /////<param name="logService">Bll tương ứng với bảng Log trong CSDL</param>
        /////<param name="cacheManager">Cache manager</param>
        /////<param name="generalSettings">Cấu hình chung</param>
        //public ResourceBll(IDbAdminContext context, LogBll logService,
        //                    MemoryCacheManager cacheManager, AdminGeneralSettings generalSettings)
        //    : base(context)
        //{
        //    _resourceRepository = Context.GetRepository<Resource>();
        //    _logService = logService;
        //    _cacheManager = cacheManager;
        //    _generalSettings = generalSettings;
        //}

        ///<summary>
        /// Khởi tạo class <see cref="ResourceBll"/>.
        ///</summary>
        ///<param name="context">Customer context</param>
        ///<param name="logService">Bll tương ứng với bảng Log trong CSDL</param>
        ///<param name="cacheManager">Cache manager</param>
        ///<param name="generalSettings">Cấu hình chung</param>
        public ResourceBll(IDbCustomerContext context, LogBll logService,
                            MemoryCacheManager cacheManager, AdminGeneralSettings generalSettings)
            : base(context)
        {
            _resourceRepository = Context.GetRepository<Resource>();
            _logService = logService;
            _cacheManager = cacheManager;
            _generalSettings = generalSettings;
        }

        /// <summary>
        /// Xóa tài nguyên
        /// </summary>
        /// <param name="resource">Entity tài nguyên</param>
        public void Delete(Resource resource)
        {
            if (resource == null)
                throw new ArgumentNullException("resource");

            _resourceRepository.Delete(resource);
            Context.SaveChanges();

            ClearCache();
        }

        /// <summary>
        /// Lấy ra tài nguyên bằng id
        /// </summary>
        /// <param name="id">Id của tài nguyên</param>
        /// <returns>Entity tài nguyên</returns>
        public Resource GetById(int id)
        {
            Resource resource = null;
            if (id > 0)
            {
                resource = _resourceRepository.Get(id);
            }
            return resource;
        }

        /// <summary>
        /// Lấy ra tài nguyên bằng key
        /// </summary>
        /// <param name="resourceKey">Key đại diện cho tài nguyên</param>
        /// <param name="logIfNotFound">Một giá trị để xác định có ghi log vào hệ thống hay không khi không tìm thấy tài nguyên</param>
        /// <returns>Entity tài nguyên</returns>
        public Resource GetLocaleStringResourceByName(string resourceKey, bool logIfNotFound = true)
        {
            Resource resource = null;

            if (_generalSettings.IsLoadAllResourceOnStartup)
            {
                //lấy ra tất cả tài nguyên (sẽ lấy trong cache nếu đã tồn tại, nếu không sẽ truy vấn vào db và lưu lại kết quả vào cache để sử dụng cho lần sau)
                if (string.IsNullOrEmpty(resourceKey))
                    resourceKey = string.Empty;
                resourceKey = resourceKey.Trim().ToLowerInvariant();

                var resources = GetAllResources();
                if (resources.ContainsKey(resourceKey))
                {
                    var id = resources[resourceKey].ResourceId;
                    resource = _resourceRepository.Get(id);
                }
            }
            else
            {
                //lazy loading
                resource = _resourceRepository.Get(false, r => r.ResourceKey == resourceKey);
            }
            if (resource == null && logIfNotFound)
            {
                //Ghi Log không tìm thấy resource
                //(Lưu ý là đoạn này nghiêm cấm không được sử dụng hàm để lấy ra resouce mà phải hardcode như thế này để tránh bị lỗi stack overflow)
                // _logService.Warning("Không tìm thấy chuỗi tài nguyên với key là {0}!", args: resourceKey);
            }

            return resource;
        }

        /// <summary>
        /// Lấy ra tất cả các tài nguyên. Kết quả chỉ để đọc
        /// </summary>
        /// <returns>Danh sách tài nguyên dạng từ điển</returns>
        public Dictionary<string, ResourceCached> GetAllResources()
        {
            return _cacheManager.Get(CacheParam.LocalstringresourcesAllKey, CacheParam.LocalstringresourcesAllCacheTimeOut, () =>
            {
                var allResource = _resourceRepository.GetsReadOnly(null,
                    Context.Filters.Sort<Resource, string>(r => r.ResourceKey));

                var data = AutoMapper.Mapper.Map<IEnumerable<Resource>, IEnumerable<ResourceCached>>(allResource);
                var resourceDictionary = data.ToDictionary(s => s.ResourceKey.ToLowerInvariant());

                return resourceDictionary;
            });
        }

        /// <summary>
        /// Lấy ra tất cả các tài nguyên có phân trang. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="resourceKey">Key của resource (dùng để tìm kiếm) Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các resource có key gần giống với key truyền vào</param>
        /// <returns>Danh sách các tài nguyên đã được phân trang</returns>
        public IEnumerable<Resource> GetAllResources(out int totalRecords,
                                                        int currentPage = 1,
                                                        int? pageSize = null,
                                                        string sortBy = "",
                                                        bool isDescending = false,
                                                        string resourceKey = "")
        {
            var spec = !string.IsNullOrWhiteSpace(resourceKey)
                        ? ResourceQuery.ContainsKey(resourceKey)
                        : null;

            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _resourceRepository.Count(spec);
            var sort = Context.Filters.CreateSort<Resource>(isDescending, sortBy);
            return _resourceRepository.GetsReadOnly(spec, sort, Context.Filters.Page<Resource>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Thêm mới tài nguyên
        /// </summary>
        /// <param name="resource">Entity tài nguyên</param>
        public void Create(Resource resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }

            if (_resourceRepository.Exist(ResourceQuery.WithKey(resource.ResourceKey)))
            {
                throw new EgovException(string.Format(GetResource("Common.Resource.CreateOrEdit.Fields.ResourceKey.Exist"), resource.ResourceKey));
            }

            _resourceRepository.Create(resource);
            Context.SaveChanges();

            //xóa cache
            ClearCache();
        }

        /// <summary>
        /// Cập nhật tài nguyên
        /// </summary>
        /// <param name="resource">Entity tài nguyên</param>
        /// <param name="oldResourceKey">Key trước khi được cập nhật (tham số này sẽ được dùng để kiểm tra khi key mới thay đổi có trùng với 1 key nào đó hay không và có khác key cũ hay không)</param>
        public void Update(Resource resource, string oldResourceKey)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }
            if (_resourceRepository.Exist(ResourceQuery.WithKey(resource.ResourceKey).And(r => r.ResourceKey.ToLower() != oldResourceKey.ToLower())))
            {
                throw new EgovException(string.Format(GetResource("Common.Resource.CreateOrEdit.Fields.ResourceKey.Exist"), resource.ResourceKey));
            }
            Context.SaveChanges();
            ClearCache();
        }

        /// <summary>
        /// Lấy ra giá trị của tài nguyên dựa vào key của tài nguyên
        /// </summary>
        /// <param name="resourceKey">Key tài nguyên.</param>
        /// <param name="logIfNotFound">Một giá trị để xác định có ghi log vào hệ thống hay không khi không tìm thấy tài nguyên</param>
        /// <param name="defaultValue">Giá trị mặc định</param>
        /// <param name="returnEmptyIfNotFound">Một giá trị để xác định khi không tìm thấy tài nguyên sẽ trả về 1 chuỗi rỗng</param>
        /// <returns>Giá trị của tài nguyên</returns>
        public string GetResource(string resourceKey, bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            var result = string.Empty;
            var resourceKeyValue = resourceKey ?? string.Empty;
            resourceKeyValue = resourceKeyValue.Trim().ToLowerInvariant();

            // TienBV: fix luôn.
            _generalSettings.IsLoadAllResourceOnStartup = true;
            if (_generalSettings.IsLoadAllResourceOnStartup)
            {
                //lấy ra tất cả tài nguyên (sẽ lấy trong cache nếu đã tồn tại, nếu không sẽ truy vấn vào db và lưu lại kết quả vào cache để sử dụng cho lần sau)
                var resources = GetAllResources();

                if (resources.ContainsKey(resourceKeyValue))
                {
                    var lsr = resources[resourceKeyValue];
                    if (lsr != null)
                        result = lsr.ResourceValue;
                }
            }
            //else
            //{
            //    //lazy loading
            //    var key = string.Format(CacheParam.LocalstringresourcesByResourcenameKey, resourceKeyValue);
            //    var resourceValue = _cacheManager.Get(key, CacheParam.LocalstringresourcesByResourcenameCacheTimeOut, () =>
            //    {
            //        var resource = _resourceRepository.GetReadOnly(r => r.ResourceKey == resourceKeyValue);
            //        return resource != null ? resource.ResourceValue : string.Empty;
            //    });

            //    if (!string.IsNullOrEmpty(resourceValue))
            //    {
            //        result = resourceValue;
            //    }
            //}

            if (string.IsNullOrEmpty(result))
            {
                if (logIfNotFound)
                {
                    //Ghi log không tìm thấy resource 
                    //(Lưu ý là đoạn này nghiêm cấm không được sử dụng hàm để lấy ra resouce mà phải hardcode như thế này để tránh bị lỗi stack overflow)
                    // _logService.Warning("Không tìm thấy chuỗi tài nguyên với key là \"{0}\"!", args: resourceKey);
                }

                if (!String.IsNullOrEmpty(defaultValue))
                {
                    result = defaultValue;
                }
                else
                {
                    if (!returnEmptyIfNotFound)
                    {
                        result = resourceKey;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Lấy description của enum trong db
        /// </summary>
        /// <typeparam name="T">Loại enum</typeparam>
        /// <param name="en">enum</param>
        /// <returns></returns>
        public string GetEnumDescription<T>(T en) where T : struct,IConvertible
        {
            var description = EnumHelper<T>.GetDescription(en);
            return GetResource(description);
        }

        /// <summary>
        /// GetNameAndDescription
        /// </summary>
        /// <typeparam name="T">Loại eNum</typeparam>
        /// <returns></returns>
        public IDictionary<string, string> GetNameAndDescription<T>() where T : struct,IConvertible
        {
            var result = new Dictionary<string, string>();
            var descriptions = EnumHelper<T>.GetNameAndDescription();
            foreach (var item in descriptions)
            {
                result.Add(item.Key, GetResource(item.Value));
            }

            return result;
        }

        /// <summary>
        /// EnumToSelectList
        /// </summary>
        /// <typeparam name="T">Loại enum</typeparam>
        /// <param name="selected">selected</param>
        /// <returns></returns>
        public List<SelectListItem> EnumToSelectList<T>(int? selected = null) where T : struct,IConvertible
        {
            var selectedList = EnumHelper<T>.EnumToSelectList(selected);
            for (var i = 0; i < selectedList.Count; i++)
            {
                selectedList[i].Text = GetResource(selectedList[i].Text);
            }
            return selectedList;
        }

        /// <summary>
        /// Xóa cache của tài nguyên
        /// </summary>
        public void ClearCache()
        {
            _cacheManager.Remove(CacheParam.LocalstringresourcesAllKey);
        }

        /// <summary>
        /// Xuất tất cả resource ra file json
        /// </summary>
        /// <returns>Chuỗi json</returns>
        public string ExportResourcesToJson()
        {
            var resources = _resourceRepository.GetsReadOnly(null,
                    Context.Filters.Sort<Resource, string>(r => r.ResourceKey));
            return resources.ToDictionary(r => r.ResourceKey, r => r.ResourceValue).Stringify();
        }

        /// <summary>
        /// Import resource từ file json
        /// </summary>
        /// <param name="jsonStream">Stream json</param>
        public void ImportResourceFromJson(Stream jsonStream)
        {
            var resources = _resourceRepository.Gets(false);
            Dictionary<string, string> resourcesImport;
            try
            {
                resourcesImport = Json2.ParseAs<Dictionary<string, string>>(jsonStream);
            }
            catch (Exception ex)
            {
                throw new EgovException(GetResource("Common.Resource.Import.FileIncorrect"), ex);
            }

            if (resources == null)
            {
                foreach (var resourceKey in resourcesImport.Keys)
                {
                    if (string.IsNullOrWhiteSpace(resourceKey))
                    {
                        continue;
                    }
                    var resourceValue = resourcesImport[resourceKey];
                    if (string.IsNullOrWhiteSpace(resourceValue))
                    {
                        continue;
                    }
                    _resourceRepository.Create(new Resource { ResourceKey = resourceKey, ResourceValue = resourceValue });
                }
                Context.SaveChanges();
            }
            else
            {
                foreach (var resourceKey in resourcesImport.Keys)
                {
                    if (string.IsNullOrWhiteSpace(resourceKey))
                    {
                        continue;
                    }
                    var resourceValue = resourcesImport[resourceKey];
                    if (string.IsNullOrWhiteSpace(resourceValue))
                    {
                        continue;
                    }

                    var resource = resources.SingleOrDefault(r => r.ResourceKey.Equals(resourceKey, StringComparison.InvariantCultureIgnoreCase));
                    if (resource == null)
                    {
                        _resourceRepository.Create(new Resource { ResourceKey = resourceKey, ResourceValue = resourceValue });
                    }
                    else
                    {
                        resource.ResourceValue = resourceValue;
                    }
                }
                Context.SaveChanges();
            }

            ClearCache();
        }
    }
}
