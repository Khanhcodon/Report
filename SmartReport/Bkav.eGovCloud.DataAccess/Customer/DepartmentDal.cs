using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DepartmentDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDepartmentDal
    /// Create Date : 270812
    /// Author      : TrungVH
    /// Modify Date: 080912
    /// Modifier: GiangPN
    /// Description : DAL tương ứng với bảng Department trong CSDL
    /// </summary>
    /// 
    public class DepartmentDal:DataAccessBase,IDepartmentDal
    {
        private readonly IRepository<Department> _departmentRepository;
        /// <summary>
        /// Khởi tạo class <see cref="DepartmentDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public DepartmentDal(IDbCustomerContext context)
            : base(context)
        {
            _departmentRepository = Context.GetRepository<Department>();
        }

        #pragma warning disable 1591

        public IEnumerable<Department> Gets(Expression<Func<Department, bool>> spec = null, Func<IQueryable<Department>, IQueryable<Department>> preFilter = null, params Func<IQueryable<Department>, IQueryable<Department>>[] postFilters)
        {
            return _departmentRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Department, TOutput>> projector, Expression<Func<Department, bool>> spec = null)
        {
            return _departmentRepository.FindAs(projector, spec);
        }

        public Department Get(int id)
        {
            return _departmentRepository.One(l => l.DepartmentId == id);
        }

        public Department GetRoot()
        {
            return _departmentRepository.One(l => l.ParentId == null);
        }

        public void Create(Department department)
        {
            _departmentRepository.Create(department);
        }

        public void Update(Department department)
        {
            _departmentRepository.Update(department);
        }

        public void Delete(Department department)
        {
            _departmentRepository.Delete(department);
        }

        public void Delete(IEnumerable<Department> departments)
        {
            foreach (var department in departments)
            {
                _departmentRepository.Delete(department, false);
            }
            Context.SaveChanges();
        }
        public bool Exist(Expression<Func<Department, bool>> spec)
        {
            return _departmentRepository.Any(spec);
        }
        public int Count(Expression<Func<Department, bool>> spec = null)
        {
            return _departmentRepository.Count(spec);
        }
    }
}
