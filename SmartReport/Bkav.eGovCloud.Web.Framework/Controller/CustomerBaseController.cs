using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;

using System.Web.Mvc;
namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BaseController - public - Framework
    /// Access Modifiers:
    ///     * Inherit : System.Web.Mvc.Controller
    /// Create Date : 310712
    /// Author      : TrungVH
    /// Description : Base controller cho tất cả các controller. Hỗ trợ ghi log khi hệ thống lỗi và 1 số hàm tiện ích
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    //[CheckLicenseAttribute]
    public class CustomerBaseController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomerBaseController()
        {
            var authenticateSettings = DependencyResolver.Current.GetService<AuthenticationSettings>();
            ViewBag.SsoDomain = authenticateSettings.SingleSignOnDomain;

#if QuanTriTapTrungEdition
            ViewBag.IsQuanTriTapTrungEdition = true;
#endif
        }

        #region Ủy quyền khởi tạo

        /// <summary>
        /// Trả về Id của cán bộ đăng nhập hiện tại hoặc Id cán bộ ủy quyền khởi tạo
        /// </summary>
        /// <returns></returns>
        protected int GetUserCreatedId()
        {
            return User.GetUserId();
        }

        #endregion Ủy quyền khởi tạo
    }
}