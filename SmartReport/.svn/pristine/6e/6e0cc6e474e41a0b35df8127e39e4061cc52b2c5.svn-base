using System;
namespace Bkav.eGovCloud.Api.Dto
{
    /// <summary>
    /// Category data transfer object
    /// </summary>
    public class FormDto
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id form
        /// </summary>
        public Guid FormId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại hồ sơ
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại form
        /// </summary>
        public int FormTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nhóm biểu mẫu
        /// </summary>
        public int? FormGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại form
        /// </summary>
        public Entities.FormType FormTypeIdInEnum
        {
            get { return (Entities.FormType)FormTypeId; }
            set { FormTypeId = (int)value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Tên form
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả form
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Định nghĩa form dạng json
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra form này là form chính
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên file template là file doc, docx hoặc html.
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên mẫu phôi cho form động.
        /// </summary>
        public string Embryonic { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập 1 giá trị chỉ ra form này đã được kích hoạt.</para>
        /// <para>
        /// Lưu ý: đối với các mẫu form, template chính cần kiểm tra ràng buộc chỉ một mẫu được active tại một thời điểm khi add hoặc update.
        /// </para>
        /// <value>
        ///     <para> 1: Form đang được sử dụng.</para>
        ///     <para> 2: Form không được sử dụng.</para>
        ///     <para> 3: Form đang lưu tạm.</para>
        /// </value>
        /// </summary>
        public int IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        public DateTime? CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người cập nhật cuối cùng
        /// </summary>
        public int? LastModifiedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày cập nhật cuối cùng
        /// </summary>
        public DateTime? LastModifiedOnDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] VersionByte { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }
    
    }
}
