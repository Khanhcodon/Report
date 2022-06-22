using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : CalendarResource - public - mapping </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Map tương ứng với bảng CalendarResource trong db </para>
    /// <para> ( TienBV@bkav.com - 13) </para>
    /// </summary>
    [ComVisible(false)]
    public class CalendarResourceMapMySql : EntityTypeConfiguration<CalendarResource>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CalendarResourceMapMySql()
        {
            ToTable("calendar_resource");
            HasKey(p => p.CalendarResourceId);
            Property(p => p.Name).IsRequired().HasColumnType("varchar");
            Property(p => p.UserId).IsRequired();
        }
    }

    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : CalendarResource - public - Map </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Map tương ứng với bảng CalendarResource trong db </para>
    /// <para> ( TienBV@bkav.com - 200213) </para>
    /// </summary>
    [ComVisible(false)]
    public class CalendarResourceMapSqlServer : EntityTypeConfiguration<CalendarResource>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CalendarResourceMapSqlServer()
        {
            ToTable("CalendarResource");
            HasKey(p => p.CalendarResourceId);
            Property(p => p.Name).IsRequired().HasColumnType("varchar");
            Property(p => p.UserId).IsRequired();
        }
    }
}
