using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;


namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportRuleValidator : AbstractValidator<ReportRuleModel>
    {
        public ReportRuleValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Mã không được để trống!");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống!");
        }
    }
}