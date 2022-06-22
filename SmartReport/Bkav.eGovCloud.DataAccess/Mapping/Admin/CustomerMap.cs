using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Mapping.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov
    /// Project: eGov Cloud v1.0
    /// Class : CustomerMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Customer&gt;
    /// Create Date : 260712
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Customer trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class CustomerMapMySql : EntityTypeConfiguration<Bkav.eGovCloud.Entities.Admin.Customer>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CustomerMapMySql()
        {
            ToTable("customer");
            Property(p => p.CustomerId).IsRequired();
            Property(p => p.Name).IsRequired().HasMaxLength(300).HasColumnType("varchar");
            Property(p => p.Email).IsRequired().HasMaxLength(150).HasColumnType("varchar");
            Property(p => p.Phone).HasMaxLength(15).HasColumnType("varchar");
            Property(p => p.Address).HasMaxLength(500).HasColumnType("varchar");
            Property(p => p.CustomerType).IsRequired().HasColumnType("bit");
            Property(p => p.Province).HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.District).HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.Commune).HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.IsActivated).IsRequired().HasColumnType("bit");
            Ignore(p => p.CustomerTypeInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CustomerMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Customer&gt;
    /// Create Date : 260712
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Customer trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class CustomerMapSqlServer : EntityTypeConfiguration<Bkav.eGovCloud.Entities.Admin.Customer>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CustomerMapSqlServer()
        {
            ToTable("customer");
            Property(p => p.CustomerId).IsRequired();
            Property(p => p.Name).IsRequired().HasMaxLength(300);
            Property(p => p.Email).IsRequired().HasMaxLength(150);
            Property(p => p.Phone).HasMaxLength(15);
            Property(p => p.Address).HasMaxLength(500);
            Property(p => p.CustomerType).IsRequired();
            Property(p => p.Province).HasMaxLength(128);
            Property(p => p.District).HasMaxLength(128);
            Property(p => p.Commune).HasMaxLength(128);
            Property(p => p.IsActivated).IsRequired();
            Ignore(p => p.CustomerTypeInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CustomerMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Customer&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Customer trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class CustomerMapOracle : EntityTypeConfiguration<Bkav.eGovCloud.Entities.Admin.Customer>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CustomerMapOracle()
        {

        }
    }
}
