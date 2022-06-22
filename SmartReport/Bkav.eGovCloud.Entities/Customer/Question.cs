using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Bkav.eGovCloud.Core.Document;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov Online.
    /// Project: eGov Online.
    /// Class: Question - public - Entities.
    /// Create Date: 120814.
    /// Author: TrinhNVd.
    /// Description: Lớp chứa thông tin câu hỏi và câu trả lời
    /// </summary>
    [DataContract]
    public class Question
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int QuestionId { get; set; }

        /// <summary>
        /// Tiêu đề câu hỏi
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Tên dưới dạng Url thân thiện của Name.
        /// </summary>
        [DataMember]
        public string Tag { get; set; }

        /// <summary>
        /// Nội dung câu hỏi
        /// </summary>
        [DataMember]
        public string Detail { get; set; }

        /// <summary>
        /// Thời gian hỏi
        /// </summary>
        [DataMember]
        public DateTime Date { get; set; }

        /// <summary>
        /// Câu trả lời
        /// </summary>
        [DataMember]
        public string Answer { get; set; }

        /// <summary>
        /// Ngày trả lời
        /// </summary>
        [DataMember]
        public DateTime? AnswerDate { get; set; }

        /// <summary>
        /// Tên người hỏi
        /// </summary>
        [DataMember]
        public string AskPeople { get; set; }

        /// <summary>
        /// Tên người trả lời
        /// </summary>
        [DataMember]
        public string AnswerPeople { get; set; }

        /// <summary>
        /// Có được đưa lên trang Client hay k ~ true.
        /// </summary>
        [DataMember]
        public bool Active { get; set; }

        /// <summary>
        /// Loại câu hỏi: 0-hỏi đáp chung, 1-Câu hỏi hồ sơ
        /// </summary>
        [DataMember]
        public int? QuestionType { get; set; }

        /// <summary>
        /// Id hồ sơ - eGov Online
        /// </summary>
        [DataMember]
        public int? DocumentId { get; set; }

        /// <summary>
        /// Id co quan- eGov Online
        /// </summary>
        [DataMember]
        public int OfficeId { get; set; }

        /// <summary>
        /// Id hồ sơ - eGov
        /// </summary>
        [DataMember]
        public Guid? DocumentLocalId { get; set; }

        /// <summary>
        /// Email người hỏi
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Đã có câu trả lời hay chưa
        /// </summary>
        [DataMember]
        public bool HasAnswered { get; set; }

        /// <summary>
        /// Comment, gợi ý hỏi đáp, trả lời
        /// </summary>
        [DataMember]
        public string Comment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid DbDocumentId { get; set; }

        /// <summary>
        /// Id cán bộ được giao nhiệm vụ trả lời
        /// </summary>
        [DataMember]
        public int? eGovUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Doc_Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Doc_Compendium { get; set; }

    }
}
