using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : VoteDetail - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Ward&gt;
    /// Create Date : 221013
    /// Author      : DungNVl
    /// Description : Mapping tương ứng với bảng VoteDetail trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class VoteDetailMapMySql : EntityTypeConfiguration<VoteDetail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public VoteDetailMapMySql()
        {
            ToTable("votedetail");
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
    public class VoteDetailMapSqlServer : EntityTypeConfiguration<VoteDetail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public VoteDetailMapSqlServer()
        {
            ToTable("votedetail");
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
    public class VoteDetailMapOracle : EntityTypeConfiguration<VoteDetail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public VoteDetailMapOracle()
        {
            
        }
    }
}
