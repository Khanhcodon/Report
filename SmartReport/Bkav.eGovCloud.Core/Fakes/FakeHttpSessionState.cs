using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

namespace Bkav.eGovCloud.Core.Fakes
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FakeHttpSessionState - public - Core
    /// Access Modifiers: 
    ///     Inherit: HttpSessionStateBase
    /// Create Date : 270612
    /// Author      : TrungVH
    /// </author>
    /// <summary>
    /// <para>Lớp giả HttpSessionState</para>
    /// (TrungVH@bkav.com - 270612)
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class FakeHttpSessionState : HttpSessionStateBase
    {
        private readonly SessionStateItemCollection _sessionItems;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="sessionItems">sessionItems</param>
        public FakeHttpSessionState(SessionStateItemCollection sessionItems)
        {
            _sessionItems = sessionItems;
        }

#pragma warning disable 1591

        public override int Count
        {
            get { return _sessionItems.Count; }
        }

        public override NameObjectCollectionBase.KeysCollection Keys
        {
            get { return _sessionItems.Keys; }
        }

        public override object this[string name]
        {
            get { return _sessionItems[name]; }
            set { _sessionItems[name] = value; }
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của session
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>True nếu tồn tại và ngược lại</returns>
        public bool Exists(string key)
        {
            return _sessionItems[key] != null;
        }

        public override object this[int index]
        {
            get { return _sessionItems[index]; }
            set { _sessionItems[index] = value; }
        }

        public override void Add(string name, object value)
        {
            _sessionItems[name] = value;
        }

        public override IEnumerator GetEnumerator()
        {
            return _sessionItems.GetEnumerator();
        }

        public override void Remove(string name)
        {
            _sessionItems.Remove(name);
        }
    }
}