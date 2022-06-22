using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Document&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Document trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocumentMapMySql : EntityTypeConfiguration<Document>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentMapMySql()
        {
            ToTable("document");
            Property(p => p.DocTypeId);
            Property(p => p.CategoryId).IsRequired();
            Property(p => p.DocCode).HasMaxLength(150).HasColumnType("varchar");
            Property(p => p.CitizenName).HasMaxLength(400).HasColumnType("varchar");
            Property(p => p.CitizenInfo).HasMaxLength(500).HasColumnType("varchar");
            Property(p => p.IdentityCard).HasColumnType("varchar").HasMaxLength(200);
            Property(p => p.Address).HasColumnType("varchar").HasMaxLength(200);
            Property(p => p.Email).HasColumnType("varchar").HasMaxLength(100);
            Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(50); 
            Property(p => p.Compendium).IsRequired().HasColumnType("text");
            Property(p => p.Compendium2).HasColumnType("text");
            Property(p => p.DateCreated).IsRequired();
            Property(p => p.UserCreatedId).IsRequired();
            Property(p => p.DateModified).IsRequired();
            Property(p => p.IsSuccess).HasColumnType("bit");
            Property(p => p.SuccessNote).HasColumnType("varchar").HasMaxLength(200);
            Property(p => p.IsReturned).HasColumnType("bit");
            Property(p => p.ReturnNote).HasColumnType("varchar").HasMaxLength(200);
            Property(p => p.Status).IsRequired();
            Property(p => p.IsAcknowledged).HasColumnType("bit");
            Property(p => p.IsGettingOut).HasColumnType("bit").IsRequired();
            Property(p => p.Original).IsRequired();
            Property(p => p.CategoryBusinessId).IsRequired();
            Property(p => p.ResultStatus).HasColumnType("bit");
            Property(p => p.IsConverted).HasColumnType("bit").IsRequired();
            Property(p => p.UrgentId).IsRequired();
            Property(p => p.InOutCode).HasColumnType("varchar").HasMaxLength(200);
            Property(p => p.InOutPlace).HasColumnType("text");
            Property(p => p.IsSupplemented).HasColumnType("bit");
            Property(p => p.DocFieldIds).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Organization).HasColumnType("text");
            Property(p => p.Keyword).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.DelayReason).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.WorkflowTypeId);
            Property(p => p.Note).HasColumnType("text");
            Property(p => p.HasCA).HasColumnType("bit");

            Ignore(p => p.DocRelations);
            Ignore(p => p.UrgentIdInEnum);
            Ignore(p => p.ListDocFieldId);
            Ignore(p => p.IsHSMC);
            //HasOptional(p => p.DocType)
            //    .WithMany(p => p.Documents)
            //    .HasForeignKey(p => p.DocTypeId)
            //    .WillCascadeOnDelete(false);
            HasOptional(p => p.UserSuccess)
                .WithMany()
                .HasForeignKey(p => p.UserSuccessId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Document&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Document trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocumentMapSqlServer : EntityTypeConfiguration<Document>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentMapSqlServer()
        {
            ToTable("document");
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier");
            Property(p => p.DocTypeId).HasColumnType("uniqueidentifier");
            Property(p => p.CategoryId).IsRequired();
            Property(p => p.DocCode).HasMaxLength(150).HasColumnType("varchar");
            Property(p => p.CitizenName).HasMaxLength(1000);
            Property(p => p.CitizenInfo).HasMaxLength(500);
            Property(p => p.IdentityCard).HasColumnType("varchar").HasMaxLength(200);
            Property(p => p.Address).HasMaxLength(200);
            Property(p => p.Email).HasColumnType("varchar").HasMaxLength(100);
            Property(p => p.Phone).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.Compendium).IsRequired();
            Property(p => p.Compendium2);
            Property(p => p.DateCreated).IsRequired();
            Property(p => p.UserCreatedId).IsRequired();
            Property(p => p.DateModified).IsRequired();
            //Property(p => p.IsSuccess);
            Property(p => p.SuccessNote).HasMaxLength(200);
            //Property(p => p.IsReturned);
            Property(p => p.ReturnNote).HasMaxLength(200);
            Property(p => p.Status).IsRequired();
            //Property(p => p.IsAcknowledged);
            Property(p => p.IsGettingOut).IsRequired();
            Property(p => p.Original).HasColumnType("tinyint").IsRequired();
            Property(p => p.CategoryBusinessId).IsRequired();
            //Property(p => p.ResultStatus);
            Property(p => p.IsConverted).IsRequired();
            Property(p => p.UrgentId).IsRequired();
            Property(p => p.InOutCode).HasColumnType("varchar").HasMaxLength(200);
            Property(p => p.InOutPlace);
            Property(p => p.IsSupplemented);
            Property(p => p.DocFieldIds).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.Organization);
            Property(p => p.Keyword).HasColumnType("nvarchar").HasMaxLength(255);
            Property(p => p.DelayReason).HasColumnType("nvarchar").HasMaxLength(255);
            Property(p => p.WorkflowTypeId);
            Property(p => p.Note);
            Property(p => p.HasCA).HasColumnType("bit");

            Ignore(p => p.DocRelations);
            Ignore(p => p.UrgentIdInEnum);
            Ignore(p => p.ListDocFieldId);
            Ignore(p => p.IsHSMC);
            HasOptional(p => p.UserSuccess)
                .WithMany()
                .HasForeignKey(p => p.UserSuccessId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocumentMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Document&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Document trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocumentMapOracle : EntityTypeConfiguration<Document>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocumentMapOracle()
        {

        }
    }
}
