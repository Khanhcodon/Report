using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class ReceiveReportDataModel : PacketModel
    {
        public ReceiveReportDataModel() : base() { }

        public string rpt_code { get; set; }

        public string rpt_name { get; set; }

        public string rpt_status { get; set; }

        public int period { get; set; }

        public DateTime start_dt { get; set; }

        public DateTime end_dt { get; set; }

        public ReceiveReportDataAssignOrgModel assign_org { get; set; }

        public ReceiveReportDataRegularDocumentModel RegulatoryDocument { get; set; }

        public string rpt_structure { get; set; }

        public ReceiveReportStructModel ReceiveReportStructModel { get; set; }

        public List<ReceiveReportDataHistoryModel> history { get; set; }


    }
}