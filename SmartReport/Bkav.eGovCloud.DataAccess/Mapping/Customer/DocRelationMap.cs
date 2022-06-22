using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocRelationMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocRelation&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocRelation trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocRelationMapMySql : EntityTypeConfiguration<DocRelation>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocRelationMapMySql()
        {
            ToTable("docrelation");
            HasKey(p => p.DocRelationId);
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.RelationId).IsRequired();
            Property(p => p.RelationCopyId).IsRequired();
            Property(p => p.RelationType).IsRequired();            
            HasRequired(p => p.Document)
                .WithMany(p => p.DocRelations)
                .HasForeignKey(p => p.DocumentId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocRelationMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocRelation&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocRelation trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocRelationMapSqlServer : EntityTypeConfiguration<DocRelation>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocRelationMapSqlServer()
        {
            ToTable("docrelation");
            HasKey(p => p.DocRelationId);
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.RelationId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.RelationCopyId).IsRequired();
            Property(p => p.RelationType).IsRequired();            
            HasRequired(p => p.Document)
                .WithMany(p => p.DocRelations)
                .HasForeignKey(p => p.DocumentId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocRelationMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocRelation&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocRelation trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocRelationMapOracle : EntityTypeConfiguration<DocRelation>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocRelationMapOracle()
        {

        }
    }
}
