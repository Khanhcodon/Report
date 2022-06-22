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
    /// DoctypeTemplateMapMySql
    /// </summary>
    [ComVisible(false)]
    public class OnlineTemplateMapMySql : EntityTypeConfiguration<OnlineTemplate>
    {
        /// <summary>
        /// constructor
        /// </summary>
        public OnlineTemplateMapMySql()
        {
            ToTable("online_template");
            HasKey(c => c.OnlineTemplateId);
            Property(c => c.Name).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            Property(c => c.Description).HasColumnType("longtext");
            Property(c => c.FileId);
        }
    }

    /// <summary>
    /// DoctypeTemplateMapMySql
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class OnlineTemplateMapSqlServer : EntityTypeConfiguration<OnlineTemplate>
    {
        /// <summary>
        /// constructor
        /// </summary>
        public OnlineTemplateMapSqlServer()
        {
            ToTable("online_template");
            HasKey(c => c.OnlineTemplateId);
            Property(c => c.Name).HasMaxLength(250).IsRequired();
            //Property(c => c.Description).HasColumnType("ntext");
            Property(c => c.FileId);
        }
    }
}
