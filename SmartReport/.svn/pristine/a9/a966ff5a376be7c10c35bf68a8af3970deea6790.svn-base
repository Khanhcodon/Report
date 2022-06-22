using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov Online.
    /// Project: eGov Online.
    /// Class: IGuideService - public - Model.
    /// Create Date: 170714.
    /// Author: TrinhNVd.
    /// Description: Chứa thông tin hướng dẫn.
    /// </summary>
    [FluentValidation.Attributes.Validator(typeof(GuideValidator))]
    public class GuideModel
    {
        /// <summary>
        /// Mã hướng dẫn
        /// </summary>
        public int GuideId { get; set; }

        /// <summary>
        /// Tên hướng dẫn
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Đường dẫn hướng dẫn
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Nội dung hướng dẫn
        /// </summary>
        public string Content { get; set; }
             
    }
}