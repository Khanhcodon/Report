using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionTypeDal - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>  * Inherit : DataAccessBase </para>
    /// <para>  * Implement : IProcessFunctionTypeDal </para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : DAL tương ứng với bảng ProcessFunctionType trong CSDL</para>
    /// </summary>
    public class ProcessFunctionTypeDal: DataAccessBase, IProcessFunctionTypeDal
    {
        private readonly IRepository<ProcessFunctionType> _functionTypeRepository;

        /// <summary>
        /// Khởi tạo class <see cref="ProcessFunctionTypeDal"/>.
        /// </summary>
        /// <param name="context">Customer context</param>
        public ProcessFunctionTypeDal(IDbCustomerContext context)
            : base(context)
        {
            _functionTypeRepository = Context.GetRepository<ProcessFunctionType>();
        }

        #pragma warning disable 1591

        public IEnumerable<ProcessFunctionType> Gets(Expression<Func<ProcessFunctionType, bool>> spec = null, Func<IQueryable<ProcessFunctionType>, IQueryable<ProcessFunctionType>> preFilter = null, params Func<IQueryable<ProcessFunctionType>, IQueryable<ProcessFunctionType>>[] postFilters)
        {
            return _functionTypeRepository.Find(spec, preFilter, postFilters);
        }

        public ProcessFunctionType Get(int id)
        {
            return _functionTypeRepository.One(id);
        }

        public void Create(ProcessFunctionType functiontype)
        {
            _functionTypeRepository.Create(functiontype);
        }

        public void Update(ProcessFunctionType functiontype)
        {
            _functionTypeRepository.Update(functiontype);
        }

        public void Delete(ProcessFunctionType functiontype)
        {
            _functionTypeRepository.Delete(functiontype);
        }

        public bool Exist(Expression<Func<ProcessFunctionType, bool>> spec)
        {
            return _functionTypeRepository.Any(spec);
        }

        public IEnumerable<IDictionary<string, object>> GetItemsOfType(ProcessFunctionType functionType, params object[] parameters)
        {
            return Context.RawQuery(functionType.Query, parameters) as IEnumerable<IDictionary<string, object>>;
        }
    }
}
