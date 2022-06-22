using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class SmsOrMailSearchModel
    {
        public string FindText { get; set; }

        [LocalizationDisplayName("Customer.SmsOrMailSearch.Index.Search.Fields.FromDate.Label")]
        [VietnameseDateTime("Customer.SmsOrMailSearch.Index.Search.Fields.FromDate.NotValid")]
        public string FromDate { get; set; }

        [LocalizationDisplayName("Customer.SmsOrMailSearch.Index.Search.Fields.ToDate.Label")]
        [VietnameseDateTime("Customer.SmsOrMailSearch.Index.Search.Fields.ToDate.NotValid")]
        public string ToDate { get; set; }

        public bool? IsSent { get; set; }
    }
}