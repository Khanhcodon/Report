using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : JobTitlesMapMySql - public - DAL
    /// Access Modifiers:
    ///     * Inherit : EntityTypeConfiguration&lt;JobTitles&gt;
    /// Create Date : 121012
    /// Author      : GiagnPN
    /// Description : Mapping tương ứng với bảng JobTitles trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class JobTitlesMapMySql : EntityTypeConfiguration<JobTitles>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public JobTitlesMapMySql()
        {
            ToTable("jobtitles");
            Property(p => p.JobTitlesName).IsRequired().HasMaxLength(64).HasColumnType("varchar");
            Property(p => p.IsApproved).IsRequired().HasColumnType("bit");
            Property(p => p.IsClerical).IsRequired().HasColumnType("bit");
            Property(p => p.CanGetDocumentCome).IsRequired().HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : JobTitlesMapSqlServer - public - DAL
    /// Access Modifiers:
    ///     * Inherit : EntityTypeConfiguration&lt;JobTitles&gt;
    /// Create Date : 121012
    /// Author      : GiagnPN
    /// Description : Mapping tương ứng với bảng JobTitles trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class JobTitlesMapSqlServer : EntityTypeConfiguration<JobTitles>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public JobTitlesMapSqlServer()
        {
            ToTable("jobtitles");
            Property(p => p.JobTitlesName).IsRequired().HasMaxLength(64);
            Property(p => p.IsApproved).IsRequired();
            Property(p => p.IsClerical).IsRequired();//.HasColumnType("bit");
            Property(p => p.CanGetDocumentCome).IsRequired();//.HasColumnType("bit");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : JobTitlesMapOracle - public - DAL
    /// Access Modifiers:
    ///     * Inherit : EntityTypeConfiguration&lt;JobTitles&gt;
    /// Create Date : 121012
    /// Author      : GiagnPN
    /// Description : Mapping tương ứng với bảng JobTitles trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class JobTitlesMapOracle : EntityTypeConfiguration<JobTitles>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public JobTitlesMapOracle()
        {
        }
    }
}