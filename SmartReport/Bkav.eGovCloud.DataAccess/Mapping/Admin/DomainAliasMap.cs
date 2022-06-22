using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Mapping.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DomainAliasMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DomainAlias&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DomainAlias trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DomainAliasMapMySql : EntityTypeConfiguration<DomainAlias>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DomainAliasMapMySql()
        {
            ToTable("domainalias");
            Property(p => p.Alias).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.DomainId).IsRequired();
            Property(p => p.IsActivated).HasColumnType("bit").IsRequired();
            Property(p => p.IsPrimary).HasColumnType("bit").IsRequired();
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasColumnType("timestamp").HasColumnName("Version");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DomainAliasMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DomainAlias&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DomainAlias trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DomainAliasMapSqlServer : EntityTypeConfiguration<DomainAlias>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DomainAliasMapSqlServer()
        {
            ToTable("domainalias");
            Property(p => p.Alias).IsRequired().HasMaxLength(255);
            Property(p => p.DomainId).IsRequired();
            Property(p => p.IsActivated).IsRequired();
            Property(p => p.IsPrimary).IsRequired();
            Ignore(p => p.VersionDateTime);
            Property(p => p.VersionByte).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DomainAliasMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DomainAlias&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DomainAlias trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DomainAliasMapOracle : EntityTypeConfiguration<DomainAlias>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DomainAliasMapOracle()
        {
            
        }
    }
}
