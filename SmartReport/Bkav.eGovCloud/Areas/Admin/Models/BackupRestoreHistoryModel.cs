using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using System;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
      [FluentValidation.Attributes.Validator(typeof(BackupRestoreHistoryValidator))]
    public class BackupRestoreHistoryModel
    {
        public BackupRestoreHistoryModel()
        {
            DateCreated = DateTime.Now;
            IsSuccessed = true;
        }

        /// <summary>
        /// Id của sau lưu phục hồi  CSDL
        /// </summary>
        public int BackupRestoreHistoryId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập server sao lưu, phục hồi dữ liệu
        /// </summary>
        [LocalizationDisplayName("BackupRestoreHistory.CreateOrEdit.Fields.Domain.Label")]
        public string Domain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ip thao tác
        /// </summary>
        [LocalizationDisplayName("BackupRestoreHistory.CreateOrEdit.Fields.Ip.Label")]
        public string Ip { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày thực thi
        /// </summary>
        [LocalizationDisplayName("BackupRestoreHistory.CreateOrEdit.Fields.DateCreated.Label")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập  mô tả về thao tác hành động
        /// </summary>
        [LocalizationDisplayName("BackupRestoreHistory.CreateOrEdit.Fields.Description.Label")]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập là sao lưu hay phục hồi
        /// </summary>
        [LocalizationDisplayName("BackupRestoreHistory.CreateOrEdit.Fields.IsBackup.Label")]
        public bool IsBackup { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người thao tác
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái thực thị có thành công hay không
        /// </summary>
        public bool IsSuccessed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập là lịch sử file hay database
        /// </summary>
        [LocalizationDisplayName("BackupRestoreHistory.CreateOrEdit.Fields.IsDataBase.Label")]
        public bool IsDataBase { get; set; }
    }
}