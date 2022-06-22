using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : PositionQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 060912
    /// Author      : GiangPN
    /// Description : Các điều kiện truy vấn cho bảng Position
    /// </summary>
    public static class PositionQuery
    {
        /// <summary>
        /// PositionId == positionId
        /// </summary>
        /// <param name="positionId">Id của chức vụ.</param>
        /// <returns></returns>
        public static Expression<Func<Position, bool>> WithId(int positionId)
        {
            return s => s.PositionId == positionId;
        }

        /// <summary>
        /// PositionName == positionName
        /// </summary>
        /// <param name="positionName">Tên chức vụ.</param>
        /// <returns></returns>
        public static Expression<Func<Position, bool>> WithPositionName(string positionName)
        {
            return s => s.PositionName == positionName;
        }

        /// <summary>
        /// PositionName == positionName
        /// </summary>
        /// <param name="positionName">Tên chức vụ</param>
        /// <returns></returns>
        public static Expression<Func<Position, bool>> ContainsKey(string positionName)
        {
            return s => s.PositionName.ToLower().Contains(positionName.ToLower());
        }
    }
}
