
namespace Bkav.eGovCloud.Entities.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Server - public - Entity
    /// Access Modifiers: 
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Server trong CSDL
    /// </summary>
    public class Server
    {
        //private ICollection<Domain> _domains;

        /// <summary>
        /// Lấy hoặc thiết lập Id máy chủ
        /// </summary>
        public int ServerId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên domain được public
        /// </summary>
        public string PublicDomain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ip
        /// </summary>
        public string PrivateIp { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ghi chú
        /// </summary>
        public string Description { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập Các domain liên quan
        ///// </summary>
        //public virtual ICollection<Domain> Domains
        //{
        //    get { return _domains ?? (_domains = new List<Domain>()); }
        //    set { _domains = value; }
        //}
    }
}
