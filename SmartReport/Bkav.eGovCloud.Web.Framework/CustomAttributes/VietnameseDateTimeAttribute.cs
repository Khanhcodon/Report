using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;

namespace Bkav.eGovCloud.Web.Framework
{
    // TODO: Lớp này chắc sẽ bỏ đi. có thể tùy theo culture hiện tại là gì thì ValidationAttribute tự check theo đúng format của culture đó rồi. Custom attribute này không giải quyết triệt để được nhiều loại culture khác nhau.
    // TODO: Nếu không thì cần sửa attribute này về dạng tổng quát, và dựa tren CurrentCulture thay vì là fix cứng culture vi-VN.
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : VietnameseDateTimeAttribute - public - Framework
    /// Access Modifiers: 
    ///     * Inherit : System.ComponentModel.ValidationAttribute
    ///     * Implement: IClientValidatable
    /// Create Date : 270812
    /// Author      : TrungVH
    /// Description : 1 custom attribute hỗ trợ kiểm tra ngày tháng theo dạng dd/MM/yyyy
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class VietnameseDateTimeAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="resourceKey">Key của resource</param>
        public VietnameseDateTimeAttribute(string resourceKey)
        {
            ResourceKey = resourceKey;
        }

        private string ResourceKey { get; set; }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class. 
        /// </returns>
        /// <param name="value">The value to validate.</param><param name="validationContext">The context information about the validation operation.</param>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result = null;
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
            }
            else
            {
                DateTime date;
                var valid = DateTime.TryParse(value.ToString(),
                                            CultureInfo.GetCultureInfo("vi-VN").DateTimeFormat,
                                            DateTimeStyles.None,
                                            out date);
                if (!valid)
                {
                    result = new ValidationResult(GetErrorMessageResource());
                }
            }
            return result;
        }

        private string GetErrorMessageResource()
        {
            var resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            return resourceService.GetResource(ResourceKey);
        }

        /// <summary>
        /// When implemented in a class, returns client validation rules for that class.
        /// </summary>
        /// <returns>
        /// The client validation rules for this validator.
        /// </returns>
        /// <param name="metadata">The model metadata.</param><param name="context">The controller context.</param>
        public IEnumerable<System.Web.Mvc.ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new System.Web.Mvc.ModelClientValidationRule
            {
                ValidationType = "vndatetime",
                ErrorMessage = GetErrorMessageResource()
            };

            rule.ValidationParameters["propertyname"] = metadata.PropertyName;

            return new[] { rule };
        }
    }
}
