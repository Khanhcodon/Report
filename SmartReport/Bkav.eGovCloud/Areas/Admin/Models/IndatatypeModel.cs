using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class IndatatypeModel
    {
        public Guid Id { get; set; }
        public Guid ValueId { get; set; }
        public int DepartmentId { get; set; }
    }
}