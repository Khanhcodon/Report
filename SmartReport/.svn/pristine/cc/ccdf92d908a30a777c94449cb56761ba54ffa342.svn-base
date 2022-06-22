using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    public class ReportQueryFilterValidator : AbstractValidator<ReportQueryFilterModel>
    {
        private readonly ResourceBll _resourceService;

        public ReportQueryFilterValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
        }
    }
}