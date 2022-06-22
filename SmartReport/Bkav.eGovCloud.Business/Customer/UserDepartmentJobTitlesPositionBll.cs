using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Business;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : InfringeListBll - public - BLL</para>
    /// <para>Access Modifiers:</para>
    /// <para>Create Date : 010516</para>
    /// <para>Author      : DungNVL</para>
    /// <para>Description : BLL tương ứng với bảng CheckInfringe trong CSDL</para>
    /// </summary>
    public class UserDepartmentJobTitlesPositionBll : ServiceBase
    {
        private readonly IRepository<UserDepartmentJobTitlesPosition> _userdepartRepository;
        private IDbContext _context;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="context"></param>
        public UserDepartmentJobTitlesPositionBll(IDbCustomerContext context) : base(context)
        {
            _context = context;
            _userdepartRepository = _context.GetRepository<UserDepartmentJobTitlesPosition>();
        }

        /// <summary>
        /// Hàm lấy giữ liệu chỉ để đọc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDepartmentJobTitlesPosition> Gets()
        {
            return _userdepartRepository.Gets(true);
        }

        /// <summary>
        /// Trả về Tiêu chí có ID bằng với giá trị đầu vào
        /// </summary>
        /// <returns></returns>
        public UserDepartmentJobTitlesPosition Get(int id)
        {
            return _userdepartRepository.Get(id);
        }

        /// <summary>
        /// Trả về Tiêu chí có userID bằng với giá trị đầu vào
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDepartmentJobTitlesPosition> GetbyUserId(int userId)
        {
            return _userdepartRepository.Gets(true, d => d.UserId == userId);
        }

        private PositionCached GetPosition(List<PositionCached> listPosition, int id)
        {
            foreach (var item in listPosition)
            {
                if (item.PositionId == id)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="positions"></param>
        /// <returns></returns>
        /// <remarks>
        /// - Đầu tiên, lấy ra danh sách tất cả phòng ban của cán bộ hiện tại.
        /// - Sau đó lấy danh sách các chức vụ của người dùng hiện tại trong mỗi phòng ban (chổ này có thể chỉ cần xử lý phòng ban chính)
        /// - Lấy ra các position có mức thấp hơn chức vụ trên.
        /// - Kiểm tra trong phòng ban chính lấy ra những người có position thuộc danh sách thấp hơn ở trên.
        /// - Return userId
        /// </remarks>
        public IEnumerable<UserDepartmentJobTitlesPosition> GetUserPersonnelDown(int userId, List<PositionCached> positions)
        {
            var searchqry = GetbyUserId(userId).ToList();
            var userDepartmentJob = new List<UserDepartmentJobTitlesPosition>();
            
            foreach (var item in searchqry)
            {
                var currentDept  = item;
                userDepartmentJob.Add(currentDept);
            }

            var userDepts = _userdepartRepository.Gets(true);

            return userDepts.Where(x =>
                        userDepartmentJob.Any(m => (x.DepartmentIdExt.Contains(m.DepartmentIdExt) || x.DepartmentIdExt.StartsWith(m.DepartmentIdExt + "."))
                        && GetPosition(positions, x.PositionId).PriorityLevel > GetPosition(positions, m.PositionId).PriorityLevel && m.IsPrimary == true)
            );
        }

        public void Create(IList<UserDepartmentJobTitlesPosition> users)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var user in users)
            {
                _userdepartRepository.Create(user);
            }
            Context.Configuration.AutoDetectChangesEnabled = true;
            Context.SaveChanges();
        }
    }
}
