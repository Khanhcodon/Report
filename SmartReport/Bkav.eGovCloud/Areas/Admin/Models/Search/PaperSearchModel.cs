using System;
namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class PaperSearchModel
    {
        public int? CategoryBusinessId { get; set; }

        public int? DocFieldId { get; set; }

        public Guid? DocTypeId { get; set; }
    }
}