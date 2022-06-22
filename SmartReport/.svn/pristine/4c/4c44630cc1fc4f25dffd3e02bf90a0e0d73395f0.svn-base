using System.Data.Entity.ModelConfiguration;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// 
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class NotificationsMapMySql : EntityTypeConfiguration<Notifications>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public NotificationsMapMySql()
        {
            ToTable("notifications");
            HasKey(n => n.NotificationId);
            Property(n => n.Title).HasColumnType("varchar").IsRequired();
            Property(n => n.Body).HasColumnType("varchar").IsRequired();
            Property(n => n.GroupId).HasColumnType("varchar");
            Property(n => n.UserId).HasColumnType("int");
            Property(n => n.Avatar).HasColumnType("varchar");
            Property(n => n.DateCreated).HasColumnType("datetime").IsRequired();
            Property(n => n.AppName).HasColumnType("varchar");
            Property(n => n.JsonData).HasColumnType("varchar").IsRequired();
            Property(n => n.IsSystemNotify).HasColumnType("bit").IsRequired();
            Property(n => n.IsSent).HasColumnType("bit").IsRequired();
            Property(n => n.IsNew).HasColumnType("bit").IsRequired();
            Property(n => n.IsReaded).HasColumnType("bit").IsRequired();

            Ignore(n => n.DateCreateStr);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class NotificationsMapSqlServer : EntityTypeConfiguration<Notifications>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public NotificationsMapSqlServer()
        {
            ToTable("Notifications");
            HasKey(n => n.NotificationId);
            Property(n => n.Title).HasColumnType("varchar").IsRequired();
            Property(n => n.Body).HasColumnType("varchar").IsRequired();
            Property(n => n.GroupId);
            Property(n => n.UserId).HasColumnType("int");
            Property(n => n.Avatar).HasColumnType("varchar");
            Property(n => n.DateCreated).HasColumnType("datetime").IsRequired();
            Property(n => n.AppName).HasColumnType("varchar");
            Property(n => n.JsonData).HasColumnType("varchar").IsRequired();
            Property(n => n.IsSystemNotify).HasColumnType("bit").IsRequired();
            Property(n => n.IsSent).HasColumnType("bit").IsRequired();
            Property(n => n.IsNew).HasColumnType("bit").IsRequired();
            Property(n => n.IsReaded).HasColumnType("bit").IsRequired();

            Ignore(n => n.DateCreateStr);
        }
    }
}
