using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(DocumentRelatedValidator))]
    public class DocumentRelatedModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int DocumentRelatedId { get; set; }

        /// <summary>
        /// 
        /// </summary>
          [LocalizationDisplayName("DocumentRelated.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UpdatedBy { get; set; }
        public List<DocumentRelatedModel> List { get; set; }
        
        public string EmbryonicPath { get; set; }
        [LocalizationDisplayName("DocumentRelated.CreateOrEdit.Fields.File.Label")]
        public string EmbryonicLocationName { get; set; }

    }
}