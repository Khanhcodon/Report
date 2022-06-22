using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using kav.eGovCloud.Areas.Admin.Validator;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(OfficeValidator))]
    public class OfficeModel
    {
        public int OfficeId { get; set; }

        public string OfficeName { get; set; }

        public string OfficeCode { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public string Description { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string FileService { get; set; }

        public string DataService { get; set; }

        public string Password { get; set; }

        public string LastPassword { get; set; }

        public bool IsMe { get; set; }

        public int? UserId { get; set; }

        //   public virtual User User { get; set; }

        public string OnlineServiceUrl { get; set; }

        public string ProcessServiceUrl { get; set; }

        public string ReportServiceUrl { get; set; }

        public bool IsOnlineRegister { get; set; }

        public int LevelId { get; set; }

        // public virtual Level Level { get; set; }

        public string LevelName { get; set; }
    }
}