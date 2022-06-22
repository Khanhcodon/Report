using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Bkav.eGovCloud.Entities.Customer;
using System.Runtime.InteropServices;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AddressMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Address&gt;
    /// Create Date : 15082013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng Address trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class RateEmployeeMapMySql : EntityTypeConfiguration<RateEmployee>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public RateEmployeeMapMySql()
        {
            ToTable("rateemployee");
            HasKey(p => p.RateEmployeeId);

            Property(c => c.Name).HasColumnType("varchar").HasMaxLength(255).IsRequired();
            Property(c => c.Description).HasColumnType("text");

            HasOptional(p => p.ParentRateEmployee)
                .WithMany(p => p.RateEmployeeChildrens)
                .HasForeignKey(p => p.ParentId)
                .WillCascadeOnDelete(false);

            HasOptional(p => p.Department)
                .WithMany(p => p.RateEmployees)
                .HasForeignKey(p => p.DepartmentId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AddressMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Address&gt;
    /// Create Date : 15082013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng Address trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class RateEmployeeMapSqlServer : EntityTypeConfiguration<RateEmployee>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public RateEmployeeMapSqlServer()
        {
            ToTable("rateemployee");
            HasKey(p => p.RateEmployeeId);

            Property(c => c.Name).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
            Property(c => c.Description).HasColumnType("nvarchar");

            HasOptional(p => p.ParentRateEmployee)
                .WithMany(p => p.RateEmployeeChildrens)
                .HasForeignKey(p => p.ParentId)
                .WillCascadeOnDelete(false);

            HasOptional(p => p.Department)
                .WithMany(p => p.RateEmployees)
                .HasForeignKey(p => p.DepartmentId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AddressMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Address&gt;
    /// Create Date : 15082013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng Address trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class RateEmployeeMapOracle : EntityTypeConfiguration<RateEmployee>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public RateEmployeeMapOracle()
        {


        }
    }
}
