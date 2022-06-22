using System.Linq;
using System.Security.Principal;

namespace Bkav.eGovCloud.Core.Fakes
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FakePrincipal - public - Core
    /// Access Modifiers: 
    ///     Inherit: IPrincipal
    /// Create Date : 270612
    /// Author      : TrungVH
    /// </author>
    /// <summary>
    /// <para>Lớp giả Principal</para>
    /// (TrungVH@bkav.com - 270612)
    /// </summary>
    public class FakePrincipal : IPrincipal
    {
        private readonly IIdentity _identity;
        private readonly string[] _roles;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="identity">Identity</param>
        /// <param name="roles">Các nhóm quyền</param>
        public FakePrincipal(IIdentity identity, string[] roles)
        {
            _identity = identity;
            _roles = roles;
        }

        #pragma warning disable 1591

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            return _roles != null && _roles.Contains(role);
        }
    }
}