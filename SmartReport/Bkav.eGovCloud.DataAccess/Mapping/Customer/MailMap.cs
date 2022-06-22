using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Mail&gt;
    /// Create Date : 261015
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng Mail trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class MailMapMySql : EntityTypeConfiguration<Mail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public MailMapMySql()
        {
            ToTable("mail");
            HasKey(c => c.MailId);

            Property(c => c.Subject).IsRequired().HasMaxLength(500).HasColumnType("varchar");
            Property(c => c.Body).IsRequired().HasColumnType("longtext");
            Property(c => c.SendTo).IsRequired().HasMaxLength(5000).HasColumnType("varchar");
            Property(c => c.Signature).HasColumnType("longtext");
            Property(c => c.Header).HasColumnType("longtext");
            Property(c => c.Sender).HasMaxLength(500).HasColumnType("varchar");
            Property(c => c.SenderDisplayName).HasMaxLength(500).HasColumnType("varchar");
            Property(c => c.IsBodyHtml).HasColumnType("bit");
            Property(c => c.CarbonCopysStr).HasMaxLength(1000).HasColumnType("varchar");
            Property(c => c.AttachmentIdStr).HasMaxLength(500).HasColumnType("varchar");

            Property(c => c.IsSent).HasColumnType("bit");
            Property(c => c.DateCreated).HasColumnType("datetime");
            Property(c => c.DateSend).HasColumnType("datetime");
            Property(c => c.UserSendId);
            Property(c => c.UserName).HasMaxLength(255).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Mail&gt;
    /// Create Date : 261015
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng Mail trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class MailMapSqlServer : EntityTypeConfiguration<Mail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public MailMapSqlServer()
        {
            ToTable("mail");
            HasKey(c => c.MailId);

            Property(c => c.Subject).IsRequired().HasMaxLength(500);
            Property(c => c.Body).IsRequired();//.HasColumnType("ntext");
            Property(c => c.SendTo).IsRequired().HasMaxLength(500);
            //Property(c => c.Signature).HasColumnType("ntext");
            //Property(c => c.Header).HasColumnType("ntext");
            Property(c => c.Sender).HasMaxLength(500).HasColumnType("varchar");
            Property(c => c.SenderDisplayName).HasMaxLength(500);
            //Property(c => c.IsBodyHtml).HasColumnType("bit");
            Property(c => c.CarbonCopysStr).HasMaxLength(1000).HasColumnType("varchar");
            Property(c => c.AttachmentIdStr).HasMaxLength(500);

            //Property(c => c.IsSent).HasColumnType("bit");
            //Property(c => c.DateCreated).HasColumnType("datetime");
            //Property(c => c.DateSend).HasColumnType("datetime");
            //Property(c => c.UserSendId);
            Property(c => c.UserName).HasMaxLength(255).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Mail&gt;
    /// Create Date : 261015
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng Mail trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class MailMapOracle : EntityTypeConfiguration<Mail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public MailMapOracle()
        {
        }
    }
}
