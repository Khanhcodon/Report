using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    ///<para></para> Bkav Corp. - BSO - eGov - eOffice team
    ///<para></para> Project: eGov Cloud v1.0
    ///<para></para> Enum : Scope - public - Entity
    ///<para></para> Access Modifiers: 
    ///<para></para> Create Date : 09092014
    ///<para></para> Author      : QuangP
    ///<para></para> Description : Scope của Api
    /// </summary>
    [DataContract]
    public enum Scope
    {
        /// <summary>
        /// API docfield
        /// </summary>
        [EnumMember]
        [Description("Docfield")]
        Docfield = 1,

        /// <summary>
        /// API Office
        /// </summary>
        [EnumMember]
        [Description("Office")]
        Office = 2,

        /// <summary>
        /// API Law
        /// </summary>
        [EnumMember]
        [Description("Law")]
        Law = 3,

        /// <summary>
        /// API Level
        /// </summary>
        [EnumMember]
        [Description("Level")]
        Level = 4,

        /// <summary>
        /// API DocType
        /// </summary>
        [EnumMember]
        [Description("Doctype")]
        Doctype = 5,

        /// <summary>
        /// API DoctyeLaw
        /// </summary>
        [EnumMember]
        [Description("DoctyeLaw")]
        DoctyeLaw = 6,

        /// <summary>
        /// API Guide
        /// </summary>
        [EnumMember]
        [Description("Guide")]
        Guide = 7,

        /// <summary>
        /// API Question
        /// </summary>
        [EnumMember]
        [Description("Question")]
        Question = 8,

        /// <summary>
        /// API Question
        /// </summary>
        [EnumMember]
        [Description("FormGroup")]
        FormGroup = 9,

        /// <summary>
        /// API Question
        /// </summary>
        [EnumMember]
        [Description("Form")]
        Form = 10,

        /// <summary>
        /// API File
        /// </summary>
        [EnumMember]
        [Description("File")]
        File = 11,

        /// <summary>
        /// API DoctypeLaw
        /// </summary>
        [EnumMember]
        [Description("DoctypeLaw")]
        DoctypeLaw = 12,

        /// <summary>
        /// API Scope
        /// </summary>
        [EnumMember]
        [Description("Scope")]
        Scope = 13,

        /// <summary>
        /// API Function
        /// </summary>
        [EnumMember]
        [Description("Functions")]
        Function = 14,

        /// <summary>
        /// API Document
        /// </summary>
        [EnumMember]
        [Description("Document")]
        Document = 15,

        /// <summary>
        /// API People
        /// </summary>
        [EnumMember]
        [Description("People")]
        People = 16
    }
}
