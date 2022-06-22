using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class ReceiveReportStructContentModel : PacketModel
    {
        public ReceiveReportStructContentModel() : base() { }

        public ReceiveReportStructContentAttributeModel Attribute { get; set; }

        public List<ReceiveReportStructContentIndicatorModel> Indicator { get; set; }

        public List<ReceiveReportStructContentAttributeRuleModel> Rule { get; set; }
    }
}