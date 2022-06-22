using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.DataAccess.Mapping.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WorkflowMapMySql - public - DAL
    /// Access Modifiers: 
    ///     * Inherit : EntityTypeConfiguration&lt;Workflow&gt;
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Mapping tương ứng với bảng Workflow trong CSDL MySql
    /// </summary>
    [ComVisible(false)]
    public class VoiceTextMapMySql : EntityTypeConfiguration<VoiceText>
    {
        ///<summary>
        /// Mapping property
        ///</summary>
        public VoiceTextMapMySql()
        {
            ToTable("voicetext");
        }
    }
    
}
