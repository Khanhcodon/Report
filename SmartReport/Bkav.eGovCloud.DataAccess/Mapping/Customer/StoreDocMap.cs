using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StoreDocMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StoreDoc&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StoreDoc trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class StoreDocMapMySql : EntityTypeConfiguration<StoreDoc>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StoreDocMapMySql()
        {
            ToTable("store_doc");
            Property(p => p.StoreId).IsRequired();
            Property(p => p.DocumentId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StoreDocMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StoreDoc&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StoreDoc trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class StoreDocMapSqlServer : EntityTypeConfiguration<StoreDoc>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StoreDocMapSqlServer()
        {
            ToTable("store_doc");
            Property(p => p.StoreId).IsRequired();
            Property(p => p.DocumentId).HasColumnType("uniqueidentifier").IsRequired();
        }
    }
    
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StoreDocMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StoreDoc&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StoreDoc trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class StoreDocMapOracle : EntityTypeConfiguration<StoreDoc>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StoreDocMapOracle()
        {
            
        }
    }
}
