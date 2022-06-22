using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : DepartmentExtension - public - BLL
    /// Access Modifiers: 
    /// Create Date : 200912
    /// Author      : TrungVH
    /// </author>
    /// <summary> 
    /// <para>Các hàm mở rộng cho xử lý phòng ban</para>
    /// <para>(TrungVH@bkav.com - 200912)</para>
    /// </summary>
    public static class DepartmentExtension
    {
        /// <summary>
        /// Lấy ra tên phòng ban có thêm '|--' ở đầu (giả tree)
        /// </summary>
        /// <param name="department">Entity phòng ban</param>
        /// <param name="allDepartments">Danh sách tất cả các phòng ban</param>
        /// <returns></returns>
        public static string GetCategoryBreadCrumb(this Department department, IEnumerable<Department> allDepartments)
        {
            var result = string.Empty;

            while (department != null && department.IsActivated)
            {
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = department.DepartmentName;
                }
                else
                {
                    //if (result.Contains("|-- "))
                    //{
                    //    result = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + result;
                    //}
                    //else
                    //{
                    //    result = "|-- " + result;
                    //}
                    result = department.DepartmentName + "/" + result;
                }

                department = allDepartments.SingleOrDefault(d => d.DepartmentId == department.ParentId);
            }
            return result;
        }
    }
}
