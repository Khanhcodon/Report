using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : FormGroupQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 011013
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng FormGroup
    /// </summary>
    public static class FormQuery
    {
        /// <summary>
        /// formId == formId
        /// </summary>
        /// <param name="formId">Id của biểu mẫu.</param>
        /// <returns></returns>
        public static Expression<Func<Form, bool>> WithId(Guid formId)
        {
            return s => s.FormId == formId;
        }

        /// <summary>
        /// FormGroupId == formGroupId
        /// </summary>
        /// <param name="formGroupId">Id của nhóm biểu mẫu.</param>
        /// <returns></returns>
        public static Expression<Func<Form, bool>> WithFormGroupId(int? formGroupId)
        {
            return s => !formGroupId.HasValue || s.FormGroupId == formGroupId;
        }

        /// <summary>
        /// FormTypeId == formTypeId
        /// </summary>
        /// <param name="formTypeId">Id của loại mẫu.</param>
        /// <returns></returns>
        public static Expression<Func<Form, bool>> WithFormTypeId(int? formTypeId)
        {
            return s => !formTypeId.HasValue || s.FormTypeId == formTypeId;
        }
    }
}
