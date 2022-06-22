using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class CriteriaBll
    {
        private readonly IRepository<RateEmployee> _criteriaRepository;
        private IDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CriteriaBll(IDbCustomerContext context)
        {
            _context = context;
            _criteriaRepository = _context.GetRepository<RateEmployee>();
        }

        /// <summary>
        /// Lấy tất cả các tiêu chí chỉ để đọc
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RateEmployee> GetReadOnlys()
        {
            return _criteriaRepository.Gets(true, null);
        }

        /// <summary>
        /// Cập nhật tiêu chí vào hệ thống
        /// </summary>
        /// <param name="entity"></param>
        public void Update(RateEmployee entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Xóa tiêu chí
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(RateEmployee entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _criteriaRepository.Delete(entity);
            _context.SaveChanges();
        }
        /// <summary>
        /// Them mới
        /// </summary>
        /// <param name="entity"></param>
        public void Create(RateEmployee entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _criteriaRepository.Create(entity);
            _context.SaveChanges();
        }
        /// <summary>
        /// Trả về Tiêu chí có ID bằng với giá trị đầu vào
        /// </summary>
        /// <returns></returns>
        public RateEmployee Get(int ID)
        {
            return _criteriaRepository.Get(false, x => x.RateEmployeeId == ID);
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="departmentid"></param>
       /// <returns></returns>
        public IEnumerable<RateEmployee> GetReadOnlysByDepartment(int departmentid)
        {
            return _criteriaRepository.Gets(true, c => c.DepartmentId == departmentid);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentIds"></param>
        /// <returns></returns>
        public IEnumerable<RateEmployee> GetReadOnlysByDepartment(List<int> departmentIds)
        {
            return _criteriaRepository.Gets(true, c => c.DepartmentId.HasValue && departmentIds.Contains(c.DepartmentId.Value));  
        }
    }
}
