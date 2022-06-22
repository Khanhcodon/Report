namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class DocTypeSearchModel
    {
        public int? ActionLevel { get; set; }

        public int? DocFieldId { get; set; }

        public bool? IsActivated { get; set; }

        public string DocTypeName { get; set; }

        public string DocTypeCode { get; set; }
    }
}