using System.Web.Mvc;

namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : JsonpExtension - public - Framework
    /// Access Modifiers:
    /// Create Date : 031012
    /// Author      : TrungVH
    /// Description : Hàm mở rộng cho JsonpResult
    /// </summary>
    public static class JsonpExtension
    {
        /// <summary>
        /// Lấy ra kết quả dạng json
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="data">Data</param>
        /// <returns>JsonpResult</returns>
        public static JsonpResult Jsonp(this System.Web.Mvc.Controller controller, object data)
        {
            var result = new JsonpResult { Data = data };
            return result;
        }

        /// <summary>
        /// Lấy ra kết quả dạng json
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="data">Data</param>
        /// <param name="behavior">Behavior</param>
        /// <returns>JsonpResult</returns>
        public static JsonpResult Jsonp(this System.Web.Mvc.Controller controller, object data, JsonRequestBehavior behavior)
        {
            var result = new JsonpResult { Data = data, JsonRequestBehavior = behavior};
            return result;
        }
    }
}
