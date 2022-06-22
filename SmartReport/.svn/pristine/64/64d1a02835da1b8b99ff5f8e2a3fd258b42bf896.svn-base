using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(TemplateKeyValidator))]
    public class TemplateKeyModel
    {
        public int id { get; set; }
        /// <summary>
        /// Get or set the primary key.
        /// </summary>
        public int TemplateKeyId { get; set; }

        /// <summary>
        /// Biểu mẫu
        /// </summary>
        public Guid FormId { get; set; }

        /// <summary>
        /// Loại hồ sơ
        /// </summary>
        public Guid? DoctypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên key
        /// </summary>
        [LocalizationDisplayName("Common.TemplateKey.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã key
        /// </summary>
        [LocalizationDisplayName("Common.TemplateKey.CreateOrEdit.Fields.Code.Label")]
        public string Code { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn dữ liệu cho key
        /// </summary>
        [LocalizationDisplayName("Common.TemplateKey.CreateOrEdit.Fields.Sql.Label")]
        public string Sql { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu hiển thị cho key
        /// </summary>
        [LocalizationDisplayName("Common.TemplateKey.CreateOrEdit.Fields.HtmlTemplate.Label")]
        public string HtmlTemplate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại key: 1. key đặc biệt, 2. key chung
        /// </summary>
        [LocalizationDisplayName("Common.TemplateKey.CreateOrEdit.Fields.Type.Label")]
        public int Type { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập sử dụng key.
        /// </summary>
        [LocalizationDisplayName("Common.Resource.CreateOrEdit.Fields.IsActive.Label")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Là key dc import từ form
        /// </summary>
        public bool IsCustomKey { get; set; }

        /// <summary>
        /// Control Id trong form.
        /// </summary>
        public Guid? KeyIdInForm { get; set; }

        public virtual DocTypeModel Doctype { get; set; }

        [LocalizationDisplayName("Common.TemplateKey.CreateOrEdit.Fields.Category.Label")]
        public int CategoryId { get; set; }

        public virtual string CategoryName { get; set; }
        public string ReportQueryId { get; set; }


    }
    public class TemplateKeyResult {
        public bool Success { get; set; }
        public string ExpiciteTemplate { get; set; }
        public int Type { get; set; }
        public string HtmlTemplate { get; set; }
        public string NewValue { get; set; }


    }
    public class TemplateKeyTree
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<ChidrenTemplate> ChidrenList { get; set; }
    }

    public class ChidrenTemplate
    {
        public int TemplateKeyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}