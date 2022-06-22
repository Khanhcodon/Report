using System;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Static Class : DepartmentQuery - public - BLL
    /// Access Modifiers: 
    /// Create Date : 080912
    /// Author      : GiangPN
    /// Description : Các điều kiện truy vấn cho bảng Department
    /// </summary>
    public static class DepartmentQuery
    {
        /// <summary>
        /// DepartmentId == departmentId
        /// </summary>
        /// <param name="departmentId">Id của phòng ban.</param>
        /// <returns></returns>
        public static Expression<Func<Department, bool>> WithId(int departmentId)
        {
            return s => s.DepartmentId == departmentId;
        }

        /// <summary>
        /// ParentId == parentId
        /// </summary>
        /// <param name="parentId">Id của phòng ban cấp cha.</param>
        /// <returns></returns>
        public static Expression<Func<Department, bool>> WithParentId(int parentId)
        {
            return s => s.ParentId == parentId;
        }

        /// <summary>
        /// DepartmentName == departmentName
        /// </summary>
        /// <param name="departmentName">Tên phòng ban.</param>
        /// <returns></returns>
        public static Expression<Func<Department, bool>> WithDepartmentName(string departmentName)
        {
            return s => s.DepartmentName.ToLower() == departmentName.ToLower();
        }

        /// <summary>
        /// DepartmentIdExt == departmentIdExt
        /// </summary>
        /// <param name="departmentIdExt">Id phòng ban lưu dưới dạng mở rộng: dạng 2.33.34</param>
        /// <returns></returns>
        public static Expression<Func<Department, bool>> ContainsDepartmentIdExt(string departmentIdExt)
        {
            return s => s.DepartmentIdExt.ToLower().StartsWith(departmentIdExt.ToLower());
        }

        /// <summary>
        /// IsActivated == isActivated
        /// </summary>
        /// <param name="isActivated">Kích hoạt.</param>
        /// <returns></returns>
        public static Expression<Func<Department, bool>> WithIsActivated(bool? isActivated = null)
        {
            return r => !isActivated.HasValue || r.IsActivated == isActivated;
        }

        /// <summary>
        /// DepartmentPath.Contain(contain)
        /// </summary>
        /// <param name="contain"></param>
        /// <returns></returns>
        internal static Expression<Func<Department, bool>> PathContain(string contain)
        {
            return d => d.DepartmentPath.ToLower().Contains(contain.ToLower());
        }
    }
}
