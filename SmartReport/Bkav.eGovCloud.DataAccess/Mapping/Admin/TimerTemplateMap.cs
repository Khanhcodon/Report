using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Mapping.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimerTemplateMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TimerTemplate&gt;
    /// Create Date : 25092013
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng TimerTemplate trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class TimerTemplateMapMySql : EntityTypeConfiguration<TimerTemplate>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TimerTemplateMapMySql()
        {
            ToTable("timertemplate");
            HasKey(p => p.TimerTemplateId);
            Property(p => p.Name).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            Property(p => p.Query).HasColumnType("text").IsRequired();
            Property(p => p.Template).HasColumnType("text");
            Property(p => p.DateCreated).HasColumnType("datetime").IsRequired();
            Property(p => p.IsActive).HasColumnType("bit").IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimeJobMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TimeJobDomain&gt;
    /// Create Date : 25092013
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng TimeJob trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class TimerTemplateMapSqlServer : EntityTypeConfiguration<TimerTemplate>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TimerTemplateMapSqlServer()
        {
            ToTable("timertemplate");
            HasKey(p => p.TimerTemplateId);
            Property(p => p.Name).HasMaxLength(250).IsRequired();
            Property(p => p.Query).IsRequired();
            Property(p => p.Template);
            Property(p => p.DateCreated).IsRequired();
            Property(p => p.IsActive).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : TimeJobMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;TimeJobDomain&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng TimeJob trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class TimerTemplateMapOracle : EntityTypeConfiguration<TimerTemplate>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public TimerTemplateMapOracle()
        {

        }
    }
}
