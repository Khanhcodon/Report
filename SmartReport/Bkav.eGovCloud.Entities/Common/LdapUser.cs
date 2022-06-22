namespace Bkav.eGovCloud.Entities.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LdapUser - public - Entity
    /// Access Modifiers: 
    /// Create Date : 170812
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng các thuộc tính trong LDAP
    /// </summary>
    public class LdapUser
    {
        private string _fullname;
        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên người dùng
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Họ người dùng
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Họ và tên người dùng
        /// </summary>
        public string FullName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_fullname))
                {
                    return LastName + " " + FirstName;
                }
                return _fullname;
            }
            set { _fullname = value; }
        }
    }
}
