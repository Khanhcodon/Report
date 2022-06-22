using System;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
	/// Các Trạng thái văn bản liên thông đến đi
	/// </summary>
    [Flags]
    public enum LienThongStatus
	{
		/// <summary>
		/// Trạng thái xác định văn bản đang có yêu cầu thu hồi từ bên gửi
		/// </summary>
        YeuCauThuHoi = 1,

		/// <summary>
		/// Trạng thái xác định văn bản từ chối ko cho thu hồi
		/// </summary>
		TuChoiThuHoi = 2,

		/// <summary>
		/// Trạng thái xác định văn bản đã đồng ý thu hồi
		/// </summary>
		DongYThuHoi = 3
    }
}
