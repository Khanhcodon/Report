using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateAttachmentMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StorePrivateAttachment&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StorePrivateAttachment trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class StorePrivateAttachmentMapMySql : EntityTypeConfiguration<StorePrivateAttachment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StorePrivateAttachmentMapMySql()
        {
            ToTable("storeprivate_attachment");
            Property(c => c.StorePrivateId).IsRequired();
            Property(c => c.AttachmentName).HasColumnType("varchar").HasMaxLength(255).IsRequired();
            Property(c => c.Description).HasColumnType("varchar").HasMaxLength(255);
            Property(c => c.Size).IsRequired();
            Property(c => c.CreatedOnDate).IsRequired();
            Property(c => c.CreatedByUserId).IsRequired();
            Property(c => c.CreatedByUserName).HasColumnType("varchar").HasMaxLength(64).IsRequired();
            Property(c => c.FileLocationId).IsRequired();
            Property(c => c.IdentityFolder).IsRequired().HasColumnType("varchar").HasMaxLength(10);
            Property(c => c.FileLocationKey).HasColumnType("varchar").HasMaxLength(36);
            Property(c => c.FileName).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            Ignore(c => c.SizeString);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateAttachmentMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StorePrivateAttachment&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StorePrivateAttachment trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class StorePrivateAttachmentMapSqlServer : EntityTypeConfiguration<StorePrivateAttachment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StorePrivateAttachmentMapSqlServer()
        {
            ToTable("storeprivate_attachment");
            Property(c => c.StorePrivateId).IsRequired();
            Property(c => c.AttachmentName).HasMaxLength(255).IsRequired();
            Property(c => c.Description).HasMaxLength(255);
            Property(c => c.Size).IsRequired();
            Property(c => c.CreatedOnDate).IsRequired();
            Property(c => c.CreatedByUserId).IsRequired();
            Property(c => c.CreatedByUserName).HasMaxLength(64).IsRequired();
            Property(c => c.FileLocationId).IsRequired();
            Property(c => c.IdentityFolder).IsRequired().HasMaxLength(10);
            Property(c => c.FileLocationKey).HasMaxLength(36);
            Property(c => c.FileName).IsRequired().HasMaxLength(36);
            Ignore(c => c.SizeString);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateAttachmentMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StorePrivateAttachment&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StorePrivateAttachment trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class StorePrivateAttachmentMapOracle : EntityTypeConfiguration<StorePrivateAttachment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StorePrivateAttachmentMapOracle()
        {

        }
    }
}
