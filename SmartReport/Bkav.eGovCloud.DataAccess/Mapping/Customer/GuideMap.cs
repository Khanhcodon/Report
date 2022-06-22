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
    public class GuideMapMySql : EntityTypeConfiguration<Guide> 
    {
        /// <summary>
        /// 
        /// </summary>
        public GuideMapMySql()
        {
            ToTable("guide");
            HasKey(x => x.GuideId);
            Property(x => x.Name).HasColumnType("varchar").HasMaxLength(200).IsRequired();
            Property(x => x.Url).HasColumnType("varchar").HasMaxLength(500).IsRequired();
            Property(x => x.Content).HasColumnType("text");
        }
    }


    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class GuideMapSqlServer : EntityTypeConfiguration<Guide>
    {
        /// <summary>
        /// 
        /// </summary>
        public GuideMapSqlServer()
        {
            ToTable("guide");
            HasKey(x => x.GuideId);
            Property(x => x.Name).HasMaxLength(200).IsRequired();
            Property(x => x.Url).HasColumnType("varchar").HasMaxLength(500).IsRequired();
            Property(x => x.Content).HasColumnType("ntext");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComVisible(false)]
    public class  GuideMapOracle : EntityTypeConfiguration<Guide>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public GuideMapOracle()
        {

        }
    }
}
