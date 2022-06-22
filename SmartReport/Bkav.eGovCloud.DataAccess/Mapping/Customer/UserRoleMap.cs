using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserRoleMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserRole&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng UserRole trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class UserRoleMapMySql : EntityTypeConfiguration<UserRole>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserRoleMapMySql()
        {
            ToTable("user_role");
            Property(p => p.UserId).IsRequired();
            Property(p => p.RoleId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserRoleMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserRole&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng UserRole trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class UserRoleMapSqlServer : EntityTypeConfiguration<UserRole>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserRoleMapSqlServer()
        {
            ToTable("user_role");
            Property(p => p.UserId).IsRequired();
            Property(p => p.RoleId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : UserRoleMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;UserRole&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng UserRole trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class UserRoleMapOracle : EntityTypeConfiguration<UserRole>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public UserRoleMapOracle()
        {
            
        }
    }
}
