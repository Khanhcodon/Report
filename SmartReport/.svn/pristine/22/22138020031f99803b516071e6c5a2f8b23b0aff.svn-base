using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Bkav.eGovCloud.Core.Fakes;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.Business.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ResourceBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 270812
    /// Author      : TrungVH
    /// Description : BLL tương ứng với bảng Log trong CSDL
    /// </summary>
    public class LogBll : ServiceBase
    {
        private readonly IRepository<Log> _logRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly HttpContextBase _context;

        ///// <summary>
        ///// Khởi tạo class <see cref="LogBll"/>.
        ///// </summary>
        ///// <param name="context">Admin context</param>
        ///// <param name="generalSettings">Cấu hình chung</param>
        //public LogBll(IDbAdminContext context, AdminGeneralSettings generalSettings)
        //    : base(context)
        //{
        //    _logRepository = Context.GetRepository<Log>();
        //    _generalSettings = generalSettings;
        //    _context = HttpContext.Current != null
        //                ? (new HttpContextWrapper(HttpContext.Current)) as HttpContextBase
        //                : new FakeHttpContext("~/");
        //}

        /// <summary>
        /// Khởi tạo class <see cref="LogBll"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        public LogBll(IDbCustomerContext context, AdminGeneralSettings generalSettings)
            : base(context)
        {
            _logRepository = Context.GetRepository<Log>();
            _generalSettings = generalSettings;
            _context = HttpContext.Current != null
                        ? (new HttpContextWrapper(HttpContext.Current)) as HttpContextBase
                        : new FakeHttpContext("~/");
        }

        /// <summary>
        /// Xóa 1 nhật ký
        /// </summary>
        /// <param name="log">Thực thể nhật ký</param>
        public void DeleteLog(Log log)
        {
            if (log == null)
            {
                throw new ArgumentNullException("log");
            }
            _logRepository.Delete(log);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa tất cả nhật ký có id nằm trong logids
        /// </summary>
        public void ClearLog(List<int> logids)
        {
            var logs = _logRepository.Gets(false, x => logids.Contains(x.LogId));
            if (logs == null || (!logs.Any()))
            {
                return;
            }
            foreach (var log in logs)
            {
                _logRepository.Delete(log);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa toàn bộ nhật ký
        /// </summary>
        public void ClearLog()
        {
            Context.RawModify("TRUNCATE TABLE log");
        }

        /// <summary>
        /// Lấy ra tất cả nhật ký phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..Kết quả chỉ để đọc
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="from">Nhật ký được tạo ra từ ngày; để null sẽ bỏ qua điều kiện này</param>
        /// <param name="to">Nhật ký được tạo ra đến ngày; để null sẽ bỏ qua điều kiện này</param>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <returns>Danh sách nhật ký</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(out int totalRecords, Expression<Func<Log, TOutput>> projector, int currentPage = 1,
                                                int? pageSize = null, string sortBy = "", bool isDescending = true,
                                                DateTime? from = null, DateTime? to = null)
        {
            Expression<Func<Log, bool>> spec = null;
            if (from.HasValue)
            {
                spec = l => from.Value <= l.CreatedOnDate;
            }
            if (spec != null)
            {
                if (to.HasValue)
                {
                    spec = spec.And(l => to.Value >= l.CreatedOnDate);
                }
            }
            else
            {
                if (to.HasValue)
                {
                    spec = l => to.Value >= l.CreatedOnDate;
                }
            }
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }
            totalRecords = _logRepository.Count(spec);
            var sort = Context.Filters.CreateSort<Log>(isDescending, sortBy);

            return _logRepository.GetsAs(projector, spec, sort, Context.Filters.Page<Log>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Lấy ra một nhật ký
        /// </summary>
        /// <param name="logId">Id của nhật ký</param>
        /// <returns>Entity nhật ký</returns>
        public Log Get(int logId)
        {
            Log log = null;
            if (logId > 0)
            {
                log = _logRepository.Get(logId);
            }
            return log;
        }

        /// <summary>
        /// Thêm mới nhật ký
        /// </summary>
        /// <param name="logType">Loại nhật ký</param>
        /// <param name="shortMessage">Nội dung tóm tắt</param>
        /// <param name="fullMessage">Nội dung chi tiết</param>
        /// <param name="isLogRequest">Ghi lại các tham số của request: true và ngược lại : false</param>
        /// <returns>Thực thể nhật ký</returns>
        public Log InsertLog(LogType logType, string shortMessage, string fullMessage, bool isLogRequest = true)
        {
            string ip = string.Empty, requestJson = null;
            if (isLogRequest)
            {
                if (_context.Request != null)
                {
                    if (_context.Request.UserHostAddress != null)
                    {
                        ip = _context.Request.UserHostAddress;
                    }
                    var requestCollection = new Dictionary<string, IDictionary<string, object>>(4)
                                        {
                                            { "ServerVariables", CopyToDictionary(_context.Request.ServerVariables) },
                                            { "Form", CopyToDictionary(_context.Request.Form) },
                                            { "QueryString", CopyToDictionary(_context.Request.QueryString) },
                                            { "Cookies", CopyToDictionary(_context.Request.Cookies) },
                                        };
                    requestJson = requestCollection.StringifyJs();
                }
            }

            var log = new Log
            {
                LogType = (int)logType,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                IpAddress = ip,
                RequestJson = requestJson,
                CreatedOnDate = DateTime.Now
            };
            _logRepository.Create(log);
            Context.SaveChanges();

            return log;
        }

        private static IDictionary<string, object> CopyToDictionary(ICollection collection)
        {
            if (collection == null || collection.Count == 0)
                return null;
            return collection.ToDictionary();
        }

        private static IDictionary<string, object> CopyToDictionary(HttpCookieCollection cookies)
        {
            if (cookies == null || cookies.Count == 0)
                return null;
            var dictionary = new Dictionary<string, object>(cookies.Count);
            for (var index = 0; index < cookies.Count; ++index)
            {
                try
                {
                    var httpCookie = cookies[index];
                    dictionary.Add(httpCookie.Name, httpCookie.Value);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return dictionary;
        }
    }
}
