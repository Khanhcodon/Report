using System.Collections.Generic;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class ReceiveReportStructContentIndicatorModel : PacketModel
    {
        public ReceiveReportStructContentIndicatorModel() : base() { }
        
        public string Index { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Unit { get; set; }

        public string Item { get; set; }

        public string ParentCode { get; set; }

        public int Type { get; set; }

        public List<ReceiveReportStructContentIndicatorModel> children { get; set; }
    }
}