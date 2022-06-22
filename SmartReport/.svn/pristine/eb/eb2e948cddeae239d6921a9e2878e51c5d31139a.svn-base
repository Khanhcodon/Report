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
    /// Class : SignatureMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Signature&gt;
    /// Create Date : 140514
    /// Author      : HopCv
    /// Description : Mapping tương ứng với bảng Signature trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class SignatureMapMySql : EntityTypeConfiguration<Signature>
    {
        /// <summary>
        /// Mapping property
        /// </summary>
        public SignatureMapMySql()
        {
            ToTable("signature");
            HasKey(p => p.SignatureId);
            Property(p => p.SignatureName).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            Property(p => p.SignaturePosition);
            Property(p => p.SearchWord).HasColumnType("varchar").HasMaxLength(250);
            Property(p => p.IsTypeImage).HasColumnType("bit");
            Property(p => p.Image).HasColumnType("longtext");
            Property(p => p.IsDispplayCertificate).HasColumnType("bit");
            Property(p => p.IsFindType).HasColumnType("bit");
            Property(p => p.ImageExtension).HasColumnType("varchar").HasMaxLength(250);
            Property(p => p.UserId);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SignatureMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Signature&gt;
    /// Create Date : 140514
    /// Author      : HopCv
    /// Description : Mapping tương ứng với bảng Signature trong CSDL Sql Server
    /// </summary>
    [ComVisible(false)]
    public class SignatureMapSqlServer : EntityTypeConfiguration<Signature>
    {
        /// <summary>
        /// Mapping property
        /// </summary>
        public SignatureMapSqlServer()
        {
            ToTable("signature");
            HasKey(p => p.SignatureId);
            Property(p => p.SignatureName).HasMaxLength(255).IsRequired();
            //Property(p => p.SignaturePosition);
            Property(p => p.SearchWord).HasMaxLength(255);
            //Property(p => p.IsTypeImage).HasColumnType("bit");
            //Property(p => p.Image).HasColumnType("ntext");
            //Property(p => p.IsDispplayCertificate).HasColumnType("bit");
            //Property(p => p.IsFindType).HasColumnType("bit");
            Property(p => p.ImageExtension).HasMaxLength(250);
            //Property(p => p.UserId);
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : SignatureMapOracle - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Signature&gt;
    /// Create Date : 140514
    /// Author      : Hopcv
    /// Description : Mapping tương ứng với bảng Signature trong CSDL Oracle
    /// </summary>
    [ComVisible(false)]
    public class SignatureMapOracle : EntityTypeConfiguration<Signature>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public SignatureMapOracle()
        {

        }
    }
}
