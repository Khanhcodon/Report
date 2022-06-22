using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ProcessFunctionModelValidator))]
    public class ProcessFunctionModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ProcessFunctionModel()
        {
            ShowTotalInTreeType = (byte)DisplayTreeType.Unread;
            HasTransferTheoLo = false;
            HasUyQuyen = false;
            IsEnablePaging = true;
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
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.ProcessFunctionTypeId.Label")]
        public int? ProcessFunctionTypeId { get; set; }

        //[LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.ProcessFunctionGroupId.Label")]
        //public int ProcessFunctionGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên function
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Đường dẫn Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query lấy ra các văn bản hồ sơ mới nhất
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.QueryLatest.Label")]
        public string QueryLatest { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query lấy ra các văn bản hồ sơ cũ hơn
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.QueryOlder.Label")]
        public string QueryOlder { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query lấy ra các văn bản hồ sơ đã bị xóa khỏi danh sách trước đó
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.QueryItemRemove.Label")]
        public string QueryItemRemove { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query tính tổng các văn bản, hồ sơ chưa được xem trong function
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.QueryCountItemUnread.Label")]
        public string QueryCountItemUnread { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query cho phân trang
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.QueryPaging.Label")]
        public string QueryPaging { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query tính tổng các văn bản, hồ sơ (dùng cho khi phân trang)
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.QueryCountAllItems.Label")]
        public string QueryCountAllItems { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Màu chữ cho function
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.Color.Label")]
        public string Color { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Màu chữ cho function
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.IsEnablePaging.Label")]
        public bool IsEnablePaging { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra function này cho phép lọc theo lĩnh vực và loại văn bản, hồ sơ
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.IsFilterByDocFieldDocType.Label")]
        public bool IsFilterByDocFieldDocType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Trạng thái sử dụng
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.IsActivated.Label")]
        public bool IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thứ tự sắp xếp
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kiểu ngày cho phép lọc trên danh sách
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
        /// Thiết lập kiểu hiển thị văn bản chưa đọc trên cây văn bản
        /// 0: hiển thị văn bản chưa đọc (default)
        /// 1: hiển thị tổng số văn bản
        /// 2: hiển thị số văn bản chưa đọc/ tổng số văn bản
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.ShowTotalInTreeType.Label")]
        public byte ShowTotalInTreeType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập chuỗi json chứa giá trị xắp xếp mặc định trên từng node
        /// Format: ({ name:"DateRecive", type: 1 }); 
        ///         Type: { asc:1, desc: 0}
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.DefaultSort.Label")]
        public string DefaultSort { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập có lấy ủy quyền vượt cấp hay không
        /// Note: trường nay chỉ dùng trên node cây văn bàn ủy quyền,
        ///      mục đích là lấy ra danh sách của những người ủy quyền cho những người ủy quyền
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.HasUyQuyen.Label")]
        public bool HasUyQuyen { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập node có chuyển theo lô hay không
        /// Note: contextmenu vào node thì hiển thị các hướng chuyển trùng nhau của các văn bản để bàn giao
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.HasTransferTheoLo.Label")]
        public bool HasTransferTheoLo { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nhóm cây
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.TreeGroupId.Label")]
        public int TreeGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nhóm cây
        /// </summary>
        public string ExportFileConfig { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị xác định có xuất danh sách văn bản ra file (word, excell)
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.HasExportFile.Label")]
        public bool HasExportFile { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn lấy dữ liệu hiển thị ra file
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.QueryExportDataToFile.Label")]
        public string QueryExportDataToFile { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id ColumnSetting
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.PermissionSettingId.Label")]
        public int? PermissionSettingId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id ColumnSetting
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.CreateOrEdit.Fields.DocColumnSettingId.Label")]
        public int DocColumnSettingId { get; set; }
    }
}