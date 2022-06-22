using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Core.Utils;
using System;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(DocFieldValidator))]
    public class DocFieldModel : PacketModel
    {
        public DocFieldModel()
            : base()
        {
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id của lĩnh vực
        /// </summary>
        public int DocFieldId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên lĩnh vực
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.DocField.CreateOrEdit.Fields.DocFieldName.Label")]
        public string DocFieldName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra lĩnh vực này đã được kích hoạt
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.DocField.CreateOrEdit.Fields.IsActivated.Label")]
        public bool IsActivated { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>GiangPN@bkav.com - 290513</para>
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.DocField.CreateOrEdit.Fields.CategoryBusinessId.Label")]
        public int CategoryBusinessId { get; set; }

        /// <summary>
        ///Tên file icon 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.DocField.CreateOrEdit.Fields.IconFileName.Label")]
        public string IconFileName { get; set; }

        /// <summary>
        ///Tên file icon 
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.DocField.CreateOrEdit.Fields.IconFileName.Label")]
        public string IconFileDisplayName { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>GiangPN@bkav.com - 290513</para>
        /// </summary>
        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>GiangPN@bkav.com - 290513</para>
        /// </summary>
        public List<int> CategoryBusiness { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id StoreId (json)
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.DocField.CreateOrEdit.Fields.StoreIds.Label")]
        public string StoreIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id Docfield 
        /// </summary>
        public List<int> ListStoreIds
        {
            get
            {
                List<int> result;
                try
                {
                    result = Json2.ParseAs<List<int>>(StoreIds);
                }
                catch
                {
                    result = new List<int>();
                }
                return result;
            }
            set { StoreIds = value.StringifyJs(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Guid> DocTypeIds { get; set; }
    }
}