using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FormMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Form&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Form trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class FormMapMySql : EntityTypeConfiguration<Form>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FormMapMySql()
        {
            ToTable("form");
            Property(p => p.FormTypeId).IsRequired();
            //Property(p => p.DocTypeId).IsRequired();
            Property(p => p.FormName).IsRequired().HasMaxLength(1000).HasColumnType("varchar");
            Property(p => p.Description).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.Json).HasColumnType("longtext");
            Property(p => p.IsPrimary).IsRequired().HasColumnType("bit");
            Property(p => p.IsActivated).IsRequired();
            Property(p => p.Template).HasColumnType("text");
            Property(p => p.EmbryonicPath).HasColumnType("varchar");
            Property(p => p.FormUrl).HasColumnType("varchar");
            Ignore(p => p.FormTypeIdInEnum);
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FormMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Form&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Form trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class FormMapSqlServer : EntityTypeConfiguration<Form>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FormMapSqlServer()
        {
            ToTable("form");
            Property(p => p.FormId).HasColumnType("uniqueidentifier");
            Property(p => p.FormTypeId).IsRequired();
            //Property(p => p.DocTypeId).IsRequired();
            Property(p => p.FormName).IsRequired().HasMaxLength(1000);
            Property(p => p.Description).HasMaxLength(255);
            //Property(p => p.Json).HasColumnType("ntext");
            Property(p => p.IsPrimary).IsRequired();
            Property(p => p.IsActivated).IsRequired();
            //Property(p => p.Template).HasColumnType("ntext");
            Property(p => p.EmbryonicPath).HasMaxLength(1500);
            Property(p => p.FormUrl).HasMaxLength(1000).HasColumnType("varchar");
            Ignore(p => p.FormTypeIdInEnum);
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnName("Version");//.HasColumnType("datetime");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FormMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Form&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Form trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class FormMapOracle : EntityTypeConfiguration<Form>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FormMapOracle()
        {
            
        }
    }
}
