using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StatisticsMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 260815
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng Statistics trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class StatisticsMapMySql : EntityTypeConfiguration<Statistics>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StatisticsMapMySql()
        {
            ToTable("statistics");
            HasKey(p => p.StatisticsId);
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(255).IsRequired();
            Property(p => p.Description).HasColumnType("varchar").HasMaxLength(500);
            Property(p => p.ParentId);
            Property(p => p.ReportGroup).HasColumnType("varchar").HasMaxLength(1000);
            Property(p => p.Query).HasColumnType("text").IsRequired();
            Property(p => p.UserPermission).HasColumnType("text");
            Property(p => p.DeptPermission).HasColumnType("text");
            Property(p => p.PositionPermission).HasColumnType("text");
            Property(p => p.IsActive).HasColumnType("bit").IsRequired();
            Property(p => p.DateCreated).HasColumnType("datetime").IsRequired();

            Ignore(c => c.ListDepartmentPositionHasPermission);
            Ignore(c => c.ListPositionHasPermission);
            Ignore(c => c.ListUserHasPermission);
            Ignore(c => c.Childs);
            Ignore(c => c.TreeGroupValue);
            Ignore(c => c.TreeGroupName);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StatisticsMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 260815
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng Statistics trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class StatisticsMapSqlServer : EntityTypeConfiguration<Statistics>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StatisticsMapSqlServer()
        {
            ToTable("statistics");
            HasKey(p => p.StatisticsId);
            Property(p => p.Name).HasMaxLength(255).IsRequired();
            Property(p => p.Description).HasMaxLength(500);
            //Property(p => p.ParentId);
            Property(p => p.ReportGroup).HasMaxLength(1000);
            Property(p => p.Query).IsRequired();//.HasColumnType("ntext");
            //Property(p => p.UserPermission).HasColumnType("ntext");
            //Property(p => p.DeptPermission).HasColumnType("ntext");
            //Property(p => p.PositionPermission).HasColumnType("ntext");
            Property(p => p.IsActive).IsRequired();//.HasColumnType("bit");
            Property(p => p.DateCreated).IsRequired();//.HasColumnType("datetime");

            Ignore(c => c.ListDepartmentPositionHasPermission);
            Ignore(c => c.ListPositionHasPermission);
            Ignore(c => c.ListUserHasPermission);
            Ignore(c => c.Childs);
            Ignore(c => c.TreeGroupValue);
            Ignore(c => c.TreeGroupName);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StatisticsMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Role&gt;
    /// Create Date : 260815
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng Statistics trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class StatisticsMapOracle : EntityTypeConfiguration<Statistics>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StatisticsMapOracle()
        {

        }
    }
}
