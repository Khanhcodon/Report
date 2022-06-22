using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Areas.Admin.Validator;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(OnlineTemplateValidator))]
    public class OnlineTemplateModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int OnlineTemplateId { get; set; }

        /// <summary>
        /// Tên biểu mẫu hành chính
        /// </summary>
        [LocalizationDisplayName("Admin.OnlineTemplate.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// File Id
        /// </summary>
        [LocalizationDisplayName("Admin.OnlineTemplate.CreateOrEdit.Fields.File.Label")]
        public int FileId { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [LocalizationDisplayName("Admin.OnlineTemplate.CreateOrEdit.Fields.Description.Label")]
        public string Description { get; set; }

        ///// <summary>
        /////
        ///// </summary>
        //public virtual File File { get; set; }

    }
}