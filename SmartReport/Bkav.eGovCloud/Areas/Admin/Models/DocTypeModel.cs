using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Helper;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(DocTypeValidator))]
    public class DocTypeModel : PacketModel
    {
        public DocTypeModel() : base() { }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại văn bản, hồ sơ
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id lĩnh vực
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.DocFieldId.Label")]
        public int? DocFieldId { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập ReportModelId của lĩnh vực
        /// </summary>
         [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.ReportModelId.Label")]
        public int? ReportModeId { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập 
        /// tên lĩnh vực
        /// </summary>
        public string DocFieldName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu đánh số văn bản
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.CodeId.Label")]
        public int? CodeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập khuôn dạng của mẫu đánh số văn bản
        /// </summary>
        public string CodeTemplate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thể loại văn bản
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.CategoryId.Label")]
        public int CategoryId { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập thể loại văn bản
        /// </summary>
        public int? FormCategoryId { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập Tên loại hồ sơ, công văn
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.DocTypeName.Label")]
        public string DocTypeName { get; set; }

        /// <summary>
        /// Mã DocType để đồng bộ với dữ liệu báo cáo chính phủ
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.DocTypeCode.Label")]
        public string DocTypeCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Trích yếu mặc định
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.CompendiumDefault.Label")]
        public string CompendiumDefault { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mẫu phần đầu mã hồ sơ
        /// </summary>
        public string BeginCode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Số hồ sơ hiện tại
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.CurrentDocNumber.Label")]
        public int? CurrentDocNumber { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hồ sơ được phép nộp qua mạng
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.IsAllowOnline.Label")]
        public bool? IsAllowOnline { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hồ sơ đã được kích hoạt
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.IsActivated.Label")]
        public bool IsActivated { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>GiangPN@bkav.com - 120413</para>
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.CategoryBusinessCode.Label")]
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>GiangPN@bkav.com - 120413</para>
        /// </summary>
        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }

        /// <summary>
        /// Lấy hoặc thiết lập permission cho doctype
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.DocTypePermission.Label")]
        public int? DocTypePermission { get; set; }

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
        /// Lấy hoặc thiết lập danh sách các sổ hồ hơ
        /// </summary>
        public string StoreIds { set; get; }

        /// <summary>
        /// Lấy hoặc thiết lập key cơ quan(eGovOnline)
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.OfficeId.Label")]
        public int? OfficeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mức áp dụng thủ tục: cho cấp thành phố, sở hay quận huyện, phường xã.(eGovOnline)
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.LevelId.Label")]
        public int? LevelId { get; set; }

        /// <summary>
        /// Lấy  hoặc thiết lập mức dịch vụ công(eGovOnline)
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.ActionLevel.Label")]
        public int? ActionLevel { get; set; }

        /// <summary>
        /// Lấy  hoặc thiết lập nội dung(eGovOnline)
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.Content.Label")]
        public string Content { get; set; }

        public List<int> DoctypePermissions { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? TotalRegisted { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tổng số lượt xem của thủ tục(eGovOnline)
        /// </summary>
        public int? TotalViewed { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Unsigned { get; set; }

        /// <summary>
        ///Tên file icon 
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.IconFileName.Label")]
        public string IconFileName { get; set; }

        /// <summary>
        ///Tên file icon 
        /// </summary>
        [LocalizationDisplayName("Customer.DocType.CreateOrEdit.Fields.IconFileName.Label")]
        public string IconFileDisplayName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ICollection<DocTypeLaw> DocTypeLaws { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các sổ hồ hơ
        /// </summary>
        public string WorkflowIds { set; get; }

        /// <summary>
        /// Lấy hoặc thiết lập thứ tự hiển thị
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái cho phép tính hạn giữ hay không (hạn xử lý trên node)
        /// </summary>
        public bool HasOverdueInNode { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Doctype id default
        /// </summary>
        public int StoreIdDefault { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập sử dung truy vấn báo cáo
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
        public string SurveyConfig { get; set; }
        public string SurveyReport { get; set; }
        public string SurveyCriteria { get; set; }
        public string SurveyImg { get; set; }
        public string SurveyImgPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UnitDelivery { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? UnitReceive { get; set; }
    }

    public class DocTypeReportKey
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Surveys
    {
        public IEnumerable<SurveyList> Items { get; set; }
        public Pager Pager { get; set; }
    }
    public class SurveyList
    {
        public Guid DocTypeId { get; set; }
        public string DocTypeName { get; set; }
        public bool Active { get; set; }
    }
}