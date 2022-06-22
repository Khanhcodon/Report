using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : FileLocationQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 070313
    /// Author      : TrungVH
    /// Description : Các điều kiện truy vấn cho bảng role
    /// </summary>
    public static class FileLocationQuery
    {
        /// <summary>
        /// FileLocationId == fileLocationId
        /// </summary>
        /// <param name="fileLocationId">Id của vị trí lưu file.</param>
        /// <returns></returns>
        public static Expression<Func<FileLocation, bool>> WithId(int fileLocationId)
        {
            return s => s.FileLocationId == fileLocationId;
        }

        /// <summary>
        /// IsActivated == isActivated
        /// </summary>
        /// <param name="isActivated">Kích hoạt.</param>
        /// <returns></returns>
        public static Expression<Func<FileLocation, bool>> WithIsActivated(bool? isActivated = null)
        {
            return r => !isActivated.HasValue || r.IsActivated == isActivated;
        }
    }
}
