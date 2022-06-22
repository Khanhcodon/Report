using System.Data.Entity.ModelConfiguration;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// 
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class NotificationMapMySql : EntityTypeConfiguration<Notification>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public NotificationMapMySql()
        {
            ToTable("notification");
            HasKey(c => c.NotificationId);
            Property(x => x.NotificationType).IsRequired();
            Property(c => c.Title).HasColumnType("varchar").IsRequired();
            Property(c => c.Content).HasColumnType("varchar").IsRequired();
            Property(c => c.SenderAvatar).HasColumnType("varchar").HasMaxLength(500);
            Property(c => c.SenderUserName).HasColumnType("varchar").HasMaxLength(50);
            Property(c => c.SenderFullName).HasColumnType("varchar").HasMaxLength(255);
            Property(c => c.ChatterJid).HasColumnType("varchar").HasMaxLength(500);
            Property(c => c.ReceiveDate).IsRequired();

            Ignore(x => x.NotificationTypeEnum);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class NotificationMapSqlServer : EntityTypeConfiguration<Notification>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public NotificationMapSqlServer()
        {
            ToTable("Notification");
            HasKey(c => c.NotificationId);
            Property(x => x.NotificationType).IsRequired();
            Property(c => c.Title).HasColumnType("nvarchar").IsRequired();
            Property(c => c.Content).HasColumnType("nvarchar").IsRequired();
            Property(c => c.SenderAvatar).HasColumnType("nvarchar").HasMaxLength(500);
            Property(c => c.SenderUserName).HasColumnType("varchar").HasMaxLength(50);
            Property(c => c.SenderFullName).HasColumnType("nvarchar").HasMaxLength(255);
            Property(c => c.ChatterJid).HasColumnType("varchar").HasMaxLength(500);
            Property(c => c.ReceiveDate).IsRequired();

            Ignore(x => x.NotificationTypeEnum);
        }
    }
}
