using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class AdministrativeMapMySql : EntityTypeConfiguration<Administrative>
    {
        /// <summary>
        /// 
        /// </summary>
        public AdministrativeMapMySql()
        {
            ToTable("administrative");
            HasKey(p => p.AdministrativeId);
            Property(p => p.AdministrativeName).HasMaxLength(300).IsRequired();
            Property(p => p.AdministrativeType).IsRequired();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class AdministrativeMapSqlServer : EntityTypeConfiguration<Administrative>
    {
        /// <summary>
        /// 
        /// </summary>
        public AdministrativeMapSqlServer()
        {
            ToTable("administrative");
            HasKey(p => p.AdministrativeId);
            Property(p => p.AdministrativeName).HasMaxLength(300).IsRequired();
            Property(d => d.AdministrativeType).IsRequired();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class AdministrativeMapOracle : EntityTypeConfiguration<Administrative>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public AdministrativeMapOracle()
        {

        }
    }
}
