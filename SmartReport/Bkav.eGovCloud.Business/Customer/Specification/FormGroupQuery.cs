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
    public static class FormGroupQuery
    {
        /// <summary>
        /// formGroupId == formGroupId
        /// </summary>
        /// <param name="formGroupId">Id của nhóm biểu mẫu.</param>
        /// <returns></returns>
        public static Expression<Func<FormGroup, bool>> WithId(int formGroupId)
        {
            return s => s.FormGroupId == formGroupId;
        }

        /// <summary>
        /// Name == name
        /// </summary>
        /// <param name="name">Tên của nhóm biểu mẫu.</param>
        /// <returns></returns>
        public static Expression<Func<FormGroup, bool>> WithName(string name)
        {
            return s => s.FormGroupName == name;
        }
    }
}
