using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CodeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Code&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Code trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class CodeMapMySql : EntityTypeConfiguration<Code>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CodeMapMySql()
        {
            ToTable("code");
            Property(p => p.CodeName).IsRequired().HasMaxLength(1000).HasColumnType("varchar");
            Property(p => p.Template).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.NumberLastest).IsRequired();
            Property(p => p.IncreaseId).IsRequired();
            Property(p => p.DepartmentId).HasColumnType("varchar").HasMaxLength(255);
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");

            Property(p => p.BussinessDocFieldDocTypeGroupId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CodeMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Code&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Code trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class CodeMapSqlServer : EntityTypeConfiguration<Code>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CodeMapSqlServer()
        {
            ToTable("code");
            Property(p => p.CodeName).IsRequired().HasMaxLength(1000);
            Property(p => p.Template).IsRequired().HasMaxLength(255);
            Property(p => p.NumberLastest).IsRequired();
            Property(p => p.IncreaseId).IsRequired();
            Property(p => p.DepartmentId).HasColumnType("varchar").HasMaxLength(255);
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnName("Version");//.HasColumnType("datetime");

            Property(p => p.BussinessDocFieldDocTypeGroupId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CodeMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Code&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Code trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class CodeMapOracle : EntityTypeConfiguration<Code>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CodeMapOracle()
        {
            
        }
    }
}
