using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(TemplateValidator))]
    public class TemplateModel
    {
        /// <summary>
        /// Key
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên mẫu
        /// </summary>
        [LocalizationDisplayName("Admin.Template.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nội dung mãua
        /// </summary>
        [LocalizationDisplayName("Admin.Template.CreateOrEdit.Fields.Content.Label")]
        [AllowHtml]
        public string Content { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập lĩnh vực
        /// </summary>
        [LocalizationDisplayName("Admin.Template.CreateOrEdit.Fields.DocFieldId.Label")]
        public int? DocFieldId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại hồ sơ cho mẫu
        /// </summary>
        [LocalizationDisplayName("Admin.Template.CreateOrEdit.Fields.DoctypeId.Label")]
        public Guid? DoctypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu cha
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại mẫu:
        /// <para>  - 1: Phiếu in </para>
        /// <para>  - 2: Email </para>
        /// <para>  - 3: SMS </para>
        /// </summary>
        [LocalizationDisplayName("Admin.Template.CreateOrEdit.Fields.Type.Label")]
        public int Type { get; set; }

        /// <summary>
        /// Lấy kiểu của mẫu dưới dang enum
        /// </summary>
        public TemplateType TypeInEnum
        {
            get
            {
                return (TemplateType)Type;
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập quyền sử dụng mẫu
        /// </summary>
        [LocalizationDisplayName("Admin.Template.CreateOrEdit.Fields.Permission.Label")]
        public int Permission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái sử dụng của mẫu
        /// </summary>
        [LocalizationDisplayName("Admin.Template.CreateOrEdit.Fields.IsActive.Label")]
        public bool IsActive { get; set; }

        [LocalizationDisplayName("Admin.Template.CreateOrEdit.Fields.ContentFile.Label")]
        public string ContentFile { get; set; }

        [LocalizationDisplayName("Admin.Template.CreateOrEdit.Fields.ContentFileLocalName.Label")]
        public string ContentFileLocalName { get; set; }

        [LocalizationDisplayName("Admin.Template.CreateOrEdit.Fields.Sql.Label")]
        public string Sql { get; set; }

        public DocField DocField { get; set; }

        public DocType DocType { get; set; }

        public List<int> Permissions { get; set; }

        [LocalizationDisplayName("Admin.Template.CreateOrEdit.Fields.Permission.Label")]
        public int CommonTemplate { get; set; }

        public List<int> CommonTemplates { get; set; }

        /// <summary>
        /// HopCV:220116
        /// Lấy hoặc thiết lập tiêu đề khi gửi mail(chỉ dùng đối với mẫu gửi mail)
        /// </summary>
        [LocalizationDisplayName("Admin.Template.CreateOrEdit.Fields.TitleMail.Label")]
        public string TitleMail { get; set; }
    }
}