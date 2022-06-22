using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// NonceMapMySql
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class NonceMapMySql : EntityTypeConfiguration<Nonce>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public NonceMapMySql()
        {
            ToTable("nonce");
            HasKey(c => c.Id);
            Property(c => c.Context).HasColumnType("text").IsRequired();
            Property(c => c.Code).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            Property(c => c.TimeStamp).HasColumnType("datetime").IsRequired();
        }
    }

    /// <summary>
    /// NonceMapSqlServer
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class NonceMapSqlServer : EntityTypeConfiguration<Nonce>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public NonceMapSqlServer()
        {
            ToTable("nonce");
            HasKey(c => c.Id);
            Property(c => c.Context).IsRequired();//.HasColumnType("ntext");
            Property(c => c.Code).HasMaxLength(100).IsRequired();
            Property(c => c.TimeStamp).IsRequired();//.HasColumnType("datetime");
        }
    }
}
