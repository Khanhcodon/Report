using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : PositionBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 270812
    /// Author      : TrungVH
    /// Description : BLL tương ứng với bảng Position trong CSDL
    /// </summary>
    public class PermissionBll : ServiceBase
    {
        private readonly IRepository<UserRolePermission> _userRolePermissionRepository;
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly UserBll _userService;
        private readonly MemoryCacheManager _cacheManager;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="userService">Bll tương ứng với bảng User trong CSDL</param>
        /// <param name="cacheManager">Cache</param>
        public PermissionBll(IDbCustomerContext context,
                                UserBll userService, MemoryCacheManager cacheManager)
            : base(context)
        {
            _userRolePermissionRepository = Context.GetRepository<UserRolePermission>();
            _permissionRepository = Context.GetRepository<Permission>();
            _userRoleRepository = Context.GetRepository<UserRole>();
            _userService = userService;
            _cacheManager = cacheManager;
            Mapper.CreateMap<UserRolePermission, PermissionCache>();
        }

        /// <summary>
        /// Kiểm tra quyền
        /// </summary>
        /// <param name="permissionKey">Mã quyền</param>
        /// <returns>True: nếu người dùng có quyền và ngược lại</returns>
        public bool HasPemission(string permissionKey)
        {
            var result = false;
            var user = _userService.CurrentUser;

            var userPermissions = user.Permissions; // GetUserPermissions(user);

            var userRoles = _userRoleRepository.Gets(true, u => u.UserId == user.UserId).ToList();
            var userRoleIds = userRoles.Select(ur => ur.RoleId);

            var rolePermissions = GetAllRolePermissions().Where(urp => userRoleIds.Contains(urp.RoleId.Value));
            var allUserPermissions = userPermissions.Concat(rolePermissions);
            var permissions = allUserPermissions.Where(p => p.PermissionKey.Equals(permissionKey, StringComparison.InvariantCultureIgnoreCase));
            if (permissions != null && permissions.Any())
            {
                result = permissions.All(p => p.AllowAccess);
            }
            return result;
        }
        
        /// <summary>
        /// Kiểm tra quyền
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionKey">Mã quyền</param>
        /// <param name="userPermissions">Nguoi dung</param>
        /// <returns>True: nếu người dùng có quyền và ngược lại</returns>
        public bool HasPermission(int userId, List<PermissionCache> userPermissions, string permissionKey)
        {
            var result = false;

            var userRoles = _userRoleRepository.Gets(true, u => u.UserId == userId).ToList();
            var userRoleIds = userRoles.Select(ur => ur.RoleId);

            var rolePermissions = GetAllRolePermissions().Where(urp => userRoleIds.Contains(urp.RoleId.Value));
            var allUserPermissions = userPermissions.Concat(rolePermissions);
            var permissions = allUserPermissions.Where(p => p.PermissionKey.Equals(permissionKey, StringComparison.InvariantCultureIgnoreCase));
            if (permissions != null && permissions.Any())
            {
                result = permissions.All(p => p.AllowAccess);
            }
            return result;
        }

        /// <summary>
        /// Lấy ra tất cả các quyền của tất cả các nhóm người dùng
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PermissionCache> GetAllRolePermissions()
        {
            return _cacheManager.Get(CacheParam.RolePermissionAllKey, CacheParam.RolePermissionAllCacheTimeOut, () =>
            {
                var allRolePermissions = _userRolePermissionRepository.GetsReadOnly(urp => urp.RoleId != null && urp.RoleKey != null);
                return Mapper.Map<IEnumerable<UserRolePermission>, IEnumerable<PermissionCache>>(allRolePermissions);
            });
        }

        ///// <summary>
        ///// Lấy ra tất cả các quyền của người dùng
        ///// </summary>
        ///// <param name="user">Người dùng</param>
        ///// <returns></returns>
        //public IEnumerable<PermissionCache> GetUserPermissions(User user)
        //{
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException("user");
        //    }

        //    return GetUserPermissions(user.UsernameEmailDomain, user.UserId);
        //}

        ///// <summary>
        ///// Lấy ra tất cả các quyền của người dùng
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="usernameDomain"></param>
        ///// <returns></returns>
        //public IEnumerable<PermissionCache> GetUserPermissions(string usernameDomain, int userId)
        //{
        //    return _cacheManager.Get(string.Format(CacheParam.UserPermissionKey, usernameDomain), CacheParam.UserPermissioCacheTimeOut, () =>
        //    {
        //        var userPermissions = _userRolePermissionRepository.Gets(true, u => u.UserId == userId);
        //        return Mapper.Map<IEnumerable<UserRolePermission>, IEnumerable<PermissionCache>>(userPermissions);
        //    });
        //}

        /// <summary>
        /// Lấy ra tất cả các quyền trong hệ thống
        /// </summary>
        /// <returns>Danh sách tất cả các quyền</returns>
        public IEnumerable<Permission> GetAllPermissonsInSystem()
        {
            var result = _permissionRepository.GetsReadOnly();
            //if (!HasPemission("DomainIndex"))
            //{
            //    result = result.Where(p => p.PermissionKey != "DomainIndex");
            //}

            return result;
        }
    }
}
