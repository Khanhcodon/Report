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
    /// Class : DistrictDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDistrictDal
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng District trong CSDL
    /// </summary>
    public class DistrictDal : DataAccessBase, IDistrictDal
    {
        private readonly IRepository<District> _districtRepository;
        /// <summary>
        /// Khởi tạo class <see cref="CodeDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public DistrictDal(IDbCustomerContext context)
            : base(context)
        {
            _districtRepository = Context.GetRepository<District>();
        }

#pragma warning disable 1591
        public IEnumerable<District> Gets(Expression<Func<District, bool>> spec = null,
                                            Func<IQueryable<District>, IQueryable<District>> preFilter = null,
                                            params Func<IQueryable<District>, IQueryable<District>>[] postFilters)
        {
            return _districtRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<District, TOutput>> projector, Expression<Func<District, bool>> spec = null, Func<IQueryable<District>, IQueryable<District>> preFilter = null, params Func<IQueryable<District>, IQueryable<District>>[] postFilters)
        {
            return _districtRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public District Get(int id)
        {
            return _districtRepository.One(a => a.DistrictId == id);
        }

        public void Create(District city)
        {
            _districtRepository.Create(city);
        }

        public void Update(District city)
        {
            _districtRepository.Update(city);
        }

        public void Delete(District city)
        {
            _districtRepository.Delete(city);
        }

        public bool Exist(Expression<Func<District, bool>> spec)
        {
            return _districtRepository.Any(spec);
        }

        public int Count(Expression<Func<District, bool>> spec = null)
        {
            return _districtRepository.Count(spec);
        }
    }
}
