using System;
using System.IO;
using System.Net;
using System.Xml;

namespace Bkav.eGovCloud.Helper
{
    public class MailSoapHelper
    {
        private string _host;
        private string _scheme;
        private int _port;
        private HttpWebRequest _webRequest;
        private XmlDocument _soapEnvelopeRequest;

        public MailSoapHelper(string url)
        {
            var mailUrl = new Uri(url);
            _host = mailUrl.Host;
            _scheme = mailUrl.Scheme;
            _port = mailUrl.Port;
        }

        public string CallSearchRequest(string account, string token, string folder = null)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                folder = "inbox";
            }
            var action = MakeActionString("SearchRequest");
            _webRequest = CreateWebRequest(action);
            _soapEnvelopeRequest = CreateCheckMailSoapEnvelope(account, token, folder);
            InsertSoapEnvelopeIntoWebRequest();

            return GetResponseString();

        }

        public string CallTemplate()
        {
            string action = "https://AuthRequest@bmail.bkav.com/service/soap";
            _webRequest = CreateWebRequest(action);
            _soapEnvelopeRequest = CreateLoginSoapEnvelope("trinhnvd", "12345678");
            InsertSoapEnvelopeIntoWebRequest();
            return GetResponseString();
        }

        private HttpWebRequest CreateWebRequest(string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(action);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private string GetResponseString()
        {
            var result = "";
            if (_webRequest != null)
            {
                try
                {
                    // begin async call to web request.
                    var asyncResult = _webRequest.BeginGetResponse(null, null);

                    // suspend this thread until call is complete. You might want to
                    // do something usefull here like update your UI.
                    asyncResult.AsyncWaitHandle.WaitOne();

                    // get the response from the completed web request.
                    using (var webResponse = _webRequest.EndGetResponse(asyncResult))
                    {
                        using (var rd = new StreamReader(webResponse.GetResponseStream()))
                        {
                            result = rd.ReadToEnd();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }


        //<soap:Envelope xmlns:soap='http://www.w3.org/2003/05/soap-envelope'>
        //    <soap:Header>
        //    <context xmlns = 'urn:zimbra' >
        //        < session />
        //        < notify />
        //        < format type='js' />
        //        </context>
        //    </soap:Header>
        //    <soap:Body>
        //    <AuthRequest xmlns = 'urn:zimbraAccount' >
        //        < account by='name'>trinhnvd</account>
        //        <virtualHost>bmail.bkav.com</virtualHost>
        //        <password>BKAV</password>
        //        <type>simple</type>
        //        </AuthRequest>
        //    </soap:Body>
        //</soap:Envelope>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private XmlDocument CreateLoginSoapEnvelope(string username, string password)
        {

            var soapEnvelop = new XmlDocument();
            var stringXML = "<soap:Envelope xmlns:soap='http://www.w3.org/2003/05/soap-envelope'><soap:Header><context xmlns='urn:zimbra'><session /><notify /><format type='js'/></context></soap:Header><soap:Body><AuthRequest xmlns='urn:zimbraAccount'><account by='name'>"
                + username + "</account><virtualHost>bmail.bkav.com</virtualHost><password>"
                + password + "</password><type>simple</type></AuthRequest></soap:Body></soap:Envelope>";
            soapEnvelop.LoadXml(stringXML);

            return soapEnvelop;
        }

        private XmlDocument CreateCheckMailSoapEnvelope(string account, string token, string folder = "inbox")
        {
            XmlDocument soapEnvelop = new XmlDocument();

            var stringXML = "<soap:Envelope xmlns:soap='http://www.w3.org/2003/05/soap-envelope'><soap:Header><context xmlns='urn:zimbra'><format type='js'/><account by='name'>"
                + account + "</account><authToken>"
                + token + "</authToken></context></soap:Header><soap:Body><SearchRequest xmlns='urn:zimbraMail' sortBy='dateDesc' locale='en_US' offset='0'  limit='25' types='message'><query>in:"
                + "\"" + folder + "\"" + "</query><type>sipmle</type></SearchRequest></soap:Body></soap:Envelope>";

            soapEnvelop.LoadXml(stringXML);

            return soapEnvelop;
        }

        private void InsertSoapEnvelopeIntoWebRequest()
        {
            if (_webRequest == null)
            {
                throw new ArgumentNullException("_webRequest");
            }
            if (_soapEnvelopeRequest == null)
            {
                throw new ArgumentNullException("_soapEnvelopeRequest");
            }
            using (var stream = _webRequest.GetRequestStream())
            {
                _soapEnvelopeRequest.Save(stream);
            }
        }

        private string ReadNodeText(string stringXML, string nodeName)
        {
            var result = "";
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(stringXML);

            var elements = xmlDoc.GetElementsByTagName(nodeName);
            for (int i = 0; i < elements.Count; i++)
            {
                result += elements[i].InnerXml;
            }

            return result;
        }

        private string MakeActionString(string acction)
        {
            var result = "";
            result = string.Format("{0}://{1}@{2}", _scheme, acction, _host);
            if (_port != 80 && _port != 443)
            {
                result += ":" + _port;
            }
            return result + "/service/soap";
        }
    }
}