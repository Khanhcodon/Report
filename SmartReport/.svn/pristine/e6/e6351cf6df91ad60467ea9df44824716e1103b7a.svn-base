using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess.Customer;
using Bkav.eGovCloud.Entities.Admin;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using MySql.Data.MySqlClient;
//using Oracle.DataAccess.Client;
using StackExchange.Profiling;
using DatabaseType = Bkav.eGovCloud.Entities.DatabaseType;

namespace Bkav.eGovCloud.DataAccess.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DomainDal - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : DataAccessBase
    ///     * Implement : IDomainDal
    /// Create Date : 010812
    /// Author      : TrungVH
    /// Description : DAL tương ứng với bảng Domain trong CSDL
    /// </summary>
    public class DomainDal : DataAccessBase, IDomainDal
    {
        private readonly IRepository<Domain> _domainRepository;

        /// <summary>
        /// Khởi tạo class <see cref="DomainDal"/>.
        /// </summary>
        /// <param name="context">Admin context</param>
        public DomainDal(IDbAdminContext context = null)
            : base(context ?? new EfAdminContext())
        {
            _domainRepository = Context.GetRepository<Domain>();
        }

        #pragma warning disable 1591

        public IEnumerable<Domain> Gets(Expression<Func<Domain, bool>> spec = null)
        {
            return _domainRepository.Find(spec);
        }

        public IEnumerable<TOutput> GetsAs<TOutput>(Expression<Func<Domain, TOutput>> projector, Expression<Func<Domain, bool>> spec = null, Func<IQueryable<Domain>, IQueryable<Domain>> preFilter = null, params Func<IQueryable<Domain>, IQueryable<Domain>>[] postFilters)
        {
            return _domainRepository.FindAs(projector, spec, preFilter, postFilters);
        }

        public IEnumerable<TOutput> Gets<TOutput>(Expression<Func<Domain, TOutput>> projector, Expression<Func<Domain, bool>> spec)
        {
            return _domainRepository.FindAs(projector, spec);
        }

        public Domain Get(int id)
        {
            return _domainRepository.One(d => d.DomainId == id);
        }

        public void Create(Domain domain, User user, DbConnection connection)
        {
            _domainRepository.Create(domain);
                
            
        }

        public void Update(Domain domain)
        {
            _domainRepository.Update(domain);
        }

        public bool Exist(Expression<Func<Domain, bool>> spec)
        {
            return _domainRepository.Any(spec);
        }

        public DbConnection TestConnection(Connection connection)
        {
            DbConnection dbConnection = null;
            string connectionString = string.Empty;
            if(!string.IsNullOrWhiteSpace(connection.ConnectionRaw))
            {
                connectionString = connection.ConnectionRaw;
            }
            else
            {
                switch (connection.DatabaseTypeIdInEnum)
                {
                    case DatabaseType.MySql:
                        connectionString = ConnectionUtil
                            .GenerateMySqlConnectionString(connection.Server,
                                                           connection.Database,
                                                           connection.Username,
                                                           connection.Password,
                                                           connection.Port.HasValue
                                                               ? uint.Parse(
                                                                   connection.Port.Value.
                                                                       ToString(CultureInfo.InvariantCulture))
                                                               : (uint?)null);
                        break;
                    case DatabaseType.SqlServer:
                        connectionString = ConnectionUtil
                            .GenerateSqlConnectionString(connection.Server,
                                                         connection.Database,
                                                         connection.Username,
                                                         connection.Password);
                        break;
                    case DatabaseType.Oracle:
                        //TODO: Them connection string cho oracle
                        break;
                }
            }

            switch (connection.DatabaseTypeIdInEnum)
            {
                case DatabaseType.MySql:
                    dbConnection = new MySqlConnection(connectionString);
                    break;
                case DatabaseType.SqlServer:
                    dbConnection = new SqlConnection(connectionString);
                    break;
                //default:
                    //dbConnection = new OracleConnection(connectionString);
                    //break;
            }
            try
            {
                dbConnection.Open();
                dbConnection.Close();
            }
            catch (Exception)
            {
                dbConnection = null;
            }

            return dbConnection;
        }
    }
}
