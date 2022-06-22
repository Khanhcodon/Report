using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class ReceiveReportStructHeaderModel : PacketModel
    {
        public ReceiveReportStructHeaderModel() : base() { }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }

        public List<string> Org { get; set; }

        public object Other { get; set; }


    }
}