using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// KeyMapMySql
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class SymmetricCryptoKeyMapMySql : EntityTypeConfiguration<SymmetricCryptoKey>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public SymmetricCryptoKeyMapMySql()
        {
            ToTable("symmetriccryptoKey");
            HasKey(c => c.Id);
            Property(c => c.Bucket).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            Property(c => c.Handle).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            Property(c => c.Secret).IsRequired().HasMaxLength(255).HasColumnType("binary");
            Property(c => c.ExpiresUtc).HasColumnType("datetime").IsRequired();
        }
    }

    /// <summary>
    /// KeyMapSqlServer
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class SymmetricCryptoKeyMapSqlServer : EntityTypeConfiguration<SymmetricCryptoKey>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public SymmetricCryptoKeyMapSqlServer()
        {
            ToTable("symmetriccryptoKey");
            HasKey(c => c.Id);
            Property(c => c.Bucket).HasMaxLength(100).IsRequired();
            Property(c => c.Handle).HasMaxLength(100).IsRequired();
            Property(c => c.Secret).IsRequired().HasMaxLength(255).HasColumnType("binary");
            Property(c => c.ExpiresUtc).IsRequired();//.HasColumnType("datetime");
        }
    }
}
