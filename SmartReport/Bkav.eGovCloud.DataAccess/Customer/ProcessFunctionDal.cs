using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionDal - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>  * Inherit : DataAccessBase </para>
    /// <para>  * Implement : IProcessFunctionDal </para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : DAL tương ứng với bảng ProcessFunction trong CSDL</para>
    /// </summary>
    public class ProcessFunctionDal: DataAccessBase, IProcessFunctionDal
    {
        private readonly IRepository<ProcessFunction> _functionRepository;

        /// <summary>
        /// Khởi tạo class <see cref="ProcessFunctionDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public ProcessFunctionDal(IDbCustomerContext context) : base(context)
        {
            _functionRepository = Context.GetRepository<ProcessFunction>();
        }

        #pragma warning disable 1591

        public IEnumerable<ProcessFunction> Gets(Expression<Func<ProcessFunction, bool>> spec = null, Func<IQueryable<ProcessFunction>, IQueryable<ProcessFunction>> preFilter = null, params Func<IQueryable<ProcessFunction>, IQueryable<ProcessFunction>>[] postFilters)
        {
            return _functionRepository.Find(spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<ProcessFunction, TOutput>> projector, Expression<Func<ProcessFunction, bool>> spec = null)
        {
            return _functionRepository.FindAs(projector, spec);
        }

        public ProcessFunction Get(int id)
        {
            return _functionRepository.One(id);
        }

        public void Create(ProcessFunction function)
        {
            _functionRepository.Create(function);
        }

        public void Update(ProcessFunction function)
        {
            _functionRepository.Update(function);
        }

        public void Delete(ProcessFunction function)
        {
            _functionRepository.Delete(function);
        }

        public void Delete(IEnumerable<ProcessFunction> functions)
        {
            foreach (var processFunction in functions)
            {
                _functionRepository.Delete(processFunction, false);
            }
            Context.SaveChanges();
        }

        public bool Exist(Expression<Func<ProcessFunction, bool>> spec)
        {
            return _functionRepository.Any(spec);
        }

        public int Count(Expression<Func<ProcessFunction, bool>> spec = null)
        {
            return _functionRepository.Count(spec);
        }

        public IEnumerable<IDictionary<string, object>> GetDocumentLatestByFunction(ProcessFunction function, params object[] parameters)
        {
            var result = Context.RawQuery(function.QueryLatest, parameters) as IEnumerable<IDictionary<string, object>>;
            return result;
        }

        public IEnumerable<int> GetDocumentCopyIdsRemove(ProcessFunction function, IEnumerable<int> currentDocumentCopyIds,
            params object[] parameters)
        {
            if (currentDocumentCopyIds != null && currentDocumentCopyIds.Any())
            {
                var result = Context.RawQuery(string.Format(function.QueryItemRemove, string.Join(",", currentDocumentCopyIds)), parameters) as IEnumerable<IDictionary<string, object>>;
                if (result.Any())
                {
                    if (!result.First().ContainsKey("DocumentCopyId"))
                    {
                        return new List<int>();
                    }
                    return result.Select(r => (int) r["DocumentCopyId"]);
                }
            }
            return new List<int>();
        }

        public IEnumerable<IDictionary<string, object>> GetDocumentOlderByFunction(ProcessFunction function, params object[] parameters)
        {
            var result = Context.RawQuery(function.QueryOlder, parameters) as IEnumerable<IDictionary<string, object>>;
            return result;
        }

        public IEnumerable<IDictionary<string, object>> GetDocumentPagingByFunction(ProcessFunction function, params object[] parameters)
        {
            var result = Context.RawQuery(function.QueryPaging, parameters) as IEnumerable<IDictionary<string, object>>;
            return result;
        }

        public int GetTotalDocumentUnread(ProcessFunction function, params object[] parameters)
        {
            if(!string.IsNullOrWhiteSpace(function.QueryCountItemUnread))
            {
                int total;
                int.TryParse((Context.RawScalar(function.QueryCountItemUnread, parameters)).ToString(), out total);
                return total;
            }
            return 0;
        }

        public int GetTotalDocument(ProcessFunction function, params object[] parameters)
        {
            if (!string.IsNullOrWhiteSpace(function.QueryCountAllItems))
            {
                int total;
                int.TryParse((Context.RawScalar(function.QueryCountAllItems, parameters)).ToString(), out total);
                return total;
            }
            return 0;
        }
    }
}