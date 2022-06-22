using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class LogSearchModel
    {
        [LocalizationDisplayName("Common.Log.Index.Search.Fields.FromDate.Label")]
        [VietnameseDateTime("Common.Log.Index.Search.Fields.FromDate.NotValid")]
        public string FromDate { get; set; }

        [LocalizationDisplayName("Common.Log.Index.Search.Fields.ToDate.Label")]
        [VietnameseDateTime("Common.Log.Index.Search.Fields.ToDate.NotValid")]
        public string ToDate { get; set; }
    }
}