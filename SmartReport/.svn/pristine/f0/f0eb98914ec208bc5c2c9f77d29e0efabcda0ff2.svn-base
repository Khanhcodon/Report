using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Core.Document
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
    public class ViewPermissionException : Exception
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        public ViewPermissionException()
        {
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="message">Tóm tắt lỗi</param>
        public ViewPermissionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="message">Tóm tắt lỗi</param>
        /// <param name="inner">Ngoại lệ</param>
        public ViewPermissionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
