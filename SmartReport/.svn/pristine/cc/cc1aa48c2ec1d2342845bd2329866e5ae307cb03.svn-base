using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StoreMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Store&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Store trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class StoreMapMySql : EntityTypeConfiguration<Store>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StoreMapMySql()
        {
            ToTable("store");
            Property(p => p.StoreName).IsRequired().HasMaxLength(1000).HasColumnType("varchar");
            Property(p => p.Description).HasMaxLength(255).HasColumnType("varchar");
            Property(p => p.CategoryBusinessId).IsRequired();
            Property(p => p.UserViewIds).HasColumnType("text");
            Property(p => p.DocFieldIds).HasColumnType("text");
            Ignore(p => p.CodeIds);
            Ignore(p => p.ListUserViewIds);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StoreMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Store&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Store trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class StoreMapSqlServer : EntityTypeConfiguration<Store>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StoreMapSqlServer()
        {
            ToTable("store");
            Property(p => p.StoreName).IsRequired().HasMaxLength(1000);
            Property(p => p.Description).HasMaxLength(255);
            Property(p => p.CategoryBusinessId).IsRequired();
            //Property(p => p.UserViewIds).HasColumnType("ntext");
            //Property(p => p.DocFieldIds).HasColumnType("ntext");
            Ignore(p => p.CodeIds);
            Ignore(p => p.ListUserViewIds);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StoreMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Store&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Store trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class StoreMapOracle : EntityTypeConfiguration<Store>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StoreMapOracle()
        {
            
        }
    }
}
