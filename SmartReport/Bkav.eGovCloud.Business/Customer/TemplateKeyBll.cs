using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;
using MySql.Data.MySqlClient;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : TemplateKeyBll - public - Bll </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 130313</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm xử lý template key </para>
    /// <para> ( TienBV@bkav.com - 130313) </para>
    /// </summary>
    public class TemplateKeyBll : ServiceBase
    {
        private readonly IRepository<TemplateKey> _templateKeyRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly IRepository<E_DataTable> _dataTableRepository;
        private readonly IRepository<ReportQuery> _reportQueryRepository;
        private readonly IRepository<ReportQueryGroup> _reportQueryGroupRepository;
        private readonly IRepository<DataSource> _dataSourceRepository;
        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        public TemplateKeyBll(IDbCustomerContext context,
            AdminGeneralSettings generalSettings)
            : base(context)
        {
            _templateKeyRepository = Context.GetRepository<TemplateKey>();
            _generalSettings = generalSettings;
            _dataTableRepository = Context.GetRepository<E_DataTable>();
            _reportQueryRepository = Context.GetRepository<ReportQuery>();
            _reportQueryGroupRepository = Context.GetRepository<ReportQueryGroup>();
            _dataSourceRepository = Context.GetRepository<DataSource>();
        }

        /// <summary>
        /// Trả về danh sách các template key điều kiện kỹ thuật truyền vào. Kết quả chỉ đọc
        /// (Tienbv@bkav.com 200313)
        /// </summary>
        /// <param name="spec">The spec</param>
        /// <returns></returns>
        public IEnumerable<TemplateKey> Gets(Expression<Func<TemplateKey, bool>> spec = null)
        {
            return _templateKeyRepository.GetsReadOnly(spec);
        }

        public TemplateKey Get(Expression<Func<TemplateKey, bool>> spec = null)
        {
            return _templateKeyRepository.Get(true, spec);
        }

        /// <summary>
        /// Trả về tất cả template key có phân trang và sort. Kết quả chỉ đọc
        /// <para>(Tienbv@bkav.com 130313)</para>
        /// </summary>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="projector"></param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số phần tử trong trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending">true: lớn -> nhỏ, false: ngược lại</param>
        /// <param name="keySearch">search theo tên</param>
        /// <returns>Danh sách các template tương ứng</returns>
        public IEnumerable<T> GetsAs<T>(out int totalRecords,
                                        Expression<Func<TemplateKey, T>> projector,
                                        int currentPage = 1,
                                        int? pageSize = null,
                                        string sortBy = "",
                                        bool isDescending = false,
                                        string keySearch = "",
                                        int? type = null)
        {

            // 20200210 VuHQ START Phase 2 - REQ 0
            //var spec = !string.IsNullOrWhiteSpace(keySearch)
            //            ? TemplateKeyQuery.ContainsKey(keySearch)
            //            : null;
            var spec =
                TemplateKeyQuery.ContainsKey(keySearch)
                .And(TemplateKeyQuery.WithType(type));
            // 20200210 VuHQ END Phase 2 - REQ 0

            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _templateKeyRepository.Count(spec);
            var sort = Context.Filters.CreateSort<TemplateKey>(isDescending, sortBy);
            return _templateKeyRepository.GetsAs(projector, spec, sort, Context.Filters.Page<TemplateKey>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Trả về template key theo id.
        /// <para>(Tienbv@bkav.com 130313)</para>
        /// </summary>
        /// <param name="id">The template key id</param>
        /// <returns></returns>
        public TemplateKey Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return _templateKeyRepository.Get(id);
        }

        /// <summary>
        /// Thêm key
        /// </summary>
        /// <param name="entity"></param>
        public void Create(TemplateKey entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (!Exist(entity.Code))
            {
                _templateKeyRepository.Create(entity);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Xóa templale key.
        /// <para>(Tienbv@bkav.com 130313)</para>
        /// </summary>
        /// <param name="entity">Template key entity</param>
        public void Delete(TemplateKey entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _templateKeyRepository.Delete(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cập nhật template key.
        /// <para>(Tienbv@bkav.com 130313)</para>
        /// </summary>
        /// <param name="entity">Template key entity</param>
        public void Update(TemplateKey entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Kiểm tra đã tồn tại mã key hay chưa.
        /// </summary>
        /// <param name="code">Mã key</param>
        /// <returns></returns>
        public bool Exist(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("Mã key không được phép trống!");
            }
            var spec = TemplateKeyQuery.ExistCode(code);
            return _templateKeyRepository.Exist(spec);
        }

        /// <summary>
        /// Trả về template key theo mã key
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public TemplateKey Get(string keyCode)
        {
            return _templateKeyRepository.Get(false, t => t.Code == keyCode && t.IsActive);
        }

        /// <summary>
        /// Lấy giá trị thật của 1 key.
        /// <para>(Tienbv@bkav.com 210313)</para>
        /// </summary>
        /// <param name="key">Template key cần lấy giá trị.</param>
        /// <param name="userId">User id.</param>
        /// <param name="docId">Document id</param>
        /// <param name="formId">Form Id</param>
        /// <param name="paperAddIds">Danh sách các giấy tờ bổ sung.</param>
        /// <param name="feeAddIds">Danh sách các lệ phí bổ sung</param>
        /// <param name="docCopyId">docCopyId</param>
        /// <param name="currentUserId">Id người đang giữ</param>
        /// <returns>Danh sách các dictionary </returns>
        public IEnumerable<IDictionary<string, object>> GetValue(TemplateKey key, int userId,
            Guid docId, Guid? formId, string paperAddIds = null, string feeAddIds = null, int docCopyId = 0, int currentUserId = 0)
        {
            if (key == null)
            {
                return new List<IDictionary<string, object>>();
            }
            if (key.IsCustomKey)
            {
                formId = key.FormId;
            }
            if (!string.IsNullOrEmpty(paperAddIds))
            {
                key.Sql = key.Sql.Replace("@PaperAddIds", paperAddIds);
            }
            if (!string.IsNullOrEmpty(feeAddIds))
            {
                key.Sql = key.Sql.Replace("@feeAddIds", feeAddIds);
            }
            var parameters = GetSqlParameters(userId, docId, formId, key.KeyIdInForm, paperAddIds, feeAddIds, docCopyId, 0, currentUserId);
            return GetValue(key, parameters.ToArray());
        }

        public IEnumerable<IDictionary<string, object>> GetValueByQuery(TemplateKey templateKey, object[] parameters = null)
        {
            var query = templateKey.Sql;
            var result = Context.RawQuery(query, parameters) as IEnumerable<IDictionary<string, object>>;
            return result;
        }
        public IEnumerable<IDictionary<string, object>> GetListByQuery(TemplateKey templateKey, object[] parameters = null)
        {

            var query = templateKey.Sql;
            if (string.IsNullOrWhiteSpace(query))
            {
                return null;
            }
            query = query.Trim();
            var isConnectDashboard = query.StartsWith("dashboard:", StringComparison.OrdinalIgnoreCase);
            if (true)
            {
                var connection = _generalSettings.DashboardConnection;
                if (!string.IsNullOrWhiteSpace(templateKey.ReportQueryId))
                {
                    int id, tableId = 0;
                    int.TryParse(Regex.Match(templateKey.ReportQueryId, @"\d+").Value, out id);
                    if (templateKey.ReportQueryId.Contains("query"))
                    {
                        var report = _reportQueryRepository.Get(id);
                        tableId = report == null ? 0 : report.DataTableId ?? 0;
                    }
                    else if (templateKey.ReportQueryId.Contains("group"))
                    {
                        var reportQueryGroup = _reportQueryGroupRepository.Get(id);
                        var reportQueryIds = reportQueryGroup.Querys.Select(p => p.ReportQueryId).ToList();
                        if (reportQueryIds.Count > 0)
                        {
                            var reportTmp = _reportQueryRepository.Get(reportQueryIds[0]);
                            tableId = reportTmp == null ? 0 : reportTmp.DataTableId ?? 0;
                        }
                    }
                    var dataTable = _dataTableRepository.Get(tableId);
                    if (dataTable != null)
                    {
                        var dataSource = _dataSourceRepository.Get(dataTable.DataSourceId);
                        if (dataSource != null && !string.IsNullOrWhiteSpace(dataSource.ConnectionString))
                            connection = dataSource.ConnectionString;
                    }
                }
                query = query.Replace("dashboard:", "");
                using (var context = new EfContext(new MySqlConnection(connection)))
                {
                    try
                    {
                        var command = "";
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
                    catch (Exception ex)
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

        /// <summary>
        /// Hàm sử dụng chung để query
        /// </summary>
        /// <param name="templateKey"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<IDictionary<string, object>> GetDataByQuery(string qry, object[] parameters = null)
        {

            var query = qry;
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
                    catch (Exception ex)
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

        private IEnumerable<IDictionary<string, object>> GetValue(TemplateKey templateKey, object[] parameters = null)
        {
            var query = templateKey.Sql;
            var result = Context.RawQuery(query, parameters) as IEnumerable<IDictionary<string, object>>;
            return result;
        }

        private List<object> GetSqlParameters(int userId, Guid docId, Guid? formId, Guid? controlId,
            string paperAddIds = null, string feeAddIds = null, int docCopyId = 0, int suppId = 0, int currentUserId = 0)
        {
            var result = new List<object> { new SqlParameter("@UserId", userId), new SqlParameter("@DocId", docId) };
            if (controlId != null)
            {
                result.Add(new SqlParameter("@ControlId", controlId));
            }
            if (formId != null)
            {
                result.Add(new SqlParameter("@FormId", formId));
            }
            if (docCopyId != 0)
            {
                result.Add(new SqlParameter("@DocumentCopyId", docCopyId));
            }
            //if (calendarId != 0)
            //{
            //    result.Add(new SqlParameter("@calendarId", calendarId));
            //}
            if (suppId != 0)
            {
                result.Add(new SqlParameter("@SuppId", suppId));
            }
            if (currentUserId != 0)
            {
                result.Add(new SqlParameter("@currentUserId", currentUserId));
            }

            return result;
        }
        public string GetQuery(string timekey, string organizeKey, string tablename, string typeTime)
        {
            var query = @"dashboard: SELECT depart.organizekey, org.organizekey reported, depart.organizename from dim_organize_standard depart 
                        LEFT JOIN (SELECT bc.organizekey  from {0} 
                        bc WHERE bc.{1}  = @timekey GROUP BY bc.organizekey) org  on depart.organizekey = org.organizekey WHERE parent = @organize and 
                        (depart.organizename LIKE 'Xã%' or depart.organizename LIKE 'Phường%' or depart.organizename LIKE 'Thị trấn%' 
                        or depart.organizename LIKE 'Huyện%' or depart.organizename LIKE 'Thị xã%'  or depart.organizename LIKE 'Thành phố%' )";
            query = string.Format(query, tablename, typeTime, timekey, organizeKey);
            return query;
        }
    }
}
