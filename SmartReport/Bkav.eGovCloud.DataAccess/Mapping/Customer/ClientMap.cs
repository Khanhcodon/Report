using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// ClientMapMySql
    /// </summary>
    [ComVisible(false)]
    public class ClientMapMySql : EntityTypeConfiguration<Client>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public ClientMapMySql()
        {
            ToTable("client");
            HasKey(c => c.Id);
            Property(c => c.Identifier).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.Secret).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.Name).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.Domain).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.Ip).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.IsActivated).HasColumnType("bit").IsRequired();
        }
    }

    /// <summary>
    /// ClientMapSqlServer
    /// </summary>
    [ComVisible(false)]
    public class ClientMapSqlServer : EntityTypeConfiguration<Client>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public ClientMapSqlServer()
        {
            ToTable("client");
            HasKey(c => c.Id);
            Property(c => c.Identifier).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.Secret).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.Name).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.Domain).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.Ip).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(c => c.IsActivated).IsRequired();//.HasColumnType("bit");
        }
    }

    
}
