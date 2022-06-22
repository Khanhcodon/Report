using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTimelineMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocTimeline&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocTimeline trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocTimelineMapMySql : EntityTypeConfiguration<DocTimeline>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTimelineMapMySql()
        {
            ToTable("doctimeline");
            HasKey(p => p.DocTimelineId);
            Property(p => p.DocumentId).IsRequired();
            Property(p => p.UserId).IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.FromDate).IsRequired();
            Property(p => p.NodeName).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.IsSuccess).HasColumnType("bit");
            Property(p => p.IsWorkingTime).IsRequired().HasColumnType("bit");
            Property(p => p.ProcessedMinutes).IsRequired();
            Property(p => p.NodeSendId);
            Property(p => p.NodeSendName).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.UserSendId).IsRequired();
            Property(p => p.WorkFlowId);
            Property(p => p.DateOverdue);
            Property(p => p.TimeInNode);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTimelineMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocTimeline&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocTimeline trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocTimelineMapSqlServer : EntityTypeConfiguration<DocTimeline>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTimelineMapSqlServer()
        {
            ToTable("doctimeline");
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.UserId).IsRequired();
            Property(p => p.DocumentCopyId).IsRequired();
            Property(p => p.FromDate).IsRequired();
            Property(p => p.NodeName).HasMaxLength(255);
            Property(p => p.IsWorkingTime).IsRequired();
            Property(p => p.ProcessedMinutes).IsRequired();
            //Property(p => p.NodeSendId);
            Property(p => p.NodeSendName).HasMaxLength(255);
            Property(p => p.UserSendId).IsRequired();
            Property(p => p.DateOverdue);
            //Property(p => p.WorkFlowId);
            //Property(p => p.TimeInNode);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTimelineMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocTimeline&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocTimeline trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocTimelineMapOracle : EntityTypeConfiguration<DocTimeline>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTimelineMapOracle()
        {
            
        }
    }
}
