using System.Text.RegularExpressions;

namespace Bkav.eGovCloud.Core.Validation
{
    /// <author>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ValidateExtension - public - Core</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 140613</para>
    /// <para>Author      : TrungVH</para>
    /// </author>
    /// <summary>
    /// <para>Thư viện mở rộng kiểm tra dữ liệu đầu vào cho hệ thống</para>
    /// </summary>
    public static class ValidateExtension
    {
        /// <summary>
        /// Kiểm tra chuỗi đầu vào có phải là một hoặc nhiều email hay không (phân cách bằng dấu ; hoặc ,)
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns>true: đúng, false: sai</returns>
        public static bool IsMatchMultiEmail(this string input)
        {
            var regex = new Regex(ValidationExpression.MultiEmailRegex);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Kiểm tra chuỗi đầu vào có phải là một email hay không
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns>true: đúng, false: sai</returns>
        public static bool IsMatchEmail(this string input)
        {
            var regex = new Regex(ValidationExpression.EmailRegex);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Kiểm tra chuỗi đầu vào có phải là ngày tháng hay không
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns>true: đúng, false: sai</returns>
        public static bool IsMatchDateTime(this string input)
        {
            var regex = new Regex(ValidationExpression.DateTimeRegex);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Kiểm tra chuỗi đầu vào có phải là một ip hay không
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns>true: đúng, false: sai</returns>
        public static bool IsMatchIp(this string input)
        {
            var regex = new Regex(ValidationExpression.IpRegex);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Kiểm tra chuỗi đầu vào có phải là một domain hay không
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns>true: đúng, false: sai</returns>
        public static bool IsMatchDomain(this string input)
        {
            var regex = new Regex(ValidationExpression.DomainRegex);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Kiểm tra chuỗi đầu vào có phải là một hoặc nhiều số điện thoại, di động hay không (phân cách bằng dấu ; hoặc ,)
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns>true: đúng, false: sai</returns>
        public static bool IsMatchMultiPhone(this string input)
        {
            var regex = new Regex(ValidationExpression.MultiPhoneRegex);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Kiểm tra chuỗi đầu vào có phải là một số điện thoại, di động hay không
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns>true: đúng, false: sai</returns>
        public static bool IsMatchPhone(this string input)
        {
            var regex = new Regex(ValidationExpression.PhoneRegex);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Kiểm tra chuỗi đầu vào có phải là chứng minh thư nhân dân hay không
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns>true: đúng, false: sai</returns>
        public static bool IsMatchCmnd(this string input)
        {
            var regex = new Regex(ValidationExpression.CmndRegex);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Kiểm tra chuỗi đầu vào có phải là một số kí hiệu hay không
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns>true: đúng, false: sai</returns>
        public static bool IsMatchDocumentCode(this string input)
        {
            var regex = new Regex(ValidationExpression.DocumentCodeRegex);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Kiểm tra chuỗi đầu vào có phải là một mã quyền định nghĩa trên hệ thống hay không
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns>true: đúng, false: sai</returns>
        public static bool IsMatchRoleKey(this string input)
        {
            var regex = new Regex(ValidationExpression.RoleKeyRegex);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Kiểm tra chuỗi đầu vào có phải là một mã màu hay không
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns>true: đúng, false: sai</returns>
        public static bool IsMatchColorCodes(this string input)
        {
            var regex = new Regex(ValidationExpression.ColorCodesRegex);
            return regex.IsMatch(input);
        }
    }
}
