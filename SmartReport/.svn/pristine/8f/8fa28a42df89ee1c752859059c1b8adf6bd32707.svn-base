using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Mapping.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ConnectionMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Connection&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Connection trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ConnectionMapMySql : EntityTypeConfiguration<Connection>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ConnectionMapMySql()
        {
            ToTable("connection");
            HasKey(p => p.ConnectionId);
            Property(p => p.ConnectionName).IsRequired().HasMaxLength(400).HasColumnType("varchar");
            Property(p => p.ServerName).IsRequired().HasMaxLength(64).HasColumnType("varchar").HasColumnName("ServerName");
            Property(p => p.Username).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.Password).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.Database).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.Port).HasColumnType("smallint");
            Property(p => p.ConnectionRaw).HasColumnType("text");
            Ignore(p => p.DatabaseTypeIdInEnum);
            Ignore(p => p.IsCreateDatabaseIfNotExist);
            Ignore(p => p.IsQuanTriTapTrung);
            Ignore(p => p.OverrideCurrentData);
            HasRequired(p => p.Domain);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ConnectionMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Connection&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Connection trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class ConnectionMapSqlServer : EntityTypeConfiguration<Connection>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ConnectionMapSqlServer()
        {
            ToTable("connection");
            HasKey(p => p.ConnectionId);
            Property(p => p.ConnectionName).IsRequired().HasMaxLength(400);
            Property(p => p.ServerName).IsRequired().HasMaxLength(64).HasColumnName("ServerName");
            Property(p => p.Username).IsRequired().HasMaxLength(64);
            Property(p => p.Password).IsRequired().HasMaxLength(64);
            Property(p => p.Database).IsRequired().HasMaxLength(64);
            Property(p => p.Port).HasColumnType("smallint");
            Property(p => p.ConnectionRaw).HasColumnType("ntext");
            HasRequired(p => p.Domain);
            Ignore(p => p.IsCreateDatabaseIfNotExist);
            Ignore(p => p.DatabaseTypeIdInEnum);
            Ignore(p => p.IsQuanTriTapTrung);
            Ignore(p => p.OverrideCurrentData);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ConnectionMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Connection&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Connection trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class ConnectionMapOracle : EntityTypeConfiguration<Connection>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ConnectionMapOracle()
        {
            
        }
    }
}
