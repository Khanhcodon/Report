using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects.CacheObjects;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : StoreBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 180912</para>
    /// <para>Author      : Giangpn</para>
    /// <para>Description : BLL tương ứng với bảng Store trong CSDL</para>
    /// </summary>
    public class StoreBll : ServiceBase
    {
        private readonly IRepository<Store> _storeRepository;
        private readonly IRepository<StoreCode> _storeCodeRepository;
        private readonly IRepository<DocTypeStore> _doctypeStoreRepository;
        private readonly CodeBll _codeService;
        private readonly ResourceBll _resourceService;
        private readonly UserBll _userSercive;
        private readonly MemoryCacheManager _cache;

        /// <summary>
        /// Khởi tạo class <see cref="StoreBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="codeService">Bll tương ứng với bảng Code</param>
        /// <param name="resourceService">>Bll tương ứng với bảng Resource</param>
        /// <param name="userService">Bll usser</param>
        /// <param name="cache">Memory cache</param>
        public StoreBll(IDbCustomerContext context,
                CodeBll codeService,
                ResourceBll resourceService, UserBll userService, MemoryCacheManager cache)
            : base(context)
        {
            _storeRepository = Context.GetRepository<Store>();
            _storeCodeRepository = Context.GetRepository<StoreCode>();
            _doctypeStoreRepository = Context.GetRepository<DocTypeStore>();
            _codeService = codeService;
            _resourceService = resourceService;
            _userSercive = userService;
            _cache = cache;
        }

        #region Admin

        /// <summary>
        /// Lất ra tất cả thể loại văn bản theo điều kiện. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<Store> Gets(Expression<Func<Store, bool>> spec = null)
        {
            return _storeRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Lấy ra tất cả thể loại văn bản có phân trang và xắp xếp. Kết quả chỉ đọc
        /// </summary>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="projector"></param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="categoryBusinessId"></param>
        /// <param name="searchName"></param>
        /// <returns>Danh sách lĩnh vực</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Store, T>> projector,
            string sortBy, int categoryBusinessId, bool isDescending = false,
            string searchName = null)
        {
            if (string.IsNullOrEmpty(sortBy))
                sortBy = string.Empty;

            var spec = StoreQuery.WithCategoryBusinessId(categoryBusinessId);

            if (!string.IsNullOrEmpty(searchName))
            {
                spec = spec.And(p => p.StoreName.Contains(searchName));
            }
            var sort = Context.Filters.CreateSort<Store>(isDescending, sortBy);
            return _storeRepository.GetsAs(projector, spec, sort);
        }

        /// <summary>
        /// Lấy ra danh sách hồ sơ
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="spec">Điều kiện</param>
        /// <returns>Danh sách sổ hồ sơ</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Store, T>> projector, Expression<Func<Store, bool>> spec = null)
        {
            return _storeRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Lấy ra danh sách id các mẫu cho sổ hồ sơ
        /// </summary>
        /// <param name="storeId">Id sổ hồ sơ</param>
        /// <returns></returns>
        public IEnumerable<int> GetCodeIds(int storeId)
        {
            return _storeCodeRepository.GetsAs(s => s.CodeId, s => s.StoreId == storeId);
        }

        #endregion

        #region Customer



        #endregion

        /// <summary>
        /// Trả về tất cả store từ cache
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StoreCached> GetsFromCache()
        {
            return _cache.Get<IEnumerable<StoreCached>>(CacheParam.StoreAllKey, CacheParam.StoreAllCacheTimeOut, () =>
            {
                var result = new List<StoreCached>();
                var stores = Gets();
                if (!stores.Any())
                {
                    return result;
                }

                var storeIds = stores.Select(st => st.StoreId);
                var storeCodes = _storeCodeRepository.GetsReadOnly(sc => storeIds.Contains(sc.StoreId));

                foreach (var store in stores)
                {
                    var cache = new StoreCached();
                    cache.StoreId = store.StoreId;
                    cache.StoreName = store.StoreName;
                    cache.UserId = store.UserId;
                    cache.CategoryBusinessId = store.CategoryBusinessId;

                    var userInStore = new List<int>();
                    if (store.UserId.HasValue)
                    {
                        userInStore.Add(store.UserId.Value);
                    }

                    if (store.DepartmentId.HasValue) { }

                    if (!string.IsNullOrEmpty(store.UserViewIds))
                    {
                        userInStore.AddRange(store.ListUserViewIds);
                    }

                    cache.ListUserViewIds = userInStore;
                    cache.CodeIds = storeCodes.Where(sc => sc.StoreId == store.StoreId).Select(sc => sc.CodeId).ToList();

                    result.Add(cache);
                }

                return result;
            });
        }

        /// <summary>
        /// Trả về store từ cache theo id
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public StoreCached GetFromCache(int storeId)
        {
            if (storeId <= 0)
            {
                return null;
            }

            return GetsFromCache().SingleOrDefault(st => st.StoreId == storeId);
        }

        /// <summary>
        /// Lấy ra một sổ hồ sơ
        /// </summary>
        /// <param name="storeId">Id của sổ hồ sơ</param>
        /// <returns>Entity sổ hồ sơ</returns>
        public Store Get(int storeId)
        {
            Store store = null;
            if (storeId > 0)
            {
                store = _storeRepository.Get(storeId);
            }
            return store;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<StoreCached> GetsByUser(int userId)
        {
            var stores = GetsFromCache();
            return stores.Where(s => s.UserId == userId || s.ListUserViewIds.Contains(userId));
        }

        /// <summary>
        /// Lấy code default
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public int GetCodeIdDefault(int storeId)
        {
            return _storeCodeRepository.GetAs(s => s.CodeId, s => s.StoreId == storeId && s.IsDefault == true);
        }

        /// <summary>
        /// Thêm mới sổ hồ sơ
        /// </summary>
        /// <param name="store">Thực thể sổ hồ sơ</param>
        /// <returns></returns>
        public void Create(Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            if (_storeRepository.Exist(StoreQuery.WithStoreName(store.StoreName)))
            {
                throw new EgovException(string.Format(_resourceService.GetResource("Store.CreateOrEdit.Fields.StoreName.Exist"), store.StoreName));
            }
            if (!string.IsNullOrEmpty(store.CodeIds))
            {
                var arrCodeId = Json2.ParseAs<List<int>>(store.CodeIds);
                var allCodeIds = _codeService.GetsAs(c => c.CodeId);
                foreach (var codeId in arrCodeId.Where(allCodeIds.Contains).ToList())
                {
                    store.StoreCodes.Add(new StoreCode { CodeId = codeId });
                }
            }
            _storeRepository.Create(store);
            Context.SaveChanges();

            _cache.Remove(CacheParam.StoreAllKey);
        }

        /// <summary>
        /// Cập nhật thông tin sổ hồ sơ
        /// </summary>
        /// <param name="store">Entity sổ hồ sơ</param>
        /// <param name="oldStoreName">Tên sổ hồ sơ trước khi cập nhật</param>
        public void Update(Store store, string oldStoreName)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            if (_storeRepository.Exist(StoreQuery.WithStoreName(store.StoreName.Trim()).And(r => r.StoreName.ToLower() != oldStoreName.Trim().ToLower())))
            {
                throw new EgovException(string.Format(_resourceService.GetResource("Store.CreateOrEdit.Fields.StoreName.Exist"), store.StoreName));
            }

            //Cập nhật thông tin về sổ hồ sơ trong loại hồ sơ(văn bản)
            var storeCodeDelete = _storeCodeRepository.Gets(false, ur => ur.StoreId == store.StoreId);
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var storeCode in storeCodeDelete)
            {
                _storeCodeRepository.Delete(storeCode);
            }
            if (!string.IsNullOrEmpty(store.CodeIds))
            {
                var arrCodeId = Json2.ParseAs<List<int>>(store.CodeIds);
                var allCodeIds = _codeService.GetsAs(c => c.CodeId);
                var storeCodeAdd = arrCodeId.Where(allCodeIds.Contains).Select(codeId => new StoreCode { StoreId = store.StoreId, CodeId = codeId }).ToList();
                foreach (var storeCode in storeCodeAdd)
                {
                    _storeCodeRepository.Create(storeCode);
                }
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();

            _cache.Remove(CacheParam.StoreAllKey);
        }

        /// <summary>
        /// Update code default
        /// </summary>
        /// <param name="store"></param>
        /// <param name="defaultCodeId"></param>
        public void UpdateCodeDefault(Store store, int defaultCodeId)
        {
            if (defaultCodeId != 0)
            {
                var storeOld = _storeCodeRepository.Gets(false, s => s.StoreId == store.StoreId && s.IsDefault == true).FirstOrDefault();
                if (storeOld != null)
                {
                    if (storeOld.CodeId != defaultCodeId)
                    {
                        storeOld.IsDefault = false;
                        UpdateNewCodeDefault(store, defaultCodeId);
                    }
                }
                else
                {
                    UpdateNewCodeDefault(store, defaultCodeId);
                }
                Context.SaveChanges();
            }

            _cache.Remove(CacheParam.StoreAllKey);
        }

        private void UpdateNewCodeDefault(Store store, int defaultCodeId)
        {
            var storeNew = _storeCodeRepository.Gets(false, s => s.StoreId == store.StoreId && s.CodeId == defaultCodeId).FirstOrDefault();
            if (storeNew != null)
            {
                storeNew.IsDefault = true;
            }

            _cache.Remove(CacheParam.StoreAllKey);
        }

        /// <summary>
        /// Xóa 1 sổ hồ sơ
        /// </summary>
        /// <param name="store">Thực thể sổ hồ sơ</param>
        public void Delete(Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            //TODO: Cần kiểm tra ràng buộc dữ liệu trong các bảng: store_code, store_doc, store_category, doctype_store
            //kiểm tra ràng buộc dữ liệu trong bảng doctype_store
            //var isUsed = store.DocTypeStores.Any();
            //if (isUsed)
            //{
            //    throw new EgovException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Store.Delete.Exception.DocType"));
            //}
            var storeCodes = _storeCodeRepository.Gets(false, ur => ur.StoreId == store.StoreId);
            foreach (var storeCode in storeCodes)
            {
                _storeCodeRepository.Delete(storeCode);
            }
            _storeRepository.Delete(store);
            Context.SaveChanges();

            _cache.Remove(CacheParam.StoreAllKey);
        }

        /// <summary>
        /// Sử dụng để fix lỗi convert.
        /// </summary>
        public void FixConvert()
        {
            var storePrivateRepository = Context.GetRepository<StorePrivate>();
            var splitChars = new char[] { '\\', '/' };
            var stores = storePrivateRepository.Gets(isReadOnly: true)
                                .OrderBy(s => s.StorePrivateName).ThenBy(s => s.CreatedOnDate);

            var storeChanges = new Dictionary<int, Dictionary<int, string>>();

            foreach (var store in stores)
            {
                var name = store.StorePrivateName;
                var parentNames = name.Split(splitChars);
                var idx = name.LastIndexOfAny(splitChars);
                if (parentNames.Length == 1)
                {
                    // Không có phân cấp
                    continue;
                }

                var surName = parentNames.Last();
                int? parentId = null;
                for (var i = 0; i < parentNames.Length - 1; i++)
                {
                    var parentName = parentNames[i];
                    var parents = storePrivateRepository.GetsReadOnly(s => s.StorePrivateName.Equals(parentName, StringComparison.OrdinalIgnoreCase) 
                                                            && (s.CreatedByUserId == store.CreatedByUserId)
                                                            && s.Status != (byte)StorePrivateStatus.IsDelete);
                    var parent = parents.Any() ? parents.First() : null;

                    if (parent == null)
                    {
                        var result = new StorePrivate()
                        {
                            StorePrivateName = parentName,
                            Status = 0,
                            Description = store.Description,
                            CreatedOnDate = store.CreatedOnDate,
                            CreatedByUserId = store.CreatedByUserId,
                            ParentId = parentId
                        };

                        storePrivateRepository.Create(result);
                        Context.SaveChanges();
                        parentId = result.StorePrivateId;
                    }
                    else
                    {
                        parentId = parent.StorePrivateId;
                    }
                }

                var value = new Dictionary<int, string>();
                if (parentId.HasValue)
                {
                    value.Add(parentId.Value, surName);
                }
                storeChanges.Add(store.StorePrivateId, value);
            }

            if (storeChanges.Any())
            {
                foreach (var change in storeChanges)
                {
                    if (!change.Value.Any())
                    {
                        continue;
                    }

                    var store = storePrivateRepository.Get(change.Key);
                    if (store == null)
                    {
                        continue;
                    }

                    var changeValues = change.Value.First();
                    store.ParentId = changeValues.Key;
                    store.StorePrivateName = changeValues.Value;
                }

                Context.SaveChanges();
            }

            var cacheKey = string.Format(CacheParam.PrivateStore, "all");

            _cache.Remove(cacheKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteDatabase<T>(string query)
        {
            var docs = Context.RawQuery(query);
            var a = Json2.Stringify(docs);
            var result = Json2.ParseAs<IEnumerable<T>>(a);

            return result;
        }
    }
}
