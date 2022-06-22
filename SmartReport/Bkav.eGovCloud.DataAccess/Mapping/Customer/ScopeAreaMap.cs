using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// ScopeAreaMySql
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class ScopeAreaMySql : EntityTypeConfiguration<ScopeArea>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public ScopeAreaMySql()
        {
            ToTable("scopearea");
            HasKey(c => c.Id);
            Property(c => c.Key).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.Name).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.Description).HasColumnType("text");
            Property(c => c.Scopes).HasColumnType("text");
        }
    }

    /// <summary>
    /// ScopeAreaSqlServer
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class ScopeAreaSqlServer : EntityTypeConfiguration<ScopeArea>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public ScopeAreaSqlServer()
        {
            ToTable("scopearea");
            HasKey(c => c.Id);
            Property(c => c.Key).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.Name).HasMaxLength(300).IsRequired();
            //Property(c => c.Description).HasColumnType("ntext");
            //Property(c => c.Scopes).HasColumnType("ntext");
        }
    }

    /// <summary>
    /// ClientScopeMySql
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class ClientScopeMySql : EntityTypeConfiguration<ClientScope>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public ClientScopeMySql()
        {
            ToTable("clientscope");
            HasKey(c => c.Id);
            Property(c => c.ClientId).IsRequired();
            Property(c => c.ScopeAreaId).IsRequired();
        }
    }

    /// <summary>
    /// ClientScopeSqlServer
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class ClientScopeSqlServer : EntityTypeConfiguration<ClientScope>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public ClientScopeSqlServer()
        {
            ToTable("clientscope");
            HasKey(c => c.Id);
            Property(c => c.ClientId).IsRequired();
            Property(c => c.ScopeAreaId).IsRequired();
        }
    }
}
