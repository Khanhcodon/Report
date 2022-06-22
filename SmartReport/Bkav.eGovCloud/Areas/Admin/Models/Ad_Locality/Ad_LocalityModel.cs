using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using System.Collections.Generic;
using System;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(Ad_LocalityValidator))]
    public class Ad_LocalityModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id kỳ báo cáo
        /// </summary>
        public Guid LocalityId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên kỳ báo cáo
        /// </summary>
        //[LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.ActionLevelName.Label")]
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string LocalityName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mã kỳ báo cáo
        /// </summary>
        //[LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.ActionLevelCode.Label")]
        [LocalizationDisplayName("BussinessDocFieldDocTypeGroup.CreateOrEdit.Fields.IsActived.Label")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thời gian bắt đầu
        /// </summary>
        //[LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.StartTime.Label")]
        public int Type { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Thời gian kết thúc
        /// </summary>
        //[LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.EndTime.Label")]
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key lưu trữ
        /// </summary>
        //[LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.TemplateKey.Label")]
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public bool IsActive { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập Key lưu trữ
        /// </summary
        [LocalizationDisplayName("InCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label")]
        public string Id { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LocalityParent { get; set; }

        public IEnumerable<Ad_LocalityModel> Ad_Localitys { get; set; }

    }
}