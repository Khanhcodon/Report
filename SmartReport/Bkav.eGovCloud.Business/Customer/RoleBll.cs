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

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : RoleBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 080912</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : BLL tương ứng với bảng Role trong CSDL</para>
    /// </summary>
    public class RoleBll : ServiceBase
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly ResourceBll _resourceService;
        private readonly UserBll _userService;
        private readonly IRepository<UserRolePermission> _userRolePermissionRepository;
        private readonly IRepository<Permission> _permissionRepository;
        private readonly MemoryCacheManager _cacheManager;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="resourceService">Bll tương ứng với bảng Resource trong CSDL</param>
        /// <param name="userService">Bll tương ứng với bảng User trong CSDL</param>
        /// <param name="cacheManager">Cache manager</param>
        public RoleBll(IDbCustomerContext context,
                        ResourceBll resourceService, UserBll userService,
                        MemoryCacheManager cacheManager)
            : base(context)
        {
            _roleRepository = Context.GetRepository<Role>();
            _userRoleRepository = Context.GetRepository<UserRole>();
            _resourceService = resourceService;
            _userService = userService;
            _userRolePermissionRepository = Context.GetRepository<UserRolePermission>();
            _permissionRepository = Context.GetRepository<Permission>();
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Lấy ra tất cả các nhóm người dùng.
        /// </summary>
        /// <param name="isActivated">Lấy ra tất cả các nhóm người dùng đang hoạt động: true và ngược lại: false. Nếu null sẽ bỏ qua điều kiện này</param>
        /// <returns>Danh sách nhóm người dùng</returns>
        public IEnumerable<Role> Gets(bool? isActivated = null)
        {
            return _roleRepository.GetsReadOnly(RoleQuery.WithIsActivated(isActivated), Context.Filters.Sort<Role, string>(r => r.RoleName));
        }

        /// <summary>
        /// Lấy ra tất cả các nhóm người dùng.
        /// </summary>
        /// <param name="projector"></param>
        /// <param name="isActivated">Lấy ra tất cả các nhóm người dùng đang hoạt động: true và ngược lại: false. Nếu null sẽ bỏ qua điều kiện này</param>
        /// <returns>Danh sách nhóm người dùng</returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Role, T>> projector, bool? isActivated = null)
        {
            return _roleRepository.GetsAs(projector, RoleQuery.WithIsActivated(isActivated), Context.Filters.Sort<Role, string>(r => r.RoleName));
        }

        /// <summary>
        /// Lấy ra tất cả các id của nhóm người dùng được tự động gán cho người dùng
        /// </summary>
        /// <returns>Danh sách các id</returns>
        public IEnumerable<int> GetAllRoleIdAutoAssign()
        {
            return _roleRepository.GetsAs(r => r.RoleId, r => r.IsActivated && r.IsAutoAssignment);
        }

        /// <summary>
        /// Lấy ra tất cả Id của các nhóm người dùng.
        /// </summary>
        /// <param name="isActivated">Lấy ra tất cả các nhóm người dùng đang hoạt động: true và ngược lại: false. Nếu null sẽ bỏ qua điều kiện này</param>
        /// <returns>Danh sách id của các nhóm người dùng</returns>
        public IEnumerable<int> GetAllRoleIds(bool? isActivated = null)
        {
            return _roleRepository.GetsAs(r => r.RoleId, RoleQuery.WithIsActivated(isActivated));
        }

        /// <summary>
        /// Lấy ra nhóm người dùng theo id
        /// </summary>
        /// <param name="id">Id của nhóm người dùng</param>
        /// <returns>Entity nhóm người dùng</returns>
        public Role Get(int id)
        {
            Role user = null;
            if (id > 0)
            {
                user = _roleRepository.Get(id);
            }
            return user;
        }

        /// <summary>
        /// Lấy ra nhóm người dùng theo mã vai trò
        /// </summary>
        /// <param name="roleKey">Mã vai trò</param>
        /// <returns>Entity nhóm người dùng</returns>
        public Role Get(string roleKey)
        {
            Role user = null;
            if (!string.IsNullOrWhiteSpace(roleKey))
            {
                user = _roleRepository.Get(false, r => r.RoleKey == roleKey);
            }
            return user;
        }
        /// <summary>
        /// Kiểm tra mã vai trò đã tồn tại chưa
        /// </summary>
        /// <param name="roleKey">mã vai trò kiểm tra</param>
        /// <param name="oldRoleKey">mã vai trì cũ</param>
        /// <returns></returns>
        public Boolean IsExistRoleKey(string roleKey, string oldRoleKey)
        {
            if (oldRoleKey != null)
            {
                return _roleRepository.Exist(RoleQuery.WithRoleKey(roleKey).And(r => r.RoleKey != oldRoleKey));
            }
            return _roleRepository.Exist(RoleQuery.WithRoleKey(roleKey));
        }

        /// <summary>
        /// Tạo mới nhóm người dùng
        /// </summary>
        /// <param name="role">Entity nhóm người dùng</param>
        public void Create(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            if (_roleRepository.Exist(RoleQuery.WithRoleKey(role.RoleKey)))
            {
                throw new EgovException(string.Format(_resourceService.GetResource("Role.CreateOrEdit.Fields.RoleKey.Exist"), role.RoleKey));
            }
            //Gán người dùng
            if (role.UserIds != null)
            {
                var allUserIds = _userService.GetAllUserIds(true);
                var userIdsValid = role.UserIds.Where(allUserIds.Contains);
                foreach (var userid in userIdsValid)
                {
                    role.UserRoles.Add(new UserRole { UserId = userid });
                }
            }

            //Gán quyền
            if (role.GrantPermissionIds != null)
            {
                var allPermissionIds = _permissionRepository.GetsReadOnly();
                if (allPermissionIds != null)
                {
                    var grantPermissions = allPermissionIds.Where(p => role.GrantPermissionIds.Contains(p.PermissionId));

                    foreach (var permission in grantPermissions)
                    {
                        role.UserRolePermissions.Add(new UserRolePermission
                        {
                            AllowAccess = true,
                            PermissionId = permission.PermissionId,
                            PermissionKey = permission.PermissionKey,
                            RoleId = role.RoleId,
                            RoleKey = role.RoleKey
                        });
                    }
                }
            }
            _roleRepository.Create(role);
            Context.SaveChanges();
            
            RemoveCache();
        }

        /// <summary>
        /// Cập nhật thông tin nhóm người dùng
        /// </summary>
        /// <param name="role">Entity nhóm người dùng</param>
        /// <param name="oldRoleKey">Mã nhóm người dùng cũ</param>
        public void Update(Role role, string oldRoleKey)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            if (_roleRepository.Exist(RoleQuery.WithRoleKey(role.RoleKey).And(r => r.RoleKey != oldRoleKey)))
            {
                throw new EgovException(string.Format(_resourceService.GetResource("Role.CreateOrEdit.Fields.RoleKey.Exist"), role.RoleKey));
            }
            //Gán người dùng
            Context.Configuration.AutoDetectChangesEnabled = false;
            if (role.UserIds != null)
            {
                var allUserIds = _userService.GetAllUserIds(true);
                var userIdsValid = role.UserIds.Where(allUserIds.Contains);
                IEnumerable<int> userIdsAdd;
                IEnumerable<int> userIdsDelete;
                var isEqual = role.UserRoles.Select(ur => ur.UserId)
                                .CompareTo(userIdsValid, out userIdsDelete, out userIdsAdd);
                if (!isEqual)
                {
                    if (userIdsDelete != null && userIdsDelete.Any())
                    {
                        var userDelete = _userRoleRepository.Gets(false, ur => userIdsDelete.Contains(ur.UserId) && ur.RoleId == role.RoleId);
                        foreach (var userRole in userDelete)
                        {
                            _userRoleRepository.Delete(userRole);
                        }
                    }
                }
                if (userIdsAdd != null && userIdsAdd.Any())
                {
                    foreach (var userid in userIdsAdd)
                    {
                        _userRoleRepository.Create(new UserRole { RoleId = role.RoleId, UserId = userid });
                    }
                }
            }
            else
            {
                var userDelete = _userRoleRepository.Gets(false, ur => ur.RoleId == role.RoleId);
                foreach (var userRole in userDelete)
                {
                    _userRoleRepository.Delete(userRole);
                }
            }

            //Gán quyền
            var currentGrantPermissionIds = role.UserRolePermissions.Where(urp => urp.AllowAccess)
                                            .Select(urp => urp.PermissionId).OrderBy(id => id);
            if (!currentGrantPermissionIds.SequenceEqual(role.GrantPermissionIds.OrderBy(id => id)))
            {
                var allPermissionIds = _permissionRepository.GetsReadOnly();
                if (allPermissionIds != null)
                {
                    var grantPermissions = allPermissionIds.Where(p => role.GrantPermissionIds.Contains(p.PermissionId));

                    var currentPermissions = _userRolePermissionRepository.Gets(false, urp => urp.RoleId == role.RoleId);
                    foreach (var userRolePermission in currentPermissions)
                    {
                        _userRolePermissionRepository.Delete(userRolePermission);
                    }
                    foreach (var permission in grantPermissions)
                    {
                        _userRolePermissionRepository.Create(new UserRolePermission
                        {
                            AllowAccess = true,
                            PermissionId = permission.PermissionId,
                            PermissionKey = permission.PermissionKey,
                            RoleId = role.RoleId,
                            RoleKey = role.RoleKey
                        });
                    }
                }
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
            RemoveCache();
        }

        /// <summary>
        /// Xóa nhóm người dùng
        /// </summary>
        /// <param name="role">Entity nhóm người dùng</param>
        public void Delete(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            var userRoles = _userRoleRepository.Gets(false, ur => ur.RoleId == role.RoleId);
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var userRole in userRoles)
            {
                _userRoleRepository.Delete(userRole);
            }

            var rolePermissions = _userRolePermissionRepository.Gets(false, rp => rp.RoleId == role.RoleId);
            foreach (var userRolePermission in rolePermissions)
            {
                _userRolePermissionRepository.Delete(userRolePermission);
            }

            _roleRepository.Delete(role);
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();

            RemoveCache();
        }

        /// <summary>
        /// Lấy ra các quyền của nhóm người dùng theo Id của nhóm người dùng
        /// </summary>
        /// <param name="roleIds">Danh sách Id nhóm người dùng</param>
        /// <param name="projector"></param>
        /// <returns>Danh sách quyền của nhóm người dùng</returns>
        public IEnumerable<T> GetRolePermissions<T>(IEnumerable<int> roleIds, Expression<Func<UserRolePermission, T>> projector)
        {
            IEnumerable<T> result = null;
            if (roleIds != null && roleIds.Any())
            {
                result = _userRolePermissionRepository.GetsAs(projector, urp => urp.RoleId.HasValue && roleIds.Contains(urp.RoleId.Value));
            }
            return result;
        }

        private void RemoveCache()
        {
            _cacheManager.Remove(CacheParam.RolePermissionAllKey);
        }
    }
}
