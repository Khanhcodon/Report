using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities.Customer;
using System;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(CategoryValidator))]
    public class CategoryModel : PacketModel
    {
        public CategoryModel() : base() { }

        /// <summary>
        /// Lấy hoặc thiết lập Id của thể loại văn bản
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên thể loại văn bản
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Category.CreateOrEdit.Fields.CategoryName.Label")]
        public string CategoryName { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>GiangPN@bkav.com - 290513</para>
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Category.CreateOrEdit.Fields.CategoryBusinessId.Label")]
        public int CategoryBusinessId { get; set; }

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
        /// Lấy hoặc thiết lập
        /// </summary>
        public List<int> CodeIds { get; set; }

        /// <summary>
        /// Mã mặc định
        /// </summary>
        public int DefaultCodeId { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập
        ///// </summary>
        //public List<bool> IsDefault { get; set; }
    }
}