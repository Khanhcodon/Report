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
    public class CalendarDetailMapMySql : EntityTypeConfiguration<CalendarDetail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CalendarDetailMapMySql()
        {
            ToTable("calendar_detail");
            HasKey(p => p.CalendarDetailId);
            Property(p => p.CalendarId).HasColumnType("int").IsRequired();
            Property(p => p.Content).HasColumnType("varchar");
            Property(p => p.Department).HasColumnType("varchar");
            Property(p => p.Joined).HasColumnType("varchar");
            Property(p => p.Note).HasColumnType("varchar");
            Property(p => p.UserPrimary).HasColumnType("varchar");
            Property(p => p.UserVieweds).HasColumnType("varchar");
            Property(p => p.Attachments).HasColumnType("varchar");
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
    public class CalendarDetailMapSqlServer : EntityTypeConfiguration<CalendarDetail>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CalendarDetailMapSqlServer()
        {
           
            ToTable("calendar_detail");
            HasKey(p => p.CalendarDetailId);
            Property(p => p.CalendarId).HasColumnType("int").IsRequired();
            Property(p => p.Content).HasColumnType("varchar");
            Property(p => p.Department).HasColumnType("varchar");
            Property(p => p.Joined).HasColumnType("varchar");
            Property(p => p.Note).HasColumnType("varchar");
            Property(p => p.UserPrimary).HasColumnType("varchar");
            Property(p => p.UserVieweds).HasColumnType("varchar");
            Property(p => p.Attachments).HasColumnType("varchar");
        }
    }
}
