using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : NotifyConfigMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;NotifyConfig&gt;
    /// Create Date : 260712
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng NotifyConfig trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class NotifyConfigMapMySql : EntityTypeConfiguration<NotifyConfig>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public NotifyConfigMapMySql()
        {
            ToTable("notify_config");
            HasKey(c => c.Id);
            Property(c => c.Key).HasColumnType("varchar").HasMaxLength(255).IsRequired();
            Property(c => c.Description).HasMaxLength(1000).HasColumnType("varchar");
            Property(c => c.MailTemplateName).HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.SmsTemplateName).HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.HasAutoSendSms).HasColumnType("bit");
            Property(c => c.SmsTemplateId).HasColumnType("int");
            Property(c => c.HasAutoSendMail).HasColumnType("bit");
            Property(c => c.MailTemplateId).HasColumnType("int");

        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : NotifyConfigMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;NotifyConfig&gt;
    /// Create Date : 260712
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng NotifyConfig trong CSDL SqlServer
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class NotifyConfigMapSqlServer : EntityTypeConfiguration<NotifyConfig>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public NotifyConfigMapSqlServer()
        {
            ToTable("notify_config");
            HasKey(c => c.Id);
            Property(c => c.Key).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
            Property(c => c.Description).HasMaxLength(1000).HasColumnType("nvarchar");
            Property(c => c.MailTemplateName).HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.SmsTemplateName).HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.HasAutoSendSms).HasColumnType("bit");
            Property(c => c.SmsTemplateId).HasColumnType("int");
            Property(c => c.HasAutoSendMail).HasColumnType("bit");
            Property(c => c.MailTemplateId).HasColumnType("int");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : NotifyConfigMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;NotifyConfig&gt;
    /// Create Date : 260712
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng NotifyConfig trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class NotifyConfigMapOracle : EntityTypeConfiguration<NotifyConfig>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public NotifyConfigMapOracle()
        {

        }
    }
}
