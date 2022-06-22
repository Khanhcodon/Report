using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class ReceiveReportModel : PacketModel
    {
        public ReceiveReportModel() : base() { }

        public int status { get; set; }

        public string err_code { get; set; }

        public string err_msg { get; set; }

        public List<ReceiveReportDataModel> data { get; set; }
    }
}