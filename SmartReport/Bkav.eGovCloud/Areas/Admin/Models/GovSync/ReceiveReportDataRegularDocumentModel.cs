using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class ReceiveReportDataRegularDocumentModel : PacketModel
    {
        public ReceiveReportDataRegularDocumentModel() : base() { }

        public string DocumentNumber { get; set; }

        public string DocumentCode { get; set; }

        public string DocumentName { get; set; }


    }
}