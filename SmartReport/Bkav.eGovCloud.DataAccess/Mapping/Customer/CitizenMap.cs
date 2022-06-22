using System.Data.Entity.ModelConfiguration;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// CitizenMapMySql
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class CitizenMapMySql : EntityTypeConfiguration<Citizen>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public CitizenMapMySql()
        {
            ToTable("citizen");
            HasKey(u => u.Id);
            Property(u => u.Account).HasColumnType("varchar").HasMaxLength(100);
            Property(u => u.PasswordHash).HasColumnType("binary");
            Property(u => u.PasswordSalt).HasColumnType("binary");
            Property(u => u.CitizenName).HasColumnType("varchar").HasMaxLength(400);
            Property(u => u.FirstName).HasColumnType("varchar").HasMaxLength(50);
            Property(u => u.LastName).HasColumnType("varchar").HasMaxLength(50);
            Property(u => u.Phone).HasColumnType("varchar").HasMaxLength(20);
            Property(u => u.Email).HasColumnType("varchar").HasMaxLength(50);
            Property(u => u.IdentityCard).HasColumnType("varchar").HasMaxLength(12);
            Property(u => u.DateOfIssue).HasColumnType("date");
            Property(u => u.PlaceOfIssue).HasColumnType("varchar").HasMaxLength(100);
            Property(u => u.IsActivated).HasColumnType("bit");
        }
    }

    /// <summary>
    /// CitizenMapSqlServer
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class CitizenMapSqlServer : EntityTypeConfiguration<Citizen>
    {
        /// <summary>
        /// constructer
        /// </summary>
        public CitizenMapSqlServer()
        {
            ToTable("citizen");
            HasKey(u => u.Id);
            Property(u => u.Account).HasColumnType("varchar").HasMaxLength(100);
            Property(u => u.PasswordHash).HasColumnType("binary");
            Property(u => u.PasswordSalt).HasColumnType("binary");
            Property(u => u.CitizenName).HasMaxLength(100);
            Property(u => u.FirstName).HasMaxLength(50);
            Property(u => u.LastName).HasMaxLength(50);
            Property(u => u.Phone).HasColumnType("varchar").HasMaxLength(20);
            Property(u => u.Email).HasColumnType("varchar").HasMaxLength(50);
            Property(u => u.IdentityCard).HasColumnType("varchar").HasMaxLength(12);
            Property(u => u.DateOfIssue).HasColumnType("date");
            Property(u => u.PlaceOfIssue).HasMaxLength(100);
            Property(u => u.IsActivated);//.HasColumnType("bit");
        }
    }
}