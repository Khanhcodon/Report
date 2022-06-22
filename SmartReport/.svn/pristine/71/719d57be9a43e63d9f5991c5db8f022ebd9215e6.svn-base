using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : CommonCommentMapMySql - public - mapping </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Map tương ứng với bảng commoncomment trong db </para>
    /// <para> ( TienBV@bkav.com - 13) </para>
    /// </summary>
    [ComVisible(false)]
    public class CommonCommentMapMySql : EntityTypeConfiguration<CommonComment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CommonCommentMapMySql()
        {
            ToTable("commoncomment");
            HasKey(p => p.CommonCommentId);
            Property(p => p.Content).IsRequired().HasColumnType("text");
            Property(p => p.UserId).IsRequired();
        }
    }

    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : CommonCommentMapSqlServer - public - Map </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 200213</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Map tương ứng với bảng commoncomment trong db </para>
    /// <para> ( TienBV@bkav.com - 200213) </para>
    /// </summary>
    [ComVisible(false)]
    public class CommonCommentMapSqlServer : EntityTypeConfiguration<CommonComment>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public CommonCommentMapSqlServer()
        {
            ToTable("commoncomment");
            HasKey(p => p.CommonCommentId);
            Property(p => p.Content).IsRequired();//.HasColumnType("ntext");
            Property(p => p.UserId).IsRequired();
        }
    }
}
