using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess.Mapping.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ServerMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Server&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Server trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ServerMapMySql : EntityTypeConfiguration<Server>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ServerMapMySql()
        {
            ToTable("server");
            Property(p => p.PublicDomain).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.Description).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.PrivateIp).IsRequired().HasMaxLength(15).HasColumnType("varchar");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ServerMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Server&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Server trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class ServerMapSqlServer : EntityTypeConfiguration<Server>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ServerMapSqlServer()
        {
            ToTable("server");
            Property(p => p.PublicDomain).IsRequired().HasMaxLength(255);
            Property(p => p.Description).HasMaxLength(255);
            Property(p => p.PrivateIp).IsRequired().HasMaxLength(15);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ServerMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Server&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Server trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class ServerMapOracle : EntityTypeConfiguration<Server>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ServerMapOracle()
        {

        }
    }
}
