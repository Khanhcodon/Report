using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTypeStoreMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocTypeStore&gt;
    /// Create Date : 270912
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocTypeStore trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class DocTypeStoreMapMySql : EntityTypeConfiguration<DocTypeStore>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTypeStoreMapMySql()
        {
            ToTable("doctype_store");
            Property(p => p.DocTypeId).IsRequired();
            Property(p => p.StoreId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTypeStoreMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocTypeStore&gt;
    /// Create Date : 270912
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocTypeStore trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class DocTypeStoreMapSqlServer : EntityTypeConfiguration<DocTypeStore>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTypeStoreMapSqlServer()
        {
            ToTable("doctype_store");
            Property(p => p.DocTypeId).HasColumnType("uniqueidentifier").IsRequired();
            Property(p => p.StoreId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocTypeStoreMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;DocTypeStore&gt;
    /// Create Date : 270912
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng DocTypeStore trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class DocTypeStoreMapOracle : EntityTypeConfiguration<DocTypeStore>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DocTypeStoreMapOracle()
        {

        }
    }
}
