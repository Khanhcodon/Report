using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;

namespace Bkav.eGovCloud.Core.Fakes
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FakeHttpContext - public - Core
    /// Access Modifiers: 
    ///     Inherit: HttpContextBase
    /// Create Date : 270612
    /// Author      : TrungVH
    /// </author>
    /// <summary>
    /// <para>Lớp giả HttpContext</para>
    /// (TrungVH@bkav.com - 270612)
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class FakeHttpContext : HttpContextBase
    {
        private readonly HttpCookieCollection _cookies;
        private readonly NameValueCollection _formParams;
        private IPrincipal _principal;
        private readonly NameValueCollection _queryStringParams;
        private readonly string _relativeUrl;
        private readonly string _method;
        private readonly SessionStateItemCollection _sessionItems;
        private readonly NameValueCollection _serverVariables;
        private HttpResponseBase _response;
        private HttpRequestBase _request;
        private readonly Dictionary<object, object> _items;

        /// <summary>
        /// Lấy ra FakeHttpContext với đường đẫn tương đối là gốc
        /// </summary>
        /// <returns>FakeHttpContext</returns>
        public static FakeHttpContext Root()
        {
            return new FakeHttpContext("~/");
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="relativeUrl">Đường dẫn tương đối</param>
        /// <param name="method"></param>
        public FakeHttpContext(string relativeUrl, string method)
            : this(relativeUrl, method, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="relativeUrl">Đường dẫn tương đối</param>
        public FakeHttpContext(string relativeUrl)
            : this(relativeUrl, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="relativeUrl">Đường dẫn tương đối</param>
        /// <param name="principal">Principal</param>
        /// <param name="formParams">Form Params</param>
        /// <param name="queryStringParams">QueryStringParams</param>
        /// <param name="cookies">Cookies</param>
        /// <param name="sessionItems">SessionItems</param>
        /// <param name="serverVariables">ServerVariables</param>
        public FakeHttpContext(string relativeUrl, 
            IPrincipal principal, NameValueCollection formParams,
            NameValueCollection queryStringParams, HttpCookieCollection cookies,
            SessionStateItemCollection sessionItems, NameValueCollection serverVariables)
            : this(relativeUrl, null, principal, formParams, queryStringParams, cookies, sessionItems, serverVariables)
        {
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="relativeUrl">Đường dẫn tương đối</param>
        /// <param name="method">Method</param>
        /// <param name="principal">Principal</param>
        /// <param name="formParams">Form Params</param>
        /// <param name="queryStringParams">QueryStringParams</param>
        /// <param name="cookies">Cookies</param>
        /// <param name="sessionItems">SessionItems</param>
        /// <param name="serverVariables">ServerVariables</param>
        public FakeHttpContext(string relativeUrl, string method,
            IPrincipal principal, NameValueCollection formParams,
            NameValueCollection queryStringParams, HttpCookieCollection cookies,
            SessionStateItemCollection sessionItems, NameValueCollection serverVariables)
        {
            _relativeUrl = relativeUrl;
            _method = method;
            _principal = principal;
            _formParams = formParams;
            _queryStringParams = queryStringParams;
            _cookies = cookies;
            _sessionItems = sessionItems;
            _serverVariables = serverVariables;

            _items = new Dictionary<object, object>();
        }

        /// <summary>
        /// Thiết lập request
        /// </summary>
        /// <param name="request">Request</param>
        public void SetRequest(HttpRequestBase request)
        {
            _request = request;
        }

        /// <summary>
        /// Thiết lập response
        /// </summary>
        /// <param name="response">Response</param>
        public void SetResponse(HttpResponseBase response)
        {
            _response = response;
        }

        #pragma warning disable 1591

        public override HttpRequestBase Request
        {
            get
            {
                return _request ??
                       new FakeHttpRequest(_relativeUrl, _method, _formParams, _queryStringParams, _cookies, _serverVariables);
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return _response ?? new FakeHttpResponse();
            }
        }

        public override IPrincipal User
        {
            get { return _principal; }
            set { _principal = value; }
        }

        public override HttpSessionStateBase Session
        {
            get { return new FakeHttpSessionState(_sessionItems); }
        }

        public override System.Collections.IDictionary Items
        {
            get
            {
                return _items;
            }
        }

        public override bool SkipAuthorization { get; set; }

        public override object GetService(Type serviceType)
        {
            return null;
        }
    }
}