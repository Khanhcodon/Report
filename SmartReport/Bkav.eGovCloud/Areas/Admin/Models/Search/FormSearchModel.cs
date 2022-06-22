using System;
namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class FormSearchModel
    {
        public int? FormGroupId { get; set; }

        public int? FormTypeId { get; set; }

        public Guid? DocTypeId { get; set; }

        public int? DocFieldId { get; set; }

        public string SearchKey { get; set; }
    }
}