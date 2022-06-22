using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTypeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocType&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocType trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocTypeMapMySql : EntityTypeConfiguration<DocType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTypeMapMySql()
        {
            ToTable("doctype");
            HasKey(p => p.DocTypeId);
            Property(p => p.CategoryId).IsRequired();
            Property(p => p.DocTypeName).IsRequired().HasMaxLength(1000).HasColumnType("varchar");
            Property(p => p.IsActivated).IsRequired().HasColumnType("bit");
            Property(p => p.IsAllowOnline).HasColumnType("bit");
            Property(p => p.CompendiumDefault).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.ContentDefault).HasColumnType("text");
            Property(p => p.OfficeId);
            Property(p => p.LevelId);
            Property(p => p.ActionLevel);
            Property(p => p.CategoryBusinessId).IsRequired();
            Property(p => p.TotalViewed);
            Property(p => p.TotalRegisted);
            Property(p => p.Unsigned).HasColumnType("varchar").HasMaxLength(1000);
            Property(l => l.Content).HasColumnType("text");
            Property(p => p.IconFileName).HasColumnType("varchar");
            Property(p => p.IconFileDisplayName).HasColumnType("varchar");
            Ignore(p => p.VersionByte);
            Ignore(p => p.StoreIds);
            Ignore(p => p.WorkflowId);
            Ignore(p => p.DocFieldName);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTypeMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocType&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocType trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocTypeMapSqlServer : EntityTypeConfiguration<DocType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTypeMapSqlServer()
        {
            ToTable("doctype");
            HasKey(p => p.DocTypeId);
            Property(p => p.DocTypeId).HasColumnType("uniqueidentifier");
            Property(p => p.CategoryId).IsRequired();
            Property(p => p.DocTypeName).IsRequired().HasMaxLength(1000);
            Property(p => p.IsActivated).IsRequired();
            Property(p => p.CompendiumDefault).HasColumnType("nvarchar").HasMaxLength(255);
            Property(p => p.ContentDefault);
            Property(p => p.CategoryBusinessId).IsRequired();
            //Property(p => p.OfficeId);
            //Property(p => p.LevelId);
            //Property(p => p.ActionLevel);
            //Property(p => p.TotalViewed);
            //Property(p => p.TotalRegisted);
            Property(p => p.Unsigned).HasColumnType("nvarchar").HasMaxLength(1000);
            Property(l => l.Content);
            Property(p => p.IconFileDisplayName);
            Property(p => p.IconFileName);
            Ignore(p => p.StoreIds);
            Ignore(p => p.VersionByte);
            Ignore(p => p.DocFieldName);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnName("Version");//.HasColumnType("datetime");
            Ignore(p => p.StoreIds);
            Ignore(p => p.WorkflowId);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTypeMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocType&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocType trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocTypeMapOracle : EntityTypeConfiguration<DocType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTypeMapOracle()
        {
            
        }
    }
}
