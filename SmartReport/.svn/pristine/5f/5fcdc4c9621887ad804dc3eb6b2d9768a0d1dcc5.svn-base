using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FormGroupMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;FormGroup&gt;
    /// Create Date : 011013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng DocTypeForm trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocTypeFormMapMySql : EntityTypeConfiguration<DocTypeForm>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTypeFormMapMySql()
        {
            ToTable("doctype_form");
            HasKey(p => p.DocTypeFormId);
            //Property(p => p.DocTypeId).IsRequired();
            //Property(p => p.FormId).IsRequired();
            Property(p => p.IsActive).IsRequired().HasColumnType("bit");
            Property(p => p.IsPrimary).IsRequired().HasColumnType("bit");
            HasRequired(p => p.DocType)
                .WithMany()
                .HasForeignKey(p => p.DocTypeId)
                .WillCascadeOnDelete(false);
            HasRequired(p => p.Form)
                .WithMany(p => p.DocTypeForms)
                .HasForeignKey(p => p.FormId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : IncreaseMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Increase&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Increase trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocTypeFormMapSqlServer : EntityTypeConfiguration<DocTypeForm>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTypeFormMapSqlServer()
        {
            ToTable("doctype_form");
            HasKey(p => p.DocTypeFormId);
            //Property(p => p.FormId).IsRequired();
            //Property(p => p.DocTypeId).IsRequired();
            Property(p => p.IsActive).IsRequired();
            Property(p => p.IsPrimary).IsRequired();
            HasRequired(p => p.DocType)
                .WithMany()
                .HasForeignKey(p => p.DocTypeId)
                .WillCascadeOnDelete(false);
            HasRequired(p => p.Form)
                .WithMany(p => p.DocTypeForms)
                .HasForeignKey(p => p.FormId)
                .WillCascadeOnDelete(false);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTypeFormMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;FormGroup&gt;
    /// Create Date : 011013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng DocTypeForm trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocTypeFormMapOracle : EntityTypeConfiguration<DocTypeForm>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTypeFormMapOracle()
        {
            
        }
    }
}
