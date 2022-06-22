using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Api.Dto
{
    public class AuthorizeDto
    {
        public string AuthorizeUser { get; set; }
        public string AuthorizedUser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
