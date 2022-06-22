using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : TreeGroupBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 021115</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : BLL tương ứng với bảng Tree_Group trong CSDL</para>
    /// </summary>
    public class TreeGroupBll : ServiceBase
    {
        private readonly IRepository<TreeGroup> _treeGroupRepository;
        private readonly MemoryCacheManager _cacheManager;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cacheManager"></param>
        public TreeGroupBll(IDbCustomerContext context, MemoryCacheManager cacheManager)
            : base(context)
        {
            _treeGroupRepository = Context.GetRepository<TreeGroup>();
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Tạo mới nhóm cây
        /// </summary>
        /// <param name="treeGroup">đối tượng nhóm cây</param>
        public void Create(TreeGroup treeGroup)
        {
            if (treeGroup == null)
            {
                throw new ArgumentNullException("TreeGroup");
            }

            _treeGroupRepository.Create(treeGroup);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.TreeGroupAllKey);
        }

        /// <summary>
        /// Tạo mới nhóm cây
        /// </summary>
        /// <param name="treeGroups">đối tượng nhóm cây</param>
        public void Create(IEnumerable<TreeGroup> treeGroups)
        {
            if (treeGroups == null || !treeGroups.Any())
            {
                throw new ArgumentNullException("TreeGroup");
            }

            foreach (var treeGroup in treeGroups)
            {
                if (!Exist(x => x.TreeGroupName.Equals(treeGroup.TreeGroupName, StringComparison.OrdinalIgnoreCase)))
                {
                    _treeGroupRepository.Create(treeGroup);
                }
            }

            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.TreeGroupAllKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public bool Exist(Expression<Func<TreeGroup, bool>> spec)
        {
            return _treeGroupRepository.Exist(spec);
        }

        /// <summary>
        /// Lấy tất cả danh sách nhóm cây theo điều kiện truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        /// <returns></returns>
        public IEnumerable<TreeGroup> Gets(Expression<Func<TreeGroup, bool>> spec = null, string sortBy = "", bool isDescending = false)
        {
            return _treeGroupRepository.GetsReadOnly(spec, Context.Filters.CreateSort<TreeGroup>(isDescending, sortBy));
        }

        /// <summary>
        /// Lấy tất cả danh sách nhóm cây theo điều kiện truyền vào. Kết quả chỉ để đọc
        /// </summary>
        /// <returns></returns>
        public int Count(Expression<Func<TreeGroup, bool>> spec = null)
        {
            return _treeGroupRepository.Count(spec);
        }

        /// <summary>
        /// Lấy tất cả danh sách nhóm cây. Kết quả sẽ được ánh xạ sang 1 dạng khác do người dùng cung cấp
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<TreeGroup, T>> projector, string sortBy = "", bool isDescending = false)
        {
            return _treeGroupRepository.GetsAs(projector, null, Context.Filters.CreateSort<TreeGroup>(isDescending, sortBy));
        }

        /// <summary>
        /// Lấy tất cả danh sách nhóm cây trong cache,.Nếu cach chưa có thì tự động lấy từ db lên và cache lại
        /// </summary>
        /// <param name="isActived">trạng thái cảu nhóm cây</param>
        /// <returns></returns>
        public IEnumerable<TreeGroup> GetCacheAllTreeGroups(bool? isActived = null)
        {
            var allTreeGroup = _cacheManager.Get(CacheParam.TreeGroupAllKey, CacheParam.TreeGroupAllCacheTimeOut, () => 
                {
                    var data = Gets(null, "Order", false);
                    return AutoMapper.Mapper.Map<IEnumerable<TreeGroup>, IEnumerable<TreeGroupCached>>(data);
                });

            var result = allTreeGroup.Where(u => !isActived.HasValue || u.IsActived == isActived.Value);

#if !HoSoMotCuaEdition

            result =  result.Where(u => !u.IsHsmc);
#endif
            return AutoMapper.Mapper.Map<IEnumerable<TreeGroupCached>, IEnumerable<TreeGroup>>(result);
        }

        /// <summary>
        /// Lấy nhóm theo id
        /// </summary>
        /// <param name="treeGroupId">Id của cơ quan ngoài</param>
        /// <returns></returns>
        public TreeGroup Get(int treeGroupId)
        {
            return _treeGroupRepository.Get(treeGroupId);
        }

        /// <summary>
        ///Xóa nhóm cây
        /// </summary>
        /// <param name="treeGroup">Đối tượng nhóm cây</param>
        public void Delete(TreeGroup treeGroup)
        {
            if (treeGroup == null)
            {
                throw new ArgumentNullException("treeGroup");
            }

            _treeGroupRepository.Delete(treeGroup);
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.TreeGroupAllKey);
        }

        /// <summary>
        ///Xóa nhóm cây
        /// </summary>
        /// <param name="treeGroups"> Danh sáchĐối tượng nhóm cây</param>
        public void Delete(IEnumerable<TreeGroup> treeGroups)
        {
            if (treeGroups == null || !treeGroups.Any())
            {
                throw new ArgumentNullException("treeGroups");
            }

            foreach (var treeGroup in treeGroups)
            {
                _treeGroupRepository.Delete(treeGroup);
            }

            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.TreeGroupAllKey);
        }

        /// <summary>
        /// Cập nhật nhóm cây
        /// </summary>
        /// <param name="treeGroup">Đối tượng nhóm cây</param>
        public void Update(TreeGroup treeGroup)
        {
            if (treeGroup == null)
            {
                throw new ArgumentNullException("treeGroup");
            }

            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.TreeGroupAllKey);
        }

        /// <summary>
        /// Cập nhật mức ưu tiên
        /// </summary>
        /// <param name="treeGroupIds">Danh sách id của nhóm cây</param>
        public void Update(int[] treeGroupIds)
        {
            if (treeGroupIds == null || !treeGroupIds.Any())
            {
                throw new ArgumentNullException("treeGroupIds");
            }

            var treeGroups = _treeGroupRepository.Gets(false);
            if (treeGroups != null && treeGroups.Any())
            {
                int order = 0;
                foreach (var treeGroupId in treeGroupIds)
                {
                    order++;
                    var treeGroup = treeGroups.Single(p => p.TreeGroupId == treeGroupId);
                    treeGroup.Order = order;
                }

                Context.SaveChanges();
                _cacheManager.Remove(CacheParam.TreeGroupAllKey);
            }
        }
    }
}
