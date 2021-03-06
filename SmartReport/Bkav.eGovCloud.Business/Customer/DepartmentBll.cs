using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Business.Common;
using System.Data.SqlClient;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : DepartmentBll - public - BLL</para>
    /// <para>Access Modifiers:</para> 
    /// <para>Create Date : 270812</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Modify Date: 080912</para>
    /// <para>Modifier: GiangPN</para>
    /// <para>Description : BLL tương ứng với bảng Department trong CSDL</para>
    /// </summary>
    public class DepartmentBll : ServiceBase
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<JobTitles> _jobTitlesRepository;
        private readonly IRepository<Position> _positionRepository;
        private readonly IRepository<Store> _storeRepository;
        private readonly IRepository<UserDepartmentJobTitlesPosition> _userDepartmentJobTitlesPositionRepository;
        private readonly UserBll _userService;
        private readonly ResourceBll _resourceService;
        private readonly MemoryCacheManager _cacheManager;
        private readonly IRepository<PermissionSetting> _permissionSettingRepository;
        private readonly DepartmentBll _departmentService;
        private readonly AdminGeneralSettings _generalSettings;

        /// <summary>
        /// 
        /// </summary>
        public ReportBll ReportService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProcessFunctionBll ProcessFunctionService { get; set; }

        /// <summary>
        /// Khởi tạo class <see cref="DepartmentBll"/>.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="userService">Bll tương ứng với bảng User trong CSDL</param>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="generalSettings"></param>
        //TODO: Anh GiangPN - Tham số đầu vào IPositionDal positionDal nên thay thành PositionBll và IJobTitlesDal jobTitlesDal nên thay thành JobTitleBll
        public DepartmentBll(IDbCustomerContext context, UserBll userService, MemoryCacheManager cacheManager,
                    AdminGeneralSettings generalSettings)
            : base(context)
        {
            _departmentRepository = Context.GetRepository<Department>();
            _userService = userService;
            _userDepartmentJobTitlesPositionRepository = Context.GetRepository<UserDepartmentJobTitlesPosition>();
            _jobTitlesRepository = Context.GetRepository<JobTitles>();
            _positionRepository = Context.GetRepository<Position>();
            _cacheManager = cacheManager;
            _storeRepository = Context.GetRepository<Store>();
            _permissionSettingRepository = Context.GetRepository<PermissionSetting>();
            _generalSettings = generalSettings;
        }

        /// <summary>
        /// Lấy ra một phòng ban
        /// </summary>
        /// <param name="departmentId">Id của phòng ban</param>
        /// <returns>Entity phòng ban</returns>
        public Department Get(int departmentId)
        {
            Department department = null;
            if (departmentId > 0)
            {
                department = _departmentRepository.Get(departmentId);
            }
            return department;
        }

        /// <summary>
        /// Lấy ra một phòng ban
        /// </summary>
        /// <param name="departmentId">Id của phòng ban</param>
        /// <returns>Entity phòng ban</returns>
        public Department GetByID(int? departmentId)
        {
            Department department = null;
            if (departmentId > 0)
            {
                department = _departmentRepository.Get(departmentId);
            }
            return department;
        }

        public Department GetName(string name)
        {
            Department department = null;
            if (name != "")
            {
                department = _departmentRepository.Get(false, d => d.DepartmentName == name);
            }
            return department;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Department> GetReadOnlys()
        {
            return _departmentRepository.GetsReadOnly(p => p.IsActivated);
        }

        /// <summary>
        /// Lấy ra tất cả các loại hồ sơ. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..
        /// </summary>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="isActivated">Trạng thái phòng ban</param>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <returns>Danh sách các thực thể được ánh xạ</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Department, TOutput>> projector, bool? isActivated = null)
        {
            return _departmentRepository.GetsAs(projector, DepartmentQuery.WithIsActivated(isActivated));
        }


        public IEnumerable<UserDepartmentJobTitlesPositionCached> GetTreeParentDepartment()
        {
            var listDepart = _departmentRepository.GetsReadOnly(d => d.IsActivated == true);
            var result = new List<UserDepartmentJobTitlesPositionCached>();
            foreach (var item in listDepart)
            {
                result.Add(new UserDepartmentJobTitlesPositionCached()
                {
                    DepartmentId = item.DepartmentId,
                    DepartmentName = item.DepartmentName,
                    ParentId = item.ParentId,
                    Order = item.Order,
                    Level = item.Level
                });
            }
            return result;
        }

        ///<summary>
        /// Lấy ra tất cả các phòng ban user, chức danh, chức vụ Id (lấy ra từ cache)
        ///</summary>
        ///<returns></returns>
        public IEnumerable<UserDepartmentJobTitlesPositionCached> GetCacheAllUserDepartmentJobTitlesPosition()
        {
            var deparments = GetCacheAllDepartments();
            var allUser = _userService.GetAllCached(true);

            var alUserDepartmentJobTitlesPosition = _cacheManager.Get(CacheParam.UserDepartmentJobtitlePositionAllKey,
                CacheParam.UserDepartmentJobtitlePositionAllCacheTimeOut, () =>
                    {
                        var result = new List<UserDepartmentJobTitlesPositionCached>();

                        var allUserDeptJobs = _userDepartmentJobTitlesPositionRepository.GetsReadOnly();

                        foreach (var item in allUserDeptJobs)
                        {
                            var cacheItem = AutoMapper.Mapper.Map<UserDepartmentJobTitlesPosition, UserDepartmentJobTitlesPositionCached>(item);
                            var department = deparments.SingleOrDefault(d => d.DepartmentId == item.DepartmentId);
                            cacheItem.DepartmentName = department == null ? "" : department.DepartmentName;
                            cacheItem.Emails = department == null ? "" : department.Emails;
                            var user = allUser.SingleOrDefault(u => u.UserId == cacheItem.UserId);
                            cacheItem.Username = user == null ? "" : user.Username;
                            cacheItem.UserFullName = user == null ? "" : user.FullName;
                            cacheItem.ParentId = department.ParentId;
                            cacheItem.Level = department.Level;
                            cacheItem.Order = department.Order;
                            result.Add(cacheItem);
                        }
                        return result;
                    });

            return alUserDepartmentJobTitlesPosition;
        }

        ///<summary>
        /// Kiểm tra xem người dùng có phải là quản trị của phòng ban hay không
        ///</summary>
        /// <param name="userId">Id người dùng</param>
        ///<returns></returns>
        public bool IsAdminDepartment(int userId)
        {
            return GetCacheAllUserDepartmentJobTitlesPosition().Any(u => u.UserId == userId && u.IsAdmin);
        }

        ///<summary>
        /// Lấy ra các phòng ban mà người dùng có quyền quản lý
        ///</summary>
        /// <param name="userId">Id người dùng</param>
        ///<returns></returns>
        public IEnumerable<DepartmentCached> GetAllDepartmentUserAccess(int userId)
        {
            var departmentAccess = GetCacheAllUserDepartmentJobTitlesPosition().Where(u => u.UserId == userId && u.IsAdmin).ToList();
            if (!departmentAccess.Any())
            {
                return new List<DepartmentCached>();
            }

            var departmentUserIds = new List<int>();
            var allDepartment = GetCacheAllDepartments();

            foreach (var userDepartment in departmentAccess)
            {
                var ext = userDepartment.DepartmentIdExt + ".";
                var department = userDepartment;
                var departments = allDepartment.Where(d => d.DepartmentId == department.DepartmentId || d.DepartmentIdExt.StartsWith(ext));
                if (departments.Any())
                {
                    departmentUserIds.AddRange(departments.Select(d => d.DepartmentId));
                }
            }

            var distinctDepartmentId = departmentUserIds.Distinct();

            return allDepartment.Where(d => distinctDepartmentId.Contains(d.DepartmentId));
        }

        /// <summary>
        /// Lấy ra cấp lớn nhất của phòng ban.
        /// </summary>
        /// <returns>Cấp phòng ban</returns>
        public int GetMaxLevel()
        {
            var maxLevel = 0;
            var listLevel = _departmentRepository.GetsAs(t => t.Level).Distinct();
            if (listLevel.Any())
            {
                maxLevel = listLevel.Max();
            }
            return maxLevel;
        }

        /// <summary>
        /// Lấy ra danh sách tất cả các người dùng (Danh sách này sẽ được cache lại trong 60 phút)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentCached> GetCacheAllDepartments(bool? isActivated = null)
        {
            var allDepartments = _cacheManager.Get(CacheParam.DepartmentAllKey, CacheParam.DepartmentAllCacheTimeOut, () =>
            {
                var result = _departmentRepository.GetsReadOnly(null, Context.Filters.Sort<Department, string>(ti => ti.DepartmentPath));
                var cached = AutoMapper.Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentCached>>(result);
                foreach (var cacheItem in cached)
                {
                    cacheItem.DepartmentLabel = MapDepartmentName(cacheItem.DepartmentPath);
                }

                return cached;
            });

            return allDepartments.Where(d => !isActivated.HasValue || d.IsActivated == isActivated);
        }

        /// <summary>
        /// Trả về danh sách phòng ban của user.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        public IEnumerable<string> GetsPath(int userId)
        {
            var result = new List<string>();
            var userDepartments = GetCacheAllUserDepartmentJobTitlesPosition().Where(d => d.UserId == userId).OrderBy(d => d.IsPrimary);
            if (userDepartments != null && userDepartments.Any())
            {
                var allDepartment = GetCacheAllDepartments();
                foreach (var ud in userDepartments)
                {
                    var dept = allDepartment.SingleOrDefault(d => d.DepartmentId == ud.DepartmentId);
                    if (dept != null)
                    {
                        result.Add(MapDepartmentName(dept.DepartmentPath));
                    }
                }
            }
            result.Reverse();

            return result;
        }

        /// <summary>
        /// Trả về danh sách phòng ban của user.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        public IEnumerable<DepartmentCached> GetsCurrentDepartEdoc(int userId)
        {
            var result = new List<DepartmentCached>();
            var userDepartments = GetCacheAllUserDepartmentJobTitlesPosition().Where(d => d.UserId == userId).OrderBy(d => d.IsPrimary);
            if (userDepartments != null && userDepartments.Any())
            {
                var allDepartment = GetCacheAllDepartments();
                foreach (var ud in userDepartments)
                {
                    var dept = allDepartment.SingleOrDefault(d => d.DepartmentId == ud.DepartmentId);
                    if (dept != null)
                    {
                        result.Add(dept);
                    }
                }
            }
            result.Reverse();

            return result;
        }

        /// <summary>
        /// Trả về phòng ban của user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<DepartmentCached> GetsCurrentDepartEdocFirst(int userId)
        {
            var result = new List<DepartmentCached>(); 
            var userDepartments = GetCacheAllUserDepartmentJobTitlesPosition().Where(d => d.UserId == userId).OrderBy(d => d.IsPrimary);
            if (userDepartments != null && userDepartments.Any())
            {
                var allDepartment = GetCacheAllDepartments();
                foreach (var ud in userDepartments)
                {
                    var dept = allDepartment.SingleOrDefault(d => d.DepartmentId == ud.DepartmentId);
                    if (dept != null)
                    {
                        result.Add(dept);
                    }
                }
            }
            result.Reverse();

            return result;
        }

        /// <summary>
        /// Trả về danh sách id ext phòng ban của user.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        public IEnumerable<string> GetsDepartmentExts(int userId)
        {
            var result = new List<string>();
            var userDepartments = GetCacheAllUserDepartmentJobTitlesPosition().Where(d => d.UserId == userId).OrderBy(d => d.IsPrimary);
            if (userDepartments != null && userDepartments.Any())
            {
                var allDepartment = GetCacheAllDepartments();
                foreach (var ud in userDepartments)
                {
                    var dept = allDepartment.SingleOrDefault(d => d.DepartmentId == ud.DepartmentId);
                    if (dept != null)
                    {
                        result.Add(dept.DepartmentIdExt);
                    }
                }
            }

            result.Reverse();

            return result;
        }

        /// <summary>
        /// Tra ve phong ban rooot
        /// </summary>
        /// <returns></returns>
        public Department GetRoot()
        {
            return _departmentRepository.Get(true, x => !x.ParentId.HasValue);
        }

        /// <summary>
        /// Trả về phòng ban mặc định của user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetPrimaryDepartmentName(int userId)
        {
            var primaryDepartment = GetPrimaryDepartment(userId);
            return primaryDepartment == null ? "" : primaryDepartment.DepartmentName;
        }

        /// <summary>
        /// Trả về phòng ban mặc định của người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DepartmentCached GetPrimaryDepartment(int userId)
        {
            DepartmentCached result = null;
            var userDepartments = GetCacheAllUserDepartmentJobTitlesPosition().Where(d => d.UserId == userId);
            if (userDepartments != null && userDepartments.Any())
            {
                var mainUserDepartment = userDepartments.OrderByDescending(x => x.IsPrimary).First();
                var allDepartment = GetCacheAllDepartments();
                result = allDepartment.SingleOrDefault(d => d.DepartmentId == mainUserDepartment.DepartmentId);
            }

            return result;
        }

        /// <summary>
        /// Trả về DepartmentPath của rootDepartment
        /// </summary>
        /// <returns></returns>
        public string GetRootDepartmentPath()
        {
            var rootDepartment = GetRoot();

            return rootDepartment != null ? rootDepartment.DepartmentPath : "";
        }

        /// <summary>
        /// Lấy ra các phòng ban cấp dưới theo phòng ban cấp cha.
        /// </summary>
        /// <param name="parentId">Id của phòng ban cấp cha</param>
        /// <param name="isActivated">Trạng thái kích hoạt</param>
        /// <returns>Danh sách phòng ban</returns>
        public IEnumerable<Department> GetChildrens(int parentId, bool? isActivated = null)
        {
            var spec = DepartmentQuery.WithParentId(parentId).And(DepartmentQuery.WithIsActivated(isActivated));
            var sort = Context.Filters.Sort<Department, string>(ti => ti.DepartmentName);
            return _departmentRepository.Gets(false, spec, sort);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="target"></param>
        /// <param name="position"></param>
        /// <param name="parentid"></param>
        public void MoveDepartment(int id, int target, string position, int parentid)
        {
            var childOfRoot = GetChildrens(parentid).OrderBy(t => t.Order);
            var departmentMove = childOfRoot.SingleOrDefault(t => t.DepartmentId == id);
            var departmentTarget = childOfRoot.SingleOrDefault(t => t.DepartmentId == target);
            if (departmentMove != null && departmentTarget != null)
            {
                var orderMoveValue = departmentMove.Order;
                var orderTargetValue = departmentTarget.Order;
                if (orderMoveValue < orderTargetValue)
                {
                    var departmentOrder = childOfRoot.Where(t => t.Order > orderMoveValue && t.Order < orderTargetValue && t.DepartmentId != id && t.DepartmentId != target);
                    foreach (var department in departmentOrder)
                    {
                        department.Order = department.Order - 1;
                    }
                    if (position == "before")
                    {
                        departmentMove.Order = orderTargetValue - 1;
                    }
                    if (position == "after")
                    {
                        departmentMove.Order = orderTargetValue;
                        departmentTarget.Order = orderTargetValue - 1;
                    }
                }
                else
                {
                    var departmentOrder = childOfRoot.Where(t => t.Order > orderTargetValue && t.Order < orderMoveValue && t.DepartmentId != id && t.DepartmentId != target);
                    foreach (var department in departmentOrder)
                    {
                        department.Order = department.Order + 1;
                    }
                    if (position == "before")
                    {
                        departmentMove.Order = orderTargetValue;
                        departmentTarget.Order = orderTargetValue + 1;
                    }
                    if (position == "after")
                    {
                        departmentMove.Order = orderTargetValue + 1;
                    }

                }
                Context.SaveChanges();
                _cacheManager.Remove(CacheParam.DepartmentAllKey);
            }
        }

        /// <summary>
        /// Thêm mới phòng ban
        /// </summary>
        /// <param name="department">Thực thể phòng ban</param>
        /// <returns></returns>
        public void Create(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException("department");
            }
            var departmentIdExt = string.Empty;
            if (department.ParentId != null)
            {
                var parent = Get(department.ParentId.Value);
                if (parent == null)
                {
                    throw new EgovException(string.Format("Phòng ban ({0}) đã tồn tại!", department.DepartmentName));
                }
                departmentIdExt = parent.DepartmentIdExt + ".";
                department.DepartmentPath = parent.DepartmentPath + "\\" + department.DepartmentName;
                department.Level = parent.Level + 1;
                department.IsActivated = parent.IsActivated;
            }
            else
            {
                department.DepartmentPath = "\\" + department.DepartmentName;
                department.IsActivated = true;
            }
            _departmentRepository.Create(department);
            Context.SaveChanges();
            department.DepartmentIdExt = departmentIdExt + department.DepartmentId;
            Context.SaveChanges();

            _cacheManager.Remove(CacheParam.DepartmentAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }
        public class DepartmentPath
        {
            public int id;
            public string name;
        }
        public void CreateByAPI(Department department)
        {
            string departmentPath = "\\" + department.DepartmentPath;
            var exist = _departmentRepository.GetsAs(p => p.DepartmentPath, p => departmentPath.Equals(p.DepartmentPath));
            if (department == null)
            {
                throw new ArgumentNullException("department");
            }
            if(exist.Count() == 0)
            {
                var departmentIdExt = string.Empty;
                department.DepartmentPath = departmentPath;

                _departmentRepository.Create(department);
                Context.SaveChanges();
                List<DepartmentPath> lstDeP = new List<DepartmentPath>();

                //handle departmentIDExt
                var lstPath = department.DepartmentPath.Split('\\');
                foreach (var lstP in lstPath)
                {
                    DepartmentPath de = new DepartmentPath();
                    var resID = getIDbyName(lstP);
                    foreach (var res in resID)
                    {
                        de.id = res.DepartmentId;
                        de.name = res.DepartmentName;
                    }

                    lstDeP.Add(de);
                }
                var lstDePExists = lstDeP.Distinct();
                string _idExt = "";
                int? _parentId = 0;

                foreach (var i in lstDePExists)
                {
                    string id = i.id.ToString();
                    string name = i.name;
                    foreach (var lstP in lstPath)
                    {
                        if (lstP == name)
                        {
                            _idExt += id + ".";
                        }
                    }
                    List<int> count = GetPositions(department.DepartmentPath, '\\');
                    if (count.Count == 1)
                    {
                        _parentId = null;
                    }
                    else
                    {

                        int start = count[count.Count - 2] + 1;
                        int end = count[count.Count - 1];
                        string _name = department.DepartmentPath.Substring(start, end - start);
                        if (name == _name)
                        {
                            _parentId = i.id;
                        }
                    }

                }

                department.ParentId = _parentId;
                department.DepartmentIdExt = _idExt.Remove(_idExt.Length - 1, 1);
                Context.SaveChanges();
            }


            _cacheManager.Remove(CacheParam.DepartmentAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }
        /// <summary>
        /// Thêm mới phòng ban
        /// </summary>
        /// <param name="departments">Thực thể phòng ban</param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public void Create(IEnumerable<Department> departments, int? parentId = null)
        {
            if (departments == null || !departments.Any())
            {
                throw new ArgumentNullException("department");
            }

            var departmentIdExt = string.Empty;
            if (parentId.HasValue)
            {
                var parent = Get(parentId.Value);
                if (parent == null)
                {
                    throw new EgovException(string.Format("Phòng ban cha không tồn tại!"));
                }

                departmentIdExt = parent.DepartmentIdExt + ".";
                foreach (var department in departments)
                {
                    department.ParentId = parentId.Value;
                    department.DepartmentPath = parent.DepartmentPath + "\\" + department.DepartmentName;
                    department.Level = parent.Level + 1;
                    department.IsActivated = parent.IsActivated;
                    _departmentRepository.Create(department);
                }
            }
            else
            {
                foreach (var department in departments)
                {
                    department.DepartmentPath = "\\" + department.DepartmentName;
                    department.IsActivated = true;
                    _departmentRepository.Create(department);
                }
            }

            Context.SaveChanges();
            foreach (var department in departments)
            {
                department.DepartmentIdExt = departmentIdExt + department.DepartmentId;
            }
            Context.SaveChanges();

            _cacheManager.Remove(CacheParam.DepartmentAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        /// <summary>
        /// Lay ra EdocId
        /// </summary>
        /// <param name="departmentId"></param>
        public string GetEdocId(int departmentId)
        {
            var userDeparts = GetCacheAllUserDepartmentJobTitlesPosition().Where(ud => ud.DepartmentId == departmentId);
            if (userDeparts == null || userDeparts.Count() == 0)
            {
                return "";
            }

            return userDeparts.First().EdocId;
        }
		
        /// <summary>
        /// Cập nhật thông tin phòng ban
        /// </summary>
        /// <param name="department">Entity phòng ban</param>
        /// <param name="oldDepartmentName">Tên phòng ban trước khi cập nhật</param>
        /// <param name="oldIsActivated">Trạng thái kích hoạt của phòng ban trước khi cập nhật</param>
        /// <param name="edocId">Trạng thái kích hoạt của phòng ban trước khi cập nhật</param>
        public void Update(Department department, string oldDepartmentName, bool oldIsActivated, string edocId = "")
        {
            if (department == null)
            {
                throw new ArgumentNullException("department");
            }

            //if (department.ParentId != null)
            //{
            //    if (_departmentRepository.Exist(DepartmentQuery.WithParentId(department.ParentId.Value).And(DepartmentQuery.WithDepartmentName(department.DepartmentName)).And(r => r.DepartmentName.ToLower() != oldDepartmentName.ToLower())))
            //    {
            //        throw new EgovException(string.Format("Phòng ban ({0}) đã tồn tại!", department.DepartmentName));
            //    }
            //}

            //Cập nhật user, chức vụ vào phòng ban
            //Nếu trạng thái kích hoạt thay đổi: nếu IsActivated = false sẽ cập nhật tất cả các phòng ban con về trạng thái kích hoạt = false.
            //Nếu IsActivated = true, sẽ cập nhật các phòng ban cấp cha(đệ quy) có trạng thái kích hoạt = true.
            if (department.IsActivated != oldIsActivated || department.DepartmentName != oldDepartmentName)
            {
                var children =
                    _departmentRepository.Gets(false, DepartmentQuery.ContainsDepartmentIdExt(department.DepartmentIdExt + "."));
                if (department.IsActivated != oldIsActivated)
                {
                    foreach (var child in children)
                    {
                        child.IsActivated = department.IsActivated;
                    }
                }
                if (department.DepartmentName != oldDepartmentName)
                {
                    department.DepartmentPath = department.DepartmentPath.Substring(0, department.DepartmentPath.LastIndexOf('\\') + 1) + department.DepartmentName;
                    foreach (var child in children)
                    {
                        var split = child.DepartmentPath.Split('\\');
                        var index = department.Level + 1;
                        if (split.Length > index)
                        {
                            split[index] = department.DepartmentName;
                            child.DepartmentPath = string.Join("\\", split);
                        }
                    }
                }

                if (department.IsActivated)
                {
                    var parentIds = department.DepartmentIdExt.Split('.').Select(int.Parse).ToList();
                    parentIds.RemoveAt(parentIds.Count - 1);
                    if (parentIds.Any())
                    {
                        var parents = _departmentRepository.Gets(false, d => parentIds.Contains(d.DepartmentId));
                        foreach (var parent in parents)
                        {
                            parent.IsActivated = true;
                        }
                    }
                }
            }

            var listUserJobTitlesPosition = new List<UserDepartmentJobTitlesPosition>();
            if (department.UserJobTitlesPositionIds != null && department.UserJobTitlesPositionIds.Count > 0)
            {
                var allUserIds = _userService.GetAllUserIds(true);
                var allJobTitlesIds = _jobTitlesRepository.GetsAs(p => p.JobTitlesId);
                var allPositionIds = _positionRepository.GetsAs(p => p.PositionId);
                foreach (var userJobTitlesPositionId in department.UserJobTitlesPositionIds)
                {
                    var split = userJobTitlesPositionId.Split(',');
                    if (split.Length != 6)
                    {
                        continue;
                    }
                    int userParsed, jobTitlesParsed, positionParsed;
                    if (int.TryParse(split[0], out userParsed)
                       && int.TryParse(split[1], out jobTitlesParsed)
                        && int.TryParse(split[2], out positionParsed))
                    {
                        if (allUserIds.Any(did => did == userParsed)
                            && allJobTitlesIds.Any(jid => jid == jobTitlesParsed)
                            && allPositionIds.Any(pid => pid == positionParsed))
                        {
                        }

                        var isPrimary = false;
                        bool.TryParse(split[3], out isPrimary);

                        var isAdmin = false;
                        bool.TryParse(split[4], out isAdmin);

                        var hasReceiveDocument = false;
                        bool.TryParse(split[5], out hasReceiveDocument);

                        listUserJobTitlesPosition.Add(new UserDepartmentJobTitlesPosition
                            {
                                UserId = userParsed,
                                DepartmentId = department.DepartmentId,
                                JobTitlesId = jobTitlesParsed,
                                PositionId = positionParsed,
                                IsPrimary = isPrimary,
                                IsAdmin = isAdmin,
                                HasReceiveDocument = hasReceiveDocument,
                                EdocId = edocId,
                                DepartmentIdExt = department.DepartmentIdExt
                            });
                    }
                }
            }
            var currentDepartmentJobTitles = _userDepartmentJobTitlesPositionRepository.Gets(false, udp => udp.DepartmentId == department.DepartmentId);
            if (listUserJobTitlesPosition.Count > 0)
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (var userDepartmentJobTitlesPosition in currentDepartmentJobTitles)
                {
                    _userDepartmentJobTitlesPositionRepository.Delete(userDepartmentJobTitlesPosition);
                }
                foreach (var userJobTitlesPosition in listUserJobTitlesPosition)
                {
                    _userDepartmentJobTitlesPositionRepository.Create(userJobTitlesPosition);
                }
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
            else
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (var userDepartmentJobTitlesPosition in currentDepartmentJobTitles)
                {
                    _userDepartmentJobTitlesPositionRepository.Delete(userDepartmentJobTitlesPosition);
                }
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
            Context.SaveChanges();
            _cacheManager.Remove(CacheParam.DepartmentAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        /// <summary>
        /// Cập nhật thông tin phòng ban
        /// </summary>
        /// <param name="department"></param>
        /// <param name="users"></param>
        /// <param name="pos"></param>
        /// <param name="job"></param>
        public void Update(Department department, IEnumerable<UserCached> users, Position pos, JobTitles job)
        {
            #region valide input

            if (department == null)
            {
                throw new ArgumentNullException("department");
            }

            if (users == null || !users.Any())
            {
                throw new ArgumentNullException("users");
            }

            if (pos == null)
            {
                throw new ArgumentNullException("pos");
            }

            if (job == null)
            {
                throw new ArgumentNullException("job");
            }

            #endregion

            var listUserJobTitlesPosition = new List<UserDepartmentJobTitlesPosition>();
            foreach (var user in users)
            {
                listUserJobTitlesPosition.Add(new UserDepartmentJobTitlesPosition
                {
                    UserId = user.UserId,
                    DepartmentId = department.DepartmentId,
                    JobTitlesId = job.JobTitlesId,
                    PositionId = pos.PositionId,
                    IsPrimary = false,
                    IsAdmin = false,
                    HasReceiveDocument = false,
                    DepartmentIdExt = department.DepartmentIdExt
                });
            }
            var userIds = users.Select(p => p.UserId);
            var currentDepartmentJobTitles = _userDepartmentJobTitlesPositionRepository.Gets(
                false,
                udp => udp.DepartmentId == department.DepartmentId
                    && userIds.Contains(udp.UserId)
                );
            Context.Configuration.AutoDetectChangesEnabled = false;

            foreach (var userDepartmentJobTitlesPosition in currentDepartmentJobTitles)
            {
                _userDepartmentJobTitlesPositionRepository.Delete(userDepartmentJobTitlesPosition);
            }

            foreach (var userJobTitlesPosition in listUserJobTitlesPosition)
            {
                _userDepartmentJobTitlesPositionRepository.Create(userJobTitlesPosition);
            }

            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();

            _cacheManager.Remove(CacheParam.DepartmentAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        /// <summary>
        /// Cập nhật thông tin phòng ban
        /// </summary>
        /// <param name="department"></param>
        /// <param name="users"></param>
        /// <param name="pos"></param>
        public void Update(Department department, IEnumerable<UserCached> users, Position pos)
        {
            #region valide input

            if (department == null)
            {
                throw new ArgumentNullException("department");
            }

            if (users == null || !users.Any())
            {
                throw new ArgumentNullException("users");
            }

            if (pos == null)
            {
                throw new ArgumentNullException("pos");
            }

            #endregion

            var userIds = users.Select(p => p.UserId);
            var currentDepartmentJobTitles = _userDepartmentJobTitlesPositionRepository.Gets(
                false,
                udp => udp.DepartmentId == department.DepartmentId
                    && userIds.Contains(udp.UserId)
                );

            Context.Configuration.AutoDetectChangesEnabled = false;

            foreach (var userDepartmentJobTitlesPosition in currentDepartmentJobTitles)
            {
                userDepartmentJobTitlesPosition.PositionId = pos.PositionId;
            }

            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();

            _cacheManager.Remove(CacheParam.DepartmentAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="users"></param>
        /// <param name="job"></param>
        public void Update(Department department, IEnumerable<UserCached> users, JobTitles job)
        {
            #region valide input

            if (department == null)
            {
                throw new ArgumentNullException("department");
            }

            if (users == null || !users.Any())
            {
                throw new ArgumentNullException("users");
            }

            if (job == null)
            {
                throw new ArgumentNullException("job");
            }

            #endregion

            var userIds = users.Select(p => p.UserId);
            var currentDepartmentJobTitles = _userDepartmentJobTitlesPositionRepository.Gets(
                false,
                udp => udp.DepartmentId == department.DepartmentId
                    && userIds.Contains(udp.UserId)
                );
            Context.Configuration.AutoDetectChangesEnabled = false;

            foreach (var userDepartmentJobTitlesPosition in currentDepartmentJobTitles)
            {
                userDepartmentJobTitlesPosition.JobTitlesId = job.JobTitlesId;
            }

            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();

            _cacheManager.Remove(CacheParam.DepartmentAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        /// <summary>
        /// Xóa 1 phòng ban
        /// </summary>
        /// <param name="department">Thực thể phòng ban</param>
        public void Delete(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException("department");
            }
            var existChild = _departmentRepository.Exist(DepartmentQuery.ContainsDepartmentIdExt(department.DepartmentIdExt).And(t => t.DepartmentIdExt != department.DepartmentIdExt));

            if (existChild)
            {
                throw new EgovException("Hãy xóa phòng ban con trước.");
            }
            //TODO: TrungVH: Xóa thêm các phòng ban liên quan đến quy trình
            var reports = ReportService.Gets();
            if (reports != null && reports.Any())
            {
                foreach (var report in reports)
                {
                    if (report.ListDepartmentPositionHasPermission.Any())
                    {
                        report.ListDepartmentPositionHasPermission = report.ListDepartmentPositionHasPermission.Where(dp => dp.ContainsKey("DepartmentId"))
                                                                        .Where(dp => dp["DepartmentId"] != department.DepartmentId)
                                                                        .ToList();
                    }
                    if (report.ListPositionHasPermission.Any())
                    {
                        report.ListPositionHasPermission = report.ListPositionHasPermission.Where(p => p != department.DepartmentId).ToList();
                    }
                }
            }

            //var processFunctions = ProcessFunctionService.Gets();
            var processFunctions = _permissionSettingRepository.Gets(false);
            if (processFunctions != null && processFunctions.Any())
            {
                foreach (var processFunction in processFunctions)
                {
                    if (processFunction.ListDepartmentPositionHasPermission.Any())
                    {
                        processFunction.ListDepartmentPositionHasPermission = processFunction.ListDepartmentPositionHasPermission.Where(dp => dp.ContainsKey("DepartmentId"))
                                                                        .Where(dp => dp["DepartmentId"] != department.DepartmentId)
                                                                        .ToList();
                    }
                    if (processFunction.ListPositionHasPermission.Any())
                    {
                        processFunction.ListPositionHasPermission = processFunction.ListPositionHasPermission.Where(p => p != department.DepartmentId).ToList();
                    }
                }
            }

            var stores = _storeRepository.Gets(false, s => s.DepartmentId.HasValue && s.DepartmentId == department.DepartmentId);
            if (stores.Any())
            {
                foreach (var store in stores)
                {
                    store.DepartmentId = null;
                }
            }

            var userDepartmentJobTitlesPositions = _userDepartmentJobTitlesPositionRepository.Gets(false, x => x.DepartmentId == department.DepartmentId);
            foreach (var userDepartmentJobTitlesPosition in userDepartmentJobTitlesPositions)
            {
                _userDepartmentJobTitlesPositionRepository.Delete(userDepartmentJobTitlesPosition);
            }

            //foreach (var department1 in departmentChild)
            //{
            //    var currentDept =
            //        _userDepartmentJobTitlesPositionDal.Gets(udp => udp.DepartmentId == department1.DepartmentId);
            //    if (currentDept.Any())
            //    {
            //        checkForDelete = false;
            //        break;
            //    }
            //}
            _departmentRepository.Delete(department);
            Context.SaveChanges();
            //xoa cac bang lien quan cua Childrend
            //_departmentDal.Delete(departmentChild);

            _cacheManager.Remove(CacheParam.DepartmentAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }

        /// <summary>
        /// Trả về Tiêu chí có ID bằng với giá trị đầu vào
        /// </summary>
        /// <returns></returns>
        public Department GetbyId(int id)
        {
            return _departmentRepository.Get(id);
        }
                
        private string MapDepartmentName(string departmentPath)
        {
            var displayType = _generalSettings.ShowDepartmentType;
            var deptNames = departmentPath.Split(new char[] { '\\' });
            var count = deptNames.Count();

            switch (displayType)
            {
                case 1: // hiển thị phòng ban hiện tại
                    return deptNames[count - 1];
                case 2: // Hiển thị phòng ban hiện tại và cha của nó
                    return count > 3 
                                ? String.Format("{0}\\{1}", deptNames[count - 2], deptNames[count - 1]) 
                                : deptNames[count - 1];
                case 3: // Hiển thị đầy đủ đường dẫn
                    return departmentPath.Remove(0, 1);
                default:
                    return departmentPath.Remove(0, 1);
            }
        }
        public List<int> GetPositions(string source, char searchString)
        {
            List<int> ret = new List<int>();
            int len = 1;
            int start = -len;
            while (true)
            {
                start = source.IndexOf(searchString, start + len);
                if (start == -1)
                {
                    break;
                }
                else
                {
                    ret.Add(start);
                }
            }
            return ret;
        }
        #region TinhQuangNinh
        public void CreateSync(IList<Department> departments, bool ignoreExist)
        {
            if (departments == null || !departments.Any())
            {
                throw new ArgumentNullException("departments");
            }

            var names = departments.Select(x => x.DepartmentName);
            var exist = _departmentRepository.GetsAs(p => p.DepartmentName, p => names.Contains(p.DepartmentName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == departments.Count())
                {
                    UpdateExists();
                    throw new EgovException("Đã xong");
                }

                var list = departments.Where(p => !exist.Contains(p.DepartmentName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("Department.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                Create(departments);
            }
        }

        private void Create(IList<Department> departments)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var item in departments)
            {
                _departmentRepository.Create(item);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
            CreateSync();
            _cacheManager.Remove(CacheParam.JobtitlesAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }
        public void UpdateExists()
        {
            var result1 = GetAll();
            foreach (var itemm in result1)
            {

                var name = itemm.DepartmentPath;
                List<int> dem = GetPositions(name, '\\');
                if (dem.Count == 1 || dem.Count == 2)
                {
                    var resID = getIDbyName("Tỉnh Quảng Ninh");
                    if (dem.Count == 1)
                    {
                        foreach (var id in resID)
                        {
                            string _departmentidExt = id.DepartmentId.ToString();
                            UpdateSync(null, _departmentidExt, itemm.DepartmentId, DateTime.Now);
                        }
                    }
                    else
                    {
                        foreach (var id in resID)
                        {
                            string _departmentidExt = id.DepartmentId.ToString() + "." + itemm.DepartmentId.ToString();
                            UpdateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId, DateTime.Now);
                        }
                    }
                }
                else
                {
                    if (dem.Count == 3)
                    {
                        int start = dem[dem.Count - 2] + 1;
                        int end = dem[dem.Count - 1];
                        string _name = name.Substring(start, end - start);
                        var resID = getIDbyName(_name);
                        foreach (var id in resID)
                        {
                            string _departmentidExt =
                                id.ParentId.ToString() + "." + id.DepartmentId.ToString() +
                                "." + itemm.DepartmentId.ToString();
                            UpdateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId, DateTime.Now);
                        }
                    }
                    else if (dem.Count == 4)
                    {
                        int start = dem[dem.Count - 2] + 1;
                        int end = dem[dem.Count - 1];
                        string _name = name.Substring(start, end - start);
                        var resID = getIDbyName(_name);
                        foreach (var id in resID)
                        {
                            var result = getIDbyParentID(id.ParentId);
                            foreach (var re in result)
                            {
                                string _departmentidExt =
                                re.ParentId.ToString() + "." + id.ParentId.ToString() + "." +
                                id.DepartmentId.ToString() + "." + itemm.DepartmentId.ToString();
                                UpdateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId, DateTime.Now);
                            }
                        }
                    }
                    else
                    {
                        int start = dem[dem.Count - 2] + 1;
                        int end = dem[dem.Count - 1];
                        string _name = name.Substring(start, end - start);
                        var resID = getIDbyName(_name);
                        foreach (var id in resID)
                        {
                            var result = getIDbyParentID(id.ParentId);
                            foreach (var re in result)
                            {
                                var resQNID = getIDbyName("Tỉnh Quảng Ninh");
                                foreach (var t in resQNID)
                                {
                                    string _departmentidExt =
                                t.DepartmentId.ToString() + "." + re.ParentId.ToString() + "." +
                                id.ParentId.ToString() + "." + id.DepartmentId.ToString() + "." +
                                itemm.DepartmentId.ToString();
                                    UpdateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId, DateTime.Now);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void CreateSync()
        {
            var result1 = GetAll();
            foreach (var itemm in result1)
            {

                var name = itemm.DepartmentPath;
                List<int> dem = GetPositions(name, '\\');
                if (dem.Count == 1 || dem.Count == 2)
                {
                    var resID = getIDbyName("Tỉnh Quảng Ninh");
                    if (dem.Count == 1)
                    {
                        foreach (var id in resID)
                        {
                            string _departmentidExt = id.DepartmentId.ToString();
                            CreateSync(null, _departmentidExt, itemm.DepartmentId);
                        }
                    }
                    else
                    {
                        foreach (var id in resID)
                        {
                            string _departmentidExt = id.DepartmentId.ToString() + "." + itemm.DepartmentId.ToString();
                            CreateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId);
                        }
                    }
                }
                else
                {
                    if (dem.Count == 3)
                    {
                        int start = dem[dem.Count - 2] + 1;
                        int end = dem[dem.Count - 1];
                        string _name = name.Substring(start, end - start);
                        var resID = getIDbyName(_name);
                        foreach (var id in resID)
                        {
                            string _departmentidExt =
                                id.ParentId.ToString() + "." + id.DepartmentId.ToString() +
                                "." + itemm.DepartmentId.ToString();
                            CreateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId);
                        }
                    }
                    else if (dem.Count == 4)
                    {
                        int start = dem[dem.Count - 2] + 1;
                        int end = dem[dem.Count - 1];
                        string _name = name.Substring(start, end - start);
                        var resID = getIDbyName(_name);
                        foreach (var id in resID)
                        {
                            var result = getIDbyParentID(id.ParentId);
                            foreach (var re in result)
                            {
                                string _departmentidExt =
                                re.ParentId.ToString() + "." + id.ParentId.ToString() + "." +
                                id.DepartmentId.ToString() + "." + itemm.DepartmentId.ToString();
                                CreateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId);
                            }
                        }
                    }
                    else
                    {
                        int start = dem[dem.Count - 2] + 1;
                        int end = dem[dem.Count - 1];
                        string _name = name.Substring(start, end - start);
                        var resID = getIDbyName(_name);
                        foreach (var id in resID)
                        {
                            var result = getIDbyParentID(id.ParentId);
                            foreach (var re in result)
                            {
                                var resQNID = getIDbyName("Tỉnh Quảng Ninh");
                                foreach (var t in resQNID)
                                {
                                    string _departmentidExt =
                                t.DepartmentId.ToString() + "." + re.ParentId.ToString() + "." +
                                id.ParentId.ToString() + "." + id.DepartmentId.ToString() + "." +
                                itemm.DepartmentId.ToString();
                                    CreateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Vien10
       
        public void CreateSyncVien10(IList<Department> departments, bool ignoreExist)
        {
            if (departments == null || !departments.Any())
            {
                throw new ArgumentNullException("departments");
            }

            var names = departments.Select(x => x.DepartmentName);
            var exist = _departmentRepository.GetsAs(p => p.DepartmentName, p => names.Contains(p.DepartmentName));

            if (exist != null && exist.Any())
            {
                if (!ignoreExist || exist.Count() == departments.Count())
                {
                    UpdateExistsVien10();
                    throw new EgovException("Đã xong");
                }

                var list = departments.Where(p => !exist.Contains(p.DepartmentName));
                if (list == null || !list.Any())
                {
                    throw new EgovException(_resourceService.GetResource("Department.Create.Exist"));
                }
                Create(list);
            }
            else
            {
                CreateVien10(departments);
            }
        }

        private void CreateVien10(IList<Department> departments)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var item in departments)
            {
                _departmentRepository.Create(item);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
            CreateSyncVien10();
            _cacheManager.Remove(CacheParam.JobtitlesAllKey);
            _cacheManager.Remove(CacheParam.UserDepartmentJobtitlePositionAllKey);
        }
        public void UpdateExistsVien10()
        {
            var result1 = GetAll();
            foreach (var itemm in result1)
            {

                var name = itemm.DepartmentPath;
                List<int> dem = GetPositions(name, '\\');
                if (dem.Count == 1 || dem.Count == 2)
                {
                    var resID = getIDbyName("BV Quân Y 110");
                    if (dem.Count == 1)
                    {
                        foreach (var id in resID)
                        {
                            string _departmentidExt = id.DepartmentId.ToString();
                            UpdateSync(null, _departmentidExt, itemm.DepartmentId, DateTime.Now);
                        }
                    }
                    else
                    {
                        foreach (var id in resID)
                        {
                            string _departmentidExt = id.DepartmentId.ToString() + "." + itemm.DepartmentId.ToString();
                            UpdateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId, DateTime.Now);
                        }
                    }
                }
                else
                {
                    if (dem.Count == 3)
                    {
                        int start = dem[dem.Count - 2] + 1;
                        int end = dem[dem.Count - 1];
                        string _name = name.Substring(start, end - start);
                        var resID = getIDbyName(_name);
                        foreach (var id in resID)
                        {
                            string _departmentidExt =
                                id.ParentId.ToString() + "." + id.DepartmentId.ToString() +
                                "." + itemm.DepartmentId.ToString();
                            UpdateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId, DateTime.Now);
                        }
                    }
                    else if (dem.Count == 4)
                    {
                        int start = dem[dem.Count - 2] + 1;
                        int end = dem[dem.Count - 1];
                        string _name = name.Substring(start, end - start);
                        var resID = getIDbyName(_name);
                        foreach (var id in resID)
                        {
                            var result = getIDbyParentID(id.ParentId);
                            foreach (var re in result)
                            {
                                string _departmentidExt =
                                re.ParentId.ToString() + "." + id.ParentId.ToString() + "." +
                                id.DepartmentId.ToString() + "." + itemm.DepartmentId.ToString();
                                UpdateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId, DateTime.Now);
                            }
                        }
                    }
                    else
                    {
                        int start = dem[dem.Count - 2] + 1;
                        int end = dem[dem.Count - 1];
                        string _name = name.Substring(start, end - start);
                        var resID = getIDbyName(_name);
                        foreach (var id in resID)
                        {
                            var result = getIDbyParentID(id.ParentId);
                            foreach (var re in result)
                            {
                                var resQNID = getIDbyName("BV Quân Y 110");
                                foreach (var t in resQNID)
                                {
                                    string _departmentidExt =
                                t.DepartmentId.ToString() + "." + re.ParentId.ToString() + "." +
                                id.ParentId.ToString() + "." + id.DepartmentId.ToString() + "." +
                                itemm.DepartmentId.ToString();
                                    UpdateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId, DateTime.Now);
                                }
                            }
                        }
                    }
                }
            }
        }
        public void CreateSyncVien10()
        {
            var result1 = GetAll();
            foreach (var itemm in result1)
            {

                var name = itemm.DepartmentPath;
                List<int> dem = GetPositions(name, '\\');
                if (dem.Count == 1 || dem.Count == 2)
                {
                    var resID = getIDbyName("BV Quân Y 110");
                    if (dem.Count == 1)
                    {
                        foreach (var id in resID)
                        {
                            string _departmentidExt = id.DepartmentId.ToString();
                            CreateSync(null, _departmentidExt, itemm.DepartmentId);
                        }
                    }
                    else
                    {
                        foreach (var id in resID)
                        {
                            string _departmentidExt = id.DepartmentId.ToString() + "." + itemm.DepartmentId.ToString();
                            CreateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId);
                        }
                    }
                }
                else
                {
                    if (dem.Count == 3)
                    {
                        int start = dem[dem.Count - 2] + 1;
                        int end = dem[dem.Count - 1];
                        string _name = name.Substring(start, end - start);
                        var resID = getIDbyName(_name);
                        foreach (var id in resID)
                        {
                            string _departmentidExt =
                                id.ParentId.ToString() + "." + id.DepartmentId.ToString() +
                                "." + itemm.DepartmentId.ToString();
                            CreateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId);
                        }
                    }
                    else if (dem.Count == 4)
                    {
                        int start = dem[dem.Count - 2] + 1;
                        int end = dem[dem.Count - 1];
                        string _name = name.Substring(start, end - start);
                        var resID = getIDbyName(_name);
                        foreach (var id in resID)
                        {
                            var result = getIDbyParentID(id.ParentId);
                            foreach (var re in result)
                            {
                                string _departmentidExt =
                                re.ParentId.ToString() + "." + id.ParentId.ToString() + "." +
                                id.DepartmentId.ToString() + "." + itemm.DepartmentId.ToString();
                                CreateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId);
                            }
                        }
                    }
                    else
                    {
                        int start = dem[dem.Count - 2] + 1;
                        int end = dem[dem.Count - 1];
                        string _name = name.Substring(start, end - start);
                        var resID = getIDbyName(_name);
                        foreach (var id in resID)
                        {
                            var result = getIDbyParentID(id.ParentId);
                            foreach (var re in result)
                            {
                                var resQNID = getIDbyName("BV Quân Y 110");
                                foreach (var t in resQNID)
                                {
                                    string _departmentidExt =
                                t.DepartmentId.ToString() + "." + re.ParentId.ToString() + "." +
                                id.ParentId.ToString() + "." + id.DepartmentId.ToString() + "." +
                                itemm.DepartmentId.ToString();
                                    CreateSync(id.DepartmentId, _departmentidExt, itemm.DepartmentId);
                                }
                            }
                        }
                    }
                }
            }
        }
        public IEnumerable<Department> Gets()
        {
            return _departmentRepository.GetsReadOnly();
        }
        #endregion

        public IEnumerable<dynamic> getIDbyParentID(int parentId)
        {
            var query = @"SELECT * FROM department WHERE DepartmentId = @parentId LIMIT 1";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("parentId", parentId));
            var result = Context.RawQuery(query, parameters.ToArray());
            return result;
        }
        public IEnumerable<dynamic> getIDbyName(string departmentName)
        {
            var query = @"SELECT * FROM department WHERE DepartmentName = @departmentName";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("departmentName", departmentName));
            var result = Context.RawQuery(query, parameters.ToArray());
            return result;
        }
        public IEnumerable<dynamic> GetAll()
        {
            var query = @"Select * From department";
            var result = Context.RawQuery(query);
            return result;
        }

        public IEnumerable<dynamic> UpdateSync(int? parentid,string departmentidext, int departmentid, DateTime update)
        {
            var query = @"UPDATE department SET DepartmentIdExt = @departmentidext, ParentId = @parentid , LastModifiedOnDate = @update
                WHERE DepartmentId = @departmentid";

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("parentid", parentid));
            parameters.Add(new SqlParameter("departmentidext", departmentidext));
            parameters.Add(new SqlParameter("departmentid", departmentid));
            parameters.Add(new SqlParameter("update", update));
            var result = Context.RawQuery(query, parameters.ToArray());
            return result;
        }
        public IEnumerable<dynamic> CreateSync(int? parentid, string departmentidext, int departmentid)
        {
            var query = @"UPDATE department SET DepartmentIdExt = @departmentidext, ParentId = @parentid , CreatedOnDate = @createondate
                WHERE DepartmentId = @departmentid";

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("parentid", parentid));
            parameters.Add(new SqlParameter("departmentidext", departmentidext));
            parameters.Add(new SqlParameter("departmentid", departmentid));
            parameters.Add(new SqlParameter("createondate", DateTime.Now));
            var result = Context.RawQuery(query, parameters.ToArray());
            return result;
        }

        public IEnumerable<dynamic> getIDbyPath(string path)
        {
            var query = @"SELECT * FROM department WHERE DepartmentPath = @path";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("path", path));
            var result = Context.RawQuery(query, parameters.ToArray());
            return result;
        }
    }
}
