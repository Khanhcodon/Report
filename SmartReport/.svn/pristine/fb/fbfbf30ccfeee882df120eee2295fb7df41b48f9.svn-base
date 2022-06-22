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
    ///     * Inherit : EntityTypeConfiguration&lt;Sms&gt;
    /// Create Date : 261015
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng Sms trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class SmsMapMySql : EntityTypeConfiguration<Sms>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SmsMapMySql()
        {
            ToTable("sms");
            HasKey(c => c.SmsId);
            Property(c => c.PhoneNumber).IsRequired().HasMaxLength(15).HasColumnType("char");
            Property(c => c.Message).IsRequired().HasColumnType("text");
            Property(c => c.IsSent).IsRequired().HasColumnType("bit");
            Property(c => c.DateCreated);
            Property(c => c.DateSend);
            Property(c => c.UserSendId);
            Property(c => c.UserName).HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.DocumentId);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Sms&gt;
    /// Create Date : 261015
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng Sms trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class SmsMapSqlServer : EntityTypeConfiguration<Sms>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SmsMapSqlServer()
        {
            ToTable("sms");
            HasKey(c => c.SmsId);
            Property(c => c.PhoneNumber).IsRequired().HasMaxLength(15).HasColumnType("varchar");
            Property(c => c.Message).IsRequired().HasMaxLength(4000);//.HasColumnType("ntext");
            Property(c => c.IsSent).IsRequired();//.HasColumnType("bit");
            //Property(c => c.DateCreated);
            //Property(c => c.DateSend);
            //Property(c => c.UserSendId);
            Property(c => c.UserName).HasMaxLength(255);
            Property(c => c.DocumentId);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AttachmentMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Sms&gt;
    /// Create Date : 261015
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng Sms trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class SmsMapOracle : EntityTypeConfiguration<Sms>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SmsMapOracle()
        {
        }
    }
}
