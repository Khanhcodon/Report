using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Fakes;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : BackupRestoreFileConfigBll - public - Bll </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 050815</para>
    /// <para> Author : HopCV@bkav.com </para>
    /// </author>
    /// <summary>
    /// </summary>
    public class BackupRestoreFileConfigBll : ServiceBase
    {
        private readonly IRepository<BackupRestoreFileConfig> _backupRestoreConfigFileRepository;
        private readonly BackupRestoreHistoryBll _backupRestoreHistoryService;
        private readonly BackupRestoreManagerBll _backupRestoreManagerService;
        private readonly FileLocationBll _fileLocationService;
        private readonly HttpContextBase _context;
        private readonly ResourceBll _resourceService;
        private const string FORMAT_DATE = "yyyyMMddHHmmss";
        private const string DEFAULT_SORT = "DateCreated";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="backupRestoreHistoryService"></param>
        /// <param name="backupRestoreManagerService"></param>
        /// <param name="fileLocationService"></param>
        /// <param name="resourceService"></param>
        public BackupRestoreFileConfigBll(IDbCustomerContext context,
            BackupRestoreHistoryBll backupRestoreHistoryService,
            BackupRestoreManagerBll backupRestoreManagerService,
            FileLocationBll fileLocationService,
            ResourceBll resourceService)
            : base(context)
        {
            _backupRestoreConfigFileRepository = Context.GetRepository<BackupRestoreFileConfig>();
            _backupRestoreHistoryService = backupRestoreHistoryService;
            _backupRestoreManagerService = backupRestoreManagerService;
            _fileLocationService = fileLocationService;
            _context = HttpContext.Current != null
                  ? (new HttpContextWrapper(HttpContext.Current)) as HttpContextBase
                  : new FakeHttpContext("~/");
            _resourceService = resourceService;
        }

        /// <summary>
        ///  Tạo mới cấu hình backup database
        /// </summary>
        /// <param name="backupRestoreFileConfig">Thực thể  sao lưu phục hồi tệp tin</param>
        public void Create(BackupRestoreFileConfig backupRestoreFileConfig)
        {
            if (backupRestoreFileConfig == null)
            {
                throw new ArgumentNullException("backupRestoreFileConfig");
            }

            _backupRestoreConfigFileRepository.Create(backupRestoreFileConfig);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa cấu hình backup
        /// </summary>
        /// <param name="backupRestoreFileConfig">Thực thể  sao lưu phục hồi tệp tin</param>
        public void Delete(BackupRestoreFileConfig backupRestoreFileConfig)
        {
            if (backupRestoreFileConfig == null)
            {
                throw new ArgumentNullException("backupRestoreFileConfig");
            }
            _backupRestoreConfigFileRepository.Delete(backupRestoreFileConfig);
            Context.SaveChanges();
        }

        /// <summary>
        ///Cập nhật đối tượng backup
        /// </summary>
        /// <param name="backupRestoreFileConfig">Thực thể  sao lưu phục hồi tệp tin</param>
        public void Update(BackupRestoreFileConfig backupRestoreFileConfig)
        {
            if (backupRestoreFileConfig == null)
            {
                throw new ArgumentNullException("backupRestoreFileConfig");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy đối tượng backup
        /// </summary>
        /// <param name="backupRestoreFileConfigId"></param>
        /// <returns></returns>
        public BackupRestoreFileConfig Get(int backupRestoreFileConfigId)
        {
            return _backupRestoreConfigFileRepository.Get(false, p => p.BackupRestoreFileConfigId == backupRestoreFileConfigId);
        }

        /// <summary>
        /// Trả về danh cách các cấu hinh chỉ để đọc
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<BackupRestoreFileConfig> GetsReadOnly(Expression<Func<BackupRestoreFileConfig, bool>> spec = null)
        {
            return _backupRestoreConfigFileRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Trả về danh cách các cấu hinh 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<BackupRestoreFileConfig> Gets(Expression<Func<BackupRestoreFileConfig, bool>> spec = null)
        {
            return _backupRestoreConfigFileRepository.Gets(false, spec);
        }

        /// <summary>
        /// Sao lưu tệp tin
        /// </summary>
        /// <param name="model">Đối tượng cấu hình sao lưu tệp tin</param>
        /// <param name="account">Account thực hiện sao lưu (mặc định là System)</param>
        public void Backup(BackupRestoreFileConfig model, string account = "System")
        {

            ValidShareFolder(model);
            var dateCreated = DateTime.Now;
            var domain = GetDomain();
            var currentFileLocation = _fileLocationService.GetCurrent();

            //Xử lý zip file và copy tới thư mục chứa file sao lưu
            var fileName = string.Format("{0}_{1}.zip", model.FileName, dateCreated.ToString(FORMAT_DATE));
            FileHelper.CreateZipFolder(currentFileLocation.FileLocationAddress, model.Directory, fileName, password: model.ZipPassword);

            var zipPath = System.IO.Path.Combine(model.Directory, fileName);
            if (!System.IO.File.Exists(zipPath))
            {
                throw new Exception("path is not exist.");
            }

            //Tạo thông tin quản lý các lần sao lưu
            using (var fileStream = System.IO.File.OpenRead(zipPath))
            {
                _backupRestoreManagerService.Create(fileStream, model.Domain, fileName,
                    model.ZipPassword, null, model.FileName, false, dateCreated, account);

                CrateHitoryBackupSuccess(model, fileName, domain, dateCreated, account);
            }

            //Vừa backup xong sao lại xóa ở đây
            //System.IO.File.Delete(zipPath);

            int outCount;
            //Trên phần quản lý chỉ giữ lại file backup 5 lần gần nhất, còn các lần khác thì loại bỏ
            var backups = _backupRestoreManagerService.GetsAs(out outCount, p => p,
                pageSize: 5, currentPage: 2, sortBy: DEFAULT_SORT,
                isDescending: true, spec: prop => prop.Domain == model.Domain && !prop.IsDatabaseFile);

            if (backups != null && backups.Any())
            {
                _backupRestoreManagerService.Delete(backups, false);
            }
        }

        /// <summary>
        /// Tạo lịch sử sao lưu cơ sở dữ liệu khi thành công
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileName"></param>
        /// <param name="domain"></param>
        /// <param name="dateCreated"></param>
        /// <param name="account"></param>
        public void CrateHitoryBackupSuccess(BackupRestoreFileConfig model, string fileName, string domain,
            DateTime dateCreated, string account = "System")
        {
            //Tạo quản lý các tệp backup Customer.BusinessType.NotPermissionCreate
            string desciption = string.Format(_resourceService.GetResource("Customer.BackupRestoreFile.LogBackupSuccessed"),
                account, model.Directory, model.Domain, fileName, domain);
            _backupRestoreHistoryService.Create(desciption, dateCreated, isBackup: true, isSuccessed: true, account: account);
        }

        /// <summary>
        /// Tạo lịch sử sao lưu khi có lỗi trong quá trình sao lưu tệp tin
        /// </summary>
        /// <param name="model">Đối tượng cấu hình sao lưu tệp tin</param>
        /// <param name="account">Tài khoản người thực hiện sao lưu (mặc định là System)</param>
        public void CreateHistoryBackupError(BackupRestoreFileConfig model, string account = "System")
        {
            string domain = GetDomain();
            string desciption = string.Format(_resourceService.GetResource("Customer.BackupRestoreFile.LogBackupError"),
                account, model.Directory, model.Domain, domain);
            _backupRestoreHistoryService.Create(desciption, DateTime.Now, isBackup: true, isSuccessed: false, account: account);
        }

        /// <summary>
        ///Kiểm tra thư mục có tồn tại hay không và có kết nối tời thư mục được hay không
        /// </summary>
        /// <param name="model"></param>
        private void ValidShareFolder(BackupRestoreFileConfig model)
        {
            if (model == null)
            {
                throw new Exception("shareFolder is not exist.");
            }

            //Kiểm tra kết nối tới nơi lưu trữ tạm
            if (model.IsNetwork)
            {
                var error = NetworkShare.ConnectToShare(model.Directory, Base64Decode(model.UserName), Base64Decode(model.Password));
                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }
            }
            else
            {
                if (!System.IO.Directory.Exists(model.Directory))
                {
                    throw new Exception("Directory is not exist.");
                }
            }
        }

        /// <summary>
        /// Phục hồi lại các tệp tin
        /// </summary>
        /// <param name="model">Đối tượng cấu hình file</param>
        /// <param name="stream">stream của tệp tin</param>
        /// <param name="fileName">Tên tệp tin</param>
        /// <param name="unZipPassword">unZipPassword</param>
        /// <param name="account"></param>
        public void Restore(BackupRestoreFileConfig model, System.IO.Stream stream, string fileName, string unZipPassword, string account)
        {
            if (model == null)
            {
                throw new ArgumentNullException("BackupRestoreFileConfig is null or empty.");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName is null or empty.");
            }

            if (stream == null || stream.Length <= 0)
            {
                throw new ArgumentNullException("stream is null or empty.");
            }

            if (!FileHelper.IsFileZip(stream))
            {
                throw new Exception("File is not file zip.");
            }

            ValidShareFolder(model);

            //Giải nén tệp
            FileHelper.UnZip(stream, model.Directory, unZipPassword);
            CreateHistoryRestoreSuccess(model, fileName, account);
        }

        /// <summary>
        /// Phục hồi lại các tệp tin
        /// </summary>
        /// <param name="model">Đối tượng cấu hình file</param>
        /// <param name="filePath">stream của tệp tin</param>
        /// <param name="fileName">Tên tệp tin</param>
        /// <param name="unZipPassword">un zip password</param>
        /// <param name="account"></param>
        public void Restore(BackupRestoreFileConfig model, string filePath, string fileName, string unZipPassword, string account)
        {
            if (model == null)
            {
                throw new ArgumentNullException("BackupRestoreFileConfig is null or empty.");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName is null or empty.");
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath is null or empty.");
            }

            if (!FileHelper.IsFileZip(filePath))
            {
                throw new Exception("File is not file zip.");
            }

            ValidShareFolder(model);

            //Giải nén tệp
            FileHelper.UnZip(filePath, model.Directory, unZipPassword);
            CreateHistoryRestoreSuccess(model, fileName, account);
        }

        private void CreateHistoryRestoreSuccess(BackupRestoreFileConfig model, string fileName, string account = "System")
        {
            string desciption = string.Format(_resourceService.GetResource("Customer.BackupRestoreFile.LogRestoreSuccessed"),
                                      account, fileName, model.Directory, model.Domain);

            _backupRestoreHistoryService.Create(desciption, DateTime.Now, isBackup: false, isSuccessed: true, account: account);
        }

        /// <summary>
        /// Tạo lịch sử sao lưu khi có lỗi trong quá trình sao lưu tệp tin
        /// </summary>
        /// <param name="model">Đối tượng cấu hình sao lưu tệp tin</param>
        /// <param name="fileName">Tên tệp sao lưu</param>
        /// <param name="account">Tài khoản người thực hiện sao lưu (mặc định là System)</param>
        public void CreateHistoryRestoreError(BackupRestoreFileConfig model, string fileName, string account = "System")
        {
            string currentDomain = GetDomain();
            string desciption = string.Format(_resourceService.GetResource("Customer.BackupRestoreFile.LogRestoreError"),
                                                account, model.Directory, model.Domain, currentDomain);

            _backupRestoreHistoryService.Create(desciption, DateTime.Now, isBackup: false, isSuccessed: false, account: account, domain: currentDomain);
        }

        /// <summary>
        /// Lấy domain hiện tại
        /// </summary>
        /// <returns></returns>
        private string GetDomain()
        {
            string domain = string.Empty;
            try
            {
                if (_context.Request != null)
                {
                    if (_context.Request.UserHostName != null)
                    {
                        domain = _context.Request.GetDomainName();
                    }
                }
            }
            catch { }

            return domain;
        }

        /// <summary>
        /// Ma hoa base 64
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string Base64Encode(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            var outPut = System.Text.Encoding.UTF8.GetBytes(input);
            return System.Convert.ToBase64String(outPut);
        }

        /// <summary>
        /// giai ma 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string Base64Decode(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var outPut = System.Convert.FromBase64String(input);
            return System.Text.Encoding.UTF8.GetString(outPut);
        }
    }
}
