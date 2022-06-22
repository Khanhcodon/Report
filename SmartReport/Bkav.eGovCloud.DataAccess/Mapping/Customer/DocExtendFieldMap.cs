using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocExtendFieldMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocExtendField&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocExtendField trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocExtendFieldMapMySql : EntityTypeConfiguration<DocExtendField>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocExtendFieldMapMySql()
        {
            ToTable("doc_extendfield");
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.ExtendFieldId).IsRequired();
            Property(p => p.FormId).IsRequired();
            Property(p => p.ExtendFieldName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.ExtendFieldValue).IsRequired().HasColumnType("varchar");
            HasRequired(p => p.Form)
                .WithMany()
                .HasForeignKey(p => p.FormId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocExtendFieldMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocExtendField&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocExtendField trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocExtendFieldMapSqlServer : EntityTypeConfiguration<DocExtendField>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocExtendFieldMapSqlServer()
        {
            ToTable("doc_extendfield");
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.ExtendFieldId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.FormId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.ExtendFieldName).IsRequired().HasMaxLength(255);
            Property(p => p.ExtendFieldValue).IsRequired();
            HasRequired(p => p.Form)
                .WithMany()
                .HasForeignKey(p => p.FormId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocExtendFieldMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocExtendField&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocExtendField trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocExtendFieldMapOracle : EntityTypeConfiguration<DocExtendField>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocExtendFieldMapOracle()
        {
            ToTable("doc_extendfield");
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.ExtendFieldId).IsRequired();
            Property(p => p.FormId).IsRequired();
            Property(p => p.ExtendFieldName).IsRequired().HasMaxLength(255);
            Property(p => p.ExtendFieldValue).IsRequired().HasMaxLength(255);
            HasRequired(p => p.Form)
                .WithMany()
                .HasForeignKey(p => p.FormId)
                .WillCascadeOnDelete(false);
        }
    }
}
