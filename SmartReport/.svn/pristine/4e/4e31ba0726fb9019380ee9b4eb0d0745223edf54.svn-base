using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Web.Framework;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class AccountModel
    {
        public int AccountId { get; set; }

        [LocalizationDisplayName("Account.CreateOrEdit.Fields.Username.Label")]
        public string Username { get; set; }

        [LocalizationDisplayName("Account.CreateOrEdit.Fields.Password.Label")]
        public string Password { get; set; }

        public string DomainName { get; set; }

        public string OpenId { get; set; }

        [LocalizationDisplayName("Account.CreateOrEdit.Fields.FullName.Label")]
        public string FullName { get; set; }

        [LocalizationDisplayName("Account.CreateOrEdit.Fields.Gender.Label")]
        public bool Gender { get; set; }

        [LocalizationDisplayName("Account.CreateOrEdit.Fields.Email.Label")]
        public string Email { get; set; }

        [LocalizationDisplayName("Account.CreateOrEdit.Fields.Phone.Label")]
        public string Phone { get; set; }

        [LocalizationDisplayName("Account.CreateOrEdit.Fields.Fax.Label")]
        public string Fax { get; set; }

        [LocalizationDisplayName("Account.CreateOrEdit.Fields.Address.Label")]
        public string Address { get; set; }

        [LocalizationDisplayName("Account.CreateOrEdit.Fields.IsActivated.Label")]
        public bool IsActivated { get; set; }

        public int? CreatedByUserId { get; set; }

        public DateTime? CreatedOnDate { get; set; }

        public int? LastModifiedByUserId { get; set; }

        public DateTime? LastModifiedOnDate { get; set; }

        /// <summary>
        /// Cho phép cán bộ xem giám sát của hệ thống tập trung
        /// </summary>
        public bool HasViewReport { get; set; }

        /// <summary>
        /// Danh sách các domain id ngăn cách theo dấu ,
        /// </summary>
        public string Domains { get; set; }

        public IEnumerable<int> DomainIds
        {
            get
            {
                if (string.IsNullOrEmpty(Domains))
                {
                    return new List<int>();
                }

                return Domains.Split(',').Select(d => int.Parse(d));
            }
        }
    }
}