using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ActionLevel = Bkav.eGovCloud.Entities.Customer.ActionLevel;

namespace Bkav.eGovCloud.Business.Customer
{
    public class ActionLevelBll : ServiceBase
	{
		#region Readonly & Static Fields

		private readonly IRepository<ActionLevel> _actionLevelRepository;
		private readonly AdminGeneralSettings _generalSettings;
		private readonly ResourceBll _resourceService;
		private readonly MemoryCacheManager _cache;

		const string CACHE_KEY = CacheParam.ActionLevelKey;
		const int CACHE_TIMEOUT = CacheParam.ActionLevelCacheTimeOut;

		#endregion

		#region C'tors

		///<summary>
		///  Khởi tạo class <see cref="ActionLevelBll" />.
		///</summary>
		///<param name="context">Context</param>
		///<param name="generalSettings"> Cấu hình chung </param>
		///<param name="resourceService"> Bll liên quan đến bảng Resource trong CSDL</param>
		///<param name="cache"></param>
		public ActionLevelBll(
			IDbCustomerContext context,
			AdminGeneralSettings generalSettings,
			ResourceBll resourceService,
			MemoryCacheManager cache)
			: base(context)
		{
			_actionLevelRepository = Context.GetRepository<ActionLevel>();
			_generalSettings = generalSettings;
			_resourceService = resourceService;
			_cache = cache;
		}

		#endregion

		#region Quản lý kỳ báo cáo

		/// <summary>
		/// Tạo mới kỳ báo cáo
		/// </summary>
		/// <param name="actionLevel"> Entity kỳ báo cáo </param>
		public void Create(ActionLevel actionLevel)
		{
			if (actionLevel == null)
			{
				throw new ArgumentNullException("actionLevel");
			}
			if (_actionLevelRepository.Exist(ActionLevelQuery.WithName(actionLevel.ActionLevelName)))
			{
				throw new EgovException(string.Format("Tên kỳ báo cáo ({0}) đã tồn tại!", actionLevel.ActionLevelName));
			}

			_actionLevelRepository.Create(actionLevel);
			Context.SaveChanges();

			_cache.Remove(CACHE_KEY);
		}

		/// <summary>
		/// Thêm mới kỳ báo cáo
		/// </summary>
		/// <param name="actionLevels"></param>
		/// <param name="ignoreExist"></param>
		public void Create(IEnumerable<ActionLevel> actionLevels, bool ignoreExist)
		{
			if (actionLevels == null || !actionLevels.Any())
			{
				throw new ArgumentNullException("actionLevels");
			}

			var names = actionLevels.Select(x => x.ActionLevelName);
			var exist = _actionLevelRepository.GetsAs(p => p.ActionLevelName, p => names.Contains(p.ActionLevelName));

			if (exist != null && exist.Any())
			{
				if (!ignoreExist || exist.Count() == actionLevels.Count())
				{
					throw new EgovException(_resourceService.GetResource("ActionLevel.Create.Exist"));
				}

				var list = actionLevels.Where(p => !exist.Contains(p.ActionLevelName));
				if (list == null || !list.Any())
				{
					throw new EgovException(_resourceService.GetResource("ActionLevel.Create.Exist"));
				}
				Create(list);
			}
			else
			{
				Create(actionLevels);
			}
		}

		private void Create(IEnumerable<ActionLevel> actionLevels)
		{
			Context.Configuration.AutoDetectChangesEnabled = false;
			foreach (var actionLevel in actionLevels)
			{
				_actionLevelRepository.Create(actionLevel);
			}
			Context.Configuration.AutoDetectChangesEnabled = true;
			Context.SaveChanges();
			_cache.Remove(CACHE_KEY);
		}

		/// <summary>
		/// Xóa 1 kỳ báo cáo
		/// </summary>
		/// <param name="actionLevel"> Thực thể kỳ báo cáo </param>
		public void Delete(ActionLevel actionLevel)
		{
			if (actionLevel == null)
			{
				throw new ArgumentNullException("actionLevel");
			}
			// TODO: kiểm tra ràng buộc dữ liệu

			_actionLevelRepository.Delete(actionLevel);
			Context.SaveChanges();
			_cache.Remove(CACHE_KEY);
		}

		/// <summary>
		/// Lấy ra kỳ báo cáo theo id
		/// </summary>
		/// <param name="actionLevelId"> Id kỳ báo cáo </param>
		/// <returns> Entity kỳ báo cáo </returns>
		public ActionLevel Get(int actionLevelId)
		{
			ActionLevel result = null;
			if (actionLevelId > 0)
			{
				result = _actionLevelRepository.Get(actionLevelId);
			}
			return result;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionLevelId"></param>
        /// <returns></returns>
        public ActionLevel GetIdAc(int? actionLevelId)
        {
            ActionLevel result = null;
            if (actionLevelId > 0)
            {
                result = _actionLevelRepository.Get(actionLevelId);
            }
            return result;
        }
		/// <summary>
		/// Lấy ra kỳ báo cáo theo mã
		/// </summary>
		/// <param name="actionLevelCode"> Mã kỳ báo cáo </param>
		/// <returns> Entity kỳ báo cáo </returns>
		public ActionLevel GetByCode(string actionLevelCode)
		{
			ActionLevel result = null;
			if (!string.IsNullOrWhiteSpace(actionLevelCode))
			{
				result = _actionLevelRepository.Get(true, a => a.ActionLevelCode == actionLevelCode);
			}
			return result;
		}

		/// <summary>
		/// Trả về kỳ báo cáo theo id, kết quả được lấy từ cache
		/// </summary>
		/// <param name="actionLevelId"></param>
		/// <returns></returns>
		public ActionLevel GetFromCache(int actionLevelId)
		{
			return GetsFromCache().SingleOrDefault(c => c.ActionLevelId == actionLevelId);
		}

		/// <summary>
		/// Trả về danh sách kỳ báo cáo có cache lại kết quả
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ActionLevel> GetsFromCache()
		{
			return _cache.Get<IEnumerable<ActionLevel>>(CACHE_KEY, () =>
			{
				return Gets();
			}, CACHE_TIMEOUT);
		}

		/// <summary>
		/// Lấy các kỳ báo cáo theo điều kiện kỹ thuật truyền vào. Kết quả chỉ đọc
		/// </summary>
		/// <param name="spec"> </param>
		/// <returns> </returns>
		public IEnumerable<ActionLevel> Gets(Expression<Func<ActionLevel, bool>> spec = null)
		{
			return _actionLevelRepository.GetsReadOnly(spec);
		}

		/// <summary>
		///   Lấy ra danh sách kỳ báo cáo
		/// </summary>
		/// <param name="totalRecords"> Tổng số bản ghi </param>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="currentPage"> Trang hiện tại </param>
		/// <param name="pageSize"> Số bản ghi trên 1 trang </param>
		/// <param name="sortBy"> Sắp xếp theo </param>
		/// <param name="isDescending"> Sắp xếp từ lớn đến nhỏ: true, ngược lại false </param>
		/// <param name="actionLevelName"> Tên sổ văn bản, hồ sơ </param>
		/// <returns> Danh sách kỳ báo cáo </returns>
		public IEnumerable<T> GetsAs<T>(out int totalRecords, Expression<Func<ActionLevel, T>> projector,
			int currentPage = 1,
			int? pageSize = null, string sortBy = "StoreName", bool isDescending = false,
			 string actionLevelName = "")
		{
			var spec = !string.IsNullOrWhiteSpace(actionLevelName)
						   ? ActionLevelQuery.ContainsName(actionLevelName)
						   : null;

			if (!pageSize.HasValue)
			{
				pageSize = _generalSettings.DefaultPageSize;
			}
			totalRecords = _actionLevelRepository.Count(spec);
			var sort = Context.Filters.CreateSort<ActionLevel>(isDescending, sortBy);
			return _actionLevelRepository.GetsAs(projector, spec, sort, Context.Filters.Page<ActionLevel>(currentPage, pageSize.Value));
		}

		/// <summary>
		/// Lấy ra tất cả các kỳ báo cáo phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
		/// </summary>
		/// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="spec">Điều kiện</param>
		/// <returns>Danh sách các thực thể được ánh xạ</returns>
		public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<ActionLevel, TOutput>> projector,
													Expression<Func<ActionLevel, bool>> spec = null)
		{
			return _actionLevelRepository.GetsAs(projector, spec);
		}

		/// <summary>
		/// Cập nhật thông tin kỳ báo cáo
		/// </summary>
		/// <param name="actionLevel"> Entity kỳ báo cáo </param>
		/// <param name="oldActionLevelName"> Tên kỳ báo cáo trước khi cập nhật </param>
		public void Update(ActionLevel actionLevel, string oldActionLevelName)
		{
			if (actionLevel == null)
			{
				throw new ArgumentNullException("actionLevel");
			}
			if (_actionLevelRepository.Exist(ActionLevelQuery.WithName(actionLevel.ActionLevelName).And(r => r.ActionLevelName.ToLower() != oldActionLevelName.ToLower())))
			{
				throw new Exception(string.Format("Tên kỳ báo cáo ({0}) đã tồn tại!", actionLevel.ActionLevelName));
			}
			Context.SaveChanges();
			_cache.Remove(CACHE_KEY);
		}

		#endregion
    }
}
