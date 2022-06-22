using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : JsonpResult - public - Framework
    /// Access Modifiers: 
    ///     * Inherit : JsonResult
    /// Create Date : 031012
    /// Author      : TrungVH
    /// Description : Class hỗ trợ trả về kết quả dạng json khi gọi ajax từ một domain khác
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class JsonpResult : JsonResult
    {
        /// <summary>
        /// Lấy hoặc thiết lập Query string json callback
        /// </summary>
        public string JsonCallback { get; set; }

        #pragma warning disable 1591
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            JsonCallback = context.HttpContext.Request["jsoncallback"];

            if (string.IsNullOrEmpty(JsonCallback))
                JsonCallback = context.HttpContext.Request["callback"];

            if (string.IsNullOrEmpty(JsonCallback))
                throw new ArgumentNullException("context");

            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
                var serializer = new JavaScriptSerializer();
                response.Write(string.Format("{0}({1});", JsonCallback, serializer.Serialize(Data)));
            }
        }
    }
}
