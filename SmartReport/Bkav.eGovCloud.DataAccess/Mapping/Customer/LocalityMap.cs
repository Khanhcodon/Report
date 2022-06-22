using Bkav.eGovCloud.Entities.Customer;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LocalityMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Locality&gt;
    /// Create Date : 100120
    /// Author      : VienTV
    /// Description : Mapping tương ứng với bảng locality trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class LocalityMapMySql : EntityTypeConfiguration<Locality>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public LocalityMapMySql()
        {
            ToTable("locality");
            Property(p => p.LocalityId).IsRequired();
            Property(p => p.LocalityName).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.Type).IsRequired();
            Property(p => p.ParentId).IsRequired();
            Property(p => p.LocalityParent).IsRequired().HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.Description).IsRequired().HasMaxLength(1000).HasColumnType("varchar");
            Property(p => p.Active);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LocalityMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Locality&gt;
    /// Create Date : 190620
    /// Author      : VienTV
    /// Description : Mapping tương ứng với bảng locality trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class LocalityMapSqlServer : EntityTypeConfiguration<Locality>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public LocalityMapSqlServer()
        {
            ToTable("locality");
            Property(p => p.LocalityId).IsRequired();
            Property(p => p.LocalityName).IsRequired().HasMaxLength(255);
            Property(p => p.Type).IsRequired();
            Property(p => p.ParentId).IsRequired();
            Property(p => p.LocalityParent).IsRequired().HasMaxLength(255);
            Property(p => p.Description).IsRequired().HasMaxLength(1000);
            Property(p => p.Active);
        }
    }
}
