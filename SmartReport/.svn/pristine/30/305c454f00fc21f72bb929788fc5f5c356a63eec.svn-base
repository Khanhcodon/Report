using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Bkav.eGovCloud.Business.FileTransfer;
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
    /// <para> Class : BackupRestoreHistoryBll - public - Bll </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : HopCV@bkav.com </para>
    /// </author>
    /// <summary>
    /// </summary>
    public class BackupRestoreHistoryBll : ServiceBase
    {
        private readonly IRepository<BackupRestoreHistory> _backupRestoreHistoryRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly HttpContextBase _context;
        private readonly FileLocationBll _fileLocationService;
        private readonly FileLocationSettings _fileLocationSettings;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="generalSettings">Cấu hình chung</param>
        /// <param name="fileLocationService"></param>
        /// <param name="fileLocationSettings"></param>
        public BackupRestoreHistoryBll(IDbCustomerContext context,
            AdminGeneralSettings generalSettings,
            FileLocationBll fileLocationService,
             FileLocationSettings fileLocationSettings)
            : base(context)
        {
            _backupRestoreHistoryRepository = Context.GetRepository<BackupRestoreHistory>();
            _generalSettings = generalSettings;
            _context = HttpContext.Current != null
                       ? (new HttpContextWrapper(HttpContext.Current)) as HttpContextBase
                       : new FakeHttpContext("~/");
            _fileLocationService = fileLocationService;
            _fileLocationSettings = fileLocationSettings;

        }

        /// <summary>
        ///  Tạo mới cấu hình backup database
        /// </summary>
        /// <param name="backupRestoreHistory"></param>
        public void Create(BackupRestoreHistory backupRestoreHistory)
        {
            if (backupRestoreHistory == null)
            {
                throw new ArgumentNullException("backupRestoreHistory");
            }

            _backupRestoreHistoryRepository.Create(backupRestoreHistory);
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

            var log = new BackupRestoreHistory
            {
                Domain = domain,
                Ip = ip,
                DateCreated = dateCreated,
                Description = description,
                IsBackup = isBackup,
                Account = account,
                IsSuccessed = isSuccessed
            };

            Create(log);
        }

        /// <summary>
        /// Xóa 1 nhật ký
        /// </summary>
        /// <param name="backupRestoreHistory">Thực thể nhật ký</param>
        public void Delete(BackupRestoreHistory backupRestoreHistory)
        {
            if (backupRestoreHistory == null)
            {
                throw new ArgumentNullException("backupRestoreHistory");
            }
            _backupRestoreHistoryRepository.Delete(backupRestoreHistory);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa  nhật ký
        /// </summary>
        /// <param name="backupRestoreHistorys"></param>
        public void Delete(IEnumerable<BackupRestoreHistory> backupRestoreHistorys)
        {
            if (backupRestoreHistorys == null || !backupRestoreHistorys.Any())
            {
                throw new ArgumentNullException("backupRestoreHistorys");
            }

            foreach (var backupRestoreHistory in backupRestoreHistorys)
            {
                _backupRestoreHistoryRepository.Delete(backupRestoreHistory);
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa tất cả nhật ký có id nằm trong logids
        /// </summary>
        public void ClearLog(List<int> logids)
        {
            var logs = _backupRestoreHistoryRepository.Gets(false, x => logids.Contains(x.BackupRestoreHistoryId));
            if (logs == null || (!logs.Any()))
            {
                return;
            }

            foreach (var log in logs)
            {
                _backupRestoreHistoryRepository.Delete(log);
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
        public void Update(BackupRestoreHistory backupRestoreHistory)
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
        public BackupRestoreHistory Get(int backupRestoreHistoryId)
        {
            return _backupRestoreHistoryRepository.Get(false, p => p.BackupRestoreHistoryId == backupRestoreHistoryId);
        }

        /// <summary>
        /// Trả về danh cách các cấu hinh chỉ để đọc
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<BackupRestoreHistory> GetsReadOnly(Expression<Func<BackupRestoreHistory, bool>> spec = null)
        {
            return _backupRestoreHistoryRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Trả về danh cách
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<BackupRestoreHistory> Gets(Expression<Func<BackupRestoreHistory, bool>> spec = null)
        {
            return _backupRestoreHistoryRepository.Gets(false, spec);
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
        /// <param name="search"></param>
        /// <param name="type"></param>
        /// <returns>Danh sách nhật ký</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(out int totalRecords,
            Expression<Func<BackupRestoreHistory, TOutput>> projector, int currentPage = 1,
            int? pageSize = null, string sortBy = "", bool isDescending = true,
            DateTime? from = null, DateTime? to = null,
            string search = null, bool? type = null)
        {
            Expression<Func<BackupRestoreHistory, bool>> spec =
                             p =>
                                (!from.HasValue || (from.HasValue && from.Value <= p.DateCreated))
                                && (!to.HasValue || (to.HasValue && to.Value >= p.DateCreated))
                                && (!type.HasValue || (type.HasValue && p.IsBackup == type.Value));

            if (!string.IsNullOrEmpty(search))
            {
                if (spec != null)
                {
                    spec = spec.And(p => p.Domain.Contains(search));
                }
                else
                {
                    spec = p => p.Domain.Contains(search);
                }
            }

            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _backupRestoreHistoryRepository.Count(spec);
            var sort = Context.Filters.CreateSort<BackupRestoreHistory>(isDescending, sortBy);

            return _backupRestoreHistoryRepository.GetsAs(projector, spec, sort, Context.Filters.Page<BackupRestoreHistory>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="projector"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="search"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(
                             Expression<Func<BackupRestoreHistory, TOutput>> projector,
                             DateTime? from = null, DateTime? to = null,
                             string search = null, bool? type = null)
        {
            Expression<Func<BackupRestoreHistory, bool>> spec =
                             p =>
                                (!from.HasValue || (from.HasValue && from.Value <= p.DateCreated))
                                && (!to.HasValue || (to.HasValue && to.Value >= p.DateCreated))
                                && (!type.HasValue || (type.HasValue && p.IsBackup == type.Value));

            if (!string.IsNullOrEmpty(search))
            {
                if (spec != null)
                {
                    spec = spec.And(p => p.Domain.Contains(search));
                }
                else
                {
                    spec = p => p.Domain.Contains(search);
                }
            }

            return _backupRestoreHistoryRepository.GetsAs(projector, spec);
        }

        /// <summary>
        /// Cập nhật cấu hình file crystal để xuất danh sách văn bản ra file (word, excell)
        /// </summary>
        /// <param name="streamFileCrystal">Luồng file crystal</param>
        /// <param name="fileName">Tên file crystal</param>
        /// <param name="oldFile">Id node</param>
        /// <param name="deleteOldFile"></param>
        public HistoryFile Upload(System.IO.Stream streamFileCrystal,
            string fileName, HistoryFile oldFile, bool deleteOldFile = true)
        {
            if (streamFileCrystal == null || streamFileCrystal.Length <= 0)
            {
                throw new ArgumentNullException("streamFileCrystal is not exist.");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName is empty.");
            }

            var currentFileLocation = _fileLocationService.GetCurrent();
            var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
            using (streamFileCrystal)
            {
                if (deleteOldFile && oldFile != null)
                {
                    try
                    {
                        DeleteTemp(oldFile);
                    }
                    catch { }
                }

                var fileInfo = transfer.Upload(streamFileCrystal, Bkav.eGovCloud.Core.FileSystem.FileType.FileRepository);
                var newFile = new HistoryFile()
                {
                    DateCreated = fileInfo.CreatedDate,
                    FileName = fileInfo.FileName,
                    IdentityFolder = fileInfo.IdentityFolder,
                    FileLocationKey = fileInfo.RootFolder,
                    FileLocationId = currentFileLocation.FileLocationId,
                    RealFileName = fileName
                };
                return newFile;
            }
        }

        /// <summary>
        /// Tai file crystal
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public System.IO.Stream Download(HistoryFile file)
        {
            var fileLocation = _fileLocationService.Get(file.FileLocationId);
            if (fileLocation == null)
            {
                throw new Exception("Không tìm thấy nơi lưu tệp đính kèm");
            }

            var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
            var downloaded =
                transfer.Download(new FileTransferInfo
                {
                    CreatedDate = file.DateCreated,
                    FileName = file.FileName,
                    FileType = Bkav.eGovCloud.Core.FileSystem.FileType.FileRepository,
                    IdentityFolder = file.IdentityFolder,
                    RootFolder = file.FileLocationKey
                });

            return downloaded;
        }

        private void DeleteTemp(HistoryFile file)
        {
            var fileLocation = _fileLocationService.Get(file.FileLocationId);
            if (fileLocation == null)
            {
                throw new Exception("Không tìm thấy nơi lưu tệp đính kèm");
            }

            var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
            transfer.Delete(new FileTransferInfo
            {
                CreatedDate = file.DateCreated,
                FileName = file.FileName,
                FileType = Bkav.eGovCloud.Core.FileSystem.FileType.FileRepository,
                IdentityFolder = file.IdentityFolder,
                RootFolder = file.FileLocationKey
            });
        }
    }
}
