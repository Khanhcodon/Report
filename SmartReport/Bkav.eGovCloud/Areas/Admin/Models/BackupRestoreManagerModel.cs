using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(BackupRestoreManagerValidator))]
    public class BackupRestoreManagerModel
    {
        public BackupRestoreManagerModel()
        {
            DateCreated = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        public int BackupRestoreManagerId { get; set; }

        /// <summary>
        ///  Lấy hoặc thiết lập 
        /// </summary>
        [LocalizationDisplayName("BackupRestoreManager.CreateOrEdit.Fields.Domain.Label")]
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
        [LocalizationDisplayName("BackupRestoreManager.CreateOrEdit.Fields.Description.Label")]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập là file CSDL hay file sao lưu thường
        /// </summary>
        [LocalizationDisplayName("BackupRestoreManager.CreateOrEdit.Fields.IsDatabaseFile.Label")]
        public bool IsDatabaseFile { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên định danh (hiển thị tên, tao file khi download xuống)
        /// </summary>
        public string FileNameAlias { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập dinh danh
        /// </summary>
        [LocalizationDisplayName("BackupRestoreManager.CreateOrEdit.Fields.Alias.Label")]
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
        /// Lấy hoặc thiết lập ZipPassword
        /// </summary>
        [LocalizationDisplayName("BackupRestoreManager.CreateOrEdit.Fields.ZipPassword.Label")]
        public string ZipPassword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày cập nhật cuối cùng
        /// </summary>
        public DateTime? LastModifiedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người cập nhật cuối cùng
        /// </summary>
        public string LastModifiedByUser { get; set; }
    }
}