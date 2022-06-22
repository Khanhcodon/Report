using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class SyncDoctypeMySql : EntityTypeConfiguration<SyncDocType>
    {
        /// <summary>
        /// 
        /// </summary>
        public SyncDoctypeMySql()
        {
            ToTable("syncdoctype");
            HasKey(x => x.Id);
            Property(x => x.InsideDocTypeId);
            Property(x => x.OutsideDocTypeId);
            //HasRequired(x => x.InsideDocType).WithMany(d => d.SyncDocTypes).HasForeignKey(d => d.InsideDocTypeId).WillCascadeOnDelete(true);
        }
    }


    /// <summary>
    /// 
    /// </summary>
   [ComVisible(false)]
    public class SyncDoctypeSqlServer : EntityTypeConfiguration<SyncDocType>
    {
       /// <summary>
       /// 
       /// </summary>
        public SyncDoctypeSqlServer()
        {
            ToTable("SyncDoctype");
            HasKey(x => x.Id);
            //Property(x => x.InsideDocTypeId);
            //Property(x => x.OutsideDocTypeId);
            //Ignore(x => x.InsideDocType);
            //HasRequired(x => x.InsideDocType).WithMany(d => d.SyncDocTypes).HasForeignKey(d => d.InsideDocTypeId).WillCascadeOnDelete(true);
        }
    }
}
