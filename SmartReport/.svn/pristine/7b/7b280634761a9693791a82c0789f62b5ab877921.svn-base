using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Mapping.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DomainMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Domain&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Domain trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DomainMapMySql : EntityTypeConfiguration<Domain>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DomainMapMySql()
        {
            ToTable("domain");
            Property(p => p.DomainName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.CustomerName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.Email).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.Phone).HasMaxLength(32).HasColumnType("varchar");
            Property(p => p.Address).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.CustomerType).IsRequired().HasColumnType("bit");
            Property(p => p.Province).HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.District).HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.Commune).HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.Department).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.IsActivated).IsRequired().HasColumnType("bit");
            // Property(p => p.ConnectionId).IsRequired();
            //HasRequired(p => p.Connection)
            //    .WithMany()
            //    .HasForeignKey(p => p.ConnectionId)
            //    .WillCascadeOnDelete(false);

            Ignore(p => p.CustomerTypeInEnum);
            Ignore(p => p.Version);
            Property(p => p.VersionDateTime).IsRequired()
                .HasColumnType("timestamp").HasColumnName("Version");
            HasOptional(p => p.DomainParent)
                .WithMany(p => p.DomainChildren)
                .HasForeignKey(p => p.ParentId)
                .WillCascadeOnDelete(false);
            Ignore(p => p.DomainIds);
            Ignore(p => p.DomainUsers);
            // Ignore(p => p.ServerId);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DomainMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Domain&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Domain trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DomainMapSqlServer : EntityTypeConfiguration<Domain>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DomainMapSqlServer()
        {
            ToTable("domain");
            Property(p => p.DomainName).IsRequired().HasMaxLength(255);
            Property(p => p.CustomerName).IsRequired().HasMaxLength(255);
            Property(p => p.Email).IsRequired().HasMaxLength(255);
            Property(p => p.Phone).HasMaxLength(32);
            Property(p => p.Address).HasMaxLength(255);
            Property(p => p.CustomerType).IsRequired();
            Property(p => p.Province).HasMaxLength(128);
            Property(p => p.District).HasMaxLength(128);
            Property(p => p.Commune).HasMaxLength(128);
            Property(p => p.IsActivated).IsRequired();
            Ignore(p => p.CustomerTypeInEnum);
            Ignore(p => p.VersionDateTime);
            Property(p => p.Version).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");
            HasOptional(p => p.DomainParent)
                .WithMany(p => p.DomainChildren)
                .HasForeignKey(p => p.ParentId)
                .WillCascadeOnDelete(false);
            Ignore(p => p.DomainIds);
            Ignore(p => p.DomainUsers);
            // Ignore(p => p.ServerId);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DomainMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Domain&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Domain trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DomainMapOracle : EntityTypeConfiguration<Domain>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DomainMapOracle()
        {

        }
    }
}
