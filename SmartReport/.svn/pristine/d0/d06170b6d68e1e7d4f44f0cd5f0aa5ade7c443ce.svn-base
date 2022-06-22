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
    public class DoctypeTemplateMapMySql : EntityTypeConfiguration<DoctypeTemplate>
    {
        /// <summary>
        /// constructor
        /// </summary>
        public DoctypeTemplateMapMySql()
        {
            ToTable("doctype_template");
            HasKey(c => c.DoctypeTemplateId);
            Property(c => c.DoctypeId).IsRequired();
            //HasRequired(d => d.Doctype).WithMany(d => d.DoctypeTemplates).HasForeignKey(d => d.DoctypeId).WillCascadeOnDelete(true);
            Property(c => c.Name).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            Ignore(x => x.FileId);
        }
    }

    /// <summary>
    /// DoctypeTemplateMapMySql
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class DoctypeTemplateMapSqlServer : EntityTypeConfiguration<DoctypeTemplate>
    {
        /// <summary>
        /// constructor
        /// </summary>
        public DoctypeTemplateMapSqlServer()
        {
            ToTable("doctype_template");
            HasKey(c => c.DoctypeTemplateId);
            Property(c => c.DoctypeId).HasColumnType("uniqueidentifier").IsRequired();
            //HasRequired(d => d.Doctype).WithMany(d => d.DoctypeTemplates).HasForeignKey(d => d.DoctypeId).WillCascadeOnDelete(true);
            Property(c => c.Name).HasMaxLength(250).IsRequired();
            Ignore(x => x.FileId);
        }
    }
}
