using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class ReceiveReportStructModel : PacketModel
    {
        public ReceiveReportStructModel() : base() { }

        public ReceiveReportStructHeaderModel header { get; set; }

        public ReceiveReportStructContentModel Content { get; set; }

    }
}