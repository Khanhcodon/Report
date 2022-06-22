using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocfieldDoctypeWorkflowMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocfieldDoctypeWorkflow&gt;
    /// Create Date : 150107
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng DocfieldDoctypeWorkflow trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocfieldDoctypeWorkflowMapMySql : EntityTypeConfiguration<DocfieldDoctypeWorkflow>
    {
        /// <summary>
        /// 
        /// </summary>
        public DocfieldDoctypeWorkflowMapMySql()
        {
            ToTable("docfield_doctype_workflow");
            HasKey(p => p.Id);
            Property(p => p.WorkflowId).IsRequired().HasColumnType("int");
            Property(p => p.IsActivated).IsRequired().HasColumnType("bit");
            Property(p => p.DocTypeId);
            Property(p => p.DocFieldId).HasColumnType("int");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocfieldDoctypeWorkflowMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocfieldDoctypeWorkflow&gt;
    /// Create Date : 150107
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng DocfieldDoctypeWorkflow trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocfieldDoctypeWorkflowMapSqlServer : EntityTypeConfiguration<DocfieldDoctypeWorkflow>
    {
        /// <summary>
        /// 
        /// </summary>
        public DocfieldDoctypeWorkflowMapSqlServer()
        {
            ToTable("docfield_doctype_workflow");
            HasKey(p => p.Id);
            Property(p => p.WorkflowId).IsRequired().HasColumnType("int");
            Property(p => p.IsActivated).IsRequired();//.HasColumnType("bit");
            Property(p => p.DocTypeId).HasColumnType("uniqueidentifier");
            //Property(p => p.DocFieldId).HasColumnType("int");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocfieldDoctypeWorkflowMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocfieldDoctypeWorkflow&gt;
    /// Create Date : 150107
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng DocfieldDoctypeWorkflow trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocfieldDoctypeWorkflowMapOracle : EntityTypeConfiguration<DocfieldDoctypeWorkflow>
    {
        /// <summary>
        /// 
        /// </summary>
        public DocfieldDoctypeWorkflowMapOracle()
        {
          
        }
    }
}
