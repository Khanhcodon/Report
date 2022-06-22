using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Enum;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ColumnSetting - public - Entity</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 241115</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : Entity tương ứng với bảng ColumnSetting trong CSDL, dùng để cấu hình các node trong cây văn bản đang xử lý trên trang chủ</para>
    /// </summary>
    public class DocColumnSetting
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id ColumnSetting
        /// </summary>
        public int DocColumnSettingId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên ColumnSetting
        /// </summary>
        public string DocColumnSettingName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập  các cột sắp xếp
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách nhóm
        /// </summary>
        public string GroupColumn { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các cột hiển thị
        /// </summary>
        public string DisplayColumn { get; set; }

        /// <summary>
        /// ColumnSettingType
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ColumnSettingType TypeEnum
        {
            get
            {
                return (ColumnSettingType)Type;
            }
        }
    }
}
