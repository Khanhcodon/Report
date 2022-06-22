using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Core.Utils;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : Node - public - Entity
    /// Access Modifiers: 
    /// Create Date : 091012
    /// Author      : GiangPN
    /// Description : Định nghĩa các node trong luồng công văn
    /// ************************************************* 
    /// Modify Date: 181012
    /// Editor: TrungVH
    /// Resons: Chỉnh lại các property cho phù hợp với việc vẽ quy trình
    ///     * Add: 
    ///         - Width                     - double    - Public: Chiều rộng của node
    ///         - Height                    - double    - Public: Chiều cao của node
    ///     * Edit: 
    ///         - Left, Top, Right, Bottom  - double            : Sửa kiểu dữ liệu từ kiểu int -> double
    /// *************************************************
    /// Modify Date: 240113
    /// Editor: CuongNT
    /// Resons:  Chuyển một số hàm từ WorkflowUtil vào Node.cs và Path.cs do nó là hàm xử lý dữ liệu nội bộ của các class này.
    /// *************************************************
    /// </author>
    /// <summary>
    /// <para>Định nghĩa các node trong luồng công văn</para>
    /// (GiangPN@bkav.com - 091012)
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Node
    {
        private List<Action> _actions;

        private List<Address> _address;

        /// <summary>
        /// Lấy hoặc thiết lập ID của node
        /// </summary>
        /// <value>Id</value>
        [JsonProperty("ID")]
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên của nodes
        /// </summary>
        /// <value>Name</value>
        [JsonProperty("NAME")]
        public string NodeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thuộc tính khởi tạo văn bản, qui định node nào được khởi tạo văn bản.
        /// </summary>
        /// <value>IsStart</value>
        [JsonProperty("START")]
        public bool IsStart { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thuộc tính cho phép văn bản có được kết thúc xử lý hay không
        /// </summary>
        /// <value>IsClose</value>
        [JsonProperty("CLOSE")]
        public bool IsClose { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của IsBlinkSign: cho phép thực hiện "Lưu sổ nội bộ"(hiển thị khi nhấn vào "Chuyển" văn bản)
        /// </summary>
        /// <value>IsInternalSaveRecord</value>
        [JsonProperty("KYNHAY")]
        public bool IsInternalSaveRecord { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của OfficialSign: cho phép thực hiện "Lưu sổ và phát hành nội bộ"(hiển thị khi nhấn vào "Chuyển" văn bản)
        /// </summary>
        /// <value>IsSaveRecordAndInternalRelease</value>
        [JsonProperty("KYCHINHTHUC")]
        public bool IsSaveRecordAndInternalRelease { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của IsStamp: cho phép đánh lại số đến(dùng cho văn thư)
        /// </summary>
        /// <value>IsStamp</value>
        [JsonProperty("DONGDAU")]
        public bool IsStamp { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của IsChangeContent: cho phép người nhận công văn được quyền sửa nội dung.
        /// </summary>
        /// <value>IsChangeContent</value>
        [JsonProperty("CHANGECONTENT")]
        public bool IsChangeContent { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị cho phép(hoặc không) thay đổi luồng công văn(chức năng phân loại).
        /// </summary>
        /// <value>IsChangeType</value>
        [JsonProperty("CHANGETYPE")]
        public bool IsChangeType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị để xác định có hỏi thay đổi thời gian xử lý khi thay đổi luồng xử lý hay không (Phân loại)
        /// </summary>
        [JsonProperty("CHANGEEXPIRE")]
        public bool IsChangeExpire { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của IsSaveRecordAndRelease: cho phép thực hiện chức năng "Lưu sổ và phát hành" và "chuyển người khởi tạo văn bản"
        /// </summary>
        /// <value>IsSaveRecordAndRelease</value>
        [JsonProperty("LUU")]
        public bool IsSaveRecordAndRelease { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền ký duyệt
        /// </summary>
        [JsonProperty("KYDUYET")]
        public bool Approve { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền cập nhật kết quả
        /// </summary>
        [JsonProperty("CAPNHATKETQUA")]
        public bool UpdateResult { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền trả kết quả
        /// </summary>
        [JsonProperty("TRAKETQUA")]
        public bool ReturnResult { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền đặt lịch hẹn công dân
        /// </summary>
        [JsonProperty("DATLICHHEN")]
        public bool Booking { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện không tính thời gian xử lý văn bản khi nằm tại nút này
        /// </summary>
        [JsonProperty("DUNGXULY")]
        public bool StopProcess { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền yêu cầu bổ sung
        /// </summary>
        [JsonProperty("YEUCAUBOSUNG")]
        public bool RequestSupplementary { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thể hiện quyền tiếp nhận bổ sung
        /// </summary>
        [JsonProperty("TIEPNHANBOSUNG")]
        public bool ReceiveSupplementary { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị cho phép(hoặc không) cán bộ xem hồ sơ một cửa của nhau.
        /// </summary>
        /// <value>ChoPhepCanBoThayHoSoMotCuaCuaNhau</value>
        [JsonProperty("ChoPhepCanBoThayHoSoMotCuaCuaNhau")]
        public bool ChoPhepCanBoThayHoSoMotCuaCuaNhau { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép cấp số khi khởi tạo văn bản (hsmc là mặc định cho phép)
        /// </summary>
        [JsonProperty("ChoPhepCapSoKhiKhoiTao")]
        public bool ChoPhepCapSoKhiKhoiTao { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thời gian 1 văn bản/hồ sơ được giữ ở 1 Node
        /// </summary>
        [JsonProperty("TIMEINNODE")]
        public int TimeInNode { get; set; }

        ///<summary>
        /// Lấy hoặc thiết lập kiểu thời gian lưu trữ: Ngày hoặc giờ
        ///</summary>
        [JsonProperty("TIMETYPE")]
        public int TimeType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của ViewOption: tùy chọn hiện thị các thuộc tính khi tạo mới công văn từ node này.
        /// <para>Các thuộc tính để tùy chọn hiển thị bao gồm: Loại văn bản(4096,8192), Số trang(32,2048), Lĩnh vực(1,64), Từ khóa(2,128),</para>
        /// <para>Thuộc nhóm(4,256), Hình thức gửi(8,512), Đơn vị nhận hồ sơ(16,1024).</para>
        /// <para>Các (value1,value2) sau mỗi thuộc tính là để chỉ giá trị khi chọn "Bỏ qua" - value1 hoặc Xác nhận "?" - value2</para>
        /// </summary>
        /// <value>ViewOption</value>
        [JsonProperty("YKIEN")]
        public int ViewOption { set; get; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị của ReceiveRecords: trạng thái tiếp nhận hồ sơ(đối với loại công văn 1 cửa)
        /// </summary>
        /// <value>ReceiveRecords</value>
        [JsonProperty("TIEPNHANHOSO")]
        public string ReceiveRecords { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị để lưu không gian user sẽ được gửi công văn (dưới dạng thông báo) khi 1 công văn được chuyển đến node này.
        /// <para>Giá trị: $pos:2!*. Có thể hiểu là tất cả các vị trí(chức vụ) của phòng ban(đơn vị) có Id == 2</para>
        /// </summary>
        /// <value>Announce</value>
        [JsonProperty("THONGBAO")]
        public string Notification { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tọa độ bên trái của node.
        /// </summary>
        /// <value>Left</value>
        [JsonProperty("LEFT")]
        public double Left { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tọa độ bên phải của node.
        /// </summary>
        /// <value>Right</value>
        [JsonProperty("RIGHT")]
        public double Right { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tọa độ trên của node.
        /// </summary>
        /// <value>Top</value>
        [JsonProperty("TOP")]
        public double Top { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tọa độ dưới của node.
        /// </summary>
        /// <value>Bottom</value>
        [JsonProperty("BOTTOM")]
        public double Bottom { get; set; }

        /// <summary>
        /// Lấy ra chiều rộng của node
        /// </summary>
        [JsonProperty("WIDTH")]
        public double Width
        {
            get { return Right - Left; }
        }

        /// <summary>
        /// Lấy ra chiều cao của node
        /// </summary>
        [JsonProperty("HEIGHT")]
        public double Height
        {
            get { return Bottom - Top; }
        }

        ///// <summary>
        ///// Lấy hoặc thiết lập mẫu giao diện cho node
        ///// </summary>
        //[JsonProperty("TEMPLATE")]
        //public string Template { get; set; }

        //HopCV: lấy mẫu giao diện theo id 
        /// <summary>
        /// Lấy hoặc thiết lập mẫu giao diện cho node
        /// </summary>
        [JsonProperty("TEMPLATE")]
        public int? TemplateId { get; set; }

        #region Children

        /// <summary>
        /// Lấy hoặc thiết lập danh sách hướng chuyển của Node.
        /// </summary>
        /// <value>Actions</value>
        [JsonProperty("ACTION")]
        public List<Action> Actions
        {
            get
            {
                return _actions ?? (_actions = new List<Action>());
            }
            set { _actions = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các không gian user của Node đến.
        /// </summary>
        /// <value>Address</value>
        [JsonProperty("ADDRESS")]
        public List<Address> Address
        {
            get
            {
                return _address ?? (_address = new List<Address>());
            }
            set { _address = value; }
        }
        #endregion

        /// <summary>
        /// <para>Lay hoac thiet lap Quy trinh chua nut hien tai.</para>
        /// CuongNT@bkav.com - 030713
        /// </summary>
        [JsonIgnore]
        public Path Parent { get; set; }

        /// <summary>
        /// <para>Trả về không gian user mặc định của nút</para>
        /// CuongNT - 240113
        /// </summary>
        /// <returns>Không gian user mặc định</returns>
        public Address GetDefaultAddress()
        {
            var address = Address.SingleOrDefault(t => t.NodeFrom == 0);
            return address;
        }

        /// <summary>
        /// <para>Trả về danh sách không gian user các hướng chuyển tới nếu có</para>
        /// CuongNT - 240113
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Address> GetRelationAddresses()
        {
            var addresses = Address.Where(t => t.NodeFrom != 0).Select(o => o);
            return addresses;
        }

        /// <summary>
        /// <para>Trả về không gian user của một hướng chuyển tới nếu có</para>
        /// CuongNT - 240113
        /// </summary>
        /// <param name="relationNodeId"></param>
        /// <returns></returns>
        public Address GetRelationAddress(int relationNodeId)
        {
            var addresses = Address.SingleOrDefault(t => t.NodeFrom == relationNodeId);
            return addresses;
        }

        /// <summary>
        /// <para>Trả về danh sách hướng chuyển có thể có của 1 node và các hướng chuyển đặc biệt</para>
        /// <para>CuongNT@bkav.com - 240113</para>
        /// </summary>
        /// <returns>Danh sách hướng chuyển</returns>
        public IEnumerable<Action> GetActions()
        {
            var results = new List<Action>();

            if (IsSaveRecordAndRelease)
            {
                results.Add(new Action
                {
                    Id = ActionSpecial.LuuSoVaPhatHanhRaNgoai.ToString(),
                    Name = EnumHelper<ActionSpecial>.GetDescription(ActionSpecial.LuuSoVaPhatHanhRaNgoai),
                    IsSpecial = true
                });
            }
            if (IsSaveRecordAndInternalRelease)
            {
                results.Add(new Action
                {
                    Id = ActionSpecial.LuuSoVaPhatHanhNoiBo.ToString(),
                    Name = EnumHelper<ActionSpecial>.GetDescription(ActionSpecial.LuuSoVaPhatHanhNoiBo),
                    IsSpecial = true
                });
            }
            if (IsInternalSaveRecord)
            {
                results.Add(new Action
                {
                    Id = ActionSpecial.LuuSoNoiBo.ToString(),
                    Name = EnumHelper<ActionSpecial>.GetDescription(ActionSpecial.LuuSoNoiBo),
                    IsSpecial = true
                });
            }
            if (StopProcess)
            {
                results.Add(new Action
                {
                    Id = ActionSpecial.LienThong.ToString(),
                    Name = EnumHelper<ActionSpecial>.GetDescription(ActionSpecial.LienThong),
                    IsSpecial = true
                });
            }
            if (Actions.Any())
            {
                results.AddRange(Actions);
            }
            return results;
        }

        /// <summary>
        /// Trả về quyền trên node trong quy trình
        /// </summary>
        /// <returns></returns>
        public NodePermissions GetNodePermission()
        {
            var result = (NodePermissions)0;

            if (UpdateResult)
            {
                result = result | NodePermissions.QuyenCapNhatKetQuaXuLyCuoi;
            }

            if (IsStart)
            {
                result = result | NodePermissions.QuyenKhoiTao;
            }

            if (IsClose)
            {
                result = result | NodePermissions.QuyenKetThucXuLy;
            }

            if (IsChangeType)
            {
                result = result | NodePermissions.QuyenPhanLoai;
            }

            if (IsStamp)
            {
                result = result | NodePermissions.QuyenDanhLaiSoDen;
            }

            if (IsChangeContent)
            {
                result = result | NodePermissions.QuyenThayDoiNoiDung;
            }

            if (IsSaveRecordAndRelease)
            {
                result = result | NodePermissions.QuyenLuuSoPhatHanh;
            }

            if (IsSaveRecordAndInternalRelease)
            {
                result = result | NodePermissions.QuyenLuuSoVaPhatHanhNoiBo;
            }

            if (ReceiveSupplementary)
            {
                result = result | NodePermissions.QuyenTiepNhanBoSung;
            }

            if (RequestSupplementary)
            {
                result = result | NodePermissions.QuyenYeuCauBoSung;
            }

            if (Approve)
            {
                result = result | NodePermissions.QuyenKiDuyet;
            }

            if (StopProcess)
            {
                result = result | NodePermissions.QuyenDungXuLy;
            }

            if (ReturnResult)
            {
                result = result | NodePermissions.QuyenTraKetQua;
            }

            if (IsChangeExpire)
            {
                result = result | NodePermissions.QuyenChoPhepCapNhatHanXuLyKhiThayDoiLuong;
            }

            if (ChoPhepCanBoThayHoSoMotCuaCuaNhau)
            {
                result = result | NodePermissions.QuyenChoPhepCanBoTiepNhanNhinThayHoSoCuaNhau;
            }

            if (ChoPhepCapSoKhiKhoiTao)
            {
                result = result | NodePermissions.QuyenCapSoKyHieuKhiKhoiTao;
            }

            if (IsInternalSaveRecord)
            {
                result = result | NodePermissions.QuyenCapSoTruoc;
            }

            if (Booking)
            {
                result = result | NodePermissions.QuyenDatLichTiepCongDanKhiKetThucHoSo;
            }
            return result;
        }

        /// <summary>
        /// Trả về <c>True</c> nếu node hiện tại có chứa cán bộ. <c>False</c> nếu ngược lại.
        /// </summary>
        /// <param name="userId">Id cán bộ cần kiểm tra</param>
        /// <param name="userDeptPos"> </param>
        /// <param name="depIdExt"> </param>
        /// <returns></returns>
        public bool ContainUser(int userId, IEnumerable<UserDepartmentPosition> userDeptPos, out List<string> depIdExt)
        {
            depIdExt = new List<string>();
            var defaultAddress = GetDefaultAddress();
            return defaultAddress != null && (!defaultAddress.Queries.Any() || defaultAddress.Queries.ContainUser(userId, userDeptPos, out depIdExt));
        }
    }
}