using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocFieldMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocField&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocField trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocFieldMapMySql : EntityTypeConfiguration<DocField>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocFieldMapMySql()
        {
            ToTable("docfield");
            Property(p => p.DocFieldName).IsRequired().HasMaxLength(128).HasColumnType("varchar");
            Property(p => p.IsActivated).IsRequired().HasColumnType("bit");
            Property(p => p.CategoryBusinessId).IsRequired();
            Property(p => p.Order);
            Property(p => p.IconFileName).HasColumnType("varchar");
            Property(p => p.IconFileDisplayName).HasColumnType("varchar");
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");

            Property(p => p.StoreIds).HasMaxLength(1000).HasColumnType("varchar");
            Ignore(p => p.ListStoreIds);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocFieldMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocField&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocField trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocFieldMapSqlServer : EntityTypeConfiguration<DocField>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocFieldMapSqlServer()
        {
            ToTable("docfield");
            Property(p => p.DocFieldName).IsRequired().HasMaxLength(128);
            Property(p => p.IsActivated).IsRequired();
            Property(p => p.CategoryBusinessId).IsRequired();
            //Property(p => p.Order);
            //Property(p => p.IconFileDisplayName);
            //Property(p => p.IconFileName);
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnName("Version");//.HasColumnType("datetime");
            Property(p => p.StoreIds).HasMaxLength(1000);
            Ignore(p => p.ListStoreIds);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocFieldMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocField&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocField trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocFieldMapOracle : EntityTypeConfiguration<DocField>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocFieldMapOracle()
        {
            
        }
    }
}
