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
    ///     * Inherit : EntityTypeConfiguration&lt;UserActivityLog&gt;
    /// Create Date : 150414
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng UserActivityLog trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class UserActivityLogMapMySql : EntityTypeConfiguration<UserActivityLog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserActivityLogMapMySql()
        {
            ToTable("useractivitylog");
            HasKey(p => p.UserActivityLogId);
            Property(p => p.UserSendId).IsRequired();
            Property(p => p.UserReceiveId).IsRequired();
            Property(p => p.Compendium).HasColumnType("text").IsRequired();
            Property(p => p.DocumentCopyType);
            Property(p => p.SentDate).HasColumnType("datetime").IsRequired();
            Property(p => p.NotificationType).IsRequired();
            Property(p => p.IsViewed).HasColumnType("bit");
            Property(p => p.IsNotified).HasColumnType("bit");
            Property(p => p.HasDisplayNumberInBell).HasColumnType("bit");
            Property(p => p.UserNameSend).HasColumnType("varchar").HasMaxLength(50);
            Property(p => p.FullNameSend).HasColumnType("varchar").HasMaxLength(255);
            Property(p => p.DocumentCopyId);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActivityLogMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserActivityLog&gt;
    /// Create Date : 150414
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng UserActivityLog trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class UserActivityLogMapSqlServer : EntityTypeConfiguration<UserActivityLog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserActivityLogMapSqlServer()
        {
            ToTable("useractivitylog");
            HasKey(p => p.UserActivityLogId);
            Property(p => p.UserSendId).IsRequired();
            Property(p => p.UserReceiveId).IsRequired();
            Property(p => p.Compendium).IsRequired();//.HasColumnType("ntext");
            Property(p => p.DocumentCopyType);
            Property(p => p.SentDate).IsRequired();//.HasColumnType("datetime");
            Property(p => p.NotificationType).IsRequired();
            //Property(p => p.IsViewed).HasColumnType("bit");
            //Property(p => p.IsNotified).HasColumnType("bit");
            //Property(p => p.HasDisplayNumberInBell).HasColumnType("bit");
            //Property(p => p.UserNameSend);
            //Property(p => p.FullNameSend);
            //Property(p => p.DocumentCopyId);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActivityLogMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserActivityLog&gt;
    /// Create Date : 150414
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng UserActivityLog trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class UserActivityLogMapOracle : EntityTypeConfiguration<UserActivityLog>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserActivityLogMapOracle()
        {

        }
    }
}
