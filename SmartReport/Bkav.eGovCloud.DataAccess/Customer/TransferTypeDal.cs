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
    /// Class : TransferTypeDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : ITransferTypeDal
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : DAL tương ứng với bảng TransferType trong CSDL
    /// </summary>
    public class TransferTypeDal : DataAccessBase, ITransferTypeDal
    {
        private readonly IRepository<TransferType> _transfertypeRepository;
        /// <summary>
        /// Khởi tạo class <see cref="TransferTypeDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public TransferTypeDal(IDbCustomerContext context)
            : base(context)
        {
            _transfertypeRepository = Context.GetRepository<TransferType>();
        }

#pragma warning disable 1591
        public IEnumerable<TransferType> Gets(Expression<Func<TransferType, bool>> spec = null,
                                            Func<IQueryable<TransferType>, IQueryable<TransferType>> preFilter = null,
                                            params Func<IQueryable<TransferType>, IQueryable<TransferType>>[] postFilters)
        {
            return _transfertypeRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<TransferType, TOutput>> projector, Expression<Func<TransferType, bool>> spec = null, Func<IQueryable<TransferType>, IQueryable<TransferType>> preFilter = null, params Func<IQueryable<TransferType>, IQueryable<TransferType>>[] postFilters)
        {
            return _transfertypeRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public TransferType Get(int id)
        {
            return _transfertypeRepository.One(a => a.TransferTypeId == id);
        }

        public void Create(TransferType transferType)
        {
            _transfertypeRepository.Create(transferType);
        }

        public void Update(TransferType transferType)
        {
            _transfertypeRepository.Update(transferType);
        }

        public void Delete(TransferType transferType)
        {
            _transfertypeRepository.Delete(transferType);
        }

        public bool Exist(Expression<Func<TransferType, bool>> spec)
        {
            return _transfertypeRepository.Any(spec);
        }

        public int Count(Expression<Func<TransferType, bool>> spec = null)
        {
            return _transfertypeRepository.Count(spec);
        }
    }
}
