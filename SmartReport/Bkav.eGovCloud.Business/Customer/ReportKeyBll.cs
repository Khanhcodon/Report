using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using ReportKey = Bkav.eGovCloud.Entities.Customer.ReportKey;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Entities;
using MySql.Data.MySqlClient;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.EReport.Entity;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportKeyKeyBll - public - Bll
    /// Access Modifiers: 
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : Cung cấp các API xử lý báo báo
    /// </summary>
    public class ReportKeyBll : ServiceBase
    {
        private readonly IRepository<ReportKey> _reportKeyRepository;

        private readonly UserBll _userService;
        private readonly DocColumnSettingBll _columnSettingsService;
        private readonly DepartmentBll _departmentService;
        private readonly FileLocationSettings _fileLocationSettings;
        private readonly FileManager _fileManager;
        private readonly FileLocationBll _fileLocationService;
        private readonly ResourceBll _resourceService;
        private readonly InfomationBll _infoService;
        private readonly OnlineRegistrationSettings _onlineSettings;
        private readonly AdminGeneralSettings _generalSettings;

        private readonly AddressBll _addressService;
        private readonly MemoryCacheManager _cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userService"></param>
        /// <param name="fileLocationService"></param>
        /// <param name="fileLocationSettings"></param>
        /// <param name="resourceService"></param>
        /// <param name="infoService"></param>
        /// <param name="addressService"></param>
        /// <param name="onlineSettings"></param>
        /// <param name="columnSettingsService"></param>
        /// <param name="cache"></param>
        public ReportKeyBll(
            IDbCustomerContext context,
            UserBll userService,
            FileLocationBll fileLocationService,
            FileLocationSettings fileLocationSettings,
            ResourceBll resourceService,
            InfomationBll infoService,
            AddressBll addressService,
            DocColumnSettingBll columnSettingsService,
            OnlineRegistrationSettings onlineSettings,
            MemoryCacheManager cache,
            AdminGeneralSettings generalSettings)
            : base(context)
        {
            _reportKeyRepository = Context.GetRepository<ReportKey>();
            _userService = userService;
            _fileLocationService = fileLocationService;
            _fileLocationSettings = fileLocationSettings;
            _fileManager = FileManager.Default;
            _resourceService = resourceService;
            _infoService = infoService;
            _addressService = addressService;
            _onlineSettings = onlineSettings;
            _columnSettingsService = columnSettingsService;
            _generalSettings = generalSettings;
            _cache = cache;
        }

        #region ReportKey 

        /// <summary>
        /// Trả về báo cáo theo id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public ReportKey Get(int id)
        {
            return _reportKeyRepository.Get(id);
        }

        /// <summary>
        /// Trả về danh sách tất cả các báo cáo trong hệ thống. Kết quả chỉ đọc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReportKey> Gets()
        {
            return _reportKeyRepository.GetsReadOnly();
        }

        /// <summary>
        /// Trả về danh sách tất cả các báo cáo trong hệ thống. Kết quả chỉ đọc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<ReportKey, T>> projector, Expression<Func<ReportKey, bool>> spec = null)
        {
            return _reportKeyRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Thêm mới báo cáo
        /// </summary>
        /// <param name="entity"></param>
        public void Create(ReportKey entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _reportKeyRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật báo cáo
        /// </summary>
        /// <param name="reportKey"></param>
        public void Update(ReportKey reportKey)
        {
            if (reportKey == null)
            {
                throw new ArgumentNullException("reportKey");
            }
            Context.SaveChanges();
        }


        /// <summary>
        /// <para>Xóa báo cáo</para>
        /// <para>Việc xóa báo cáo này sẽ kèm theo xóa tất cả các báo cáo con của nó</para>
        /// </summary>
        /// <param name="reportKey">Báo cáo</param>
        /// <exception cref="ArgumentNullException">Tham số ReportKey không được phép null</exception>
        private void Delete(ReportKey reportKey)
        {
            if (reportKey == null)
            {
                throw new ArgumentNullException("reportKey");
            }
            using (var trans = new TransactionScope())
            {
                var chills = _reportKeyRepository.Gets(false, r => r.ParentId == reportKey.ReportKeyId);
                var reportKeys = chills as ReportKey[] ?? chills.ToArray();
                if (reportKeys.Any())
                {
                    foreach (var child in reportKeys)
                    {
                        Delete(child);
                    }
                }
                _reportKeyRepository.Delete(reportKey);
                trans.Complete();
            }
        }

        /// <summary>
        /// <para>Xóa báo cáo</para>
        /// <para>Việc xóa báo cáo này sẽ kèm theo xóa tất cả các báo cáo con của nó</para>
        /// </summary>
        /// <param name="id">Báo cáo</param>
        /// <exception cref="ArgumentNullException">Tham số ReportKey không được phép null</exception>
        public void Delete(int id)
        {
            var reportKey = Get(id);
            Delete(reportKey);
            Context.SaveChanges();
        }

        /// <summary>
        /// Sao chép một báo cáo.
        /// </summary>
        /// <param name="targetId">Báo cáo được sao chép</param>
        /// <param name="toParentId">Báo cáo được dán</param>
        /// <exception cref="Exception">Báo cáo được sao chép không tồn tại</exception>
        /// <exception cref="Exception">Báo cáo được dán không tồn tại</exception>
        public ReportKey Copy(int targetId, int toParentId)
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
            var newReportKey = new ReportKey
            {
                ParentId = toParentId,
                Name = target.Name,
                Description = target.Description,
                IsActive = target.IsActive,
                Sql = target.Sql
            };
            _reportKeyRepository.Create(newReportKey);
            Context.SaveChanges();
            return newReportKey;
        }



        #endregion

      

        #region Private Methods

        private string GetGroupName(string query, string groupName)
        {
            string[] parts = query.Split(new string[] { "FROM" },
                         StringSplitOptions.RemoveEmptyEntries);
            var queryField = parts[0];
            queryField = queryField.Replace("select", "");
            queryField = queryField.Replace("\r\n", "");
            var field = queryField.Split(new char[] { ',' },
                         StringSplitOptions.RemoveEmptyEntries);
            var fileSuccess = field.Where(f => f.Contains(groupName));
            if (fileSuccess != null)
            {
                var result = fileSuccess.First();
                if (result.ToLower().Contains("as"))
                {
                    var fieldFinish = result.Split(new string[] { " " },
                         StringSplitOptions.RemoveEmptyEntries);
                    return fieldFinish[0];
                };
            }
            return groupName;
        }

        private bool HasPermission(ReportKey ReportKey, CurrentUserCached user, IEnumerable<DepartmentCached> allDepartments)
        {
            //if (!ReportKey.ListUserHasPermission.Any()
            //    && !ReportKey.ListPositionHasPermission.Any()
            //    && !ReportKey.ListDepartmentPositionHasPermission.Any())
            //{
            //    return true;
            //}

            //if (ReportKey.ListUserHasPermission.Any(uid => uid == user.UserId))
            //{
            //    return true;
            //}

            //var positionIds = user.UserDepartmentJobTitless.Select(u => u.PositionId).Distinct();
            //if (positionIds.Any(positionId => ReportKey.ListPositionHasPermission.Any(pid => pid == positionId)))
            //{
            //    return true;
            //}

            //var departmentPositionIds = user.UserDepartmentJobTitless.Select(u => new { u.DepartmentId, u.PositionId });
            //foreach (var departmentPosition in ReportKey.ListDepartmentPositionHasPermission)
            //{
            //    if (departmentPosition.ContainsKey("DepartmentId") && departmentPosition.ContainsKey("PositionId"))
            //    {
            //        var departmentId = departmentPosition["DepartmentId"];
            //        var positionId = departmentPosition["PositionId"];
            //        if (departmentPositionIds.Any(d => d.DepartmentId == departmentId && d.PositionId == positionId))
            //        {
            //            return true;
            //        }
            //        var children = GetDepartmentChildrenId2(departmentId, allDepartments);
            //        foreach (var child in children)
            //        {
            //            if (departmentPositionIds.Any(d => d.DepartmentId == child && d.PositionId == positionId))
            //            {
            //                return true;
            //            }
            //        }
            //    }
            //}

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

        //private Dictionary<string, object> GetSpecialField(int userId, DateTimeReportKey time, DateTime from, DateTime to)
        //{
        //    var result = new Dictionary<string, object>
        //    {
        //        {"CurrentUser", userId},
        //        {"ThoiGian", _resourceService.GetEnumDescription<DateTimeReportKey>(time)},
        //        {"TuThoiGian", @from.ToShortDateString()},
        //        {"DenThoiGian", to.ToShortDateString()}
        //    };
        //    return result;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="time"></param>
        ///// <param name="treeGroupName"></param>
        ///// <param name="from"></param>
        ///// <param name="to"></param>
        ///// <returns></returns>
        //public DataTable GetSpecialTable(int userId, DateTimeReportKey time = DateTimeReportKey.TrongNgay, string treeGroupName = "", DateTime? from = null, DateTime? to = null)
        //{
        //    var result = new DataTable("Special");
        //    result.Columns.Add("CurrentUser", typeof(string));
        //    result.Columns.Add("ThoiGian", typeof(string));
        //    result.Columns.Add("TuThoiGian", typeof(string));
        //    result.Columns.Add("DenThoiGian", typeof(string));
        //    result.Columns.Add("OfficeName", typeof(string));
        //    result.Columns.Add("OfficeParentName", typeof(string));
        //    result.Columns.Add("OfficeNameUpperCase", typeof(string));
        //    result.Columns.Add("OfficePhone", typeof(string));
        //    result.Columns.Add("OfficeExt", typeof(string));
        //    result.Columns.Add("OnlineLink", typeof(string));
        //    result.Columns.Add("DocFieldName", typeof(string));
        //    result.Columns.Add("DocTypeName", typeof(string));
        //    result.Columns.Add("TreeGroupName", typeof(string));
        //    result.Columns.Add("Day", typeof(string));
        //    result.Columns.Add("Month", typeof(string));
        //    result.Columns.Add("Year", typeof(string));

        //    var user = _userService.GetFromCache(userId);
        //    var info = _infoService.Gets().FirstOrDefault();
        //    var officeName = info == null ? string.Empty : info.Name;
        //    var officeParentName = info == null ? string.Empty : info.ParentName;
        //    var row = result.NewRow();
        //    row["CurrentUser"] = (user == null ? string.Empty : user.FullName);
        //    row["ThoiGian"] = _resourceService.GetEnumDescription<DateTimeReportKey>(time);
        //    row["TuThoiGian"] = from.HasValue ? from.Value.ToShortDateString() : DateTime.Now.ToString();
        //    row["DenThoiGian"] = to.HasValue ? to.Value.ToShortDateString() : DateTime.Now.ToString();
        //    row["OfficeName"] = officeName;
        //    row["OfficeParentName"] = officeParentName;
        //    row["OfficeNameUpperCase"] = officeName.ToUpper();
        //    var phone = info == null ? string.Empty : info.Phone;
        //    phone += (info == null || string.IsNullOrEmpty(info.PhoneExt)) ? string.Empty : (" - ext: " + info.PhoneExt);
        //    row["OfficePhone"] = phone;
        //    row["OfficeExt"] = info == null ? string.Empty : (" - ext: " + info.PhoneExt);
        //    row["OnlineLink"] = _onlineSettings.OnlineLink;
        //    row["DocFieldName"] = string.Empty;
        //    row["DocTypeName"] = string.Empty;
        //    row["TreeGroupName"] = treeGroupName;
        //    row["Day"] = DateTime.Now.Day;
        //    row["Month"] = DateTime.Now.Month;
        //    row["Year"] = DateTime.Now.Year;
        //    result.Rows.Add(row);
        //    return result;
        //}

        private object[] GetParameters(int userId, DateTime from, DateTime to, int page, int pageSize, string groupValue)
        {
            var readAllDoc = _userService.CurrentUser.CanReadEveryDocument.HasValue && _userService.CurrentUser.CanReadEveryDocument.Value ? 1 : 0;
            var organize = "000.00.00.H00";
            try
            {
                var depart = _userService.CurrentUser.UserDepartmentJobTitless.FirstOrDefault();
                organize = _departmentService.Get(depart.DepartmentId).Emails;
            }
            catch { }

            return new List<Object> {
                new SqlParameter("@readAllDoc", readAllDoc),
                new SqlParameter("@userId", userId),
                new SqlParameter("@from", from),
                new SqlParameter("@to", to),
                new SqlParameter("@skip", pageSize * (page - 1)),
                new SqlParameter("@take", pageSize),
                new SqlParameter("@treeGroupValue", groupValue ?? ""),
                new SqlParameter("@organize", organize ?? "")
            }.ToArray();
        }

        //private ReportKeyFile UploadCrystal(Dictionary<string, string> tempFile, out string filename)
        //{
        //    filename = string.Empty;
        //    ReportKeyFile result = null;
        //    var file = tempFile.First();
        //    var tempPath = ResourceLocation.Default.FileUploadTemp;
        //    var currentFileLocation = _fileLocationService.GetCurrent();
        //    var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
        //    if (_fileManager.Exist(file.Key, tempPath))
        //    {
        //        using (var stream = _fileManager.Open(file.Key, tempPath))
        //        {
        //            var fileInfo = transfer.Upload(stream, FileType.ReportKey);
        //            result = new ReportKeyFile()
        //            {
        //                DateCreated = fileInfo.CreatedDate,
        //                FileName = fileInfo.FileName,
        //                IdentityFolder = fileInfo.IdentityFolder,
        //                FileLocationKey = fileInfo.RootFolder,
        //                FileLocationId = currentFileLocation.FileLocationId
        //            };
        //            filename = tempFile[file.Key];
        //        }
        //        //_fileManager.Delete(file.Key, tempPath);  => Cập nhật 2 lần liên tiếp sẽ lỗi, khi nào thì xóa dc?
        //    }
        //    return result;
        //}

        private string ParseSql(string query, string groupBy, string groupDisplayBy, string treeGroupName = "", string sortBy = "Desc", bool isDesc = false)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }

            groupBy = string.IsNullOrEmpty(groupBy) ? "''" : groupBy;
            groupDisplayBy = string.IsNullOrEmpty(groupDisplayBy) ? "''" : groupDisplayBy;
            sortBy = string.IsNullOrEmpty(sortBy) ? "DateCreated" : sortBy;
            treeGroupName = string.IsNullOrEmpty(treeGroupName) ? "''" : treeGroupName;
            var desc = isDesc ? "DESC" : "";
            query = query.Replace("#treeGroup", treeGroupName);
            query = query.Replace("#sortBy", sortBy);
            query = query.Replace("#isDesc", desc);
            query = query.Replace("#groupValue", groupBy);
            query = query.Replace("#groupName", groupDisplayBy);
            return query;
        }

        private DataTable GetDataForCrystal(string query, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }

            if (query.StartsWith("CAll", StringComparison.OrdinalIgnoreCase))
            {
                var stores = query.Split(' ');
                if (stores.Length <= 1)
                {
                    return new DataTable("ReportKeyData");
                }
                return Context.RawProcedureTable(stores[1], parameters);
            }
            var result = Context.RawTable(query, parameters);
            return result;
        }

        private IEnumerable<IDictionary<string, object>> GetData(string query, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }
            query = query.Trim();
            var isConnectDashboard = query.StartsWith("dashboard:", StringComparison.OrdinalIgnoreCase);
            if (isConnectDashboard)
            {
                query = query.Replace("dashboard:", "");
                using (var context = new EfContext(new MySqlConnection(_generalSettings.DashboardConnection)))
                {

                    var command = "";
                    try
                    {
                        if (query.StartsWith("call", StringComparison.OrdinalIgnoreCase))
                        {
                            command = query.Split(' ').Last();
                            var result = context.RawProcedure(command, parameters.ToArray()) as IEnumerable<IDictionary<string, object>>;
                            return result;
                        }
                        else
                        {
                            command = query;
                            var result = context.RawQuery(query, parameters.ToArray()) as IEnumerable<IDictionary<string, object>>;
                            return result;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
            }

            if (query.StartsWith("CAll", StringComparison.OrdinalIgnoreCase))
            {
                var stores = query.Split(' ');
                if (stores.Length <= 1)
                {
                    return new List<IDictionary<string, object>>();
                }
                return Context.RawProcedure(stores[1], parameters) as IEnumerable<IDictionary<string, object>>;
            }
            var kq = Context.RawQuery(query, parameters) as IEnumerable<IDictionary<string, object>>;
            return kq;
        }

        public IEnumerable<IDictionary<string, object>> GetTableName()
        {
            var tablesQuery = "dashboard:select TABLE_NAME, TABLE_COMMENT from information_schema.tables where TABLE_SCHEMA='wso2_quangninh';";

            return GetData(tablesQuery);
        }

        private IEnumerable<IDictionary<string, object>> GetGroups(string query, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }

            return Context.RawQuery(query.Trim(), parameters) as IEnumerable<IDictionary<string, object>>;
        }

        private int GetTotal(string query, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return 0;
            }

            int total;
            int.TryParse((Context.RawScalar(query.Trim(), parameters)).ToString(), out total);
            return total;
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="userId"></param>
        /// <param name="time"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="groupId"></param>
        /// <param name="treeGroupValue"></param>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        public ReportKeyObject GetReportDetailCache(int reportKeyId, int userId, DateTimeReport time, DateTime from, DateTime to, int groupId, string treeGroupValue, string groupBy = "")
        {
            var cacheKey = BuildCacheKey(reportKeyId, userId, from, to, groupBy);
            //// test
            _cache.Remove(cacheKey);

            var result = _cache.Get(cacheKey, () =>
            {
                var data = GetReportDetail(reportKeyId, userId, time, from, to, groupId, treeGroupValue, groupBy);
                if (data == null) return null;
                data.CacheKey = cacheKey;
                data.LastUpdate = DateTime.Now;
                data.Model = data.Data;
                return data;
            }, CacheParam.ReportCacheTimeOut);
            
            //var group = _reportGroupService.GetGroup(groupId);
            //if ((group == null || string.IsNullOrEmpty(treeGroupValue)) && result != null)
            //{
            //    result.Model = result.Data;
            //    result.Total = result.Data == null ? 0 : result.Data.Count();
            //    return result;
            //}

            //// Lọc dữ liệu theo nhóm con trên cây báo cáo
            //var treeGroupBy = group?.FieldName;
            //if (treeGroupBy == null || result == null) return result;
            //result.Model = result.Data.Where(d => d[treeGroupBy] != null && d[treeGroupBy].ToString() == treeGroupValue);

            //result.Total = result.Model.Count();

            return result;
        }
        private string BuildCacheKey(int reportId, int userId, DateTime from, DateTime to, string groupBy)
        {
            var key = string.Join("_", new string[] {
                reportId.ToString(), userId.ToString(),
                from.ToString("yyyyMMdd"), to.ToString("yyyyMMdd"),groupBy });
            return string.Format(CacheParam.ReportKeyCache, key);
        }
        /// <summary>
        /// Trả về dữ liệu 
        /// </summary>
        /// <param name="reportId">Report ID</param>
        /// <param name="userId">Người xem báo cáo</param>
        /// <param name="time">Kiểu thời gian</param>
        /// <param name="from">Từ thời gian</param>
        /// <param name="to">Đến thời gian</param>
        /// <param name="groupId">GroupId</param>
        /// <param name="treeGroupValue">Báo cáo con trên cây</param>
        /// <param name="groupBy">Báo cáo nhóm</param>
        /// <returns></returns>
        public ReportKeyObject GetReportDetail(int reportId, int userId, DateTimeReport time, DateTime from, DateTime to, int groupId, string treeGroupValue, string groupBy = "")
        {
            var report = Get(reportId);
            if (report == null)
            {
                return null;
            }

            //var columnConfig = _columnSettingsService.Get(report.DocColumnId);
            //if (columnConfig == null)
            //{
            //    return null;
            //}
            //var group = _reportGroupService.GetGroup(groupId);

            var result = new ReportKeyObject();
            var data = GetDataForReport(report.Sql, userId, time, from, to, null, "", groupBy);

            result.ReportKeyId = reportId;
            result.Data = data.DataValues ?? new List<IDictionary<string, object>>();
            result.Description = report.Description;
            result.ReportKeyName = report.Name;

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="userId"></param>
        /// <param name="time"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="group"></param>
        /// <param name="treeGroupValue"></param>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        public ReportData GetDataForReport(string query, int userId, DateTimeReport time, DateTime from, DateTime to, ReportGroup group, string treeGroupValue, string groupBy = "")
        {
            //if (!string.IsNullOrEmpty(groupBy))
            //{
            //    var groupByName = GetGroupName(query, groupBy);
            //    groupBy = ", " + groupByName + " as GroupName";
            //}
            //else
            //{
            //    groupBy = "";
            //}
            //if (query != null)
            //    query = query.Replace("#groupNameQuery", groupBy);

            var parameters = GetParameters(userId, from, to, 1, 30, treeGroupValue);
            var groupField = group == null ? string.Empty : group.FieldName;
            var groupName = group == null ? string.Empty : group.FieldDisplay;
            query = ParseSql(query, groupField, groupName);
            var result = GetData(query, parameters);
            return new ReportData
            {
                DataValues = result,
                SpecialValues = GetSpecialField(userId, time, from, to)
            };
        }
        private Dictionary<string, object> GetSpecialField(int userId, DateTimeReport time, DateTime from, DateTime to)
        {
            var result = new Dictionary<string, object>
            {
                {"CurrentUser", userId},
                {"ThoiGian", _resourceService.GetEnumDescription<DateTimeReport>(time)},
                {"TuThoiGian", @from.ToShortDateString()},
                {"DenThoiGian", to.ToShortDateString()}
            };
            return result;
        }
    }
}
