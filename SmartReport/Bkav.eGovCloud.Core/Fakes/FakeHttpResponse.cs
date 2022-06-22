using System.Text;
using System.Web;

namespace Bkav.eGovCloud.Core.Fakes
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : FakeHttpResponse - public - Core
    /// Access Modifiers: 
    ///     Inherit: HttpResponseBase
    /// Create Date : 270612
    /// Author      : TrungVH
    /// </author>
    /// <summary>
    /// <para>Lớp giả HttpResponse</para>
    /// (TrungVH@bkav.com - 270612)
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class FakeHttpResponse : HttpResponseBase
    {
        private readonly StringBuilder _outputString = new StringBuilder();

        /// <summary>
        /// 
        /// </summary>
        public string ResponseOutput
        {
            get { return _outputString.ToString(); }
        }

        #pragma warning disable 1591

        public override int StatusCode { get; set; }

        public override string RedirectLocation { get; set; }

        public override void Write(string s)
        {
            _outputString.Append(s);
        }

        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }
    }
}