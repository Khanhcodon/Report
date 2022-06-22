using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Bkav.eGovCloud.Core.Fakes;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : ReplaceUser - public - Bll </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : HopCV@bkav.com </para>
    /// </author>
    /// <summary>
    /// </summary>
    public class ReplaceUserBll : ServiceBase
    {
        private readonly IRepository<ReplaceUser> _replaceUserRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly HttpContextBase _context;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        public ReplaceUserBll(IDbCustomerContext context, AdminGeneralSettings generalSettings)
            : base(context)
        {
            _replaceUserRepository = Context.GetRepository<ReplaceUser>();
            _generalSettings = generalSettings;
            _context = HttpContext.Current != null
                       ? (new HttpContextWrapper(HttpContext.Current)) as HttpContextBase
                       : new FakeHttpContext("~/");
        }

        /// <summary>
        ///  Tạo mới cấu hình backup database
        /// </summary>
        /// <param name="backupRestoreHistory"></param>
        public void Create(ReplaceUser backupRestoreHistory)
        {
            if (backupRestoreHistory == null)
            {
                throw new ArgumentNullException("backupRestoreHistory");
            }

            _replaceUserRepository.Create(backupRestoreHistory);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tạo mới lịch sử thao tác sao lưu, phục hồi dữ liêu, tệp tin
        /// </summary>       
        /// <param name="description">Mô tả</param>
        /// <param name="dateCreated">Ngày tạo</param>
        /// <param name="isBackup">True: backup, False : restore</param>
        /// <param name="isSuccessed"></param>
        /// <param name="account"> Người tạo</param>
        /// <param name="domain"></param>
        public void Create(
            string description,
            DateTime dateCreated,
            bool isBackup,
            bool isSuccessed,
            string account,
            string domain = "")
        {
            var ip = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(domain))
                {
                    if (_context.Request != null)
                    {
                        if (_context.Request.UserHostAddress != null)
                        {
                            ip = _context.Request.UserHostAddress;
                        }

                        if (_context.Request.UserHostName != null)
                        {
                            domain = _context.Request.GetDomainName();
                        }
                    }
                }
            }
            catch { }

            //TrinhNVd: De tam
            var log = new ReplaceUser
            {
                DateCreated = dateCreated,

            };

            Create(log);
        }

        /// <summary>
        /// Xóa 1 nhật ký
        /// </summary>
        /// <param name="backupRestoreHistory">Thực thể nhật ký</param>
        public void Delete(ReplaceUser backupRestoreHistory)
        {
            if (backupRestoreHistory == null)
            {
                throw new ArgumentNullException("backupRestoreHistory");
            }
            _replaceUserRepository.Delete(backupRestoreHistory);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa  nhật ký
        /// </summary>
        /// <param name="backupRestoreHistorys"></param>
        public void Delete(IEnumerable<ReplaceUser> backupRestoreHistorys)
        {
            if (backupRestoreHistorys == null || !backupRestoreHistorys.Any())
            {
                throw new ArgumentNullException("backupRestoreHistorys");
            }

            foreach (var backupRestoreHistory in backupRestoreHistorys)
            {
                _replaceUserRepository.Delete(backupRestoreHistory);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa tất cả nhật ký có id nằm trong logids
        /// </summary>
        public void ClearLog(List<int> logids)
        {
            var logs = _replaceUserRepository.Gets(false, x => logids.Contains(x.ReplaceUserId));
            if (logs == null || (!logs.Any()))
            {
                return;
            }

            foreach (var log in logs)
            {
                _replaceUserRepository.Delete(log);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa toàn bộ nhật ký
        /// </summary>
        public void ClearLog()
        {
            Context.RawModify("TRUNCATE TABLE backup_restore_history");
        }

        /// <summary>
        ///Cập nhật đối tượng backup
        /// </summary>
        /// <param name="backupRestoreHistory"></param>
        public void Update(ReplaceUser backupRestoreHistory)
        {
            if (backupRestoreHistory == null)
            {
                throw new ArgumentNullException("backupRestoreHistory");
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy đối tượng backup
        /// </summary>
        /// <param name="backupRestoreHistoryId"></param>
        /// <returns></returns>
        public ReplaceUser Get(int backupRestoreHistoryId)
        {
            return _replaceUserRepository.Get(false, p => p.ReplaceUserId == backupRestoreHistoryId);
        }

        /// <summary>
        /// Trả về danh cách các cấu hinh chỉ để đọc
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<ReplaceUser> GetsReadOnly(Expression<Func<ReplaceUser, bool>> spec = null)
        {
            return _replaceUserRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Trả về danh cách
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<ReplaceUser> Gets(Expression<Func<ReplaceUser, bool>> spec = null)
        {
            return _replaceUserRepository.Gets(false, spec);
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
        public IEnumerable<TOutput> GetsAs<TOutput>(out int totalRecords,
            Expression<Func<ReplaceUser, TOutput>> projector, int currentPage = 1,
            int? pageSize = null, string sortBy = "", bool isDescending = true,
            DateTime? from = null, DateTime? to = null)
        {
            Expression<Func<ReplaceUser, bool>> spec =
                             p =>
                                (!from.HasValue || (from.HasValue && from.Value <= p.DateCreated))
                                && (!to.HasValue || (to.HasValue && to.Value >= p.DateCreated));

            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _replaceUserRepository.Count(spec);
            var sort = Context.Filters.CreateSort<ReplaceUser>(isDescending, sortBy);

            return _replaceUserRepository.GetsAs(projector, spec, sort, Context.Filters.Page<ReplaceUser>(currentPage, pageSize.Value));
        }
    }
}
