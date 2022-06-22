using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Models
{
    public class XMLDocumentsModel
    {
        public string CitizenName { get; set; }

        public string DocCode { get; set; }

        public string InOutCode { get; set; }

        public string Compendium { get; set; }

        public byte Urgent { get; set; }

        public int TotalPage { get; set; }

        public string CategoryName { get; set; }

        public string DateArrived { get; set; }

        public string DatePublished { get; set; }
    }
}