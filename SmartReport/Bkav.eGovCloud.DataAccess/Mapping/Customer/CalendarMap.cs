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
    /// Class : AddressMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Address&gt;
    /// Create Date : 15082013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng Address trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class CalendarMapMySql : EntityTypeConfiguration<Calendar>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CalendarMapMySql()
        {
            ToTable("calendar");
            HasKey(p => p.CalendarId);
            Property(p => p.Title).HasColumnType("varchar").IsRequired();
            Property(p => p.Place).HasColumnType("varchar").IsRequired();
            Property(p => p.DepartmentIdExt).HasColumnType("varchar").IsRequired();
            Property(p => p.BeginTime).HasColumnType("datetime");
            Property(p => p.IsAccepted).HasColumnType("bit");
            Property(p => p.IsPrivate).HasColumnType("bit");            
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AddressMapSqlServer - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Address&gt;
    /// Create Date : 15082013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng Address trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class CalendarMapSqlServer : EntityTypeConfiguration<Calendar>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CalendarMapSqlServer()
        {
            ToTable("calendar");
            HasKey(p => p.CalendarId);
            Property(p => p.Title).HasColumnType("varchar").IsRequired();
            Property(p => p.Place).HasColumnType("varchar").IsRequired();
            Property(p => p.BeginTime).HasColumnType("datetime");
            Property(p => p.IsAccepted).IsRequired();
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AddressMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Address&gt;
    /// Create Date : 15082013
    /// Author      : DungHV
    /// Description : Mapping tương ứng với bảng Address trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class CalendarMapOracle : EntityTypeConfiguration<Calendar>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CalendarMapOracle()
        {
        }
    }
}
