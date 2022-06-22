using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class NotifyModel
    {
        public NotifyModel()
        {
        }

        public NotifyModel(string option, int? templateId, string description)
        {
            this.Option = option;
            this.TemplateId = templateId;
            this.Description = description;
        }

        public int Id { get; set; }

        public string Option { get; set; }

        public string Description { get; set; }

        public int? TemplateId { get; set; }

        public Template Template { get; set; }
    }
}