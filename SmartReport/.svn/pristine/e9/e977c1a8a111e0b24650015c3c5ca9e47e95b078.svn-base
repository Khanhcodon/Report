using System;
using System.Security.Principal;

namespace Bkav.eGovCloud.Core.Fakes
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FakeIdentity - public - Core
    /// Access Modifiers: 
    ///     Inherit: IIdentity
    /// Create Date : 270612
    /// Author      : TrungVH
    /// </author>
    /// <summary>
    /// <para>Lớp giả Identity</para>
    /// (TrungVH@bkav.com - 270612)
    /// </summary>
    public class FakeIdentity : IIdentity
    {
        private readonly string _name;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="userName">Tên đăng nhập</param>
        public FakeIdentity(string userName)
        {
            _name = userName;
        }

#pragma warning disable 1591

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return !String.IsNullOrEmpty(_name); }
        }

        public string Name
        {
            get { return _name; }
        }

    }
}