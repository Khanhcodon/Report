using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class BackupRestoreManagerSearchModel
    {
        [LocalizationDisplayName("Customer.BackupRestoreManager.Index.Search.Fields.FromDate.Label")]
        //[VietnameseDateTime("Customer.BackupRestoreManager.Index.Search.Fields.FromDate.NotValid")]
        public string FromDate { get; set; }

        [LocalizationDisplayName("Customer.BackupRestoreManager.Index.Search.Fields.ToDate.Label")]
        //[VietnameseDateTime("Customer.BackupRestoreManager.Index.Search.Fields.ToDate.NotValid")]
        public string ToDate { get; set; }

        [LocalizationDisplayName("Customer.BackupRestoreManager.Index.Search.Fields.IsDatabaseFile.Label")]
        public bool? IsDatabaseFile { get; set; }
    }
}