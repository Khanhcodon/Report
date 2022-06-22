using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(FormGroupValidator))]
    public class FormGroupModel : PacketModel
    {
        public FormGroupModel()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int FormGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên nhóm mẫu
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.FormGroup.CreateOrEdit.Fields.FormGroupName.Label")]
        public string FormGroupName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách mẫu con
        /// </summary>
        public List<Guid> FormIds { get; set; }
    }
}