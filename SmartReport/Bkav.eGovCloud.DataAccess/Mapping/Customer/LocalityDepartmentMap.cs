using Bkav.eGovCloud.Entities.Customer;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActionLevelMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ActionLevel&gt;
    /// Create Date : 190620
    /// Author      : SuBD
    /// Description : Mapping tương ứng với bảng ActionLevel trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class LocalityDepartmentValueMapMySql : EntityTypeConfiguration<LocalityDepartment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public LocalityDepartmentValueMapMySql()
        {
            ToTable("dim_localitydepartmentvalue");
            Property(p => p.LocalityDepartmentId).IsRequired();
            Property(p => p.LocalityId).IsRequired();
            Property(p => p.DepartmentId).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ActionLevelMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;ActionLevel&gt;
    /// Create Date : 190620
    /// Author      : SuBD
    /// Description : Mapping tương ứng với bảng ActionLevel trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class LocalityDepartmentValueMapSqlServer : EntityTypeConfiguration<LocalityDepartment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public LocalityDepartmentValueMapSqlServer()
        {
            ToTable("dim_localitydepartmentvalue");
            Property(p => p.LocalityDepartmentId).IsRequired();
            Property(p => p.LocalityId).IsRequired();
            Property(p => p.DepartmentId).IsRequired();
        }
    }
}
