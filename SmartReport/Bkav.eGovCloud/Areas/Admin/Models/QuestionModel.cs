using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov Online.
    /// Project: eGov Online.
    /// Class: IQuestionService - public - Model.
    /// Create Date: 120814.
    /// Author: TrinhNVd.
    /// Description: Chứa thông tin hướng dẫn.
    /// </summary>
    [FluentValidation.Attributes.Validator(typeof(QuestionValidator))]
    public class QuestionModel
    {
        private readonly DocumentBll _documentService;

        public QuestionModel()
        {
            _documentService = DependencyResolver.Current.GetService<DocumentBll>();
        }

        /// <summary>
        /// Mã câu hỏi
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Nội dung câu hỏi
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// Thời gian hỏi
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Người hỏi
        /// </summary>
        public string AskPeople { get; set; }

        /// <summary>
        /// Người trả lời
        /// </summary>
        public string AnswerPeople { get; set; }

        /// <summary>
        /// Câu trả lời
        /// </summary>
        public string Answer { get; set; }

        //Ngày trả lời
        public DateTime? AnswerDate { get; set; }

        /// <summary>
        /// Có được lên trang Client hay k
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Loại câu hỏi: 0-hỏi đáp chung, 1-Câu hỏi hồ sơ
        /// </summary>
        public int? QuestionType { get; set; }

        /// <summary>
        /// Id hồ sơ - eGov Online
        /// </summary>
        public int? DocumentId { get; set; }

        /// <summary>
        /// Id hồ sơ - eGov
        /// </summary>
        public Guid? DocumentLocalId { get; set; }

        /// <summary>
        /// Email người hỏi
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Đã có câu trả lời hay chưa
        /// </summary>
        public bool HasAnswered { get; set; }

        /// <summary>
        /// Mã lĩnh vực
        /// </summary>
        public int? DocfieldId { get; set; }

        public int? eGovUserId { get; set; }

        /// <summary>
        /// Comment, gợi ý hỏi đáp, trả lời
        /// </summary>
        public string Comment { get; set; }

        public bool IsGeneralQuestion
        {
            get
            {
                return QuestionType == null || QuestionType == 0;
            }
        }

        public Document Document
        {
            get
            {

                if (DocumentLocalId.HasValue)
                {
                    var document = _documentService.Get(DocumentLocalId.Value);
                    if (document != null)
                    {
                        return document;
                    }
                }
                return new Document();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DocCode
        {
            get
            {
                return Document.DocCode;
            }
            set
            {
                this.DocCode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Compendium
        {
            get
            {
                return Document.Compendium;
            }
            set
            {
                this.Compendium = value;
            }
        }

        public UserHolder AnswerHolder { get; set; }

        //}

        public UserHolder CurrentUser { get; set; }

        public List<UserComment> UserComments { get; set; }

    }

    public class UserHolder
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Account { get; set; }
        public string FullAccount { get; set; }
        public string Department { get; set; }

    }

    public class UserComment
    {
        public string FullName { get; set; }

        public string Account { get; set; }

        public string Comment { get; set; }

        public DateTime CommentDate { get; set; }
    }
}