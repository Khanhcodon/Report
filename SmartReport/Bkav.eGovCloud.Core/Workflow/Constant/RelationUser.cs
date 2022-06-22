using System.Collections.Generic;

namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : PositionUser - public - Entity
    /// Access Modifiers: 
    /// Create Date : 181212
    /// Author      : GiangPN
    /// </author>
    /// <summary>
    /// <para>Định nghĩa các quan hệ của user giữa các node trong quy trình</para>
    /// (GiangPN@bkav.com - 181212)
    /// </summary>
    public static class RelationUser
    {
        /// <summary>
        /// <para>Các user ở cùng 1 đơn vị(bộ phận) ở cấp gần nhất. (@0)</para>
        /// <para>Ví dụ: Phòng A có 2 nhân viên A và B thì A và B là quan hệ cùng 1 đơn vị</para>
        /// </summary>
        public static readonly string SameUnit = RelationType.CanBoCungDonVi.ToString();

        /// <summary>
        /// <para>Các user cùng cấp(quan hệ ngang hàng) biểu thị trên cây phòng ban (@~)</para>
        /// <para>Ví dụ: Phòng A có nhân viên B, Phòng C có nhân viên D thì B và D là cùng cấp.</para>
        /// </summary>
        public static readonly string PeerNode = RelationType.CanBoCungCap.ToString();

        /// <summary>
        /// Các user cùng node cha (@^)
        /// </summary>
        public static readonly string SameParentNode = RelationType.CanBoCungNutCha.ToString();

        /// <summary>
        /// Quan hệ cấp dưới (@1)
        /// </summary>
        public static readonly string Subordinate = RelationType.CanBoCapDuoi.ToString();

        /// <summary>
        /// Quan hệ cấp trên (@-1)
        /// </summary>
        public static readonly string Superiors = RelationType.CanBoCapTren.ToString();

        /// <summary>
        /// Danh sách các quan hệ
        /// </summary>
        public static Dictionary<string, string> AllRelationUsers = new Dictionary<string, string>
        {
            {SameUnit,            "Cùng đơn vị"},
            {Subordinate,         "Cấp dưới"},
            {Superiors,           "Cấp trên"},
            {SameParentNode,      "Cùng node cha"},
            {PeerNode,             "Node ngang hàng"}
        };
    }
}
