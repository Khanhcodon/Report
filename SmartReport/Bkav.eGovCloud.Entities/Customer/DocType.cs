using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Bkav.eGovCloud.Core.Document;
using System.Web.Mvc;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng DocType trong CSDL
    /// </summary>
    public class DocType
    {
        private ICollection<DocTypeStore> _docTypeStores;

        /// <summary>
        /// Lấy  hoặc thiết lập rằng buộc loại thủ tục hành chính văn bản quy phạm
        /// </summary>
        private ICollection<DocTypeLaw> _docTypeLaws;

        /// <summary>
        /// Lấy hoặc thiết lập Các loại phí liên quan
        /// </summary>
        public virtual ICollection<DocTypeLaw> DocTypeLaws
        {
            get { return _docTypeLaws ?? (_docTypeLaws = new List<DocTypeLaw>()); }
            set { _docTypeLaws = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Tên lĩnh vực
        /// </summary>
        public string DocFieldName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id lĩnh vực
        /// </summary>
        public int? DocFieldId { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập ReportModelId của lĩnh vực
        /// </summary>
        public int? ReportModeId { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập Id thể loại văn bản
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>CuongNT@bkav.com - 120413</para>
        /// </summary>
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>CuongNT@bkav.com - 120413</para>
        /// </summary>
        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu đánh số văn bản
        /// </summary>
        public int? CodeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên loại hồ sơ, công văn
        /// </summary>
        public string DocTypeName { get; set; }

        /// <summary>
        /// Mã DocType để đồng bộ với dữ liệu báo cáo chính phủ
        /// </summary>
        public string DocTypeCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Trích yếu mặc định
        /// </summary>
        public string CompendiumDefault { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Nội dung mặc định
        /// </summary>
        public string ContentDefault { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hồ sơ được phép nộp qua mạng
        /// </summary>
        public bool? IsAllowOnline { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hồ sơ đã được kích hoạt
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các Id sổ hồ sơ
        /// </summary>
        public string StoreIds { get; set; }

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

        /// <summary>
        /// Lấy hoặc thiết lập permission cho doctype
        /// </summary>
        public int? DocTypePermission { get; set; }

        /// <summary>
        ///Tên file icon 
        /// </summary>
        public string IconFileName { get; set; }

        /// <summary>
        ///Tên file icon hiển thị
        /// </summary>
        public string IconFileDisplayName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập permission cho doctype
        /// </summary>
        public DocTypePermissions? DocTypePermissionInEnum
        {
            get
            {
                if (DocTypePermission == null)
                {
                    return null;
                }
                return (DocTypePermissions)DocTypePermission;
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Lĩnh vực
        /// </summary>
        public virtual DocField DocField { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mẫu sổ công văn hồ sơ
        /// </summary>
        public virtual Code Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<DocTypeStore> DocTypeStores
        {
            get { return _docTypeStores ?? (_docTypeStores = new List<DocTypeStore>()); }
            set { _docTypeStores = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập key cơ quan(eGovOnline)
        /// </summary>
        public int? OfficeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mức áp dụng thủ tục: cho cấp thành phố, sở hay quận huyện, phường xã.(eGovOnline)
        /// </summary>
        public int? LevelId { get; set; }

        /// <summary>
        /// Lấy  hoặc thiết lập mức dịch vụ công(eGovOnline)
        /// </summary>
        public int? ActionLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? TotalRegisted { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tổng số lượt xem của thủ tục(eGovOnline)
        /// </summary>
        public int? TotalViewed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên thủ tục không dấu
        /// </summary>
        public string Unsigned { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nội dung thủ tục hành chính(eGovOnline)
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cơ quan sử dụng thủ tục
        /// </summary>
        public Office Office { get; set; }

        /// <summary>
        /// Lấy  hoặc thiết lập rằng buộc thủ tục hành chính giấy tờ
        /// </summary>
        public virtual ICollection<DoctypePaper> DoctypePapers { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các Id quy trình
        /// </summary>
        public int WorkflowId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thứ tự hiển thị
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép tính hạn giữ hay không (hạn xử lý trên node)
        /// </summary>
        public bool HasOverdueInNode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập truy vấn báo cáo
        /// </summary>
        public bool IsReportQuery { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập truy vấn báo cáo
        /// </summary>
        public string ReportQuery { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị thủ tục cha của thủ tục hiện tại
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập giá trị Survey
        /// </summary>
        [AllowHtml]
        public string SurveyConfig { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập giá trị Survey báo cáo 
        /// </summary>
        public string SurveyReport { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập giá trị Survey tiêu chí
        /// </summary>
        public string SurveyCriteria { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập giá trị Survey Image
        /// </summary>
        public string SurveyImg { get; set; } 
        /// <summary>
        /// Lấy hoặc thiết lập giá trị Survey Image
        /// </summary>
        public string SurveyImgPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UnitDelivery { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UnitReceive { get; set; }

        /// <summary>
        /// Trả về trạng thái loại văn bản phải là của HSMC ko
        /// </summary>
        /// <returns></returns>    
        public bool IsHsmc()
        {
#if HoSoMotCuaEdition
            return CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc;
#else
            return false;
#endif
        }
    }
}
