namespace Bkav.eGovCloud.Entities.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : AccountDomain - public - Entity
    /// Access Modifiers: 
    /// Create Date : 120912
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng AccountDomain trong CSDL
    /// </summary>
    public class AccountDomain
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id mapping giữa người dùng và domain
        /// </summary>
        public int AccountDomainId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người dùng
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id domain
        /// </summary>
        public int DomainId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Người dùng
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Domain
        /// </summary>
        public Domain Domain { get; set; }
    }
}
