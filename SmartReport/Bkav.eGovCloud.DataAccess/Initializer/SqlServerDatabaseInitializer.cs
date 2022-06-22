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
    /// Class : SqlServerDatabaseInitializer - public - DAL
    /// Access Modifiers:
    ///     Inherit: IDatabaseInitializer&lt;TContext&gt;
    /// Create Date : 260912
    /// Author      : TrungVH
    /// Description : Class khởi tạo database cho sql server
    /// </summary>
    public class SqlServerDatabaseInitializer : IDatabaseInitializer<EfContext>
    {
        private readonly IEnumerable<string> _customCommands;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="customCommands">Các câu lệnh Sql sẽ chạy sau khi tạo database xong (VD như: Tạo Index)</param>
        public SqlServerDatabaseInitializer(IEnumerable<string> customCommands)
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
                    var numberOfTables = context.Database
                                        .SqlQuery<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE'")
                                        .FirstOrDefault();

                    if(numberOfTables == 0)
                    {
                        var dbCreationScript = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();
                        context.Database.ExecuteSqlCommand(dbCreationScript);

                        Seed(context);
                        context.SaveChanges();
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


            var roleRepository = context.GetRepository<Role>();
            foreach (var role in SystemRole.EgovSystemRoles)
            {
                roleRepository.Create(role);
            }

            var permissionRepository = context.GetRepository<Permission>();
            foreach (var permission in SystemPermission.EgovSystemPermissions)
            {
                permissionRepository.Create(permission);
            }
            context.SaveChanges();
        }
    }
}
