using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ResourceBll - public - BLL
    /// Access Modifiers: 
    /// Create Date : 150414
    /// Author      : TrungVH
    /// Description : BLL tương ứng với bảng ActivityLog trong CSDL
    /// </summary>
    public class ActivityLogBll : ServiceBase
    {
        private readonly IRepository<ActivityLog> _logRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly UserBll _userService;

        /// <summary>
        /// Khởi tạo class <see cref="ActivityLogBll"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        /// <param name="userService">Bll tương ứng với bảng User trong CSDL</param>
        public ActivityLogBll(IDbCustomerContext context,
            AdminGeneralSettings generalSettings, UserBll userService)
            : base(context)
        {
            _logRepository = Context.GetRepository<ActivityLog>();
            _generalSettings = generalSettings;
            _userService = userService;
        }

        /// <summary>
        /// Xóa 1 nhật ký
        /// </summary>
        /// <param name="log">Thực thể nhật ký</param>
        public void Delete(ActivityLog log)
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
        public void Clear(List<int> logids)
        {
            var logs = _logRepository.Gets(false, x => logids.Contains(x.ActivityLogId));
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
        /// Lấy ra tất cả nhật ký phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..Kết quả chỉ để đọc
        /// </summary>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="from">Nhật ký được tạo ra từ ngày; để null sẽ bỏ qua điều kiện này</param>
        /// <param name="to">Nhật ký được tạo ra đến ngày; để null sẽ bỏ qua điều kiện này</param>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="activityLogType">Loại nhật ký</param>
        /// <param name="findByUser">Từ tìm kiếm</param>
        /// <returns>Danh sách nhật ký</returns>
        public IEnumerable<ActivityLog> Gets(out int totalRecords, int currentPage = 1,
            int? pageSize = null, string sortBy = "",
            bool isDescending = true,
            DateTime? from = null, DateTime? to = null,
            byte? activityLogType = null,
            string findByUser = "")
        {
            Expression<Func<ActivityLog, bool>> spec =
                l =>
                    (from == null || from.Value <= l.CreatedOnDate)
                    && (to == null || to.Value >= l.CreatedOnDate)
                    && (activityLogType == null || l.ActivityLogType == activityLogType);

            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            if (!string.IsNullOrWhiteSpace(findByUser))
            {
                spec = spec.And(p => p.UserName.Contains(findByUser));
            }

            totalRecords = _logRepository.Count(spec);
            var sort = Context.Filters.CreateSort<ActivityLog>(isDescending, sortBy);

            return _logRepository.GetsReadOnly(spec, sort, Context.Filters.Page<ActivityLog>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Lấy ra một nhật ký
        /// </summary>
        /// <param name="logId">Id của nhật ký</param>
        /// <returns>Entity nhật ký</returns>
        public ActivityLog Get(int logId)
        {
            ActivityLog log = null;
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
        /// <param name="content">Nội dung chi tiết</param>
        /// <param name="ip">Ip</param>
        /// <param name="username">Tên đăng nhập</param>
        /// <param name="userId">Id người đăng nhập</param>
        /// <returns>Thực thể nhật ký</returns>
        public ActivityLog Create(ActivityLogType logType, string content, string ip, string username, int? userId)
        {
            var log = new ActivityLog
            {
                ActivityLogTypeInEnum = logType,
                Ip = ip,
                Content = content,
                UserId = userId,
                UserName = username,
                CreatedOnDate = DateTime.Now
            };
            _logRepository.Create(log);
            Context.SaveChanges();

            return log;
        }

        /// <summary>
        /// Xóa tất cả nhật ký có id nằm trong logids
        /// </summary>
        public void Delete(List<int> logids)
        {
            var logs = _logRepository.Gets(false, x => logids.Contains(x.ActivityLogId));
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
    }
}
