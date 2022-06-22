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
    /// Class : FormGroupDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IIncreaseDal
    /// Create Date : 40912
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng FormGroup trong CSDL
    /// </summary>
    public class FormGroupDal : DataAccessBase, IFormGroupDal
    {
        private readonly IRepository<FormGroup> _formgroupRepository;
        /// <summary>
        /// Khởi tạo class <see cref="FormGroupDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public FormGroupDal(IDbCustomerContext context)
            : base(context)
        {
            _formgroupRepository = Context.GetRepository<FormGroup>();
        }

#pragma warning disable 1591
        public IEnumerable<FormGroup> Gets(Expression<Func<FormGroup, bool>> spec = null, Func<IQueryable<FormGroup>, IQueryable<FormGroup>> preFilter = null, params Func<IQueryable<FormGroup>, IQueryable<FormGroup>>[] postFilters)
        {
            return _formgroupRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<T> GetsAs<T>(Expression<Func<FormGroup, T>> projector, Expression<Func<FormGroup, bool>> spec = null)
        {
            return _formgroupRepository.FindAs(projector, spec);
        }

        public FormGroup Get(int id)
        {
            return _formgroupRepository.One(a => a.FormGroupId == id);
        }

        public void Create(FormGroup formGroup)
        {
            _formgroupRepository.Create(formGroup);
        }

        public void Delete(FormGroup formGroup)
        {
            _formgroupRepository.Delete(formGroup);
        }

        public bool Exist(Expression<Func<FormGroup, bool>> spec)
        {
            return _formgroupRepository.Any(spec);
        }

        public int Count(Expression<Func<FormGroup, bool>> spec = null)
        {
            return _formgroupRepository.Count(spec);
        }

        public void Update(FormGroup formGroup)
        {
            _formgroupRepository.Update(formGroup);
        }
    }
}
