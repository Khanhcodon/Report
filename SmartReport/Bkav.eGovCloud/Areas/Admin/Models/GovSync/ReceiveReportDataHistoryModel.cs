using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class ReceiveReportDataHistoryModel : PacketModel
    {
        public ReceiveReportDataHistoryModel() : base() { }

        public string his_status { get; set; }

        public string note { get; set; }

        public DateTime his_dt { get; set; }


    }
}