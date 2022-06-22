namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : AnticipateType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 180214
    /// Author      : TrungVH
    /// Description : Loại dự kiến
    /// </summary>
    public enum AnticipateType
    {
        /// <summary>
        /// Debug
        /// </summary>
        YKien = 1,

        /// <summary>
        /// Information
        /// </summary>
        DungXuLy = 2,

        /// <summary>
        /// Warning
        /// </summary>
        Chuyen = 3,

        /// <summary>
        /// Error
        /// </summary>
        PhatHanh = 4,

        /// <summary>
        /// Fatal
        /// </summary>
        KyDuyet = 5
    }
}
