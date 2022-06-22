using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Report trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ReportMapMySql : EntityTypeConfiguration<Report>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportMapMySql()
        {
            ToTable("report");
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(300).IsRequired();
            Property(p => p.Description).HasColumnType("varchar").HasMaxLength(400);
            Property(p => p.ParentId);
            Property(p => p.DocColumnId).HasColumnType("int").IsRequired();
            Property(p => p.GroupForTree).HasColumnType("varchar").HasMaxLength(30);
            Property(p => p.QueryStatistics).HasColumnType("text");
            Property(p => p.QueryTotal).HasColumnType("text");
            Property(p => p.CrystalPath).HasColumnType("varchar").HasMaxLength(1000);
            Property(p => p.FileLocationName).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.UserPermission).HasColumnType("text");
            Property(p => p.DeptPermission).HasColumnType("text");
            Property(p => p.PositionPermission).HasColumnType("text");
            Property(p => p.IsActive).HasColumnType("bit").IsRequired();
            Property(p => p.IsLabel).HasColumnType("bit");
            Property(p => p.DateCreated).IsRequired();
            Property(p => p.QueryTotalDocumentIsOverdue).HasColumnType("text");
            Property(p => p.QueryTotalDocumentProcessed).HasColumnType("text");

            Ignore(c => c.ListDepartmentPositionHasPermission);
            Ignore(c => c.ListPositionHasPermission);
            Ignore(c => c.ListUserHasPermission);
            Ignore(c => c.Childs);
            Ignore(c => c.TreeGroupValue);
            Ignore(c => c.GroupId);
            Ignore(c => c.TreeGroupName);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Report trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class ReportMapSqlServer : EntityTypeConfiguration<Report>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportMapSqlServer()
        {
            ToTable("report");
            Property(p => p.Name).HasMaxLength(300).IsRequired();
            Property(p => p.Description).HasMaxLength(400);
            //Property(p => p.ParentId);
            Property(p => p.DocColumnId).IsRequired();
            //Property(p => p.QueryReport).HasColumnType("ntext");
            //Property(p => p.QueryGroup).HasColumnType("ntext");
            Property(p => p.GroupForTree).HasColumnType("varchar").HasMaxLength(30);
            //Property(p => p.QueryStatistics).HasColumnType("ntext");
            //Property(p => p.QueryTotal).HasColumnType("ntext");
            //Property(p => p.Content).HasColumnType("ntext");
            //Property(p => p.ViewContent).HasColumnType("ntext");
            Property(p => p.CrystalPath).HasMaxLength(1000);
            Property(p => p.FileLocationName).HasMaxLength(50);
            //Property(p => p.UserPermission).HasColumnType("ntext");
            //Property(p => p.DeptPermission).HasColumnType("ntext");
            //Property(p => p.PositionPermission).HasColumnType("ntext");
            Property(p => p.IsActive).IsRequired();
            Property(p => p.DateCreated).IsRequired();
            //Property(p => p.QueryTotalDocumentIsOverdue).HasColumnType("ntext");
            //Property(p => p.QueryTotalDocumentProcessed).HasColumnType("ntext");

            Ignore(c => c.ListDepartmentPositionHasPermission);
            Ignore(c => c.ListPositionHasPermission);
            Ignore(c => c.ListUserHasPermission);
            Ignore(c => c.Childs); 
            Ignore(c => c.GroupId);
            Ignore(c => c.TreeGroupValue);
            Ignore(c => c.TreeGroupName);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ReportMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 080713
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Report trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class ReportMapOracle : EntityTypeConfiguration<Report>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ReportMapOracle()
        {

        }
    }
}
