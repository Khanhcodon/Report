using System;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class LicenseModel
    {
        public string CustomerName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int TotalUser { get; set; }

        public DateTime ToDate { get; set; }
    }
}