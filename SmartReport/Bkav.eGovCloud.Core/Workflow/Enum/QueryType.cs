namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : QueryType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 211212
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Định nghĩa các kiểu không gian cán bộ được phép cấu hình cho từng nút trong quy trình bàn giao </para>
    /// (CuongNT@bkav.com - 211212)
    /// </summary>
    public enum QueryType
    {
        /// <summary>
        /// Theo cán bộ
        /// </summary>
        TheoCanBo,

        /// <summary>
        /// Theo cấp trên cây phòng ban
        /// </summary>
        TheoCap,

        /// <summary>
        /// Theo vị trí tương đối với một node trên cây phòng ban
        /// </summary>
        TheoViTri,

        /// <summary>
        /// Theo quan hệ tương đối với một node khác trong quy trình
        /// </summary>
        TheoQuanHe
    }
}
