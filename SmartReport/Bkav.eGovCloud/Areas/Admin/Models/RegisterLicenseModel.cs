using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class RegisterLicenseModel
    {
        public string CustomerName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Key { get; set; }
    }
}