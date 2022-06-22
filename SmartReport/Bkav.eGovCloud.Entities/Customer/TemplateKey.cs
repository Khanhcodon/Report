using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : Entity - public  </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 130313</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Quản lý các mẫu key cho template</para>
    /// <para> ( TienBV@bkav.com - 130313) </para>
    /// </summary>
    public class TemplateKey
    {
        /// <summary>
        /// Get or set the primary key.
        /// </summary>
        public int TemplateKeyId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập form id
        /// </summary>
        public Guid? FormId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại hồ sơ
        /// </summary>
        public Guid? DoctypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên key
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mã key
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn dữ liệu cho key
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu hiển thị cho key
        /// </summary>
        public string HtmlTemplate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại key: 1. key đặc biệt, 2. key chung
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại key: 1. key đặc biệt, 2. key chung
        /// </summary>
        public TemplateKeyType TypeInEnum
        {
            get { return (TemplateKeyType) Type; }
            set { Type = (int) value; }
        }

        /// <summary>
        /// lấy hoặc thiết lập sử dụng key.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Là key custom từ form
        /// </summary>
        public bool IsCustomKey { get; set; }

        /// <summary>
        /// Control Id trong form.
        /// </summary>
        public Guid? KeyIdInForm { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại hồ sơ
        /// </summary>
        public virtual DocType Doctype { get; set; }
        /// <summary>
        /// Lấy hoặc thiết lập category
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// ReportQuery, ReportGroupQuery
        /// </summary>
        public string ReportQueryId { get; set; }
    }
}
