using System;
namespace Bkav.eGovCloud.Models
{
    public class BusinessSearchModel
    {
        public int? BusinessTypeId { get; set; }
        public string CityCode { get; set; }
        public string DistrictCode { get; set; }
        public int? WardId { get; set; }
        public int? BusinessId { get; set; }
        public int? DocFieldId { get; set; }
        public Guid? DocTypeId { get; set; }
        public int? Timezone { get; set; }
        public int? ExpireDay { get; set; }
    }
}