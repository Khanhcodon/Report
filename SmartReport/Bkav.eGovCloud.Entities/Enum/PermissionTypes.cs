using System;
using System.ComponentModel;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : Permission - public - Entity
    /// Access Modifiers: 
    /// Create Date : 060913
    /// Author      : DungHV
    /// Description : quyền đối với văn bản
    /// </summary>
    [Flags]
    public enum PermissionTypes
    {
        /// <summary>
        /// Khởi tạo văn bản
        /// </summary>
        [Description("egovcloud.enum.permissiontypes.ktao")]
        KTao = 0x00000001,

        /// <summary>
        ///Xử lý văn bản
        /// </summary>
        [Description("egovcloud.enum.permissiontypes.xly")]
        XLy = 0x00000002
    }
}
