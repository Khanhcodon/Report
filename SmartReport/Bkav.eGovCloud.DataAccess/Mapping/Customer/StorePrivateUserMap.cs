using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateUserMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StorePrivateUser&gt;
    /// Create Date : 071013
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StorePrivateUser trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class StorePrivateUserMapMySql : EntityTypeConfiguration<StorePrivateUser>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StorePrivateUserMapMySql()
        {
            ToTable("storeprivate_user");
            Property(p => p.StorePrivateId).IsRequired();
            Property(p => p.UserId);
            Property(p => p.DepartmentId);
            Property(p => p.DepartmentIdExt);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateUserMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StorePrivateUser&gt;
    /// Create Date : 071013
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StorePrivateUser trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class StorePrivateUserMapSqlServer : EntityTypeConfiguration<StorePrivateUser>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StorePrivateUserMapSqlServer()
        {
            ToTable("storeprivate_user");
            Property(p => p.StorePrivateId).IsRequired();
            Property(p => p.UserId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : StorePrivateUserMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;StorePrivateUser&gt;
    /// Create Date : 071013
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng StorePrivateUser trong CSDL Oracal
    /// </summary>
    [ComVisible(false)]
    public class StorePrivateUserMapOracle : EntityTypeConfiguration<StorePrivateUser>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public StorePrivateUserMapOracle()
        {

        }
    }
}
