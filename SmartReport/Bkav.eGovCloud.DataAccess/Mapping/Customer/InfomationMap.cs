using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : InfomationMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Address&gt;
    /// Create Date : 17082013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng Infomation trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class InfomationMapMySql : EntityTypeConfiguration<Infomation>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public InfomationMapMySql()
        {
            ToTable("infomation");
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(p => p.Address).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(150);
            Property(p => p.PhoneExt).HasColumnType("varchar").HasMaxLength(150);
            Property(p => p.Email).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.Fax).HasColumnType("varchar").HasMaxLength(15);
            Property(p => p.Website).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.Alias).HasColumnType("varchar").HasMaxLength(50);
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
    public class InfomationMapSqlServer : EntityTypeConfiguration<Infomation>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public InfomationMapSqlServer()
        {
            ToTable("infomation");
            Property(p => p.Name).HasMaxLength(150).IsRequired();
            Property(p => p.Address).HasMaxLength(300).IsRequired();
            Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(150);
            Property(p => p.PhoneExt).HasColumnType("varchar").HasMaxLength(150);
            Property(p => p.Email).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.Fax).HasColumnType("varchar").HasMaxLength(15);
            Property(p => p.Website).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.Alias).HasMaxLength(50);
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
    public class InfomationMapOracle : EntityTypeConfiguration<Infomation>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public InfomationMapOracle()
        {
        }
    }
}
