using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Common;

namespace Bkav.eGovCloud.DataAccess.Mapping.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LogMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Log&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Log trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class LogMapMySql : EntityTypeConfiguration<Log>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public LogMapMySql()
        {
            ToTable("log");
            Property(p => p.LogType).IsRequired();
            Property(p => p.ShortMessage).IsRequired().HasColumnType("text");
            Property(p => p.FullMessage).HasColumnType("longtext");
            Property(p => p.RequestJson).HasColumnType("longtext");
            Property(p => p.IpAddress).HasMaxLength(15).HasColumnType("varchar");
            Property(p => p.CreatedOnDate).HasColumnType("datetime");
            Ignore(p => p.LogTypeInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LogMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Log&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Log trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class LogMapSqlServer : EntityTypeConfiguration<Log>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public LogMapSqlServer()
        {
            ToTable("log");
            Property(p => p.LogType).IsRequired();
            Property(p => p.ShortMessage).IsRequired().HasColumnType("ntext");
            Property(p => p.FullMessage).HasColumnType("ntext");
            Property(p => p.RequestJson).HasColumnType("ntext");
            Property(p => p.IpAddress).HasMaxLength(15);
            Property(p => p.CreatedOnDate).HasColumnType("datetime");
            Ignore(p => p.LogTypeInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LogMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Log&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Log trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class LogMapOracle : EntityTypeConfiguration<Log>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public LogMapOracle()
        {

        }
    }
}
