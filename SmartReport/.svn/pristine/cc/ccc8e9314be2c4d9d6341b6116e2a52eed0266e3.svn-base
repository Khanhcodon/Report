using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bkav.eGovCloud.Business.FileTransfer;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    ///
    /// </summary>
    public class FileBll : ServiceBase
    {
        private readonly IRepository<Bkav.eGovCloud.Entities.Customer.File> _fileRepository;
        private readonly FileLocationBll _fileLocationService;
        private readonly FileLocationSettings _fileLocationSettings;
        private readonly UserBll _userService;

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fileLocationService"></param>
        /// <param name="fileLocationSettings"></param>
        /// <param name="userService"></param>
        public FileBll(
        IDbCustomerContext context,
        FileLocationBll fileLocationService,
        FileLocationSettings fileLocationSettings,
        UserBll userService)
            : base(context)
        {
            _fileRepository = Context.GetRepository<Bkav.eGovCloud.Entities.Customer.File>();
            _fileLocationService = fileLocationService;
            _userService = userService;
            _fileLocationSettings = fileLocationSettings;
        }

        /// <summary>
        ///  Upload tệp văn bản quy phạm lên server
        ///  Note: Đối tượng file trả ra là chưa có trong database
        /// </summary>
        /// <param name="fileStream">Luồng file</param>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <param name="lawId"></param>
        /// <returns></returns>
        public Bkav.eGovCloud.Entities.Customer.File Upload(Stream fileStream, string fileName, string fileExtension, int lawId)
        {
            if (fileStream == null || fileStream.Length <= 0)
            {
                throw new ArgumentNullException("fileStream is null.");
            }

            var currentFileLocation = _fileLocationService.GetCurrent();
            var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
            var file = transfer.Upload(fileStream, FileType.Law);
            return new Bkav.eGovCloud.Entities.Customer.File
            {
                CreatedOnDate = file.CreatedDate,
                FileExtension = fileExtension,
                FileLocationId = currentFileLocation.FileLocationId,
                FileLocationKey = file.RootFolder,
                IdentityFolder = file.IdentityFolder,
                IsDeleted = false,
                Size = (int)file.Length,
                FileLocalName = file.FileName,
                FileName = fileName,
                LawId = lawId
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        public void Create(IEnumerable<Bkav.eGovCloud.Entities.Customer.File> files)
        {
            if (files == null || !files.Any())
            {
                throw new ArgumentNullException();
            }

            foreach (var item in files)
            {
                _fileRepository.Create(item);
            }
            Context.SaveChanges();
        }

        /// <summary> 
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="extension"></param>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public Bkav.eGovCloud.Entities.Customer.File Create(string name, string extension, Stream fileStream)
        {
            var currentFileLocation = _fileLocationService.GetCurrent();
            var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
            var fileInfo = transfer.Upload(fileStream, FileType.Law);
            var file = new Bkav.eGovCloud.Entities.Customer.File()
            {
                CreatedOnDate = fileInfo.CreatedDate,
                FileExtension = extension,
                FileLocationId = currentFileLocation.FileLocationId,
                FileLocationKey = fileInfo.RootFolder,
                IdentityFolder = fileInfo.IdentityFolder,
                IsDeleted = false,
                Size = (int)fileInfo.Length,
                FileLocalName = fileInfo.FileName,
                FileName = name
            };

            _fileRepository.Create(file);
            Context.SaveChanges();
            return file;
        }

        /// <summary>
        /// Cap nhat file
        /// </summary>
        /// <param name="file"></param>
        public void Update(Bkav.eGovCloud.Entities.Customer.File file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file is null");
            }

            Context.SaveChanges();
        }

        /// <summary>
        /// Xóa tệp đính kèm
        /// </summary>
        /// <param name="file">Thông tin file</param>
        /// <param name="isDeleteLocation">Có xóa file vật lý hay không</param>
        public void Delete(Bkav.eGovCloud.Entities.Customer.File file, bool isDeleteLocation = true)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            if (isDeleteLocation)
            {
                DeleteFileLocation(file);
            }

            _fileRepository.Delete(file);
            Context.SaveChanges();
        }

        /// <summary>
        /// Xoa file vat ly
        /// </summary>
        /// <param name="file"></param>
        public void DeleteFileLocation(Bkav.eGovCloud.Entities.Customer.File file)
        {
            try
            {
                var currentFileLocation = _fileLocationService.GetCurrent();
                var transfer = Transfer.GetTransfer(currentFileLocation, _fileLocationSettings);
                transfer.Delete(new FileTransferInfo()
                {
                    FileName = file.FileLocalName,
                    FileType = FileType.Law,
                    CreatedDate = file.CreatedOnDate,
                    IdentityFolder = file.IdentityFolder,
                    Length = file.Size,
                    RootFolder = file.FileLocationKey
                });
            }
            catch { }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public Bkav.eGovCloud.Entities.Customer.File GetFile(int fileId)
        {
            return _fileRepository.Get(fileId);
        }

        /// <summary>
        /// Tải tệp văn bản quy phạm
        /// </summary>
        /// <param name="fileName">Trả ra tên tệp</param>
        /// <param name="id">id của tệp đính kèm</param>
        /// <returns></returns>
        public System.IO.Stream DownloadFile(out string fileName, int id)
        {
            var file = GetFile(id);
            if (file == null)
            {
                throw new Exception("File is not exist.");
            }

            var fileLocation = _fileLocationService.Get(file.FileLocationId);
            if (fileLocation == null)
            {
                throw new EgovException("Không tìm thấy nơi lưu tệp đính kèm");
            }

            fileName = file.FileName;
            var transfer = Transfer.GetTransfer(fileLocation, _fileLocationSettings);
            var downloaded = transfer.Download(new FileTransferInfo
               {
                   CreatedDate = file.CreatedOnDate,
                   FileName = file.FileLocalName,
                   FileType = FileType.Law,
                   IdentityFolder = file.IdentityFolder,
                   RootFolder = file.FileLocationKey
               });

            return downloaded;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nameFile"></param>
        /// <returns></returns>
        public Bkav.eGovCloud.Entities.Customer.File GetFile(string nameFile)
        {
            return _fileRepository.Get(true, f => f.FileName.Equals(nameFile));
        }
    }
}