using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov Online.
    /// Project: eGov Online.
    /// Class: GuideValidator - public - Validator.
    /// Create Date: 170714.
    /// Author: TrinhNVd.
    /// Description: Lớp chứa thông tin nhập liệu khi tạo mới, chỉnh sửa thông tin hướng dẫn
    /// </summary>
    public class GuideValidator : AbstractValidator<GuideModel>
    {
        /// <summary>
        /// Đối tượng ResourceService lấy ra các cảnh báo khi nhập liệu
        /// </summary>
        private readonly ResourceBll _resourceService;

        /// <summary>TrinhNVd - 170714
        /// Tạo ràng buộc nhập liệu cho tên và đường dẫn của hướng dẫn.
        /// </summary>
        public GuideValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_resourceService.GetResource("Common.Guide.CreateOrEdit.Fields.Name.Required"))
                .Length(1, 200).WithMessage(_resourceService.GetResource("Common.Guide.CreateOrEdit.Fields.Name.Length"));
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage(_resourceService.GetResource("Common.Guide.CreateOrEdit.Fields.Url.Required"))
                .Length(1, 500).WithMessage(_resourceService.GetResource("Common.Guide.CreateOrEdit.Fields.Url.Length"));
        }
    }
}