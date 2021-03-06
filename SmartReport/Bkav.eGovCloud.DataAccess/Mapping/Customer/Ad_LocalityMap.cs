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
    public class Ad_LocalityMapMySql : EntityTypeConfiguration<Ad_Locality>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public Ad_LocalityMapMySql()
        {
            ToTable("dim_ad_locality");
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
    public class Ad_LocalityMapSqlServer : EntityTypeConfiguration<Ad_Locality>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public Ad_LocalityMapSqlServer()
        {
            ToTable("dim_ad_locality");
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
    public class Ad_LocalityMapOracle : EntityTypeConfiguration<Increase>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public Ad_LocalityMapOracle()
        {

        }
    }

}
