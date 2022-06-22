using System.Data.Entity.ModelConfiguration;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// 
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class MobileDeviceMapMySql : EntityTypeConfiguration<MobileDevice>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public MobileDeviceMapMySql()
        {
            ToTable("mobiledevice");
            HasKey(c => c.MobileDeviceId);
            Property(x => x.OS).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
            Property(c => c.Serial).HasColumnType("varchar").IsRequired();
            Property(c => c.DeviceName).HasColumnType("varchar");
            Property(c => c.Location).HasColumnType("varchar");
            Property(c => c.Token).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class MobileDeviceMapSqlServer : EntityTypeConfiguration<MobileDevice>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public MobileDeviceMapSqlServer()
        {
            ToTable("mobiledevice");
            HasKey(c => c.MobileDeviceId);
            Property(x => x.OS).IsRequired();
            Property(x => x.CreatedDate).IsRequired();
            Property(c => c.Serial).HasColumnType("nvarchar").IsRequired();
            Property(c => c.DeviceName).HasColumnType("nvarchar");
            Property(c => c.Location).HasColumnType("nvarchar");
            Property(c => c.Token).HasColumnType("nvarchar");
        }
    }
}
