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
    /// <para> Quản lý các mẫu category cho template</para>
    /// <para> ( TienBV@bkav.com - 130313) </para>
    /// </summary>
    public class TemplateKeyCategory
    {
        /// <summary>
        /// Get or set the primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên key
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// lấy hoặc thiết lập sử dụng key.
        /// </summary>
        public bool IsActive { get; set; }

    }
}
