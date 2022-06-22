using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Fakes;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using MySql.Data.MySqlClient;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : BackupRestoreConfigBll - public - Bll </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : HopCV@bkav.com </para>
    /// </author>
    /// <summary>
    /// </summary>
    public class BackupRestoreConfigBll : ServiceBase
    {
        private readonly IRepository<BackupRestoreConfig> _backupRestoreConfigRepository;
        private readonly BackupRestoreHistoryBll _backupRestoreHistoryService;
        private readonly BackupRestoreManagerBll _backupRestoreManagerService;
        private readonly HttpContextBase _context;
        private readonly ResourceBll _resourceService;
        private readonly AdminGeneralSettings _generalSettings;
        private const string FORMAT_DATE = "yyyyMMddHHmmss";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="backupRestoreHistoryService"></param>
        /// <param name="backupRestoreManagerService"></param>
        /// <param name="resourceService"></param>
        /// <param name="generalSettings"></param>
        public BackupRestoreConfigBll(
            IDbCustomerContext context,
            BackupRestoreHistoryBll backupRestoreHistoryService,
            BackupRestoreManagerBll backupRestoreManagerService,
            ResourceBll resourceService,
            AdminGeneralSettings generalSettings)
            : base(context)
        {
            _backupRestoreConfigRepository = Context.GetRepository<BackupRestoreConfig>();
            _backupRestoreHistoryService = backupRestoreHistoryService;
            _backupRestoreManagerService = backupRestoreManagerService;
            _context = HttpContext.Current != null
                  ? (new HttpContextWrapper(HttpContext.Current)) as HttpContextBase
                  : new FakeHttpContext("~/");
            _resourceService = resourceService;
            _generalSettings = generalSettings;
        }

        /// <summary>
        ///  Tạo mới cấu hình backup database
        /// </summary>
        /// <param name="backupRestoreConfig"></param>
        public void Create(BackupRestoreConfig backupRestoreConfig)
        {
            if (backupRestoreConfig == null)
            {
                throw new ArgumentNullException("backupRestoreConfig");
            }

            _backupRestoreConfigRepository.Create(backupRestoreConfig);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa cáu hình backup
        /// </summary>
        /// <param name="backupRestoreConfig"></param>
        public void Delete(BackupRestoreConfig backupRestoreConfig)
        {
            if (backupRestoreConfig == null)
            {
                throw new ArgumentNullException("backupRestoreConfig");
            }
            _backupRestoreConfigRepository.Delete(backupRestoreConfig);
            Context.SaveChanges();
        }

        /// <summary>
        ///Cập nhật đối tượng backup
        /// </summary>
        /// <param name="backupRestoreConfig"></param>
        public void Update(BackupRestoreConfig backupRestoreConfig)
        {
            if (backupRestoreConfig == null)
            {
                throw new ArgumentNullException("backupRestoreConfig");
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Lấy đối tượng backup
        /// </summary>
        /// <param name="backupRestoreConfigId"></param>
        /// <returns></returns>
        public BackupRestoreConfig Get(int backupRestoreConfigId)
        {
            return _backupRestoreConfigRepository.Get(false, p => p.BackupRestoreConfigId == backupRestoreConfigId);
        }

        /// <summary>
        /// Trả về danh cách các cấu hinh chỉ để đọc
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<BackupRestoreConfig> GetsReadOnly(Expression<Func<BackupRestoreConfig, bool>> spec = null)
        {
            return _backupRestoreConfigRepository.GetsReadOnly(spec);
        }

        /// <summary>
        /// Trả về danh cách các cấu hinh sao lưu phục hồi CSDL
        /// </summary>
        /// <typeparam name="T">Kiểu thực thể trả về</typeparam>
        /// <param name="totalRecords">Số bản ghi</param>
        /// <param name="projector">Thực thể trả về</param>
        /// <param name="spec">Điều kiện</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="sortBy">Sắp xếp theo</param>
        /// <param name="isDescending"></param>
        /// <returns></returns>
        public IEnumerable<T> GetsAs<T>(out int totalRecords,
                                       Expression<Func<BackupRestoreConfig, T>> projector,
                                       Expression<Func<BackupRestoreConfig, bool>> spec = null,
                                       int currentPage = 1,
                                       int? pageSize = null,
                                       string sortBy = "",
                                       bool isDescending = false)
        {
            if (!pageSize.HasValue)
            {
                pageSize = _generalSettings.DefaultPageSize;
            }

            totalRecords = _backupRestoreConfigRepository.Count(spec);
            var sort = Context.Filters.CreateSort<BackupRestoreConfig>(isDescending, sortBy);
            var paging = Context.Filters.Page<BackupRestoreConfig>(currentPage, pageSize.Value);
            return _backupRestoreConfigRepository.GetsAs(projector, spec, sort, paging);
        }

        /// <summary>
        /// Trả về danh cách các cấu hinh 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<BackupRestoreConfig> Gets(Expression<Func<BackupRestoreConfig, bool>> spec = null)
        {
            return _backupRestoreConfigRepository.Gets(false, spec);
        }

        /// <summary>
        /// Trả về danh cách các cấu hinh 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public IEnumerable<BackupRestoreConfig> GetReaOnlys(Expression<Func<BackupRestoreConfig, bool>> spec = null)
        {
            return _backupRestoreConfigRepository.Gets(true, spec);
        }

        /// <summary>
        /// Test kết nối database cần backup
        /// </summary>
        /// <param name="backupRestoreConfig"></param>
        /// <returns></returns>
        public DbConnection CreateConnection(BackupRestoreConfig backupRestoreConfig)
        {
            if (backupRestoreConfig == null)
            {
                return null;
            }

            var userName = Base64Decode(backupRestoreConfig.UserName);
            var password = Base64Decode(backupRestoreConfig.Password);

            var connection = ConnectionUtil.TestConnection(string.Empty,
                backupRestoreConfig.Server,
                backupRestoreConfig.DatabaseName,
                userName,
                password,
                (short)backupRestoreConfig.Port,
                (Entities.DatabaseType)backupRestoreConfig.DatabaseType);

            return connection;
        }

        /// <summary>
        /// Sao lưu CSDL
        /// </summary>
        /// <param name="backupRestoreConfig">Đối tượng đã cấu hình sao lưu phục hồi</param>
        /// <param name="account"></param>
        public void Backup(BackupRestoreConfig backupRestoreConfig, string account = "System")
        {
            if (backupRestoreConfig == null)
            {
                throw new ArgumentNullException("backupRestoreConfig is null.");
            }

            //lấy nơi lưu trữ tạm
            var shareFolder = backupRestoreConfig.ShareFolder;
            ValidShareFolder(shareFolder);

            string outFilePath = string.Empty, outFilename = string.Empty;
            var dateCreated = DateTime.Now;

            switch (backupRestoreConfig.DatabaseType)
            {
                case (byte)Entities.DatabaseType.SqlServer:
                    SqlServerBackup(backupRestoreConfig, shareFolder, dateCreated, out outFilePath, out outFilename);
                    break;

                case (byte)Entities.DatabaseType.MySql:
                    MySqlBackup(backupRestoreConfig, shareFolder, dateCreated, out outFilePath, out outFilename);
                    break;

                case (byte)Entities.DatabaseType.Oracle:
                    //todo: Chưa có
                    break;

                default:
                    break;
            }

            //Tạo quản lý các tệp backup
            if (!System.IO.File.Exists(outFilePath))
            {
                throw new Exception("outFilePath is not exist.");
            }

            var fileName = outFilename + ".zip";

            //todo: Chỗ này không được xử lý trên bộ nhớ ram vì file backup dung lượng lớn=> tran bộ nhớ
            //Tạo file zip và xóa tệp back up
            FileHelper.CreateZipFile(outFilePath, shareFolder.Directory, fileName, true, backupRestoreConfig.ZipPassword);

            var zipFilepath = System.IO.Path.Combine(shareFolder.Directory, fileName);
            if (!System.IO.File.Exists(zipFilepath))
            {
                throw new Exception("zipFilepath is not exist.");
            }

            using (var stream = System.IO.File.OpenRead(zipFilepath))
            {
                _backupRestoreManagerService.Create(stream,
                    backupRestoreConfig.Domain, fileName, backupRestoreConfig.ZipPassword, null,
                    backupRestoreConfig.DatabaseName, true, dateCreated, account);
                //Tạo lịch sử backup
                CrateHitoryBackupSuccess(backupRestoreConfig, fileName, dateCreated, account);
            }

            //System.IO.File.Delete(zipFilepath);
            int outCount;
            //Trên phần quản lý chỉ giữ lại file backup 5 lần gần nhất, còn các lần khác thì loại bỏ
            var backups = _backupRestoreManagerService.GetsAs(out outCount, p => p,
                pageSize: 5, currentPage: 2, sortBy: "DateCreated",
                isDescending: true, spec: prop => prop.Domain == backupRestoreConfig.Domain && prop.IsDatabaseFile);
            if (backups != null && backups.Any())
            {
                _backupRestoreManagerService.Delete(backups, false);
            }
        }

        /// <summary>
        /// Tạo lịch sử sao lưu cơ sở dữ liệu khi thành công
        /// </summary>
        /// <param name="model">Đối tượng cấu hình sao lưu, phục hồi cơ sở dữ liệu</param>
        /// <param name="fileName">Tên file phục hồi</param>
        /// <param name="dateCreate"></param>
        /// <param name="account"></param>
        public void CrateHitoryBackupSuccess(BackupRestoreConfig model, string fileName, DateTime dateCreate, string account = "System")
        {
            var currentDomain = GetDomain();
            var desciption = string.Format(_resourceService.GetResource("Customer.BackupRestore.LogBackupSuccessed"),
                account, model.DatabaseName, model.Domain, fileName, currentDomain);

            _backupRestoreHistoryService.Create(desciption, dateCreate, isBackup: true, isSuccessed: true, account: account, domain: currentDomain);
        }

        /// <summary>
        /// Tạo lịch sử sao lưu cơ sở dữ liệu khi có lỗi
        /// </summary>
        /// <param name="model">Đối tượng cấu hình sao lưu, phục hồi cơ sở dữ liệu</param>
        /// <param name="dateCreate">Thời gian </param>
        /// <param name="account"></param>
        public void CrateHitoryBackupError(BackupRestoreConfig model, DateTime dateCreate, string account = "System")
        {
            var currentDomain = GetDomain();
            var desciption = string.Format(_resourceService.GetResource("Customer.BackupRestore.LogBackupError"),
               account, model.DatabaseName, model.Domain, currentDomain);
            _backupRestoreHistoryService.Create(desciption, dateCreate, isBackup: true, isSuccessed: false, account: account, domain: currentDomain);
        }

        /// <summary>
        /// Phục hồi cơ sở dữ liệu
        /// </summary>
        /// <param name="backupRestoreConfig">Đối tượng đã cấu hình sao lưu phục hồi</param>
        /// <param name="stream">Stream tệp phục hồi</param>
        /// <param name="fileName"></param>
        /// <param name="account"></param>
        public void Restore(BackupRestoreConfig backupRestoreConfig, Stream stream, string fileName, string account = "System")
        {
            if (backupRestoreConfig == null)
            {
                throw new ArgumentNullException("backupRestoreConfig is null.");
            }

            if (stream == null)
            {
                throw new ArgumentNullException("stream is null.");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName is null");
            }

            var success = false;
            switch (backupRestoreConfig.DatabaseType)
            {
                case (byte)Entities.DatabaseType.SqlServer:
                    // throw new Exception(" Not support");
                    break;

                case (byte)Entities.DatabaseType.MySql:
                    MySqlRestore(backupRestoreConfig, stream);
                    success = true;
                    break;

                case (byte)Entities.DatabaseType.Oracle:
                    //todo: Chưa có
                    break;

                default:
                    break;
            }

            if (!success)
            {
                throw new Exception("Restore error");
            }

            CrateHitoryRestoreSuccess(backupRestoreConfig, fileName, account);
        }

        /// <summary>
        /// Tạo lịch sử phục hồi cơ sở dữ liệu khi thành công
        /// </summary>
        /// <param name="model">Đối tượng cấu hình sao lưu, phục hồi cơ sở dữ liệu</param>
        /// <param name="fileName">Tên file phục hồi</param>
        /// <param name="account"></param>
        public void CrateHitoryRestoreSuccess(BackupRestoreConfig model, string fileName, string account = "System")
        {
            var currentDomain = GetDomain();
            string desciption = string.Format(_resourceService.GetResource("Customer.BackupRestore.LogRestoreSuccessed"),
             account, model.DatabaseName, model.Domain, fileName, currentDomain);

            _backupRestoreHistoryService.Create(desciption, DateTime.Now, isBackup: false, isSuccessed: true, account: account, domain: currentDomain);
        }

        /// <summary>
        /// Tạo lịch sử phục hồi cơ sở dữ liệu khi có lỗi
        /// </summary>
        /// <param name="model">Đối tượng cấu hình sao lưu, phục hồi cơ sở dữ liệu</param>
        /// <param name="fileName">Tên file phục hồi</param>
        /// <param name="account"></param>
        public void CrateHitoryRestoreError(BackupRestoreConfig model, string fileName, string account = "System")
        {
            var currentDomain = GetDomain();
            string desciption = string.Format(_resourceService.GetResource("Customer.BackupRestore.LogRestoreError"),
                 account, model.DatabaseName, model.Domain, currentDomain);

            _backupRestoreHistoryService.Create(desciption, DateTime.Now, isBackup: false, isSuccessed: false, account: account, domain: currentDomain);
        }

        /// <summary>
        ///  Lấy domain hiện tại
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
        ///Kiểm tra thư mục có tồn tại hay không và có kết nối tời thư mục được hay không
        /// </summary>
        /// <param name="shareFolder"></param>
        private void ValidShareFolder(ShareFolder shareFolder)
        {
            if (shareFolder == null)
            {
                throw new Exception("shareFolder is not exist.");
            }

            //Kiểm tra kết nối tới nơi lưu trữ tạm
            if (shareFolder.IsNetwork)
            {
                var error = NetworkShare.ConnectToShare(shareFolder.Directory,
                    Base64Decode(shareFolder.UserName), Base64Decode(shareFolder.Password));
                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }
            }
            else
            {
                if (!System.IO.Directory.Exists(shareFolder.Directory))
                {
                    throw new Exception("Directory is not exist.");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        ///<param name="model"></param>
        ///<param name="stream"></param>
        private void MySqlRestore(BackupRestoreConfig model, Stream stream)
        {
            if (stream == null || stream.Length <= 0)
            {
                throw new ArgumentNullException("stream is null");
            }

            if (model == null)
            {
                throw new ArgumentNullException("BackupRestoreConfig is null");
            }

            var connection = CreateConnection(model);
            if (connection == null)
            {
                throw new Exception("Not connected!");
            }

            using (var cmd = (MySqlCommand)connection.CreateCommand())
            {
                connection.Open();

                #region restore

                var file = new StreamReader(stream);
                //bị out of memory chỗ này
                var input = file.ReadToEnd();
                file.Close();

                if (string.IsNullOrWhiteSpace(input))
                {
                    throw new ArgumentNullException("File content is null or empty.");
                }

                var workingDirectory = string.Empty;
                if (!string.IsNullOrWhiteSpace(model.Config))
                {
                    var config = Json2.ParseAs<ConfigInMySql>(model.Config);
                    workingDirectory = config.WorkingDirectory;
                }

                var userName = Base64Decode(model.UserName);
                var password = Base64Decode(model.Password);
                var command = string.Format(@"-u {0} -p{1} -h {2} --port={3} {4}",
                                            userName,
                                            password,
                                            model.Server,
                                            model.Port,
                                            model.DatabaseName);

                var dumpProcess = new Process();

                if (!string.IsNullOrEmpty(workingDirectory))
                {
                    dumpProcess.StartInfo.WorkingDirectory = workingDirectory;
                }

                dumpProcess.StartInfo.Arguments = command;
                dumpProcess.StartInfo.FileName = @"mysql.exe";
                dumpProcess.StartInfo.UseShellExecute = false;
                dumpProcess.StartInfo.RedirectStandardInput = true;
                dumpProcess.StartInfo.RedirectStandardOutput = false;
                dumpProcess.StandardInput.WriteLine(file);
                dumpProcess.StandardInput.Close();
                dumpProcess.Start();
                dumpProcess.WaitForExit();
                dumpProcess.Close();

                #endregion

                connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="backupRestoreConfig"></param>
        /// <param name="shareFolder"></param>
        /// <param name="dateCreated"></param>
        /// <param name="outFilePath"></param>
        /// <param name="outFileName"></param>
        private void MySqlBackup(BackupRestoreConfig backupRestoreConfig, ShareFolder shareFolder, DateTime dateCreated, out string outFilePath, out string outFileName)
        {
            var connection = CreateConnection(backupRestoreConfig);
            if (connection == null)
            {
                throw new Exception("Not connected!");
            }

            using (var cmd = (MySqlCommand)connection.CreateCommand())
            {
                connection.Open();

                #region  backup

                var filename = backupRestoreConfig.DatabaseName + "_" + dateCreated.ToString(FORMAT_DATE);
                var filePath = System.IO.Path.Combine(shareFolder.Directory, filename + ".sql");
                var command = string.Format(@"--default-character-set=utf8 -u {0} -p{1} -h {2} --port={3} --databases {4}",
                                            Base64Decode(backupRestoreConfig.UserName),
                                            Base64Decode(backupRestoreConfig.Password),
                                            backupRestoreConfig.Server,
                                            backupRestoreConfig.Port,
                                            backupRestoreConfig.DatabaseName);
                var workingDirectory = string.Empty;

                if (!string.IsNullOrWhiteSpace(backupRestoreConfig.Config))
                {
                    var config = Json2.ParseAs<ConfigInMySql>(backupRestoreConfig.Config);
                    if (config.HasGetTrigger)
                    {
                        command += " --triggers";
                    }

                    if (config.HasGetFunctionAndProcedure)
                    {
                        command += " --routines";
                    }

                    workingDirectory = config.WorkingDirectory;
                }

                command += " -r " + filePath;

                workingDirectory = GetWorkingDirectory(workingDirectory);
                var fileDump = string.Format(@"{0}\{1}", workingDirectory, @"mysqldump.exe");
                if (!System.IO.File.Exists(fileDump))
                {
                    throw new FileNotFoundException("Not found mysqldump.exe.");
                }

                var dumpProcess = new Process();
                dumpProcess.StartInfo.Arguments = command;
                dumpProcess.StartInfo.FileName = fileDump;
                dumpProcess.StartInfo.UseShellExecute = false;
                dumpProcess.StartInfo.RedirectStandardInput = false;
                dumpProcess.StartInfo.RedirectStandardOutput = false;
                dumpProcess.Start();
                dumpProcess.WaitForExit();
                dumpProcess.Close();

                #endregion

                outFileName = filename;
                outFilePath = filePath;

                connection.Close();
            }
        }

        private string GetWorkingDirectory(string workingDirectory)
        {
            if (!string.IsNullOrWhiteSpace(workingDirectory))
            {
                workingDirectory = workingDirectory.Replace(@"/", @"\");
                if (workingDirectory.EndsWith(@"\"))
                {
                    workingDirectory = workingDirectory.Substring(0, workingDirectory.LastIndexOf(@"\"));
                }
            }
            return workingDirectory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="backupRestoreConfig"></param>
        /// <param name="shareFolder"></param>
        /// <param name="dateCreate"></param>
        /// <param name="outFilePath"></param>
        /// <param name="outFileName"></param>
        private void SqlServerBackup(BackupRestoreConfig backupRestoreConfig,
            ShareFolder shareFolder,
            DateTime dateCreate, out string outFilePath, out string outFileName)
        {
            var connection = CreateConnection(backupRestoreConfig);
            if (connection == null)
            {
                throw new Exception("Not connected!");
            }

            using (var cmd = (SqlCommand)connection.CreateCommand())
            {
                connection.Open();
                var query = GetQuerySqlServer(backupRestoreConfig, shareFolder, dateCreate, out outFilePath, out outFileName);
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="backupRestoreConfig"></param>
        /// <param name="shareFolder"></param>
        /// <param name="dateCreate"></param>
        /// <param name="outFilePath"></param>
        /// <param name="outFileName"></param>
        /// <returns></returns>
        private string GetQuerySqlServer(BackupRestoreConfig backupRestoreConfig,
            ShareFolder shareFolder, DateTime dateCreate,
            out string outFilePath, out string outFileName)
        {
            var filename = backupRestoreConfig.DatabaseName + "_" + dateCreate.ToString(FORMAT_DATE);
            var config = Json2.ParseAs<ConfigInSqlServer>(backupRestoreConfig.Config);
            var filePath = System.IO.Path.Combine(shareFolder.Directory, filename + ".bak");
            string typeBackupStr = string.Empty;

            if (config.DaySetBackup.HasValue && config.DaySetBackup.Value > 0)
            {
                typeBackupStr = " RETAINDAYS =" + config.DaySetBackup.Value + "',";
            }
            else if (config.DateTimeSetBackup.HasValue)
            {
                typeBackupStr = " EXPIREDATE = N'" + config.DateTimeSetBackup.Value.ToString() + "',";
            }

            string query = "BACKUP DATABASE"
                            + backupRestoreConfig.DatabaseName
                            + " TO  DISK = N'"
                            + filePath + "'"
                            + " WITH NOFORMAT, INIT,"
                            + typeBackupStr
                            + " NAME = N'"
                            + backupRestoreConfig.DatabaseName + "', SKIP, NOREWIND, NOUNLOAD,  STATS = 10 GO";

            outFilePath = filePath;
            outFileName = filename;

            return query;
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
