using System.Collections.Generic;
using System.ComponentModel;

namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : PositionUser - public - Entity
    /// Access Modifiers: 
    /// Create Date : 091012
    /// Author      : GiangPN
    /// </author>
    /// <summary>
    /// <para>Định nghĩa các vị trí của user trong phòng ban đơn vị</para>
    /// (GiangPN@bkav.com - 091012)
    /// </summary>
    public static class PositionUser
    {
        /// <summary>
        /// Đơn vị hiện tại (id)
        /// </summary>
        [Description("Workflow.PositionUser.CurrentUnit.Description")]
        public static readonly string CurrentUnit = PositionType.DonViHienTai.ToString();

        /// <summary>
        /// Các đơn vị(bộ phận) cấp dưới (?)
        /// </summary>
        [Description("Workflow.PositionUser.SubUnit.Description")]
        public static readonly string SubUnit = PositionType.DonViCapDuoi.ToString();

        /// <summary>
        /// Tất cả các đơn vị(bộ phận) trực thuộc (*)
        /// </summary>
        [Description("Workflow.PositionUser.DependentUnit.Description")]
        public static readonly string DependentUnit = PositionType.DonViTrucThuoc.ToString();

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, string> AllPositionUsers = new Dictionary<string, string>
        {
            {CurrentUnit,        "Workflow.PositionUser.CurrentUnit.Description"},
            {SubUnit,            "Workflow.PositionUser.SubUnit.Description"},
            {DependentUnit,      "Workflow.PositionUser.DependentUnit.Description"}
        };
    }
}
