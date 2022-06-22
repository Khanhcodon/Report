using System;
using System.IO;
using Bkav.eGovCloud.Business.FileManagerService;

namespace Bkav.eGovCloud.Business.FileTransfer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : FileTransferService - public - Service</para>
    /// <para>Access Modifiers: </para>
    /// <para>    Implement: IFileTransfer</para>
    /// <para>Create Date : 060313</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Đọc ghi file tại service</para>
    /// </summary>
    internal class FileTransferService : IFileTransfer
    {
        private readonly string _address;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="address">Đường dẫn service</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal FileTransferService(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException("address");
            }
            _address = address.EndsWith("/mex") ? address : string.Format("{0}/mex", address);
        }

#pragma warning disable 1591
        public FileTransferInfo Upload(Stream file, Core.FileSystem.FileType type)
        {
            var service = new FileTransferClient("BasicHttpBinding_IFileTransfer", _address);
            var fileDetail = service.UploadFile(file.Length, (FileManagerService.FileType)type, file);
            return new FileTransferInfo
            {
                CreatedDate = fileDetail.CreatedDate,
                FileName = fileDetail.FileName,
                FileType = type,
                IdentityFolder = fileDetail.IdentityFolder,
                RootFolder = fileDetail.RootFolderKey,
                Length = fileDetail.Length
            };
        }

        public Stream Download(FileTransferInfo fileInfo)
        {
            var service = new FileTransferClient("BasicHttpBinding_IFileTransfer", _address);

            return service.DownloadFile(new FileDetail
            {
                CreatedDate = fileInfo.CreatedDate,
                FileName = fileInfo.FileName,
                FileType = (FileManagerService.FileType)fileInfo.FileType,
                IdentityFolder = fileInfo.IdentityFolder,
                RootFolderKey = fileInfo.RootFolder,
                Length = fileInfo.Length
            });
        }

        public FileTransferInfo Copy(FileTransferInfo fileInfo)
        {
            var service = new FileTransferClient("BasicHttpBinding_IFileTransfer", _address);
            var fileDetail = new FileDetail
                                 {
                                     CreatedDate = fileInfo.CreatedDate,
                                     FileName = fileInfo.FileName,
                                     FileType = (FileType)fileInfo.FileType,
                                     IdentityFolder = fileInfo.IdentityFolder,
                                     RootFolderKey = fileInfo.RootFolder,
                                     Length = fileInfo.Length
                                 };
            service.CopyFile(ref fileDetail);
            return new FileTransferInfo
            {
                CreatedDate = fileDetail.CreatedDate,
                FileName = fileDetail.FileName,
                FileType = fileInfo.FileType,
                IdentityFolder = fileDetail.IdentityFolder,
                RootFolder = fileDetail.RootFolderKey,
                Length = fileDetail.Length
            };
        }

        public void Delete(FileTransferInfo fileInfo)
        {
            var service = new FileTransferClient("BasicHttpBinding_IFileTransfer", _address);
            var fileDetail = new FileDetail
                        {
                            CreatedDate = fileInfo.CreatedDate,
                            FileName = fileInfo.FileName,
                            FileType = (FileType)fileInfo.FileType,
                            IdentityFolder = fileInfo.IdentityFolder,
                            RootFolderKey = fileInfo.RootFolder,
                            Length = fileInfo.Length
                        };
            service.Delete(fileDetail);
        }

        public string GetPath(FileTransferInfo fileInfo)
        {
            var service = new FileTransferClient("BasicHttpBinding_IFileTransfer", _address);

            var fileDetail = new FileDetail
            {
                CreatedDate = fileInfo.CreatedDate,
                FileName = fileInfo.FileName,
                FileType = (FileType)fileInfo.FileType,
                IdentityFolder = fileInfo.IdentityFolder,
                RootFolderKey = fileInfo.RootFolder,
                Length = fileInfo.Length
            };

            return "";
        }
    }
}
