using System.ComponentModel;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    ///<para></para> Bkav Corp. - BSO - eGov - eOffice team
    ///<para></para> Project: eGov Cloud v1.0
    ///<para></para> Enum : Urgent - public - Entity
    ///<para></para> Access Modifiers: 
    ///<para></para> Create Date : 250214
    ///<para></para> Author      : TrungVH
    ///<para></para> Description : Độ khẩn
    /// </summary>
    public enum Urgent
    {
        /// <summary>
        /// Thường
        /// </summary>
        [Description("egovcloud.enum.urgent.thuong")]
        Thuong = 1,

        /// <summary>
        /// Khẩn
        /// </summary>
        [Description("egovcloud.enum.urgent.khan")]
        Khan = 2,
        
        /// <summary>
        /// Hỏa tốc
        /// </summary>
        [Description("egovcloud.enum.urgent.thuongkhan")]
        ThuongKhan = 3,

        /// <summary>
        /// Hỏa tốc
        /// </summary>
        [Description("egovcloud.enum.urgent.hoatoc")]
        HoaToc = 4
    }

    /// <summary>
    /// 
    /// </summary>
    public enum DatabaseRelationType
    {
        /// <summary>
        /// InnerJoin
        /// </summary>
        [Description("Inner Join")]
        InnerJoin = 1,

        /// <summary>
        /// LeftJoin
        /// </summary>
        [Description("Left Join")]
        LeftJoin = 3,

        /// <summary>
        /// RightJoin        /// </summary>
        [Description("Right Join")]
        RightJoin = 3,

        /// <summary>
        /// OuterJoin
        /// </summary>
        [Description("Outer Join")]
        OuterJoin = 4,

        /// <summary>
        /// OuterJoin
        /// </summary>
        [Description("Blend")]
        Blend = 5
    }
}
