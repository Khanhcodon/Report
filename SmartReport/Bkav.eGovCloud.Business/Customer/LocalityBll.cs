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
using Locality = Bkav.eGovCloud.Entities.Customer.Locality;

namespace Bkav.eGovCloud.Business.Customer
{
    public class LocalityBll : ServiceBase
	{
		#region Readonly & Static Fields

		private readonly IRepository<Locality> _LocalityRepository;
		private readonly AdminGeneralSettings _generalSettings;
		private readonly ResourceBll _resourceService;

		#endregion

		#region C'tors

		///<summary>
		///  Khởi tạo class <see cref="LocalityBll" />.
		///</summary>
		///<param name="context">Context</param>
		///<param name="generalSettings"> Cấu hình chung </param>
		///<param name="resourceService"> Bll liên quan đến bảng Resource trong CSDL</param>
		///<param name="cache"></param>
		public LocalityBll(
			IDbCustomerContext context,
			AdminGeneralSettings generalSettings,
			ResourceBll resourceService)
			: base(context)
		{
			_LocalityRepository = Context.GetRepository<Locality>();
			_generalSettings = generalSettings;
			_resourceService = resourceService;
		}

		#endregion

		#region Quản lý địa bàn

		/// <summary>
		/// Tạo mới địa bàn
		/// </summary>
		/// <param name="locality"> Entity địa bàn </param>
		public void Create(Locality locality)
		{
			if (locality == null)
			{
				throw new ArgumentNullException("locality");
			}
			if (_LocalityRepository.Exist(p=> p.LocalityName == locality.LocalityName))
			{
				throw new EgovException("Địa bàn đã tồn tại!");
			}

			_LocalityRepository.Create(locality);
			Context.SaveChanges();
		}

		/// <summary>
		/// Thêm mới địa bàn
		/// </summary>
		/// <param name="localities"></param>
		/// <param name="ignoreExist"></param>
		public void Create(IEnumerable<Locality> localities, bool ignoreExist = false)
		{
			if (localities == null || !localities.Any())
			{
				throw new ArgumentNullException("localities");
			}

			var names = localities.Select(x => x.LocalityName);
			var exist = _LocalityRepository.GetsAs(p => p.LocalityName, p => names.Contains(p.LocalityName));

			if (exist != null && exist.Any())
			{
				if (!ignoreExist || exist.Count() == localities.Count())
				{
					throw new EgovException("Địa bàn đã tồn tại!");
				}

				var list = localities.Where(p => !exist.Contains(p.LocalityName));
				if (list == null || !list.Any())
				{
					throw new EgovException("Địa bàn đã tồn tại!");
				}
				Create(list);
			}
			else
			{
				Create(localities);
			}
		}

		private void Create(IEnumerable<Locality> localities)
		{
			foreach (var locality in localities)
			{
				_LocalityRepository.Create(locality);
			}
			Context.SaveChanges();
		}

		/// <summary>
		/// Xóa 1 địa bàn
		/// </summary>
		/// <param name="locality"> Thực thể địa bàn </param>
		public void Delete(Locality locality)
		{
			if (locality == null)
			{
				throw new ArgumentNullException("locality");
			}
			// TODO: kiểm tra ràng buộc dữ liệu
			_LocalityRepository.Delete(locality);
			Context.SaveChanges();
		}

		/// <summary>
		/// Lấy ra địa bàn theo id
		/// </summary>
		/// <param name="localityId"> Id địa bàn </param>
		/// <returns> Entity địa bàn </returns>
		public Locality Get(Guid localityId)
		{
			Locality result = null;
			result = _LocalityRepository.Get(localityId);
			return result;
		}

		/// <summary> VienTV 100120
		/// Lấy ra tất cả các địa bàn có sắp xếp. Kết quả chỉ để đọc
		/// </summary>
		/// <param name="sortBy">Sắp xếp theo</param>
		/// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
		/// <returns>Danh sách các tài nguyên đã được phân trang</returns>
		public IEnumerable<Locality> Gets(string sortBy = "", bool isDescending = false)
		{
			return _LocalityRepository.GetsReadOnly(null, Context.Filters.CreateSort<Locality>(isDescending, sortBy));
		}

		/// <summary> VienTV 100120
		/// Lấy ra tất cả địa bàn theo điều kiện kỹ thuật truyền vào. Kết quả chỉ để đọc
		/// </summary>
		/// <param name="spec">The spec.</param>
		/// <returns></returns>
		public IEnumerable<Locality> Gets(Expression<Func<Locality, bool>> spec = null)
		{
			return _LocalityRepository.GetsReadOnly(spec);
		}

		/// <summary>
		/// Lấy ra tất cả các địa bàn. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
		/// </summary>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="spec">Điều kiện</param>
		/// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
		/// <returns>Danh sách các thực thể được ánh xạ</returns>
		public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Locality, TOutput>> projector, Expression<Func<Locality, bool>> spec = null)
		{
			return _LocalityRepository.GetsAs(projector, spec);
		}

		/// <summary>
		/// Cập nhật thông tin địa bàn
		/// </summary>
		/// <param name="locality"> Entity kỳ báo cáo </param>
		public void Update(Locality locality)
		{
			if (locality == null)
			{
				throw new ArgumentNullException("locality");
			}
			if (Get(locality.LocalityId) == null)
			{
				throw new Exception(string.Format("Địa bàn không tồn tại"));
            }
            else
            {
				Context.SaveChanges();
			}
		}

		#endregion
    }
}
