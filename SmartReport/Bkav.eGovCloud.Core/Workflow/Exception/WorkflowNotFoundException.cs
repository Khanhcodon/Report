using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Core.Workflow
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : EgovException - public - Core
    /// Access Modifiers: 
    ///     *Inherit: Exception
    /// Create Date : 270613
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Ngoại lệ được ném khi mở một văn bản không có quyền xem</para>
    /// (CuongNT@bkav.com - 270613)
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    [Serializable]
    public class WorkflowNotFoundException : Exception
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        public WorkflowNotFoundException()
        {
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="message">Tóm tắt lỗi</param>
        public WorkflowNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="message">Tóm tắt lỗi</param>
        /// <param name="inner">Ngoại lệ</param>
        public WorkflowNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
