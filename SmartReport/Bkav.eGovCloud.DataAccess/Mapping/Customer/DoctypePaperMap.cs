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
    /// DoctypePaperMapMySql
    /// </summary>
    [ComVisible(false)]
    public class DoctypePaperMapMySql : EntityTypeConfiguration<DoctypePaper>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public DoctypePaperMapMySql()
        {
            ToTable("doctype_paper");
            HasKey(c => c.Id);
            Property(c => c.DocTypeId).IsRequired();
            //HasRequired(d => d.Doctype).WithMany(d => d.DoctypePapers).HasForeignKey(d => d.DocTypeId).WillCascadeOnDelete(false);
            Property(c => c.PaperId).IsRequired();
            // HasRequired(d => d.Paper).WithMany(d => d.DoctypePaper).HasForeignKey(d => d.PaperId).WillCascadeOnDelete(false);
            Ignore(x => x.Amount);
            Ignore(x => x.PaperName);
            //Ignore(x => x.IsRequired);
        }
    }

    /// <summary>
    /// DoctypePaperMapMySql
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class DoctypePaperMapSqlServer : EntityTypeConfiguration<DoctypePaper>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public DoctypePaperMapSqlServer()
        {
            ToTable("doctype_paper");
            HasKey(c => c.Id);
            Property(c => c.DocTypeId).HasColumnType("uniqueidentifier").IsRequired();
            //HasRequired(d => d.Doctype).WithMany(d => d.DoctypePapers).HasForeignKey(d => d.DocTypeId).WillCascadeOnDelete(false);
            Property(c => c.PaperId).IsRequired();
            // HasRequired(d => d.Paper).WithMany(d => d.DoctypePaper).HasForeignKey(d => d.PaperId).WillCascadeOnDelete(false);
            Ignore(x => x.Amount);
            Ignore(x => x.PaperName);
            //Ignore(x => x.IsRequired);
        }
    }
}
