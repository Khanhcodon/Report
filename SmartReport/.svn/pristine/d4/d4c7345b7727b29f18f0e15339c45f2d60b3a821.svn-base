using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(FormValidator))]
    public class FormModel : PacketModel
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
        /// Lấy hoặc thiết lập nhóm biểu mẫu
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.FormGroup.Label")]
        public int FormGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại form
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.FormType.Label")]
        public int FormTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên form
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.FormName.Label")]
        public string FormName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả form
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.Description.Label")]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Định nghĩa form dạng json
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra form này là form chính
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.IsPrimary.Label")]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên file template là file doc, docx hoặc html.
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên mẫu phôi cho form động.
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.Embryonic.Label")]
        public string EmbryonicPath { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên mẫu phôi cho form động.
        /// </summary>
        public string EmbryonicLocationName{get;set;}

        /// <summary>
        /// Lấy hoặc thiết lập Url cho Form theo Url
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.FormUrl.Label")]
        public string FormUrl { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra form này đã được kích hoạt.<para></para>
        /// Lưu ý: đối với các mẫu form, template chính cần kiểm tra ràng buộc chỉ một mẫu được active tại một thời điểm khi add hoặc update.<para></para>
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.IsActivated.Label")]
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
        public string ConfigForm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FormCode { get; set; }

		/// <summary>
        /// 
        /// </summary>
        public string ConfigFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.CompilationId.Label")]
        public string CompilationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.ChildCompilationId.Label")]
        public string ChildCompilationId { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        public int VersionForm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }

        public ICollection<FormType> FormTypes { get; set; }

        /// <summary>
        /// VuHQ REQ-02
        /// Json cấu hình của header (name, comment) của handsontable
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.DefineFieldJson.Label")]
        public string DefineFieldJson { get; set; }

        /// <summary>
        /// VuHQ REQ-02
        /// Json cấu hình của header (type, required, default value, readonly) của handsontable
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.DefineConfigJson.Label")]
        public string DefineConfigJson { get; set; }

        /// <summary>
        /// VuHQ REQ-02
        /// Json cấu hình giá trị mặc định của handsontable
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.DefineValueJson.Label")]
        public string DefineValueJson { get; set; }

        /// <summary>
        /// VuHQ REQ-05
        /// Mẫu header của báo cáo
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.FormHeader.Label")]
        public string FormHeader { get; set; }

        /// <summary>
        /// VuHQ REQ-05
        /// Mẫu footer của báo cáo
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.FormFooter.Label")]
        public string FormFooter { get; set; }

        /// <summary>
        /// VuHQ REQ-05
        /// Ten table của báo cáo
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.TableName.Label")]
        public string TableName { get; set; }

        /// <summary>
        /// VuHQ Phase 2 REQ 0
        /// Mẫu báo cáo tường minh
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.ExplicitTemplate.Label")]
        public string ExplicitTemplate { get; set; }

        /// <summary>
        /// VuHQ Phase 2 REQ 0
        /// Mẫu báo cáo tường minh
        /// </summary>
        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.FormCodeCompilation.Label")]
        public string FormCodeCompilation { get; set; }

        public Compilation Compilation { get; set; }

        public GeneralCompilationHeader GeneralCompilationHeader { get; set; }

        public GeneralCompilationDetail GeneralCompilationDetail { get; set; }

        [LocalizationDisplayName("DocType.Form.CreateOrEdit.Fields.FormCategoryId.Label")]
        public int? FormCategoryId { get; set; }

        public FormModel()
            : base()
        {
            IsActivated = 3; // mặc định thêm mới là thêm mẫu tạm
            IsPrimary = true; 
            VersionDateTime = DateTime.Now;
            CreatedOnDate = DateTime.Now;
        }

        public List<int> ReportKeyId { get; set; }
    }

    public class Compilation
    {
        public string Field { get; set; }

        public string Value { get; set; }

        public string Match { get; set; }

        public string Select { get; set; }

        public string Display { get; set; }
    }

    public class GeneralCompilationHeader
    {
        public string TableName { get; set; }

        public string Select { get; set; }

        public string Display { get; set; }

        public string Formula { get; set; }
    }

    public class GeneralCompilationDetail
    {
        public List<GeneralMethod> Method { get; set; }

        public List<GeneralQuery> Query { get; set; }
    }

    public class GeneralMethod
    {
        [JsonProperty("MethodName")]
        public string MethodName { get; set; }

        [JsonProperty("DBFieldName")]
        public string DBFieldName { get; set; }
    }

    public class GeneralQuery
    {
        [JsonProperty("QueryString")]
        public string QueryString { get; set; }
    }

    public class ConfigCompilation
    {
        [JsonProperty("type")]
        public string typeOther { get; set; }

        public string title { get; set; }

        [JsonProperty("default")]
        public string text  { get; set; }

        public string htmlClass { get; set; }

        //[JsonProperty("enum")]
        //public object[] enumOther { get;set; }
    }
}