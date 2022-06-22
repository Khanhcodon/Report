using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov </para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionAndFilterMapMySql - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ProcessFunctionAndFilter&gt;</para>
    /// <para>Create Date : 21/01/2015</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : Mapping tương ứng với bảng processfunction_processfunctionfilter trong CSDL MySql</para>
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionAndFilterMapMySql : EntityTypeConfiguration<ProcessFunctionAndFilter>
    {
        /// <summary>
        /// 
        /// </summary>
        public ProcessFunctionAndFilterMapMySql()
        {
            ToTable("processfunction_processfunctionfilter");
            HasKey(c => c.ProcessFunctionAndFilterId);
            Property(c => c.ProcessFunctionFilterId).IsRequired();
            Property(c => c.ProcessFunctionId).IsRequired();
        }
    }

    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov </para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionAndFilterMapSqlServer - public - DAL</para>
    /// <para>Access Modifiers: </para>
    /// <para>    * Inherit : EntityTypeConfiguration&lt;ProcessFunctionAndFilter&gt;</para>
    /// <para>Create Date : 21/01/2015</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : Mapping tương ứng với bảng processfunction_filter trong CSDL SqlServer</para>
    /// </summary>
    [ComVisible(false)]
    public class ProcessFunctionAndFilterMapSqlServer : EntityTypeConfiguration<ProcessFunctionAndFilter>
    {
        /// <summary>
        /// 
        /// </summary>
        public ProcessFunctionAndFilterMapSqlServer()
        {
            ToTable("processfunction_processfunctionfilter");
            HasKey(c => c.ProcessFunctionAndFilterId);
            Property(c => c.ProcessFunctionFilterId).IsRequired();
            Property(c => c.ProcessFunctionId).IsRequired();
        }
    }
}
