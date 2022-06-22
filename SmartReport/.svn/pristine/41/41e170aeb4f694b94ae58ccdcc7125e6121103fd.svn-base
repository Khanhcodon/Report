using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActivityLogMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ActivityLog&gt;
    /// Create Date : 150414
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng ActivityLog trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ActivityLogMapMySql : EntityTypeConfiguration<ActivityLog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ActivityLogMapMySql()
        {
            ToTable("activitylog");
            Property(p => p.ActivityLogType).IsRequired();
            Property(p => p.Ip).HasColumnType("varchar").HasMaxLength(15).IsRequired();
            Property(p => p.UserName).HasColumnType("varchar").HasMaxLength(64);
            Property(p => p.Content).HasColumnType("text").IsRequired();
            Property(p => p.CreatedOnDate).IsRequired();
            Ignore(p => p.ActivityLogTypeInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActivityLogMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ActivityLog&gt;
    /// Create Date : 150414
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng ActivityLog trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class ActivityLogMapSqlServer : EntityTypeConfiguration<ActivityLog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ActivityLogMapSqlServer()
        {
            ToTable("activitylog");
            Property(p => p.ActivityLogType).HasColumnType("tinyint").IsRequired();
            Property(p => p.Ip).HasColumnType("varchar").HasMaxLength(15).IsRequired();
            Property(p => p.UserName).HasColumnType("varchar").HasMaxLength(64);
            Property(p => p.Content).IsRequired();
            Property(p => p.CreatedOnDate).IsRequired();
            Ignore(p => p.ActivityLogTypeInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActivityLogMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ActivityLog&gt;
    /// Create Date : 150414
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng ActivityLog trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class ActivityLogMapOracle : EntityTypeConfiguration<ActivityLog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ActivityLogMapOracle()
        {

        }
    }
}
