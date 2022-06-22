using System;
using FluentValidation;
using FluentValidation.Attributes;

namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : EgovValidatorFactory - public - Presentation
    /// Access Modifiers: 
    ///     * Inherit : FluentValidation.Attributes.AttributedValidatorFactory
    /// Create Date : 191012
    /// Author      : TrungVH
    /// Description : Provider thay thế cho provider validate mặc định của MVC
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class EgovValidatorFactory : AttributedValidatorFactory
    {
        #pragma warning disable 1591
        public override IValidator GetValidator(Type type)
        {
            if (type != null)
            {
                var attribute = (ValidatorAttribute)Attribute.GetCustomAttribute(type, typeof(ValidatorAttribute));
                if ((attribute != null) && (attribute.ValidatorType != null))
                {
                    var instance = Activator.CreateInstance(attribute.ValidatorType);
                    return instance as IValidator;
                }
            }
            return null;
        }
    }
}
