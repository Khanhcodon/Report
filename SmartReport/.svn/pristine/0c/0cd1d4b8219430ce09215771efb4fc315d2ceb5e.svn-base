using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Document - public - Entity
    /// Access Modifiers:
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Document trong CSDL
    /// </summary>
    public class Document
    {
        #region Fields

        private ICollection<Attachment> _attachments;
        private ICollection<DocCatalog> _docCatalogs;
        private ICollection<DocumentContent> _docContents;
        private ICollection<DocExtendField> _docExtendFields;
        private ICollection<StoreDoc> _storeDocs;
        private ICollection<DocFee> _docFees;
        private ICollection<DocPaper> _docPapers;
        private ICollection<DocRelation> _docRelations;

        #endregion Fields

        #region C'tor

        /// <summary>
        ///
        /// </summary>
        public Document()
        {
            ResultStatus = null;
            Status = (int)DocumentStatus.DangXuLy;
            UrgentIdInEnum = Urgent.Thuong;
            UrgentId = (int)Urgent.Thuong;
			LienThongStatus = "";
        }

        #endregion C'tor

        #region Instance Properties

        #region Columns

        /// <summary>
        /// Lấy hoặc thiết lập Id văn bản, hồ sơ
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public Guid? DocTypeId { get; set; }

        /// <summary>
        /// Tên loại văn bản
        /// </summary>
        public string DocTypeName { get; set; }

        /// <summary>
        /// Mã loại văn bản
        /// </summary>
        public string DocTypeCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id thể loại
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Tên hình thức văn bản
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số kí hiệu (eOffice), mã hồ sơ (eGate)
        /// </summary>
        public string DocCode { get; set; }

        /// <summary>
        /// Tên công dân: dùng cho hs một cửa
        /// </summary>
        public string CitizenName { get; set; }

        /// <summary>
        /// Thông tin khác về công dân
        /// </summary>
        public string CitizenInfo { get; set; }

        /// <summary>
        /// CMND
        /// </summary>
        public string IdentityCard { get; set; }

		/// <summary>
		/// Địa chỉ công dân (hsmc), Mã định danh văn bản của văn bản đến
		/// <para>
		/// Note: lưu mã định danh văn bản của văn bản đến
		/// </para>
		/// </summary>
		public string Address { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Trích yếu
        /// </summary>
        public string Compendium { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Trích yếu không dấu
        /// </summary>
        public string Compendium2 { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày hẹn trả (eGate), ngày giải quyết (eOffice)
        /// </summary>
        public DateTime? DateAppointed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày cập nhật
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo văn bản.
        /// </summary>
        public int UserCreatedId { get; set; }

        /// <summary>
        /// Tên người khởi tạo.
        /// </summary>
        public string UserCreatedName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái xử lý cuối cùng của hồ sơ
        /// <para> True: Đồng ý</para>
        /// <para> False: Từ chối</para>
        /// <para> Null: Chưa duyệt</para>
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày kí duyệt
        /// </summary>
        public DateTime? DateSuccess { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người kí chính (eOffice), người kí duyệt (eGate)
        /// </summary>
        public int? UserSuccessId { get; set; }

        /// <summary>
        /// Tên người ký
        /// </summary>
        public string UserSuccessName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ghi chú kí duyệt
        /// </summary>
        public string SuccessNote { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái đã trả kết quả
        /// <para> True: Đã trả</para>
        /// <para> False: Chưa trả</para>
        /// <para> Null: Không trả kết quả</para>
        /// </summary>
        public bool? IsReturned { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày kết thúc xử lý (eOffice), ngày trả kết quả (eGate)
        /// </summary>
        public DateTime? DateReturned { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người trả kết quả (eGate)
        /// </summary>
        public int? UserReturnedId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ghi chú trả kết quả (thông tin công dân nhận kết quả)
        /// </summary>
        public string ReturnNote { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày kết thúc xử lý văn bản
        /// </summary>
        public DateTime? DateFinished { get; set; }

        /// <summary>
        /// Trạng thái xử lý của văn bản. Sử dụng DocumentStatusMachineHelper.ValidateNewDocumentStatus() để kiểm tra trước khi set;
        /// <para> 0: văn bản dự thảo.</para>
        /// <para> 1: văn bản đang xử lý.</para>
        /// <para> 2: văn bản đã kết thúc.</para>
        /// <para> 4: văn bản đã hủy.</para>
        /// <para> 8: văn bản dừng xử lý.</para>
        /// </summary>
        public byte Status { get; set; }

		/// <summary>
		/// Trạng thái văn bản liên thông đến đi
		/// </summary>
		public string LienThongStatus { get; set; }

		/// <summary>
		/// <para>Trạng thái của văn bản copy</para>
		/// <para>(CuongNT@bkav.com - 050413)</para>
		/// </summary>
		public DocumentStatus StatusInEnum
        {
            get
            {
                return (DocumentStatus)Status;
            }
        }

        /// <summary>
        /// Văn bản hồi báo: True (đã được hồi báo), False (cần hồi báo).
        /// </summary>
        public bool IsAcknowledged { get; set; }

        /// <summary>
        /// Đang gửi liên thông.
        /// </summary>
        public bool IsGettingOut { get; set; }

        /// <summary>
        /// Nguồn gốc văn bản:
        /// <para> 0: văn bản tạo trực tiếp.</para>
        /// <para> 1: văn bản nhận từ ĐKQM.</para>
        /// <para> 2: văn bản liên thông.</para>
        /// <value>
        /// Default: 0.
        /// </value>
        /// </summary>
        public byte Original { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id nghiệp vụ
        /// </summary>
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id nghiệp vụ</para>
        /// <para>(CuongNT@bkav.com - 050413)</para>
        /// </summary>
        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }

        /// <summary>
        /// Trạng thái xử lý:
        /// <para> True: thụ lý thành công.</para>
        /// <para> False: thụ lý không thành công.</para>
        /// <default> Null </default>
        /// </summary>
        public bool? ResultStatus { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày có kết quả xử lý thành công
        /// </summary>
        public DateTime? DateResult { get; set; }

        /// <summary>
        /// Văn bản được convert từ hệ thống cũ.
        /// </summary>
        public bool IsConverted { get; set; }

        /// <summary>
        /// ky BC
        /// </summary>
        public string TimeKey { get; set; }

        /// <summary>
        /// luu du lieu tong hop
        /// </summary>
        public string CompilationData { get; set; }

        /// <summary>
		/// Lấy hoặc thiết lập mã cơ quan ban hành
		/// </summary>
		public string FormId { get; set; }

        /// <summary>
        /// trang thai bao cao
        /// </summary>
        public int? StatusReport { get; set; }

        /// <summary>
        /// Độ khẩn
        /// </summary>
        public byte UrgentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại ý kiến
        /// </summary>
        public Urgent UrgentIdInEnum
        {
            get { return (Urgent)UrgentId; }
            set { UrgentId = (byte)value; }
        }

        /// <summary>
        /// Số đến đi, mã đến đi.
        /// </summary>
        public string InOutCode { get; set; }

        /// <summary>
        /// Nơi đến đi.
        /// </summary>
        public string InOutPlace { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái có yêu cầu bổ sung hay không
        /// <para> True: Đã bổ sung</para>
        /// <para> False: Chưa bổ sung</para>
        /// <default> Null: Chưa từng có yêu cầu bổ sung </default>
        /// </summary>
        public bool? IsSupplemented { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày yêu cầu bổ sung đầu tiên
        /// </summary>
        public DateTime? DateRequireSupplementary { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Danh sách Id của lĩnh vực (lưu dạng ;id;id;)
        /// </summary>
        public string DocFieldIds { get; set; }

        private List<int> _listDocField;

        /// <summary>
        /// Lấy danh sách id lĩnh vực
        /// </summary>
        public List<int> ListDocFieldId
        {
            get
            {
                if (_listDocField == null)
                {
                    _listDocField = new List<int>();
                    if (!string.IsNullOrEmpty(DocFieldIds))
                    {
                        var split = DocFieldIds.Split(';');
                        if (split.Length > 0)
                        {
                            foreach (var s in split)
                            {
                                if (string.IsNullOrEmpty(s))
                                {
                                    continue;
                                }
                                int id;
                                if (int.TryParse(s, out id))
                                {
                                    _listDocField.Add(id);
                                }
                            }
                        }
                    }
                }
                return _listDocField;
            }
        }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Danh sách quyền tác động lên loại hồ sơ.</para>
        /// <para>Dữ liệu thộc kiểu: DocTypePermission</para>
        /// CuongNT - 240113
        /// </summary>
        public int? DocTypePermission { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime? DateResponsed { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime? DateResponsedOverdue { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? ProcessedMinutes { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày tạo mã hồ sơ/văn bản
        /// </summary>
        [DataMember]
        public DateTime? DateOfIssueCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày(thời hạn) hồi báo
        /// </summary>
        public DateTime? DateResponse { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id cơ quan ban hành
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id cơ quan ban hành
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id sổ văn bản
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id độ mật
        /// </summary>
        public int? SecurityId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập số trang
        /// </summary>
        public int? TotalPage { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số tờ trình
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id hình thức gửi
        /// </summary>
        public int? SendTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày đến của văn bản (văn bản đến)
        /// </summary>
        public DateTime? DateArrived { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày văn bản
        /// </summary>
        public DateTime? DatePublished { get; set; }

        /// <summary>
        /// Dùng trong tiếp nhận HSMC, khi mất điện/lỗi hệ thống ko tiếp nhận được cv, lúc sau nhập lại lý do
        /// </summary>
        public string DelayReason { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? WorkflowTypeId { get; set; }

        /// <summary>
        /// Thời hạn xử lý (theo ngày)
        /// </summary>
        public int? ExpireProcess { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã sử dụng
        /// </summary>
        public int? CodeId { get; set; }

        /// <summary>
        /// Ghi chú thông tin khác
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Thông tin xử lý: thông tin phát hành với văn bản đi và thông tin chuyển xử lý khi tiếp nhận văn bản đên.
        /// </summary>
        public string ProcessInfo { get; set; }

        /// <summary>
        /// Nơi trả hồ sơ khi tiếp nhận
        /// </summary>
        public int? TypeReturned { get; set; }

        /// <summary>
        /// Thông tin có phải trạng thái phát hành ra ngoài
        /// true: phát hành ra ngoài
        /// false: phát hành nội bộ
        /// null: không phát hành
        /// </summary>
        public bool? IsTransferPublish { get; set; }

        /// <summary>
        /// Tên nơi nhận trả kết quả
        /// </summary>
        public string TypeReturnedDescription
        {
            get
            {
                if (this.TypeReturned.HasValue)
                {
                    foreach (DocumentLocationReturned location in System.Enum.GetValues(typeof(DocumentLocationReturned)))
                    {
                        if ((int)location == this.TypeReturned.Value)
                        {
                            var description = EnumHelper<DocumentLocationReturned>.GetDescription(location);
                            return description;
                        }
                    }
                }
                return "";
            }
        }

        /// <summary>
        /// Giá trị xác định văn bản có ký số không
        /// </summary>
        public bool? HasCA { get; set; }


        /// <summary>
        /// Xử lý phần đóng mở báo cáo
        /// </summary>
        public bool? StatusOpenClose { get; set; }

        ///// <summary>
        ///// Cờ đánh dấu văn bản/hồ sơ có tệp đính kèm hay không
        ///// </summary>
        //public bool HasAttachments { get; set; }

        ///// <summary>
        ///// Cờ đánh dấu văn bản/hồ sơ có văn bản/hồ sơ liên quan hay không
        ///// </summary>
        //public bool HasRelations { get; set; }

        ///// <summary>
        ///// Cờ đánh dấu văn bản/hồ sơ có yêu cầu bổ sung hay không
        ///// </summary>
        //public bool HasSupplementaries { get; set; }

        #endregion Columns

        #region Relations

        /// <summary>
        /// Lấy hoặc thiết lập Các tệp đính kèm liên quan
        /// </summary>
        public virtual ICollection<Attachment> Attachments
        {
            get { return _attachments ?? (_attachments = new List<Attachment>()); }
            set { _attachments = value; }
        }
        
        /// <summary>
        /// Lấy hoặc thiết lập nội dung của hồ sơ, văn bản
        /// </summary>
        public virtual ICollection<DocumentContent> DocumentContents
        {
            get { return _docContents ?? (_docContents = new List<DocumentContent>()); }
            set { _docContents = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lâp user cập nhật kết quả xử lý cuối
        /// </summary>
        public User UserSuccess { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ICollection<DocExtendField> DocExtendFields
        {
            get { return _docExtendFields ?? (_docExtendFields = new List<DocExtendField>()); }
            set { _docExtendFields = value; }
        }

        /// <summary>
        /// Danh sách hồ sơ liên quan
        /// </summary>
        public virtual ICollection<DocRelation> DocRelations
        {
            get { return _docRelations ?? (_docRelations = new List<DocRelation>()); }
            set { _docRelations = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public ICollection<DocCatalog> DocCatalogs
        {
            get { return _docCatalogs ?? (_docCatalogs = new List<DocCatalog>()); }
            set { _docCatalogs = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public virtual ICollection<StoreDoc> StoreDocs
        {
            get { return _storeDocs ?? (_storeDocs = new List<StoreDoc>()); }
            set { _storeDocs = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public ICollection<DocFee> DocFees
        {
            get { return _docFees ?? (_docFees = new List<DocFee>()); }
            set { _docFees = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public ICollection<DocPaper> DocPapers
        {
            get { return _docPapers ?? (_docPapers = new List<DocPaper>()); }
            set { _docPapers = value; }
        }
        
        #endregion Relations

        #region Method

        /// <summary>
        /// Trạng thái thể hiện văn bản là hồ sơ một cửa
        /// </summary>
        public bool IsHSMC
        {
            get
            {
#if HoSoMotCuaEdition
                return CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc;
#else
                return false;
#endif
            }
        }

        #endregion

        #endregion Instance Properties
    }
}