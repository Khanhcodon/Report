using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WorkflowMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Workflow&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Workflow trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class WorkflowMapMySql : EntityTypeConfiguration<Workflow>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public WorkflowMapMySql()
        {
            ToTable("workflow");
            //Property(p => p.DocTypeId).IsRequired();
            Property(p => p.WorkflowName).IsRequired().HasColumnType("text");
           // Property(p => p.Template).HasColumnType("text");
            Property(p => p.Json).HasColumnType("text");
            Property(p => p.IsActivated).IsRequired().HasColumnType("bit");
            Property(p => p.CreatedByUserId).IsOptional();
            Property(p => p.CreatedOnDate).IsOptional();
            Property(p => p.WorkflowTypeJson).HasColumnType("text");
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnType("timestamp").HasColumnName("Version");

            Property(p => p.InterfaceConfigId).IsOptional();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WorkflowMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Workflow&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Workflow trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class WorkflowMapSqlServer : EntityTypeConfiguration<Workflow>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public WorkflowMapSqlServer()
        {
            ToTable("workflow");
           // Property(p => p.DocTypeId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.WorkflowName).IsRequired().HasColumnType("nvarchar").HasMaxLength(255);
          //  Property(p => p.Template);
            Property(p => p.Json);
            Property(p => p.IsActivated).IsRequired().HasColumnType("bit");
            Property(p => p.CreatedByUserId).IsOptional();
            Property(p => p.CreatedOnDate).IsOptional();
            Property(p => p.WorkflowTypeJson);
            Ignore(p => p.VersionByte);
            Property(p => p.VersionDateTime).IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .HasColumnName("Version");//.HasColumnType("datetime");

            Property(p => p.InterfaceConfigId).IsOptional();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WorkflowMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Workflow&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Workflow trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class WorkflowMapOracle : EntityTypeConfiguration<Workflow>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public WorkflowMapOracle()
        {

        }
    }
}
