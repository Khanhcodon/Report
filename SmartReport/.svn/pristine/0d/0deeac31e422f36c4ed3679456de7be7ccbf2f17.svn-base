using Bkav.eGovCloud.Entities.Customer;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActionLevelMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ActionLevel&gt;
    /// Create Date : 190620
    /// Author      : SuBD
    /// Description : Mapping tương ứng với bảng ActionLevel trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ActionLevelMapMySql : EntityTypeConfiguration<ActionLevel>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ActionLevelMapMySql()
        {
            ToTable("actionlevel");
            Property(p => p.ActionLevelId).IsRequired();
            Property(p => p.ActionLevelName).IsRequired().HasMaxLength(1000).HasColumnType("varchar");
            Property(p => p.ActionLevelCode).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.StartTime);
            Property(p => p.EndTime);
            Property(p => p.TemplateKey).HasMaxLength(255).HasColumnType("varchar");
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActionLevelMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ActionLevel&gt;
    /// Create Date : 190620
    /// Author      : SuBD
    /// Description : Mapping tương ứng với bảng ActionLevel trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class ActionLevelMapSqlServer : EntityTypeConfiguration<ActionLevel>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ActionLevelMapSqlServer()
        {
            ToTable("actionlevel");
            Property(p => p.ActionLevelId).IsRequired();
            Property(p => p.ActionLevelName).IsRequired().HasMaxLength(255);
            Property(p => p.ActionLevelCode).IsRequired();
            Property(p => p.StartTime);
            Property(p => p.EndTime);
            Property(p => p.TemplateKey).HasMaxLength(255);
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnName("Version");//.HasColumnType("datetime");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActionLevelMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ActionLevel&gt;
    /// Create Date : 190620
    /// Author      : SuBD
    /// Description : Mapping tương ứng với bảng ActionLevel trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class ActionLevelMapOracle : EntityTypeConfiguration<ActionLevel>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ActionLevelMapOracle()
        {
            
        }
    }
}
