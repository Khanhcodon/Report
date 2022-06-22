using System;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Areas.Admin.Validator;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(DomainValidator))]
    public class DomainModel
    {
        //private ICollection<AccountModel> _accounts;

        public int DomainId { get; set; }

        public int ServerId { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Fields.DomainName.Label")]
        public string DomainName { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Fields.CustomerName.Label")]
        public string CustomerName { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Fields.Email.Label")]
        public string Email { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Fields.Phone.Label")]
        public string Phone { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Fields.Address.Label")]
        public string Address { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Fields.CustomerType.Label")]
        public bool CustomerType { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Fields.Province.Label")]
        public string Province { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Fields.District.Label")]
        public string District { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Fields.Commune.Label")]
        public string Commune { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Fields.Department.Label")]
        public string Department { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Fields.IsActivated.Label")]
        public bool IsActivated { get; set; }

        public int? CreatedByUserId { get; set; }

        public DateTime? CreatedOnDate { get; set; }

        public int? LastModifiedByUserId { get; set; }

        public DateTime? LastModifiedOnDate { get; set; }

        public ConnectionModel Connection { get; set; }

        [LocalizationDisplayName("Account.CreateOrEdit.Fields.Username.Label")]
        public string AccountUsername { get; set; }

        [LocalizationDisplayName("Account.CreateOrEdit.Fields.Password.Label")]
        public string AccountPassword { get; set; }

        public int[] DomainIds { get; set; }

        [LocalizationDisplayName("Domain.CreateOrEdit.Fields.IsPrimary.Label")]
        public bool IsPrimary { get; set; }
    }
}