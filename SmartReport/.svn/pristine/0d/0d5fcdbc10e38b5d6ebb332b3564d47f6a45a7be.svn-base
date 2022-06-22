using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FeeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Fee&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Fee trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class FeeMapMySql : EntityTypeConfiguration<Fee>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FeeMapMySql()
        {
            ToTable("fee");
            Property(p => p.FeeTypeId).IsRequired();
            Property(p => p.DocTypeId).IsRequired();
            Property(p => p.FeeName).IsRequired().HasMaxLength(1000).HasColumnType("varchar");
            Property(p => p.Price).IsRequired();
            Property(p => p.IsRequired).IsRequired().HasColumnType("bit");
            Ignore(p => p.FeeTypeIdInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FeeMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Fee&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Fee trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class FeeMapSqlServer : EntityTypeConfiguration<Fee>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FeeMapSqlServer()
        {
            ToTable("fee");
            Property(p => p.FeeTypeId).IsRequired();
            Property(p => p.DocTypeId).IsRequired();
            Property(p => p.FeeName).IsRequired().HasMaxLength(1000);
            Property(p => p.Price).IsRequired();
            Property(p => p.IsRequired).IsRequired();
            Ignore(p => p.FeeTypeIdInEnum);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FeeMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Fee&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Fee trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class FeeMapOracle : EntityTypeConfiguration<Fee>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public FeeMapOracle()
        {
            
        }
    }
}
