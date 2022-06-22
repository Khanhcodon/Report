using System.Collections.Generic;

namespace Bkav.eGovCloud.Core.Bmail.SOAP
{
#pragma warning disable 1591
    public class Email
    {
        public string a { get; set; }
        public string d { get; set; }
        public string p { get; set; }
        public string t { get; set; }
    }

    public class BMail
    {
        public int s { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public double d { get; set; }
        /// <summary>
        /// LocationId
        /// </summary>
        public string l { get; set; }

        /// <summary>
        /// Conversion Id
        /// </summary>
        public string cid { get; set; }

        /// <summary>
        /// f==u => Unread
        /// </summary>
        public string f { get; set; }
        public int rev { get; set; }
        /// <summary>
        /// MailId
        /// </summary>
        public string id { get; set; }
        public IEnumerable<Email> e { get; set; }
        //public int score { get; set; }
        public bool cm { get; set; }
        public string su { get; set; }
        public string fr { get; set; }
        public string sf { get; set; }
    }

    public class SearchResponse
    {
        public string sortBy { get; set; }
        public int offset { get; set; }
        public IEnumerable<BMail> m { get; set; }
        public bool more { get; set; }
        public string _jsns { get; set; }
    }

    public class Body
    {
        public SearchResponse SearchResponse { get; set; }
    }

    public class context
    {
        public string _jsns { get; set; }
    }

    public class Header
    {
        public context context;
    }

    public class MailSoapSearchResponse
    {
        public Header Header;
        public Body Body;
        public string _jsns;
    }
}