using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentCopyMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentCopy&gt;
    /// Create Date : 251212
    /// Author      : GiangPN
    /// Description : Mapping tương ứng với bảng DocumentCopy trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocumentCopyMapMySql : EntityTypeConfiguration<DocumentCopy>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentCopyMapMySql()
        {
            ToTable("documentcopy");
            Property(p => p.DateCreated).IsRequired();
            Property(p => p.DateReceived).IsRequired();
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.WorkflowId).IsRequired();
            Property(p => p.UserCurrentId).IsRequired();
            Property(p => p.History).HasColumnType("text").IsRequired();
            Property(p => p.Status).IsRequired();
            Property(p => p.DocumentCopyType).IsRequired();
            Property(p => p.NodeCurrentName).HasColumnType("varchar").HasMaxLength(256);
            Property(p => p.LastComment).HasColumnType("text");
            Property(p => p.LastUserComment).HasColumnType("varchar").HasMaxLength(128);
            Property(p => p.DateModified).HasColumnType("timestamp");
            //HasRequired(p => p.Document)
            //    .WithMany(p => p.DocumentCopys)
            //    .HasForeignKey(p => p.DocumentId)
            //    .WillCascadeOnDelete(false);
            //HasRequired(p => p.UserCurrent)
            //    .WithMany(p => p.DocumentCopys)
            //    .HasForeignKey(p => p.UserCurrentId)
            //    .WillCascadeOnDelete(false);

            Ignore(p => p.Histories);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentCopyMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentCopy&gt;
    /// Create Date : 251212
    /// Author      : GiangPN
    /// Description : Mapping tương ứng với bảng DocumentCopy trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocumentCopyMapSqlServer : EntityTypeConfiguration<DocumentCopy>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentCopyMapSqlServer()
        {
            ToTable("documentcopy");
            Property(p => p.DateCreated).IsRequired();
            Property(p => p.DateReceived).HasColumnType("datetime2").IsRequired();
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.WorkflowId).IsRequired();
            Property(p => p.UserCurrentId).IsRequired();
            Property(p => p.History).IsRequired();
            Property(p => p.Status).IsRequired();
            Property(p => p.DocumentCopyType).IsRequired();
            Property(p => p.NodeCurrentName).HasColumnType("nvarchar").HasMaxLength(256);
            Property(p => p.LastComment);
            Property(p => p.LastUserComment).HasColumnType("nvarchar").HasMaxLength(128);
            Property(p => p.DateModified).HasColumnType("datetime");
            //HasRequired(p => p.Document)
            //    .WithMany(p => p.DocumentCopys)
            //    .HasForeignKey(p => p.DocumentId)
            //    .WillCascadeOnDelete(false);
            //HasRequired(p => p.UserCurrent)
            //    .WithMany(p => p.DocumentCopys)
            //    .HasForeignKey(p => p.UserCurrentId)
            //    .WillCascadeOnDelete(false);

            Ignore(p => p.Histories);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentCopyMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocumentCopy&gt;
    /// Create Date : 251212
    /// Author      : GiangPN
    /// Description : Mapping tương ứng với bảng DocumentCopy trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocumentCopyMapOracle : EntityTypeConfiguration<DocumentCopy>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentCopyMapOracle()
        {

        }
    }
}
