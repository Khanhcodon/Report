
namespace Bkav.eGovCloud.Areas.Admin.Models
{
    //[FluentValidation.Attributes.Validator(typeof(NodeValidator))]
    public class NodeModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập ID của node
        /// </summary>
        /// <value>Id</value>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên của nodes
        /// </summary>
        /// <value>Name</value>
        public string NodeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thuộc tính khởi tạo văn bản, qui định node nào được khởi tạo văn bản.
        /// </summary>
        /// <value>IsStart</value>
        public bool IsStart { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thuộc tính cho phép văn bản có được kết thúc xử lý hay không
        /// </summary>
        /// <value>IsClose</value>
        public bool IsClose { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của IsBlinkSign: cho phép thực hiện "Lưu sổ nội bộ"(hiển thị khi nhấn vào "Chuyển" văn bản)
        /// </summary>
        /// <value>IsInternalSaveRecord</value>
        public bool IsInternalSaveRecord { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của OfficialSign: cho phép thực hiện "Lưu sổ và phát hành nội bộ"(hiển thị khi nhấn vào "Chuyển" văn bản)
        /// </summary>
        /// <value>IsSaveRecordAndInternalRelease</value>
        public bool IsSaveRecordAndInternalRelease { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của IsStamp: cho phép đánh lại số đến(dùng cho văn thư)
        /// </summary>
        /// <value>IsStamp</value>
        public bool IsStamp { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của IsChangeContent: cho phép người nhận công văn được quyền sửa nội dung.
        /// </summary>
        /// <value>IsChangeContent</value>
        public bool IsChangeContent { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị cho phép(hoặc không) thay đổi luồng công văn(chức năng phân loại).
        /// </summary>
        /// <value>IsChangeType</value>
        public bool IsChangeType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị cho phép(hoặc không) thay đổi luồng hạn xử lý theo luồng công văn mới(khi thay đổi luồng công văn).
        /// </summary>
        /// <value>IsChangeType</value>
        public bool IsChangeExpire { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của IsSaveRecordAndRelease: cho phép thực hiện chức năng "Lưu sổ và phát hành" và "chuyển người khởi tạo văn bản"
        /// </summary>
        /// <value>IsSaveRecordAndRelease</value>
        public bool IsSaveRecordAndRelease { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền ký duyệt
        /// </summary>
        public bool Approve { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền cập nhật kết quả
        /// </summary>
        public bool UpdateResult { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền trả kết quả
        /// </summary>
        public bool ReturnResult { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập việc giao kế hoạch
        /// </summary>
        public bool AssignPlan { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền đặt lịch hẹn công dân
        /// </summary>
        public bool Booking { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền dừng xử lý
        /// </summary>
        public bool StopProcess { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền yêu cầu bổ sung
        /// </summary>
        public bool RequestSupplementary { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền tiếp nhận bổ sung
        /// </summary>
        public bool ReceiveSupplementary { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị cho phép cấp số
        /// </summary>
        public bool ChoPhepCapSoKhiKhoiTao { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian 1 văn bản/hồ sơ được giữ ở 1 Node
        /// </summary>
        public int TimeInNode { get; set; }

        ///<summary>
        /// Lấy hoặc thiết lập kiểu thời gian lưu trữ: Ngày hoặc giờ
        ///</summary>
        public int TimeType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của ViewOption: tùy chọn hiện thị các thuộc tính khi tạo mới công văn từ node này.
        /// <para>Các thuộc tính để tùy chọn hiển thị bao gồm: Loại văn bản(4096,8192), Số trang(32,2048), Lĩnh vực(1,64), Từ khóa(2,128),</para>
        /// <para>Thuộc nhóm(4,256), Hình thức gửi(8,512), Đơn vị nhận hồ sơ(16,1024).</para>
        /// <para>Các (value1,value2) sau mỗi thuộc tính là để chỉ giá trị khi chọn "Bỏ qua" - value1 hoặc Xác nhận "?" - value2</para>
        /// </summary>
        /// <value>ViewOption</value>
        public int ViewOption { set; get; }

        #region "Lấy giá trị cho ViewOption"

        ///<summary>
        /// Lấy hoặc thiết lập hiển thị loại hồ sơ(khi tạo mới văn bản)
        /// <para>Có 2 giá trị là: DocTypeIgnore và DocTypeConfirm</para>
        ///</summary>
        public string DocTypeView { set; get; }

        ///<summary>
        /// Lấy hoặc thiết lập hiển thị số trang(khi tạo mới văn bản)
        /// <para>Có 2 giá trị là: PageNumberIgnore và PageNumberConfirm</para>
        ///</summary>
        public string PageNumberView { set; get; }

        ///<summary>
        /// Lấy hoặc thiết lập hiển thị lĩnh vực(khi tạo mới văn bản)
        /// <para>Có 2 giá trị là: DocFieldIgnore và DocFieldConfirm</para>
        ///</summary>
        public string DocFieldView { set; get; }

        ///<summary>
        /// Lấy hoặc thiết lập hiển thị từ khóa(khi tạo mới văn bản)
        /// <para>Có 2 giá trị là: KeywordIgnore và KeywordConfirm</para>
        ///</summary>
        public string KeywordView { set; get; }

        ///<summary>
        /// Lấy hoặc thiết lập hiển thị thuộc nhóm(khi tạo mới văn bản)
        /// <para>Có 2 giá trị là: GroupIgnore và GroupConfirm</para>
        ///</summary>
        public string GroupView { set; get; }

        ///<summary>
        /// Lấy hoặc thiết lập hiển thị hình thức gửi(khi tạo mới văn bản)
        /// <para>Có 2 giá trị là: SendTypeIgnore và SendTypeConfirm</para>
        ///</summary>
        public string SendTypeView { set; get; }

        ///<summary>
        /// Lấy hoặc thiết lập hiển thị đơn vị nhận(khi tạo mới văn bản)
        /// <para>Có 2 giá trị là: DestinationIgnore và DestinationConfirm</para>
        ///</summary>
        public string DestinationView { set; get; }

        #endregion

        /// <summary>
        /// <para>Lấy hoặc thiết lập ID của path(cũng là Id của WorkFlow).</para>
        /// <para>(TODO: Lưu ý, thuộc tính này hiện đang bằng 0 khi parse từ json ra. Đang cần xem lại.)</para>
        /// </summary>
        /// <value>PathId</value>
        public int PathId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của ReceiveRecords: trạng thái tiếp nhận hồ sơ(đối với loại công văn 1 cửa)
        /// </summary>
        /// <value>ReceiveRecords</value>
        public string ReceiveRecords { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị để lưu không gian user sẽ được gửi công văn (dưới dạng thông báo) khi 1 công văn được chuyển đến node này.
        /// <para>Giá trị: $pos:2!*. Có thể hiểu là tất cả các vị trí(chức vụ) của phòng ban(đơn vị) có Id == 2</para>
        /// </summary>
        /// <value>Announce</value>
        public string Notification { get; set; }

        /// <summary>
        /// Address json
        /// </summary>
        public string NodeAddress { get; set; }

        /// <summary>
        /// Address json
        /// </summary>
        public string Template { get; set; }
    }
}