using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class FormRelationModel : PacketModel
    {
        public Guid RelationId { get; set; }

        public string RelationName { get; set; }

        public Guid FromFormId { get; set; }

        public Guid ToFormId { get; set; }

        public string Json { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public Form FromForm { get; set; }

        public Form ToForm { get; set; }

        public FormRelationModel(): base()
        {
            CreatedOnDate = DateTime.Now;
        }
    }
}