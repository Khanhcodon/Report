using System.ComponentModel;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Kiểu captcha
    /// </summary>
    public enum CaptchaType
    {
        /// <summary>
        /// Không chọn captcha
        /// </summary>
        [Description("egovcloud.enum.captchatype.nochoose")]
        NoChoose = 0,

        /// <summary>
        /// Toán học
        /// <example>5 + 5 = ...</example>
        /// </summary>
        [Description("egovcloud.enum.captchatype.math")]
        Math = 1,

        /// <summary>
        /// Nhập chữ bình thường
        /// <example>NCAJJA</example>
        /// </summary>
        [Description("egovcloud.enum.captchatype.text")]
        Text = 2,

    }
}