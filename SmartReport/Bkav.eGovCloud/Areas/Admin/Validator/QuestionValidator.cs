using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovCloud.Business.Common;
using FluentValidation;

namespace Bkav.eGovCloud.Areas.Admin.Validator
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov Online.
    /// Project: eGov Online.
    /// Class: QuestionValidator - public - Validator.
    /// Create Date: 120814.
    /// Author: TrinhNVd.
    /// Description: Lớp chứa thông tin nhập liệu khi tạo mới, chỉnh sửa thông tin hỏi đáp
    /// </summary>
    public class QuestionValidator : AbstractValidator<QuestionModel>
    {
        /// <summary>
        /// Đối tượng ResourceService lấy ra các cảnh báo khi nhập liệu
        /// </summary>
        private readonly ResourceBll _resourceService;

        /// <summary>TrinhNVd - 120814
        /// Tạo ràng buộc nhập liệu cho tên, người hỏi, người trả lời, nội dung câu hỏi.
        /// </summary>
        public QuestionValidator()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_resourceService.GetResource("Common.Question.CreateOrEdit.Fields.Name.Required"))
                .Length(0, 200).WithMessage(_resourceService.GetResource("Common.Question.CreateOrEdit.Fields.Name.Length"));
            RuleFor(x => x.AskPeople)
                .NotEmpty().WithMessage(_resourceService.GetResource("Common.Question.CreateOrEdit.Fields.AskPeople.Required"))
                .Length(1, 100).WithMessage(_resourceService.GetResource("Common.Question.CreateOrEdit.Fields.AskPeople.Length"));
            RuleFor(x => x.AnswerPeople)
                .NotEmpty().WithMessage(_resourceService.GetResource("Common.Question.CreateOrEdit.Fields.AnswerPeople.Required"))
                .Length(1, 100).WithMessage(_resourceService.GetResource("Common.Question.CreateOrEdit.Fields.AnswerPeople.Length"));
            RuleFor(x => x.Detail)
                .NotEmpty().WithMessage(_resourceService.GetResource("Common.Question.CreateOrEdit.Fields.Detail.Required"));
            RuleFor(x => x.Answer)
                .NotEmpty().WithMessage(_resourceService.GetResource("Common.Question.CreateOrEdit.Fields.Answer.Required"));
        }
    }
}