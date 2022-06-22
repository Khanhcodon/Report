using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : OtpMapMySql - public - DAL
    /// Access Modifiers:
    /// Create Date : 06062017
    /// Author      : QuiBQ
    /// Description : Mapping tương ứng với bảng Otp trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class OtpMapMySql : EntityTypeConfiguration<Otp>
    {
        /// <summary>
        /// Maping MySQL
        /// </summary>
        public OtpMapMySql(){
            ToTable("otp");
            Property(o => o.Content).IsRequired().HasMaxLength(250).HasColumnType("varchar");
            Property(o => o.Email).IsRequired().HasMaxLength(250).HasColumnType("varchar");
            Property(o => o.Sms).IsRequired().HasMaxLength(250).HasColumnType("varchar");
            Property(o => o.Status).IsRequired().HasColumnType("bit");
            Property(o => o.ActivedCode).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            Property(o => o.ActivedUrl).HasMaxLength(250).HasColumnType("varchar");
            Property(o => o.DateCreated).HasColumnType("datetime");
            Property(o => o.DateLimit).HasColumnType("datetime");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : OtpMapMySql - public - DAL
    /// Access Modifiers:
    /// Create Date : 06062017
    /// Author      : QuiBQ
    /// Description : Mapping tương ứng với bảng Otp trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class OtpMapSqlServer : EntityTypeConfiguration<Otp>
    {
        /// <summary>
        /// Maping property SqlServer
        /// </summary>
        public OtpMapSqlServer(){
            ToTable("otp");
            Property(o => o.Content).IsRequired().HasMaxLength(250).HasColumnType("varchar");
            Property(o => o.Email).IsRequired().HasMaxLength(250).HasColumnType("varchar");
            Property(o => o.Sms).IsRequired().HasMaxLength(250).HasColumnType("varchar");
            Property(o => o.Status).IsRequired().HasColumnType("bit");
            Property(o => o.ActivedCode).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            Property(o => o.ActivedUrl).HasMaxLength(250).HasColumnType("varchar");
            Property(o => o.DateCreated).HasColumnType("datetime");
            Property(o => o.DateLimit).HasColumnType("datetime");
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : OtpMapMySql - public - DAL
    /// Access Modifiers:
    /// Create Date : 06062017
    /// Author      : QuiBQ
    /// Description : Mapping tương ứng với bảng Otp trong CSDL SqlServer
    /// </summary>
    [ComVisible(false)]
    public class OtpMapOracle : EntityTypeConfiguration<Otp>
    {
        /// <summary>
        /// Maping property oracle
        /// </summary>
        public OtpMapOracle()
        {
        }
    }
}
