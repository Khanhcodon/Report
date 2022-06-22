using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
//using Bkav.eGovCloud.eGovService;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(BussinessDocFieldDocTypeGroupValidator))]
    public class BussinessDocFieldDocTypeGroupModel
    {
        public BussinessDocFieldDocTypeGroupModel()
        {
            IsActived = true;
            CreatedDate = DateTime.Now;
            CategoryBusinessId = (int)CategoryBusinessTypes.VbDen;
        }

        /// <summary>
        /// id
        /// </summary>
        public int BussinessDocFieldDocTypeGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên cho nhóm
        /// </summary>
        [LocalizationDisplayName("BussinessDocFieldDocTypeGroup.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nghiệp vụ cho nhóm
        /// </summary>
        [LocalizationDisplayName("BussinessDocFieldDocTypeGroup.CreateOrEdit.Fields.CategoryBusinessId.Label")]
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập lĩnh vực
        /// </summary>
        [LocalizationDisplayName("BussinessDocFieldDocTypeGroup.CreateOrEdit.Fields.DocFieldId.Label")]
        public int? DocFieldId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại văn bản
        /// </summary>
        [LocalizationDisplayName("BussinessDocFieldDocTypeGroup.CreateOrEdit.Fields.DocTypeId.Label")]
        public string DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái kích hoạt
        /// </summary>
        [LocalizationDisplayName("BussinessDocFieldDocTypeGroup.CreateOrEdit.Fields.IsActived.Label")]
        public bool IsActived { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}