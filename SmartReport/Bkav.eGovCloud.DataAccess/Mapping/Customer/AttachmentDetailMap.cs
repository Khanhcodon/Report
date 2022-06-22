using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentDetailMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;AttachmentDetail&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng AttachmentDetail trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class AttachmentDetailMapMySql : EntityTypeConfiguration<AttachmentDetail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AttachmentDetailMapMySql()
        {
            ToTable("attachment_detail");
            Property(c => c.AttachmentId).IsRequired();
            Property(c => c.VersionAttachmentDetail).IsRequired();
            Property(c => c.CreatedOnDate).IsRequired();
            Property(c => c.CreatedByUserId).IsRequired();
            Property(c => c.CreatedByUserName).HasColumnType("varchar").HasMaxLength(64).IsRequired();
            Property(c => c.Size).IsRequired();
            Property(c => c.FileLocationId).IsRequired();
            Property(c => c.IdentityFolder).IsRequired().HasColumnType("varchar").HasMaxLength(10);
            Property(c => c.FileLocationKey).HasColumnType("varchar").HasMaxLength(1000);
            Property(c => c.FileName).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            Ignore(c => c.IdentityAttachmentDetail);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentDetailMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;AttachmentDetail&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng AttachmentDetail trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class AttachmentDetailMapSqlServer : EntityTypeConfiguration<AttachmentDetail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AttachmentDetailMapSqlServer()
        {
            ToTable("attachment_detail");
            Property(c => c.AttachmentId).IsRequired();
            Property(c => c.VersionAttachmentDetail).IsRequired();
            Property(c => c.CreatedOnDate).IsRequired();
            Property(c => c.CreatedByUserId).IsRequired();
            Property(c => c.CreatedByUserName).HasColumnType("varchar").HasMaxLength(64).IsRequired();
            Property(c => c.Size).IsRequired();
            Property(c => c.FileLocationId).IsRequired();
            Property(c => c.IdentityFolder).IsRequired().HasColumnType("varchar").HasMaxLength(10);
            Property(c => c.FileLocationKey).HasColumnType("varchar").HasMaxLength(1000);
            Property(c => c.FileName).IsRequired().HasColumnType("varchar").HasMaxLength(36);
            Ignore(c => c.IdentityAttachmentDetail);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentDetailMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;AttachmentDetail&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng AttachmentDetail trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class AttachmentDetailMapOracle : EntityTypeConfiguration<AttachmentDetail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AttachmentDetailMapOracle()
        {

        }
    }
}
