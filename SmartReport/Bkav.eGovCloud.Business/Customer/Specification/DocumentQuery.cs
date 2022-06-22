using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Static Class : CategoryQuery - public - BLL
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 271112
    /// <para></para> Author      : TienBV
    /// <para></para> Description : Các điều kiện truy vấn cho bảng Document
    /// </summary>
    public static class DocumentQuery
    {
        /// <summary> Tienbv 301112
        /// Query lấy ra danh sách các hồ sơ, văn bản để add vào danh sách liên quan.
        /// </summary>
        /// <param name="startWith">Từ khóa tìm kiếm.</param>
        /// <returns></returns>
        public static Expression<Func<Document, bool>> StartWith(string startWith)
        {
            return d => (d.Compendium.ToLower().StartsWith(startWith)
                        || d.Compendium2.ToLower().StartsWith(startWith)
                        || d.DocCode.StartsWith(startWith)
                        || d.CitizenName.ToLower().StartsWith(startWith)
                        || d.Email.ToLower().StartsWith(startWith));
        }
    }
}
