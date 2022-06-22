using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Web.Framework;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class LocalityModel
    {
        public Guid LocalityId { get; set; }

        public string LocalityName { get; set; }

        public int Type { get; set; }

        public Guid? PerentId { get; set; }

        public string LocalityParent { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

    }
}