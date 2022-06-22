using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

using StackExchange.Profiling;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : EfRepository - public - DAL
    /// Access Modifiers: 
    ///     *Implement: IRepository
    /// Create Date : 200612
    /// Author      : TrungVH
    /// Description : Repository của 1 kiểu thực thể (1 bảng trong db)
    /// </summary>
    /// <typeparam name="T">Kiểu thực thể</typeparam>
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        /// <summary>
        /// Khởi tạo <see cref="EfRepository&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="context">Context (IDbContext).</param>
        public EfRepository(IDbContext context)
        {
            _context = context;
        }

        private IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }

#pragma warning disable 1591

        public IQueryable<T> Raw
        {
            get { return Entities; }
        }

        public IEnumerable<T> Gets(bool isReadOnly,
                                    Expression<Func<T, bool>> spec = null,
                                    Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                                    params Func<IQueryable<T>, IQueryable<T>>[] postFilters)
        {
            using (MiniProfiler.Current.Step("Sql query: Gets " + (isReadOnly ? "readonly" : "")))
            {
                return FindCore(isReadOnly, spec, preFilter, postFilters).ToList();
            }
        }

        public IEnumerable<T> GetsReadOnly(Expression<Func<T, bool>> spec = null,
                                            Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                                            params Func<IQueryable<T>, IQueryable<T>>[] postFilter)
        {
            return Gets(true, spec, preFilter, postFilter);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<T, TOutput>> projector,
                                                    Expression<Func<T, bool>> spec = null,
                                                    Func<IQueryable<T>, IQueryable<T>> preFilter = null,
                                                    params Func<IQueryable<T>, IQueryable<T>>[] postFilters)
        {
            if (projector == null)
            {
                throw new ArgumentNullException("projector");
            }
            using (MiniProfiler.Current.Step("Sql query: FindAs"))
            {
                return FindCore(true, spec, preFilter, postFilters).Select(projector).ToList();
            }
        }

        public T Get(params object[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                throw new ArgumentException("no id specified");
            }

            using (MiniProfiler.Current.Step("Sql query: One(object[])"))
            {
                return Entities.Find(ids);
            }
        }

        public T Get(bool isReadOnly, Expression<Func<T, bool>> spec)
        {
            if (spec == null)
            {
                throw new ArgumentNullException("spec");
            }

            using (MiniProfiler.Current.Step("Sql query: One(spec) " + (isReadOnly ? "readonly" : "")))
            {
                return isReadOnly ? Entities.AsNoTracking().SingleOrDefault(spec) : Entities.SingleOrDefault(spec);
            }
        }

        public T GetReadOnly(Expression<Func<T, bool>> spec)
        {
            return Get(true, spec);
        }

        public TOutput GetAs<TOutput>(Expression<Func<T, TOutput>> projector,
                                        Expression<Func<T, bool>> spec)
        {
            if (projector == null)
            {
                throw new ArgumentNullException("projector");
            }

            if (spec == null)
            {
                throw new ArgumentNullException("spec");
            }

            using (MiniProfiler.Current.Step("Sql query: OneAs"))
            {
                var result = Entities.AsNoTracking().Where(spec).Select(projector).SingleOrDefault();
                return result;
            }
        }

        public bool Exist(Expression<Func<T, bool>> spec = null)
        {
            using (MiniProfiler.Current.Step("Sql query: Any"))
            {
                return spec == null ? Entities.Count() > 0 : Entities.Count(spec) > 0;

                // TienBV: sử dụng Count > 0 hiệu năng cao hơn Any
                // Đã test lại câu SQL được sinh ra ở cả 2 trường hợp.
                // Xem chi tiết: http://stackoverflow.com/questions/305092/which-method-performs-better-any-vs-count-0
                // return spec == null ? Entities.Any() : Entities.Any(spec);
            }
        }

        public int Count(Expression<Func<T, bool>> spec = null)
        {
            using (MiniProfiler.Current.Step("Sql query: Count"))
            {
                return spec == null ? Entities.Count() : Entities.Count(spec);
            }
        }

        public void Create(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                using (MiniProfiler.Current.Step("Sql command: Create"))
                {
                    Entities.Add(entity);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                using (MiniProfiler.Current.Step("Sql command: Delete"))
                {
                    _context.Set<T>().Attach(entity);
                    Entities.Remove(entity);
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        private IQueryable<T> FindCore(bool isReadOnly, Expression<Func<T, bool>> spec,
            Func<IQueryable<T>, IQueryable<T>> preFilter, params Func<IQueryable<T>, IQueryable<T>>[] postFilters)
        {
            var entities = isReadOnly ? Entities.AsNoTracking() : Entities;
            var result = preFilter != null ? preFilter(entities) : entities;
            if (spec != null)
            {
                result = result.Where(spec);
            }
            foreach (var postFilter in postFilters)
            {
                if (postFilter != null)
                {
                    result = postFilter(result);
                }
            }
            return result;
        }

    }
}