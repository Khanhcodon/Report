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
    /// Class : PositionDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IPositionDal
    /// Create Date : 270812
    /// Author      : TrungVH
    /// Modify Date: 050912
    /// Modifier: GiangPN
    /// Description : DAL tương ứng với bảng Position trong CSDL
    /// </summary>
    public class PositionDal : DataAccessBase, IPositionDal
    {
        private readonly IRepository<Position> _positionRepository;
        /// <summary>
        /// Khởi tạo class <see cref="PositionDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public PositionDal(IDbCustomerContext context)
            : base(context)
        {
            _positionRepository = Context.GetRepository<Position>();
        }

        #pragma warning disable 1591

        public IEnumerable<Position> Gets(Expression<Func<Position, bool>> spec = null, Func<IQueryable<Position>, IQueryable<Position>> preFilter = null, params Func<IQueryable<Position>, IQueryable<Position>>[] postFilters)
        {
            return _positionRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Position, TOutput>> projector, Expression<Func<Position, bool>> spec = null)
        {
            return _positionRepository.FindAs(projector, spec);
        }

        public Position Get(int id)
        {
            return _positionRepository.One(l => l.PositionId == id);
        }

        public void Create(Position position)
        {
            _positionRepository.Create(position);
        }

        public void Update(Position position)
        {
            _positionRepository.Update(position);
        }

        public void Delete(Position position)
        {
            _positionRepository.Delete(position);
        }

        public void Delete(IEnumerable<Position> positions)
        {
            foreach (var position in positions)
            {
                _positionRepository.Delete(position, false);
            }
            Context.SaveChanges();
        }
        public bool Exist(Expression<Func<Position, bool>> spec)
        {
            return _positionRepository.Any(spec);
        }
        public int Count(Expression<Func<Position, bool>> spec = null)
        {
            return _positionRepository.Count(spec);
        }
    }
}
