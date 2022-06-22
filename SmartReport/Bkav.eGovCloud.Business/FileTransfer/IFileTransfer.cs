using System.IO;
using Bkav.eGovCloud.Core.FileSystem;

namespace Bkav.eGovCloud.Business.FileTransfer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Interface : IFileTransfer - public - BLL</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 060313</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Interface dùng cho việc tải file</para>
    /// </summary>
    internal interface IFileTransfer
    {
        /// <summary>
        /// Tải file lên
        /// </summary>
        /// <param name="file">Stream</param>
        /// <param name="type">Loại file</param>
        FileTransferInfo Upload(Stream file, FileType type);

        /// <summary>
        /// Tải file xuống
        /// </summary>
        /// <param name="fileInfo">Thông tin file</param>
        Stream Download(FileTransferInfo fileInfo);

        /// <summary>
        /// Lấy dường dẫn tới thư mục vật lý của file
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        string GetPath(FileTransferInfo fileInfo);

        /// <summary>
        /// Tải file lên
        /// </summary>
        /// <param name="fileInfo">Thông tin file cần copy</param>
        FileTransferInfo Copy(FileTransferInfo fileInfo);

        /// <summary>
        /// HopCV : 060815
        /// Xóa file
        /// </summary>
        /// <param name="fileInfo">Thông tin file cần xóa</param>
        void Delete(FileTransferInfo fileInfo);
    }
}
