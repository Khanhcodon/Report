using System.Collections.Generic;
using System.Linq;

namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : FormExtension - public - BLL
    /// Access Modifiers: 
    /// Create Date : 201212
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Các hàm mở rộng cho Workflow</para>
    /// (CuongNT@bkav.com - 201212)
    /// </summary>
    public static class WorkflowExtension
    {
        /// <summary>
        /// Trả về true nếu không gian cán bộ có chứa cán bộ cần kiểm tra
        /// </summary>
        /// <param name="queries"> </param>
        /// <param name="userId"> </param>
        /// <param name="userDeptPos"> </param>
        /// <param name="depIdExt"> </param>
        /// <returns></returns>
        public static bool ContainUser(this List<QueryBase> queries, int userId, IEnumerable<UserDepartmentPosition> userDeptPos, out List<string> depIdExt)
        {
            var userDeptPosition = userDeptPos.Where(udp => udp.UserId == userId).ToList();
            // Nếu không gian cán bộ rỗng + cán bộ không tồn tại trên hệ thống
            if (!queries.Any() || userDeptPosition.All(c => c.UserId != userId))
            {
                depIdExt = new List<string>();
                return false;
            }

            // Không gian theo Cán bộ "userid"
            var userQueries = queries.Where(c => c.Type == QueryType.TheoCanBo).Select(o => (UserQuery)o).ToList();
            if (userQueries.ContainUser(userId, userDeptPosition, out depIdExt))
            {
                return true;
            }

            // Không gian theo cấp "level"
            var levelQueries = queries.Where(c => c.Type == QueryType.TheoCap).Select(o => (LevelQuery)o).ToList();
            if (levelQueries.ContainUser(userId, userDeptPosition, out depIdExt))
            {
                return true;
            }

            // Không gian theo vị trí "pos"
            var positionQueries = queries.Where(c => c.Type == QueryType.TheoViTri).Select(o => (PositionQuery)o).ToList();
            if (positionQueries.ContainUser(userId, userDeptPosition, out depIdExt))
            {
                return true;
            }

            depIdExt = new List<string>();
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queries"></param>
        /// <param name="userId"></param>
        /// <param name="userDeptPos"></param>
        /// <param name="depIdExt"></param>
        /// <returns></returns>
        public static bool ContainUser(this List<UserQuery> queries, int userId, IEnumerable<UserDepartmentPosition> userDeptPos, out List<string> depIdExt)
        {
            var userDeptPosition = userDeptPos.Where(udp => udp.UserId == userId).ToList();
            // Nếu không gian cán bộ rỗng + cán bộ không tồn tại trên hệ thống
            if (!queries.Any() || userDeptPosition.All(c => c.UserId != userId))
            {
                depIdExt = new List<string>();
                return false;
            }

            if (queries.Any(c => c.UserId == userId))
            {
                depIdExt = userDeptPosition.Where(c => c.UserId == userId).Select(c => c.DepartmentIdExt).ToList();
                return true;
            }
            depIdExt = new List<string>();
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queries"></param>
        /// <param name="userId"></param>
        /// <param name="userDeptPos"></param>
        /// <param name="depIdExt"></param>
        /// <returns></returns>
        public static bool ContainUser(this List<PositionQuery> queries, int userId, IEnumerable<UserDepartmentPosition> userDeptPos, out List<string> depIdExt)
        {
            var userDeptPositions = userDeptPos.Where(udp => udp.UserId == userId).ToList();
            depIdExt = new List<string>();
            // Nếu không gian cán bộ rỗng + cán bộ không tồn tại trên hệ thống
            if (!queries.Any() || userDeptPositions.All(c => c.UserId != userId))
            {
                return false;
            }

            foreach (var u in userDeptPositions)
            {
                // Không gian theo vị trí "pos" - đơn vị hiện tại.
                var currentPos = queries.Where(c => c.Position == PositionType.DonViHienTai).Select(o => o).ToList();
                if (currentPos.Any() && (currentPos.Any(c => c.DepId == u.DepartmentId && (c.PositionId == -1 || c.PositionId == u.PositionId))))
                {
                    depIdExt.Add(u.DepartmentIdExt);
                }

                // Không gian theo vị trí "pos" - đơn vị cấp dưới.
                var parentDepId = GetParentId(u.DepartmentIdExt);
                var subPos = queries.Where(c => c.Position == PositionType.DonViCapDuoi).Select(o => o).ToList();
                if (subPos.Any() && (subPos.Any(c => c.DepId == parentDepId && (c.PositionId == -1 || c.PositionId == u.PositionId))))
                {
                    depIdExt.Add(u.DepartmentIdExt);
                }

                // Không gian theo vị trí "pos" - đơn vị trực thuộc.
                // TODO: Xem lại chỗ này. Cần loại bỏ phòng ban hiện tại đi chứ nhỉ?
                var listParentId = u.DepartmentIdExt.Split('.').Select(int.Parse).ToList();

                var innerPos = queries.Where(c => c.Position == PositionType.DonViTrucThuoc).Select(o => o).ToList();
                foreach (var childDepId in listParentId)
                {
                    if (innerPos.Any() && (innerPos.Any(c => c.DepId == childDepId && (c.PositionId == -1 || c.PositionId == u.PositionId))))
                    {
                        depIdExt.Add(u.DepartmentIdExt);
                    }
                }
            }

            return depIdExt.Any();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queries"></param>
        /// <param name="userId"></param>
        /// <param name="userDeptPos"></param>
        /// <param name="depIdExt"></param>
        /// <returns></returns>
        public static bool ContainUser(this List<LevelQuery> queries, int userId, IEnumerable<UserDepartmentPosition> userDeptPos, out List<string> depIdExt)
        {
            var userDeptPositions = userDeptPos.Where(udp => udp.UserId == userId).ToList();
            // Nếu không gian cán bộ rỗng + cán bộ không tồn tại trên hệ thống
            if (!queries.Any() || userDeptPositions.All(c => c.UserId != userId))
            {
                depIdExt = new List<string>();
                return false;
            }

            foreach (var u in userDeptPositions)
            {
                var level = GetLevel(u.DepartmentIdExt);
                if (queries.Any(c => c.Level == level && (c.PositionId == -1 || c.PositionId == u.PositionId)))
                {
                    depIdExt = new List<string>() { u.DepartmentIdExt };
                    return true;
                }
            }

            depIdExt = new List<string>();
            return false;
        }

        /// <summary>
        /// Lấy Id phòng ban từ Id mở rộng
        /// </summary>
        /// <param name="departmentIdExt"></param>
        /// <returns></returns>
        public static int GetDepIdFromExt(string departmentIdExt)
        {
            if (!departmentIdExt.Contains("."))
                return int.Parse(departmentIdExt);
            var listIds = departmentIdExt.Split('.');
            return int.Parse(listIds[listIds.Length - 1]);
        }

        /// <summary>
        /// Lấy phòng ban cha của phòng ban 
        /// </summary>
        /// <param name="departmentIdExt">Id mở rộng của phòng ban</param>
        /// <returns>Id phòng ban cấp cha</returns>
        private static int? GetParentId(string departmentIdExt)
        {
            if (!departmentIdExt.Contains("."))
                return null;
            var temp = departmentIdExt.Substring(0, departmentIdExt.LastIndexOf('.'));
            return int.Parse(!departmentIdExt.Contains(".")
                ? temp
                : temp.Substring(temp.LastIndexOf('.') + 1, temp.Length - temp.LastIndexOf('.') - 1));
        }

        /// <summary>
        /// Lấy cấp độ của phòng ban 
        /// </summary>
        /// <param name="depIdExt">Id phòng ban mở rộng: dạng 2.34.45</param>
        /// <returns></returns>
        private static int GetLevel(string depIdExt)
        {
            return depIdExt.ToCharArray().Count(t => t == '.') + 1;
        }
    }
}
