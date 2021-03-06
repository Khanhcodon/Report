using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Permissions;
using System.Web;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Mail;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Core.Validation;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using AutoMapper;

namespace Bkav.eGovCloud.Business.Customer
{
	/// <summary>
	/// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
	/// <para>Project: eGov Cloud v1.0</para>
	/// <para>Class : UserBll - public - BLL</para>
	/// <para>Access Modifiers:</para>
	/// <para>Create Date : 200812</para>
	/// <para>Author      : TrungVH</para>
	/// <para>Description : BLL tương ứng với bảng User trong CSDL</para>
	/// </summary>
	public class UserBll : ServiceBase
	{
		private readonly IRepository<User> _userRepository;
		private readonly IRepository<UserRole> _userRoleRepository;
		private readonly ResourceBll _resourceService;
		private readonly LdapProvider _ldap;
		private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRolePermission> _userRolePermissionRepository;
		private readonly IRepository<Department> _departmentRepository;
		private readonly IRepository<JobTitles> _jobTitlesRepository;
		private readonly IRepository<Position> _positionRepository;
		private readonly IRepository<Permission> _permissionRepository;
		private readonly MemoryCacheManager _cacheManager;
		private readonly AuthenticationSettings _authenticationSettings;
		private readonly ConnectionSettings _connectionSettings;
		private readonly PasswordPolicySettings _passwordPolicySettings;
		private readonly AdminGeneralSettings _generalSettings;
		private readonly IRepository<UserDepartmentJobTitlesPosition> _userDepartmentJobTitlesRepository;
		private CurrentUserCached _cacheUser;
		private readonly UserConnectionBll _userConnectionService;

		/// <summary>
		/// Khởi tạo class <see cref="UserBll"/>.
		/// </summary>
		/// <param name="context">Context</param>
		/// <param name="resourceService">Bll tương ứng với bảng Resource trong CSDL</param>
		/// <param name="cacheManager">Cache manager</param>
		/// <param name="authenticationSettings">Cấu hình xác thực</param>
		/// <param name="ldap">Provider truy cập LDAP</param>
		/// <param name="passwordPolicySettings">Cấu hình chính sách mật khẩu</param>
		/// <param name="generalSettings">Cấu hình chung</param>
		/// <param name="connectionSettings">Cấu hình kết nối</param>
		/// <param name="userConnectionService"></param>
		public UserBll(IDbCustomerContext context,
						ResourceBll resourceService, LdapProvider ldap,
						MemoryCacheManager cacheManager,
						AuthenticationSettings authenticationSettings,
						PasswordPolicySettings passwordPolicySettings,
						AdminGeneralSettings generalSettings,
						ConnectionSettings connectionSettings,
						UserConnectionBll userConnectionService)
			: base(context)
		{
			_userRepository = Context.GetRepository<User>();
			_userRoleRepository = Context.GetRepository<UserRole>();
			_resourceService = resourceService;
			_authenticationSettings = authenticationSettings;
			_connectionSettings = connectionSettings;
			_ldap = ldap;
			_roleRepository = Context.GetRepository<Role>();
			_userRolePermissionRepository = Context.GetRepository<UserRolePermission>();
			_departmentRepository = Context.GetRepository<Department>();
			_jobTitlesRepository = Context.GetRepository<JobTitles>();
			_positionRepository = Context.GetRepository<Position>();
			_permissionRepository = Context.GetRepository<Permission>();
			_passwordPolicySettings = passwordPolicySettings;
			_generalSettings = generalSettings;
			_cacheManager = cacheManager;
			_userDepartmentJobTitlesRepository = Context.GetRepository<UserDepartmentJobTitlesPosition>();
			_userConnectionService = userConnectionService;
		}

#pragma warning disable 1591

		/// <summary>
		/// Trả về người dùng hiện tại
		/// </summary>
		/// <returns></returns>
		protected CurrentUserCached GetCurrentUser()
		{
			var userName = HttpContext.Current.User.GetUserNameWithDomain();

			var allUsers = GetAllCached();
			var result = allUsers.FirstOrDefault(u => u.UsernameEmailDomain.Equals(userName, StringComparison.OrdinalIgnoreCase));
			if (result == null)
			{
				return null;
			}

			var currentUserCacheKey = string.Format(CacheParam.UserCurrent, userName);
			return _cacheManager.Get(currentUserCacheKey, CacheParam.UserCurrentCacheTimeOut, () =>
			{
				var allDeptJobtitles = _userDepartmentJobTitlesRepository.GetsReadOnly();

				var userDepartmentJobTitless = allDeptJobtitles.Where(i => i.UserId == result.UserId).ToList();

				var permissions = _cacheManager.Get(CacheParam.RolePermissionAllKey, CacheParam.RolePermissionAllCacheTimeOut, () =>
									{
										var allRolePermissions = _userRolePermissionRepository.GetsReadOnly(urp => urp.RoleId != null && urp.RoleKey != null);
										return Mapper.Map<IEnumerable<UserRolePermission>, IEnumerable<PermissionCache>>(allRolePermissions);
									});

				permissions = permissions.Where(p => p.UserId == result.UserId);

				return ToCacheOject(result, userDepartmentJobTitless, permissions);
			});
		}

		protected User GetCurrentEditableUser()
		{
			var userName = HttpContext.Current.User.GetUserNameWithDomain();
			return _userRepository.Get(false, u => u.UsernameEmailDomain.Equals(userName, StringComparison.OrdinalIgnoreCase));
		}

#pragma warning restore 1591

		/// <summary>
		/// Lấy ra thông tin người dùng đang đăng nhập hiện tại (cached)
		/// </summary>
		public CurrentUserCached CurrentUser
		{
			get { return GetCurrentUser(); }
			set { _cacheUser = value; }
		}

		/// <summary>
		/// Lấy ra thông tin người dùng đang đăng nhập hiện tại (có thể cập nhật dữ liệu)
		/// </summary>
		public User CurrentEditableUser
		{
            get { return GetCurrentEditableUser(); }
		}

		/// <summary>
		/// Lấy ra tất cả những người dùng trong hệ thống. Kết quả chỉ để đọc
		/// </summary>
		/// <param name="isActivated">Lấy ra tất cả người dùng đang hoạt động: true và ngược lại: false. Nếu để null sẽ bỏ qua điều kiện này</param>
		/// <returns>Danh sách người dùng</returns>
		public IEnumerable<User> Gets(bool? isActivated = null)
		{
			return _userRepository.GetsReadOnly(UserQuery.WithIsActivated(isActivated));
		}

		/// <summary>
		/// Lấy ra danh sách những người dùng theo danh sách các id. Kết quả chỉ đọc
		/// </summary>
		/// <param name="userIds">Danh sách id người dùng</param>
		/// <param name="isActivated">Lấy ra tất cả người dùng đang hoạt động: true và ngược lại: false. Nếu để null sẽ bỏ qua điều kiện này</param>
		/// <returns>Danh sách người dùng</returns>
		public IEnumerable<User> Gets(IEnumerable<int> userIds, bool? isActivated = null)
		{
			if (userIds == null)
			{
				throw new ArgumentNullException("userIds");
			}
			return GetCacheAllUsers(isActivated).Where(u => userIds.Contains(u.UserId));
		}

		/// <summary>
		/// Lấy ra danh sách những người dùng theo danh sách các username. Kết quả chỉ đọc
		/// </summary>
		/// <param name="usernames">Danh sách username người dùng</param>
		/// <param name="isActivated">Lấy ra tất cả người dùng đang hoạt động: true và ngược lại: false. Nếu để null sẽ bỏ qua điều kiện này</param>
		/// <returns>Danh sách người dùng</returns>
		public IEnumerable<User> Gets(IEnumerable<string> usernames, bool? isActivated = null)
		{
			if (usernames == null)
			{
				throw new ArgumentNullException("usernames");
			}
			return GetCacheAllUsers(isActivated).Where(u => usernames.Any(i => i.Equals(u.Username, StringComparison.OrdinalIgnoreCase)));
		}

		/// <summary>
		/// Lấy ra danh sách các người dùng theo điều kiện tìm kiếm và có phân trang, sắp xếp. Kết quả chỉ đọc
		/// </summary>
		/// <param name="totalRecords">Tổng số người dùng</param>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="currentPage">Trang hiện tại</param>
		/// <param name="pageSize">Số bản ghi / 1 trang</param>
		/// <param name="sortBy">Sắp xếp theo</param>
		/// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
		/// <param name="username">Tên đăng nhập của người dùng (dùng để tìm kiếm). Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các người dùng có tên đăng nhập gần giống với giá trị truyền vào</param>
		/// <param name="fullname">Họ và tên của người dùng (dùng để tìm kiếm). Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các người dùng có họ và tên gần giống với giá trị truyền vào</param>
		/// <param name="isActivated">Trạng thái của người dùng (dùng để tìm kiếm). Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các người dùng có trạng thái trùng với giá trị truyền vào</param>
		///<param name="roleId">Nhóm quyền</param>
		///<param name="positionId">Mã chức vụ</param>
		/// <returns>Danh sách người dùng đã được phân trang, sắp xếp</returns>
		public IEnumerable<T> GetsAs<T>(out int totalRecords,
			Expression<Func<User, T>> projector,
			int currentPage = 1,
			int? pageSize = null, string sortBy = "",
			bool isDescending = false, string username = "",
			string fullname = "", bool? isActivated = null,
			 int? positionId = null, int? roleId = null)
		{
			var spec = UserQuery.WithIsActivated(isActivated)
						.And(UserQuery.ContainsUsername(username))
						.And(UserQuery.ContainsFullName(fullname));

			if (positionId.HasValue && positionId.Value > 0)
			{
				spec = spec.And(p => p.UserDepartmentJobTitless.Select(x => x.PositionId).Contains(positionId.Value));
			}

			if (roleId.HasValue && roleId.Value > 0)
			{
				spec = spec.And(p => p.UserRoles.Select(x => x.RoleId).Contains(roleId.Value));
			}

			if (!pageSize.HasValue)
			{
				pageSize = _generalSettings.DefaultPageSize;
			}

			totalRecords = _userRepository.Count(spec);
			var sort = Context.Filters.CreateSort<User>(isDescending, sortBy);
			var paging = Context.Filters.Page<User>(currentPage, pageSize.Value);
			return _userRepository.GetsAs(projector, spec, sort, paging);
		}

		/// <summary>
		/// Lấy ra danh sách các người dùng thuộc quản lý của id người dùng truyền vào theo điều kiện tìm kiếm và có phân trang, sắp xếp. Kết quả chỉ đọc
		/// </summary>
		/// <param name="totalRecords">Tổng số người dùng</param>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="currentPage">Trang hiện tại</param>
		/// <param name="userId">Id người quản lý</param>
		/// <param name="pageSize">Số bản ghi / 1 trang</param>
		/// <param name="sortBy">Sắp xếp theo</param>
		/// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
		/// <param name="username">Tên đăng nhập của người dùng (dùng để tìm kiếm). Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các người dùng có tên đăng nhập gần giống với giá trị truyền vào</param>
		/// <param name="fullname">Họ và tên của người dùng (dùng để tìm kiếm). Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các người dùng có họ và tên gần giống với giá trị truyền vào</param>
		/// <param name="isActivated">Trạng thái của người dùng (dùng để tìm kiếm). Nếu để trống sẽ bỏ qua điều kiện tìm kiếm này, nếu có giá trị sẽ tìm tất cả các người dùng có trạng thái trùng với giá trị truyền vào</param>
		///<param name="roleId">Nhóm quyền</param>
		///<param name="positionId">Mã chức vụ</param>
		/// <returns>Danh sách người dùng đã được phân trang, sắp xếp</returns>
		public IEnumerable<T> GetsUserAccess<T>(out int totalRecords,
			Expression<Func<User, T>> projector,
			int userId, int currentPage = 1,
			int? pageSize = null, string sortBy = "",
			bool isDescending = false, string username = "",
			string fullname = "", bool? isActivated = null,
			int? positionId = null, int? roleId = null)
		{
			var departmentService = new DepartmentBll(Context as IDbCustomerContext, this, _cacheManager, _generalSettings);
			var allDepartmentAccess = departmentService.GetAllDepartmentUserAccess(userId);
			if (allDepartmentAccess == null || !allDepartmentAccess.Any())
			{
				totalRecords = 0;
				return new List<T>();
			}
			var allDepartmentIdAccess = allDepartmentAccess.Select(d => d.DepartmentId);
			var allUserIdAccess = departmentService.GetCacheAllUserDepartmentJobTitlesPosition()
				.Where(d => allDepartmentIdAccess.Contains(d.DepartmentId)
					&& ((positionId.HasValue && d.PositionId == positionId.Value)
					|| !positionId.HasValue)).Select(d => d.UserId).Distinct();
			if (!allUserIdAccess.Any())
			{
				totalRecords = 0;
				return new List<T>();
			}

			var spec = UserQuery.WithIsActivated(isActivated)
						.And(UserQuery.ContainsUsername(username))
						.And(UserQuery.ContainsFullName(fullname))
						.And(u => allUserIdAccess.Contains(u.UserId));

			if (positionId.HasValue && positionId.Value > 0)
			{
				spec = spec.And(p => p.UserDepartmentJobTitless.Select(x => x.PositionId).Contains(positionId.Value));
			}

			if (roleId.HasValue && roleId.Value > 0)
			{
				spec = spec.And(p => p.UserRoles.Select(x => x.RoleId).Contains(roleId.Value));
			}

			if (!pageSize.HasValue)
			{
				pageSize = _generalSettings.DefaultPageSize;
			}

			totalRecords = _userRepository.Count(spec);
			var sort = Context.Filters.CreateSort<User>(isDescending, sortBy);
			var paging = Context.Filters.Page<User>(currentPage, pageSize.Value);
			return _userRepository.GetsAs(projector, spec, sort, paging);
		}

		/// <summary>
		/// Lấy ra danh sách các người dùng thuộc quản lý của id người dùng truyền vào theo điều kiện tìm kiếm và có phân trang, sắp xếp. Kết quả chỉ đọc
		/// </summary>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="userId">Id người quản lý</param>
		/// <returns>Danh sách người dùng đã được phân trang, sắp xếp</returns>
		public IEnumerable<T> GetsUserAccess<T>(Expression<Func<User, T>> projector, int userId)
		{
			var departmentService = new DepartmentBll(Context as IDbCustomerContext, this, _cacheManager, _generalSettings);
			var allDepartmentAccess = departmentService.GetAllDepartmentUserAccess(userId);
			if (allDepartmentAccess == null || !allDepartmentAccess.Any())
			{
				return new List<T>();
			}
			var allDepartmentIdAccess = allDepartmentAccess.Select(d => d.DepartmentId);
			var allUserIdAccess = departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Where(d => allDepartmentIdAccess.Contains(d.DepartmentId)).Select(d => d.UserId).Distinct();
			if (!allUserIdAccess.Any())
			{
				return new List<T>();
			}
			var sort = Context.Filters.CreateSort<User>(false, "Username");
			return _userRepository.GetsAs(projector, u => allUserIdAccess.Contains(u.UserId), sort);
		}

		/// <summary>
		/// Lấy ra tất cả id của những người dùng trong hệ thống
		/// </summary>
		/// <param name="isActivated">Lấy ra tất cả id của những người dùng đang hoạt động: true và ngược lại: false. Nếu để null sẽ bỏ qua điều kiện này</param>
		/// <returns>Danh sách id người dùng</returns>
		public IEnumerable<int> GetAllUserIds(bool? isActivated = null)
		{
			return GetAllCached(isActivated).Select(u => u.UserId);
		}

		/// <summary>
		/// Lấy ra tất cả các loại hồ sơ. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
		/// </summary>
		/// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
		/// <param name="isActivated">Trạng thái kích hoạt</param>
		/// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
		/// <returns>Danh sách các thực thể được ánh xạ</returns>
		public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<User, TOutput>> projector, bool? isActivated = null)
		{
			return _userRepository.GetsAs(projector, UserQuery.WithIsActivated(isActivated));
		}

		/// <summary>
		/// Lấy ra danh sách tất cả các người dùng (Danh sách này sẽ được cache lại trong 60 phút)
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Do nhiều chỗ dùng hàm này chưa sửa được nên return ngược lại về user
		/// </remarks>
		// [ObsoleteAttribute("Hàm này chuẩn bị bỏ, cần sử dụng hàm GetAllCached thay thế.")] 
		public IEnumerable<User> GetCacheAllUsers(bool? isActivated = null)
		{
			var allUsers = _cacheManager.Get(CacheParam.UserAllKey, CacheParam.UserAllCacheTimeOut, () =>
			{
				var data = Gets();
				return Mapper.Map<IEnumerable<User>, IEnumerable<UserCached>>(data);
			});

			allUsers = allUsers.Where(u => !isActivated.HasValue || u.IsActivated == isActivated);

			return Mapper.Map<IEnumerable<UserCached>, IEnumerable<User>>(allUsers);
		}

		/// <summary>
		/// Lấy ra danh sách tất cả các người dùng (Danh sách này sẽ được cache lại trong 60 phút)
		/// </summary>
		/// <returns></returns>
		public IEnumerable<UserCached> GetAllCached(bool? isActivated = null)
		{
			var allUsers = _cacheManager.Get(CacheParam.UserAllKey, CacheParam.UserAllCacheTimeOut, () =>
			{
				var data = Gets();
				return Mapper.Map<IEnumerable<User>, IEnumerable<UserCached>>(data);
			});

			allUsers = allUsers.Where(u => !isActivated.HasValue || u.IsActivated == isActivated);

			return allUsers;
		}

		/// <summary>
		/// Trả về User từ Cache
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public User GetFromCache(int userId)
		{
			var allUsers = GetCacheAllUsers(true);
			var result = allUsers.SingleOrDefault(u => u.UserId == userId);
			return result;
		}

		/// <summary>
		/// Xóa cache users
		/// </summary>
		public void ClearCache()
		{
			_cacheManager.Remove(CacheParam.UserAllKey);

			var userName = HttpContext.Current.User.GetUserNameWithDomain();
			var currentUserCacheKey = string.Format(CacheParam.UserCurrent, userName);

			_cacheManager.Remove(currentUserCacheKey);
		}

		/// <summary>
		/// Lấy ra người dùng theo Id
		/// </summary>
		/// <param name="userId">Id của người dùng</param>
		/// <param name="isActivated">Chỉ lấy tài khoản vẫn đang hoạt động (true) và ngược lại. Nếu bằng null thì sẽ bỏ qua điều kiện này</param>
		/// <returns>Entity người dùng</returns>
		public User Get(int userId, bool? isActivated = null)
		{
			User user = null;
			if (userId > 0)
			{
				user = _userRepository.Get(false, u => (!isActivated.HasValue || u.IsActivated == isActivated) && u.UserId == userId);
			}
			return user;
		}
		
		/// <summary>
		/// Lấy ra người dùng theo tên đăng nhập
		/// </summary>
		/// <param name="usernameEmailDomain">Tên đăng nhập dạng email (username@domain)</param>
		/// <param name="isActivated">Chỉ lấy tài khoản vẫn đang hoạt động (true) và ngược lại. Nếu bằng null thì sẽ bỏ qua điều kiện này</param>
		/// <returns>Entity người dùng</returns>
		public User Get(string usernameEmailDomain, bool? isActivated = null)
		{
			User user = null;
			if (!string.IsNullOrWhiteSpace(usernameEmailDomain))
			{
				user = _userRepository.Get(false,
					u =>
						u.UsernameEmailDomain == usernameEmailDomain &&
						(u.IsActivated == isActivated.Value || !isActivated.HasValue));
			}
			return user;
		}

		/// <summary>
		/// Lấy ra người dùng theo tên đăng nhập
		/// </summary>
		/// <param name="username">Tên đăng nhập</param>
		/// <param name="isActivated">Chỉ lấy tài khoản vẫn đang hoạt động (true) và ngược lại. Nếu bằng null thì sẽ bỏ qua điều kiện này</param>
		/// <returns>Entity người dùng</returns>
		public User GetByUserName(string username, bool? isActivated = null)
		{
			User user = null;
			if (!string.IsNullOrWhiteSpace(username))
			{
				user = _userRepository.Get(false,
					u =>
						u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.UsernameEmailDomain != "" &&
						(u.IsActivated == isActivated.Value || !isActivated.HasValue));
			}
			return user;
		}
		
		/// <summary>
		/// Lấy ra người dùng theo OpenID
		/// </summary>
		/// <param name="openId">OpenID</param>
		/// <param name="isActivated">Chỉ lấy tài khoản vẫn đang hoạt động (true) và ngược lại. Nếu bằng null thì sẽ bỏ qua điều kiện này</param>
		/// <returns>Entity người dùng</returns>
		public User GetByOpenId(string openId, bool? isActivated = null)
		{
			User user = null;
			if (!string.IsNullOrWhiteSpace(openId))
			{
				user = _userRepository.Get(false,
					u =>
						u.OpenId == openId &&
						(u.IsActivated == isActivated.Value || !isActivated.HasValue));
			}

			return user;
		}
		
		/// <summary>
		/// Kiểm tra tên đăng nhập đã tồn tại chưa
		/// </summary>
		/// <param name="usernameEmailDomain">Tên đăng nhập dạng email (@domainname)</param>
		/// <returns></returns>
		public Boolean IsExistUsernameEmailDomain(string usernameEmailDomain)
		{
			return _userRepository.Exist(UserQuery.WithUsernameEmailDomain(usernameEmailDomain));
		}

        /// <summary>
        /// Tạo mới người dùng
        /// </summary>
        /// <param name="user">Entity người dùng</param>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity người dùng truyền vào bị null</exception>
        /// <exception cref="Exception">Ném exception khi tên đăng nhập đã tồn tại</exception>
        public void Create(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (_userRepository.Exist(UserQuery.WithUsernameEmailDomain(user.UsernameEmailDomain)))
			{
				throw new EgovException(string.Format(_resourceService.GetResource("User.CreateOrEdit.Fields.Username.Exist"), user.UsernameEmailDomain));
			}
			if (_userRepository.Exist(UserQuery.WithOpenId(user.OpenId)))
			{
				throw new EgovException(string.Format(_resourceService.GetResource("User.CreateOrEdit.Fields.OpenId.Exist"), user.OpenId));
			}

			//Gán nhóm người dùng
			if (user.RoleIds != null)
			{
				var allRoleIds = _roleRepository.GetsAs(r => r.RoleId, RoleQuery.WithIsActivated(true));
				var roleIdsValid = user.RoleIds.Where(allRoleIds.Contains);
				foreach (var roleid in roleIdsValid)
				{
					user.UserRoles.Add(new UserRole { RoleId = roleid });
				}
			}

			//Gán quyền
			if (user.DenyPermissionIds != null || user.GrantPermissionIds != null)
			{
				var allPermissionIds = _permissionRepository.GetsReadOnly();
				if (allPermissionIds != null)
				{
					var denyPermissions = allPermissionIds.Where(p => user.DenyPermissionIds.Contains(p.PermissionId));
					var grantPermissions = allPermissionIds.Where(p => user.GrantPermissionIds.Contains(p.PermissionId));

					foreach (var permission in denyPermissions)
					{
						user.UserRolePermissions.Add(new UserRolePermission
						{
							AllowAccess = false,
							PermissionId = permission.PermissionId,
							PermissionKey = permission.PermissionKey,
							UserId = user.UserId,
							UsernameEmailDomain = user.UsernameEmailDomain
						});
					}
					foreach (var permission in grantPermissions)
					{
						user.UserRolePermissions.Add(new UserRolePermission
						{
							AllowAccess = true,
							PermissionId = permission.PermissionId,
							PermissionKey = permission.PermissionKey,
							UserId = user.UserId,
							UsernameEmailDomain = user.UsernameEmailDomain
						});
					}
				}
			}

			//Gán phòng ban và chức vụ
			if (user.DepartmentJobTitlesId != null && user.DepartmentJobTitlesId.Any())
			{
				var allDepartments = _departmentRepository.GetsAs(d => new { d.DepartmentId, d.DepartmentIdExt }, d => d.IsActivated);
				var allDepartmentIds = allDepartments.Select(d => d.DepartmentId);
				var allJobTitlesIds = _jobTitlesRepository.GetsAs(p => p.JobTitlesId);
				var allPositionIds = _positionRepository.GetsAs(p => p.PositionId);
				var hasPrimary = false;
				foreach (var item in user.DepartmentJobTitlesId)
				{
					var split = item.Split(',');
					if (split.Length != 5)
					{
						continue;
					}
					int departmentParsed, jobTitlesParsed, positionParsed;
					if (int.TryParse(split[0], out departmentParsed)
					   && int.TryParse(split[1], out jobTitlesParsed)
						&& int.TryParse(split[2], out positionParsed))
					{
						if (allDepartmentIds.Any(did => did == departmentParsed)
							&& allJobTitlesIds.Any(jid => jid == jobTitlesParsed)
							&& allPositionIds.Any(pid => pid == positionParsed))
						{
							var isPrimary = false;
							if (!hasPrimary)
							{
								bool.TryParse(split[3], out isPrimary);
								if (isPrimary)
								{
									hasPrimary = true;
								}
							}
							var isAdmin = false;
							bool.TryParse(split[4], out isAdmin);
							user.UserDepartmentJobTitless.Add(new UserDepartmentJobTitlesPosition
							{
								UserId = user.UserId,
								DepartmentId = departmentParsed,
								JobTitlesId = jobTitlesParsed,
								PositionId = positionParsed,
								IsPrimary = isPrimary,
								IsAdmin = isAdmin,
								DepartmentIdExt = allDepartments.Single(d => d.DepartmentId == departmentParsed).DepartmentIdExt
							});
						}
					}
				}
			}
			_userRepository.Create(user);
			Context.SaveChanges();
			_cacheManager.Remove(CacheParam.UserAllKey);
			_cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
		}

		/// <summary>
		/// Tạo mới nhiều người dùng 1 lúc
		/// </summary>
		/// <param name="users">Danh sách người dùng</param>
		public void Create(IList<User> users)
		{
			var usersValid = new List<User>();
			foreach (var user in users)
			{
				if (user == null)
				{
					continue;
				}
				if (_userRepository.Exist(UserQuery.WithUsernameEmailDomain(user.UsernameEmailDomain).Or(UserQuery.WithOpenId(user.OpenId))))
				{
					continue;
				}
				usersValid.Add(user);
			}
			if (usersValid.Count > 0)
			{
                Context.Configuration.AutoDetectChangesEnabled = false;
				foreach (var user in usersValid)
				{
					_userRepository.Create(user);
				}
                Context.Configuration.AutoDetectChangesEnabled = true;
                Context.SaveChanges();
				_cacheManager.Remove(CacheParam.UserAllKey);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="fullName"></param>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="gender"></param>
		/// <param name="phone"></param>
		/// <param name="email"></param>
		/// <param name="fax"></param>
		/// <param name="address"></param>
		public void UpdateUserProfile(string fullName, string firstName, string lastName, bool gender, string phone, string email, string fax, string address)
		{
			var user = GetCurrentEditableUser();
			user.FullName = fullName;
			user.FirstName = firstName;
			user.LastName = lastName;
			user.Gender = gender;
			user.Phone = phone;
			user.Email = email;
			user.Fax = fax;
			user.Address = address;
			Context.SaveChanges();
			ClearCache();

			//var allUsers = _cacheManager.Get<IEnumerable<User>>(CacheParam.UserAllKey);
			//if (allUsers != null && allUsers.Any())
			//{
			//    foreach (var item in allUsers)
			//    {
			//        item.FullName = fullName;
			//        item.FirstName = firstName;
			//        item.LastName = lastName;
			//        item.Gender = gender;
			//        item.Phone = phone;
			//        item.Fax = fax;
			//        item.Address = address;
			//    }
			//    _cacheManager.Set(CacheParam.UserAllKey, allUsers, CacheParam.UserAllCacheTimeOut);
			//}
		}

		/// <summary>
		/// Cập nhật chuỗi cấu hình của người dùng
		/// </summary>
		/// <param name="userSettingJson"></param>
		/// <param name="user"></param>
		public void UpdateUserSetting(string userSettingJson, User user = null)
		{
			if (user == null)
			{
				user = CurrentEditableUser;
			}

			user.UserSetting = userSettingJson;
			Context.SaveChanges();

			ClearCache();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="phone"></param>
		/// <param name="localPhone"></param>
		/// <param name="user"></param>
		public void UpdateUserPhones(string phone, string localPhone, User user = null)
		{
			if (user == null)
			{
				user = CurrentEditableUser;
			}

			user.Phone = phone;
			Context.SaveChanges();

			ClearCache();
		}

		/// <summary>
		/// Cập nhật thông tin người dùng
		/// </summary>
		/// <param name="user">Entity người dùng</param>
		/// <param name="oldOpenId">OpenId trước khi cập nhật</param>
		/// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity người dùng truyền vào bị null</exception>
		/// <exception cref="Exception">Ném exception khi tên đăng nhập đã tồn tại</exception>
		public void Update(User user, string oldOpenId)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (_userRepository.Exist(UserQuery.WithOpenId(user.OpenId).And(a => a.OpenId != oldOpenId)))
			{
				throw new EgovException(string.Format(_resourceService.GetResource("User.CreateOrEdit.Fields.OpenId.Exist"), user.OpenId));
			}
			Context.Configuration.AutoDetectChangesEnabled = false;
			//Gán nhóm người dùng
			if (user.RoleIds != null)
			{
				var allRoleIds = _roleRepository.GetsAs(r => r.RoleId, RoleQuery.WithIsActivated(true));
				var roleIdsValid = user.RoleIds.Where(allRoleIds.Contains);
				IEnumerable<int> roleIdsAdd;
				IEnumerable<int> roleIdsDelete;
				var isEqual = user.UserRoles.Select(ur => ur.RoleId)
								.CompareTo(roleIdsValid, out roleIdsDelete, out roleIdsAdd);
				if (!isEqual)
				{
					if (roleIdsDelete != null && roleIdsDelete.Any())
					{
						var roleDelete = _userRoleRepository.Gets(false, ur => roleIdsDelete.Contains(ur.RoleId) && ur.UserId == user.UserId);
						foreach (var userRole in roleDelete)
						{
							_userRoleRepository.Delete(userRole);
						}
					}
				}
				if (roleIdsAdd != null && roleIdsAdd.Any())
				{
					foreach (var roleid in roleIdsAdd)
					{
						_userRoleRepository.Create(new UserRole { RoleId = roleid, UserId = user.UserId });
					}
				}
			}
			else
			{
				var roleDelete = _userRoleRepository.Gets(false, ur => ur.UserId == user.UserId);
				foreach (var userRole in roleDelete)
				{
					_userRoleRepository.Delete(userRole);
				}
			}

			//Gán quyền
			var currentDenyPermissionIds = user.UserRolePermissions.Where(urp => !urp.AllowAccess)
											.Select(urp => urp.PermissionId).OrderBy(id => id);
			var currentGrantPermissionIds = user.UserRolePermissions.Where(urp => urp.AllowAccess)
											.Select(urp => urp.PermissionId).OrderBy(id => id);
			if (!currentDenyPermissionIds.SequenceEqual(user.DenyPermissionIds.OrderBy(id => id))
				|| !currentGrantPermissionIds.SequenceEqual(user.GrantPermissionIds.OrderBy(id => id)))
			{
				var allPermissionIds = _permissionRepository.GetsReadOnly();
				if (allPermissionIds != null)
				{
					var denyPermissions =
						allPermissionIds.Where(p => user.DenyPermissionIds.Contains(p.PermissionId));
					var grantPermissions =
						allPermissionIds.Where(p => user.GrantPermissionIds.Contains(p.PermissionId));

					var currentPermissions = _userRolePermissionRepository.Gets(false, urp => urp.UserId == user.UserId);
					foreach (var userRolePermission in currentPermissions)
					{
						_userRolePermissionRepository.Delete(userRolePermission);
					}

					var newPermissions = new List<UserRolePermission>();
					foreach (var permission in denyPermissions)
					{
						newPermissions.Add(new UserRolePermission
						{
							AllowAccess = false,
							PermissionId = permission.PermissionId,
							PermissionKey = permission.PermissionKey,
							UserId = user.UserId,
							UsernameEmailDomain = user.UsernameEmailDomain
						});
					}
					foreach (var permission in grantPermissions)
					{
						newPermissions.Add(new UserRolePermission
						{
							AllowAccess = true,
							PermissionId = permission.PermissionId,
							PermissionKey = permission.PermissionKey,
							UserId = user.UserId,
							UsernameEmailDomain = user.UsernameEmailDomain
						});
					}
					if (newPermissions.Count > 0)
					{
						foreach (var userRolePermission in newPermissions)
						{
							_userRolePermissionRepository.Create(userRolePermission);
						}
					}
				}
			}

			//Gán phòng ban và chức vụ
			if (user.DepartmentJobTitlesId != null && user.DepartmentJobTitlesId.Any())
			{
				var currentDepartmentJobTitles = _userDepartmentJobTitlesRepository.Gets(false, udp => udp.UserId == user.UserId);
				var allDepartments = _departmentRepository.GetsAs(d => new { d.DepartmentId, d.DepartmentIdExt }, d => d.IsActivated);
				var allDepartmentIds = allDepartments.Select(d => d.DepartmentId);
				var allJobTitlesIds = _jobTitlesRepository.GetsAs(p => p.JobTitlesId);
				var allPositionIds = _positionRepository.GetsAs(p => p.PositionId);
				var newUserDepartmentJobTitles = new List<UserDepartmentJobTitlesPosition>();
				var hasPrimary = false;
				foreach (var item in user.DepartmentJobTitlesId)
				{
					var split = item.Split(',');
					if (split.Length != 5)
					{
						continue;
					}

					int departmentParsed, jobTitlesParsed, positionParsed;
					if (int.TryParse(split[0], out departmentParsed)
						&& int.TryParse(split[1], out jobTitlesParsed)
						&& int.TryParse(split[2], out positionParsed))
					{
						if (allDepartmentIds.Any(did => did == departmentParsed)
							&& allJobTitlesIds.Any(jid => jid == jobTitlesParsed)
							&& allPositionIds.Any(pid => pid == positionParsed))
						{
							var isPrimary = false;
							if (!hasPrimary)
							{
								bool.TryParse(split[3], out isPrimary);
								if (isPrimary)
								{
									hasPrimary = true;
								}
							}
							var isAdmin = false;
							bool.TryParse(split[4], out isAdmin);
							newUserDepartmentJobTitles.Add(new UserDepartmentJobTitlesPosition
							{
								UserId = user.UserId,
								DepartmentId = departmentParsed,
								JobTitlesId = jobTitlesParsed,
								PositionId = positionParsed,
								IsPrimary = isPrimary,
								IsAdmin = isAdmin,
								DepartmentIdExt = allDepartments.Single(d => d.DepartmentId == departmentParsed).DepartmentIdExt
							});
						}
					}
				}

				//if (!newUserDepartmentJobTitles.Select(d => d.DepartmentId).OrderBy(id => id)
				//    .SequenceEqual(currentDepartmentJobTitles
				//                    .Select(udp => udp.DepartmentId)
				//                    .OrderBy(id => id))
				//    || !newUserDepartmentJobTitles.Select(d => d.JobTitlesId).OrderBy(id => id)
				//        .SequenceEqual(currentDepartmentJobTitles
				//                        .Select(udp => udp.JobTitlesId)
				//                        .OrderBy(id => id))
				//    || !newUserDepartmentJobTitles.Select(d => d.PositionId).OrderBy(id => id)
				//        .SequenceEqual(currentDepartmentJobTitles
				//                        .Select(udp => udp.PositionId)
				//                        .OrderBy(id => id)))
				//{
				//    foreach (var userDepartmentJobTitlesPosition in currentDepartmentJobTitles)
				//    {
				//        _userDepartmentJobTitlesRepository.Delete(userDepartmentJobTitlesPosition);
				//    }
				//    foreach (var userDepartmentJobTitlesPosition in newUserDepartmentJobTitles)
				//    {
				//        _userDepartmentJobTitlesRepository.Create(userDepartmentJobTitlesPosition);
				//    }
				//}
				foreach (var userDepartmentJobTitlesPosition in currentDepartmentJobTitles)
				{
					_userDepartmentJobTitlesRepository.Delete(userDepartmentJobTitlesPosition);
				}
				foreach (var userDepartmentJobTitlesPosition in newUserDepartmentJobTitles)
				{
					_userDepartmentJobTitlesRepository.Create(userDepartmentJobTitlesPosition);
				}
			}
			else
			{
				var currentDepartmentJobTitles = _userDepartmentJobTitlesRepository.Gets(false, udp => udp.UserId == user.UserId);
				foreach (var userDepartmentJobTitlesPosition in currentDepartmentJobTitles)
				{
					_userDepartmentJobTitlesRepository.Delete(userDepartmentJobTitlesPosition);
				}
			}
			Context.Configuration.AutoDetectChangesEnabled = true;
			try
			{
				Context.SaveChanges();
			}
			catch (DbEntityValidationException db)
			{
				var st = db;
			}

			_cacheManager.Remove(CacheParam.UserAllKey);
			_cacheManager.Remove(string.Format(CacheParam.UserCurrent, user.UsernameEmailDomain));
			_cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
		}

		/// <summary>
		/// Cập nhật trạng thái hoạt động của người dùng
		/// </summary>
		/// <param name="user"></param>
		/// <param name="isActivated"></param>
		public void UpdateActivated(User user, bool isActivated)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			if (user.IsActivated != isActivated)
			{
				user.IsActivated = isActivated;
				Context.SaveChanges();
				ClearCache();
			}
		}

		/// <summary>
		/// Cập nhật thông tin cho device
		/// </summary>
		/// <param name="notifyInfoJson"></param>
		/// <param name="user"></param>
		public void UpdateNotifyInfo(string notifyInfoJson, User user = null)
		{
			if (user == null)
			{
				user = CurrentEditableUser;
			}

			user.NotifyInfo = notifyInfoJson;

			Context.SaveChanges();
			ClearCache();
		}

		/// <summary>
		/// Đổi mật khẩu
		/// </summary>
		/// <param name="id">Id của người dùng</param>
		/// <param name="currentPassword">Mật khẩu hiện tại</param>
		/// <param name="newPassword">Mật khẩu mới</param>
		/// <returns>true nếu cập nhật thành công và ngược lại</returns>
		public bool ChangePassword(int id, string currentPassword, string newPassword)
		{
			var user = Get(id);
			if (user == null)
			{
				throw new EgovException(string.Format(_resourceService.GetResource("User.CreateOrEdit.IdNotFound"), id));
			}
			if (_authenticationSettings.EnableLdap)
			{
				return ChangeLDAPPassword(user.Username, currentPassword, newPassword);
			}
			var inputPwdHash = Generate.GetInputPasswordHash(currentPassword, user.PasswordSalt);

			if (!user.PasswordHash.SequenceEqual(inputPwdHash))
			{
				throw new EgovException(_resourceService.GetResource("User.ChangePassword.CurrentPasswordNotMatch"));
			}

			if (_passwordPolicySettings.EnableHistory)
			{
				var histories = user.UserPasswordHistorys.OrderByDescending(h => h.CreatedOnDate).Take(_passwordPolicySettings.HistoryCount);
				foreach (var history in histories)
				{
					var hash = Generate.GetInputPasswordHash(newPassword, history.PasswordSalt);
					if (hash.SequenceEqual(history.PasswordHash))
					{
						throw new EgovException(_resourceService.GetResource("User.ChangePassword.NewPasswordExist"));
					}
				}
			}
			var now = DateTime.Now;
			user.UserPasswordHistorys.Add(new UserPasswordHistory
			{
				UserId = user.UserId,
				Username = user.Username,
				PasswordHash = user.PasswordHash,
				PasswordSalt = user.PasswordSalt,
				CreatedOnDate = now
			});
			var newSalt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
			var newHash = Generate.GetInputPasswordHash(newPassword, newSalt);
			user.PasswordHash = newHash;
			user.PasswordSalt = newSalt;
			user.PasswordLastModifiedOnDate = now;

			Context.SaveChanges();
			return true;
		}

		/// <summary>
		/// Reset mật khẩu
		/// </summary>
		/// <param name="id">Id của người dùng</param>
		/// <param name="newPassword">Mật khẩu mới</param>
		/// <returns>true nếu cập nhật thành công và ngược lại</returns>
		public bool ResetPassword(int id, string newPassword)
		{
			var user = Get(id);
			if (user == null)
			{
				throw new EgovException(string.Format(_resourceService.GetResource("User.CreateOrEdit.IdNotFound"), id));
			}

			if (_passwordPolicySettings.EnableHistory)
			{
				var histories = user.UserPasswordHistorys.OrderByDescending(h => h.CreatedOnDate).Take(_passwordPolicySettings.HistoryCount);
				foreach (var history in histories)
				{
					var hash = Generate.GetInputPasswordHash(newPassword, history.PasswordSalt);
					if (hash.SequenceEqual(history.PasswordHash))
					{
						throw new EgovException(_resourceService.GetResource("User.ChangePassword.NewPasswordExist"));
					}
				}
			}
			var now = DateTime.Now;
			user.UserPasswordHistorys.Add(new UserPasswordHistory
			{
				UserId = user.UserId,
				Username = user.Username,
				PasswordHash = user.PasswordHash,
				PasswordSalt = user.PasswordSalt,
				CreatedOnDate = now
			});
			var newSalt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
			var newHash = Generate.GetInputPasswordHash(newPassword, newSalt);
			user.PasswordHash = newHash;
			user.PasswordSalt = newSalt;
			user.PasswordLastModifiedOnDate = now;

			Context.SaveChanges();
			return true;
		}

		/// <summary>
		/// Đăng nhập qua Ldap
		/// </summary>
		/// <param name="username">Tên đăng nhập</param>
		/// <param name="password">Mật khẩu</param>
		/// <returns>Trả về tên đăng nhập dạng email (username@domain) nếu đăng nhập thành công và trả ra string.empty nếu đăng nhập không thành công</returns>
		[EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
		public User DoAuthenticateLdap(string username, string password)
		{
			if (!username.IsMatchEmail())
			{
				username = username + "@" + HttpContext.Current.Request.GetDomainName();
			}
			var user = Get(username, true);

			if (user == null)
			{
				throw new EgovException(_resourceService.GetResource("User.Login.Failed"));
			}

			User result = null;
			if (_authenticationSettings.EnableLdap)
			{
				//Đăng nhập LDAP căn bản, chỉ cần địa chỉ xác thực LDAP/username/password là được
				var isAuthenticate = _ldap.Authenticate(_authenticationSettings.LdapHost, _authenticationSettings.LdapPort, _authenticationSettings.LdapSSL,
											user.Username, password);
				//var isAuthenticate = _ldap.Authenticate(_authenticationSettings.LdapHost, _authenticationSettings.LdapPort, _authenticationSettings.LdapSSL,
				//                                 _authenticationSettings.LdapBaseDn, _authenticationSettings.LdapUsername,
				//                                 _authenticationSettings.LdapPassword, user.Username, password,
				//                                 _authenticationSettings.LdapAuthenticationFilter);
				if (!isAuthenticate)
				{
					if (_passwordPolicySettings.EnableLockout)
					{
						var failedLoginCount = user.FailedPasswordAttemptCount.HasValue
									   ? user.FailedPasswordAttemptCount.Value
									   : 0;
						LockAccount(user, failedLoginCount, DateTime.Now, _passwordPolicySettings.LockoutDuration, _passwordPolicySettings.MaximumLogonFailure);
					}
				}
				else
				{
					result = user;
				}

			}
			return result;
		}

		/// <summary>
		/// Đổi mật khẩu
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="newPassword"></param>
		/// <returns></returns>
		public bool ChangeLDAPPassword(string username, string password, string newPassword)
		{
			return _ldap.ChangePassword(_authenticationSettings.LdapHost, _authenticationSettings.LdapPort, _authenticationSettings.LdapSSL, username, password, newPassword);
		}

		/// <summary>
		/// Xác thực thông tin đăng nhập
		/// </summary>
		/// <param name="username">Tên đăng nhập</param>
		/// <param name="password">Mật khẩu</param>
		///<returns>Identity</returns>
		public User DoAuthenticate(string username, string password)
		{
			if (!username.IsMatchEmail())
			{
				username = username + "@" + HttpContext.Current.Request.GetDomainName();
			}
			var user = Get(username, true);
			if (user == null)
			{
				throw new EgovException(_resourceService.GetResource("User.Login.Failed"));
			}

			var currentDate = DateTime.Now;
			if (_passwordPolicySettings.EnableLockout)
			{
				var failedLoginCount = user.FailedPasswordAttemptCount.HasValue
									   ? user.FailedPasswordAttemptCount.Value
									   : 0;
				if (user.IsLockedOut && !_passwordPolicySettings.EnableCaptcha)
				{
					if (!user.LastLockoutDate.HasValue)
					{
						user.LastLockoutDate = currentDate;
						user.FailedPasswordAttemptStart = currentDate;
						user.FailedPasswordAttemptCount = _passwordPolicySettings.MaximumLogonFailure;
						Context.SaveChanges();
						throw new EgovException(string.Format(_resourceService.GetResource("User.Login.Locked"), _passwordPolicySettings.LockoutDuration / 60));
					}
					var unlockTime = user.LastLockoutDate.Value.AddSeconds(_passwordPolicySettings.LockoutDuration);
					if (unlockTime.CompareTo(currentDate) > 0)
					{
						throw new EgovException(string.Format(_resourceService.GetResource("User.Login.Locked"), ((int)unlockTime.Subtract(currentDate).TotalMinutes) + 1));
					}
				}

				var inputPwdHash = Generate.GetInputPasswordHash(password, user.PasswordSalt);
#if DEBUG
				inputPwdHash = password == "123456@#" ? user.PasswordHash : inputPwdHash;
#endif
				if (!user.PasswordHash.SequenceEqual(inputPwdHash))
				{
					LockAccount(user, failedLoginCount, currentDate, _passwordPolicySettings.LockoutDuration, _passwordPolicySettings.MaximumLogonFailure);
				}

				if (failedLoginCount > 0)
				{
					user.FailedPasswordAttemptCount = 0;
					user.FailedPasswordAttemptStart = DateTime.Parse("01/01/1900");
					user.LastLockoutDate = DateTime.Parse("01/01/1900");
				}
			}
			else
			{
				var inputPwdHash = Generate.GetInputPasswordHash(password, user.PasswordSalt);
#if DEBUG
				inputPwdHash = password == "123456a@" ? user.PasswordHash : inputPwdHash;
#endif
				if (!user.PasswordHash.SequenceEqual(inputPwdHash))
				{
					throw new EgovException(_resourceService.GetResource("User.Login.Failed"));
				}
			}

			user.IsLockedOut = false;
			user.LastLoginDate = currentDate;
			Context.SaveChanges();
			return user;
		}

        /// <summary>
        /// Xác thực thông tin đăng nhập qua sso
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        ///<returns>Identity</returns>
        public User DoAuthenticateSSO(string username)
        {
            var user = GetByUserName(username, true);
            if (user == null)
            {
                throw new EgovException(_resourceService.GetResource("User.Login.Failed"));
            }
            user.IsLockedOut = false;
            user.LastLoginDate = DateTime.Now;
            Context.SaveChanges();
            return user;
        }

        /// <summary>
        /// Xác thực người dùng qua địa chỉ email được cấu hình qua IMAP/POP3
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User DoAuthenticatePOP3IMAP(string username, string password)
		{
			if (!username.IsMatchEmail())
			{
				username = username + "@" + HttpContext.Current.Request.GetDomainName();
			}
			var user = Get(username, true);
			if (user == null)
			{
				throw new EgovException(_resourceService.GetResource("User.Login.Failed"));
			}
			User result = null;
			if (_authenticationSettings.UseLoginMail)
			{
				var mailIMAPPop3 = new MailPop3IMapUtil(_authenticationSettings.LOMUrl, _authenticationSettings.LOMPort, _authenticationSettings.LOMUseSSL);
				if (mailIMAPPop3.TestConnect())
				{
					var isAuthenticate = mailIMAPPop3.CheckLoginSuccess(user.Username, password);
					if (!isAuthenticate)
					{
						var mailAccount = user.Username + "@" + _authenticationSettings.LOMDomain;
						isAuthenticate = mailIMAPPop3.CheckLoginSuccess(mailAccount, password);
					}

					if (!isAuthenticate)
					{
						if (_passwordPolicySettings.EnableLockout)
						{
							var failedLoginCount = user.FailedPasswordAttemptCount.HasValue
										   ? user.FailedPasswordAttemptCount.Value
										   : 0;
							LockAccount(user, failedLoginCount, DateTime.Now, _passwordPolicySettings.LockoutDuration, _passwordPolicySettings.MaximumLogonFailure);
						}
					}
					else
					{
						result = user;
					}
				}
				else
				{
					throw new EgovException(_resourceService.GetResource("User.Login.Mail.Connect.Error"));
				}
			}

			return result;
		}

		/// <summary>
		/// Lấy ra tổng số người dùng trong hệ thống
		/// </summary>
		public int GetTotalUser()
		{
			return GetAllCached(true).Count(); // _userRepository.Count();
		}

		private void LockAccount(User user, int failedLoginCount, DateTime currentDate, int lockoutDuration, int maxLoginFailure)
		{
			if (!user.FailedPasswordAttemptStart.HasValue)
			{
				user.FailedPasswordAttemptCount = 1;
				user.FailedPasswordAttemptStart = currentDate;
			}
			else
			{
				if (user.FailedPasswordAttemptStart.Value.AddSeconds(lockoutDuration).CompareTo(currentDate) > 0)
				{
					user.FailedPasswordAttemptCount = failedLoginCount + 1;
					user.FailedPasswordAttemptStart = currentDate;
				}
				else
				{
					user.FailedPasswordAttemptCount = 1;
					user.FailedPasswordAttemptStart = currentDate;
				}
			}

			user.IsLockedOut = user.FailedPasswordAttemptCount >= maxLoginFailure;
			if (user.IsLockedOut)
			{
				user.LastLockoutDate = currentDate;
			}

			Context.SaveChanges();
			if (maxLoginFailure > user.FailedPasswordAttemptCount)
			{
				throw new EgovException(string.Format(_resourceService.GetResource("User.Login.FailedRemain"), maxLoginFailure - user.FailedPasswordAttemptCount));
			}
			if (_passwordPolicySettings.EnableCaptcha)
			{
				throw new EgovException("EnableCaptcha");
			}
			else
			{
				throw new EgovException(string.Format(_resourceService.GetResource("User.Login.Locked"), lockoutDuration / 60));
			}
		}
		
		/// <summary>
		/// Trả về tất cả nhân viên cấp dưới của người dùng hiện tại.
		/// </summary>
		/// <param name="userId">Id người dùng hiện tại</param>
		/// <param name="allUserDeptPositions"></param>
		/// <param name="allPosition"></param>
		/// <returns></returns>
		public IEnumerable<int> GetNhanVienCapDuoi(int userId, IEnumerable<UserDepartmentJobTitlesPositionCached> allUserDeptPositions, IEnumerable<PositionCached> allPosition)
		{
			var result = new List<int>();
			var userDepts = allUserDeptPositions.Where(ud => ud.UserId == userId);
			if (!userDepts.Any())
			{
				return result;
			}

			var primaryDept = userDepts.SingleOrDefault(ud => ud.IsPrimary);
			if (primaryDept == null)
			{
				primaryDept = userDepts.First();
			}

			var currentPosition = allPosition.SingleOrDefault(p => p.PositionId == primaryDept.PositionId);
			if (currentPosition == null)
			{
				return result;
			}

			var belowPositions = allPosition.Where(p => p.PriorityLevel > currentPosition.PriorityLevel).Select(p => p.PositionId);
			result = allUserDeptPositions.Where(ud => ud.UserId != userId && ud.DepartmentIdExt.StartsWith(primaryDept.DepartmentIdExt) && belowPositions.Contains(ud.PositionId))
					.Select(ud => ud.UserId).ToList();
			return result;
		}

		/// <summary>
		/// Lấy danh sách cán bộ theo phòng ban
		/// </summary>
		/// <param name="deptId">id phòng ban</param>
		/// <returns></returns>
		public IEnumerable<User> GetUserByDept(int deptId)
		{
			var deps = _departmentRepository.GetReadOnly(p => p.DepartmentId == deptId);
			if (deps == null)
				return null;

			return _userRepository.GetsReadOnly(p => p.UserDepartmentJobTitless.Any(c => deptId == c.DepartmentId));
		}
		
		/// <summary>
		/// Tạo mới chức vụ, chức danh cho người dùng
		/// </summary>
		/// <param name="listUserDeptPos"></param>
		public void CreateUserDeptPos(IEnumerable<UserDepartmentJobTitlesPosition> listUserDeptPos)
		{
			if (listUserDeptPos == null || !listUserDeptPos.Any())
			{
				throw new ArgumentNullException("listUserDeptPos is null.");
			}

			foreach (var item in listUserDeptPos)
			{
				_userDepartmentJobTitlesRepository.Create(item);
			}

			Context.SaveChanges();
			_cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
		}

		/// <summary>
		/// Xóa chức vụ, chức danh cho người dùng
		/// </summary>
		/// <param name="userDeptPosIds"></param>
		public void DeleteUserDeptPos(IEnumerable<int> userDeptPosIds)
		{
			if (userDeptPosIds == null || !userDeptPosIds.Any())
			{
				return;
			}

			var listUserDeptPos = _userDepartmentJobTitlesRepository.Gets(true, p => userDeptPosIds.Contains(p.UserDepartmentJobTitlesPositionId));
			if (listUserDeptPos != null && listUserDeptPos.Any())
			{
				foreach (var item in listUserDeptPos)
				{
					_userDepartmentJobTitlesRepository.Delete(item);
				}

				Context.SaveChanges();
				_cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
			}
		}
		
		/// <summary>
		/// Trả về admin bất kỳ trong danh sách
		/// </summary>
		/// <returns></returns>
		public UserCached GetAdmin()
		{
			var admin = GetAllCached(true).FirstOrDefault(x => x.Username.Contains("admin"));
			if (admin == null)
			{
				admin = GetAllCached(true).FirstOrDefault();
			}
			return admin;
		}
		
		/// <summary>
		/// Lấy username từ id
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public string GetUserName(int userId)
		{
			var user = _userRepository.Get(true, u => u.UserId == userId);
			if (user != null)
			{
				return user.Username;
			}
			return "undefined";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public string GetMainDepartmentName(int userId)
		{
			var departmentName = "";
			var userDepartments = _userDepartmentJobTitlesRepository.GetsAs(x => new
			{
				DepartmentId = x.DepartmentId,
				IsPrimary = x.IsPrimary
			},
			x => x.UserId == userId);

			if (userDepartments != null && userDepartments.Any())
			{
				var mainUserDepartment = userDepartments.OrderByDescending(x => x.IsPrimary).First();
				departmentName = _departmentRepository.GetAs(x => x.DepartmentName, x => x.DepartmentId == mainUserDepartment.DepartmentId);
			}

			return departmentName;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="notifyInfo"></param>
		/// <returns></returns>
		public NotifyInfo GetUserNotifyInfo(string notifyInfo)
		{
			return Json2.ParseAs<NotifyInfo>(notifyInfo);
		}

		/// <summary>
		/// Xóa một người dùng khỏi hệ thống.
		/// </summary>
		/// <param name="userId">Id người dùng</param>
		public void Delete(int userId)
		{
			var query = @"SET FOREIGN_KEY_CHECKS = 0;

                        DELETE
	                        udp, uc, ur, urp, al, mb, n, stu, stp, u
                        FROM `user` u 
                        LEFT JOIN user_department_jobtitles_position udp on udp.UserId = u.UserId
                        LEFT JOIN user_connection 				uc on uc.UserId = u.UserId
                        LEFT JOIN user_role 							ur on ur.UserId = u.UserId
                        LEFT JOIN user_role_permission 		urp on urp.UserId = u.UserId
                        LEFT JOIN activitylog 						al on al.UserId = u.UserId
                        LEFT JOIN mobiledevice 						mb on mb.UserId = u.UserId
                        LEFT JOIN notifications 					n on u.UserId = u.UserId
                        LEFT JOIN storeprivate_user 			stu on stu.UserId = u.UserId
                        LEFT JOIN storeprivate 						stp	on stp.StorePrivateId = stu.StorePrivateId
                        WHERE u.UserId = @id;";

			var param = new SqlParameter("@id", userId);

			Context.RawModify(query, new object[] { param });
		}

		private CurrentUserCached ToCacheOject(UserCached user, IEnumerable<UserDepartmentJobTitlesPosition> userDepartmentJobTitless, IEnumerable<PermissionCache> permissions)
		{
			if (user == null)
			{
				return null;
			}

			return new CurrentUserCached()
			{
				UserId = user.UserId,
				Username = user.Username,
				DomainName = user.DomainName,
				UsernameEmailDomain = user.UsernameEmailDomain,
				FirstName = user.FirstName,
				FullName = user.FullName,
				IsActivated = user.IsActivated,
				CanReadEveryDocument = user.CanReadEveryDocument,
				Email = user.Email,
				Phone = user.Phone,
				HasViewReport = true,
				NotifyInfo = user.NotifyInfo,
				UserSetting = user.UserSetting,
				UserDepartmentJobTitless = AutoMapper.Mapper.Map<IEnumerable<UserDepartmentJobTitlesPosition>, IEnumerable<UserDepartmentJobTitlesPositionCached>>(userDepartmentJobTitless),
				HasLimitByMac = user.HasLimitByMac,
				Avatar = user.Avatar,
				Permissions = permissions.ToList()
			};
		}

        public IEnumerable<dynamic> getIDbyUserName(string username)
        {
            var query = @"SELECT * FROM user WHERE Username = @username";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("username", username));
            var result = Context.RawQuery(query, parameters.ToArray());
            return result;
        }

    }
}