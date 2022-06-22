using Bkav.eGovCloud.Entities.Customer;
using System.Data.Entity.ModelConfiguration;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlTemplateMapMySql : EntityTypeConfiguration<SqlTemplate>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SqlTemplateMapMySql()
        {
            ToTable("sql_template");
            HasKey(p => p.TemplateId);
        }
    }
}
