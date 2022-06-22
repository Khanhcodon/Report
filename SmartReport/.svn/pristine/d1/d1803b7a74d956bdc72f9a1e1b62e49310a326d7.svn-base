using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;

namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WebViewPage&lt;TModel&gt; - public - Framework
    /// Access Modifiers: 
    ///     * Inherit : System.Web.Mvc.WebViewPage&lt;TModel&gt;
    /// Create Date : 300712
    /// Author      : TrungVH
    /// Description : 1 Customer webviewpage dùng cho tất cả các View trong hệ thống
    /// </summary>
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
#pragma warning disable 1591
        private ResourceBll _resourceService;

        public override void InitHelpers()
        {
            base.InitHelpers();
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
        }

        public override string Layout
        {
            get
            {
                var layout = base.Layout;

                if (!string.IsNullOrEmpty(layout))
                {
                    var filename = System.IO.Path.GetFileNameWithoutExtension(layout);
                    var viewResult = ViewEngines.Engines.FindView(ViewContext.Controller.ControllerContext, filename, "");

                    if (viewResult.View != null)
                    {
                        var razorView = viewResult.View as RazorView;
                        if (razorView != null) layout = razorView.ViewPath;
                    }
                }

                return layout;
            }
            set
            {
                base.Layout = value;
            }
        }

#pragma warning restore 1591

        /// <summary>
        /// Lấy giá trị resource tương ứng với key
        /// </summary>
        /// <param name="resourceKey">Key resource</param>
        /// <param name="args">Tham số để format (string.Format)</param>
        /// <returns></returns>
        public string Localizer(string resourceKey, params object[] args)
        {
            var resFormat = _resourceService.GetResource(resourceKey);
            if (string.IsNullOrEmpty(resFormat))
            {
                return resourceKey;
            }
            return (args == null || args.Length == 0)
                    ? resFormat
                    : string.Format(resFormat, args);
        }

        /// <summary>
        /// Là phiên bản xử lý văn bản
        /// </summary>
        public bool IsXuLyVanBanEdition
        {
            get
            {
#if XuLyVanBanEdition
                return true;
#endif
                return false;
            }
        }

        /// <summary>
        /// Là phiên bản hồ sơ một cửa
        /// </summary>
        public bool IsHoSoMotCuaEdition
        {
            get
            {
#if HoSoMotCuaEdition
                return true;
#endif
                return false;
            }
        }

        /// <summary>
        /// Là phiên bản full
        /// </summary>
        public bool IsFullEdition
        {
            get
            {
#if !XuLyVanBanEdition && !HoSoMotCuaEdition
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// Là phiên bản hỗ trợ quản trị tập trung
        /// </summary>
        public bool IsQuanTriTapTrungEdition
        {
            get
            {
#if QuanTriTapTrungEdition
                return true;
#endif
                return false;
            }
        }

        /// <summary>
        /// Phiên bản nội bộ
        /// </summary>
        public bool IsBkavEdition
        {
            get
            {
#if BkavVersion
                return true;
#endif
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDevEdition
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
    }

    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : WebViewPage&lt;dynamic&gt; - public - Framework
    /// Access Modifiers: 
    ///     * Inherit : Bkav.eGovCloud.Web.Framework.WebViewPage&lt;dynamic&gt;
    /// Create Date : 300712
    /// Author      : TrungVH
    /// Description : 1 Customer webviewpage dùng cho tất cả các View trong hệ thống
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}