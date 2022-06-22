using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.FileTransfer;
using Bkav.eGovCloud.Core.Fakes;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : BackupRestoreManagerBll - public - Bll </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : HopCV@bkav.com </para>
    /// </author>
    /// </summary>
    public class BackupRestoreManagerBll : ServiceBase
    {
        private readonly IRepository<BackupRestoreManager> _backupRestoreManagerRepository;
        private readonly AdminGeneralSettings _generalSettings;
        private readonly FileLocationBll _fileLocationService;
        private readonly FileLocationSettings _fileLocationSettings;
        private readonly HttpContextBase _context;
        private readonly BackupRestoreHistoryBll _backupRestoreHistoryService;
        private readonly ResourceBll _resourceService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="generalSettings"></param>
        /// <param name="fileLocationService"></param>
        /// <param name="fileLocationSettings"></param>
        /// <param name="backupRestoreHistoryService"></param>
        /// <param name="resourceService"></param>
        public BackupRestoreManagerBll(
            IDbCustomerContext context,
            AdminGeneralSettings generalSettings,
            FileLocationBll fileLocationService,
            FileLocationSettings fileLocationSettings,
            BackupRestoreHistoryBll backupRestoreHistoryService,
            ResourceBll resourceService)
            : base(context)
        {
            _backupRestoreManagerRepository = Context.GetRepository<BackupRestoreManager>();
            _generalSettings = generalSettings;
            _fileLocationService = fileLocationService;
            _fileLocationSettings = fileLocationSettings;
            _context = HttpContext.Current != null
                   ? (new HttpContextWrapper(HttpContext.Current)) as HttpContextBase
                   : new FakeHttpContext("~/");

            _backupRestoreHistoryService = backupRestoreHistoryService;
            _resourceService = resourceService;
        }

        /// <summary>
        ///  Tạo mới cấu hình backup database
        /// </summary>
        /// <param name="backupRestoreManager"></param>
        public void Create(BackupRestoreManager backupRestoreManager)
        {
            if (backupRestoreManager == null)
            {
                throw new ArgumentNullException("backupRestoreManager");
            }

            _backupRestoreManagerRepository.Create(backupRestoreManager);
            Context.SaveChanges();
        }

        /// <summary>
        /// Tao mới quản lý các tệp tin sao lưu 
        /// </summary>
        /// <param name="stream">Luồng tin</param>
        /// <param name="domain">Tên miền</param>
        /// <param name="filenameAlias">Tên gốc của tệp tin</param>
        /// <param name="zipPassword">Mật khẩu mã hóa khi zip file</param>
        /// <param name="description">Mô tả</param>
        /// <param name="alias">Định danh</param>
        /// <param name="isDatabaseFile">True: là tệp tin CSDl, False: tệp tin thông thường</param>
        /// <param name="dateCreated">Ngày tạo</param>
        /// <param name="account">NGười tạo</param>
        public void Create(Stream stream,
            string domain, string filenameAlias,
            string zipPassword, string description,
            string alias, bool isDatabaseFile,
            DateTime dateCreated,
            string account = "System")
        {
            if (stream == null)
                throw new ArgumentNullException("stream is null.");

            var currentFileLocation = _fileLocationService.GetCurrent();
            var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
            var fileInfo = transfer.Upload(stream, FileType.FileRepository);
            var backupRestoreManager = new BackupRestoreManager
            {
                DateCreated = dateCreated,
                Description = description,
                Domain = domain,
                Account = account,
                IsDatabaseFile = isDatabaseFile,
                Alias = alias,
                ZipPassword = zipPassword,

                FileLocationId = currentFileLocation.FileLocationId,
                FileLocationKey = currentFileLocation.FileLocationAddress,
                FileName = fileInfo.FileName,
                FileNameAlias = filenameAlias,
                Size = (int)fileInfo.Length,
                IdentityFolder = fileInfo.IdentityFolder,
            };

            Create(backupRestoreManager);
        }

        /// <summary>
        /// Xóa 1 nhật ký
        /// </summary>
        /// <param name="backupRestoreManager">Thực thể nhật ký</param>
        /// <param name="isDeleteDatabase">true: có xóa bản ghi trong database, false: không xóa trong database</param>
        public void Delete(BackupRestoreManager backupRestoreManager, bool isDeleteDatabase = true)
        {
            if (backupRestoreManager == null)
            {
                throw new ArgumentNullException("backupRestoreManager");
            }

            //Xóa file sao lưu
            DeleteFile(backupRestoreManager);
            if (isDeleteDatabase)
            {
                _backupRestoreManagerRepository.Delete(backupRestoreManager);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Xóa  nhật ký
        /// </summary>
        /// <param name="backupRestoreManagers">Thực thể nhật ký</param>
        /// <param name="isDeleteDatabase">true: có xóa bản ghi trong database, false: không xóa trong database</param>
        public void Delete(IEnumerable<BackupRestoreManager> backupRestoreManagers, bool isDeleteDatabase = true)
        {
            if (backupRestoreManagers == null || !backupRestoreManagers.Any())
            {
                throw new ArgumentNullException("backupRestoreManagers");
            }

            foreach (var backupRestoreManager in backupRestoreManagers)
            {
                //Xóa file sao lưu
                DeleteFile(backupRestoreManager);
                if (isDeleteDatabase)
                {
                    _backupRestoreManagerRepository.Delete(backupRestoreManager);
                }
            }

            if (isDeleteDatabase)
            {
                Context.SaveChanges();
            }
        }

        /// <summary>
        ///Cập nhật đối tượng backup
        /// </summary>
        /// <param name="backupRestoreManager"></param>
        public void Update(BackupRestoreManager backupRestoreManager)
        {
            if (backupRestoreManager == null)
            {
                throw new ArgumentNullException("backupRestoreManager");
            }

            Context.SaveChanges();
        }

        /// <summary>
        ///Cập nhật đối tượng backup
        /// </summary>
        /// <param name="backupRestoreManager"></param>
        /// <param name="stream">File up load</param>
        /// <param name="hasDeleteOldFile">Có xóa file cũ hay không</param>
        public void Update(BackupRestoreManager backupRestoreManager, Stream stream, bool hasDeleteOldFile = false)
        {
            if (backupRestoreManager == null)
            {
                throw new ArgumentNullException("backupRestoreManager");
            }

            if (stream != null)
            {
                if (hasDeleteOldFile)
                {
                    DeleteFile(backupRestoreManager);
                }

                var currentFileLocation = _fileLocationService.GetCurrent();
                var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
                var fileInfo = transfer.Upload(stream, FileType.FileRepository);

                backupRestoreManager.FileLocationId = currentFileLocation.FileLocationId;
                backupRestoreManager.FileLocationKey = currentFileLocation.FileLocationAddress;
                backupRestoreManager.FileName = fileInfo.FileName;
                backupRestoreManager.Size = (int)fileInfo.Length;
                backupRestoreManager.IdentityFolder = fileInfo.IdentityFolder;
            }

            Context.SaveChanges();
        }

        /// <summary>
        ///  Lấy đối tượng backup
        /// </summary>
        /// <param name="backupRestoreManagerId"></param>
        /// <returns></returns>
        public BackupRestoreManager Get(int backupRestoreManagerId)
        {
            return _backupRestoreManagerRepository.Get(false, p => p.BackupRestoreManagerId == backupRestoreManagerId);
        }

        /// <summary>
        /// Trả về danh cách các cấu hinh chỉ để đọc
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<BackupRestoreManager> GetsReadOnly(Expression<Func<BackupRestoreManager, bool>> spec = null)
        {
            return _backupRestoreManagerRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Trả về danh cách
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<BackupRestoreManager> Gets(Expression<Func<BackupRestoreManager, bool>> spec = null)
        {
            return _backupRestoreManagerRepository.Gets(false, spec);
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
        /// <param name="isDatabase">True: database , False: file</param>
        /// <returns>Danh sách nhật ký</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(
            out int totalRecords, Expression<Func<BackupRestoreManager, TOutput>> projector,
            int currentPage = 1, int? pageSize = null, string sortBy = "", bool isDescending = true,
            DateTime? from = null, DateTime? to = null, bool? isDatabase = null)
        {
            Expression<Func<BackupRestoreManager, bool>> spec =
                            p =>
                                (!from.HasValue || (from.HasValue && from.Value <= p.DateCreated))
                                && (!to.HasValue || (to.HasValue && to.Value >= p.DateCreated))
                                && (!isDatabase.HasValue || (isDatabase.HasValue && p.IsDatabaseFile == isDatabase.Value));

            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _backupRestoreManagerRepository.Count(spec);
            var sort = Context.Filters.CreateSort<BackupRestoreManager>(isDescending, sortBy);

            return _backupRestoreManagerRepository.GetsAs(projector, spec,
                sort, Context.Filters.Page<BackupRestoreManager>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Lấy ra tất cả nhật ký phù hợp với điều kiện truyền vào. Kết quả được ánh xạ sang một dạng khác bằng cách sử dụng một mapper do người dùng cung cấp..Kết quả chỉ để đọc
        /// </summary>
        /// <typeparam name="TOutput">Kiểu đầu ra.</typeparam>
        /// <param name="isDescending">Sắp xếp từ lớn đến nhỏ: true, ngược lại false</param>
        /// <param name="totalRecords">Tổng số bản ghi</param>
        /// <param name="projector">Là một công cụ để ánh xạ từ 1 kiểu thực thể sang 1 kiểu thực thể khác (nó tương đương Select column1, column2 From Table)</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="spec">True: database , False: file</param>
        /// <returns>Danh sách nhật ký</returns>
        public IEnumerable<TOutput> GetsAs<TOutput>(
            out int totalRecords, Expression<Func<BackupRestoreManager, TOutput>> projector,
            Expression<Func<BackupRestoreManager, bool>> spec,
            int currentPage = 1, int? pageSize = null, string sortBy = "", bool isDescending = true)
        {
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _backupRestoreManagerRepository.Count(spec);
            var sort = Context.Filters.CreateSort<BackupRestoreManager>(isDescending, sortBy);

            return _backupRestoreManagerRepository.GetsAs(projector, spec,
                sort, Context.Filters.Page<BackupRestoreManager>(currentPage, pageSize.Value));
        }

        /// <summary>
        /// Tải tệp sao lưu
        /// </summary>
        /// <param name="backupRestoreManager"></param>
        /// <returns></returns>
        public Stream Download(BackupRestoreManager backupRestoreManager)
        {
            if (backupRestoreManager == null)
            {
                throw new ArgumentNullException("embryonicForm");
            }

            var fileLocation = backupRestoreManager.FileLocation;
            if (fileLocation == null)
            {
                throw new Exception("fileLocation is not exist.");
            }

            var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
            var downloaded = transfer.Download(new FileTransferInfo
                {
                    CreatedDate = backupRestoreManager.DateCreated,
                    FileName = backupRestoreManager.FileName,
                    FileType = FileType.FileRepository,
                    IdentityFolder = backupRestoreManager.IdentityFolder,
                    RootFolder = backupRestoreManager.FileLocationKey
                });

            return downloaded;
        }

        /// <summary>
        /// Tải tệp về
        /// </summary>
        /// <param name="backupRestoreManagerId">id</param>
        /// <returns>Trả về 1 stream</returns>
        public Stream Download(int backupRestoreManagerId)
        {
            var backupRestoreManager = Get(backupRestoreManagerId);
            return Download(backupRestoreManager);
        }

        /// <summary>
        /// Xóa tệp sao lưu
        /// </summary>
        /// <param name="backupRestoreManager"></param>
        private void DeleteFile(BackupRestoreManager backupRestoreManager)
        {
            var currentFileLocation = _fileLocationService.Get(backupRestoreManager.FileLocationId);
            if (currentFileLocation != null)
            {
                var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
                transfer.Delete(new FileTransferInfo
                {
                    FileName = backupRestoreManager.FileName,
                    FileType = FileType.FileRepository,
                    CreatedDate = backupRestoreManager.DateCreated,
                    IdentityFolder = backupRestoreManager.IdentityFolder,
                    RootFolder = backupRestoreManager.FileLocationKey,
                    Length = backupRestoreManager.Size
                });
            }
        }
    }
}
