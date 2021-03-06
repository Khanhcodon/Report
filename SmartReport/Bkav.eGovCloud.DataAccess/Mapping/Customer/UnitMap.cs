using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
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
    public class UnitMapMySql : EntityTypeConfiguration<Ad_Unit>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UnitMapMySql()
        {
            ToTable("dim_ad_unit");
            HasKey(c => c.IdUnit);
            Property(c => c.Unit).IsRequired().HasMaxLength(500).HasColumnType("varchar");
            

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
    public class UnitMapSqlServer : EntityTypeConfiguration<Ad_Unit>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UnitMapSqlServer()
        {
            ToTable("dim_ad_unit");
            Property(c => c.IdUnit).HasColumnType("uniqueidentifier");
            Property(c => c.Unit).IsRequired().HasMaxLength(500);
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
    public class UnitMapOracle : EntityTypeConfiguration<Ad_Unit>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UnitMapOracle()
        {

        }
    }
}
