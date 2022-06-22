using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Transactions;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : MySqlDatabaseInitializer - public - DAL
    /// Access Modifiers:
    ///     Inherit: IDatabaseInitializer&lt;TContext&gt;
    /// Create Date : 260912
    /// Author      : TrungVH
    /// Description : Class khởi tạo database cho mysql
    /// </summary>
    public class MySqlDatabaseInitializer : IDatabaseInitializer<EfContext>
    {
        private readonly IEnumerable<string> _customCommands;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="customCommands">Các câu lệnh Sql sẽ chạy sau khi tạo database xong (VD như: Tạo Index)</param>
        public MySqlDatabaseInitializer(IEnumerable<string> customCommands)
        {
            _customCommands = customCommands;
        }

        #pragma warning disable 1591
        public void InitializeDatabase(EfContext context)
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Suppress))
            {
                var exist = context.Database.Exists();
                if (exist)
                {
                    var query = string.Format(
                                "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE' AND TABLE_SCHEMA = '{0}'",
                                context.Database.Connection.Database);
                    var numberOfTables = context.Database
                                        .SqlQuery<int>(query)
                                        .FirstOrDefault();

                    if (numberOfTables == 0)
                    {
                        ((IObjectContextAdapter) context).ObjectContext.CommandTimeout = 600;
                        Seed(context);
                    }
                }
                else
                {
                    throw new Exception("Database not found!");
                }
                transaction.Complete();
            }
        }

        protected void Seed(EfContext context)
        {
            foreach (var command in _customCommands)
            {
                context.Database.ExecuteSqlCommand(command);
            }

            context.SaveChanges();
        }
    }
}