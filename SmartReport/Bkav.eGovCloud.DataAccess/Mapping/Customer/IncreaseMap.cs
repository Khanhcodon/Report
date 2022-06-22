using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : IncreaseMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Increase&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Increase trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class IncreaseMapMySql : EntityTypeConfiguration<Increase>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public IncreaseMapMySql()
        {
            ToTable("increase");
            Property(p => p.Name).IsRequired().HasMaxLength(1000).HasColumnType("varchar");
            Property(p => p.Value).IsRequired();
            Property(p => p.BussinessDocFieldDocTypeGroupId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : IncreaseMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Increase&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Increase trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class IncreaseMapSqlServer : EntityTypeConfiguration<Increase>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public IncreaseMapSqlServer()
        {
            ToTable("increase");
            Property(p => p.Name).IsRequired().HasMaxLength(1000);
            Property(p => p.Value).IsRequired();
            Property(p => p.BussinessDocFieldDocTypeGroupId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : IncreaseMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Increase&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Increase trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class IncreaseMapOracle : EntityTypeConfiguration<Increase>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public IncreaseMapOracle()
        {
            
        }
    }
}
