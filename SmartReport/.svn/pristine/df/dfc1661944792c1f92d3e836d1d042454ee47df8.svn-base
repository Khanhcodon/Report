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
    public class AddressMapMySql : EntityTypeConfiguration<Address>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AddressMapMySql()
        {
            ToTable("address");
            HasKey(p => p.AddressId);
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            Property(p => p.AddressString).HasColumnType("varchar").HasMaxLength(300).HasColumnName("Address");
            Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(15);
            Property(p => p.PhoneExt).HasColumnType("varchar").HasMaxLength(5);
            Property(p => p.Email).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.Fax).HasColumnType("varchar").HasMaxLength(15);
            Property(p => p.EdocId).HasColumnType("varchar").HasMaxLength(32).IsRequired();
            Property(p => p.IsMe).HasColumnType("bit");
            Property(p => p.Website).HasColumnType("varchar").HasMaxLength(300).HasColumnName("Website");
            Property(p => p.Alias).HasColumnType("varchar").HasMaxLength(300).HasColumnName("Alias");
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
    public class AddressMapSqlServer : EntityTypeConfiguration<Address>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AddressMapSqlServer()
        {
            ToTable("address");
            Property(p => p.Name).HasMaxLength(250).IsRequired();
            Property(p => p.AddressString).HasMaxLength(300).HasColumnName("Address");
            Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(15);
            Property(p => p.PhoneExt).HasColumnType("varchar").HasMaxLength(5);
            Property(p => p.Email).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.Fax).HasColumnType("varchar").HasMaxLength(15);
            Property(p => p.EdocId).HasColumnType("varchar").HasMaxLength(32).IsRequired();
            Property(p => p.Website).HasColumnType("varchar").HasMaxLength(300).HasColumnName("Website");
            Property(p => p.Alias).HasColumnType("varchar").HasMaxLength(300).HasColumnName("Alias");
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
    public class AddressMapOracle : EntityTypeConfiguration<Address>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AddressMapOracle()
        {
        }
    }
}
