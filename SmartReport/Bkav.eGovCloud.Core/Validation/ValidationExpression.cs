namespace Bkav.eGovCloud.Core.Validation
{
    /// <summary>
    /// Biểu thức kiểm tra dữ liệu đầu vào cho hệ thống
    /// </summary>
    public static class ValidationExpression
    {
        /// <summary>
        /// Chuỗi biểu thức chính quy kiểm tra một hoặc nhiều email (phân cách bằng dấu ; hoặc ,)
        /// </summary>
        public const string MultiEmailRegex =
            @"^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))([;,](?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\])))*$";

        /// <summary>
        /// Chuỗi biểu thức chính quy kiểm tra email
        /// </summary>
        public const string EmailRegex =
            @"^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))$";

        /// <summary>
        /// Chuỗi biểu thức chính quy kiểm tra ngày giờ
        /// </summary>
        public const string DateTimeRegex =
            @"^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$";

        /// <summary>
        /// Chuỗi biểu thức chính quy kiểm tra ip
        /// </summary>
        public const string IpRegex =
            @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$";

        /// <summary>
        /// Chuỗi biểu thức chính quy kiểm tra domain
        /// </summary>
        public const string DomainRegex =
            @"^[a-zA-Z0-9]+([a-zA-Z0-9\-\.]+)?\.(vn|com|org|net|mil|edu|VN|COM|ORG|NET|MIL|EDU)$";

        /// <summary>
        /// Chuỗi biểu thức chính quy kiểm tra một hoặc nhiều số điện thoại, di động (phân cách bằng dấu ; hoặc ,)
        /// </summary>
        public const string MultiPhoneRegex = @"^(\+?[0-9. ()-]{10,12})([;,](\+?[0-9. ()-]{10,12}))*$";

        /// <summary>
        /// Chuỗi biểu thức chính quy kiểm tra số điện thoại, di động
        /// </summary>
        public const string PhoneRegex = @"^\+?[0-9. ()-]{10,12}$";

        /// <summary>
        /// Chuỗi biểu thức chính quy kiểm tra chứng minh thư nhân dân
        /// </summary>
        public const string CmndRegex = @"^(\d{9}|\d{12})$";

        /// <summary>
        /// Chuỗi biểu thức chính quy kiểm tra số kí hiệu văn bản/hồ sơ
        /// </summary>
        public const string DocumentCodeRegex = @".*\$N\$.*";

        /// <summary>
        /// Chuỗi biểu thức chính quy kiểm tra mã các quyền định nghĩa trên hệ thống
        /// </summary>
        public const string RoleKeyRegex = @"^(?=.*[A-Za-z0-9])[\S]*$";

        /// <summary>
        /// Chuỗi biểu thức chính quy kiểm tra mã màu
        /// </summary>
        public const string ColorCodesRegex = @"^\#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$";
    }
}
