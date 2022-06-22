using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class ReceiveStatusReportStatusModel : PacketModel
    {
        public ReceiveStatusReportStatusModel() : base() { }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}