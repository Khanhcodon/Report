using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : Action - public - Entity
    /// Access Modifiers: 
    /// Create Date : 181212
    /// Author      : GiangPN
    /// </author>
    /// <summary>
    /// <para>Định nghĩa 1 hành động(1 hướng chuyển) từ node này sang node khác</para>
    /// (GiangPN@bkav.com - 181212)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Action
    {
        /// <summary>
        /// C'tor
        /// </summary>
        public Action()
        {
            IsAllow = true;
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id của hành động
        /// </summary>
        [JsonProperty("ID")]
        public string Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên của hành động: đây là tên hướng chuyển văn bản, hồ sơ
        /// </summary>
        /// <value>Tên hướng chuyển.</value>
        [JsonProperty("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị Id của node kế tiếp(node chuyển tới)
        /// </summary>
        /// <value>Lưu giá trị của node kế tiếp</value>
        [JsonProperty("NEXT")]
        public int Next { get; set; }


        /// <summary>
        /// Lấy hoặc thiết lập giá trị Id của user được chuyển tới trong các hướng chuyển đặc biệt
        /// </summary>
        [JsonProperty("USERIDNEXT")]
        public int UserIdNext { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập là hướng chuyển đặc biệt hay không: False là hướng chuyển bình thường, True là hướng chuyển đặc biệt.</para>
        /// <para>True: Lưu sổ phát hành, Lưu sổ và phát hành nội bộ, Trả người khởi tạo....</para>
        /// <para>False: Các hướng chuyển được cấu hình trong quy trình</para>
        /// </summary>
        [JsonProperty("ISSPECIAL")]
        public bool IsSpecial { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hướng chuyển này đã được kích hoạt
        /// </summary>
        /// <value>
        /// 	<c>true</c> nếu hướng chuyển này đã được kích hoạt; ngược lại, <c>false</c>.
        /// </value>
        [JsonProperty("ISACTIVATED")]
        public bool IsActivated { get; set; }

        /// <summary>
        /// Hướng chuyển có được phép sử dụng ở thời điểm hiện tại hay ko
        /// </summary>
        public bool IsAllow { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id node bắt đầu của hành động
        /// </summary>
        [JsonProperty("CURRENT")]
        public int Current { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tọa độ bên trái (tọa độ X) điểm bắt đầu của hành động
        /// </summary>
        [JsonProperty("STARTLEFT")]
        public double StartPointLeft { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tọa độ bên trên (tọa độ Y) điểm bắt đầu của hành động
        /// </summary>
        [JsonProperty("STARTTOP")]
        public double StartPointTop { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tọa độ bên trái (tọa độ X) điểm kết thúc của hành động
        /// </summary>
        [JsonProperty("ENDLEFT")]
        public double EndPointLeft { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tọa độ bên trên (tọa độ Y) điểm kết thúc của hành động
        /// </summary>
        [JsonProperty("ENDTOP")]
        public double EndPointTop { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại hành động
        /// </summary>
        [JsonProperty("TYPE")]
        public ActionType Type { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Hướng chuyển này được phép ký điện tử
        /// </summary>
        [JsonProperty("ISALLOWSIGN")]
        public bool IsAllowSign { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id của quy trình mà hướng chuyển thuộc vào</para>
        /// <para>CuongNT@bkav.com - 270613</para>
        /// </summary>
        [JsonIgnore]
        public int WorkflowId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập quy trình mà hướng chuyển thuộc vào</para>
        /// <para>CuongNT@bkav.com - 270613</para>
        /// </summary>
        [JsonIgnore]
        public Path Parent { get; set; }
    }
}