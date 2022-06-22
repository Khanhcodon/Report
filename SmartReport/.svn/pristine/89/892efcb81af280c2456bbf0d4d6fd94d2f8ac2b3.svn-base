using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StorePrivate&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StorePrivate trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class StorePrivateMapMySql : EntityTypeConfiguration<StorePrivate>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StorePrivateMapMySql()
        {
            ToTable("storeprivate");
            Property(p => p.StorePrivateName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.Description).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.CreatedByUserId).IsRequired();
            Property(p => p.Status).IsRequired();
            Property(p => p.StorePrivateIdExt).HasColumnType("varchar").HasMaxLength(64);
            //HasRequired(p => p.CreatedByUser)
            //    .WithMany(p => p.StorePrivates)
            //    .HasForeignKey(p => p.CreatedByUserId)
            //    .WillCascadeOnDelete(false);
            //HasOptional(p => p.StorePrivateParent)
            //    .WithMany(p => p.StorePrivateChildren)
            //    .HasForeignKey(p => p.ParentId)
            //    .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StorePrivate&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StorePrivate trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class StorePrivateMapSqlServer : EntityTypeConfiguration<StorePrivate>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StorePrivateMapSqlServer()
        {
            ToTable("storeprivate");
            Property(p => p.StorePrivateName).IsRequired().HasMaxLength(255);
            Property(p => p.Description).HasMaxLength(255);
            Property(p => p.CreatedByUserId).IsRequired();
            Property(p => p.Status).IsRequired();
            Property(p => p.StorePrivateIdExt).HasColumnType("varchar").HasMaxLength(64);
            //HasRequired(p => p.CreatedByUser)
            //    .WithMany(p => p.StorePrivates)
            //    .HasForeignKey(p => p.CreatedByUserId)
            //    .WillCascadeOnDelete(false);
            //HasOptional(p => p.StorePrivateParent)
            //    .WithMany(p => p.StorePrivateChildren)
            //    .HasForeignKey(p => p.ParentId)
            //    .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StorePrivate&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StorePrivate trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class StorePrivateMapOracle : EntityTypeConfiguration<StorePrivate>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StorePrivateMapOracle()
        {
            
        }
    }
}
