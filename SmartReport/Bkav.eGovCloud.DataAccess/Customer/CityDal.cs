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
    /// Class : CityDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : ICityDal
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng City trong CSDL
    /// </summary>
    public class CityDal : DataAccessBase, ICityDal
    {
        private readonly IRepository<City> _cityRepository;
        /// <summary>
        /// Khởi tạo class <see cref="CodeDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public CityDal(IDbCustomerContext context)
            : base(context)
        {
            _cityRepository = Context.GetRepository<City>();
        }

#pragma warning disable 1591
        public IEnumerable<City> Gets(Expression<Func<City, bool>> spec = null,
                                            Func<IQueryable<City>, IQueryable<City>> preFilter = null,
                                            params Func<IQueryable<City>, IQueryable<City>>[] postFilters)
        {
            return _cityRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<City, TOutput>> projector, Expression<Func<City, bool>> spec = null, Func<IQueryable<City>, IQueryable<City>> preFilter = null, params Func<IQueryable<City>, IQueryable<City>>[] postFilters)
        {
            return _cityRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public City Get(int id)
        {
            return _cityRepository.One(a => a.CityId == id);
        }

        public void Create(City city)
        {
            _cityRepository.Create(city);
        }

        public void Update(City city)
        {
            _cityRepository.Update(city);
        }

        public void Delete(City city)
        {
            _cityRepository.Delete(city);
        }

        public bool Exist(Expression<Func<City, bool>> spec)
        {
            return _cityRepository.Any(spec);
        }

        public int Count(Expression<Func<City, bool>> spec = null)
        {
            return _cityRepository.Count(spec);
        }
    }
}
