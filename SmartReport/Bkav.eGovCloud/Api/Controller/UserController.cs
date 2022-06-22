using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Api.Controller
{
    public class UserController : EgovApiBaseController
    {
        private readonly UserBll _userService;
        private readonly DepartmentBll _departmentService;
        private readonly PositionBll _positionService;
        private readonly JobTitlesBll _jobTitlesService;
        private readonly AuthenticationSettings _authenticationSettings;

        private const int CACHE_TIME = 2 * 60; // 2 phút

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public UserController()
        {
            _userService = DependencyResolver.Current.GetService<UserBll>();
            _departmentService = DependencyResolver.Current.GetService<DepartmentBll>();
            _positionService = DependencyResolver.Current.GetService<PositionBll>();
            _jobTitlesService = DependencyResolver.Current.GetService<JobTitlesBll>();
            _authenticationSettings = DependencyResolver.Current.GetService<AuthenticationSettings>();
        }

        public bool ChangePassword(string userName, string password, string token)
        {
            if (!IsValidToken(token))
            {
                return false;
            }

            var userNameEmailDomain = userName;
            if (!userNameEmailDomain.Contains("@"))
            {
                userNameEmailDomain += "@" + _authenticationSettings.DefaultDomain;
            }

            try
            {
                var user = _userService.Get(userNameEmailDomain);
                var newSalt = Generate.GenerateRandomBytes(Generate.PasswordSaltLength);
                var newHash = Generate.GetInputPasswordHash(password, newSalt);
                user.PasswordHash = newHash;
                user.PasswordSalt = newSalt;

                _userService.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Trả về thông tin người dùng
        /// </summary>
        /// <param name="username">username người dùng</param>
        /// <returns></returns>
        public User GetUserProfile(string username)
        {
            var userNameEmailDomain = username;
            if (!username.Contains("@"))
            {
                userNameEmailDomain = username + "@" + _authenticationSettings.DefaultDomain;
            }

            var user = _userService.Get(userNameEmailDomain);
            return new User()
            {
                Username = user.Username,
                FullName = user.FullName,
                Address = user.Address,
                Gender = user.Gender,
                Phone = user.Phone,
                Fax = user.Fax
            };
        }

        /// <summary>
        /// Lấy ra danh sách phòng ban
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = CACHE_TIME)]
        public IEnumerable<dynamic> GetAllDepartment()
        {
            var result = _departmentService
                .GetCacheAllDepartments(true)
                .Select(u => new
                {
                    value = u.DepartmentId,
                    parentid = u.ParentId.HasValue ? u.ParentId : 0,
                    data = u.DepartmentName,
                    metadata = new { id = u.DepartmentId },
                    idext = u.DepartmentIdExt,
                    state = "leaf",
                    order = u.Order,
                    level = u.Level,
                    attr = new { id = u.DepartmentId, rel = "dept", idext = u.DepartmentIdExt, label = u.DepartmentPath },
                    label = u.DepartmentPath
                });
            return result;
        }

        /// <summary>
        ///   Trả về danh sách user trong cơ quan
        /// </summary>
        /// <returns>Json object danh sách tất cả user.</returns>
        [OutputCache(Duration = CACHE_TIME)]
        public IEnumerable<dynamic> GetAllUsers()
        {
            var result = _userService.GetAllCached(true)
                .Select(u => new
                {
                    value = u.UserId,
                    label = u.Username + " - " + u.FullName,
                    fullname = u.FullName,
                    username = u.Username,
                    userNameDomain = u.UsernameEmailDomain
                })
                .OrderBy(u => u.username);
            return result;
        }

        /// <summary>
        /// Lấy ra danh sách chức vụ
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = CACHE_TIME)]
        public IEnumerable<dynamic> GetAllPositions()
        {
            var result = _positionService.GetCacheAllPosition().Select(
                    u => new { positionId = u.PositionId, positionName = u.PositionName, priorityLevel = u.PriorityLevel });
            return result;
        }

        /// <summary>
        /// Lấy ra danh sách chức danh
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = CACHE_TIME)]
        public IEnumerable<dynamic> GetAllJobTitles()
        {
            var result = _jobTitlesService.GetCacheAllJobtitles()
                .Select(u => new { jobTitlesId = u.JobTitlesId, jobTitlesName = u.JobTitlesName, priorityLevel = u.PriorityLevel });
            return result;
        }

        /// <summary>
        /// Trả về danh sách tất cả các quan hệ người dùng - phòng ban - chức vụ.
        /// </summary>
        /// <returns>Json object danh sách quan hệ người dùng - phòng ban - chức vụ.</returns>
        [OutputCache(Duration = CACHE_TIME)]
        public IEnumerable<dynamic> GetAllUserDepartmentJobTitlesPosition()
        {
            var result = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition().Select(
                t =>
                    new
                    {
                        departmentid = t.DepartmentId,
                        userid = t.UserId,
                        positionid = t.PositionId,
                        idext = t.DepartmentIdExt,
                        jobtitleid = t.JobTitlesId
                    });
            return result;
        }

        /// <summary>
        /// Lấy ra các nhân viên cấp dưới của user được truyền vào
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private string GetSubordinate(int userId = 0)
        {
            if (userId != 0)
            {
                var boss = _userService.GetFromCache(userId);
                if (boss != null)
                {
                    var department = _departmentService.Get(boss.UserId);
                    var userDeptPositions = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
                    var userIds = _userService.GetNhanVienCapDuoi(userId, userDeptPositions, _positionService.GetCacheAllPosition());
                    if (userIds.Count() > 0)
                    {
                        var result = _userService.Gets(userIds).Select(t => new
                        {
                            UserId = t.UserId,
                            UserName = t.Username,
                            BossId = boss.UserId,
                            BossName = boss.Username,
                        });
                        return JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        return "[]";
                    }

                }
            }
            return "lỗi rồi";
        }

        public dynamic GetBindTreeDepart()
        {
            var depts = _departmentService.GetCacheAllDepartments(true);
            var userDepts = _departmentService.GetCacheAllUserDepartmentJobTitlesPosition();
            var users = _userService.GetAllCached();
            var results = depts.Select(u =>
            {
                var currentDept = userDepts.Where(ud => ud.DepartmentId == u.DepartmentId);

                return new
                {
                    DepartmentId = u.DepartmentId,
                    ParentId = u.ParentId,
                    DepartmentName = u.DepartmentName,
                    DepartmentIdExt = u.DepartmentIdExt,
                    DepartmentPath = u.DepartmentPath,
                    Order = u.Order,
                    Level = u.Level,
                    Users = currentDept.Select(x =>
                    {
                        var user = users.Where(us => us.UserId == x.UserId).FirstOrDefault();
                        return new
                        {
                            UserId = user.UserId,
                            Username = user != null ? user.Username : "",
                            FullName = user != null ? user.FullName : ""
                        };
                    })
                };
            });

            return results;
        }

        private bool IsValidToken(string token)
        {
            return token.Equals("1659433a-0ed4-44c6-ad16-7a72a86ffb87");
        }
    }
}