using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WardMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Ward&gt;
    /// Create Date : 221013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng Ward trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class VoteMapMySql : EntityTypeConfiguration<Vote>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public VoteMapMySql()
        {
            ToTable("vote");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WardMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Ward&gt;
    /// Create Date : 221013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng Ward trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class VoteMapSqlServer : EntityTypeConfiguration<Ward>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public VoteMapSqlServer()
        {
            ToTable("Vote");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WardMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Ward&gt;
    /// Create Date : 171013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng Ward trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class VoteMapOracle : EntityTypeConfiguration<Ward>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public VoteMapOracle()
        {
            
        }
    }
}
