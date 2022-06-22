using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StoreCategoryMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StoreCategory&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StoreCategory trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class StoreCategoryMapMySql : EntityTypeConfiguration<StoreCategory>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StoreCategoryMapMySql()
        {
            ToTable("store_category");
            Property(p => p.StoreId).IsRequired();
            Property(p => p.CategoryId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StoreCategoryMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StoreCategory&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StoreCategory trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class StoreCategoryMapSqlServer : EntityTypeConfiguration<StoreCategory>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StoreCategoryMapSqlServer()
        {
            ToTable("store_category");
            Property(p => p.StoreId).IsRequired();
            Property(p => p.CategoryId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StoreCategoryMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StoreCategory&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StoreCategory trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class StoreCategoryMapOracle : EntityTypeConfiguration<StoreCategory>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StoreCategoryMapOracle()
        {
            
        }
    }
}
