using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(StoreValidator))]
    public class StoreModel
    {
        private readonly ResourceBll _resourceService = DependencyResolver.Current.GetService<ResourceBll>();

        /// <summary>
        /// Lấy hoặc thiết lập Id của sổ hồ sơ
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên sổ hồ sơ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Store.CreateOrEdit.Fields.StoreName.Label")]
        public string StoreName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người phụ trách
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Store.CreateOrEdit.Fields.UserNameResponsible.Label")]
        public int? UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên người phụ trách
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id đơn vị, phòng ban phụ trách
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Store.CreateOrEdit.Fields.DepartmentNameResponsible.Label")]
        public int? DepartmentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên phòng phụ trách
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id người xem (json)
        /// </summary>
        public string UserViewIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id người xem
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Store.CreateOrEdit.Fields.ListUserViewStore.Label")]
        public List<int> ListUserViewIds
        {
            get
            {
                List<int> result;
                try
                {
                    result = Json2.ParseAs<List<int>>(UserViewIds);
                }
                catch (Exception)
                {
                    result = new List<int>();
                }
                return result;
            }
            set { UserViewIds = value.StringifyJs(); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Store.CreateOrEdit.Fields.Description.Label")]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã của danh mục nghiệp vụ
        /// </summary>
        public int CategoryBusinessId { set; get; }

        /// <summary>
        /// Lấy hoặc thiết lập tên của danh mục nghiệp vụ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Store.CreateOrEdit.Fields.CategoryBusinessName.Label")]
        public string CategoryBusinessName
        {
            get { return _resourceService.GetEnumDescription<CategoryBusinessTypes>((CategoryBusinessTypes)CategoryBusinessId); }
        }

        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id của mẫu sổ hồ sơ
        /// </summary>
        public string CodeIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id Docfield (json)
        /// </summary>
        public string DocFieldIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id Docfield 
        /// </summary>
        public List<int> ListDocFieldIds
        {
            get
            {
                List<int> result;
                try
                {
                    result = Json2.ParseAs<List<int>>(DocFieldIds);

                }
                catch (Exception)
                {
                    result = new List<int>();
                }
                return result;
            }
            set { DocFieldIds = value.StringifyJs(); }
        }

        /// <summary>
        /// Lấy các item được checked
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Lấy code default
        /// </summary>
        public int DefaultCodeId { get; set; }

        /// <summary>
        /// Lấy storeId nào là default
        /// </summary>
        public bool IsDefault { get; set; }
    }
}