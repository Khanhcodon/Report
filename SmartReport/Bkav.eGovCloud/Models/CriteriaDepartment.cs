using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Models
{
    public class CriteriaDepartment
    {
        public int RateEmployeeId { get; set; }
        public int CheckInfringeId { get; set; }
        public int Point { get; set; }
        public int? ParentId { get; set; }
        public int InfringeUserId { get; set; }
        public string UserName { get; set; }
        public string PositionName { get; set; }



    }
}