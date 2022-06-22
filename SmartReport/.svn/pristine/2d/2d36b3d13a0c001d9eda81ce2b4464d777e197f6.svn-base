using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : CodeQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 200912
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng Paper
    /// </summary>
    public static class PaperQuery
    {
        /// <summary>
        /// PaperId == paperId
        /// </summary>
        /// <param name="paperId">Id của giấy tờ.</param>
        /// <returns></returns>
        public static Expression<Func<Paper, bool>> WithId(int paperId)
        {
            return s => s.PaperId == paperId;
        }

        /// <summary>
        /// DocTypeId == doctypeId
        /// </summary>
        /// <param name="doctypeId">Id của loại hồ sơ.</param>
        /// <returns></returns>
        public static Expression<Func<Paper, bool>> WithDocTypeId(Guid? doctypeId)
        {
            return s => !doctypeId.HasValue || (doctypeId.HasValue && s.DocTypeId == doctypeId.Value);
        }

        /// <summary>
        /// PaperName == papername
        /// </summary>
        /// <param name="papername">Tên của giấy tờ.</param>
        /// <returns></returns>
        public static Expression<Func<Paper, bool>> WithName(string papername)
        {
            return s => s.PaperName.Equals(papername, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// DoctypeId = doctypeid and PaperTypeId = paperType
        /// </summary>
        /// <param name="doctypeId">Document type id</param>
        /// <param name="paperType">Enum.paperType</param>
        /// <returns></returns>
        public static Expression<Func<Paper, bool>> WithDocTypeAndPaperType(Guid doctypeId, PaperType paperType)
        {
            return s => s.DocTypeId.Equals(doctypeId) && s.PaperTypeId.Equals((int)paperType);
        }
    }
}
