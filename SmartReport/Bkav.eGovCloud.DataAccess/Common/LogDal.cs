using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Common;
using System.Data.SqlClient;
using System.Configuration;

namespace Bkav.eGovCloud.DataAccess.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LogDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : ILogDal
    /// Create Date : 270812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Log trong CSDL
    /// </summary>
    public class LogDal : DataAccessBase, ILogDal
    {
        private readonly IRepository<Log> _logRepository;

        /// <summary>
        /// Khởi tạo class <see cref="LogDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public LogDal(IDbAdminContext context) 
            : base(context)
        {
            _logRepository = Context.GetRepository<Log>();
 
        }

        /// <summary>
        /// Khởi tạo class <see cref="LogDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public LogDal(IDbCustomerContext context)
            : base(context)
        {
            _logRepository = Context.GetRepository<Log>();
        }

        #pragma warning disable 1591

        public IEnumerable<Log> Gets(Expression<Func<Log, bool>> spec = null, Func<IQueryable<Log>, IQueryable<Log>> preFilter = null, params Func<IQueryable<Log>, IQueryable<Log>>[] postFilters)
        {
            return _logRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Log, TOutput>> projector, Expression<Func<Log, bool>> spec = null, Func<IQueryable<Log>, IQueryable<Log>> preFilter = null, params Func<IQueryable<Log>, IQueryable<Log>>[] postFilters)
        {
            return _logRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public Log Get(int id)
        {
            return _logRepository.One(l => l.LogId == id);
        }

        public void Create(Log log)
        {
            _logRepository.Create(log);
        }

        public void Delete(Log log)
        {
            _logRepository.Delete(log);
        }

        public void Delete(IEnumerable<Log> logs)
        {
            foreach (var log in logs)
            {
                _logRepository.Delete(log, false);
            }
            Context.SaveChanges();
        }

        public void DeleteAll()
        {
            Context.RawModify("TRUNCATE TABLE log");
        }
        

        public int Count(Expression<Func<Log, bool>> spec = null)
        {
            return _logRepository.Count(spec);
        }
    }
}
