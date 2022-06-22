

using Bkav.eGovCloud.Entities.Admin;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
namespace Bkav.eGovCloud.DataAccess.Mapping.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SiteMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Site&gt;
    /// Create Date : 260712
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Site trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class SiteMapMySql : EntityTypeConfiguration<Site>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SiteMapMySql()
        {
            ToTable("site");
            HasKey(p => p.SiteId);
            Property(p => p.SiteName).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(p => p.ServerIp).HasColumnType("varchar").HasMaxLength(20).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SiteMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Site&gt;
    /// Create Date : 260712
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Site trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class SiteMapSqlServer : EntityTypeConfiguration<Site>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SiteMapSqlServer()
        {
            ToTable("site");
            HasKey(p => p.SiteId);
            Property(p => p.SiteName).IsRequired().HasMaxLength(150);
            Property(p => p.ServerIp).IsRequired().HasMaxLength(20);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SiteMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Site&gt;
    /// Create Date : 260712
    /// Author      : TienBV
    /// Description : Mapping tương ứng với bảng Site trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class SiteMapOracle : EntityTypeConfiguration<Site>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SiteMapOracle()
        {

        }
    }
}
