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
    /// 
    /// </summary>
    [ComVisible(false)]
    public class DoctypeFeeMySql : EntityTypeConfiguration<DoctypeFee>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DoctypeFeeMySql()
        {
            ToTable("doctype_fee");
            HasKey(p => p.Id);
            Property(p => p.DocTypeId).IsRequired();
            Property(p => p.FeeId).IsRequired();
            //HasRequired(d => d.Doctype).WithMany(d => d.DoctypeFees).HasForeignKey(d => d.DocTypeId).WillCascadeOnDelete(false);
            //HasRequired(d => d.Fee).WithMany(d => d.DoctypeFees).HasForeignKey(d => d.FeeId).WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class DoctypeFeeSqlServer : EntityTypeConfiguration<DoctypeFee>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DoctypeFeeSqlServer()
        {
            ToTable("doctype_fee");
            HasKey(p => p.Id);
            Property(p => p.DocTypeId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.FeeId).IsRequired();
            //HasRequired(d => d.Doctype).WithMany(d => d.DoctypeFees).HasForeignKey(d => d.DocTypeId).WillCascadeOnDelete(false);
            //HasRequired(d => d.Fee).WithMany(d => d.DoctypeFees).HasForeignKey(d => d.FeeId).WillCascadeOnDelete(false);
        }
    }
}
