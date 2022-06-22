using System;

namespace Bkav.eGovCloud.Core.Exceptions
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : EgovException - public - Core
    /// Access Modifiers: 
    ///     *Inherit: Exception
    /// Create Date : 200812
    /// Author      : TrungVH
    /// </author>
    /// <summary>
    /// <para>1 custom exception để giúp cho việc bắt lỗi chính xác hơn (cụ thể là khi gặp lỗi nào mà đã lường trước mà muốn bắt được lỗi đó trên tầng presentation và thông báo ra lỗi đó thì sẽ dùng class này)</para>
    /// (TrungVH@bkav.com - 200812)
    /// </summary>
    [Serializable]
    public class EgovException : Exception
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="message">Tóm tắt lỗi</param>
        public EgovException(string message) : base(message)
        {
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
		/// <param name="messageFormat">Định dạng tóm tắt lỗi (string format).</param>
		/// <param name="args">Tham số</param>
        public EgovException(string messageFormat, params object[] args)
			: base(string.Format(messageFormat, args))
		{
		}
        
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="message">Tóm tắt lỗi</param>
        /// <param name="inner">Ngoại lệ</param>
        public EgovException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
