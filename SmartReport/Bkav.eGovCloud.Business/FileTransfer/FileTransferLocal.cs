using System;
using System.IO;
using Bkav.eGovCloud.Core.FileSystem;
using FileType = Bkav.eGovCloud.Core.FileSystem.FileType;

namespace Bkav.eGovCloud.Business.FileTransfer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : FileTransferLocal - public - Service</para>
    /// <para>Access Modifiers: </para>
    /// <para>    Implement: IFileTransfer</para>
    /// <para>Create Date : 060313</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Đọc ghi file tại local</para>
    /// </summary>
    internal class FileTransferLocal : IFileTransfer
    {
        private readonly string _rootFolder;
        private readonly int _threshold;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="rootFolder">Đường dẫn thư mục gốc</param>
        /// <param name="threshold">Số file tối đa trong một thư mục</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal FileTransferLocal(string rootFolder, int threshold = 4096)
        {
            if (string.IsNullOrWhiteSpace(rootFolder))
            {
                throw new ArgumentNullException("rootFolder");
            }
            _rootFolder = rootFolder;
            _threshold = threshold;
        }

#pragma warning disable 1591
        public FileTransferInfo Upload(Stream file, FileType type)
        {
            var now = DateTime.Now;
            var newFile = FileManager.Default.Create(file, type, now, _threshold, _rootFolder);

            return new FileTransferInfo
            {
                CreatedDate = now,
                FileName = newFile.Name,
                FileType = type,
                IdentityFolder = newFile.Directory.Name,
                RootFolder = _rootFolder,
                Length = newFile.Length
            };
        }

        /// <summary>
        /// Lấy ra đường dẫn lưu file
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public string GetPath(FileTransferInfo fileInfo)
        {
            return FileManager.Default.Resolve(fileInfo.FileName, fileInfo.FileType, fileInfo.CreatedDate, fileInfo.IdentityFolder, _rootFolder);
        }

        public Stream Download(FileTransferInfo fileInfo)
        {
            return FileManager.Default.Open(fileInfo.FileName, fileInfo.FileType, fileInfo.CreatedDate, fileInfo.IdentityFolder, _rootFolder);
        }

        public FileTransferInfo Copy(FileTransferInfo fileInfo)
        {
            var stream = FileManager.Default.Open(fileInfo.FileName, fileInfo.FileType, fileInfo.CreatedDate, fileInfo.IdentityFolder, _rootFolder);
            var now = DateTime.Now;
            var copyFile = FileManager.Default.Create(stream, fileInfo.FileType, now, _threshold, _rootFolder);
            return new FileTransferInfo
            {
                CreatedDate = now,
                FileName = copyFile.Name,
                FileType = fileInfo.FileType,
                IdentityFolder = copyFile.Directory.Name,
                RootFolder = _rootFolder,
                Length = copyFile.Length
            };
        }

        public void Delete(FileTransferInfo fileInfo)
        {
            FileManager.Default.Delete(fileInfo.FileName, fileInfo.FileType, fileInfo.CreatedDate, fileInfo.IdentityFolder, fileInfo.RootFolder);
        }
    }
}
