namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : RelationType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 211212
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Định nghĩa các quan hệ của user giữa các node trong quy trình</para>
    /// (CuongNT@bkav.com - 211212)
    /// </summary>
    public enum RelationType
    {
        /// <summary>
        /// <para>@0 : Các user ở cùng 1 đơn vị(bộ phận) ở cấp gần nhất.</para>
        /// <para>Ví dụ: Phòng A có 2 nhân viên A và B thì A và B là quan hệ cùng 1 đơn vị</para>
        /// </summary>
        CanBoCungDonVi,

        /// <summary>
        /// <para>@~ : Các user cùng cấp(quan hệ ngang hàng) biểu thị trên cây phòng ban</para>
        /// <para>Ví dụ: Phòng A có nhân viên B, Phòng C có nhân viên D thì B và D là cùng cấp.</para>
        /// </summary>
        CanBoCungCap,

        /// <summary>
        /// @^ : Các user cùng node cha
        /// </summary>
        CanBoCungNutCha,

        /// <summary>
        /// @1 : Quan hệ cấp dưới
        /// </summary>
        CanBoCapDuoi,

        /// <summary>
        /// @-1 : Quan hệ cấp trên
        /// </summary>
        CanBoCapTren
    }
}
