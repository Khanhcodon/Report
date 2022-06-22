using Newtonsoft.Json;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CommentTransfer - public - Entity
    /// Access Modifiers: 
    /// Create Date : 120413
    /// Author      : GiangPN
    /// </author>
    /// <summary>
    /// Danh sách người nhận bàn giao được chọn trên form bàn giao
    /// </summary>
    public class CommentTransfer
    {
        /// <summary>
        /// Fullname/Tên nhóm người nhận văn bản
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Tên phòng ban người/nhóm người nhận văn bản
        /// </summary>
        [JsonProperty("department")]
        public string Department { get; set; }

        /// <summary>
        /// Tên class hiển thị khi view trong nội dung ý kiến
        /// <para><c>viewXlc</c> class(css) hiển thị tên user xử lý chính</para>
        /// <para><c>viewDxl</c> class(css) hiển thị tên user đồng xử lý</para>
        /// <para><c>viewDonggui</c> class(css) hiển thị tên user đồng gửi</para>
        /// </summary> 
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Xác định loại xý chính, đồng xử lý, đồng gửi
        /// <para><c>xulychinh</c> Là xử lý chính</para>
        /// <para><c>dongxuly</c> Là đồng xử lý</para>
        /// <para><c>donggui</c> Là đồng gửi(dạng thông báo)</para>
        /// </summary> 
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Tên class hiển thị khi view  danh sách hướng chuyển
        /// <para><c>jobtitlesItem</c>Chức vụ</para>
        /// <para><c>jobtitlesDeptItem</c> Phòng ban - chức vụ</para>
        /// <para><c>allUsers</c> class(css)Tất cả người dùng, cán bộ</para>
        ///  <para><c>deptItem</c> class(css) Phòng ban</para>
        ///   <para><c>userItem</c> class(css)Cán bộ, người dùng</para>
        /// </summary> 
        [JsonProperty("class")]
        public string Class { get; set; }

        /// <summary>
        /// Giá trị xác định đang xử lsy trên di động.
        /// </summary>
        [JsonProperty("isMobile")]
        public bool IsMobile { get; set; }
    }
}
