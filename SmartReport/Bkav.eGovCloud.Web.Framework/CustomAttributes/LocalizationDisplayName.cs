using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;

namespace Bkav.eGovCloud.Web.Framework
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : LocalizationDisplayName - public - Framework
    /// Access Modifiers: 
    ///     * Inherit : System.ComponentModel.DisplayNameAttribute
    /// Create Date : 300712
    /// Author      : TrungVH
    /// Description : 1 custom attribute hỗ trợ lấy ra resource tương ứng với 1 thuộc tính
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class LocalizationDisplayName : System.ComponentModel.DisplayNameAttribute
    {
        private string _resourceValue = string.Empty;
        private string _hint = string.Empty;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="resourceKey">Key của resource</param>
        public LocalizationDisplayName(string resourceKey)
            : base(resourceKey)
        {
            ResourceKey = resourceKey;
        }

        /// <summary>
        /// Lấy hoặc thiết lập Key của resource
        /// </summary>
        public string ResourceKey { get; set; }

        #pragma warning disable 1591
        public override string DisplayName
        {
            get
            {
                _resourceValue = DependencyResolver.Current.GetService<ResourceBll>()
                                    .GetResource(ResourceKey, true, ResourceKey);
                return _resourceValue;
            }
        }
        #pragma warning restore 1591

        /// <summary>
        /// Lấy ra gợi ý về thuộc tính (Sử dụng cho extension LabelHintFor)
        /// </summary>
        public string Hint
        {
            get
            {
                _hint = DependencyResolver.Current.GetService<ResourceBll>()
                            .GetResource(ResourceKey + ".Hint", false, returnEmptyIfNotFound:true);
                return _hint;
            }
        }
    }
}
