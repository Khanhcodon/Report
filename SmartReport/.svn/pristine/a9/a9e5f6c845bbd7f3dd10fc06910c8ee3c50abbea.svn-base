using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// 
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class NotifyMapMySql : EntityTypeConfiguration<Notify>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public NotifyMapMySql()
        {
            ToTable("notify");
            HasKey(c => c.Id);
            Property(c => c.Option).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(c => c.TemplateId).IsRequired();
            Property(c => c.Description).HasMaxLength(100).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class NotifyMapSqlServer : EntityTypeConfiguration<Notify>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public NotifyMapSqlServer()
        {
            ToTable("notify");
            HasKey(c => c.Id);
            Property(c => c.Option).HasMaxLength(50).IsRequired();
            Property(c => c.TemplateId).IsRequired();
            Property(c => c.Description).HasMaxLength(100);
        }
    }
}
