using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : CategoryBll - public - BLL</para>
    /// <para>Create Date : 240912</para>
    /// <para>Author      : GiangPN</para>
    /// <para>Description : BLL tương ứng với bảng Category trong CSDL</para>
    /// </summary>
    public class CategoryBll : ServiceBase
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<CategoryCode> _categoryCodeRepository;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly MemoryCacheManager _cache;

        /// <summary>
        /// Khởi tạo class <see cref="CategoryBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="resourceBll">Bll tương ứng với bảng resource trong CSDL</param>
        /// <param name="generalSettings"></param>
        /// <param name="cache">Quản lý cache</param>
        public CategoryBll(IDbCustomerContext context,
                    ResourceBll resourceBll,
                    AdminGeneralSettings generalSettings, MemoryCacheManager cache)
            : base(context)
        {
            _categoryRepository = Context.GetRepository<Category>();
            _categoryCodeRepository = Context.GetRepository<CategoryCode>();
            _resourceService = resourceBll;
            _generalSettings = generalSettings;
            _cache = cache;
        }

        /// <summary>
        /// Xóa 1 thể loại văn bản
        /// </summary>
        /// <param name="category">Thực thể thể loại văn bản</param>
        public void Delete(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            //TODO: Cần kiểm tra ràng buộc dữ liệu trong các bảng: category_code, store_category
            //var isUsed = _documentBll.Exist(d => d.CategoryId == category.CategoryId);
            //if (isUsed)
            //{
            //    throw new EgovException(_resourceService.GetResource("Bkav.eGovCloud.Areas.Admin.Category.Delete.Exception.Document"));
            //}

            var cateCodes = _categoryCodeRepository.Gets(false, p => p.CategoryId == category.CategoryId);
            if (cateCodes != null && cateCodes.Any())
            {
                foreach (var item in cateCodes)
                {
                    _categoryCodeRepository.Delete(item);
                }
            }

            _categoryRepository.Delete(category);
            Context.SaveChanges();
        }

        /// <summary>
        /// Lất ra tất cả thể loại văn bản theo điều kiện. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        private IEnumerable<Category> Gets(Expression<Func<Category, bool>> spec = null)
        {
            return _categoryRepository.Gets(true, spec);
        }

        /// <summary>
        /// Trả về tất cả hình thức văn bản từ cache
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetsFromCache()
        {
            var result = _cache.Get(CacheParam.CategoryAllKey, CacheParam.CategoryAllCacheTimeOut, () =>
            {
                var data = Gets();
                return AutoMapper.Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryCached>>(data);
            });

            return AutoMapper.Mapper.Map<IEnumerable<CategoryCached>, IEnumerable<Category>>(result);
        }

        /// <summary>
        /// Lất ra tất cả thể loại văn bản theo điều kiện. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="spec">Điều kiện.</param>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Category, T>> projector, Expression<Func<Category, bool>> spec = null)
        {
            return _categoryRepository.GetsAs(projector, spec);
        }

        /// <summary>
        ///   HopCV 170913
        ///   Lấy các mã theo sổ linh vuc. Kết quả chỉ đọc
        /// </summary>
        /// <param name="categoryId">Id sổ văn bản</param>
        /// <param name="projector"></param>
        /// <returns> </returns>
        public IEnumerable<T> GetsAs<T>(int categoryId, Expression<Func<CategoryCode, T>> projector)
        {
            return _categoryCodeRepository.GetsAs(projector, s => s.CategoryId == categoryId);
        }

        /// <summary>
        /// Lấy ra tất cả thể loại văn bản có phân trang và xắp xếp. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="totalRecords"> Tổng số bản ghi </param>
        /// <param name="currentPage"> Trang hiện tại </param>
        /// <param name="pageSize"> Số bản ghi trên 1 trang </param>
        /// <param name="sortBy"> Sắp xếp theo </param>
        /// <param name="isDescending"> Sắp xếp từ lớn đến nhỏ: true, ngược lại false </param>
        /// <param name="categoryBusinessId"></param>
        /// <param name="search"></param>
        /// <returns>Danh sách thể loại văn bản</returns>
        public IEnumerable<Category> Gets(out int totalRecords,
            int categoryBusinessId, int currentPage = 1,
            int? pageSize = null, string sortBy = "",
            bool isDescending = false, string search = null)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                sortBy = string.Empty;
            }

            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            var spec = CategoryQuery.WithCateogryBusinessId(categoryBusinessId);
            if (!string.IsNullOrEmpty(search))
                spec = spec.And(p => p.CategoryName.Contains(search));

            totalRecords = _categoryRepository.Count(spec);
            var sort = Context.Filters.CreateSort<Category>(isDescending, sortBy);
            return _categoryRepository.GetsReadOnly(spec, sort, Context.Filters.Page<Category>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Kiểm tra xem có phần tử nào phù hợp với điều kiện truyền vào hay không
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<Category, bool>> spec)
        {
            return _categoryRepository.Exist(spec);
        }

        /// <summary>
        /// Lấy ra một thể loại văn bản
        /// </summary>
        /// <param name="categoryId">Id của thể loại văn bản</param>
        /// <returns>Entity thể loại văn bản</returns>
        public Category Get(int categoryId)
        {
            Category category = null;
            if (categoryId > 0)
            {
                category = _categoryRepository.Get(categoryId);
            }
            return category;
        }

        /// <summary>
        /// Trả về thể loại văn bản theo tên
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public Category Get(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return null;
            }

            return _categoryRepository.GetReadOnly(CategoryQuery.ContainsKey(categoryName));
        }

        /// <summary>
        /// Trả về hình thức văn bản từ cache theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetFromCache(int id)
        {
            return GetsFromCache().SingleOrDefault(c => c.CategoryId == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="isReadOnly"></param>
        /// <returns></returns>
        public IEnumerable<CategoryCode> GetCodes(int categoryId, bool isReadOnly = true)
        {
            return _categoryCodeRepository.Gets(isReadOnly, p => p.CategoryId == categoryId);
        }

        /// <summary>
        /// Lấy ra một danh sách codeid được check
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<int> GetListCodeIdsChecked(int categoryId)
        {
            return _categoryCodeRepository.GetsAs(p => p.CodeId, p => p.CategoryId == categoryId).ToList();
        }

        /// <summary>
        /// Lấy ra một codeid default
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int getCodeIDCheckedDefault(int categoryId)
        {
            return _categoryCodeRepository.GetAs(p => p.CodeId, p => p.CategoryId == categoryId && p.IsDefault == true);
        }

        /// <summary>
        /// Thêm mới thể loại văn bản
        /// </summary>
        /// <param name="category">Thực thể thể loại văn bản</param>
        /// <returns></returns>
        public void Create(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }
            if (Exist(CategoryQuery.WithCategoryName(category.CategoryName)))
            {
                throw new EgovException(string.Format("Thể loại văn bản ({0}) đã tồn tại!", category.CategoryName));
            }

            if (category.CodeIds != null && category.CodeIds.Count > 0)
            {
                foreach (var item in category.CodeIds)
                {
                    category.CategoryCodes.Add(new CategoryCode { CodeId = item });
                }
            }

            _categoryRepository.Create(category);
            Context.SaveChanges();
        }

        /// <summary>
        /// Thêm mới thể loại văn bản
        /// </summary>
        /// <param name="categories">Danh sach doi tuwong loai hinh doanh nghiep</param>
        /// <param name="ignoreExist">True:Bo qua neu ton tai</param>
        public void Create(IEnumerable<Category> categories, bool ignoreExist)
        {
            if (categories == null)
            {
                throw new ArgumentNullException("categorys");
            }

            var names = categories.Select(x => x.CategoryName);
            var exist = _categoryRepository.GetsAs(p => p.CategoryName, p => names.Contains(p.CategoryName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == categories.Count())
                {
                    throw new EgovException(_resourceService.GetResource("Category.Create.Exist"));
                }

                var list = categories.Where(p => !exist.Contains(p.CategoryName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("Category.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(categories);
            }
        }

        private void Create(IEnumerable<Category> categories)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var category in categories)
            {
                if (category.CodeIds != null && category.CodeIds.Count > 0)
                {
                    foreach (var item in category.CodeIds)
                    {
                        category.CategoryCodes.Add(new CategoryCode { CodeId = item });
                    }
                }
                _categoryRepository.Create(category);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thông tin thể loại văn bản
        /// </summary>
        /// <param name="category">Entity thể loại văn bản</param>
        /// <param name="oldcategoryName">Tên thể loại văn bản trước khi cập nhật</param>
        /// <param name="codeIdDefault">Tên thể loại văn bản trước khi cập nhật</param>
        public void Update(Category category, string oldcategoryName, int codeIdDefault = 0)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            if (Exist(CategoryQuery.WithCategoryName(category.CategoryName.Trim()).And(r => r.CategoryName.ToLower() != oldcategoryName.Trim().ToLower())))
            {
                throw new EgovException(string.Format("Thể loại văn bản ({0}) đã tồn tại!", category.CategoryName));
            }

            #region code

            if (category.CodeIds != null)
            {
                IEnumerable<int> codeIdsAdd;
                IEnumerable<int> codeIdsDelete;
                category.CategoryCodes = GetCodes(category.CategoryId, false).ToList();
                var isEqual = category.CategoryCodes.Select(ur => ur.CodeId)
                                .CompareTo(category.CodeIds, out codeIdsDelete, out codeIdsAdd);
                if (!isEqual)
                {
                    if (codeIdsDelete != null && codeIdsDelete.Any())
                    {
                        var listDeletes = _categoryCodeRepository.Gets(false,
                            ur => ur.CategoryId == category.CategoryId
                            && codeIdsDelete.Contains(ur.CodeId));
                        foreach (var cateCode in listDeletes)
                        {
                            _categoryCodeRepository.Delete(cateCode);
                        }
                    }
                }
                if (codeIdsAdd != null && codeIdsAdd.Any())
                {
                    foreach (var codeId in codeIdsAdd)
                    {
                        _categoryCodeRepository.Create(new CategoryCode { CodeId = codeId, CategoryId = category.CategoryId });
                    }
                }
                if (codeIdDefault != 0)
                {
                    var categoryCodeOld = _categoryCodeRepository.Gets(false, d => d.CategoryId == category.CategoryId && d.IsDefault == true).FirstOrDefault();
                    if (categoryCodeOld != null)
                    {
                        categoryCodeOld.IsDefault = false;
                    }
                    var categoryCode = _categoryCodeRepository.Gets(false, d => d.CategoryId == category.CategoryId && d.CodeId == codeIdDefault).FirstOrDefault();
                    if (categoryCode != null)
                    {
                        categoryCode.IsDefault = true;
                    }
                    Context.SaveChanges();
                }
            }
            else
            {
                var listDeletes = _categoryCodeRepository.Gets(false, ur => ur.CategoryId == category.CategoryId);
                foreach (var cateCode in listDeletes)
                {
                    _categoryCodeRepository.Delete(cateCode);
                }
            }
            #endregion

            Context.SaveChanges();
        }
    }
}
