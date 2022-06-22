using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Class : ConfigTypeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ConfigType&gt;
    /// Create Date : 20191101
    /// Author      : VuHQ
    /// Description : Mapping tương ứng với bảng config_type trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class ConfigTypeMapMySql : EntityTypeConfiguration<ConfigType>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public ConfigTypeMapMySql()
        {
            ToTable("config_type");
            Property(c => c.TypeName).IsRequired().HasColumnType("varchar");
            Property(c => c.IsActivated).IsRequired().HasColumnType("bit");
        }
    }
}
