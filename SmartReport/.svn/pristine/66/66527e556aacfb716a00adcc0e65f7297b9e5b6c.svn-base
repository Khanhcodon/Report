using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.FileTransfer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.EReport.Entity;
using Report = Bkav.eGovCloud.Entities.Customer.Report;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Entities;
using MySql.Data.MySqlClient;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportBll - public - Bll
    /// Access Modifiers: 
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : Cung cấp các API xử lý báo báo
    /// </summary>
    public class ReportBll : ServiceBase
    {
        private readonly IRepository<Report> _reportRepository;
        private readonly ReportGroupBll _reportGroupService;
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
        /// <param name="departmentService"></param>
        /// <param name="resourceService"></param>
        /// <param name="reportGroupService"></param>
        /// <param name="infoService"></param>
        /// <param name="addressService"></param>
        /// <param name="onlineSettings"></param>
        /// <param name="columnSettingsService"></param>
        /// <param name="cache"></param>
        public ReportBll(
            IDbCustomerContext context,
            UserBll userService,
            FileLocationBll fileLocationService,
            FileLocationSettings fileLocationSettings,
            DepartmentBll departmentService,
            ResourceBll resourceService,
            ReportGroupBll reportGroupService,
            InfomationBll infoService,
            AddressBll addressService,
            DocColumnSettingBll columnSettingsService,
            OnlineRegistrationSettings onlineSettings,
            MemoryCacheManager cache,
            AdminGeneralSettings generalSettings)
            : base(context)
        {
            _reportRepository = Context.GetRepository<Report>();
            _reportGroupService = reportGroupService;
            _userService = userService;
            _fileLocationService = fileLocationService;
            _fileLocationSettings = fileLocationSettings;
            _fileManager = FileManager.Default;
            departmentService.ReportService = this;
            _departmentService = departmentService;
            _resourceService = resourceService;
            _infoService = infoService;
            _addressService = addressService;
            _onlineSettings = onlineSettings;
            _columnSettingsService = columnSettingsService;
            _generalSettings = generalSettings;
            _cache = cache;
        }

        #region Report

        /// <summary>
        /// Trả về báo cáo theo id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public Report Get(int id)
        {
            return _reportRepository.Get(id);
        }

        /// <summary>
        /// Trả về danh sách tất cả các báo cáo trong hệ thống. Kết quả chỉ đọc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Report> Gets()
        {
            return _reportRepository.GetsReadOnly();
        }

        /// <summary>
        /// Trả về danh sách tất cả các báo cáo trong hệ thống. Kết quả chỉ đọc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(Expression<Func<Report, T>> projector, Expression<Func<Report, bool>> spec = null)
        {
            return _reportRepository.GetsAs(projector, spec);
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
        public dynamic Gets(CurrentUserCached user, int? parentId)
        {
            if (user == null)
            {
                throw new Exception("User is null!");
            }

            var allReport = _reportRepository.GetsReadOnly(f => f.IsActive);

#if XuLyVanBanEdition
            allReport = allReport.Where(r => !r.IsHSMC).ToList();
#endif

            var reports = allReport.Where(f => (parentId.HasValue && f.ParentId == parentId.Value) || (!parentId.HasValue && f.ParentId == 0));
            var result = new List<Report>();

            var organizeKey = "";

            var allDepartments = _departmentService.GetCacheAllDepartments(true);
            var departUser = user.UserDepartmentJobTitless.OrderBy(d => d.IsPrimary).FirstOrDefault();
            if (departUser != null)
            {
                organizeKey = allDepartments.FirstOrDefault(d => d.DepartmentId == departUser.DepartmentId).Emails;
            }

            if (reports != null && reports.Any())
            {
                foreach (var report in reports)
                {
                    if (HasPermission(report, user, allDepartments))
                    {
                        report.Childs = new List<Report>();
                        if (parentId.HasValue)
                        {
                            report.Childs.AddRange(GetGroupForTree(report, user.UserId, organizeKey));
                        }
                        result.Add(report);
                    }
                }
            }

            if (parentId.HasValue)
            {
                // lấy các group tree cho node hiện tại
                var parent = Get(parentId.Value);
                result.AddRange(GetGroupForTree(parent, user.UserId, organizeKey));
            }

            var results = result.OrderBy(r => r.Name).Select(r => new
            {
                id = r.ReportId,
                name = r.Name,
                desc = r.Description,
                groupId = r.GroupId,
                parentid = r.ParentId,
                //state = (allReport.Any(a => a.ParentId == r.ReportId && string.IsNullOrEmpty(r.TreeGroupName)) || r.GroupForTrees.Any()) ? "closed" : "leaf",
                state = allReport.Any(a => a.ParentId == r.ReportId && string.IsNullOrEmpty(r.TreeGroupName)) ? "closed" : "leaf",
                children = (r.Childs != null && r.Childs.Any()) ? r.Childs.Select(c => new
                {
                    id = c.ReportId,
                    name = c.Name,
                    parentid = c.ParentId,
                    state = "leaf",
                    treeGroupName = c.TreeGroupName,
                    treeGroupValue = c.TreeGroupValue,
                    isLabel = c.IsLabel,
                }) : null,
                treeGroupName = r.TreeGroupName,
                treeGroupValue = r.TreeGroupValue,
                isLabel = r.IsLabel,
                isTotal = r.IsShowTotal
            });

            return results;
        }

        /// <summary>
        /// Lấy tất cả danh sách của report bao gồm parentid của nó
        /// </summary>
        /// <returns></returns>
        public dynamic GetsSearch()
        {
            var allReport = _reportRepository.GetsReadOnly(f => f.IsActive);
            var result = new List<Report>();
            foreach(var report in allReport)
            {
                result.Add(report);
            }
            return result;
        }

        /// <summary>
        /// Trả về danh sách các báo cáo đã được phân quyền theo user id
        /// </summary>
        /// <param name="userId">Current User id</param>
        /// <returns></returns>
        public IEnumerable<Report> GetAndPermission(int userId)
        {
            var reports = _reportRepository.GetsReadOnly(f => f.IsActive);
            var result = new List<Report>();
            var user = _userService.CurrentUser;
            var allDepartments = _departmentService.GetCacheAllDepartments(true);

            if (reports.Any())
            {
                foreach (var report in reports)
                {
                    if (HasPermission(report, user, allDepartments))
                    {
                        result.Add(report);
                        var groups = GetGroupForTree(report, user.UserId);
                        result.AddRange(groups);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Lấy danh sách các node con
        /// </summary>
        /// <param name="report">Đối tượng node truyền vào để lấy các node con</param>
        /// <param name="userId">Id cán bộ</param>
        /// <returns></returns>
        private List<Report> GetGroupForTree(Report report, int userId, string organizekey = "000.00.00.H00")
        {
            var result = new List<Report>();
            var groupForTree = report.GroupForTrees;
            var groups = _reportGroupService.GetGroups(groupForTree);
            if (groups != null && groups.Any())
            {
                var parameters = new Object[] { new SqlParameter("@userId", userId), new SqlParameter("@organizekey", organizekey) };

                foreach (var group in groups)
                {
                    var query = group.Query;
                    var groupValues = GetData(query, parameters);

                    var reportGroup = new Report()
                    {
                        Name = group.Name,
                        Childs = new List<Report>(),
                        IsLabel = true
                    };

                    if (groupValues != null && groupValues.Any())
                    {
                        foreach (var gValue in groupValues)
                        {
                            var child = new Report
                            {
                                ReportId = report.ReportId,
                                Name = gValue["GroupName"].ToString(),
                                TreeGroupValue = gValue["GroupValue"].ToString(),
                                TreeGroupName = group.FieldName,
                                GroupId = group.ReportGroupId,
                                IsLabel = false,
                                IsShowTotal = true,
                                ParentId = report.ReportId,
                            };
                            // reportGroup.Childs.Add(child);
                            result.Add(child);
                        }
                    }
                    // result.Add(reportGroup);
                }
            }
            return result;
        }

        /// <summary>
        /// Thêm mới báo cáo
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Report entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _reportRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Add report
        /// </summary>
        /// <param name="report"></param>
        /// <param name="tempFile"></param>
        public void Create(Report report, Dictionary<string, string> tempFile)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }
            if (tempFile != null)
            {
                string filename;
                var reportFile = UploadCrystal(tempFile, out filename);
                report.DateCreated = reportFile.DateCreated;
                report.FileLocationName = reportFile.FileName;
                report.CrystalPath = filename;
            }
            _reportRepository.Create(report);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật báo cáo
        /// </summary>
        /// <param name="report"></param>
        public void Update(Report report)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật báo cáo
        /// </summary>
        /// <param name="report"></param>
        /// <param name="tempFile"></param>
        /// <param name="tempFileGroup"></param>
        public void Update(Report report, Dictionary<string, string> tempFile, Dictionary<string, string> tempFileGroup)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }

            if (tempFileGroup != null)
            {
                // string filename;
                // var reportFile = UploadCrystal(tempFile, out filename);
                // report.DateCreated = mp;
                report.FileLocationNameGroup = tempFileGroup.Keys.First();
                report.CrystalGroupPath = tempFileGroup.Values.First();
            }

            if (tempFile != null)
            {
                // string filename;
                // var reportFile = UploadCrystal(tempFile, out filename);
                // report.DateCreated = mp;
                report.FileLocationName = tempFile.Keys.First();
                report.CrystalPath = tempFile.Values.First();
            }

            Update(report);
        }

        /// <summary>
        /// <para>Xóa báo cáo</para>
        /// <para>Việc xóa báo cáo này sẽ kèm theo xóa tất cả các báo cáo con của nó</para>
        /// </summary>
        /// <param name="report">Báo cáo</param>
        /// <exception cref="ArgumentNullException">Tham số report không được phép null</exception>
        private void Delete(Report report)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report");
            }
            using (var trans = new TransactionScope())
            {
                var childs = _reportRepository.Gets(false, r => r.ParentId == report.ReportId);
                if (childs.Any())
                {
                    foreach (var child in childs)
                    {
                        Delete(child);
                    }
                }
                _reportRepository.Delete(report);
                trans.Complete();
            }
        }

        /// <summary>
        /// <para>Xóa báo cáo</para>
        /// <para>Việc xóa báo cáo này sẽ kèm theo xóa tất cả các báo cáo con của nó</para>
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
        /// Sao chép một báo cáo.
        /// </summary>
        /// <param name="targetId">Báo cáo được sao chép</param>
        /// <param name="toParentId">Báo cáo được dán</param>
        /// <exception cref="Exception">Báo cáo được sao chép không tồn tại</exception>
        /// <exception cref="Exception">Báo cáo được dán không tồn tại</exception>
        public Report Copy(int targetId, int toParentId)
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
            var newReport = new Report
            {
                ParentId = toParentId,
                Name = target.Name,
                Description = target.Description,
                IsActive = target.IsActive,
                IsLabel = target.IsLabel,
                GroupForTree = target.GroupForTree,
                QueryStatistics = target.QueryStatistics,
                QueryTotal = target.QueryTotal,
                CrystalPath = target.CrystalPath,
                DeptPermission = target.DeptPermission,
                PositionPermission = target.PositionPermission,
                UserPermission = target.UserPermission,
                QueryTotalDocumentIsOverdue = target.QueryTotalDocumentIsOverdue,
                QueryTotalDocumentProcessed = target.QueryTotalDocumentProcessed
            };
            _reportRepository.Create(newReport);
            Context.SaveChanges();
            return newReport;
        }

        /// <summary>
        /// Tải tệp crystal
        /// </summary>
        /// <param name="fileName">Tên file</param>
        /// <param name="id">Report id</param>
        /// <returns></returns>
        public Stream Download(out string fileName, int id)
        {
            var report = Get(id);
            if (report == null)
            {
                throw new EgovException("Không tìm thấy báo cáo");
            }
            return Download(out fileName, report);
        }

        /// <summary>
        /// Tải tệp crystal
        /// </summary>
        /// <param name="fileName">Tên file</param>
        /// <param name="report">Report</param>
        /// <returns></returns>
        public Stream Download(out string fileName, Report report)
        {
            //var fileLocation = report.FileLocation;
            //if (fileLocation == null)
            //{
            //    throw new EgovException("Không tìm thấy nơi lưu tệp đính kèm");
            //}

            fileName = report.CrystalPath;
            var downloaded = _fileManager.Open(report.FileLocationName, ResourceLocation.Default.CrystalReport);
            //var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
            //var downloaded =
            //    transfer.Download(new FileTransferInfo
            //    {
            //        CreatedDate = report.DateCreated,
            //        FileName = report.FileLocationName,
            //        FileType = FileType.Report,
            //        IdentityFolder = report.IdentityFolder,
            //        RootFolder = report.FileLocationKey
            //    });

            return downloaded;
        }

        #endregion

        #region ViewReport

        /// <summary>
        /// Trả về dữ liệu báo cáo cho crystal
        /// </summary>
        /// <param name="report"></param>
        /// <param name="group"></param>
        /// <param name="userId"></param>
        /// <param name="time"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="treeGroupValue"></param>
        /// <param name="treeGroupName"></param>
        /// <param name="treeDisplayName"></param>
        /// <returns></returns>
        public DataSet GetDataForCrystal(Report report, ReportGroup group, int userId, DateTimeReport time, DateTime from, DateTime to, string treeGroupValue, string treeGroupName = "", string treeDisplayName = "")
        {
            return null;
            //var parameters = GetParameters(userId, from, to, 1, 0, treeGroupValue);
            //var groupField = group == null ? string.Empty : group.FieldName;
            //var groupName = group == null ? string.Empty : group.FieldDisplay;
            //var query = ParseSql(report.QueryReport, groupField, groupName, treeGroupName);
            //var data = GetDataForCrystal(query, parameters);
            //data.TableName = "ReportData";
            //var result = new DataSet();
            //result.Tables.Add(data);

            //var specials = GetSpecialTable(userId, time, treeDisplayName, from, to);
            //specials.TableName = "Special";
            //result.Tables.Add(specials);

            //return result;
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
        public ReportData GetDataForReport(string query, int userId, DateTimeReport time, DateTime from, DateTime to, ReportGroup group, string treeGroupValue, string groupBy = "", int timekey = 0)
        {
            if (!string.IsNullOrEmpty(groupBy))
            {
                var groupByName = GetGroupName(query, groupBy);
                groupBy = ", " + groupByName + " as GroupName";
            }
            else
            {
                groupBy = "";
            }
            if (query != null)
                query = query.Replace("#groupNameQuery", groupBy);

            var parameters = GetParameters(userId, from, to, 1, 30, treeGroupValue, timekey);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryStatistics"></param>
        /// <param name="specicalValues"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ReportData GetDataStatistics(string queryStatistics, IDictionary<string, object> specicalValues, params object[] parameters)
        {
            var data = GetData(queryStatistics, parameters);
            return new ReportData
            {
                DataValues = data,
                SpecialValues = specicalValues
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryGroup"></param>
        /// <param name="specicalValues"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ReportData GetDataStatisticsByGroup(string queryGroup, IDictionary<string, object> specicalValues, params object[] parameters)
        {
            return new ReportData
            {
                DataValues = new List<IDictionary<string, object>>(),
                SpecialValues = specicalValues,
                GroupValues = GetGroups(queryGroup, parameters)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryStatistics"></param>
        /// <param name="specicalValues"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ReportData GetDataForPage(string queryStatistics, IDictionary<string, object> specicalValues, params object[] parameters)
        {
            return new ReportData
            {
                DataValues = GetData(queryStatistics, parameters),
                SpecialValues = specicalValues,
                GroupValues = null
            };
        }

        /// <summary>
        /// Lấy ra tổng số tất cả văn bản
        /// </summary>
        /// <param name="totalQuery"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int CountTotalRecord(string totalQuery, params object[] parameters)
        {
            var result = GetTotal(totalQuery, parameters);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupQuery"></param>
        /// <param name="groupValue"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int CountTotalByGroup(string groupQuery, string groupValue, params object[] parameters)
        {
            var groups = GetGroups(groupQuery, parameters);
            try
            {
                foreach (var group in groups)
                {
                    if (group["GroupValue"].ToString() == groupValue)
                    {
                        return int.Parse(group["Total"].ToString());
                    }
                }
            }
            catch { }

            return 0;
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

        private bool HasPermission(Report report, CurrentUserCached user, IEnumerable<DepartmentCached> allDepartments)
        {
            if (!report.ListUserHasPermission.Any()
                && !report.ListPositionHasPermission.Any()
                && !report.ListDepartmentPositionHasPermission.Any())
            {
                return true;
            }

            if (report.ListUserHasPermission.Any(uid => uid == user.UserId))
            {
                return true;
            }

            var positionIds = user.UserDepartmentJobTitless.Select(u => u.PositionId).Distinct();
            if (positionIds.Any(positionId => report.ListPositionHasPermission.Any(pid => pid == positionId)))
            {
                return true;
            }

            var departmentPositionIds = user.UserDepartmentJobTitless.Select(u => new { u.DepartmentId, u.PositionId });
            foreach (var departmentPosition in report.ListDepartmentPositionHasPermission)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="time"></param>
        /// <param name="treeGroupName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public DataTable GetSpecialTable(int userId, DateTimeReport time = DateTimeReport.TrongNgay, string treeGroupName = "", DateTime? from = null, DateTime? to = null)
        {
            var result = new DataTable("Special");
            result.Columns.Add("CurrentUser", typeof(string));
            result.Columns.Add("ThoiGian", typeof(string));
            result.Columns.Add("TuThoiGian", typeof(string));
            result.Columns.Add("DenThoiGian", typeof(string));
            result.Columns.Add("OfficeName", typeof(string));
            result.Columns.Add("OfficeParentName", typeof(string));
            result.Columns.Add("OfficeNameUpperCase", typeof(string));
            result.Columns.Add("OfficePhone", typeof(string));
            result.Columns.Add("OfficeExt", typeof(string));
            result.Columns.Add("OnlineLink", typeof(string));
            result.Columns.Add("DocFieldName", typeof(string));
            result.Columns.Add("DocTypeName", typeof(string));
            result.Columns.Add("TreeGroupName", typeof(string));
            result.Columns.Add("Day", typeof(string));
            result.Columns.Add("Month", typeof(string));
            result.Columns.Add("Year", typeof(string));

            var user = _userService.GetFromCache(userId);
            var info = _infoService.Gets().FirstOrDefault();
            var officeName = info == null ? string.Empty : info.Name;
            var officeParentName = info == null ? string.Empty : info.ParentName;
            var row = result.NewRow();
            row["CurrentUser"] = (user == null ? string.Empty : user.FullName);
            row["ThoiGian"] = _resourceService.GetEnumDescription<DateTimeReport>(time);
            row["TuThoiGian"] = from.HasValue ? from.Value.ToShortDateString() : DateTime.Now.ToString();
            row["DenThoiGian"] = to.HasValue ? to.Value.ToShortDateString() : DateTime.Now.ToString();
            row["OfficeName"] = officeName;
            row["OfficeParentName"] = officeParentName;
            row["OfficeNameUpperCase"] = officeName.ToUpper();
            var phone = info == null ? string.Empty : info.Phone;
            phone += (info == null || string.IsNullOrEmpty(info.PhoneExt)) ? string.Empty : (" - ext: " + info.PhoneExt);
            row["OfficePhone"] = phone;
            row["OfficeExt"] = info == null ? string.Empty : (" - ext: " + info.PhoneExt);
            row["OnlineLink"] = _onlineSettings.OnlineLink;
            row["DocFieldName"] = string.Empty;
            row["DocTypeName"] = string.Empty;
            row["TreeGroupName"] = treeGroupName;
            row["Day"] = DateTime.Now.Day;
            row["Month"] = DateTime.Now.Month;
            row["Year"] = DateTime.Now.Year;
            result.Rows.Add(row);
            return result;
        }

        private object[] GetParameters(int userId, DateTime from, DateTime to, int page, int pageSize, string groupValue, int timekey)
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
                new SqlParameter("@organize", organize ?? ""),
                new SqlParameter("@timekey", timekey)
            }.ToArray();
        }

        private ReportFile UploadCrystal(Dictionary<string, string> tempFile, out string filename)
        {
            filename = string.Empty;
            ReportFile result = null;
            var file = tempFile.First();
            var tempPath = ResourceLocation.Default.FileUploadTemp;
            var currentFileLocation = _fileLocationService.GetCurrent();
            var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
            if (_fileManager.Exist(file.Key, tempPath))
            {
                using (var stream = _fileManager.Open(file.Key, tempPath))
                {
                    var fileInfo = transfer.Upload(stream, FileType.Report);
                    result = new ReportFile()
                    {
                        DateCreated = fileInfo.CreatedDate,
                        FileName = fileInfo.FileName,
                        IdentityFolder = fileInfo.IdentityFolder,
                        FileLocationKey = fileInfo.RootFolder,
                        FileLocationId = currentFileLocation.FileLocationId
                    };
                    filename = tempFile[file.Key];
                }
                //_fileManager.Delete(file.Key, tempPath);  => Cập nhật 2 lần liên tiếp sẽ lỗi, khi nào thì xóa dc?
            }
            return result;
        }

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
                    return new DataTable("ReportData");
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
        public ReportObject GetReportDetailCache(int reportId, int userId, DateTimeReport time, DateTime from, DateTime to, int groupId, string treeGroupValue, string groupBy = "", int timekey = 0)
        {
            var cacheKey = BuildCacheKey(reportId, userId, from, to, groupBy);
            //// test
            _cache.Remove(cacheKey);

            var result = _cache.Get<ReportObject>(cacheKey, () =>
            {
                var data = GetReportDetail(reportId, userId, time, from, to, groupId, treeGroupValue, groupBy, timekey);
                if (data != null)
                {
                    data.CacheKey = cacheKey;
                    data.LastUpdate = DateTime.Now;
                }
                return data;
            }, CacheParam.ReportCacheTimeOut);

            var group = _reportGroupService.GetGroup(groupId);
            if ((group == null || string.IsNullOrEmpty(treeGroupValue)) && result != null)
            {
                result.Model = result.Data;
                result.Total = result.Data == null ? 0 : result.Data.Count();
                return result;
            }

            // Lọc dữ liệu theo nhóm con trên cây báo cáo
            var treeGroupBy = group?.FieldName;
            if (treeGroupBy == null || result == null) return result;
            result.Model = result.Data.Where(d => d[treeGroupBy] != null && d[treeGroupBy].ToString() == treeGroupValue);

            result.Total = result.Model.Count();

            return result;
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
        public ReportObject GetReportDetail(int reportId, int userId, DateTimeReport time, DateTime from, DateTime to, int groupId, string treeGroupValue, string groupBy = "", int timekey = 0)
        {
            var report = Get(reportId);
            if (report == null)
            {
                return null;
            }

            var columnConfig = _columnSettingsService.Get(report.DocColumnId);
            if (columnConfig == null)
            {
                return null;
            }
            var group = _reportGroupService.GetGroup(groupId);

            var result = new ReportObject();
            var data = GetDataForReport(report.QueryStatistics, userId, time, from, to, group, treeGroupValue, groupBy, timekey);

            result.ReportId = reportId;
            result.ColumnSettings = columnConfig;
            result.Data = data.DataValues ?? new List<IDictionary<string, object>>();
            result.Description = report.Description;
            result.ReportName = report.Name;
            result.Header = report.QueryTotal;
            result.Footer = report.QueryTotalDocumentIsOverdue;
            result.PivotConfig = report.QueryTotalDocumentProcessed;
            result.ColumnConfig = report.ColumnConfig;
            result.isHCMC = report.IsHSMC;
            return result;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void UpdateCache(string key, ReportObject value)
        {
            _cache.Remove(key);
            value.LastUpdate = DateTime.Now;
            _cache.Set(key, value, CacheParam.ReportCacheTimeOut);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="userId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        private string BuildCacheKey(int reportId, int userId, DateTime from, DateTime to, string groupBy)
        {
            var key = string.Join("_", new string[] {
                            reportId.ToString(), userId.ToString(),
                            from.ToString("yyyyMMdd"), to.ToString("yyyyMMdd"),groupBy });
            return string.Format(CacheParam.ReportKey, key);
        }

    }
}
