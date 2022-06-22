using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
	/// <summary>
	/// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
	/// <para>Project: eGov Cloud v1.0</para>
	/// <para>Class : AddressBll - public - BLL</para>
	/// <para>Access Modifiers:</para> 
	/// <para>Create Date : 15082013</para>
	/// <para>Author      : DungHV</para>
	/// <para>Description : BLL tương ứng với bảng Address trong CSDL</para>
	/// </summary>
	public class AddressBll : ServiceBase
	{
		private readonly IRepository<Address> _addressRepository;
		private readonly AdminGeneralSettings _generalSettings;
		private readonly MemoryCacheManager _cache;

		private const string CACHE_KEY = CacheParam.AddressCache;
		private const int CACHE_TIME = CacheParam.AddressCacheTimeOut;

		/// <summary>
		/// Khởi tạo
		/// </summary>
		/// <param name="context"></param>
		/// <param name="generalSettings"></param>
		/// <param name="cache"></param>
		public AddressBll(IDbCustomerContext context, AdminGeneralSettings generalSettings, MemoryCacheManager cache)
			: base(context)
		{
			_addressRepository = Context.GetRepository<Address>();
			_generalSettings = generalSettings;
			_cache = cache;

		}

		/// <summary>
		/// Tạo mới cơ quan ngoài
		/// </summary>
		/// <param name="address">đối tượng cơ quan ngoài</param>
		public void Create(Address address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}

			var existeDocId = GetByeDocId(address.EdocId);
			if (existeDocId != null)
			{
				return;
			}

			var checkName = _addressRepository.Exist(a => a.Name.Equals(address.Name, StringComparison.OrdinalIgnoreCase));
			if (checkName)
			{
				return;
			}

			if (address.IsMe)
			{
				var current = GetCurrent();
				if (current != null && !current.ParentId.HasValue)
				{
					current.IsMe = false;
				}
			}

			_addressRepository.Create(address);
			Context.SaveChanges();
			_cache.Remove(CACHE_KEY);
		}

		/// <summary>
		/// Lấy tất cả danh sách cơ quan ngoài. Kết quả chỉ để đọc
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Address> Gets(string sortBy = "", bool isDescending = false)
		{
			return _addressRepository.GetsReadOnly(null, Context.Filters.CreateSort<Address>(isDescending, sortBy));
		}

		/// <summary>
		/// Lấy tất cả danh sách cơ quan theo danh sách id
		/// </summary>
		/// <param name="addressIds"></param>
		/// <param name="isReadonly"></param>
		/// <returns></returns>
		public IEnumerable<Address> Gets(List<int> addressIds, bool isReadonly = true)
		{
			if (isReadonly)
			{
				return GetsFromCache().Where(d => addressIds.Contains(d.AddressId));
			}

			return _addressRepository.Gets(isReadonly, d => addressIds.Contains(d.AddressId));
		}

		/// <summary>
		/// Trả về danh sách địa chỉ từ cache
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Address> GetsFromCache()
		{
			return _cache.Get<IEnumerable<Address>>(CACHE_KEY, () =>
			{
				return Gets();
			}, CACHE_TIME);
		}

		/// <summary>
		/// Lấy ra danh sách các đợn vị cơ sở của cơ quan ngoài
		/// </summary>
		/// <param name="addressId"></param>
		/// <param name="aboutMe"> Thiết lập có hay không lấy cả cơ quan đó</param>
		/// <param name="isReadonly"></param>
		/// <returns></returns>
		public IEnumerable<Address> GetAddresses(int addressId, bool aboutMe = false, bool isReadonly = true)
		{
			if (isReadonly)
			{
				return GetsFromCache().Where(a => (a.ParentId.HasValue && a.ParentId.Value == addressId) || (aboutMe && a.AddressId == addressId));
			}

			return _addressRepository.Gets(isReadonly, a => (a.ParentId.HasValue && a.ParentId.Value == addressId) || (aboutMe && a.AddressId == addressId));
		}

		/// <summary>
		/// Lấy tất cả danh sách cơ quan ngoài. Kết quả sẽ được ánh xạ sang 1 dạng khác do người dùng cung cấp
		/// </summary>
		/// <returns></returns>
		public IEnumerable<T> GetsAs<T>(Expression<Func<Address, T>> projector, string sortBy = "", bool isDescending = false)
		{
			return _addressRepository.GetsAs(projector, null, Context.Filters.CreateSort<Address>(isDescending, sortBy));
		}

		/// <summary>
		/// Lấy tất cả danh sách cơ quan ngoài. Kết quả sẽ được ánh xạ sang 1 dạng khác do người dùng cung cấp
		/// </summary>
		/// <param name="totalRecords"></param>
		/// <param name="spec"></param>
		/// <param name="currentPage"></param>
		/// <param name="pageSize"></param>
		/// <param name="sortBy"></param>
		/// <param name="isDescending"></param>
		/// <returns></returns>
		public IEnumerable<Address> Gets(out int totalRecords,
				Expression<Func<Address, bool>> spec = null, int currentPage = 1,
				int? pageSize = null, string sortBy = "",
				bool isDescending = true)
		{
			if (!pageSize.HasValue)
			{
				pageSize = _generalSettings.DefaultPageSize;
			}

			totalRecords = _addressRepository.Count(spec);
			var sort = Context.Filters.CreateSort<Address>(isDescending, sortBy);

			return _addressRepository.GetsReadOnly(spec, sort, Context.Filters.Page<Address>(currentPage, pageSize.Value));
		}

		/// <summary>
		/// Lấy cơ quan ngoài theo id
		/// </summary>
		/// <param name="addressId">Id của cơ quan ngoài</param>
		/// <returns></returns>
		public Address Get(int addressId)
		{
			return _addressRepository.Get(addressId);
		}

		/// <summary>
		/// xóa cơ quan ngoài
		/// </summary>
		/// <param name="address">đối tượng cơ quan ngoài</param>
		public void Delete(Address address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			_addressRepository.Delete(address);
			Context.SaveChanges();
			_cache.Remove(CACHE_KEY);
		}

		/// <summary>
		/// cập nhật cơ quan nhóm cho các cơ quan
		/// </summary>
		/// <param name="addresses">Các cơ quan</param>
		/// <param name="groupName">Tên nhóm</param>
		/// <param name="isOverride"></param>
		public void UpdateGroupName(IEnumerable<Address> addresses, string groupName, bool isOverride = false)
		{
			if (!addresses.Any())
			{
				throw new ArgumentNullException("address");
			}

			foreach (var address in addresses)
			{
				if (!isOverride && !string.IsNullOrEmpty(address.GroupName) && !string.IsNullOrEmpty(groupName))
				{
					var groupNameOlds = ParseGroupName(address.GroupName);
					groupNameOlds.AddRange(ParseGroupName(groupName));
					groupName = GroupNameCompareString(groupNameOlds.Distinct().ToList());
				}

				// Xử lý nhóm của con
				var addressChildren = GetAddresses(address.AddressId, false, false);
				foreach (var childAddress in addressChildren)
				{
					childAddress.GroupName = groupName;
				}
				address.GroupName = groupName;
			}

			Context.SaveChanges();
			_cache.Remove(CACHE_KEY);
		}

		/// <summary>
		/// cập nhật cơ quan ngoài
		/// </summary>
		/// <param name="address">đối tượng cơ quan ngoài</param>
		public void Update(Address address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}

			var exist = GetByeDocId(address.EdocId);
			if (exist == null || (exist != null && exist.AddressId != address.AddressId))
			{
				if (address.IsMe)
				{
					var current = GetCurrent();
					if (current != null && current.AddressId != address.AddressId)
					{
						current.IsMe = false;
					}
				}
			}
			Context.SaveChanges();
			_cache.Remove(CACHE_KEY);
		}

		/// <summary>
		/// Trả về cơ quan theo mã định danh
		/// </summary>
		/// <param name="code">Mã định danh</param>
		/// <returns></returns>
		public Address GetByeDocId(string code)
		{
			if (string.IsNullOrEmpty(code))
			{
				return null;
			}

			return _addressRepository.Get(false, a => a.EdocId.Equals(code, StringComparison.OrdinalIgnoreCase));
		}

		/// <summary>
		/// Trả về cơ quan hiện tại
		/// </summary>
		/// <returns></returns>
		public Address GetCurrent()
		{
			return _addressRepository.Get(false, a => a.IsMe == true && !a.ParentId.HasValue);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="groupName"></param>
		/// <returns></returns>
		public string GroupNameCompareString(List<string> groupName)
		{
			return string.Join(";", groupName);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="groupName"></param>
		/// <returns></returns>
		public List<string> ParseGroupName(string groupName)
		{
			var result = new List<string>();
			if (string.IsNullOrEmpty(groupName))
			{
				return result;
			}

			result = groupName.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();

			return result;
		}
	}
}
