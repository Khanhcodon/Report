using System.ComponentModel;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    ///
    /// </summary>
    public enum AddressGroupStatus
    {
        /// <summary>
        ///
        /// </summary>
        [Description("egovcloud.enum.addressgroupstatus.all")]
        All = 0,

        /// <summary>
        ///
        /// </summary>
        [Description("egovcloud.enum.addressgroupstatus.any")]
        Any = 1,

        /// <summary>
        ///
        /// </summary>
        [Description("egovcloud.enum.addressgroupstatus.empty")]
        Empty = 3,

        /// <summary>
        ///
        /// </summary>
        [Description("egovcloud.enum.actionlevel.levelfour")]
        LevelFour = 4
    }
}