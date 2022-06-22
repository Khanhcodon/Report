using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class TemplateQuery
    {
        /// <summary>
        /// Name.contain(keysearch);
        /// </summary>
        /// <param name="keySearch"></param>
        /// <returns></returns>
        public static Expression<Func<Template, bool>> IsParentAndContainsKey(string keySearch)
        {
            return t => t.Name.ToLower().Contains(keySearch.ToLower()) && !t.ParentId.HasValue;
        }

        /// <summary>
        /// ParentId == null
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<Template, bool>> IsParent()
        {
            return t => !t.ParentId.HasValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="docfieldIds"></param>
        /// <returns></returns>
        public static Expression<Func<Template, bool>> WithPermissionAndDocField(List<DocumentProcessType> permissions, List<int> docfieldIds)
        {
            // Là mẫu phiếu in, đang dc active và là mẫu in chung (mẫu cha)
            var result = IsPrintTemplate().And(IsActive()).And(IsParent());

            // Theo permission
            result = result.And(WithNonePermisson().Or(WithPermission(permissions)));

            // Theo docfield
            result = result.And(t => !t.DocFieldId.HasValue || (docfieldIds.Any() && docfieldIds.Contains(t.DocFieldId.Value)));

            return result;
        }

        /// <summary>
        /// HopCV:081015
        /// Lấy theo lĩnh vực, quyền và loại mẫu
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Expression<Func<Template, bool>> WithPermission(List<DocumentProcessType> permissions, int type)
        {
            // Là mẫu phiếu in, đang dc active và là mẫu in chung (mẫu cha)
            var result = IsActive().And(IsParent());

            // Theo permission
            result = result.And(WithNonePermisson().Or(WithPermission(permissions)));

            //  theo loai(- 1: Phiếu in, - 2: Email, - 3: SMS )
            result = result.And(t => t.Type == type);

            return result;
        }

        /// <summary>
        /// HopCV:081015
        /// Lấy theo lĩnh vực, quyền và loại mẫu
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="docfieldIds"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Expression<Func<Template, bool>> WithPermissionAndDocField(List<DocumentProcessType> permissions, List<int> docfieldIds, int type)
        {
            // Là mẫu phiếu in, đang dc active và là mẫu in chung (mẫu cha)
            var result = WithPermission(permissions, type)
                .And(t => t.DocFieldId.HasValue && (docfieldIds.Any()
                    && docfieldIds.Contains(t.DocFieldId.Value)));

            return result;
        }

        /// <summary>
        /// HopCV:081015
        /// Lấy theo loai văn bản, quyền và loai mẫu 
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="docTypeIds"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Expression<Func<Template, bool>> WithPermissionAndDocType(List<DocumentProcessType> permissions, List<Guid> docTypeIds, int type)
        {
            var result = WithPermission(permissions, type)
               .And(t => t.DoctypeId.HasValue && (docTypeIds.Any()
                   && docTypeIds.Contains(t.DoctypeId.Value)));

            return result;
        }

        /// <summary>
        /// T.Permisson == permisson
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public static Expression<Func<Template, bool>> WithPermission(List<DocumentProcessType> permissions)
        {
            return t => permissions.Any(p => (t.Permission & (int)p) == (int)p);
        }

        /// <summary>
        /// t.Permission == 0
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<Template, bool>> WithNonePermisson()
        {
            return t => t.Permission == 0;
        }

        /// <summary>
        /// t.IsActive
        /// </summary>
        /// <returns>Linq Expression</returns>
        public static Expression<Func<Template, bool>> IsActive()
        {
            return t => t.IsActive;
        }

        /// <summary>
        /// t.Type = Template.PhieuIn
        /// </summary>
        /// <returns>Linq expression</returns>
        public static Expression<Func<Template, bool>> IsPrintTemplate()
        {
            return t => t.Type == (int)TemplateType.PhieuIn;
        }

        /// <summary>
        /// t.Type = Template.Email
        /// </summary>
        /// <returns>Linq expression</returns>
        public static Expression<Func<Template, bool>> IsEmailTemplate()
        {
            return t => t.Type == (int)TemplateType.Email;
        }

        /// <summary>
        /// t.Type = Template.Sms
        /// </summary>
        /// <returns>Linq expression</returns>
        public static Expression<Func<Template, bool>> IsSmsTemplate()
        {
            return t => t.Type == (int)TemplateType.Sms;
        }
    }
}
