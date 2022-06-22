using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// <para> Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para> Project: eGov Cloud v1.0</para>
    /// <para> Class : FileLocation - public - Entity</para>
    /// <para> Access Modifiers:</para>
    /// <para> Create Date : 060313</para>
    /// <para> Author      : TrungVH</para>
    /// <para> Description : Entity tương ứng với bảng FileLocation (Vị trí lưu file) trong CSDL</para>
    /// </summary>
    public class FileLocation
    {
        private ICollection<AttachmentDetail> _attachmentDetail;
        //private ICollection<StorePrivateAttachment> _storePrivateAttachments;


        /// <summary>
        /// Lấy hoặc thiết lập Id vị trí lưu file
        /// </summary>
        public int FileLocationId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vị trí lưu file
        /// </summary>
        public string FileLocationAddress { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kiểu đọc ghi file, qua service (true), tại local (false)
        /// </summary>
        public bool FileLocationType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kiểu đọc ghi file, qua service, tại local
        /// </summary>
        public FileLocationType FileLocationTypeInEnum
        {
            get { return FileLocationType ? Entities.FileLocationType.Service : Entities.FileLocationType.Local; }
            set { FileLocationType = Convert.ToBoolean((int) value); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra vị trí lưu file đang được sử dụng
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Các tệp đính kèm
        /// </summary>
        public virtual ICollection<AttachmentDetail> AttachmentDetails
        {
            get { return _attachmentDetail ?? (_attachmentDetail = new List<AttachmentDetail>()); }
            set { _attachmentDetail = value; }
        }

        ///// <summary>
        ///// Lấy hoặc thiết lập Các tài liệu trong hồ sơ cá nhân
        ///// </summary>
        //public virtual ICollection<StorePrivateAttachment> StorePrivateAttachments
        //{
        //    get { return _storePrivateAttachments ?? (_storePrivateAttachments = new List<StorePrivateAttachment>()); }
        //    set { _storePrivateAttachments = value; }
        //}
    }
}
