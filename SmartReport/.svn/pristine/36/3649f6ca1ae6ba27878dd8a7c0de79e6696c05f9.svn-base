using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportBll - public - Bll
    /// Access Modifiers: 
    /// Create Date : 260815
    /// Author      : HopCV
    /// Description : Cung cấp các API xử lý báo báo
    /// </summary>
    public class StatisticsBll : ServiceBase
    {
        private readonly IRepository<Statistics> _statisticsRepository;
        private readonly ReportGroupBll _reportGroupService;
        private readonly DepartmentBll _departmentService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="departmentService"></param>
        /// <param name="reportGroupService"></param>
        public StatisticsBll(
            IDbCustomerContext context,
            DepartmentBll departmentService,
            ReportGroupBll reportGroupService)
            : base(context)
        {
            _statisticsRepository = Context.GetRepository<Statistics>();
            _departmentService = departmentService;
            _reportGroupService = reportGroupService;
        }

        #region Statistics

        /// <summary>
        /// Trả về thống kê theo id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public Statistics Get(int id)
        {
            return _statisticsRepository.Get(id);
        }

        /// <summary>
        /// Trả về danh sách tất cả các thống kê trong hệ thống. Kết quả chỉ đọc
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<Statistics> Gets(Expression<Func<Statistics, bool>> spec = null)
        {
            return _statisticsRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Trả về danh sách tất cả các thống kê trong hệ thống. Kết quả chỉ đọc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Statistics, T>> projector, Expression<Func<Statistics, bool>> spec = null)
        {
            return _statisticsRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Thêm mới thống kê
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Statistics entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _statisticsRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật thống kê
        /// </summary>
        /// <param name="statistics"></param>
        public void Update(Statistics statistics)
        {
            if (statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// <para>Xóa thống kê</para>
        /// <para>Việc xóa thống kêo này sẽ kèm theo xóa tất cả các thống kê con của nó</para>
        /// </summary>
        /// <param name="statistics">thống kê</param>
        /// <exception cref="ArgumentNullException">Tham số report không được phép null</exception>
        private void Delete(Statistics statistics)
        {
            if (statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }

            using (var trans = new TransactionScope())
            {
                var childs = _statisticsRepository.Gets(false, r => r.ParentId == statistics.StatisticsId);
                if (childs.Any())
                {
                    foreach (var child in childs)
                    {
                        Delete(child);
                    }
                }
                _statisticsRepository.Delete(statistics);
                trans.Complete();
            }
        }

        /// <summary>
        /// <para>Xóa thống kê</para>
        /// <para>Việc xóa thống kê này sẽ kèm theo xóa tất cả các thống kê con của nó</para>
        /// </summary>
        /// <param name="id">Báo cáo</param>
        /// <exception cref="ArgumentNullException">Tham số report không được phép null</exception>
        public void Delete(int id)
        {
            var report = Get(id);
            Delete(report);
            Context.SaveChanges();
        }

        /// <summary>
        /// Sao chép một thống kê.
        /// </summary>
        /// <param name="targetId">Báo cáo được sao chép</param>
        /// <param name="toParentId">Báo cáo được dán</param>
        /// <exception cref="Exception">Báo cáo được sao chép không tồn tại</exception>
        /// <exception cref="Exception">Báo cáo được dán không tồn tại</exception>
        public Statistics Copy(int targetId, int toParentId)
        {
            var target = Get(targetId);
            if (target == null)
            {
                throw new Exception("Báo cáo được sao chép không tồn tại");
            }
            if (toParentId > 0)
            {
                var parent = Get(toParentId);
                if (parent == null)
                {
                    throw new Exception("Báo cáo được dán không tồn tại");
                }
            }

            var newReport = new Statistics
            {
                ParentId = toParentId,
                Name = target.Name,
                Description = target.Description,
                IsActive = target.IsActive,
                Query = target.Query,
                ReportGroup = target.ReportGroup,
                DeptPermission = target.DeptPermission,
                PositionPermission = target.PositionPermission,
                UserPermission = target.UserPermission,
                DateCreated = DateTime.Now
            };
            _statisticsRepository.Create(newReport);
            Context.SaveChanges();
            return newReport;
        }

        /// <summary>
        /// Trả về danh sách báo cáo theo parent Id
        /// <remarks>
        /// <para>
        ///     - Sử dụng cho việc load tree báo cáo phía cán bộ
        /// </para>
        /// <para>
        ///     - Lấy luôn cả các báo cáo nhóm được cấu hình
        /// </para>
        /// </remarks>
        /// </summary>
        /// <param name="parentId">Parent id</param>
        /// <param name="user">User id</param>
        /// <returns></returns>
        public IEnumerable<Statistics> Gets(User user, int? parentId = null)
        {
            if (user == null)
            {
                throw new Exception("User is null!");
            }

            var allReport = _statisticsRepository.GetsReadOnly(f => f.IsActive);
            var reports = allReport.Where(f => (parentId.HasValue && f.ParentId == parentId.Value) || (!parentId.HasValue && f.ParentId == 0));
            var result = new List<Statistics>();
            var allDepartments = _departmentService.GetCacheAllDepartments(true);

            if (reports != null && reports.Any())
            {
                foreach (var report in reports)
                {
                    if (HasPermission(report, user, allDepartments))
                    {
                        report.Childs = new List<Statistics>();
                        if (report.ReportGroups.Any())
                        {
                            report.Childs.AddRange(GetGroupForTree(report, user.UserId));
                        }
                        result.Add(report);
                    }
                }
            }

            if (parentId.HasValue)
            {
                // lấy các group tree cho node hiện tại
                var parent = Get(parentId.Value);
                result.AddRange(GetGroupForTree(parent, user.UserId));
            }

            return result;
        }

        /// <summary>
        /// Lấy dữ liệu thống kê
        /// </summary>
        /// <param name="query">Câu truy vấn lấy dữ liệu</param>
        /// <param name="userId">Cán bộ</param>
        /// <param name="from">Thời gian so sánh bắt đầu lấy</param>
        /// <param name="to">Thời gian kết thúc so sanh lấy dữ liệu</param>
        /// <param name="treeGroupName"></param>
        /// <param name="treeGroupValue"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public IEnumerable<IDictionary<string, object>> GetStatisticsToDictionary(string query, int userId,
            DateTime from,
            DateTime to,
            string treeGroupName,
            string treeGroupValue,
            IEnumerable<int> userIds)
        {
            var paras = GetParameters(userId, from, to, treeGroupValue);
            query = ParseSql(query, userIds, treeGroupName);
            return GetDataToDictionary(query, paras);
        }

        /// <summary>
        /// Lấy dữ liệu thống kê
        /// </summary>
        /// <param name="query">Câu truy vấn lấy dữ liệu</param>
        /// <param name="userId">Cán bộ</param>
        /// <param name="from">Thời gian so sánh bắt đầu lấy</param>
        /// <param name="to">Thời gian kết thúc so sanh lấy dữ liệu</param>
        /// <param name="treeGroupName"></param>
        /// <param name="treeGroupValue"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public DataTable GetStatisticsToDataTable(string query, int userId, DateTime from, DateTime to, string treeGroupName, string treeGroupValue, IEnumerable<int> userIds)
        {
            var paras = GetParameters(userId, from, to, treeGroupValue);
            query = ParseSql(query, userIds, treeGroupName);
            return GetDataToDataTable(query, paras);
        }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Tạo bộ lọc loại bỏ các node là node con của node khác và trả ra các node root hoặc node không có node cha
        /// </summary>
        /// <param name="allDepartments">Danh sách phòng ban</param>
        /// <param name="allStatistic">Tất cả các node thống kê</param>
        /// <param name="user">Người dùng</param>
        /// <returns></returns>
        private List<Statistics> CreateObjectStatistics(IEnumerable<Department> allDepartments, List<Statistics> allStatistic, User user)
        {
            allStatistic = allStatistic.OrderBy(p => p.ParentId).ToList();
            var leng = allStatistic.Count();
            var removeStatistic = new List<int>();
            for (var i = leng - 1; i >= 0; i--)
            {
                var item = allStatistic[i];
                if (item.ReportGroups.Any())
                {
                    if (allStatistic[i].Childs == null)
                    {
                        allStatistic[i].Childs = new List<Statistics>();
                    }
                    allStatistic[i].Childs.AddRange(GetGroupForTree(item, user.UserId));
                }

                var parent = allStatistic.FirstOrDefault(p => p.StatisticsId == item.ParentId);
                if (parent != null)
                {
                    removeStatistic.Add(item.StatisticsId);
                    if (parent.Childs == null)
                    {
                        parent.Childs = new List<Statistics>();
                    }
                    parent.Childs.Add(item);
                }
            }

            allStatistic = allStatistic.Where(p => !removeStatistic.Contains(p.StatisticsId)).OrderBy(p => p.ParentId).ToList();
            return allStatistic;
        }

        private void GetChilds(IEnumerable<Department> allDepartments, IEnumerable<Statistics> allStatistic, Statistics statistics, User user)
        {
            if (statistics.Childs == null)
            {
                statistics.Childs = new List<Statistics>();
            }

            if (statistics.ReportGroups.Any())
            {
                statistics.Childs.AddRange(GetGroupForTree(statistics, user.UserId));
            }

            var children = allStatistic.Where(p => p.ParentId == statistics.StatisticsId);
            if (children != null && children.Any())
            {
                foreach (var item in children)
                {
                    if (item.Childs == null)
                    {
                        item.Childs = new List<Statistics>();
                    }
                    item.Childs.AddRange(GetGroupForTree(item, user.UserId));
                    GetChilds(allDepartments, allStatistic, item, user);
                }

                statistics.Childs.AddRange(children);
            }
        }

        /// <summary>
        /// Lấy danh sách các node con
        /// </summary>
        /// <param name="statistics">Đối tượng node truyền vào để lấy các node con</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IEnumerable<Statistics> GetGroupForTree(Statistics statistics, int userId)
        {
            var result = new List<Statistics>();
            var groups = _reportGroupService.GetGroups(statistics.ReportGroups);
            if (groups != null && groups.Any())
            {
                var parameters = new Object[] { new SqlParameter("@userId", userId) };
                foreach (var group in groups)
                {
                    var query = group.Query;
                    var groupValues = GetDataToDictionary(query, parameters);
                    if (groupValues != null && groupValues.Any())
                    {
                        foreach (var gValue in groupValues)
                        {
                            var child = new Statistics
                            {
                                StatisticsId = statistics.StatisticsId,
                                Query = statistics.Query,
                                Name = gValue["GroupName"].ToString(),
                                TreeGroupValue = gValue["GroupValue"].ToString(),
                                TreeGroupName = group.FieldName
                            };
                            result.Add(child);
                        }
                    }
                }
            }
            return result;
        }

        private bool HasPermission(Statistics statistics, User user, IEnumerable<DepartmentCached> allDepartments)
        {
            if (!statistics.ListUserHasPermission.Any()
                && !statistics.ListPositionHasPermission.Any()
                && !statistics.ListDepartmentPositionHasPermission.Any())
            {
                return true;
            }

            if (statistics.ListUserHasPermission.Any(uid => uid == user.UserId))
            {
                return true;
            }

            var positionIds = user.UserDepartmentJobTitless.Select(u => u.PositionId).Distinct();
            if (positionIds.Any(positionId => statistics.ListPositionHasPermission.Any(pid => pid == positionId)))
            {
                return true;
            }

            var departmentPositionIds = user.UserDepartmentJobTitless.Select(u => new { u.DepartmentId, u.PositionId });
            foreach (var departmentPosition in statistics.ListDepartmentPositionHasPermission)
            {
                if (departmentPosition.ContainsKey("DepartmentId") && departmentPosition.ContainsKey("PositionId"))
                {
                    var departmentId = departmentPosition["DepartmentId"];
                    var positionId = departmentPosition["PositionId"];
                    if (departmentPositionIds.Any(d => d.DepartmentId == departmentId && d.PositionId == positionId))
                    {
                        return true;
                    }
                    var children = GetDepartmentChildrenId2(departmentId, allDepartments);
                    foreach (var child in children)
                    {
                        if (departmentPositionIds.Any(d => d.DepartmentId == child && d.PositionId == positionId))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private IEnumerable<int> GetDepartmentChildrenId(int parentId, IEnumerable<Department> allDepartment)
        {
            var result = new List<int>();
            var children = allDepartment.Where(d => d.ParentId == parentId);
            if (children.Any())
            {
                result.AddRange(children.Select(d => d.DepartmentId));
                foreach (var department in children)
                {
                    result.AddRange(GetDepartmentChildrenId(department.DepartmentId, allDepartment));
                }
            }
            return result;
        }

        private IEnumerable<int> GetDepartmentChildrenId2(int parentId, IEnumerable<DepartmentCached> allDepartment)
        {
            var result = new List<int>();
            var dept = allDepartment.FirstOrDefault(p => p.DepartmentId == parentId);
            if (dept != null)
            {
                var deptExt = dept.DepartmentIdExt + ".";
                var chilDepts = allDepartment.Where(p => p.DepartmentIdExt.StartsWith(deptExt)).Select(p => p.DepartmentId);
                result.AddRange(chilDepts);
            }
            return result;
        }

        private object[] GetParameters(int userId, DateTime from, DateTime to, string groupValue)
        {
            return new Object[] { 
                new SqlParameter("@userId", userId), 
                new SqlParameter("@from", from),
                new SqlParameter("@to", to),
                new SqlParameter("@treeGroupValue", groupValue )
            };
        }

        private string ParseSql(string query, IEnumerable<int> userIds, string treeGroupName = null)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }

            treeGroupName = string.IsNullOrEmpty(treeGroupName) ? "''" : treeGroupName;
            query = query.Replace("#treeGroup", treeGroupName);
            query = string.Format(query, string.Join(",", userIds));

            return query;
        }

        private DataTable GetDataToDataTable(string query, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }

            query = query.Trim();
            if (query.StartsWith("CAll", StringComparison.OrdinalIgnoreCase))
            {
                var stores = query.Split(' ');
                if (stores.Length <= 1)
                {
                    return new DataTable("ReportData");
                }
                return Context.RawProcedureTable(stores[1], parameters);
            }
            return Context.RawTable(query, parameters);
        }

        private IEnumerable<IDictionary<string, object>> GetDataToDictionary(string query, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }

            query = query.Trim();
            if (query.StartsWith("CAll", StringComparison.OrdinalIgnoreCase))
            {
                var stores = query.Split(' ');
                if (stores.Length <= 1)
                {
                    return new List<IDictionary<string, object>>();
                }
                return Context.RawProcedure(stores[1], parameters) as IEnumerable<IDictionary<string, object>>;
            }
            return Context.RawQuery(query, parameters) as IEnumerable<IDictionary<string, object>>;
        }

        #endregion
    }
}
