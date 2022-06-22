using Bkav.eGovCloud.Entities.Customer;
using System.Data.Entity.ModelConfiguration;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class DataFieldMapMySql : EntityTypeConfiguration<DataField>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public DataFieldMapMySql()
        {
            ToTable("datafield");
            HasKey(p => p.DataFieldId);
        }
    }
}
