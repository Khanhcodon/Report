using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(CitizenValidator))]
    public class CitizenModel
    {
        public int Id { get; set; }

        [LocalizationDisplayName("Citizen.CreateOrEdit.Fields.Account.Label")]
        public string Account { get; set; }

        [LocalizationDisplayName("Citizen.CreateOrEdit.Fields.Password.Label")]
        public string Password { get; set; }

        [LocalizationDisplayName("Citizen.CreateOrEdit.Fields.ConfirmPassword.Label")]
        public string ConfirmPassword { get; set; }

        [LocalizationDisplayName("Citizen.CreateOrEdit.Fields.FirstName.Label")]
        public string FirstName { get; set; }

        [LocalizationDisplayName("Citizen.CreateOrEdit.Fields.LastName.Label")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName))
                    return string.Empty;

                return string.Format("{0} {1}", FirstName.Trim(), LastName.Trim()).Trim();
            }
        }

        [LocalizationDisplayName("Citizen Name")]
        public string CitizenName { get; set; }

        [LocalizationDisplayName("Citizen.CreateOrEdit.Fields.PhoneNumber.Label")]
        public string PhoneNumber { get; set; }

        [LocalizationDisplayName("Citizen.CreateOrEdit.Fields.Email.Label")]
        public string Email { get; set; }

        [LocalizationDisplayName("Citizen.CreateOrEdit.Fields.IdCardNumber.Label")]
        public string IdCardNumber { get; set; }

        [LocalizationDisplayName("Citizen.CreateOrEdit.Fields.DateOfIssue.Label")]
        public DateTime? DateOfIssue { get; set; }

        [LocalizationDisplayName("Citizen.CreateOrEdit.Fields.PlaceOfIssue.Label")]
        public string PlaceOfIssue { get; set; }

        [LocalizationDisplayName("Citizen.CreateOrEdit.Fields.Address.Label")]
        public string Address { get; set; }

        [LocalizationDisplayName("Citizen.CreateOrEdit.Fields.IsActivated.Label")]
        public bool IsActivated { get; set; }
    }
}