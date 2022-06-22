namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : LogType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270812
    /// Author      : TrungVH
    /// Description : Kiểu nhật ký (mức độ nhật ký)
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// Debug
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Information
        /// </summary>
        Information = 2,

        /// <summary>
        /// Warning
        /// </summary>
        Warning = 3,

        /// <summary>
        /// Error
        /// </summary>
        Error = 4,

        /// <summary>
        /// Fatal
        /// </summary>
        Fatal = 5
    }
}
