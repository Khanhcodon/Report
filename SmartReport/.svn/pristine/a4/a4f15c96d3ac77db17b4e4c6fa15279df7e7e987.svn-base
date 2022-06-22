using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Bkav.eGovCloud.Core.Document;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// <para></para> Bkav Corp. - BSO - eGov - eOffice team
    /// <para></para> Project: eGov Cloud v1.0
    /// <para></para> Class : Form - public - Entity
    /// <para></para> Access Modifiers: 
    /// <para></para> Create Date : 270612
    /// <para></para> Author      : TrungVH
    /// <para></para> Description : 
    ///     <para> Entity tương ứng với bảng Form trong CSDL.</para>
    ///     <para> Dùng để lưu các mẫu form động, và template của loại hồ sơ.</para>
    /// </summary>
    [DataContract]
    public class Form
    {
        private ICollection<DocTypeForm> _docTypeForms;

        /// <summary>
        /// Lấy hoặc thiết lập Id form
        /// </summary>
        [DataMember]
        public Guid FormId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại form
        /// </summary>
        [DataMember]
        public int FormTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nhóm biểu mẫu
        /// </summary>
        [DataMember]
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
        [DataMember]
        public string FormName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả form
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Định nghĩa form dạng json
        /// </summary>
        [DataMember]
        public string Json { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Định nghĩa form dạng json
        /// </summary>
        [DataMember]
        public string MappingMaDinhDanhCP { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra form này là form chính
        /// </summary>
        [DataMember]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên file template là file doc, docx hoặc html.
        /// </summary>
        [DataMember]
        public string Template { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên mẫu phôi cho form động.
        /// </summary>
        [DataMember]
        public string EmbryonicPath { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên mẫu phôi cho form động.
        /// </summary>
        [DataMember]
        public string EmbryonicLocationName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Url cho Form theo Url.
        /// </summary>
        [DataMember]
        public string FormUrl { get; set; }

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
        [DataMember]
        public int IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo
        /// </summary>
        [DataMember]
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo
        /// </summary>
        [DataMember]
        public DateTime? CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người cập nhật cuối cùng
        /// </summary>
        [DataMember]
        public int? LastModifiedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày cập nhật cuối cùng
        /// </summary>
        [DataMember]
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
        public string ConfigFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompilationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ChildCompilationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FormCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int VersionForm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime VersionDateTime { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập Loại form
        /// </summary>
        public FormType FormType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<DocTypeForm> DocTypeForms
        {
            get { return _docTypeForms ?? (_docTypeForms = new List<DocTypeForm>()); }
            set { _docTypeForms = value; }
        }

        /// <summary>
        /// VuHQ REQ-02
        /// Json cấu hình của header (name, comment) của handsontable
        /// </summary>
        [DataMember]
        public string DefineFieldJson { get; set; }

        /// <summary>
        /// VuHQ REQ-02
        /// Json cấu hình của header (type, required, default value, readonly) của handsontable
        /// </summary>
        [DataMember]
        public string DefineConfigJson { get; set; }

        /// <summary>
        /// VuHQ REQ-02
        /// Json cấu hình giá trị mặc định của handsontable
        /// </summary>
        [DataMember]
        public string DefineValueJson { get; set; }

        /// <summary>
        /// VuHQ REQ-05
        /// Mẫu header của báo cáo
        /// </summary>
        [DataMember]
        public string FormHeader { get; set; }

        /// <summary>
        /// VuHQ REQ-05
        /// Mẫu footer của báo cáo
        /// </summary>
        [DataMember]
        public string FormFooter { get; set; }

        /// <summary>
        /// VuHQ REQ-05
        /// Ten table
        /// </summary>
        [DataMember]
        public string TableName { get; set; }

        /// <summary>
        /// VuHQ Phase 2 - REQ 0
        /// Ten table
        /// </summary>
        [DataMember]
        public string ExplicitTemplate { get; set; }

        /// <summary>
        /// VuHQ Phase 2 - REQ 0
        /// Ten table
        /// </summary>
        [DataMember]
        public string FormCodeCompilation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? FormCategoryId { get; set; } // 1: Loại thường, 2. Loại báo cáo chỉ tiêu, 3. Loại báo cáo tổng hợp

    }
}
