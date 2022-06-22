using Bkav.eGovCloud.Entities.Enum;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : Template - public - Entity </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 190313</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Quản lý các mẫu template</para>
    /// <para> ( TienBV@bkav.com - 190313) </para>
    /// </summary>
    /// <remarks>
    /// (CuongNT@bkav.com - 110613)
    /// Mẫu in, email, sms có 3 loại MẪU CHUNG: Mẫu chung cho tất cả, Mẫu chung cho 1 lĩnh vực nào đó, mẫu chung cho 1 loại hồ sơ.
    /// Mỗi mẫu chung lại có các MẪU RIÊNG:
    /// - Mẫu chung cho tất cả: Mẫu riêng cho lĩnh vực, Mẫu riêng cho loại.
    /// - Mẫu chung cho 1 lĩnh vực nào đó: Mẫu riêng cho loại.
    /// - Mẫu chung cho 1 loại hồ sơ: Không có mẫu riêng.
    /// </remarks>
    [DataContract]
    public class Template
    {
        /// <summary>
        /// Key
        /// </summary>
        [DataMember]
        public int TemplateId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên mẫu
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nội dung mãua
        /// </summary>
        [DataMember]
        public string Content { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập lĩnh vực
        /// </summary>
        [DataMember]
        public int? DocFieldId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại hồ sơ cho mẫu
        /// </summary>
        [DataMember]
        public Guid? DoctypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu cha
        /// </summary>
        [DataMember]
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại mẫu:
        /// <para>  - 1: Phiếu in </para>
        /// <para>  - 2: Email </para>
        /// <para>  - 3: SMS </para>
        /// </summary>
        [DataMember]
        public int Type { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại mẫu:
        /// <para>  - 1: Phiếu in </para>
        /// <para>  - 2: Email </para>
        /// <para>  - 3: SMS </para>
        /// </summary>
        [DataMember]
        public TemplateType TypeInEnum
        {
            get { return (TemplateType)Type; }
            set { Type = (int)value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập quyền sử dụng mẫu
        /// </summary>
        public int Permission { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DocumentProcessType? PermissionInEnum
        {
            get
            {
                try
                {
                    return (DocumentProcessType)Permission;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập quyền sử dụng mẫu
        /// </summary>
        public int? CommonTemplate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public CommonTemplate? CommonTemplateInEnum
        {
            get
            {
                try
                {
                    return (CommonTemplate)CommonTemplate;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Tên file crystal report
        /// </summary>
        [DataMember]
        public string ContentFile { get; set; }

        /// <summary>
        /// Tên file trong thư mục
        /// </summary>
        [DataMember]
        public string ContentFileLocalName { get; set; }

        /// <summary>
        /// Câu sql lấy dữ liệu
        /// </summary>
        [DataMember]
        public string Sql { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái sử dụng của mẫu
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại hồ sơ
        /// </summary>
        [JsonIgnore]
        public virtual DocType Doctype { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại hồ sơ
        /// </summary>
        [JsonIgnore]
        public virtual DocField DocField { get; set; }

        /// <summary>
        /// HopCV:220116
        /// Lấy hoặc thiết lập tiêu đề khi gửi mail(chỉ dùng đối với mẫu gửi mail)
        /// </summary>
        public string TitleMail { get; set; }
    }
}