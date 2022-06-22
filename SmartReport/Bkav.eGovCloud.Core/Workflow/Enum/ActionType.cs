namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : ActionType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 181012
    /// Author      : TrungVH
    /// </author>
    /// <summary>
    /// <para>Loại hành động</para>
    /// (TrungVH@bkav.com - 181012)
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// Đường thẳng (Hành động từ node A -> node B)
        /// </summary>
        Line = 1,

        /// <summary>
        /// Vòng tròn (Hành động từ node A -> node A)
        /// </summary>
        Circle = 2,

        /// <summary>
        /// Xoay vòng (Hành động từ node A -> node B và ngược lại)
        /// </summary>
        Turnaround = 3
    }
}
