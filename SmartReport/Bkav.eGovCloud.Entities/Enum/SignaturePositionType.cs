using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : SignaturePosition - public - Entity
    /// Access Modifiers: 
    /// Create Date : 150414
    /// Author      : HopCV
    /// Description : Vị trí của chữ ký người dùng
    /// </summary>
    public enum SignaturePositionType
    {
        /// <summary>
        /// Bên trái
        /// </summary>
        Left = 0,

        /// <summary>
        /// Bên phải
        /// </summary>
        Right = 1,

        /// <summary>
        /// Bên trên
        /// </summary>
        Above = 2,

        /// <summary>
        /// Bên dưới
        /// </summary>
        Below = 3
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : SignatureType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 150414
    /// Author      : HopCV
    /// Description : Loại chữ ký của người dùng
    /// </summary>
    public enum SignatureType
    {
        /// <summary>
        /// Chứ ký dạng text
        /// </summary>
        Text = 1,

        /// <summary>
        /// Chữ ký ảnh
        /// </summary>
        Image = 0
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : SignatureFindType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 150414
    /// Author      : HopCV
    /// Description : Vị trí của chữ ký người dùng
    /// </summary>
    public enum SignatureFindType
    {
        /// <summary>
        /// Tìm từ trên xuống
        /// </summary>
        TrenXuong = 0,

        /// <summary>
        /// Tìm từ dưới lên
        /// </summary>
        DuoiLen = 1
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : SignatureTypeDispplayType - public - Entity
    /// Access Modifiers: 
    /// Create Date : 150414
    /// Author      : HopCV
    /// Description : Vị trí của chữ ký người dùng
    /// </summary>
    public enum SignatureTypeDispplayType
    {
        /// <summary>
        ///  Hiển thị text o chứ ký dạng ảnh 
        /// </summary>
        Show = 1,

        /// <summary>
        /// Ẩn text o chứ ký dạng ảnh 
        /// </summary>
        Hidden = 0
    }
}
