using System;
using Bkav.eGovCloud.Core.FileSystem;

namespace Bkav.eGovCloud.Business.FileTransfer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : FileTransferInfo - public - BLL</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 060313</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Class thông tin file</para>
    /// </summary>
    internal class FileTransferInfo
    {
        /// <summary>
        /// Tên file
        /// </summary>
        internal string FileName { get; set; }

        /// <summary>
        /// Loại file
        /// </summary>
        internal FileType FileType { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        internal DateTime CreatedDate { get; set; }

        /// <summary>
        /// Tên thư mục tự tăng
        /// </summary>
        internal string IdentityFolder { get; set; }

        /// <summary>
        /// Tên thư mục gốc
        /// </summary>
        internal string RootFolder { get; set; }

        /// <summary>
        /// Dung lượng file
        /// </summary>
        internal long Length { get; set; }
    }
}
