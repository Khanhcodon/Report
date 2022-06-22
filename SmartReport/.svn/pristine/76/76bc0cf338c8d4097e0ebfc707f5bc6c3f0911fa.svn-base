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
    /// Class : DailyProcessDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDailyProcessDal
    /// Create Date : 241113
    /// Author      : TienBV
    /// Description : DAL tương ứng với bảng DailyProcess trong CSDL
    /// </summary>
    public class DailyProcessDal : DataAccessBase, IDailyProcessDal
    {
        private readonly IRepository<DailyProcess> _processRepository;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context">Customer context</param>
         public DailyProcessDal(IDbCustomerContext context)
             : base(context)
        {
            _processRepository = Context.GetRepository<DailyProcess>();
        }

        #pragma warning disable 1591

        public IEnumerable<DailyProcess> Gets(Expression<Func<DailyProcess, bool>> spec = null, Func<IQueryable<DailyProcess>, IQueryable<DailyProcess>> preFilter = null, params Func<IQueryable<DailyProcess>, IQueryable<DailyProcess>>[] postFilter)
        {
            return _processRepository.Find(spec, preFilter, postFilter);
        }

        public void Create(DailyProcess process)
        {
            _processRepository.Create(process);
        }

        public void Delete(IEnumerable<DailyProcess> processes)
        {
            foreach (var process in processes)
            {
                _processRepository.Delete(process);
            }
        }

        public bool Exist(Expression<Func<DailyProcess, bool>> expression)
        {
            return _processRepository.Any(expression);
        }
    }
}
