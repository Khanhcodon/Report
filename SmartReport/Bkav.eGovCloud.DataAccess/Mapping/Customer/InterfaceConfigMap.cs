using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Bkav.eGovCloud.Entities.Customer;
using System.Runtime.InteropServices;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : InterfaceConfigMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;InterfaceConfig&gt;
    /// Create Date : 010216
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng InterfaceConfig trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class InterfaceConfigMapMySql : EntityTypeConfiguration<InterfaceConfig>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public InterfaceConfigMapMySql()
        {
            ToTable("interface_config");
            HasKey(p => p.InterfaceConfigId);
            Property(p => p.InterfaceConfigName).HasColumnType("varchar").HasMaxLength(255).IsRequired();
            Property(p => p.Description).HasColumnType("varchar").HasMaxLength(2000);
            Property(p => p.Template).HasColumnType("text");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : InterfaceConfigMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;InterfaceConfig&gt;
    /// Create Date : 010216
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng InterfaceConfig trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class InterfaceConfigMapSqlServer : EntityTypeConfiguration<InterfaceConfig>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public InterfaceConfigMapSqlServer()
        {
            ToTable("interface_config");
            HasKey(p => p.InterfaceConfigId);
            Property(p => p.InterfaceConfigName).HasMaxLength(255).IsRequired();
            Property(p => p.Description).HasMaxLength(2000);
            Property(p => p.Template).HasColumnType("text");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : InterfaceConfigMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;InterfaceConfig&gt;
    /// Create Date : 010216
    /// Author      : HopCV
    /// Description : Mapping tương ứng với bảng InterfaceConfig trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class InterfaceConfigMapOracle : EntityTypeConfiguration<InterfaceConfig>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public InterfaceConfigMapOracle()
        {
        }
    }
}
