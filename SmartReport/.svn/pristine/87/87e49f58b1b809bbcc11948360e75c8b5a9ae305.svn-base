using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Attachment&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Attachment trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class AttachmentMapMySql : EntityTypeConfiguration<Attachment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AttachmentMapMySql()
        {
            ToTable("attachment");
            Property(c => c.AttachmentName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.DocumentId).IsRequired();
            Property(c => c.IsDeleted).IsRequired().HasColumnType("bit");
            Property(c => c.VersionAttachment).IsRequired();
            Property(c => c.Size).IsRequired();
            Ignore(c => c.Extension);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Attachment&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Attachment trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class AttachmentMapSqlServer : EntityTypeConfiguration<Attachment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AttachmentMapSqlServer()
        {
            ToTable("attachment");
            Property(c => c.AttachmentName).IsRequired().HasMaxLength(255);
            Property(c => c.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(c => c.IsDeleted).IsRequired();
            Property(c => c.VersionAttachment).IsRequired();
            Property(c => c.Size).IsRequired();
            Ignore(c => c.Extension);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Attachment&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Attachment trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class AttachmentMapOracle : EntityTypeConfiguration<Attachment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AttachmentMapOracle()
        {

        }
    }
}
