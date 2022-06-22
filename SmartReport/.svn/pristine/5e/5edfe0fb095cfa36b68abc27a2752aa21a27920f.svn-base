using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunction - public - Entity</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 111212</para>
    /// <para>Author      : TrungVH</para>
    /// <para>Description : Entity tương ứng với bảng ProcessFunction trong CSDL, dùng để cấu hình các node trong cây văn bản đang xử lý trên trang chủ</para>
    /// </summary>
    public class ProcessFunction
    {
        //private ICollection<ListSetting> _listSettings;
        /// <summary>
        /// 
        /// </summary>
        public ProcessFunction()
        {
            ShowTotalInTreeType = (byte)DisplayTreeType.Unread;
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id function
        /// </summary>
        public int ProcessFunctionId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id function cha
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại function
        /// </summary>
        public int? ProcessFunctionTypeId { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập kho văn bản tương ứng
        ///// </summary>
        //public int ProcessFunctionGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên function
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Đường dẫn Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Màu chữ cho function
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query lấy ra các văn bản hồ sơ mới nhất
        /// </summary>
        public string QueryLatest { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query lấy ra các văn bản hồ sơ cũ hơn
        /// </summary>
        public string QueryOlder { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query lấy ra các văn bản hồ sơ đã bị xóa khỏi danh sách trước đó
        /// </summary>
        public string QueryItemRemove { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query tính tổng các văn bản, hồ sơ chưa được xem trong function
        /// </summary>
        public string QueryCountItemUnread { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query cho phân trang
        /// </summary>
        public string QueryPaging { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query tính tổng các văn bản, hồ sơ (dùng cho khi phân trang)
        /// </summary>
        public string QueryCountAllItems { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Màu chữ cho function
        /// </summary>
        public bool IsEnablePaging { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Trạng thái sử dụng
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thứ tự sắp xếp
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại function
        /// </summary>
        public virtual ProcessFunctionType ProcessFunctionType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kiểu ngày cho phép lọc trên danh sách)
        /// </summary>
        public string DateFilter { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kiểu ngày cho phép lọc trên danh sách để hiện thị lên toolbar
        /// </summary>
        public string DateFilterView { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Trạng thái lọc theo hạn xử lý
        /// </summary>
        public bool IsOverdueFilter { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Trạng thái lọc theo ngày
        /// </summary>
        public bool IsDateFilter { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kiểu của văn bản, hồ sơ
        /// <para> HopCV</para>
        /// <para> createDate: 020414</para>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Thiết lập kiểu hiển thị văn bản chưa đọc trên cây văn bản
        /// 0: hiển thị văn bản chưa đọc (default)
        /// 1: hiển thị tổng số văn bản
        /// 2: hiển thị số văn bản chưa đọc/ tổng số văn bản
        /// </summary>
        public byte ShowTotalInTreeType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập có lấy ủy quyền vượt cấp hay không
        /// Note: trường nay chỉ dùng trên node cây văn bàn ủy quyền,
        ///      mục đích là lấy ra danh sách của những người ủy quyền cho những người ủy quyền
        /// </summary>
        public bool HasUyQuyen { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập node có chuyển theo lô hay không
        /// Note: contextmenu vào node thì hiển thị các hướng chuyển trùng nhau của các văn bản để bàn giao
        /// </summary>
        public bool HasTransferTheoLo { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nhóm cây
        /// </summary>
        public int TreeGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập file crystal để xuất danh sách văn bản ra file (word, excell)
        /// </summary>
        public string ExportFileConfig { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị xác định có xuất danh sách văn bản ra file (word, excell)
        /// </summary>
        public bool HasExportFile { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn lấy dữ liệu hiển thị ra file
        /// </summary>
        public string QueryExportDataToFile { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id ColumnSetting
        /// </summary>
        public int DocColumnSettingId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id ColumnSetting
        /// </summary>
        public int PermissionSettingId { get; set; }

    }
}

