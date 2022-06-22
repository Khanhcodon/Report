using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreManager - public - Entity
    /// Access Modifiers: 
    /// Create Date : 130715
    /// Author      : HopCV
    /// Description : Entity tương ứng với bảng BackupRestoreManager trong CSDL
    /// </summary>
    public class BackupRestoreManager
    {
        /// <summary>
        /// 
        /// </summary>
        public int BackupRestoreManagerId { get; set; }

        /// <summary>
        ///  Lấy hoặc thiết lập 
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        ///  Lấy hoặc thiết lập người tạo
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày tạo
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập là file CSDL hay file sao lưu thường
        /// </summary>
        public bool IsDatabaseFile { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên định danh (hiển thị tên, tao file khi download xuống)
        /// </summary>
        public string FileNameAlias { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ZipPassword
        /// </summary>
        public string ZipPassword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên goc cua tep tin
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên file được tạo khi upload lên server
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kích thước file sao lưu
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày cập nhật cuối cùng
        /// </summary>
        public DateTime? LastModifiedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người cập nhật cuối cùng
        /// </summary>
        public string LastModifiedByUser { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id vị trí lưu file
        /// </summary>
        public int FileLocationId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên thư mục tự sinh
        /// </summary>
        public string IdentityFolder { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của cấu hình thư mục gốc phía service
        /// </summary>
        public string FileLocationKey { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập vị trí lưu file
        /// </summary>
        public virtual FileLocation FileLocation { get; set; }
    }
}
