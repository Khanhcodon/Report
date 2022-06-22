using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : TransferTypeQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : Các điều kiện truy vấn cho bảng TransferType
    /// </summary>
    public static class TransferTypeQuery
    {
        /// <summary>
        /// TransferTypeId == transferTypeId
        /// </summary>
        /// <param name="transferTypeId">Id của hình thức chuyển.</param>
        /// <returns></returns>
        public static Expression<Func<TransferType, bool>> WithId(int transferTypeId)
        {
            return s => s.TransferTypeId == transferTypeId;
        }


        /// <summary>
        /// TransferTypeName == transferTypeName
        /// </summary>
        /// <param name="transferTypeName">Tên hình thức chuyển.</param>
        /// <returns></returns>
        public static Expression<Func<TransferType, bool>> WithName(string transferTypeName)
        {
            return s => s.TransferTypeName == transferTypeName;
        }

        /// <summary>
        /// TransferTypeName == transferTypeName
        /// </summary>
        /// <param name="transferTypeName">Tên hình thức chuyển.</param>
        /// <returns></returns>
        public static Expression<Func<TransferType, bool>> ContainsName(string transferTypeName)
        {
            return s => s.TransferTypeName.ToLower().Contains(transferTypeName.ToLower());
        }
    }
}
