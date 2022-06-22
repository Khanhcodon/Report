using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Bkav.eGovCloud.Entities.Customer;
using System.Runtime.InteropServices;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CheckInfringeMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;CheckInfringe&gt;
    /// Create Date : 15082013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng CheckInfringe trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class CheckInfringeMapMySql : EntityTypeConfiguration<CheckInfringe>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CheckInfringeMapMySql()
        {
            ToTable("check_infringe");
            HasKey(p => p.CheckInfringeId);
            Property(p => p.Date).IsRequired();
            Property(p => p.InfringeUserId).IsRequired();
            Property(p => p.CreateUserId).IsRequired();
            Property(p => p.RateEmployeeId).IsRequired();
            Property(p => p.Cause).HasColumnType("varchar").HasMaxLength(1000).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CheckInfringeMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;CheckInfringe&gt;
    /// Create Date : 15082013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng CheckInfringe trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class CheckInfringeMapSqlServer : EntityTypeConfiguration<CheckInfringe>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CheckInfringeMapSqlServer()
        {
            ToTable("check_infringe");
            Property(p => p.Date).IsRequired();
            Property(p => p.InfringeUserId).IsRequired();
            Property(p => p.CreateUserId).IsRequired();
            Property(p => p.RateEmployeeId).IsRequired();
            Property(p => p.Cause).HasColumnType("nvarchar").HasMaxLength(1000).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CheckInfringeMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;CheckInfringe&gt;
    /// Create Date : 15082013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng CheckInfringe trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class CheckInfringeMapOracle : EntityTypeConfiguration<CheckInfringe>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CheckInfringeMapOracle()
        {
        }
    }
}
