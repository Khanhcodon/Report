using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Class : AddressDal - public - DAL
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 180214
    /// <para></para> Author      : TrungVH
    /// <para></para> Description : DAL tương ứng với bảng Anticipate trong CSDL
    /// </summary>
    public class AnticipateDal : DataAccessBase, IAnticipateDal
    {
        #region private fields

        private readonly IRepository<Anticipate> _anticipateRepository;

        #endregion private fields

        #region c'tor

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
        public AnticipateDal(IDbCustomerContext context)
            : base(context)
        {
            _anticipateRepository = context.GetRepository<Anticipate>();
        }

        #endregion c'tor

#pragma warning disable 1591
        public void Create(Anticipate anticipate)
        {
            _anticipateRepository.Create(anticipate);
        }

        public void Delete(Anticipate anticipate)
        {
            _anticipateRepository.Delete(anticipate);
        }

        public void Update(Anticipate anticipate)
        {
            _anticipateRepository.Update(anticipate);
        }

        public Anticipate Get(int id)
        {
            return _anticipateRepository.One(id);
        }

        public Anticipate Get(Expression<Func<Anticipate, bool>> spec = null)
        {
            return _anticipateRepository.One(spec);
        }

        public IEnumerable<Anticipate> Gets(Expression<Func<Anticipate, bool>> spec = null, Func<IQueryable<Anticipate>, IQueryable<Anticipate>> preFilter = null, params Func<IQueryable<Anticipate>, IQueryable<Anticipate>>[] postFilters)
        {
            return _anticipateRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Anticipate, TOutput>> projector, Expression<Func<Anticipate, bool>> spec = null, Func<IQueryable<Anticipate>, IQueryable<Anticipate>> preFilter = null,
            params Func<IQueryable<Anticipate>, IQueryable<Anticipate>>[] postFilters)
        {
            return _anticipateRepository.FindAs(projector, spec, preFilter, postFilters);
        }
    }
}
