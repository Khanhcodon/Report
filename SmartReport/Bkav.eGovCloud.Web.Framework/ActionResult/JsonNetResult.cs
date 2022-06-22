using System;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Core.Utils;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// 
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class JsonNetResult : JsonResult
    {
        // public JsonRequestBehavior JsonRequestBehavior { get; set; }
        // public Encoding ContentEncoding { get; set; }
        // public string ContentType { get; set; }
        // public object Data { get; set; }
        // public JsonSerializerSettings SerializerSettings { get; set; }
        // public Formatting Formatting { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JsonNetResult()
        {
            // Formatting = Formatting.None;
            // SerializerSettings = new JsonSerializerSettings();
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet
                    && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType)
                    ? ContentType
                    : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                var writer = new JsonTextWriter(response.Output);// { Formatting = Formatting };
                var serializer = Json2.JsSerializer();//JsonSerializer.Create(SerializerSettings);
                serializer.Serialize(writer, Data);
                writer.Flush();
            }
        }
    }
}
